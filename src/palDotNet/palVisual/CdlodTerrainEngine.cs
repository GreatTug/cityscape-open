using System;
using System.Collections.Generic;
using System.Drawing;
using palDotNet.palModernEngine;

namespace palDotNet.palVisual
{
    /// <summary>
    /// Represents an individual terrain chunk selected for rendering with continuous Level-of-Detail (CDLOD).
    /// </summary>
    public class CdlodRenderChunk
    {
        public CdlodChunkNode Node { get; set; }
        public int LodLevel { get; set; }
        public float MorphFactor { get; set; } // 0.0f = full current LOD, 1.0f = morphed towards coarser LOD

        public CdlodRenderChunk(CdlodChunkNode node, int lodLevel, float morphFactor = 0f)
        {
            Node = node;
            LodLevel = lodLevel;
            MorphFactor = Math.Max(0f, Math.Min(1f, morphFactor));
        }

        /// <summary>
        /// Generates a local 3D mesh grid for this chunk at the selected LOD resolution.
        /// </summary>
        public ExportMeshData GenerateMesh(TerrainSurfaceData terrain, int baseGridResolution = 8)
        {
            if (terrain == null || Node == null) return null;

            float chunkLenX = Node.MaxX - Node.MinX;
            float chunkLenZ = Node.MaxZ - Node.MinZ;
            float aspectXZ = chunkLenX / Math.Max(1.0f, chunkLenZ);

            int baseDiv = Math.Max(2, baseGridResolution >> Math.Min(4, LodLevel));
            int divX = Math.Min(48, Math.Max(2, (int)Math.Round(baseDiv * Math.Sqrt(Math.Max(1.0, aspectXZ)))));
            int divZ = Math.Min(48, Math.Max(2, (int)Math.Round(baseDiv * Math.Sqrt(Math.Max(1.0, 1.0 / Math.Max(0.01, aspectXZ))))));

            var mesh = new ExportMeshData { Name = $"TerrainChunk_LOD{LodLevel}_{Node.ChunkId}" };

            float stepX = chunkLenX / divX;
            float stepZ = chunkLenZ / divZ;

            for (int j = 0; j <= divZ; j++)
            {
                float z = Node.MinZ + j * stepZ;
                float v = (float)j / divZ;

                for (int i = 0; i <= divX; i++)
                {
                    float x = Node.MinX + i * stepX;
                    float u = (float)i / divX;

                    float elev = terrain.EvaluateElevation(x, z);

                    // Compute terrain normal via central difference
                    float delta = Math.Max(1.0f, Math.Min(stepX, stepZ) * 0.5f);
                    float hL = terrain.EvaluateElevation(x - delta, z);
                    float hR = terrain.EvaluateElevation(x + delta, z);
                    float hD = terrain.EvaluateElevation(x, z - delta);
                    float hU = terrain.EvaluateElevation(x, z + delta);

                    float nx = (hL - hR) / (2.0f * delta);
                    float ny = 1.0f;
                    float nz = (hD - hU) / (2.0f * delta);
                    float invLen = 1.0f / (float)Math.Sqrt(nx * nx + ny * ny + nz * nz);

                    mesh.Vertices.Add(new ExportVertex
                    {
                        X = x,
                        Y = elev,
                        Z = z,
                        Nx = nx * invLen,
                        Ny = ny * invLen,
                        Nz = nz * invLen,
                        U = u,
                        V = v
                    });
                }
            }

            int stride = divX + 1;
            for (int j = 0; j < divZ; j++)
            {
                for (int i = 0; i < divX; i++)
                {
                    uint i00 = (uint)(j * stride + i);
                    uint i10 = (uint)(j * stride + (i + 1));
                    uint i01 = (uint)((j + 1) * stride + i);
                    uint i11 = (uint)((j + 1) * stride + (i + 1));

                    mesh.Indices.Add(i00);
                    mesh.Indices.Add(i10);
                    mesh.Indices.Add(i11);

                    mesh.Indices.Add(i00);
                    mesh.Indices.Add(i11);
                    mesh.Indices.Add(i01);
                }
            }

            return mesh;
        }
    }

