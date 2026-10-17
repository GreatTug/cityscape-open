using System;
using System.Collections.Generic;
using System.Drawing;
using palDotNet.palModernEngine;

namespace palDotNet.palVisual
{
    public enum BuildingStyle
    {
        ModernGlassTower = 0,
        CommercialOffice = 1,
        ResidentialApartment = 2,
        HistoricBrick = 3,
        SuburbanVilla = 4,
        Skyscraper = 5
    }

    public enum RoofType
    {
        Flat = 0,
        Gable = 1,
        Hip = 2,
        Mansard = 3
    }

    /// <summary>
    /// Represents a 2D building footprint polygon and height attributes.
    /// </summary>
    public class GisBuildingFootprint
    {
        public string Name { get; set; } = "Building";
        public BuildingStyle Style { get; set; } = BuildingStyle.CommercialOffice;
        public RoofType Roof { get; set; } = RoofType.Flat;
        public int FloorCount { get; set; } = 4;
        public float FloorHeightMeters { get; set; } = 3.5f;
        public List<PointF> Polygon { get; } = new List<PointF>();

        public float TotalHeight => FloorCount * FloorHeightMeters;

        public PointF Center
        {
            get
            {
                if (Polygon.Count == 0) return PointF.Empty;
                float sx = 0f, sz = 0f;
                foreach (var pt in Polygon)
                {
                    sx += pt.X;
                    sz += pt.Y;
                }
                return new PointF(sx / Polygon.Count, sz / Polygon.Count);
            }
        }
    }

    /// <summary>
    /// 3D extruded procedural building instance placed on the terrain surface.
    /// Implements ISpatialObject for hierarchical spatial Quadtree indexing and fast frustum culling.
    /// </summary>
    public class GisBuildingInstance : ISpatialObject
    {
        public GisBuildingFootprint Footprint { get; set; }
        public float CenterX { get; set; }
        public float CenterZ { get; set; }
        public float BaseElevation { get; set; }
        public float TotalHeight { get; set; }
        public Color FacadeColor { get; set; } = Color.FromArgb(200, 210, 220);
        public Color RoofColor { get; set; } = Color.FromArgb(70, 75, 80);

        public string SpatialId => Footprint?.Name ?? "Building";

        public BoundingBox3D SpatialBounds
        {
            get
            {
                if (Footprint == null || Footprint.Polygon.Count == 0)
                {
                    return new BoundingBox3D(CenterX - 10f, BaseElevation, CenterZ - 10f,
                                             CenterX + 10f, BaseElevation + TotalHeight, CenterZ + 10f);
                }
                float minX = float.MaxValue, minZ = float.MaxValue;
                float maxX = float.MinValue, maxZ = float.MinValue;
                foreach (var pt in Footprint.Polygon)
                {
                    if (pt.X < minX) minX = pt.X;
                    if (pt.X > maxX) maxX = pt.X;
                    if (pt.Y < minZ) minZ = pt.Y;
                    if (pt.Y > maxZ) maxZ = pt.Y;
                }
                return new BoundingBox3D(minX, BaseElevation, minZ, maxX, BaseElevation + TotalHeight, maxZ);
            }
        }

        public GisBuildingInstance(GisBuildingFootprint footprint, TerrainSurfaceData terrain = null)
        {
            Footprint = footprint ?? throw new ArgumentNullException(nameof(footprint));
            var c = footprint.Center;
            CenterX = c.X;
            CenterZ = c.Y;
            TotalHeight = footprint.TotalHeight;

            if (terrain != null)
            {
                BaseElevation = terrain.EvaluateElevation(CenterX, CenterZ);
            }
            else
            {
                BaseElevation = 0f;
            }

            // Assign procedural colors by style
            switch (footprint.Style)
            {
                case BuildingStyle.Skyscraper:
                    FacadeColor = Color.FromArgb(140, 185, 230); // Mirrored cyan-blue glass
                    RoofColor = Color.FromArgb(50, 55, 65);
                    break;
                case BuildingStyle.ModernGlassTower:
                    FacadeColor = Color.FromArgb(170, 205, 225);
                    RoofColor = Color.FromArgb(60, 70, 80);
                    break;
                case BuildingStyle.HistoricBrick:
                    FacadeColor = Color.FromArgb(185, 85, 65); // Terracotta red brick
                    RoofColor = Color.FromArgb(55, 50, 50);
                    break;
                case BuildingStyle.ResidentialApartment:
                    FacadeColor = Color.FromArgb(220, 215, 195); // Warm stucco
                    RoofColor = Color.FromArgb(160, 70, 50); // Clay tile
                    break;
                default:
                    FacadeColor = Color.FromArgb(200, 205, 210);
                    RoofColor = Color.FromArgb(70, 75, 80);
                    break;
            }
        }

