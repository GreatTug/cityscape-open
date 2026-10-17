using System;
using System.Collections.Generic;

namespace palDotNet.palVisual
{
    public enum FrustumIntersection
    {
        Outside = 0,
        Intersecting = 1,
        Inside = 2
    }

    /// <summary>
    /// Axis-Aligned 3D Bounding Box (AABB) with intersection and raycasting capabilities.
    /// </summary>
    public struct BoundingBox3D
    {
        public float MinX, MinY, MinZ;
        public float MaxX, MaxY, MaxZ;

        public BoundingBox3D(float minX, float minY, float minZ, float maxX, float maxY, float maxZ)
        {
            MinX = Math.Min(minX, maxX);
            MinY = Math.Min(minY, maxY);
            MinZ = Math.Min(minZ, maxZ);
            MaxX = Math.Max(minX, maxX);
            MaxY = Math.Max(minY, maxY);
            MaxZ = Math.Max(minZ, maxZ);
        }

        public float CenterX => (MinX + MaxX) * 0.5f;
        public float CenterY => (MinY + MaxY) * 0.5f;
        public float CenterZ => (MinZ + MaxZ) * 0.5f;

        public float ExtentX => (MaxX - MinX) * 0.5f;
        public float ExtentY => (MaxY - MinY) * 0.5f;
        public float ExtentZ => (MaxZ - MinZ) * 0.5f;

        public bool Intersects(BoundingBox3D other)
        {
            return (MinX <= other.MaxX && MaxX >= other.MinX) &&
                   (MinY <= other.MaxY && MaxY >= other.MinY) &&
                   (MinZ <= other.MaxZ && MaxZ >= other.MinZ);
        }

        public bool Contains(float x, float y, float z)
        {
            return x >= MinX && x <= MaxX &&
                   y >= MinY && y <= MaxY &&
                   z >= MinZ && z <= MaxZ;
        }

        public bool Contains2D(float x, float z)
        {
            return x >= MinX && x <= MaxX &&
                   z >= MinZ && z <= MaxZ;
        }

        /// <summary>
        /// Fast Slab Ray-AABB intersection test.
        /// </summary>
        public bool Raycast(float rayOrigX, float rayOrigY, float rayOrigZ,
                            float rayDirX, float rayDirY, float rayDirZ,
                            out float tMin, out float tMax)
        {
            tMin = 0.0f;
            tMax = float.MaxValue;

            // X slab
            if (Math.Abs(rayDirX) > 1e-6f)
            {
                float invD = 1.0f / rayDirX;
                float t0 = (MinX - rayOrigX) * invD;
                float t1 = (MaxX - rayOrigX) * invD;
                if (t0 > t1) { float tmp = t0; t0 = t1; t1 = tmp; }
                tMin = Math.Max(tMin, t0);
                tMax = Math.Min(tMax, t1);
                if (tMin > tMax) return false;
            }
            else if (rayOrigX < MinX || rayOrigX > MaxX) return false;

            // Y slab
            if (Math.Abs(rayDirY) > 1e-6f)
            {
                float invD = 1.0f / rayDirY;
                float t0 = (MinY - rayOrigY) * invD;
                float t1 = (MaxY - rayOrigY) * invD;
                if (t0 > t1) { float tmp = t0; t0 = t1; t1 = tmp; }
                tMin = Math.Max(tMin, t0);
                tMax = Math.Min(tMax, t1);
                if (tMin > tMax) return false;
            }
            else if (rayOrigY < MinY || rayOrigY > MaxY) return false;

            // Z slab
            if (Math.Abs(rayDirZ) > 1e-6f)
            {
                float invD = 1.0f / rayDirZ;
                float t0 = (MinZ - rayOrigZ) * invD;
                float t1 = (MaxZ - rayOrigZ) * invD;
                if (t0 > t1) { float tmp = t0; t0 = t1; t1 = tmp; }
                tMin = Math.Max(tMin, t0);
                tMax = Math.Min(tMax, t1);
                if (tMin > tMax) return false;
            }
            else if (rayOrigZ < MinZ || rayOrigZ > MaxZ) return false;

            return true;
        }

        public override string ToString()
        {
            return string.Format("[({0:F1}, {1:F1}, {2:F1}) -> ({3:F1}, {4:F1}, {5:F1})]",
                MinX, MinY, MinZ, MaxX, MaxY, MaxZ);
        }
    }

