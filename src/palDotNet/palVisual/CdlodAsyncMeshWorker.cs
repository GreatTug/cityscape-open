using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using palDotNet.palModernEngine;

namespace palDotNet.palVisual
{
    /// <summary>
    /// Represents an asynchronous terrain chunk mesh generation request.
    /// </summary>
    public class CdlodMeshRequest
    {
        public CdlodChunkNode Node { get; set; }
        public int LodLevel { get; set; }
        public int GridResolution { get; set; }
        public TerrainSurfaceData Terrain { get; set; }
        public float MorphFactor { get; set; } = 0f;
        public bool EnableSkirts { get; set; } = true;
        public Stopwatch EnqueueTimer { get; set; }

        public string Key => $"{Node?.ChunkId}_LOD{LodLevel}_{GridResolution}_{(EnableSkirts ? "Skirt" : "NoSkirt")}";
    }

    /// <summary>
    /// Completed terrain chunk mesh result computed on a background worker thread.
    /// </summary>
    public class CdlodCompletedMesh
    {
        public string ChunkKey { get; set; }
        public int LodLevel { get; set; }
        public ExportMeshData Mesh { get; set; }
        public double ProcessingTimeMs { get; set; }
    }

    /// <summary>
    /// High-throughput Multithreaded CDLOD Chunk Mesh Worker Pool.
    /// Eliminates main-thread CPU spikes and frame hitches during rapid camera flythroughs over 100km+ terrain.
    /// </summary>
    public class CdlodAsyncMeshWorker : IDisposable
    {
        private readonly ConcurrentQueue<CdlodMeshRequest> mPendingQueue = new ConcurrentQueue<CdlodMeshRequest>();
        private readonly ConcurrentDictionary<string, CdlodCompletedMesh> mCompletedCache = new ConcurrentDictionary<string, CdlodCompletedMesh>();
        private readonly ConcurrentDictionary<string, byte> mInFlight = new ConcurrentDictionary<string, byte>();

        private readonly int mMaxDegreeOfParallelism;
        private readonly AutoResetEvent mWorkAvailableEvent = new AutoResetEvent(false);
        private readonly List<Thread> mWorkerThreads = new List<Thread>();
        private volatile bool mIsRunning = true;

        public int PendingQueueCount => mPendingQueue.Count;
        public int CompletedCacheCount => mCompletedCache.Count;
        public long TotalMeshesGenerated { get; private set; }
        public double AverageGenerationTimeMs { get; private set; }

        public CdlodAsyncMeshWorker(int workerThreads = 0)
        {
            mMaxDegreeOfParallelism = workerThreads > 0 ? workerThreads : Math.Max(2, Math.Min(8, Environment.ProcessorCount));
            for (int i = 0; i < mMaxDegreeOfParallelism; i++)
            {
                var t = new Thread(WorkerLoop)
                {
                    Name = $"CityScape_CDLOD_Worker_{i}",
                    IsBackground = true,
                    Priority = ThreadPriority.BelowNormal
                };
                mWorkerThreads.Add(t);
                t.Start();
            }
        }

        /// <summary>
        /// Enqueues a chunk for asynchronous background mesh generation with optional LOD morphing and skirts.
        /// </summary>
        public bool EnqueueRequest(CdlodChunkNode node, int lodLevel, TerrainSurfaceData terrain, int gridResolution = 8, float morphFactor = 0f, bool enableSkirts = true)
        {
            if (node == null || terrain == null || !mIsRunning) return false;

            string key = $"{node.ChunkId}_LOD{lodLevel}_{gridResolution}_{(enableSkirts ? "Skirt" : "NoSkirt")}";
            if (mCompletedCache.ContainsKey(key) || mInFlight.ContainsKey(key))
            {
                // Already cached or currently processing
                return false;
            }

            var req = new CdlodMeshRequest
            {
                Node = node,
                LodLevel = lodLevel,
                GridResolution = gridResolution,
                Terrain = terrain,
                MorphFactor = morphFactor,
                EnableSkirts = enableSkirts,
                EnqueueTimer = Stopwatch.StartNew()
            };

            mInFlight.TryAdd(key, 1);
            mPendingQueue.Enqueue(req);
            mWorkAvailableEvent.Set();
            return true;
        }

