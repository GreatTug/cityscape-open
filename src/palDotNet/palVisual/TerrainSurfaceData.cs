using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;

namespace palDotNet.palVisual
{
    /// <summary>
    /// Represents the active physical terrain geometry, scale, tiling, and sampling properties.
    /// Synchronizes the New Terrain dialog parameters with the 3D viewport rendering engine.
    /// </summary>
    public class TerrainSurfaceData
    {
        public string Name { get; set; } = "Untitled Terrain";
        public float SizeX { get; set; } = 1024.0f;
        public float SizeY { get; set; } = 1024.0f;
        public float TileSizeX { get; set; } = 1024.0f;
        public float TileSizeY { get; set; } = 1024.0f;
        public int TileCountX { get; set; } = 1;
        public int TileCountY { get; set; } = 1;
        public float SamplingDistance { get; set; } = 2.0f;
        public double OriginLongitude { get; set; } = 0.0;
        public double OriginLatitude { get; set; } = 0.0;
        public bool IsGeolocated { get; set; } = false;
        public bool HasActiveTerrain { get; set; } = true;
        public float ElevationMin { get; set; } = 0.0f;
        public float ElevationMax { get; set; } = 150.0f;

        public double AreaSquareMeters => (double)SizeX * (double)SizeY;
        public double AreaSquareKilometers => AreaSquareMeters / 1_000_000.0;
        public float AspectRatio => SizeY > 0.001f ? SizeX / SizeY : 1.0f;

        public long TotalSamplePoints
        {
            get
            {
                if (SamplingDistance <= 0.01f) return 0;
                long samplesX = (long)Math.Max(1, SizeX / SamplingDistance);
                long samplesY = (long)Math.Max(1, SizeY / SamplingDistance);
                return samplesX * samplesY;
            }
        }

        public TerrainSurfaceData()
        {
        }

        public TerrainSurfaceData(float sizeX, float sizeY, float tileSizeX, float tileSizeY, int tileCountX, int tileCountY, float sampleDist)
        {
            SizeX = Math.Max(1.0f, sizeX);
            SizeY = Math.Max(1.0f, sizeY);
            TileSizeX = Math.Max(1.0f, tileSizeX > 0f ? tileSizeX : sizeX);
            TileSizeY = Math.Max(1.0f, tileSizeY > 0f ? tileSizeY : sizeY);
            TileCountX = Math.Max(1, tileCountX);
            TileCountY = Math.Max(1, tileCountY);
            SamplingDistance = Math.Max(0.01f, sampleDist);
            HasActiveTerrain = true;
        }

        public static TerrainSurfaceData CreateDefault()
        {
            return new TerrainSurfaceData(1024.0f, 1024.0f, 1024.0f, 1024.0f, 1, 1, 2.0f);
        }

        /// <summary>
        /// Evaluates procedural or imported elevation at world position (X, Z).
        /// </summary>
        public float EvaluateElevation(float x, float z)
        {
            // Smooth natural rolling terrain relief proportional to scale
            float nx = x / Math.Max(100.0f, SizeX);
            float nz = z / Math.Max(100.0f, SizeY);

            float w1 = (float)Math.Sin(nx * 12.0) * (float)Math.Cos(nz * 12.0) * 0.5f;
            float w2 = (float)Math.Sin(nx * 3.5 + 0.8) * (float)Math.Cos(nz * 4.2 - 0.4) * 0.35f;
            float w3 = (float)Math.Cos(nx * 20.0 + nz * 15.0) * 0.15f;

            float reliefScale = Math.Min(ElevationMax, Math.Max(10.0f, Math.Min(SizeX, SizeY) * 0.035f));
            return (w1 + w2 + w3 + 0.5f) * reliefScale;
        }