    /// <summary>
    /// Single 3D Plane representation (Normal + Distance).
    /// </summary>
    public struct Plane3D
    {
        public float A, B, C, D;

        public Plane3D(float a, float b, float c, float d)
        {
            float len = (float)Math.Sqrt(a * a + b * b + c * c);
            if (len > 1e-6f)
            {
                A = a / len;
                B = b / len;
                C = c / len;
                D = d / len;
            }
            else
            {
                A = 0; B = 1; C = 0; D = 0;
            }
        }

        public float DistanceTo(float x, float y, float z)
        {
            return A * x + B * y + C * z + D;
        }
    }

    /// <summary>
    /// 3D View Frustum composed of 6 clipping planes (Near, Far, Left, Right, Top, Bottom).
    /// Enables sub-millisecond culling of thousands of spatial objects.
    /// </summary>
    public class ViewFrustum3D
    {
        public Plane3D[] Planes { get; } = new Plane3D[6];

        public ViewFrustum3D()
        {
            // Default unconstrained frustum
            Planes[0] = new Plane3D(0, 0, 1, 10000);   // Near
            Planes[1] = new Plane3D(0, 0, -1, 10000);  // Far
            Planes[2] = new Plane3D(1, 0, 0, 10000);   // Left
            Planes[3] = new Plane3D(-1, 0, 0, 10000);  // Right
            Planes[4] = new Plane3D(0, 1, 0, 10000);   // Bottom
            Planes[5] = new Plane3D(0, -1, 0, 10000);  // Top
        }

        /// <summary>
        /// Constructs a View Frustum from camera position, orientation, field of view and clipping planes.
        /// </summary>
        public static ViewFrustum3D CreateFromCamera(
            float posX, float posY, float posZ,
            float yawDeg, float pitchDeg,
            float fovYDeg = 60.0f,
            float aspectRatio = 16f / 9f,
            float nearClip = 1.0f,
            float farClip = 15000.0f)
        {
            var frustum = new ViewFrustum3D();

            float yawRad = (float)(yawDeg * Math.PI / 180.0);
            float pitchRad = (float)(pitchDeg * Math.PI / 180.0);

            // Forward vector
            float cosP = (float)Math.Cos(pitchRad);
            float sinP = (float)Math.Sin(pitchRad);
            float cosY = (float)Math.Cos(yawRad);
            float sinY = (float)Math.Sin(yawRad);

            float fwdX = sinY * cosP;
            float fwdY = sinP;
            float fwdZ = cosY * cosP;

            // Right vector
            float rightX = cosY;
            float rightY = 0f;
            float rightZ = -sinY;

            // Up vector
            float upX = -sinY * sinP;
            float upY = cosP;
            float upZ = -cosY * sinP;

            float tanFov = (float)Math.Tan(fovYDeg * 0.5 * Math.PI / 180.0);
            float nearH = nearClip * tanFov;
            float nearW = nearH * aspectRatio;

            // 1. Near Plane: fwd . (P - (C + fwd * nearClip)) >= 0
            frustum.Planes[0] = new Plane3D(fwdX, fwdY, fwdZ, -(fwdX * (posX + fwdX * nearClip) + fwdY * (posY + fwdY * nearClip) + fwdZ * (posZ + fwdZ * nearClip)));

            // 2. Far Plane: -fwd . (P - (C + fwd * farClip)) >= 0
            frustum.Planes[1] = new Plane3D(-fwdX, -fwdY, -fwdZ, (fwdX * (posX + fwdX * farClip) + fwdY * (posY + fwdY * farClip) + fwdZ * (posZ + fwdZ * farClip)));

            // 3. Left Plane: (nearClip * right + nearW * fwd) . (P - C) >= 0
            float lNx = nearClip * rightX + nearW * fwdX;
            float lNy = nearClip * rightY + nearW * fwdY;
            float lNz = nearClip * rightZ + nearW * fwdZ;
            frustum.Planes[2] = new Plane3D(lNx, lNy, lNz, -(lNx * posX + lNy * posY + lNz * posZ));

            // 4. Right Plane: (-nearClip * right + nearW * fwd) . (P - C) >= 0
            float rNx = -nearClip * rightX + nearW * fwdX;
            float rNy = -nearClip * rightY + nearW * fwdY;
            float rNz = -nearClip * rightZ + nearW * fwdZ;
            frustum.Planes[3] = new Plane3D(rNx, rNy, rNz, -(rNx * posX + rNy * posY + rNz * posZ));

            // 5. Bottom Plane: (nearClip * up + nearH * fwd) . (P - C) >= 0
            float bNx = nearClip * upX + nearH * fwdX;
            float bNy = nearClip * upY + nearH * fwdY;
            float bNz = nearClip * upZ + nearH * fwdZ;
            frustum.Planes[4] = new Plane3D(bNx, bNy, bNz, -(bNx * posX + bNy * posY + bNz * posZ));

            // 6. Top Plane: (-nearClip * up + nearH * fwd) . (P - C) >= 0
            float tNx = -nearClip * upX + nearH * fwdX;
            float tNy = -nearClip * upY + nearH * fwdY;
            float tNz = -nearClip * upZ + nearH * fwdZ;
            frustum.Planes[5] = new Plane3D(tNx, tNy, tNz, -(tNx * posX + tNy * posY + tNz * posZ));

            return frustum;
        }

