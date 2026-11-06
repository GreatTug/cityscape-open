# CityScape Open Source Status & Handover Roadmap

This document serves as the **official state-of-the-art audit, gap analysis, and developer roadmap** for the PixelActive CityScape 1.8 Open Source project.

---

## 📈 Executive Summary

* **Project Name**: PixelActive CityScape 1.8 (Complete Decompiled Edition)
* **Original Publisher**: PixelActive Inc. (acquired by Autodesk in 2010)
* **Original Binary**: 49,069,568 bytes, Mixed-Mode C++/CLI PE32, timestamp 2010-03-16
* **Decompiled Codebase**: 299 C# source files, 39,192 lines of code across 5 assemblies (.NET Framework 4.8)
* **Build Status**: ✅ **100% Compiling with 0 Errors and 0 Warnings**
* **Verification Suite**: ✅ **11/11 Master Verification Suites Passing (100% Green)**
* **Distribution Releases**: Standalone portable zero-install zip packages (`dist/CityScape_1.8_OpenSource_Portable.zip`)

---

## 🎯 Detailed Subsystem Status Matrix

### 1. User Interface & Workspace Layer
| Component | Status | Details |
| :--- | :--- | :--- |
| **WinForms Shell (`palDotNet.palVisual.FormApp`)** | ✅ 100% Complete | Full Office 2007 docking UI, menu bar, status strip, toolbars. |
| **Docking Panels (`WeifenLuo.WinFormsUI.Docking`)** | ✅ 100% Complete | Persisted docking layouts, floating panes, tool windows. |
| **Property Grid (`SmartPropertyGrid`)** | ✅ 100% Complete | Real-time attribute inspector for roads, lots, buildings, terrain. |
| **Toolbars (`edEditorDotNet`)** | ✅ 100% Complete | Road spline, height tool, tangent manipulator, building, lot, prop, terrain, sky, water toolbars. |
| **Localization & Units (`palUnit`)** | ✅ 100% Complete | Metric (meters, km) and Imperial (feet, miles) conversion. |

### 2. Runtime & Engine Activation
| Component | Status | Details |
| :--- | :--- | :--- |
| **.NET Runtime Interop** | ✅ 100% Complete | `<startup useLegacyV2RuntimeActivationPolicy="true">` bridges modern .NET 4.8 assemblies with the 2008 C++/CLI runtime without `COR_E_NEWER_RUNTIME` (0x8013101B) crash. |
| **Large Address Aware (LAA)** | ✅ 100% Complete | Unlocks 4 GB virtual address space for the 32-bit engine process. |
| **Private MSVC 2008 Runtime** | ✅ 100% Complete | Bundled private SxS `Microsoft.VC90.CRT` directory allows execution on clean Windows 10/11 installations without needing `vcredist_x86.exe`. |

### 3. Terrain & Surface Modeling
| Component | Status | Details |
| :--- | :--- | :--- |
| **1,600m Boundary Limit** | ✅ Unlocked (10,000,000m) | Surgical native patch replaces `1600.0f` constant in `.rdata` with `10,000,000.0f` and patches boundary checks. |
| **Single-Tile Limitation** | ✅ Unlocked (Multi-Tile) | Unlocked 2×2, 4×4, and multi-tile terrain creation in `TerrainSettingsPanel` and native solver. |
| **Dynamic Elevation Board Sync** | ✅ Hooked & Synced | Hooked code caves at `0x006E4D96` and `0x00AE3A00` to expand the workable white elevation grid dynamically. |
| **DEM Elevation Sampling (`trTerrainSettings`)** | 🟡 Disassembled & Mapped | Bilinear height interpolation and 4-point central difference surface normals mapped. Ready for C++20 port. |

### 4. Road Networks & Intersections
| Component | Status | Details |
| :--- | :--- | :--- |
| **Catmull-Rom Ribbon Geometry (`trRoadSegment`)** | 🟡 Disassembled & Mapped | Cubic Catmull-Rom spline evaluation with 2% crown slope camber and 52-byte vertex stride mapped in `tools/decompiled_math_engine_complete.txt`. |
| **Radial Intersection Solver (`rdIntersection`)** | 🟡 Disassembled & Mapped | Multi-arm radial angular sorting ($atan2$), 8.0m corner fillet circular arcs (`rdCornerCurveBorder`), hub polygon fan tessellation mapped. |
| **Dead-End Cul-de-Sacs (`rdCuldesacInfo`)** | 🟡 Disassembled & Mapped | Dead-end turnaround bulb math mapped. |

