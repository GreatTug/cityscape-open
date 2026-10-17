using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace palDotNet.palModernEngine
{
    /// <summary>
    /// Native GPU device detection and telemetry probe via DXGI 1.1+ and Vulkan ICD interop.
    /// Safely inspects physical GPU hardware, dedicated video RAM, and driver versions.
    /// </summary>
    public static class NativeGpuProber
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct DXGI_ADAPTER_DESC1
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string Description;
            public uint VendorId;
            public uint DeviceId;
            public uint SubSysId;
            public uint Revision;
            public UIntPtr DedicatedVideoMemory;
            public UIntPtr DedicatedSystemMemory;
            public UIntPtr SharedSystemMemory;
            public long AdapterLuid;
            public uint Flags;
        }

        [ComImport]
        [Guid("770aae78-f26f-4dba-a829-253c83d1b387")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IDXGIFactory1
        {
            [PreserveSig] int SetPrivateData(ref Guid Name, uint DataSize, IntPtr pData);
            [PreserveSig] int SetPrivateDataInterface(ref Guid Name, [MarshalAs(UnmanagedType.IUnknown)] object pUnknown);
            [PreserveSig] int GetPrivateData(ref Guid Name, ref uint pDataSize, IntPtr pData);
            [PreserveSig] int GetParent(ref Guid riid, out IntPtr ppParent);
            [PreserveSig] int EnumAdapters(uint Adapter, out IntPtr ppAdapter);
            [PreserveSig] int MakeWindowAssociation(IntPtr WindowHandle, uint Flags);
            [PreserveSig] int GetWindowAssociation(out IntPtr pWindowHandle);
            [PreserveSig] int CreateSwapChain(IntPtr pDevice, IntPtr pDesc, out IntPtr ppSwapChain);
            [PreserveSig] int CreateSoftwareAdapter(IntPtr Module, out IntPtr ppAdapter);
            [PreserveSig] int EnumAdapters1(uint Adapter, out IDXGIAdapter1 ppAdapter);
            [PreserveSig] bool IsCurrent();
        }

        [ComImport]
        [Guid("29038f61-3839-4626-91fd-086879011a05")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IDXGIAdapter1
        {
            [PreserveSig] int SetPrivateData(ref Guid Name, uint DataSize, IntPtr pData);
            [PreserveSig] int SetPrivateDataInterface(ref Guid Name, [MarshalAs(UnmanagedType.IUnknown)] object pUnknown);
            [PreserveSig] int GetPrivateData(ref Guid Name, ref uint pDataSize, IntPtr pData);
            [PreserveSig] int GetParent(ref Guid riid, out IntPtr ppParent);
            [PreserveSig] int EnumOutputs(uint Output, out IntPtr ppOutput);
            [PreserveSig] int GetDesc(IntPtr pDesc);
            [PreserveSig] int CheckInterfaceSupport(ref Guid InterfaceName, out long pUMDVersion);
            [PreserveSig] int GetDesc1(out DXGI_ADAPTER_DESC1 pDesc);
        }

        [DllImport("dxgi.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int CreateDXGIFactory1(ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out IDXGIFactory1 ppFactory);

        [DllImport("d3d12.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int D3D12CreateDevice(IntPtr pAdapter, uint MinimumFeatureLevel, ref Guid riid, IntPtr ppDevice);

        private static List<DXGI_ADAPTER_DESC1> _cachedDxgiAdapters;
        private static bool _probedDxgi;
        private static bool _hasVulkanDriver;
        private static bool _probedVulkan;

        public static List<DXGI_ADAPTER_DESC1> GetDxgiAdapters()
        {
            if (_probedDxgi) return _cachedDxgiAdapters;
            _probedDxgi = true;
            _cachedDxgiAdapters = new List<DXGI_ADAPTER_DESC1>();

            try
            {
                Guid factoryGuid = typeof(IDXGIFactory1).GUID;
                IDXGIFactory1 factory;
                int hr = CreateDXGIFactory1(ref factoryGuid, out factory);
                if (hr == 0 && factory != null)
                {
                    uint i = 0;
                    IDXGIAdapter1 adapter;
                    while (factory.EnumAdapters1(i, out adapter) == 0)
                    {
                        DXGI_ADAPTER_DESC1 desc;
                        adapter.GetDesc1(out desc);
                        _cachedDxgiAdapters.Add(desc);
                        Marshal.ReleaseComObject(adapter);
                        i++;
                    }
                    Marshal.ReleaseComObject(factory);
                }
            }
            catch
            {
            }

            return _cachedDxgiAdapters;
        }

        public static bool IsVulkanDriverInstalled()
        {
            if (_probedVulkan) return _hasVulkanDriver;
            _probedVulkan = true;

            try
            {
                IntPtr hVk = LoadLibrary("vulkan-1.dll");
                if (hVk != IntPtr.Zero)
                {
                    IntPtr pfn = GetProcAddress(hVk, "vkGetInstanceProcAddr");
                    _hasVulkanDriver = (pfn != IntPtr.Zero);
                    FreeLibrary(hVk);
                }
            }
            catch
            {
                _hasVulkanDriver = false;
            }

            return _hasVulkanDriver;
        }

        public static DXGI_ADAPTER_DESC1? GetPrimaryHardwareAdapter()
        {
            var adapters = GetDxgiAdapters();
            if (adapters == null || adapters.Count == 0) return null;

            // Prioritize discrete hardware adapter (non-software, highest VRAM)
            DXGI_ADAPTER_DESC1? best = null;
            ulong maxVram = 0;

            foreach (var ad in adapters)
            {
                // DXGI_ADAPTER_FLAG_SOFTWARE = 2
                if ((ad.Flags & 2) != 0) continue;

                ulong vram = ad.DedicatedVideoMemory.ToUInt64();
                if (best == null || vram > maxVram)
                {
                    best = ad;
                    maxVram = vram;
                }
            }

            return best ?? (adapters.Count > 0 ? (DXGI_ADAPTER_DESC1?)adapters[0] : null);
        }

        public static string ResolveVendorName(uint vendorId)
        {
            switch (vendorId)
            {
                case 0x10DE: return "NVIDIA Corporation";
                case 0x1002: return "Advanced Micro Devices, Inc. (AMD)";
                case 0x8086: return "Intel Corporation";
                case 0x1414: return "Microsoft Corporation";
                case 0x5143: return "Qualcomm Technologies, Inc.";
                default: return string.Format("Hardware Vendor 0x{0:X4}", vendorId);
            }
        }
    }
}