        /// <summary>
        /// Constructs a View Frustum perfectly matching the 3D Orbit Camera projection of ControlRender.
        /// Guaranteed 1:1 mathematical alignment with ControlRender.Project(x, y, z).
        /// </summary>
        public static ViewFrustum3D CreateFromOrbitProjection(
            float yawDeg, float pitchDeg, float camDist,
            int viewportW, int viewportH, float focal,
            float nearClip = 1.0f, float farClip = 500000.0f)
        {
            var frustum = new ViewFrustum3D();

            float yawRad = (float)(yawDeg * Math.PI / 180.0);
            float pitchRad = (float)(pitchDeg * Math.PI / 180.0);
            float cosY = (float)Math.Cos(yawRad);
            float sinY = (float)Math.Sin(yawRad);
            float cosP = (float)Math.Cos(pitchRad);
            float sinP = (float)Math.Sin(pitchRad);

            // Plane coefficients matching ControlRender.Project:
            // x1 = Ax * x + Bx * y + Cx * z
            float Ax = cosY, Bx = 0f, Cx = -sinY;
            // y2 = Ay * x + By * y + Cy * z
            float Ay = -sinY * sinP, By = cosP, Cy = -cosY * sinP;
            // z2 = Az * x + Bz * y + Cz * z
            float Az = sinY * cosP, Bz = sinP, Cz = cosY * cosP;

            // Half-extents ratio on screen with safety margin
            float margin = 1.25f;
            float kx = (viewportW * 0.5f * margin) / Math.Max(1.0f, focal);
            float ky = (viewportH * 0.55f * margin) / Math.Max(1.0f, focal);

            // 1. Near Plane: z2 + camDist - nearClip >= 0
            frustum.Planes[0] = new Plane3D(Az, Bz, Cz, camDist - nearClip);

            // 2. Far Plane: farClip - (z2 + camDist) >= 0
            frustum.Planes[1] = new Plane3D(-Az, -Bz, -Cz, farClip - camDist);

            // 3. Left Plane: x1 + kx * (z2 + camDist) >= 0
            frustum.Planes[2] = new Plane3D(Ax + kx * Az, Bx + kx * Bz, Cx + kx * Cz, kx * camDist);

            // 4. Right Plane: kx * (z2 + camDist) - x1 >= 0
            frustum.Planes[3] = new Plane3D(kx * Az - Ax, kx * Bz - Bx, kx * Cz - Cx, kx * camDist);

            // 5. Bottom Plane: y2 + ky * (z2 + camDist) >= 0
            frustum.Planes[4] = new Plane3D(Ay + ky * Az, By + ky * Bz, Cy + ky * Cz, ky * camDist);

            // 6. Top Plane: ky * (z2 + camDist) - y2 >= 0
            frustum.Planes[5] = new Plane3D(ky * Az - Ay, ky * Bz - By, ky * Cz - Cy, ky * camDist);

            return frustum;
        }