### 5. Buildings & Urban Lots
| Component | Status | Details |
| :--- | :--- | :--- |
| **Lot Parceling & Setback Generator** | 🟡 Disassembled & Mapped | Roadside margin division and setback polygon math mapped. |
| **Facade Quad Extrusion (`trBuilding`)** | 🟡 Disassembled & Mapped | 3D floor extrusion ($H = 3.5\text{m}$) and floor banding mapped. |
| **Roof Architecture Dispatcher** | 🟡 Disassembled & Mapped | 4 roof styles (Flat + HVAC, Gable, Hip, Mansard) disassembled. |

### 6. Assets & 3D Models
| Component | Status | Details |
| :--- | :--- | :--- |
| **Authentic `.gob` 3D Models** | ✅ 123/123 Verified (100%) | All 123 models (trees, buildings, street lights, vehicles, signs) verified intact. |
| **Vehicle Simulation (`.gox`)** | ✅ 5/5 Verified (100%) | `sedan.gox`, `bus_drive.gox`, `truck_drive.gox`, `van_drive.gox`, `bell_222.gox`. |
| **PARC Asset Container (`assets.parc`)** | ✅ 110/110 Verified (100%) | Shaders, moon phase textures, celestial catalog extracted and verified by `tools/parc_tool.py`. |

### 7. Graphics & Viewport
| Component | Status | Details |
| :--- | :--- | :--- |
| **Direct3D 9 HWND Surface** | ✅ 100% Operational | Authentic GPU hardware viewport renders to WinForms window. |
| **Khronos Vulkan 1.3 Translation** | ✅ Integrated (Default) | DXVK 2.2 translates D3D9 to Vulkan 1.3 for modern GPUs. |
| **DirectX 11/12 Translation** | ✅ Integrated | dgVoodoo2 flip-model translation layer. |
| **Backend Switcher** | ✅ 1-Click Interactive | `switch_graphics.bat` toggles Vulkan / DX12 / D3D9. |

---

## 🔍 Gap Analysis: What is Missing for Full Native Open Source?

While all managed assemblies (C#) are 100% open-source and recompilable, the underlying 3D math and geometry execution is currently provided by the authentic 49 MB hybrid executable (`CityScape.exe`). 

To achieve **100% pure native open-source recompilation without any proprietary 32-bit binary**, the following components need to be implemented:

```
[Phase 1: DONE] Decompile .NET C# Assemblies (palDotNet, edEditorDotNet, ktKitDotNet)
       │
[Phase 2: DONE] Asset Extraction & Modern Graphics (DXVK / Vulkan 1.3 / LAA 4GB)
       │
[Phase 3: IN PROGRESS] Port Disassembled Geometry Algorithms to Clean C++20 / C#
       │
       ├──> Task 3.1: Port `rdIntersection` (Multi-arm radial sorting & 8m fillets)
       ├──> Task 3.2: Port `trRoadSegment` (Catmull-Rom ribbon & 2% crown slope)
       ├──> Task 3.3: Port `trBuilding` (Facade extrusion & 4 roof architectures)
       └──> Task 3.4: Port `trTerrainSettings` (Bilinear DEM elevation grid)
       │
[Phase 4: ROADMAP] Pure 64-Bit Cross-Platform Standalone Engine
       │
       ├──> Task 4.1: Cross-platform graphics backend (BGFX / Vulkan / Silk.NET)
       ├──> Task 4.2: Native glTF 2.0 (.glb) & OpenUSD (.usd) scene exporters
       └──> Task 4.3: Linux & macOS compatibility
```

---

## 🛠️ Step-by-Step Guide for Next Contributors

### 1. Re-implementing a Disassembled Native Routine
All 22 core native functions have already been disassembled with machine instructions and constants in `tools/decompiled_math_engine_complete.txt`.

For example, to implement `SolveOrganicJunction` (`rdIntersection`):
1. Open `tools/decompiled_math_engine_complete.txt` and find section `rdIntersection::SolveOrganicJunction` (`0x00CB1C00`).
2. Read the trigonometric constants from `.rdata`:
   - `0.10f` at `0x022CDE60`
   - `1.00f` at `0x022CDB2C`
   - `0.20f` at `0x022D2DE4`
3. Translate the branch loop:
   - Compute azimuth angles $\theta_i = \text{atan2}(dy_i, dx_i)$ for all incident branches.
   - Sort branches in counter-clockwise order.
   - Connect adjacent curb lines with circular fillet arcs using the radius formula:
     $$R = \text{CurbRadius} \cdot \max(0.1, \sin(\Delta \theta / 2))$$
4. Write the clean C# or C++ implementation and bind it into `palDotNet`!

### 2. Testing Your Changes
Always run:
```cmd
build.bat
test.bat
```
Ensure all 11 test suites pass with 0 errors!