    /// <summary>
    /// Node within the CDLOD quadtree representing a spatial chunk of terrain.
    /// </summary>
    public class CdlodChunkNode
    {
        public string ChunkId { get; }
        public int LodLevel { get; }
        public float MinX { get; }
        public float MaxX { get; }
        public float MinZ { get; }
        public float MaxZ { get; }
        public float MinY { get; set; }
        public float MaxY { get; set; }
        public BoundingBox3D Bounds { get; private set; }
        public CdlodChunkNode[] Children { get; private set; }

        public bool IsLeaf => Children == null;
        public float CenterX => (MinX + MaxX) * 0.5f;
        public float CenterZ => (MinZ + MaxZ) * 0.5f;

        public CdlodChunkNode(string id, int lodLevel, float minX, float maxX, float minZ, float maxZ, float minY = -25f, float maxY = 300f)
        {
            ChunkId = id;
            LodLevel = lodLevel;
            MinX = minX;
            MaxX = maxX;
            MinZ = minZ;
            MaxZ = maxZ;
            MinY = minY;
            MaxY = maxY;
            Bounds = new BoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
        }

        public void Subdivide(TerrainSurfaceData terrain, int maxLodLevels)
        {
            if (LodLevel <= 0 || Children != null) return;

            float midX = (MinX + MaxX) * 0.5f;
            float midZ = (MinZ + MaxZ) * 0.5f;
            int nextLod = LodLevel - 1;

            Children = new CdlodChunkNode[4];
            // 0: NW, 1: NE, 2: SW, 3: SE
            Children[0] = new CdlodChunkNode($"{ChunkId}_NW", nextLod, MinX, midX, MinZ, midZ, MinY, MaxY);
            Children[1] = new CdlodChunkNode($"{ChunkId}_NE", nextLod, midX, MaxX, MinZ, midZ, MinY, MaxY);
            Children[2] = new CdlodChunkNode($"{ChunkId}_SW", nextLod, MinX, midX, midZ, MaxZ, MinY, MaxY);
            Children[3] = new CdlodChunkNode($"{ChunkId}_SE", nextLod, midX, MaxX, midZ, MaxZ, MinY, MaxY);

            // Refine min/max elevations from terrain
            if (terrain != null)
            {
                for (int c = 0; c < 4; c++)
                {
                    Children[c].UpdateElevationBounds(terrain);
                    if (nextLod > 0)
                    {
                        Children[c].Subdivide(terrain, maxLodLevels);
                    }
                }
            }
        }

        public void UpdateElevationBounds(TerrainSurfaceData terrain)
        {
            if (terrain == null) return;
            float eMid = terrain.EvaluateElevation(CenterX, CenterZ);
            float eMinX = terrain.EvaluateElevation(MinX, CenterZ);
            float eMaxX = terrain.EvaluateElevation(MaxX, CenterZ);
            float eMinZ = terrain.EvaluateElevation(CenterX, MinZ);
            float eMaxZ = terrain.EvaluateElevation(CenterX, MaxZ);

            MinY = Math.Min(MinY, Math.Min(eMid, Math.Min(eMinX, Math.Min(eMaxX, Math.Min(eMinZ, eMaxZ))))) - 10f;
            MaxY = Math.Max(MaxY, Math.Max(eMid, Math.Max(eMinX, Math.Max(eMaxX, Math.Max(eMinZ, eMaxZ))))) + 10f;
            Bounds = new BoundingBox3D(MinX, MinY, MinZ, MaxX, MaxY, MaxZ);
        }

