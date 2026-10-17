using System;
using System.Drawing;
using System.Collections.Generic;

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
    }
}