        /// <summary>
        /// Extrudes the 2D polygon footprint vertically into a 3D mesh with walls, roof, and materials.
        /// </summary>
        public ExportMeshData GenerateMesh()
        {
            var mesh = new ExportMeshData
            {
                Name = Footprint?.Name ?? "BuildingMesh",
                Material = new PbrMaterialParameters
                {
                    BaseColorR = FacadeColor.R / 255f,
                    BaseColorG = FacadeColor.G / 255f,
                    BaseColorB = FacadeColor.B / 255f,
                    BaseColorA = 1.0f,
                    Metallic = Footprint.Style == BuildingStyle.Skyscraper ? 0.7f : 0.1f,
                    Roughness = Footprint.Style == BuildingStyle.Skyscraper ? 0.2f : 0.7f
                }
            };

            var poly = Footprint.Polygon;
            if (poly.Count < 3) return mesh;

            int n = poly.Count;
            float bY = BaseElevation;
            float tY = BaseElevation + TotalHeight;

            // 1. Generate Extruded Wall Quads
            for (int i = 0; i < n; i++)
            {
                int next = (i + 1) % n;
                PointF p0 = poly[i];
                PointF p1 = poly[next];

                // Compute wall face normal
                float edgeX = p1.X - p0.X;
                float edgeZ = p1.Y - p0.Y;
                float normX = edgeZ;
                float normZ = -edgeX;
                float len = (float)Math.Sqrt(normX * normX + normZ * normZ);
                if (len > 0.0001f) { normX /= len; normZ /= len; }

                uint baseIdx = (uint)mesh.Vertices.Count;

                // 4 vertices per wall segment: (p0 bottom, p1 bottom, p1 top, p0 top)
                mesh.Vertices.Add(new ExportVertex { X = p0.X, Y = bY, Z = p0.Y, Nx = normX, Ny = 0f, Nz = normZ, U = 0f, V = 0f });
                mesh.Vertices.Add(new ExportVertex { X = p1.X, Y = bY, Z = p1.Y, Nx = normX, Ny = 0f, Nz = normZ, U = 1f, V = 0f });
                mesh.Vertices.Add(new ExportVertex { X = p1.X, Y = tY, Z = p1.Y, Nx = normX, Ny = 0f, Nz = normZ, U = 1f, V = Footprint.FloorCount });
                mesh.Vertices.Add(new ExportVertex { X = p0.X, Y = tY, Z = p0.Y, Nx = normX, Ny = 0f, Nz = normZ, U = 0f, V = Footprint.FloorCount });

                // Triangles: 0-1-2 and 0-2-3
                mesh.Indices.Add(baseIdx);
                mesh.Indices.Add(baseIdx + 1);
                mesh.Indices.Add(baseIdx + 2);

                mesh.Indices.Add(baseIdx);
                mesh.Indices.Add(baseIdx + 2);
                mesh.Indices.Add(baseIdx + 3);
            }

            // 2. Generate Roof Fan / Triangulation
            uint roofCenterIdx = (uint)mesh.Vertices.Count;
            float roofPeakY = tY + (Footprint.Roof == RoofType.Gable || Footprint.Roof == RoofType.Hip ? 4.0f : 0.0f);
            mesh.Vertices.Add(new ExportVertex { X = CenterX, Y = roofPeakY, Z = CenterZ, Nx = 0f, Ny = 1f, Nz = 0f, U = 0.5f, V = 0.5f });

            uint roofStartIdx = (uint)mesh.Vertices.Count;
            for (int i = 0; i < n; i++)
            {
                PointF pt = poly[i];
                mesh.Vertices.Add(new ExportVertex { X = pt.X, Y = tY, Z = pt.Y, Nx = 0f, Ny = 1f, Nz = 0f, U = (pt.X - CenterX) * 0.05f + 0.5f, V = (pt.Y - CenterZ) * 0.05f + 0.5f });
            }

            for (int i = 0; i < n; i++)
            {
                int next = (i + 1) % n;
                mesh.Indices.Add(roofCenterIdx);
                mesh.Indices.Add(roofStartIdx + (uint)i);
                mesh.Indices.Add(roofStartIdx + (uint)next);
            }

            return mesh;
        }
    }