        /// <summary>
        /// Selects chunks matching the CDLOD distance metric and view frustum.
        /// </summary>
        public void SelectLOD(ViewFrustum3D frustum, float camX, float camY, float camZ, float[] lodRanges,
                              List<CdlodRenderChunk> results, ref int nodesTested, ref int nodesCulled)
        {
            nodesTested++;
            var intersection = frustum.IntersectsBox(Bounds);
            if (intersection == FrustumIntersection.Outside)
            {
                nodesCulled++;
                return;
            }

            // Calculate distance from camera to nearest point on chunk AABB
            float dx = Math.Max(0f, Math.Max(MinX - camX, camX - MaxX));
            float dy = Math.Max(0f, Math.Max(MinY - camY, camY - MaxY));
            float dz = Math.Max(0f, Math.Max(MinZ - camZ, camZ - MaxZ));
            float distance = (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);

            float range = LodLevel < lodRanges.Length ? lodRanges[LodLevel] : float.MaxValue;

            // If close enough and children exist, subdivide into higher detail
            if (distance < range && Children != null)
            {
                for (int c = 0; c < 4; c++)
                {
                    Children[c].SelectLOD(frustum, camX, camY, camZ, lodRanges, results, ref nodesTested, ref nodesCulled);
                }
            }
            else
            {
                // Chunk is at its optimal LOD level
                float prevRange = LodLevel > 0 && (LodLevel - 1) < lodRanges.Length ? lodRanges[LodLevel - 1] : 0f;
                float morphSpan = Math.Max(1.0f, range - prevRange);
                float morph = Math.Max(0f, Math.Min(1f, (distance - prevRange) / morphSpan));

                results.Add(new CdlodRenderChunk(this, LodLevel, morph));
            }
        }

        public int CountNodes()
        {
            int count = 1;
            if (Children != null)
            {
                for (int c = 0; c < 4; c++)
                {
                    count += Children[c].CountNodes();
                }
            }
            return count;
        }
    }

    /// <summary>
    /// Continuous Distance-Dependent Level-of-Detail (CDLOD) Multi-Resolution Terrain Quadtree Engine.
    /// Manages hierarchical chunk selection, view frustum culling, and morphing for 100km+ worlds.
    /// </summary>
    public class CdlodTerrainQuadtree
    {
        public CdlodChunkNode Root { get; private set; }
        public TerrainSurfaceData Terrain { get; private set; }
        public int MaxLodLevels { get; private set; } = 4;
        public float[] LodRanges { get; private set; }

        public int TotalNodes => Root?.CountNodes() ?? 0;

        public CdlodTerrainQuadtree(TerrainSurfaceData terrain, int maxLodLevels = 4)
        {
            Rebuild(terrain, maxLodLevels);
        }

        public void Rebuild(TerrainSurfaceData terrain, int maxLodLevels = 4)
        {
            Terrain = terrain ?? TerrainSurfaceData.CreateDefault();
            MaxLodLevels = Math.Max(1, Math.Min(7, maxLodLevels));

            float halfX = Terrain.SizeX * 0.5f;
            float halfZ = Terrain.SizeY * 0.5f;

            // Compute distance ranges dynamically based on terrain extent
            float diag = (float)Math.Sqrt(Terrain.SizeX * Terrain.SizeX + Terrain.SizeY * Terrain.SizeY);
            LodRanges = new float[MaxLodLevels];
            float baseRange = Math.Max(250f, diag * 0.08f);

            for (int l = 0; l < MaxLodLevels; l++)
            {
                // Geometric progression of LOD distance thresholds
                LodRanges[l] = baseRange * (float)Math.Pow(2.2, l);
            }

            Root = new CdlodChunkNode("TerrainRoot", MaxLodLevels - 1, -halfX, halfX, -halfZ, halfZ,
                                      Terrain.ElevationMin - 20f, Terrain.ElevationMax + 50f);
            Root.UpdateElevationBounds(Terrain);
            Root.Subdivide(Terrain, MaxLodLevels);
        }

        /// <summary>
        /// Selects all visible CDLOD chunks matching the camera's view frustum and distance thresholds.
        /// </summary>
        public List<CdlodRenderChunk> SelectVisibleChunks(ViewFrustum3D frustum, float camX, float camY, float camZ,
                                                          out int nodesTested, out int nodesCulled)
        {
            nodesTested = 0;
            nodesCulled = 0;
            var results = new List<CdlodRenderChunk>();

            if (Root != null && frustum != null)
            {
                Root.SelectLOD(frustum, camX, camY, camZ, LodRanges, results, ref nodesTested, ref nodesCulled);
            }

            return results;
        }
    }
}
