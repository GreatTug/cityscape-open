using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace palDotNet.palModernEngine
{
    /// <summary>
    /// Central manager and factory for 3D graphics hardware backends in CityScape.
    /// Manages adapter enumeration, runtime API switching (DX12, Vulkan, DX9),
    /// multi-architecture targeting (x64, ARM64, x86), and persistent configuration.
    /// </summary>
    public static class GraphicsBackendManager
    {
        private static IGraphicsBackend _activeBackend;
        private static GraphicsBackendType _activeBackendType = GraphicsBackendType.DirectX12_Modern;
        private static IntPtr _currentHwnd = IntPtr.Zero;
        private static int _viewportWidth = 1280;
        private static int _viewportHeight = 800;
        private static readonly string _configFile = "graphics_backend.cfg";

        public static event Action<GraphicsBackendType, IGraphicsBackend> BackendChanged;

        public static IGraphicsBackend ActiveBackend
        {
            get
            {
                if (_activeBackend == null)
                {
                    InitializeDefault();
                }
                return _activeBackend;
            }
        }

        public static GraphicsBackendType ActiveBackendType => _activeBackendType;

        public static TargetArchitecture CurrentArchitecture
        {
            get
            {
                if (RuntimeInformationHelper.IsArm64())
                {
                    return TargetArchitecture.ARM64_Modern;
                }
                if (Environment.Is64BitProcess)
                {
                    return TargetArchitecture.x64_Modern;
                }
                return TargetArchitecture.x86_Legacy;
            }
        }

        static GraphicsBackendManager()
        {
            LoadSavedBackendPreference();
        }

        private static void LoadSavedBackendPreference()
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string path = Path.Combine(baseDir, _configFile);
                if (File.Exists(path))
                {
                    string text = File.ReadAllText(path).Trim();
                    if (Enum.TryParse(text, true, out GraphicsBackendType savedType))
                    {
                        _activeBackendType = savedType;
                    }
                }
            }
            catch
            {
            }
        }

        private static void SaveBackendPreference(GraphicsBackendType type)
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string path = Path.Combine(baseDir, _configFile);
                File.WriteAllText(path, type.ToString());
            }
            catch
            {
            }
        }

        private static void InitializeDefault()
        {
            _activeBackend = CreateBackendInstance(_activeBackendType);
            _activeBackend.Initialize(_currentHwnd, _viewportWidth, _viewportHeight);
        }

        public static IGraphicsBackend CreateBackendInstance(GraphicsBackendType type)
        {
            switch (type)
            {
                case GraphicsBackendType.DirectX12_Modern:
                    return new DirectX12Backend();
                case GraphicsBackendType.Vulkan_Modern:
                    return new VulkanBackend();
                case GraphicsBackendType.DirectX9_Legacy:
                default:
                    return new DirectX9LegacyBackend();
            }
        }

        /// <summary>
        /// Hot-switches the active graphics backend at runtime using the active window handle.
        /// </summary>
        public static bool SwitchBackend(GraphicsBackendType newBackend)
        {
            return SwitchBackend(newBackend, _currentHwnd);
        }

        /// <summary>
        /// Hot-switches the active graphics backend at runtime.
        /// Reallocates GPU device resources and notifies all subscribed viewports.
        /// </summary>
        public static bool SwitchBackend(GraphicsBackendType newBackend, IntPtr windowHandle)
        {
            if (_activeBackend != null && _activeBackendType == newBackend && _activeBackend.IsInitialized)
            {
                return true;
            }

            if (windowHandle != IntPtr.Zero)
            {
                _currentHwnd = windowHandle;
            }
            IGraphicsBackend oldBackend = _activeBackend;

            try
            {
                IGraphicsBackend candidate = CreateBackendInstance(newBackend);
                if (!candidate.Initialize(_currentHwnd, _viewportWidth, _viewportHeight))
                {
                    candidate.Dispose();
                    return false;
                }

                _activeBackend = candidate;
                _activeBackendType = newBackend;
                SaveBackendPreference(newBackend);

                if (oldBackend != null)
                {
                    oldBackend.Dispose();
                }

                BackendChanged?.Invoke(newBackend, _activeBackend);
                return true;
            }
            catch (Exception)
            {
                // Fallback to legacy safe mode if modern backend initialization fails
                if (newBackend != GraphicsBackendType.DirectX9_Legacy)
                {
                    return SwitchBackend(GraphicsBackendType.DirectX9_Legacy, _currentHwnd);
                }
                return false;
            }
        }

        public static void SetViewportSize(int width, int height)
        {
            _viewportWidth = Math.Max(1, width);
            _viewportHeight = Math.Max(1, height);
            if (_activeBackend != null && _activeBackend.IsInitialized)
            {
                _activeBackend.Resize(_viewportWidth, _viewportHeight);
            }
        }

        public static List<AdapterInfo> EnumerateAvailableAdapters()
        {
            List<AdapterInfo> list = new List<AdapterInfo>();
            try
            {
                using (var dx12 = new DirectX12Backend())
                {
                    list.Add(dx12.Adapter);
                }
                using (var vk = new VulkanBackend())
                {
                    list.Add(vk.Adapter);
                }
                using (var dx9 = new DirectX9LegacyBackend())
                {
                    list.Add(dx9.Adapter);
                }
            }
            catch
            {
            }
            return list;
        }
    }

    internal static class RuntimeInformationHelper
    {
        public static bool IsArm64()
        {
            try
            {
                string arch = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
                string arch6432 = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432");
                return (arch != null && arch.IndexOf("ARM64", StringComparison.OrdinalIgnoreCase) >= 0) ||
                       (arch6432 != null && arch6432.IndexOf("ARM64", StringComparison.OrdinalIgnoreCase) >= 0);
            }
            catch
            {
                return false;
            }
        }
    }
}
