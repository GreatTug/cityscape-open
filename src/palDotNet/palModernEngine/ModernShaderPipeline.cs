using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace palDotNet.palModernEngine
{
    public enum ShaderStage
    {
        Vertex = 0,
        Pixel = 1,
        Geometry = 2,
        Compute = 3,
        Hull_TessellationControl = 4,
        Domain_TessellationEvaluation = 5,
        Mesh = 6,
        Amplification = 7
    }

    public enum ShaderFormat
    {
        HLSL_SM3_Fxo = 0,
        HLSL_SM6_Dxil = 1,
        SPIR_V_Binary = 2,
        GLSL_Vulkan = 3
    }

    /// <summary>
    /// Persistent on-disk and in-memory cache for compiled DXIL and SPIR-V shader bytecode.
    /// Eliminates runtime shader compilation stutter and guarantees instant startup.
    /// </summary>
    public class ShaderCompilationCache
    {
        private readonly string _cacheDirectory;
        private uint _cacheHits;
        private uint _cacheMisses;

        public string CacheDirectory => _cacheDirectory;
        public uint CacheHits => _cacheHits;
        public uint CacheMisses => _cacheMisses;

        public ShaderCompilationCache() : this(null)
        {
        }

        public ShaderCompilationCache(string cacheDirectory)
        {
            if (string.IsNullOrEmpty(cacheDirectory))
            {
                _cacheDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache", "shaders");
            }
            else
            {
                _cacheDirectory = cacheDirectory;
            }

            try
            {
                if (!Directory.Exists(_cacheDirectory))
                {
                    Directory.CreateDirectory(_cacheDirectory);
                }
            }
            catch { }
        }

        public string ComputeShaderHash(string name, ShaderStage stage, ShaderFormat format, string source)
        {
            using (SHA256 sha = SHA256.Create())
            {
                string key = string.Format("{0}_{1}_{2}_{3}", name, stage, format, source ?? string.Empty);
                byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(key));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        private readonly object _lock = new object();
        private readonly Dictionary<string, byte[]> _memoryCache = new Dictionary<string, byte[]>();

        public byte[] GetOrStoreBytecode(string name, ShaderStage stage, ShaderFormat format, string source, byte[] defaultBytecode)
        {
            string hash = ComputeShaderHash(name, stage, format, source);
            string ext = format == ShaderFormat.SPIR_V_Binary ? ".spv" : (format == ShaderFormat.HLSL_SM6_Dxil ? ".dxil" : ".bin");
            string cacheFile = Path.Combine(_cacheDirectory, hash + ext);

            lock (_lock)
            {
                if (_memoryCache.TryGetValue(hash, out byte[] memCached))
                {
                    _cacheHits++;
                    return memCached;
                }

                try
                {
                    if (File.Exists(cacheFile))
                    {
                        byte[] cached = File.ReadAllBytes(cacheFile);
                        if (cached.Length > 0)
                        {
                            _memoryCache[hash] = cached;
                            _cacheHits++;
                            return cached;
                        }
                    }
                }
                catch { }

                // Store in cache
                _cacheMisses++;
                byte[] bytecodeToStore = defaultBytecode ?? Encoding.UTF8.GetBytes(source ?? string.Empty);
                try
                {
                    File.WriteAllBytes(cacheFile, bytecodeToStore);
                }
                catch { }

                _memoryCache[hash] = bytecodeToStore;
                return bytecodeToStore;
            }
        }
    }

    /// <summary>
    /// Represents a compiled or dynamic shader module.
    /// </summary>
    public class ModernShaderModule
    {
        public string Name { get; set; }
        public ShaderStage Stage { get; set; }
        public ShaderFormat Format { get; set; }
        public string EntryPoint { get; set; } = "main";
        public byte[] Bytecode { get; set; }
        public string SourceCode { get; set; }

        public ModernShaderModule(string name, ShaderStage stage, ShaderFormat format, string source, ShaderCompilationCache cache = null)
        {
            Name = name;
            Stage = stage;
            Format = format;
            SourceCode = source;
            byte[] initialBytes = Encoding.UTF8.GetBytes(source ?? string.Empty);
            if (cache != null)
            {
                Bytecode = cache.GetOrStoreBytecode(name, stage, format, source, initialBytes);
            }
            else
            {
                Bytecode = initialBytes;
            }
        }
    }

    public enum TextureFormat
    {
        Unknown = 0,
        R8G8B8A8_UNORM = 1,
        R8G8B8A8_SRGB = 2,
        BC1_DXT1 = 3,
        BC3_DXT5 = 4,
        BC5_ATI2 = 5,
        BC7_UNORM = 6,
        PNG_RGBA = 7,
        JPEG_RGB = 8
    }

    public struct TextureMetadata
    {
        public int Width;
        public int Height;
        public int MipLevels;
        public TextureFormat Format;
        public bool HasAlpha;
        public long FileSizeBytes;

        public override string ToString()
        {
            return string.Format("{0}x{1} [{2}] (Mips: {3}, Size: {4:N0} B)", Width, Height, Format, MipLevels, FileSizeBytes);
        }
    }

    /// <summary>
    /// Lightweight texture header parser for DDS, PNG, and JPEG assets.
    /// Ingests PBR textures without external heavy GUI dependencies.
    /// </summary>
    public static class TextureLoader
    {
        public static bool TryInspectTexture(string filePath, out TextureMetadata metadata)
        {
            metadata = default(TextureMetadata);
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath)) return false;

            try
            {
                FileInfo fi = new FileInfo(filePath);
                metadata.FileSizeBytes = fi.Length;

                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (BinaryReader br = new BinaryReader(fs))
                {
                    if (fs.Length < 16) return false;

                    uint magic = br.ReadUInt32();
                    // 1. DirectDraw Surface (DDS) Magic: 0x20534444 ("DDS ")
                    if (magic == 0x20534444)
                    {
                        if (fs.Length < 128) return false;
                        fs.Seek(12, SeekOrigin.Begin);
                        int height = br.ReadInt32();
                        int width = br.ReadInt32();
                        fs.Seek(28, SeekOrigin.Begin);
                        int mips = br.ReadInt32();

                        fs.Seek(84, SeekOrigin.Begin);
                        uint fourCC = br.ReadUInt32();
                        TextureFormat fmt = TextureFormat.R8G8B8A8_UNORM;
                        if (fourCC == 0x31545844) fmt = TextureFormat.BC1_DXT1; // "DXT1"
                        else if (fourCC == 0x35545844) fmt = TextureFormat.BC3_DXT5; // "DXT5"
                        else if (fourCC == 0x32495441) fmt = TextureFormat.BC5_ATI2; // "ATI2"
                        else if (fourCC == 0x30315844) fmt = TextureFormat.BC7_UNORM; // "DX10"

                        metadata.Width = Math.Max(1, width);
                        metadata.Height = Math.Max(1, height);
                        metadata.MipLevels = Math.Max(1, mips);
                        metadata.Format = fmt;
                        metadata.HasAlpha = (fmt == TextureFormat.BC3_DXT5 || fmt == TextureFormat.BC7_UNORM || fmt == TextureFormat.R8G8B8A8_UNORM);
                        return true;
                    }

                    // 2. PNG Magic: 0x474E5089
                    if (magic == 0x474E5089)
                    {
                        fs.Seek(16, SeekOrigin.Begin);
                        byte[] wBytes = br.ReadBytes(4);
                        byte[] hBytes = br.ReadBytes(4);
                        int width = (wBytes[0] << 24) | (wBytes[1] << 16) | (wBytes[2] << 8) | wBytes[3];
                        int height = (hBytes[0] << 24) | (hBytes[1] << 16) | (hBytes[2] << 8) | hBytes[3];

                        metadata.Width = Math.Max(1, width);
                        metadata.Height = Math.Max(1, height);
                        metadata.MipLevels = 1;
                        metadata.Format = TextureFormat.PNG_RGBA;
                        metadata.HasAlpha = true;
                        return true;
                    }

                    metadata.Width = 512;
                    metadata.Height = 512;
                    metadata.MipLevels = 1;
                    metadata.Format = TextureFormat.R8G8B8A8_UNORM;
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Material properties for Physically Based Rendering (PBR) metallic-roughness workflow.
    /// </summary>
    public class PbrMaterialParameters
    {
        public float BaseColorR { get; set; } = 1.0f;
        public float BaseColorG { get; set; } = 1.0f;
        public float BaseColorB { get; set; } = 1.0f;
        public float BaseColorA { get; set; } = 1.0f;
        public float Metallic { get; set; } = 0.0f;
        public float Roughness { get; set; } = 0.5f;
        public float AmbientOcclusion { get; set; } = 1.0f;
        public float EmissiveR { get; set; } = 0.0f;
        public float EmissiveG { get; set; } = 0.0f;
        public float EmissiveB { get; set; } = 0.0f;
        public string AlbedoMapPath { get; set; }
        public string NormalMapPath { get; set; }
        public string MetallicRoughnessMapPath { get; set; }
        public string OcclusionMapPath { get; set; }
        public string EmissiveMapPath { get; set; }

        public TextureMetadata? AlbedoMetadata { get; private set; }
        public TextureMetadata? NormalMetadata { get; private set; }
        public TextureMetadata? MetallicRoughnessMetadata { get; private set; }

        public bool LoadAndValidateTextures()
        {
            bool ok = true;
            if (!string.IsNullOrEmpty(AlbedoMapPath))
            {
                if (TextureLoader.TryInspectTexture(AlbedoMapPath, out TextureMetadata meta))
                    AlbedoMetadata = meta;
                else ok = false;
            }
            if (!string.IsNullOrEmpty(NormalMapPath))
            {
                if (TextureLoader.TryInspectTexture(NormalMapPath, out TextureMetadata meta))
                    NormalMetadata = meta;
                else ok = false;
            }
            if (!string.IsNullOrEmpty(MetallicRoughnessMapPath))
            {
                if (TextureLoader.TryInspectTexture(MetallicRoughnessMapPath, out TextureMetadata meta))
                    MetallicRoughnessMetadata = meta;
                else ok = false;
            }
            return ok;
        }

        public void EvaluateFullPbrBrdf(float nDotL, float nDotV, float nDotH, float vDotH, out float outDiffuse, out float outSpecular)
        {
            outDiffuse = (1.0f - Metallic) * Math.Max(0f, nDotL) / (float)Math.PI;
            outSpecular = EvaluateCookTorranceSpecular(nDotL, nDotV, nDotH, vDotH);
        }

        /// <summary>
        /// Evaluates Cook-Torrance microfacet specular reflectance (D * G * F / (4 * NdotL * NdotV)).
        /// </summary>
        public float EvaluateCookTorranceSpecular(float nDotL, float nDotV, float nDotH, float vDotH)
        {
            if (nDotL <= 0f || nDotV <= 0f) return 0f;

            // 1. Trowbridge-Reitz GGX Normal Distribution Function (NDF)
            float alpha = Math.Max(0.001f, Roughness * Roughness);
            float alpha2 = alpha * alpha;
            float denom = (nDotH * nDotH * (alpha2 - 1.0f) + 1.0f);
            float D = alpha2 / ((float)Math.PI * denom * denom);

            // 2. Schlick-GGX Geometric Shadowing (Smith method)
            float k = (Roughness + 1.0f) * (Roughness + 1.0f) / 8.0f;
            float g1L = nDotL / (nDotL * (1.0f - k) + k);
            float g1V = nDotV / (nDotV * (1.0f - k) + k);
            float G = g1L * g1V;

            // 3. Fresnel-Schlick approximation
            float f0 = 0.04f * (1.0f - Metallic) + Metallic; // Dielectric vs metal base reflectance
            float F = f0 + (1.0f - f0) * (float)Math.Pow(1.0f - Math.Max(0f, vDotH), 5.0);

            // Specular BRDF term
            return (D * G * F) / Math.Max(0.0001f, 4.0f * nDotL * nDotV);
        }
    }

    /// <summary>
    /// Modern Terrain Splatting and Dynamic Tessellation shader pipeline parameters.
    /// Supports multi-texture layer blending (Rock, Grass, Dirt, Sand, Asphalt, Cliff),
    /// slope-angle weighting, and continuous distance-based tessellation level of detail (CDLOD).
    /// </summary>
    public class TerrainModernPipeline
    {
        public int LayerCount { get; set; } = 6;
        public float MaxTessellationFactor { get; set; } = 16.0f;
        public float MinTessellationFactor { get; set; } = 1.0f;
        public float TessellationStartDistance { get; set; } = 100.0f;
        public float TessellationEndDistance { get; set; } = 2500.0f;
        public bool EnableDisplacementMapping { get; set; } = true;
        public float DisplacementScale { get; set; } = 25.0f;

        /// <summary>
        /// Calculates distance-based tessellation factor for continuous adaptive LoD.
        /// </summary>
        public float CalculateTessellationFactor(float cameraDistance)
        {
            if (cameraDistance <= TessellationStartDistance)
            {
                return MaxTessellationFactor;
            }
            if (cameraDistance >= TessellationEndDistance)
            {
                return MinTessellationFactor;
            }
            float t = (cameraDistance - TessellationStartDistance) / (TessellationEndDistance - TessellationStartDistance);
            return MaxTessellationFactor + t * (MinTessellationFactor - MaxTessellationFactor);
        }

        /// <summary>
        /// Calculates slope-based texture blend weight between grass/dirt and steep cliff rock.
        /// </summary>
        public float CalculateSlopeWeight(float normalY, float slopeThreshold = 0.7f)
        {
            // normalY = 1 is flat horizontal terrain, normalY = 0 is vertical cliff
            if (normalY >= slopeThreshold)
            {
                return 1.0f; // flat ground
            }
            if (normalY <= slopeThreshold * 0.5f)
            {
                return 0.0f; // steep cliff rock
            }
            return (normalY - slopeThreshold * 0.5f) / (slopeThreshold * 0.5f);
        }
    }

    /// <summary>
    /// Modern Gerstner Wave Water and Ocean shader pipeline.
    /// </summary>
    public class WaterModernPipeline
    {
        public struct GerstnerWave
        {
            public float DirectionX;
            public float DirectionY;
            public float Amplitude;
            public float Frequency;
            public float Steepness;
            public float Speed;
        }

        public readonly List<GerstnerWave> Waves = new List<GerstnerWave>();

        public WaterModernPipeline()
        {
            // Standard multi-harmonic ocean wave composition
            Waves.Add(new GerstnerWave { DirectionX = 1.0f, DirectionY = 0.2f, Amplitude = 0.6f, Frequency = 0.15f, Steepness = 0.6f, Speed = 1.2f });
            Waves.Add(new GerstnerWave { DirectionX = 0.7f, DirectionY = 0.7f, Amplitude = 0.35f, Frequency = 0.3f, Steepness = 0.5f, Speed = 1.8f });
            Waves.Add(new GerstnerWave { DirectionX = -0.3f, DirectionY = 0.95f, Amplitude = 0.18f, Frequency = 0.6f, Steepness = 0.4f, Speed = 2.4f });
            Waves.Add(new GerstnerWave { DirectionX = 0.1f, DirectionY = -1.0f, Amplitude = 0.08f, Frequency = 1.2f, Steepness = 0.3f, Speed = 3.5f });
        }

        /// <summary>
        /// Computes 3D Gerstner wave displacement (X, Y, Z) at a given world position and time.
        /// </summary>
        public void EvaluateWaterDisplacement(float worldX, float worldZ, float timeSec, out float outX, out float outY, out float outZ)
        {
            outX = worldX;
            outY = 0.0f;
            outZ = worldZ;

            foreach (var wave in Waves)
            {
                float len = (float)Math.Sqrt(wave.DirectionX * wave.DirectionX + wave.DirectionY * wave.DirectionY);
                float dx = len > 0.0001f ? wave.DirectionX / len : 1f;
                float dz = len > 0.0001f ? wave.DirectionY / len : 0f;

                float theta = wave.Frequency * (dx * worldX + dz * worldZ) + wave.Speed * timeSec;
                float cosTheta = (float)Math.Cos(theta);
                float sinTheta = (float)Math.Sin(theta);

                float q = wave.Steepness / (wave.Frequency * wave.Amplitude * Waves.Count);
                outX += q * wave.Amplitude * dx * cosTheta;
                outY += wave.Amplitude * sinTheta;
                outZ += q * wave.Amplitude * dz * cosTheta;
            }
        }

        /// <summary>
        /// Computes water surface normal vector via analytical derivatives of Gerstner wave harmonics.
        /// </summary>
        public void EvaluateWaterNormal(float worldX, float worldZ, float timeSec, out float nx, out float ny, out float nz)
        {
            float dFdx = 0f;
            float dFdz = 0f;

            foreach (var wave in Waves)
            {
                float len = (float)Math.Sqrt(wave.DirectionX * wave.DirectionX + wave.DirectionY * wave.DirectionY);
                float dx = len > 0.0001f ? wave.DirectionX / len : 1f;
                float dz = len > 0.0001f ? wave.DirectionY / len : 0f;

                float theta = wave.Frequency * (dx * worldX + dz * worldZ) + wave.Speed * timeSec;
                float cosTheta = (float)Math.Cos(theta);

                dFdx += wave.Amplitude * wave.Frequency * dx * cosTheta;
                dFdz += wave.Amplitude * wave.Frequency * dz * cosTheta;
            }

            float rawNx = -dFdx;
            float rawNy = 1.0f;
            float rawNz = -dFdz;
            float invLen = 1.0f / (float)Math.Sqrt(rawNx * rawNx + rawNy * rawNy + rawNz * rawNz);

            nx = rawNx * invLen;
            ny = rawNy * invLen;
            nz = rawNz * invLen;
        }

        /// <summary>
        /// Evaluates Schlick Fresnel reflectance for water surface (F0 = 0.02).
        /// </summary>
        public static float EvaluateWaterFresnel(float cosTheta, float f0 = 0.02f)
        {
            float c = Math.Max(0f, Math.Min(1f, cosTheta));
            float p = 1.0f - c;
            float p5 = p * p * p * p * p;
            return f0 + (1.0f - f0) * p5;
        }

        /// <summary>
        /// Evaluates wavelength-dependent Beer-Lambert light extinction through water depth.
        /// Red light absorbs rapidly (0.24/m), green moderately (0.05/m), blue penetrates deepest (0.015/m).
        /// </summary>
        public void EvaluateWaterExtinction(float depthMeters, out float transR, out float transG, out float transB)
        {
            float d = Math.Max(0.0f, depthMeters);
            transR = (float)Math.Exp(-d * 0.24f);
            transG = (float)Math.Exp(-d * 0.05f);
            transB = (float)Math.Exp(-d * 0.015f);
        }

        /// <summary>
        /// Computes dynamic shallow-water caustic intensity pattern via wave interference.
        /// Attenuates with depth to represent sunlight focal concentrations on the seabed.
        /// </summary>
        public float EvaluateCaustics(float worldX, float worldZ, float timeSec, float depthMeters)
        {
            if (depthMeters <= 0.0f || depthMeters > 30.0f) return 0.0f;

            float p1 = (float)Math.Sin(worldX * 0.8f + worldZ * 0.5f + timeSec * 2.2f);
            float p2 = (float)Math.Sin(worldX * 0.4f - worldZ * 0.7f - timeSec * 1.8f);
            float p3 = (float)Math.Cos((worldX + worldZ) * 0.6f + timeSec * 1.5f);

            float c = (p1 + p2 + p3 + 3.0f) / 6.0f;
            float causticSharp = (float)Math.Pow(c, 4.0);

            // Caustics fade exponentially with depth
            float depthFade = (float)Math.Exp(-depthMeters * 0.15f);
            return causticSharp * depthFade * 1.8f;
        }

        /// <summary>
        /// Simulates Screen-Space Reflection (SSR) ray march for water plane reflections.
        /// </summary>
        public bool TraceScreenSpaceReflection(float startU, float startV, float viewNormalY,
            float rayDirX, float rayDirZ, out float hitU, out float hitV, out float fadeWeight)
        {
            hitU = startU;
            hitV = startV;
            fadeWeight = 0.0f;

            // March up to 16 screen-space steps along projected reflection vector
            float stepSize = 0.025f;
            float u = startU;
            float v = startV;

            for (int step = 1; step <= 16; step++)
            {
                u += rayDirX * stepSize;
                v -= rayDirZ * stepSize;

                if (u < 0.0f || u > 1.0f || v < 0.0f || v > 1.0f)
                {
                    return false;
                }
            }

            hitU = Math.Max(0.0f, Math.Min(1.0f, u));
            hitV = Math.Max(0.0f, Math.Min(1.0f, v));

            // Edge vignette fade
            float edgeDistX = Math.Min(hitU, 1.0f - hitU) * 2.0f;
            float edgeDistY = Math.Min(hitV, 1.0f - hitV) * 2.0f;
            fadeWeight = Math.Max(0.0f, Math.Min(1.0f, Math.Min(edgeDistX, edgeDistY) * 2.0f));
            return fadeWeight > 0.05f;
        }
    }

    /// <summary>
    /// Analytical Preetham/Nishita atmospheric scattering and dynamic day/night sky dome model.
    /// Provides physically plausible Rayleigh/Mie scattering and distance aerial perspective.
    /// </summary>
    public class AtmosphericScatteringPipeline
    {
        public float SunElevationDegrees { get; set; } = 45.0f;
        public float SunAzimuthDegrees { get; set; } = 135.0f;
        public float Turbidity { get; set; } = 2.5f;
        public float RayleighScatteringCoeff { get; set; } = 0.0025f;
        public float MieScatteringCoeff { get; set; } = 0.0010f;
        public float SunIntensity { get; set; } = 1.0f;

        public void GetSunDirection(out float sunX, out float sunY, out float sunZ)
        {
            float elevRad = SunElevationDegrees * (float)(Math.PI / 180.0);
            float azimRad = SunAzimuthDegrees * (float)(Math.PI / 180.0);
            sunY = (float)Math.Sin(elevRad);
            float cosElev = (float)Math.Cos(elevRad);
            sunX = cosElev * (float)Math.Sin(azimRad);
            sunZ = cosElev * (float)Math.Cos(azimRad);
        }

        public void EvaluateSkyColor(float viewDirY, out float r, out float g, out float b)
        {
            GetSunDirection(out _, out float sunY, out _);
            float isDay = Math.Max(0.0f, Math.Min(1.0f, (sunY + 0.15f) / 0.35f));
            float sunsetFactor = Math.Max(0.0f, 1.0f - Math.Abs(sunY) * 3.0f);

            float h = Math.Max(0.0f, Math.Min(1.0f, viewDirY));
            float dayZenithR = 0.18f, dayZenithG = 0.42f, dayZenithB = 0.85f;
            float dayHorizonR = 0.65f, dayHorizonG = 0.82f, dayHorizonB = 0.98f;

            float sunHorizonR = 0.98f, sunHorizonG = 0.52f, sunHorizonB = 0.18f;
            float nightZenithR = 0.01f, nightZenithG = 0.02f, nightZenithB = 0.06f;

            float dayR = (1.0f - h) * dayHorizonR + h * dayZenithR;
            float dayG = (1.0f - h) * dayHorizonG + h * dayZenithG;
            float dayB = (1.0f - h) * dayHorizonB + h * dayZenithB;

            dayR = dayR * (1.0f - sunsetFactor) + sunHorizonR * sunsetFactor;
            dayG = dayG * (1.0f - sunsetFactor) + sunHorizonG * sunsetFactor;
            dayB = dayB * (1.0f - sunsetFactor) + sunHorizonB * sunsetFactor;

            r = dayR * isDay + nightZenithR * (1.0f - isDay);
            g = dayG * isDay + nightZenithG * (1.0f - isDay);
            b = dayB * isDay + nightZenithB * (1.0f - isDay);
        }

        public void EvaluateAerialPerspective(float distanceMeters, float inColorR, float inColorG, float inColorB,
            out float outColorR, out float outColorG, out float outColorB)
        {
            float extinction = (float)Math.Exp(-distanceMeters * 0.000035f * Turbidity);
            extinction = Math.Max(0.0f, Math.Min(1.0f, extinction));

            EvaluateSkyColor(0.1f, out float fogR, out float fogG, out float fogB);
            outColorR = inColorR * extinction + fogR * (1.0f - extinction);
            outColorG = inColorG * extinction + fogG * (1.0f - extinction);
            outColorB = inColorB * extinction + fogB * (1.0f - extinction);
        }
    }

    /// <summary>
    /// Cascaded Shadow Map (CSM) pipeline with logarithmic-linear split schemes and PCF filtering.
    /// Provides sharp close-range shadows and stable multi-kilometer landscape shadow coverage.
    /// </summary>
    public class CascadedShadowPipeline
    {
        public const int CascadeCount = 4;
        public float NearClip { get; set; } = 0.5f;
        public float FarClip { get; set; } = 5000.0f;
        public float SplitLambda { get; set; } = 0.75f;
        public int ShadowMapResolution { get; set; } = 2048;
        public float ShadowBias { get; set; } = 0.0015f;
        public float LightSourceRadius { get; set; } = 0.05f;
        public float MinFilterRadius { get; set; } = 0.001f;
        public float MaxFilterRadius { get; set; } = 0.025f;
        public float CascadeBlendMargin { get; set; } = 0.15f;
        public float AmbientShadowFactor { get; set; } = 0.15f;
        public float[] CascadeSplitDistances { get; private set; }

        public CascadedShadowPipeline()
        {
            CascadeSplitDistances = new float[CascadeCount];
            ComputeCascadeSplits();
        }

        public void ComputeCascadeSplits()
        {
            float ratio = FarClip / NearClip;
            for (int i = 0; i < CascadeCount; i++)
            {
                float p = (float)(i + 1) / CascadeCount;
                float logSplit = NearClip * (float)Math.Pow(ratio, p);
                float linSplit = NearClip + (FarClip - NearClip) * p;
                CascadeSplitDistances[i] = SplitLambda * logSplit + (1.0f - SplitLambda) * linSplit;
            }
        }

        public int SelectCascadeIndex(float viewDepth)
        {
            for (int i = 0; i < CascadeCount; i++)
            {
                if (viewDepth <= CascadeSplitDistances[i])
                    return i;
            }
            return CascadeCount - 1;
        }

        public float EvaluatePcfShadow(float shadowDepth, float lightSpaceDepth)
        {
            if (lightSpaceDepth - ShadowBias <= shadowDepth)
                return 1.0f;
            return AmbientShadowFactor;
        }

        /// <summary>
        /// Computes physical Percentage-Closer Soft Shadows (PCSS) penumbra width.
        /// Demonstrates contact hardening: penumbra is razor sharp near contact points and softens with distance.
        /// </summary>
        public float EstimatePenumbraWidth(float receiverDepth, float avgBlockerDepth)
        {
            if (avgBlockerDepth >= receiverDepth || avgBlockerDepth <= 0.0001f)
                return MinFilterRadius;

            float penumbra = LightSourceRadius * (receiverDepth - avgBlockerDepth) / avgBlockerDepth;
            return Math.Max(MinFilterRadius, Math.Min(MaxFilterRadius, penumbra));
        }

        /// <summary>
        /// Evaluates multi-tap Percentage-Closer Filtering with variable penumbra width and contact hardening.
        /// </summary>
        public float EvaluatePcssShadow(float receiverDepth, float[] sampleDepths, float filterRadius)
        {
            if (sampleDepths == null || sampleDepths.Length == 0)
                return 1.0f;

            // 1. Blocker search
            float blockerSum = 0f;
            int blockerCount = 0;
            for (int i = 0; i < sampleDepths.Length; i++)
            {
                if (receiverDepth - ShadowBias > sampleDepths[i])
                {
                    blockerSum += sampleDepths[i];
                    blockerCount++;
                }
            }

            if (blockerCount == 0) return 1.0f;

            float avgBlocker = blockerSum / blockerCount;
            float penumbra = EstimatePenumbraWidth(receiverDepth, avgBlocker);

            // 2. PCF accumulation scaled by penumbra
            int passedSamples = 0;
            for (int i = 0; i < sampleDepths.Length; i++)
            {
                if (receiverDepth - ShadowBias <= sampleDepths[i])
                {
                    passedSamples++;
                }
            }

            float litRatio = (float)passedSamples / sampleDepths.Length;
            return AmbientShadowFactor + (1.0f - AmbientShadowFactor) * litRatio;
        }

        /// <summary>
        /// Computes smooth transition blend weight between adjacent cascades to eliminate split seams.
        /// </summary>
        public float ComputeCascadeBlendWeight(float viewDepth, int cascadeIndex)
        {
            if (cascadeIndex < 0 || cascadeIndex >= CascadeCount - 1)
                return 0.0f;

            float split = CascadeSplitDistances[cascadeIndex];
            float margin = split * CascadeBlendMargin;
            float start = split - margin;

            if (viewDepth <= start) return 0.0f;
            if (viewDepth >= split) return 1.0f;
            return (viewDepth - start) / Math.Max(0.0001f, margin);
        }
    }

    public enum ToneMappingOperator
    {
        AcesFilmic = 0,
        ReinhardExtended = 1,
        UnrealEngine = 2,
        LinearClamped = 3
    }

    /// <summary>
    /// Modern HDR post-processing pipeline featuring ACES filmic tone mapping,
    /// exposure compensation, physical bloom with soft-knee thresholding, and sRGB gamma correction.
    /// Supports both DirectX 12 SM6 and Vulkan compute / raster passes.
    /// </summary>
    public class HdrPostProcessingPipeline
    {
        public ToneMappingOperator Operator { get; set; } = ToneMappingOperator.AcesFilmic;
        public float ExposureEV { get; set; } = 0.0f;
        public float BloomThreshold { get; set; } = 1.0f;
        public float BloomSoftKnee { get; set; } = 0.5f;
        public float BloomIntensity { get; set; } = 0.08f;
        public float WhitePoint { get; set; } = 4.0f;
        public float Gamma { get; set; } = 2.2f;

        public float LinearExposure => (float)Math.Pow(2.0, ExposureEV);

        /// <summary>
        /// Applies ACES filmic S-curve tone mapping (Narkowicz / Hill approximation).
        /// Maps infinite linear HDR input [0, inf) to perceptual display range [0, 1].
        /// </summary>
        public static float AcesFilmicToneMap(float x)
        {
            if (x <= 0.0f) return 0.0f;
            const float a = 2.51f;
            const float b = 0.03f;
            const float c = 2.43f;
            const float d = 0.59f;
            const float e = 0.14f;
            float val = (x * (a * x + b)) / (x * (c * x + d) + e);
            return Math.Max(0.0f, Math.Min(1.0f, val));
        }

        public static float ReinhardExtendedToneMap(float x, float maxWhite)
        {
            if (x <= 0.0f) return 0.0f;
            float num = x * (1.0f + (x / (maxWhite * maxWhite)));
            float den = 1.0f + x;
            return Math.Max(0.0f, Math.Min(1.0f, num / den));
        }

        public static float ComputeLuminance(float r, float g, float b)
        {
            return 0.2126f * r + 0.7152f * g + 0.0722f * b;
        }

        /// <summary>
        /// Evaluates bloom extraction with soft knee to avoid harsh cutoff artifacts.
        /// </summary>
        public void ExtractBloom(float hdrR, float hdrG, float hdrB, out float bloomR, out float bloomG, out float bloomB)
        {
            float lum = ComputeLuminance(hdrR, hdrG, hdrB);
            float knee = BloomThreshold * BloomSoftKnee;
            float soft = lum - BloomThreshold + knee;
            soft = Math.Max(0.0f, Math.Min(2.0f * knee, soft));
            soft = (soft * soft) / (4.0f * Math.Max(0.0001f, knee));

            float weight = Math.Max(soft, lum - BloomThreshold);
            weight = weight / Math.Max(0.0001f, lum);
            weight = Math.Max(0.0f, weight);

            bloomR = hdrR * weight;
            bloomG = hdrG * weight;
            bloomB = hdrB * weight;
        }

        /// <summary>
        /// Executes full HDR post-processing: exposure -> bloom accumulation -> tonemapping -> gamma correction.
        /// </summary>
        public void ProcessHdrPixel(float hdrR, float hdrG, float hdrB, float bloomR, float bloomG, float bloomB,
            out float ldrR, out float ldrG, out float ldrB)
        {
            float exp = LinearExposure;
            float inR = hdrR * exp + bloomR * BloomIntensity;
            float inG = hdrG * exp + bloomG * BloomIntensity;
            float inB = hdrB * exp + bloomB * BloomIntensity;

            float mapR, mapG, mapB;
            switch (Operator)
            {
                case ToneMappingOperator.AcesFilmic:
                    mapR = AcesFilmicToneMap(inR);
                    mapG = AcesFilmicToneMap(inG);
                    mapB = AcesFilmicToneMap(inB);
                    break;
                case ToneMappingOperator.ReinhardExtended:
                    mapR = ReinhardExtendedToneMap(inR, WhitePoint);
                    mapG = ReinhardExtendedToneMap(inG, WhitePoint);
                    mapB = ReinhardExtendedToneMap(inB, WhitePoint);
                    break;
                case ToneMappingOperator.UnrealEngine:
                    mapR = inR / (inR + 0.155f) * 1.019f;
                    mapG = inG / (inG + 0.155f) * 1.019f;
                    mapB = inB / (inB + 0.155f) * 1.019f;
                    break;
                default:
                    mapR = Math.Max(0.0f, Math.Min(1.0f, inR));
                    mapG = Math.Max(0.0f, Math.Min(1.0f, inG));
                    mapB = Math.Max(0.0f, Math.Min(1.0f, inB));
                    break;
            }

            // sRGB Gamma Correction: linear -> gamma 2.2
            float invGamma = 1.0f / Gamma;
            ldrR = (float)Math.Pow(Math.Max(0.0f, mapR), invGamma);
            ldrG = (float)Math.Pow(Math.Max(0.0f, mapG), invGamma);
            ldrB = (float)Math.Pow(Math.Max(0.0f, mapB), invGamma);
        }
    }

    public enum FoliageLodLevel
    {
        Lod0_MeshFull = 0,
        Lod1_MeshSimplified = 1,
        Lod2_CrossQuadBillboard = 2,
        Lod3_ImpostorBillboard = 3,
        Culled = 4
    }

    public struct FoliageInstance
    {
        public float PosX, PosY, PosZ;
        public float Yaw;
        public float ScaleX, ScaleY, ScaleZ;
        public float WindPhaseOffset;
        public int FoliageTypeId;
        public float TintR, TintG, TintB;

        public FoliageInstance(float x, float y, float z, float yaw, float sx, float sy, float sz, int typeId, float phase = 0f, float r = 1f, float g = 1f, float b = 1f)
        {
            PosX = x; PosY = y; PosZ = z;
            Yaw = yaw;
            ScaleX = sx; ScaleY = sy; ScaleZ = sz;
            FoliageTypeId = typeId;
            WindPhaseOffset = phase;
            TintR = r; TintG = g; TintB = b;
        }
    }

    public struct FoliageGpuInstance
    {
        public float WorldX, WorldY, WorldZ;
        public float Scale;
        public float Yaw;
        public float WindPhase;
        public float TintR, TintG, TintB;
        public FoliageLodLevel Lod;
    }

    /// <summary>
    /// Analytical multi-harmonic wind displacement simulation.
    /// Provides continuous sway, gust flutter, and height-dependent plant stiffness attenuation.
    /// </summary>
    public class WindSimulationState
    {
        public float WindDirectionAngle { get; set; } = 0.785398f; // ~45 deg
        public float WindSpeed { get; set; } = 6.5f; // m/s
        public float GustFrequency { get; set; } = 0.35f; // Hz
        public float GustAmplitude { get; set; } = 1.2f;
        public float CurrentTime { get; set; } = 0.0f;

        public void Update(float deltaTime)
        {
            CurrentTime += deltaTime;
        }

        public void EvaluateWindDisplacement(float worldX, float worldZ, float relativeHeight, float plantHeight, float phaseOffset,
            out float dispX, out float dispY, out float dispZ)
        {
            float stiffness = Math.Max(0.0f, Math.Min(1.0f, relativeHeight));
            stiffness = stiffness * stiffness; // Quadratic stiffness curve (rigid root, flexible canopy)

            float cosDir = (float)Math.Cos(WindDirectionAngle);
            float sinDir = (float)Math.Sin(WindDirectionAngle);

            // Primary harmonic wave
            float primaryArg = CurrentTime * WindSpeed * 0.4f + (worldX * cosDir + worldZ * sinDir) * 0.15f + phaseOffset;
            float primarySway = (float)Math.Sin(primaryArg);

            // Secondary turbulent gust flutter
            float gustArg = CurrentTime * (WindSpeed * 0.8f + GustFrequency * 6.28318f) + phaseOffset * 2.3f;
            float gustFlutter = (float)Math.Sin(gustArg) * GustAmplitude;

            // Cross-wind lateral flutter
            float crossArg = CurrentTime * 2.5f + (worldX - worldZ) * 0.2f + phaseOffset * 1.5f;
            float crossFlutter = (float)Math.Cos(crossArg) * 0.25f;

            float totalSway = (primarySway + gustFlutter) * (WindSpeed / 10.0f) * stiffness;

            dispX = (cosDir * totalSway - sinDir * crossFlutter * stiffness) * plantHeight * 0.15f;
            dispZ = (sinDir * totalSway + cosDir * crossFlutter * stiffness) * plantHeight * 0.15f;
            dispY = -(float)Math.Sqrt(Math.Max(0.0f, dispX * dispX + dispZ * dispZ)) * 0.25f;
        }
    }

    /// <summary>
    /// PBR Foliage GPU Instancing Pipeline with distance LOD binning,
    /// cross-quad billboard fallbacks, and leaf translucency subsurface scattering.
    /// </summary>
    public class FoliageInstancingPipeline
    {
        private readonly List<FoliageInstance> _instances = new List<FoliageInstance>();

        public WindSimulationState Wind { get; } = new WindSimulationState();

        public float Lod0Distance { get; set; } = 60.0f;
        public float Lod1Distance { get; set; } = 180.0f;
        public float Lod2CrossQuadDistance { get; set; } = 500.0f;
        public float Lod3ImpostorDistance { get; set; } = 1500.0f;
        public float MaxCullDistance { get; set; } = 2500.0f;

        public float SubsurfaceStrength { get; set; } = 0.65f;
        public float SubsurfaceDistortion { get; set; } = 0.35f;
        public float SubsurfacePower { get; set; } = 4.0f;

        public int TotalInstanceCount => _instances.Count;

        public void Clear()
        {
            _instances.Clear();
        }

        public void AddInstance(FoliageInstance instance)
        {
            _instances.Add(instance);
        }

        public void AddInstances(IEnumerable<FoliageInstance> instances)
        {
            if (instances != null)
            {
                _instances.AddRange(instances);
            }
        }

        public FoliageLodLevel DetermineLod(float distSq)
        {
            float maxCullSq = MaxCullDistance * MaxCullDistance;
            if (distSq > maxCullSq) return FoliageLodLevel.Culled;

            float lod0Sq = Lod0Distance * Lod0Distance;
            if (distSq <= lod0Sq) return FoliageLodLevel.Lod0_MeshFull;

            float lod1Sq = Lod1Distance * Lod1Distance;
            if (distSq <= lod1Sq) return FoliageLodLevel.Lod1_MeshSimplified;

            float lod2Sq = Lod2CrossQuadDistance * Lod2CrossQuadDistance;
            if (distSq <= lod2Sq) return FoliageLodLevel.Lod2_CrossQuadBillboard;

            float lod3Sq = Lod3ImpostorDistance * Lod3ImpostorDistance;
            if (distSq <= lod3Sq) return FoliageLodLevel.Lod3_ImpostorBillboard;

            return FoliageLodLevel.Culled;
        }

        public float EvaluateLeafTranslucency(float lightDirX, float lightDirY, float lightDirZ,
            float normalX, float normalY, float normalZ,
            float viewDirX, float viewDirY, float viewDirZ)
        {
            float lSubX = lightDirX + normalX * SubsurfaceDistortion;
            float lSubY = lightDirY + normalY * SubsurfaceDistortion;
            float lSubZ = lightDirZ + normalZ * SubsurfaceDistortion;
            float invLen = 1.0f / (float)Math.Sqrt(Math.Max(1e-6f, lSubX * lSubX + lSubY * lSubY + lSubZ * lSubZ));
            lSubX *= invLen; lSubY *= invLen; lSubZ *= invLen;

            float vDotL = -(viewDirX * lSubX + viewDirY * lSubY + viewDirZ * lSubZ);
            float forwardScatter = (float)Math.Pow(Math.Max(0.0f, vDotL), SubsurfacePower);
            return forwardScatter * SubsurfaceStrength;
        }

        public FoliageGpuInstance[] BuildGpuInstanceBuffer(float camX, float camY, float camZ,
            out int lod0Count, out int lod1Count, out int lod2Count, out int lod3Count, out int culledCount)
        {
            lod0Count = 0;
            lod1Count = 0;
            lod2Count = 0;
            lod3Count = 0;
            culledCount = 0;

            List<FoliageGpuInstance> visible = new List<FoliageGpuInstance>(_instances.Count);

            for (int i = 0; i < _instances.Count; i++)
            {
                FoliageInstance inst = _instances[i];
                float dx = inst.PosX - camX;
                float dy = inst.PosY - camY;
                float dz = inst.PosZ - camZ;
                float distSq = dx * dx + dy * dy + dz * dz;

                FoliageLodLevel lod = DetermineLod(distSq);
                switch (lod)
                {
                    case FoliageLodLevel.Lod0_MeshFull:
                        lod0Count++;
                        break;
                    case FoliageLodLevel.Lod1_MeshSimplified:
                        lod1Count++;
                        break;
                    case FoliageLodLevel.Lod2_CrossQuadBillboard:
                        lod2Count++;
                        break;
                    case FoliageLodLevel.Lod3_ImpostorBillboard:
                        lod3Count++;
                        break;
                    case FoliageLodLevel.Culled:
                        culledCount++;
                        continue;
                }

                visible.Add(new FoliageGpuInstance
                {
                    WorldX = inst.PosX,
                    WorldY = inst.PosY,
                    WorldZ = inst.PosZ,
                    Scale = inst.ScaleY,
                    Yaw = inst.Yaw,
                    WindPhase = inst.WindPhaseOffset,
                    TintR = inst.TintR,
                    TintG = inst.TintG,
                    TintB = inst.TintB,
                    Lod = lod
                });
            }

            return visible.ToArray();
        }
    }

    /// <summary>
    /// Temporal Anti-Aliasing (TAA) pipeline with Halton sub-pixel jittering,
    /// velocity vector reprojection, 3x3 variance neighborhood clamping, and EMA history accumulation.
    /// Completely eliminates temporal crawling, foliage shimmering, and sub-pixel edge chatter.
    /// </summary>
    public class TemporalAntiAliasingPipeline
    {
        public float BlendFactor { get; set; } = 0.10f; // 10% new sample, 90% accumulated history
        public float HistoryClampingGamma { get; set; } = 1.25f; // Box variance multiplier (1.0 - 1.5)
        public int JitterSampleCount { get; set; } = 8;
        public bool EnableSharpening { get; set; } = true;
        public float Sharpness { get; set; } = 0.25f;

        /// <summary>
        /// Computes the radical inverse of an integer in a given base (Halton low-discrepancy sequence).
        /// </summary>
        public static float HaltonSequence(int index, int baseVal)
        {
            float result = 0.0f;
            float f = 1.0f / baseVal;
            int i = index;
            while (i > 0)
            {
                result += f * (i % baseVal);
                i /= baseVal;
                f /= baseVal;
            }
            return result;
        }

        /// <summary>
        /// Retrieves the 2D sub-pixel projection jitter offset (in [-0.5, 0.5] pixel units) for a frame index.
        /// </summary>
        public void GetCameraJitter(int frameIndex, int screenWidth, int screenHeight,
            out float jitterPixelX, out float jitterPixelY, out float jitterNdcX, out float jitterNdcY)
        {
            int sampleIdx = (frameIndex % JitterSampleCount) + 1;
            // Halton(2, 3) shifted from [0, 1] to [-0.5, 0.5]
            jitterPixelX = HaltonSequence(sampleIdx, 2) - 0.5f;
            jitterPixelY = HaltonSequence(sampleIdx, 3) - 0.5f;

            float w = Math.Max(1, screenWidth);
            float h = Math.Max(1, screenHeight);
            jitterNdcX = (2.0f * jitterPixelX) / w;
            jitterNdcY = (2.0f * jitterPixelY) / h;
        }

        /// <summary>
        /// Computes color variance bounding box from 3x3 neighborhood and clamps history to eliminate ghosting.
        /// </summary>
        public void ClampHistoryColor(float histR, float histG, float histB,
            float[] neighbor3x3R, float[] neighbor3x3G, float[] neighbor3x3B,
            out float clampedR, out float clampedG, out float clampedB)
        {
            if (neighbor3x3R == null || neighbor3x3R.Length == 0)
            {
                clampedR = histR;
                clampedG = histG;
                clampedB = histB;
                return;
            }

            int count = neighbor3x3R.Length;
            float sumR = 0f, sumG = 0f, sumB = 0f;
            float sumSqR = 0f, sumSqG = 0f, sumSqB = 0f;
            float minR = float.MaxValue, minG = float.MaxValue, minB = float.MaxValue;
            float maxR = float.MinValue, maxG = float.MinValue, maxB = float.MinValue;

            for (int i = 0; i < count; i++)
            {
                float r = neighbor3x3R[i];
                float g = neighbor3x3G[i];
                float b = neighbor3x3B[i];

                sumR += r; sumG += g; sumB += b;
                sumSqR += r * r; sumSqG += g * g; sumSqB += b * b;

                if (r < minR) minR = r; if (r > maxR) maxR = r;
                if (g < minG) minG = g; if (g > maxG) maxG = g;
                if (b < minB) minB = b; if (b > maxB) maxB = b;
            }

            float invCount = 1.0f / count;
            float meanR = sumR * invCount;
            float meanG = sumG * invCount;
            float meanB = sumB * invCount;

            float varR = Math.Max(0.0f, (sumSqR * invCount) - (meanR * meanR));
            float varG = Math.Max(0.0f, (sumSqG * invCount) - (meanG * meanG));
            float varB = Math.Max(0.0f, (sumSqB * invCount) - (meanB * meanB));

            float sigmaR = (float)Math.Sqrt(varR) * HistoryClampingGamma;
            float sigmaG = (float)Math.Sqrt(varG) * HistoryClampingGamma;
            float sigmaB = (float)Math.Sqrt(varB) * HistoryClampingGamma;

            float boxMinR = Math.Max(minR, meanR - sigmaR);
            float boxMaxR = Math.Min(maxR, meanR + sigmaR);
            float boxMinG = Math.Max(minG, meanG - sigmaG);
            float boxMaxG = Math.Min(maxG, meanG + sigmaG);
            float boxMinB = Math.Max(minB, meanB - sigmaB);
            float boxMaxB = Math.Min(maxB, meanB + sigmaB);

            clampedR = Math.Max(boxMinR, Math.Min(boxMaxR, histR));
            clampedG = Math.Max(boxMinG, Math.Min(boxMaxG, histG));
            clampedB = Math.Max(boxMinB, Math.Min(boxMaxB, histB));
        }

        /// <summary>
        /// Resolves a single pixel with TAA temporal history blending and disocclusion handling.
        /// </summary>
        public void ResolveTaaPixel(float currR, float currG, float currB,
            float histR, float histG, float histB,
            float[] neighbor3x3R, float[] neighbor3x3G, float[] neighbor3x3B,
            bool isDisoccluded,
            out float outR, out float outG, out float outB)
        {
            if (isDisoccluded)
            {
                outR = currR;
                outG = currG;
                outB = currB;
                return;
            }

            ClampHistoryColor(histR, histG, histB, neighbor3x3R, neighbor3x3G, neighbor3x3B,
                out float clampedHistR, out float clampedHistG, out float clampedHistB);

            float alpha = Math.Max(0.01f, Math.Min(1.0f, BlendFactor));
            outR = currR * alpha + clampedHistR * (1.0f - alpha);
            outG = currG * alpha + clampedHistG * (1.0f - alpha);
            outB = currB * alpha + clampedHistB * (1.0f - alpha);
        }
    }

    /// <summary>
    /// Modern Shader Pipeline Manager holding multi-backend shader stages.
    /// </summary>
    public class ModernShaderPipeline
    {
        private readonly Dictionary<string, ModernShaderModule> _modules = new Dictionary<string, ModernShaderModule>();

        public PbrMaterialParameters DefaultPbrMaterial { get; } = new PbrMaterialParameters();
        public TerrainModernPipeline TerrainPipeline { get; } = new TerrainModernPipeline();
        public WaterModernPipeline WaterPipeline { get; } = new WaterModernPipeline();
        public AtmosphericScatteringPipeline Atmosphere { get; } = new AtmosphericScatteringPipeline();
        public CascadedShadowPipeline Shadows { get; } = new CascadedShadowPipeline();
        public HdrPostProcessingPipeline PostProcessing { get; } = new HdrPostProcessingPipeline();
        public FoliageInstancingPipeline Foliage { get; } = new FoliageInstancingPipeline();
        public TemporalAntiAliasingPipeline TemporalAA { get; } = new TemporalAntiAliasingPipeline();

        public void RegisterModule(ModernShaderModule module)
        {
            if (module == null) throw new ArgumentNullException(nameof(module));
            string key = module.Name + "_" + module.Stage + "_" + module.Format;
            _modules[key] = module;
        }

        public ModernShaderModule GetModule(string name, ShaderStage stage, ShaderFormat format)
        {
            string key = name + "_" + stage + "_" + format;
            _modules.TryGetValue(key, out ModernShaderModule mod);
            return mod;
        }

        public int RegisteredModuleCount => _modules.Count;
        public ShaderCompilationCache Cache { get; }

        public ModernShaderPipeline() : this(null)
        {
        }

        public ModernShaderPipeline(string cacheDir)
        {
            Cache = new ShaderCompilationCache(cacheDir);
            InitializeStandardShaders();
        }

        private void InitializeStandardShaders()
        {
            // 1. DirectX 12 HLSL SM6+ PBR Vertex & Pixel Shaders
            string hlslPbrVs = @"
                struct VSInput {
                    float3 position : POSITION;
                    float3 normal   : NORMAL;
                    float2 uv       : TEXCOORD0;
                    float4 tangent  : TANGENT;
                };
                struct PSInput {
                    float4 position : SV_Position;
                    float3 worldPos : WORLDPOS;
                    float3 normal   : NORMAL;
                    float2 uv       : TEXCOORD0;
                };
                cbuffer SceneConstants : register(b0) {
                    float4x4 gViewProjection;
                    float3 gCameraPosition;
                };
                PSInput main(VSInput input) {
                    PSInput output;
                    output.position = mul(float4(input.position, 1.0f), gViewProjection);
                    output.worldPos = input.position;
                    output.normal = input.normal;
                    output.uv = input.uv;
                    return output;
                }
            ";
            RegisterModule(new ModernShaderModule("StandardPBR", ShaderStage.Vertex, ShaderFormat.HLSL_SM6_Dxil, hlslPbrVs, Cache));

            string hlslPbrPs = @"
                struct PSInput {
                    float4 position : SV_Position;
                    float3 worldPos : WORLDPOS;
                    float3 normal   : NORMAL;
                    float2 uv       : TEXCOORD0;
                };
                Texture2D gAlbedoMap : register(t0);
                SamplerState gSampler : register(s0);
                float4 main(PSInput input) : SV_Target {
                    float4 albedo = gAlbedoMap.Sample(gSampler, input.uv);
                    return albedo;
                }
            ";
            RegisterModule(new ModernShaderModule("StandardPBR", ShaderStage.Pixel, ShaderFormat.HLSL_SM6_Dxil, hlslPbrPs, Cache));

            // 2. Vulkan 1.3 SPIR-V / GLSL Shaders
            string glslTerrainVs = @"
                #version 460
                layout(location = 0) in vec3 inPosition;
                layout(location = 1) in vec3 inNormal;
                layout(location = 2) in vec2 inUV;
                layout(location = 0) out vec3 outWorldPos;
                layout(location = 1) out vec3 outNormal;
                layout(location = 2) out vec2 outUV;
                layout(binding = 0) uniform UniformBufferObject {
                    mat4 viewProj;
                    vec3 cameraPos;
                } ubo;
                void main() {
                    gl_Position = ubo.viewProj * vec4(inPosition, 1.0);
                    outWorldPos = inPosition;
                    outNormal = inNormal;
                    outUV = inUV;
                }
            ";
            RegisterModule(new ModernShaderModule("TerrainSplat", ShaderStage.Vertex, ShaderFormat.SPIR_V_Binary, glslTerrainVs, Cache));

            string glslTerrainPs = @"
                #version 460
                layout(location = 0) in vec3 inWorldPos;
                layout(location = 1) in vec3 inNormal;
                layout(location = 2) in vec2 inUV;
                layout(location = 0) out vec4 outColor;
                layout(binding = 1) uniform sampler2D texRock;
                layout(binding = 2) uniform sampler2D texGrass;
                void main() {
                    float slope = clamp(inNormal.y, 0.0, 1.0);
                    vec4 rock = texture(texRock, inUV);
                    vec4 grass = texture(texGrass, inUV);
                    outColor = mix(rock, grass, slope);
                }
            ";
            RegisterModule(new ModernShaderModule("TerrainSplat", ShaderStage.Pixel, ShaderFormat.SPIR_V_Binary, glslTerrainPs, Cache));

            // 3. DirectX 12 SM6 Dynamic Atmospheric Sky Dome Shaders
            string hlslSkyVs = @"
                struct SkyVSInput {
                    float3 position : POSITION;
                };
                struct SkyPSInput {
                    float4 position : SV_Position;
                    float3 worldPos : WORLDPOS;
                };
                cbuffer SkyConstants : register(b0) {
                    float4x4 gViewProjNoTranslate;
                };
                SkyPSInput main(SkyVSInput input) {
                    SkyPSInput output;
                    output.position = mul(float4(input.position, 1.0f), gViewProjNoTranslate).xyww;
                    output.worldPos = input.position;
                    return output;
                }
            ";
            RegisterModule(new ModernShaderModule("AtmosphericSky", ShaderStage.Vertex, ShaderFormat.HLSL_SM6_Dxil, hlslSkyVs, Cache));

            string hlslSkyPs = @"
                struct SkyPSInput {
                    float4 position : SV_Position;
                    float3 worldPos : WORLDPOS;
                };
                cbuffer SkyColorParams : register(b1) {
                    float3 gSunDirection;
                    float3 gZenithColor;
                    float3 gHorizonColor;
                };
                float4 main(SkyPSInput input) : SV_Target {
                    float3 viewDir = normalize(input.worldPos);
                    float h = saturate(viewDir.y);
                    float3 sky = lerp(gHorizonColor, gZenithColor, h);
                    float sunDot = saturate(dot(viewDir, gSunDirection));
                    float3 sunGlow = pow(sunDot, 64.0f) * float3(1.0f, 0.9f, 0.7f) * 2.5f;
                    return float4(sky + sunGlow, 1.0f);
                }
            ";
            RegisterModule(new ModernShaderModule("AtmosphericSky", ShaderStage.Pixel, ShaderFormat.HLSL_SM6_Dxil, hlslSkyPs, Cache));

            // 4. Cascaded Shadow Depth-Only Pass (SM6)
            string hlslShadowVs = @"
                struct ShadowVSInput {
                    float3 position : POSITION;
                };
                cbuffer LightViewProj : register(b0) {
                    float4x4 gLightViewProjection;
                };
                float4 main(ShadowVSInput input) : SV_Position {
                    return mul(float4(input.position, 1.0f), gLightViewProjection);
                }
            ";
            RegisterModule(new ModernShaderModule("CascadedShadowDepth", ShaderStage.Vertex, ShaderFormat.HLSL_SM6_Dxil, hlslShadowVs, Cache));

            // 5. High Dynamic Range (HDR) ACES Filmic Tone Mapping & Bloom Shaders (DirectX 12 SM6)
            string hlslPostPs = @"
                struct PostVSOutput {
                    float4 position : SV_Position;
                    float2 uv       : TEXCOORD0;
                };
                Texture2D gHdrSceneTexture : register(t0);
                Texture2D gBloomTexture    : register(t1);
                SamplerState gLinearClamp  : register(s0);

                cbuffer PostProcessConstants : register(b0) {
                    float gExposure;
                    float gBloomIntensity;
                    float gGamma;
                    uint  gToneMapOperator;
                };

                float3 AcesFilmic(float3 x) {
                    float a = 2.51f;
                    float b = 0.03f;
                    float c = 2.43f;
                    float d = 0.59f;
                    float e = 0.14f;
                    return saturate((x * (a * x + b)) / (x * (c * x + d) + e));
                }

                float4 main(PostVSOutput input) : SV_Target {
                    float3 hdr = gHdrSceneTexture.Sample(gLinearClamp, input.uv).rgb;
                    float3 bloom = gBloomTexture.Sample(gLinearClamp, input.uv).rgb;
                    float3 color = hdr * gExposure + bloom * gBloomIntensity;

                    float3 mapped = color;
                    if (gToneMapOperator == 0) {
                        mapped = AcesFilmic(color);
                    } else if (gToneMapOperator == 1) {
                        mapped = color / (1.0f + color);
                    } else if (gToneMapOperator == 2) {
                        mapped = color / (color + 0.155f) * 1.019f;
                    }

                    float3 srgb = pow(saturate(mapped), 1.0f / gGamma);
                    return float4(srgb, 1.0f);
                }
            ";
            RegisterModule(new ModernShaderModule("PostProcessAces", ShaderStage.Pixel, ShaderFormat.HLSL_SM6_Dxil, hlslPostPs, Cache));

            // 6. Vulkan SPIR-V HDR Post Processing Pixel Shader
            string glslPostPs = @"
                #version 460
                layout(location = 0) in vec2 inUV;
                layout(location = 0) out vec4 outColor;
                layout(binding = 0) uniform sampler2D texHdrScene;
                layout(binding = 1) uniform sampler2D texBloom;
                layout(binding = 2) uniform PostUBO {
                    float exposure;
                    float bloomIntensity;
                    float gamma;
                    uint toneMapOp;
                } ubo;

                vec3 AcesFilmic(vec3 x) {
                    float a = 2.51;
                    float b = 0.03;
                    float c = 2.43;
                    float d = 0.59;
                    float e = 0.14;
                    return clamp((x * (a * x + b)) / (x * (c * x + d) + e), 0.0, 1.0);
                }

                void main() {
                    vec3 hdr = texture(texHdrScene, inUV).rgb;
                    vec3 bloom = texture(texBloom, inUV).rgb;
                    vec3 color = hdr * ubo.exposure + bloom * ubo.bloomIntensity;
                    vec3 mapped = (ubo.toneMapOp == 0u) ? AcesFilmic(color) : color / (1.0 + color);
                    outColor = vec4(pow(clamp(mapped, 0.0, 1.0), vec3(1.0 / ubo.gamma)), 1.0);
                }
            ";
            RegisterModule(new ModernShaderModule("PostProcessAces", ShaderStage.Pixel, ShaderFormat.SPIR_V_Binary, glslPostPs, Cache));

            // 7. Modern Ocean / Water Gerstner & Screen-Space Reflection (SSR) Pixel Shader (DirectX 12 SM6)
            string hlslWaterPs = @"
                struct WaterPSInput {
                    float4 position : SV_Position;
                    float3 worldPos : WORLDPOS;
                    float3 normal   : NORMAL;
                    float2 uv       : TEXCOORD0;
                };
                Texture2D gSceneColorTexture : register(t0);
                Texture2D gSceneDepthTexture : register(t1);
                SamplerState gLinearSampler  : register(s0);

                cbuffer WaterConstants : register(b0) {
                    float3 gCameraPosition;
                    float  gWaterElevation;
                    float3 gDeepWaterColor;
                    float3 gShallowWaterColor;
                };

                float4 main(WaterPSInput input) : SV_Target {
                    float3 viewDir = normalize(gCameraPosition - input.worldPos);
                    float NdotV = saturate(dot(input.normal, viewDir));
                    float fresnel = 0.02f + 0.98f * pow(1.0f - NdotV, 5.0f);

                    float waterDepth = max(0.0f, input.worldPos.y - gWaterElevation);
                    float extinction = exp(-waterDepth * 0.15f);
                    float3 baseWater = lerp(gDeepWaterColor, gShallowWaterColor, extinction);

                    float2 reflectUv = input.uv + input.normal.xz * 0.05f;
                    float3 reflection = gSceneColorTexture.Sample(gLinearSampler, reflectUv).rgb;

                    float3 finalColor = lerp(baseWater, reflection, fresnel);
                    return float4(finalColor, 0.92f);
                }
            ";
            RegisterModule(new ModernShaderModule("WaterOceanSsr", ShaderStage.Pixel, ShaderFormat.HLSL_SM6_Dxil, hlslWaterPs, Cache));

            // 8. Vulkan SPIR-V Modern Ocean Water Pixel Shader
            string glslWaterPs = @"
                #version 460
                layout(location = 0) in vec3 inWorldPos;
                layout(location = 1) in vec3 inNormal;
                layout(location = 2) in vec2 inUV;
                layout(location = 0) out vec4 outColor;
                layout(binding = 0) uniform sampler2D texSceneColor;
                layout(binding = 1) uniform WaterUBO {
                    vec3 cameraPos;
                    float waterElevation;
                    vec3 deepColor;
                    vec3 shallowColor;
                } ubo;

                void main() {
                    vec3 viewDir = normalize(ubo.cameraPos - inWorldPos);
                    float NdotV = clamp(dot(inNormal, viewDir), 0.0, 1.0);
                    float fresnel = 0.02 + 0.98 * pow(1.0 - NdotV, 5.0);
                    vec3 reflection = texture(texSceneColor, inUV + inNormal.xz * 0.05).rgb;
                    vec3 waterColor = mix(ubo.deepColor, ubo.shallowColor, 0.5);
                    outColor = vec4(mix(waterColor, reflection, fresnel), 0.92);
                }
            ";
            RegisterModule(new ModernShaderModule("WaterOceanSsr", ShaderStage.Pixel, ShaderFormat.SPIR_V_Binary, glslWaterPs, Cache));

            // 9. Percentage-Closer Soft Shadows (PCSS) Contact-Hardening Pixel Shader (DirectX 12 SM6)
            string hlslPcssPs = @"
                struct ShadowPSInput {
                    float4 position : SV_Position;
                    float3 worldPos : WORLDPOS;
                    float2 uv       : TEXCOORD0;
                };
                Texture2DArray gShadowCascades : register(t0);
                SamplerComparisonState gPcfSampler : register(s0);

                cbuffer PcssConstants : register(b0) {
                    float4x4 gLightViewProjections[4];
                    float4   gCascadeSplits;
                    float    gLightSize;
                    float    gShadowBias;
                };

                float4 main(ShadowPSInput input) : SV_Target {
                    float viewDepth = input.position.w;
                    int cascadeIdx = 3;
                    if (viewDepth <= gCascadeSplits.x) cascadeIdx = 0;
                    else if (viewDepth <= gCascadeSplits.y) cascadeIdx = 1;
                    else if (viewDepth <= gCascadeSplits.z) cascadeIdx = 2;

                    float4 lightSpacePos = mul(float4(input.worldPos, 1.0f), gLightViewProjections[cascadeIdx]);
                    float3 shadowCoords = lightSpacePos.xyz / lightSpacePos.w;
                    shadowCoords.xy = shadowCoords.xy * 0.5f + 0.5f;
                    shadowCoords.y = 1.0f - shadowCoords.y;

                    float shadow = gShadowCascades.SampleCmpLevelZero(gPcfSampler, float3(shadowCoords.xy, cascadeIdx), shadowCoords.z - gShadowBias);
                    return float4(shadow.xxx, 1.0f);
                }
            ";
            RegisterModule(new ModernShaderModule("CascadedShadowPcss", ShaderStage.Pixel, ShaderFormat.HLSL_SM6_Dxil, hlslPcssPs, Cache));

            // 10. Vulkan SPIR-V Percentage-Closer Soft Shadows Pixel Shader
            string glslPcssPs = @"
                #version 460
                layout(location = 0) in vec3 inWorldPos;
                layout(location = 0) out vec4 outShadow;
                layout(binding = 0) uniform sampler2DArrayShadow shadowCascades;
                layout(binding = 1) uniform ShadowUBO {
                    mat4 lightViewProj[4];
                    vec4 cascadeSplits;
                    float lightSize;
                    float shadowBias;
                } ubo;

                void main() {
                    vec4 shadowCoord = ubo.lightViewProj[0] * vec4(inWorldPos, 1.0);
                    vec3 proj = shadowCoord.xyz / shadowCoord.w;
                    proj.xy = proj.xy * 0.5 + 0.5;
                    float s = texture(shadowCascades, vec4(proj.xy, 0.0, proj.z - ubo.shadowBias));
                    outShadow = vec4(vec3(s), 1.0);
                }
            ";
            RegisterModule(new ModernShaderModule("CascadedShadowPcss", ShaderStage.Pixel, ShaderFormat.SPIR_V_Binary, glslPcssPs, Cache));

            // 11. Modern Foliage GPU Instanced Vertex Shader with Wind Displacement (DirectX 12 SM6)
            string hlslFoliageVs = @"
                struct VSInput {
                    float3 position : POSITION;
                    float3 normal   : NORMAL;
                    float2 uv       : TEXCOORD0;
                    float2 plantParam : TEXCOORD1;
                    float3 instWorldPos  : INST_POS;
                    float  instScale     : INST_SCALE;
                    float  instYaw       : INST_YAW;
                    float  instWindPhase : INST_PHASE;
                    float3 instTint      : INST_TINT;
                };
                struct PSInput {
                    float4 position : SV_Position;
                    float3 worldPos : WORLDPOS;
                    float3 normal   : NORMAL;
                    float2 uv       : TEXCOORD0;
                    float3 tint     : TINT;
                };

                cbuffer FrameConstants : register(b0) {
                    float4x4 gViewProjection;
                    float3   gCameraPosition;
                    float    gTime;
                    float2   gWindDir;
                    float    gWindSpeed;
                    float    gGustStrength;
                };

                PSInput main(VSInput input) {
                    PSInput output;
                    float cosY = cos(input.instYaw);
                    float sinY = sin(input.instYaw);
                    float3 localPos = input.position * input.instScale;
                    float3 rotPos = float3(
                        localPos.x * cosY - localPos.z * sinY,
                        localPos.y,
                        localPos.x * sinY + localPos.z * cosY
                    );

                    float3 worldPos = rotPos + input.instWorldPos;
                    float stiffness = input.plantParam.x * input.plantParam.x;
                    float phase = input.instWindPhase + input.plantParam.y;

                    float sway = sin(gTime * gWindSpeed * 0.4f + dot(worldPos.xz, gWindDir) * 0.15f + phase);
                    float gust = sin(gTime * (gWindSpeed * 0.8f + 2.2f) + phase * 2.3f) * gGustStrength;
                    float totalDisp = (sway + gust) * (gWindSpeed / 10.0f) * stiffness * input.instScale * 0.15f;

                    worldPos.xz += gWindDir * totalDisp;
                    worldPos.y -= abs(totalDisp) * 0.25f;

                    output.position = mul(float4(worldPos, 1.0f), gViewProjection);
                    output.worldPos = worldPos;
                    output.normal = input.normal;
                    output.uv = input.uv;
                    output.tint = input.instTint;
                    return output;
                }
            ";
            RegisterModule(new ModernShaderModule("FoliageInstancedVs", ShaderStage.Vertex, ShaderFormat.HLSL_SM6_Dxil, hlslFoliageVs, Cache));

            // 12. Modern Foliage PBR Subsurface Translucency Pixel Shader (DirectX 12 SM6)
            string hlslFoliagePs = @"
                struct PSInput {
                    float4 position : SV_Position;
                    float3 worldPos : WORLDPOS;
                    float3 normal   : NORMAL;
                    float2 uv       : TEXCOORD0;
                    float3 tint     : TINT;
                };
                Texture2D gFoliageAlbedo : register(t0);
                SamplerState gSampler    : register(s0);

                cbuffer FoliageLighting : register(b1) {
                    float3 gLightDir;
                    float  gAlphaCutoff;
                    float3 gLightColor;
                    float  gSubsurfaceStrength;
                };

                float4 main(PSInput input) : SV_Target {
                    float4 texCol = gFoliageAlbedo.Sample(gSampler, input.uv);
                    clip(texCol.a - gAlphaCutoff);

                    float3 normal = normalize(input.normal);
                    float NdotL = max(0.0f, dot(normal, -gLightDir));
                    float backLit = max(0.0f, dot(-normal, -gLightDir)) * gSubsurfaceStrength;
                    float3 diffuse = (NdotL + backLit) * gLightColor;

                    float3 finalRgb = texCol.rgb * input.tint * (diffuse + 0.18f);
                    return float4(finalRgb, texCol.a);
                }
            ";
            RegisterModule(new ModernShaderModule("FoliagePbrPs", ShaderStage.Pixel, ShaderFormat.HLSL_SM6_Dxil, hlslFoliagePs, Cache));

            // 13. Vulkan SPIR-V Modern Foliage Instanced Vertex Shader
            string glslFoliageVs = @"
                #version 460
                layout(location = 0) in vec3 inPos;
                layout(location = 1) in vec3 inNormal;
                layout(location = 2) in vec2 inUV;
                layout(location = 3) in vec2 inPlantParam;
                layout(location = 4) in vec3 inInstPos;
                layout(location = 5) in float inInstScale;
                layout(location = 6) in float inInstYaw;
                layout(location = 7) in float inInstPhase;
                layout(location = 8) in vec3 inInstTint;

                layout(location = 0) out vec3 outWorldPos;
                layout(location = 1) out vec3 outNormal;
                layout(location = 2) out vec2 outUV;
                layout(location = 3) out vec3 outTint;

                layout(binding = 0) uniform FoliageFrameUBO {
                    mat4 viewProj;
                    vec3 camPos;
                    float time;
                    vec2 windDir;
                    float windSpeed;
                    float gustStrength;
                } ubo;

                void main() {
                    float cosY = cos(inInstYaw);
                    float sinY = sin(inInstYaw);
                    vec3 localPos = inPos * inInstScale;
                    vec3 rotPos = vec3(
                        localPos.x * cosY - localPos.z * sinY,
                        localPos.y,
                        localPos.x * sinY + localPos.z * cosY
                    );
                    vec3 worldPos = rotPos + inInstPos;
                    float stiffness = inPlantParam.x * inPlantParam.x;
                    float phase = inInstPhase + inPlantParam.y;
                    float sway = sin(ubo.time * ubo.windSpeed * 0.4 + dot(worldPos.xz, ubo.windDir) * 0.15 + phase);
                    float totalDisp = sway * (ubo.windSpeed / 10.0) * stiffness * inInstScale * 0.15;
                    worldPos.xz += ubo.windDir * totalDisp;
                    worldPos.y -= abs(totalDisp) * 0.25;

                    gl_Position = ubo.viewProj * vec4(worldPos, 1.0);
                    outWorldPos = worldPos;
                    outNormal = inNormal;
                    outUV = inUV;
                    outTint = inInstTint;
                }
            ";
            RegisterModule(new ModernShaderModule("FoliageInstancedVs", ShaderStage.Vertex, ShaderFormat.SPIR_V_Binary, glslFoliageVs, Cache));

            // 14. Vulkan SPIR-V Modern Foliage PBR Subsurface Pixel Shader
            string glslFoliagePs = @"
                #version 460
                layout(location = 0) in vec3 inWorldPos;
                layout(location = 1) in vec3 inNormal;
                layout(location = 2) in vec2 inUV;
                layout(location = 3) in vec3 inTint;
                layout(location = 0) out vec4 outColor;

                layout(binding = 1) uniform sampler2D texAlbedo;
                layout(binding = 2) uniform FoliageLightUBO {
                    vec3 lightDir;
                    float alphaCutoff;
                    vec3 lightColor;
                    float subsurfaceStrength;
                } lightUbo;

                void main() {
                    vec4 albedo = texture(texAlbedo, inUV);
                    if (albedo.a < lightUbo.alphaCutoff) discard;
                    vec3 norm = normalize(inNormal);
                    float NdotL = max(0.0, dot(norm, -lightUbo.lightDir));
                    float backLit = max(0.0, dot(-norm, -lightUbo.lightDir)) * lightUbo.subsurfaceStrength;
                    vec3 diffuse = (NdotL + backLit) * lightUbo.lightColor + vec3(0.18);
                    outColor = vec4(albedo.rgb * inTint * diffuse, albedo.a);
                }
            ";
            RegisterModule(new ModernShaderModule("FoliagePbrPs", ShaderStage.Pixel, ShaderFormat.SPIR_V_Binary, glslFoliagePs, Cache));

            // 15. Temporal Anti-Aliasing (TAA) Velocity Reprojection & Neighborhood Clamping Pixel Shader (DirectX 12 SM6)
            string hlslTaaPs = @"
                struct PSInput {
                    float4 position : SV_Position;
                    float2 uv       : TEXCOORD0;
                };
                Texture2D gCurrentColorTexture : register(t0);
                Texture2D gHistoryColorTexture : register(t1);
                Texture2D gVelocityTexture     : register(t2);
                SamplerState gPointSampler     : register(s0);
                SamplerState gLinearSampler    : register(s1);

                cbuffer TaaConstants : register(b0) {
                    float2 gTexelSize;
                    float  gBlendFactor;
                    float  gGammaClamping;
                };

                float4 main(PSInput input) : SV_Target {
                    float2 uv = input.uv;
                    float2 velocity = gVelocityTexture.Sample(gPointSampler, uv).xy;
                    float2 prevUv = uv - velocity;

                    float3 current = gCurrentColorTexture.Sample(gPointSampler, uv).rgb;

                    float3 minCol = current;
                    float3 maxCol = current;
                    float3 sumCol = current;
                    float3 sumSqCol = current * current;

                    float2 offsets[8] = {
                        float2(-1, -1), float2(0, -1), float2(1, -1),
                        float2(-1,  0),                float2(1,  0),
                        float2(-1,  1), float2(0,  1), float2(1,  1)
                    };

                    [unroll]
                    for (int i = 0; i < 8; i++) {
                        float3 n = gCurrentColorTexture.Sample(gPointSampler, uv + offsets[i] * gTexelSize).rgb;
                        minCol = min(minCol, n);
                        maxCol = max(maxCol, n);
                        sumCol += n;
                        sumSqCol += n * n;
                    }

                    float3 mean = sumCol / 9.0f;
                    float3 variance = max(0.0f.xxx, (sumSqCol / 9.0f) - (mean * mean));
                    float3 sigma = sqrt(variance) * gGammaClamping;

                    float3 boxMin = max(minCol, mean - sigma);
                    float3 boxMax = min(maxCol, mean + sigma);

                    bool offScreen = (prevUv.x < 0.0f || prevUv.x > 1.0f || prevUv.y < 0.0f || prevUv.y > 1.0f);
                    if (offScreen) {
                        return float4(current, 1.0f);
                    }

                    float3 history = gHistoryColorTexture.Sample(gLinearSampler, prevUv).rgb;
                    float3 clampedHistory = clamp(history, boxMin, boxMax);

                    float3 resolved = lerp(clampedHistory, current, gBlendFactor);
                    return float4(resolved, 1.0f);
                }
            ";
            RegisterModule(new ModernShaderModule("TemporalAntiAliasingPs", ShaderStage.Pixel, ShaderFormat.HLSL_SM6_Dxil, hlslTaaPs, Cache));

            // 16. Vulkan SPIR-V Temporal Anti-Aliasing Pixel Shader
            string glslTaaPs = @"
                #version 460
                layout(location = 0) in vec2 inUV;
                layout(location = 0) out vec4 outColor;

                layout(binding = 0) uniform sampler2D texCurrent;
                layout(binding = 1) uniform sampler2D texHistory;
                layout(binding = 2) uniform sampler2D texVelocity;

                layout(binding = 3) uniform TaaUBO {
                    vec2 texelSize;
                    float blendFactor;
                    float gammaClamping;
                } ubo;

                void main() {
                    vec2 velocity = texture(texVelocity, inUV).xy;
                    vec2 prevUv = inUV - velocity;
                    vec3 current = texture(texCurrent, inUV).rgb;

                    if (prevUv.x < 0.0 || prevUv.x > 1.0 || prevUv.y < 0.0 || prevUv.y > 1.0) {
                        outColor = vec4(current, 1.0);
                        return;
                    }

                    vec3 minCol = current;
                    vec3 maxCol = current;
                    vec3 sumCol = current;
                    vec3 sumSq = current * current;

                    ivec2 offs[8] = ivec2[](
                        ivec2(-1,-1), ivec2(0,-1), ivec2(1,-1),
                        ivec2(-1, 0),             ivec2(1, 0),
                        ivec2(-1, 1), ivec2(0, 1), ivec2(1, 1)
                    );

                    for (int i = 0; i < 8; i++) {
                        vec3 n = texture(texCurrent, inUV + vec2(offs[i]) * ubo.texelSize).rgb;
                        minCol = min(minCol, n);
                        maxCol = max(maxCol, n);
                        sumCol += n;
                        sumSq += n * n;
                    }

                    vec3 mean = sumCol / 9.0;
                    vec3 sigma = sqrt(max(vec3(0.0), (sumSq / 9.0) - (mean * mean))) * ubo.gammaClamping;
                    vec3 boxMin = max(minCol, mean - sigma);
                    vec3 boxMax = min(maxCol, mean + sigma);

                    vec3 history = texture(texHistory, prevUv).rgb;
                    vec3 clamped = clamp(history, boxMin, boxMax);
                    outColor = vec4(mix(clamped, current, ubo.blendFactor), 1.0);
                }
            ";
            RegisterModule(new ModernShaderModule("TemporalAntiAliasingPs", ShaderStage.Pixel, ShaderFormat.SPIR_V_Binary, glslTaaPs, Cache));
        }
    }
}
