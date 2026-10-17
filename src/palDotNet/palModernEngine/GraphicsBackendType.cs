using System;

namespace palDotNet.palModernEngine
{
    /// <summary>
    /// Target hardware architecture for CityScape modern runtime.
    /// </summary>
    public enum TargetArchitecture
    {
        /// <summary>
        /// Legacy 32-bit x86 architecture for older PCs and legacy fallback.
        /// </summary>
        x86_Legacy = 0,

        /// <summary>
        /// Modern 64-bit AMD64/x86-64 architecture with large address space and AVX2/SIMD acceleration.
        /// </summary>
        x64_Modern = 1,

        /// <summary>
        /// Modern 64-bit ARM architecture (Windows on Snapdragon / Surface Pro ARM64 with NEON).
        /// </summary>
        ARM64_Modern = 2
    }

    /// <summary>
    /// Selectable 3D Graphics API backends in CityScape modern engine.
    /// </summary>
    public enum GraphicsBackendType
    {
        /// <summary>
        /// Microsoft DirectX 9.0c Legacy Safe Mode (Legacy fixed function / Shader Model 3.0).
        /// Preserved for vintage hardware and complete backward compatibility.
        /// </summary>
        DirectX9_Legacy = 0,

        /// <summary>
        /// Microsoft DirectX 12 Modern Explicit API (Low-overhead, HLSL SM6+ / DXIL, PBR, Multi-queue).
        /// </summary>
        DirectX12_Modern = 1,

        /// <summary>
        /// Khronos Vulkan 1.3 Modern Explicit API (Cross-platform, SPIR-V shaders, PBR, Pipeline caching).
        /// </summary>
        Vulkan_Modern = 2
    }

    /// <summary>
    /// Feature capabilities detected on active graphics hardware.
    /// </summary>
    [Flags]
    public enum GraphicsFeatures
    {
        None = 0,
        ShaderModel3 = 1 << 0,
        ShaderModel5 = 1 << 1,
        ShaderModel6_DXIL = 1 << 2,
        SPIR_V_Bytecode = 1 << 3,
        HardwareTessellation = 1 << 4,
        MeshShaders = 1 << 5,
        RaytracingTier1 = 1 << 6,
        VariableRateShading = 1 << 7,
        DirectStorage = 1 << 8,
        BindlessResources = 1 << 9
    }
}