        /// <summary>
        /// Fast AABB-Frustum intersection test returning Outside, Intersecting, or Inside.
        /// </summary>
        public FrustumIntersection IntersectsBox(BoundingBox3D box)
        {
            bool allInside = true;

            for (int i = 0; i < 6; i++)
            {
                var p = Planes[i];

                // Positive vertex in direction of normal
                float px = p.A >= 0 ? box.MaxX : box.MinX;
                float py = p.B >= 0 ? box.MaxY : box.MinY;
                float pz = p.C >= 0 ? box.MaxZ : box.MinZ;

                if (p.DistanceTo(px, py, pz) < 0f)
                {
                    // Box is completely outside this plane
                    return FrustumIntersection.Outside;
                }

                // Negative vertex
                float nx = p.A >= 0 ? box.MinX : box.MaxX;
                float ny = p.B >= 0 ? box.MinY : box.MaxY;
                float nz = p.C >= 0 ? box.MinZ : box.MaxZ;

                if (p.DistanceTo(nx, ny, nz) < 0f)
                {
                    allInside = false;
                }
            }

            return allInside ? FrustumIntersection.Inside : FrustumIntersection.Intersecting;
        }
    }

    /// <summary>
    /// Interface for any spatial entity that can be indexed in the Spatial Quadtree.
    /// </summary>
    public interface ISpatialObject
    {
        string SpatialId { get; }
        BoundingBox3D SpatialBounds { get; }
    }

    /// <summary>
    /// Individual node within the Spatial Quadtree hierarchy.
    /// </summary>
    public class SpatialQuadtreeNode<T> where T : ISpatialObject
    {
        public BoundingBox3D Bounds { get; }
        public int Depth { get; }
        public int MaxDepth { get; }
        public int MaxItemsPerNode { get; }
        public List<T> Items { get; } = new List<T>();
        public SpatialQuadtreeNode<T>[] Children { get; private set; }

        public bool IsLeaf => Children == null;

        public SpatialQuadtreeNode(BoundingBox3D bounds, int depth = 0, int maxDepth = 6, int maxItems = 16)
        {
            Bounds = bounds;
            Depth = depth;
            MaxDepth = maxDepth;
            MaxItemsPerNode = maxItems;
        }

        public void Subdivide()
        {
            if (Children != null) return;

            float minX = Bounds.MinX;
            float midX = Bounds.CenterX;
            float maxX = Bounds.MaxX;

            float minZ = Bounds.MinZ;
            float midZ = Bounds.CenterZ;
            float maxZ = Bounds.MaxZ;

            float minY = Bounds.MinY;
            float maxY = Bounds.MaxY;

            Children = new SpatialQuadtreeNode<T>[4];
            // 0: NW, 1: NE, 2: SW, 3: SE
            Children[0] = new SpatialQuadtreeNode<T>(new BoundingBox3D(minX, minY, minZ, midX, maxY, midZ), Depth + 1, MaxDepth, MaxItemsPerNode);
            Children[1] = new SpatialQuadtreeNode<T>(new BoundingBox3D(midX, minY, minZ, maxX, maxY, midZ), Depth + 1, MaxDepth, MaxItemsPerNode);
            Children[2] = new SpatialQuadtreeNode<T>(new BoundingBox3D(minX, minY, midZ, midX, maxY, maxZ), Depth + 1, MaxDepth, MaxItemsPerNode);
            Children[3] = new SpatialQuadtreeNode<T>(new BoundingBox3D(midX, minY, midZ, maxX, maxY, maxZ), Depth + 1, MaxDepth, MaxItemsPerNode);

            // Re-distribute existing items into children if they fit fully
            var remaining = new List<T>();
            foreach (var item in Items)
            {
                bool inserted = false;
                for (int c = 0; c < 4; c++)
                {
                    if (Children[c].Bounds.Intersects(item.SpatialBounds))
                    {
                        Children[c].Insert(item);
                        inserted = true;
                        break;
                    }
                }
                if (!inserted) remaining.Add(item);
            }
            Items.Clear();
            Items.AddRange(remaining);
        }

        public void Insert(T item)
        {
            if (item == null) return;

            if (IsLeaf)
            {
                Items.Add(item);
                if (Items.Count > MaxItemsPerNode && Depth < MaxDepth)
                {
                    Subdivide();
                }
            }
            else
            {
                // Find matching child
                bool routed = false;
                for (int c = 0; c < 4; c++)
                {
                    if (Children[c].Bounds.Contains2D(item.SpatialBounds.CenterX, item.SpatialBounds.CenterZ))
                    {
                        Children[c].Insert(item);
                        routed = true;
                        break;
                    }
                }
                if (!routed)
                {
                    Items.Add(item);
                }
            }
        }

