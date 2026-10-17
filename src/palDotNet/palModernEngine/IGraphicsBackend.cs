using System;
using System.Drawing;

namespace palDotNet.palModernEngine
{
    /// <summary>
    /// Metadata describing the detected graphics adapter.
    /// </summary>
    public class AdapterInfo
    {
        public string AdapterName { get; set; } = "Generic Display Adapter";
        public string VendorName { get; set; } = "Unknown Vendor";
        public uint DeviceId { get; set; }
        public uint VendorId { get; set; }
        public ulong DedicatedVideoMemoryBytes { get; set; }
        public GraphicsBackendType BackendType { get; set; }
        public GraphicsFeatures SupportedFeatures { get; set; }
        public TargetArchitecture RuntimeArchitecture { get; set; }
        public bool IsHardwareAccelerated { get; set; } = true;

        public string FormattedVram
        {
            get
            {
                if (DedicatedVideoMemoryBytes >= 1024 * 1024 * 1024)
                {
                    return string.Format("{0:F1} GB", (double)DedicatedVideoMemoryBytes / (1024 * 1024 * 1024));
                }
                return string.Format("{0:N0} MB", DedicatedVideoMemoryBytes / (1024 * 1024));
            }
        }

        public override string ToString()
        {
            return string.Format("{0} [{1}] - VRAM: {2} ({3})", AdapterName, BackendType, FormattedVram, RuntimeArchitecture);
        }
    }

    /// <summary>
    /// Frame render statistics and telemetry.
    /// </summary>
    public struct RenderStats
    {
        public ulong FrameIndex;
        public float FrameTimeMs;
        public float FramesPerSecond;
        public uint DrawCalls;
        public uint TrianglesRendered;
        public uint ShaderBinds;
        public uint PipelineStateChanges;
        public ulong AllocatedGpuMemoryBytes;
    }

    /// <summary>
    /// Core interface for 3D graphics hardware backends in CityScape.
    /// </summary>
    public interface IGraphicsBackend : IDisposable
    {
        GraphicsBackendType BackendType { get; }
        AdapterInfo Adapter { get; }
        bool IsInitialized { get; }
        GraphicsFeatures Features { get; }

        bool Initialize(IntPtr windowHandle, int width, int height);
        void Resize(int width, int height);
        void BeginFrame(Color clearColor);
        void EndFrame();
        void Present();
        RenderStats GetStats();
        string GetDiagnosticReport();
    }
}