    /// <summary>
    /// Procedural Urban Block and GIS / OSM Building Footprint Generator.
    /// Creates realistic cities, zoning blocks, and skyscraper skylines.
    /// </summary>
    public static class GisProceduralCityGenerator
    {
        public static List<GisBuildingInstance> GenerateUrbanGrid(
            TerrainSurfaceData terrain,
            int blocksX = 5,
            int blocksZ = 5,
            float blockSize = 140.0f,
            float streetWidth = 20.0f,
            int randomSeed = 42)
        {
            var buildings = new List<GisBuildingInstance>();
            if (terrain == null) return buildings;

            Random rng = new Random(randomSeed);
            float totalBlockStep = blockSize + streetWidth;
            float startX = -((blocksX * totalBlockStep) * 0.5f);
            float startZ = -((blocksZ * totalBlockStep) * 0.5f);

            for (int bx = 0; bx < blocksX; bx++)
            {
                float blockMinX = startX + bx * totalBlockStep;
                for (int bz = 0; bz < blocksZ; bz++)
                {
                    float blockMinZ = startZ + bz * totalBlockStep;
                    // Subdivide block into parcels (e.g. 2x2 or 3x2 parcels per block)
                    int parcelsX = 2;
                    int parcelsZ = 2;
                    float parcelW = (blockSize - 8.0f) / parcelsX;
                    float parcelD = (blockSize - 8.0f) / parcelsZ;

                    float distFromCenter = (float)Math.Sqrt((blockMinX + blockSize * 0.5f) * (blockMinX + blockSize * 0.5f) +
                                                          (blockMinZ + blockSize * 0.5f) * (blockMinZ + blockSize * 0.5f));
                    float maxDist = (float)Math.Sqrt(terrain.SizeX * terrain.SizeX + terrain.SizeY * terrain.SizeY) * 0.5f;
                    float centerProximity = Math.Max(0f, 1.0f - (distFromCenter / Math.Max(100f, maxDist)));

                    for (int px = 0; px < parcelsX; px++)
                    {
                        for (int pz = 0; pz < parcelsZ; pz++)
                        {
                            float pMinX = blockMinX + 4.0f + px * parcelW;
                            float pMinZ = blockMinZ + 4.0f + pz * parcelD;
                            float setback = 4.0f;
                            float bW = parcelW - setback * 2.0f;
                            float bD = parcelD - setback * 2.0f;

                            if (bW < 10f || bD < 10f) continue;

                            var fp = new GisBuildingFootprint();
                            fp.Name = string.Format("Building_{0}_{1}_{2}_{3}", bx, bz, px, pz);

                            // Determine building height based on center proximity
                            if (centerProximity > 0.65f)
                            {
                                fp.Style = BuildingStyle.Skyscraper;
                                fp.FloorCount = rng.Next(18, 48);
                                fp.Roof = RoofType.Flat;
                            }
                            else if (centerProximity > 0.35f)
                            {
                                fp.Style = rng.NextDouble() > 0.5 ? BuildingStyle.ModernGlassTower : BuildingStyle.CommercialOffice;
                                fp.FloorCount = rng.Next(6, 18);
                                fp.Roof = RoofType.Flat;
                            }
                            else
                            {
                                fp.Style = rng.NextDouble() > 0.5 ? BuildingStyle.ResidentialApartment : BuildingStyle.HistoricBrick;
                                fp.FloorCount = rng.Next(2, 6);
                                fp.Roof = rng.NextDouble() > 0.4 ? RoofType.Gable : RoofType.Flat;
                            }

                            // Define rectangular footprint polygon
                            fp.Polygon.Add(new PointF(pMinX + setback, pMinZ + setback));
                            fp.Polygon.Add(new PointF(pMinX + setback + bW, pMinZ + setback));
                            fp.Polygon.Add(new PointF(pMinX + setback + bW, pMinZ + setback + bD));
                            fp.Polygon.Add(new PointF(pMinX + setback, pMinZ + setback + bD));

                            var inst = new GisBuildingInstance(fp, terrain);
                            buildings.Add(inst);
                        }
                    }
                }
            }

            return buildings;
        }
    }
}