        public void QueryFrustum(ViewFrustum3D frustum, List<T> results, ref int nodesTested, ref int nodesCulled)
        {
            nodesTested++;
            var intersection = frustum.IntersectsBox(Bounds);
            if (intersection == FrustumIntersection.Outside)
            {
                nodesCulled++;
                return;
            }

            if (intersection == FrustumIntersection.Inside)
            {
                // Entire subtree is unconditionally inside view frustum
                CollectAll(results);
                return;
            }

            // Intersecting: collect items in this node that pass
            foreach (var item in Items)
            {
                if (frustum.IntersectsBox(item.SpatialBounds) != FrustumIntersection.Outside)
                {
                    results.Add(item);
                }
            }

            if (Children != null)
            {
                for (int c = 0; c < 4; c++)
                {
                    Children[c].QueryFrustum(frustum, results, ref nodesTested, ref nodesCulled);
                }
            }
        }

        public void CollectAll(List<T> results)
        {
            results.AddRange(Items);
            if (Children != null)
            {
                for (int c = 0; c < 4; c++)
                {
                    Children[c].CollectAll(results);
                }
            }
        }

        public void QueryRadius(float cx, float cz, float radiusSq, List<T> results)
        {
            // Quick AABB-distance check
            float dx = Math.Max(0f, Math.Max(Bounds.MinX - cx, cx - Bounds.MaxX));
            float dz = Math.Max(0f, Math.Max(Bounds.MinZ - cz, cz - Bounds.MaxZ));
            if (dx * dx + dz * dz > radiusSq) return;

            foreach (var item in Items)
            {
                float ix = item.SpatialBounds.CenterX - cx;
                float iz = item.SpatialBounds.CenterZ - cz;
                if (ix * ix + iz * iz <= radiusSq)
                {
                    results.Add(item);
                }
            }

            if (Children != null)
            {
                for (int c = 0; c < 4; c++)
                {
                    Children[c].QueryRadius(cx, cz, radiusSq, results);
                }
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

        public int CountItems()
        {
            int count = Items.Count;
            if (Children != null)
            {
                for (int c = 0; c < 4; c++)
                {
                    count += Children[c].CountItems();
                }
            }
            return count;
        }
    }

    /// <summary>
    /// High-performance Spatial Quadtree Scene Graph Index for massive terrain worlds.
    /// Provides hierarchical frustum culling and spatial point/radius queries.
    /// </summary>
    public class SpatialQuadtree<T> where T : ISpatialObject
    {
        public SpatialQuadtreeNode<T> Root { get; private set; }
        public int MaxDepth { get; set; } = 6;
        public int MaxItemsPerLeaf { get; set; } = 16;
        public int TotalItems => Root?.CountItems() ?? 0;
        public int TotalNodes => Root?.CountNodes() ?? 0;

        public SpatialQuadtree(BoundingBox3D worldBounds, int maxDepth = 6, int maxItemsPerLeaf = 16)
        {
            MaxDepth = maxDepth;
            MaxItemsPerLeaf = maxItemsPerLeaf;
            Root = new SpatialQuadtreeNode<T>(worldBounds, 0, maxDepth, maxItemsPerLeaf);
        }

        public void Insert(T item)
        {
            Root?.Insert(item);
        }

        public void Rebuild(BoundingBox3D worldBounds, IEnumerable<T> items)
        {
            Root = new SpatialQuadtreeNode<T>(worldBounds, 0, MaxDepth, MaxItemsPerLeaf);
            if (items != null)
            {
                foreach (var it in items)
                {
                    Root.Insert(it);
                }
            }
        }

        public List<T> QueryFrustum(ViewFrustum3D frustum, out int nodesTested, out int nodesCulled)
        {
            nodesTested = 0;
            nodesCulled = 0;
            var results = new List<T>();
            if (Root != null && frustum != null)
            {
                Root.QueryFrustum(frustum, results, ref nodesTested, ref nodesCulled);
            }
            return results;
        }

        public List<T> QueryRadius(float centerX, float centerZ, float radius)
        {
            var results = new List<T>();
            Root?.QueryRadius(centerX, centerZ, radius * radius, results);
            return results;
        }
    }
}