        /// <summary>
        /// Generates a complete physical 3D mesh representation of this terrain surface,
        /// including surface normals, PBR material properties, and texture UV mapping.
        /// </summary>
        public palModernEngine.ExportMeshData GenerateExportMesh(int resX = 64, int resY = 64)
        {
            var mesh = new palModernEngine.ExportMeshData();
            mesh.Name = string.IsNullOrEmpty(Name) ? "ActiveTerrainMesh" : Name.Replace(" ", "_");
            mesh.Material = new palModernEngine.PbrMaterialParameters
            {
                BaseColorR = 0.35f,
                BaseColorG = 0.55f,
                BaseColorB = 0.32f,
                BaseColorA = 1.0f,
                Roughness = 0.85f,
                Metallic = 0.05f
            };

            int nx = Math.Max(2, Math.Min(256, resX));
            int ny = Math.Max(2, Math.Min(256, resY));

            float halfX = SizeX * 0.5f;
            float halfY = SizeY * 0.5f;
            float dx = SizeX / (nx - 1);
            float dy = SizeY / (ny - 1);

            float[,] heights = new float[nx, ny];
            for (int i = 0; i < nx; i++)
            {
                float x = -halfX + i * dx;
                for (int j = 0; j < ny; j++)
                {
                    float z = -halfY + j * dy;
                    heights[i, j] = EvaluateElevation(x, z);
                }
            }

            for (int i = 0; i < nx; i++)
            {
                float x = -halfX + i * dx;
                float u = (float)i / (nx - 1);

                for (int j = 0; j < ny; j++)
                {
                    float z = -halfY + j * dy;
                    float v = (float)j / (ny - 1);
                    float y = heights[i, j];

                    float hL = i > 0 ? heights[i - 1, j] : y;
                    float hR = i < nx - 1 ? heights[i + 1, j] : y;
                    float hD = j > 0 ? heights[i, j - 1] : y;
                    float hU = j < ny - 1 ? heights[i, j + 1] : y;

                    float normX = -(hR - hL) / (2.0f * dx);
                    float normZ = -(hU - hD) / (2.0f * dy);
                    float normY = 1.0f;
                    float len = (float)Math.Sqrt(normX * normX + normY * normY + normZ * normZ);
                    if (len > 0.0001f) { normX /= len; normY /= len; normZ /= len; }

                    mesh.Vertices.Add(new palModernEngine.ExportVertex
                    {
                        X = x,
                        Y = y,
                        Z = z,
                        Nx = normX,
                        Ny = normY,
                        Nz = normZ,
                        U = u,
                        V = v
                    });
                }
            }

            for (int i = 0; i < nx - 1; i++)
            {
                for (int j = 0; j < ny - 1; j++)
                {
                    uint i00 = (uint)(i * ny + j);
                    uint i01 = (uint)(i * ny + (j + 1));
                    uint i10 = (uint)((i + 1) * ny + j);
                    uint i11 = (uint)((i + 1) * ny + (j + 1));

                    mesh.Indices.Add(i00);
                    mesh.Indices.Add(i10);
                    mesh.Indices.Add(i01);

                    mesh.Indices.Add(i01);
                    mesh.Indices.Add(i10);
                    mesh.Indices.Add(i11);
                }
            }

            return mesh;
        }

        /// <summary>
        /// Exports normalized elevation data as a 24-bit Grayscale PNG heightmap image.
        /// </summary>
        public bool ExportHeightfieldImage(string filePath, int resX = 512, int resY = 512)
        {
            if (string.IsNullOrEmpty(filePath)) return false;

            int rx = Math.Max(16, Math.Min(2048, resX));
            int ry = Math.Max(16, Math.Min(2048, resY));

            using (Bitmap bmp = new Bitmap(rx, ry, System.Drawing.Imaging.PixelFormat.Format24bppRgb))
            {
                float halfX = SizeX * 0.5f;
                float halfY = SizeY * 0.5f;
                float dx = SizeX / (rx - 1);
                float dy = SizeY / (ry - 1);
                float minH = float.MaxValue;
                float maxH = float.MinValue;
                float[,] grid = new float[rx, ry];

                for (int x = 0; x < rx; x++)
                {
                    float wx = -halfX + x * dx;
                    for (int y = 0; y < ry; y++)
                    {
                        float wz = -halfY + y * dy;
                        float h = EvaluateElevation(wx, wz);
                        grid[x, y] = h;
                        if (h < minH) minH = h;
                        if (h > maxH) maxH = h;
                    }
                }

                float range = Math.Max(0.001f, maxH - minH);
                for (int y = 0; y < ry; y++)
                {
                    for (int x = 0; x < rx; x++)
                    {
                        float norm = (grid[x, y] - minH) / range;
                        int val = Math.Max(0, Math.Min(255, (int)(norm * 255.0f)));
                        bmp.SetPixel(x, y, Color.FromArgb(val, val, val));
                    }
                }

                bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                return true;
            }
        }

        /// <summary>
        /// Exports high-precision raw 32-bit floating-point Digital Elevation Model (DEM) data.
        /// </summary>
        public bool ExportHeightfieldRaw(string filePath, int resX = 512, int resY = 512)
        {
            if (string.IsNullOrEmpty(filePath)) return false;

            int rx = Math.Max(16, Math.Min(2048, resX));
            int ry = Math.Max(16, Math.Min(2048, resY));

            float halfX = SizeX * 0.5f;
            float halfY = SizeY * 0.5f;
            float dx = SizeX / (rx - 1);
            float dy = SizeY / (ry - 1);

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                bw.Write(System.Text.Encoding.ASCII.GetBytes("RAW_DEM\0"));
                bw.Write(rx);
                bw.Write(ry);
                bw.Write(SizeX);
                bw.Write(SizeY);
                bw.Write(SamplingDistance);

                for (int y = 0; y < ry; y++)
                {
                    float wz = -halfY + y * dy;
                    for (int x = 0; x < rx; x++)
                    {
                        float wx = -halfX + x * dx;
                        bw.Write(EvaluateElevation(wx, wz));
                    }
                }
            }
            return true;
        }
    }
}