        /// <summary>
        /// Tries to retrieve a previously generated mesh from the background worker cache.
        /// </summary>
        public bool TryGetMesh(CdlodChunkNode node, int lodLevel, int gridResolution, out ExportMeshData mesh, bool enableSkirts = true)
        {
            mesh = null;
            if (node == null) return false;

            string key = $"{node.ChunkId}_LOD{lodLevel}_{gridResolution}_{(enableSkirts ? "Skirt" : "NoSkirt")}";
            if (mCompletedCache.TryGetValue(key, out var completed))
            {
                mesh = completed.Mesh;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Synchronously generates or retrieves a mesh if not yet computed in background.
        /// </summary>
        public ExportMeshData GetOrGenerateImmediate(CdlodChunkNode node, int lodLevel, TerrainSurfaceData terrain, int gridResolution = 8, float morphFactor = 0f, bool enableSkirts = true)
        {
            if (TryGetMesh(node, lodLevel, gridResolution, out var cached, enableSkirts))
            {
                return cached;
            }

            var chunk = new CdlodRenderChunk(node, lodLevel, morphFactor);
            var mesh = chunk.GenerateMesh(terrain, gridResolution, enableSkirts, enableMorphing: true);
            if (mesh != null)
            {
                string key = $"{node.ChunkId}_LOD{lodLevel}_{gridResolution}_{(enableSkirts ? "Skirt" : "NoSkirt")}";
                mCompletedCache.TryAdd(key, new CdlodCompletedMesh
                {
                    ChunkKey = key,
                    LodLevel = lodLevel,
                    Mesh = mesh,
                    ProcessingTimeMs = 0
                });
            }
            return mesh;
        }

        /// <summary>
        /// Flushes completed cache (e.g. when terrain surface data parameters change).
        /// </summary>
        public void ClearCache()
        {
            mCompletedCache.Clear();
            mInFlight.Clear();
            while (mPendingQueue.TryDequeue(out _)) { }
        }

        /// <summary>
        /// Blocks until all queued background mesh jobs are finished (useful for testing and offline exporters).
        /// </summary>
        public bool WaitForCompletion(int timeoutMs = 5000)
        {
            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < timeoutMs)
            {
                if (mPendingQueue.IsEmpty && mInFlight.IsEmpty)
                {
                    return true;
                }
                Thread.Sleep(10);
            }
            return mPendingQueue.IsEmpty && mInFlight.IsEmpty;
        }

        private void WorkerLoop()
        {
            while (mIsRunning)
            {
                if (mPendingQueue.TryDequeue(out var req))
                {
                    try
                    {
                        var sw = Stopwatch.StartNew();
                        var chunk = new CdlodRenderChunk(req.Node, req.LodLevel, req.MorphFactor);
                        var mesh = chunk.GenerateMesh(req.Terrain, req.GridResolution, req.EnableSkirts, enableMorphing: true);
                        sw.Stop();

                        if (mesh != null)
                        {
                            var completed = new CdlodCompletedMesh
                            {
                                ChunkKey = req.Key,
                                LodLevel = req.LodLevel,
                                Mesh = mesh,
                                ProcessingTimeMs = sw.Elapsed.TotalMilliseconds
                            };
                            mCompletedCache.TryAdd(req.Key, completed);

                            lock (mWorkerThreads)
                            {
                                TotalMeshesGenerated++;
                                AverageGenerationTimeMs = (AverageGenerationTimeMs * 0.9) + (completed.ProcessingTimeMs * 0.1);
                            }
                        }
                    }
                    finally
                    {
                        mInFlight.TryRemove(req.Key, out _);
                    }
                }
                else
                {
                    mWorkAvailableEvent.WaitOne(100);
                }
            }
        }

        public void Dispose()
        {
            mIsRunning = false;
            mWorkAvailableEvent.Set();
            foreach (var t in mWorkerThreads)
            {
                try
                {
                    if (t.IsAlive) t.Join(200);
                }
                catch { }
            }
            mWorkAvailableEvent.Dispose();
        }
    }
}
