using System;
using System.Collections.Generic;
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

        public ModernShaderModule(string name, ShaderStage stage, ShaderFormat format, string source)
        {
            Name = name;
            Stage = stage;
            Format = format;
            SourceCode = source;
            Bytecode = Encoding.UTF8.GetBytes(source ?? string.Empty);
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

        public ModernShaderPipeline()
        {
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
            RegisterModule(new ModernShaderModule("StandardPBR", ShaderStage.Vertex, ShaderFormat.HLSL_SM6_Dxil, hlslPbrVs));

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
            RegisterModule(new ModernShaderModule("StandardPBR", ShaderStage.Pixel, ShaderFormat.HLSL_SM6_Dxil, hlslPbrPs));

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
            RegisterModule(new ModernShaderModule("TerrainSplat", ShaderStage.Vertex, ShaderFormat.SPIR_V_Binary, glslTerrainVs));

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
            RegisterModule(new ModernShaderModule("TerrainSplat", ShaderStage.Pixel, ShaderFormat.SPIR_V_Binary, glslTerrainPs));
        }
    }
}
