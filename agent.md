# Project Agent Instructions & Architectural Blueprint (agent.md)

## 📌 Project Identity & Supreme Architectural Mandate
**PixelActive CityScape 1.8 (Complete Decompiled & Modernized Open-Source Edition)**
- **Supreme Mandate**: **100% Birebir Orijinal Legacy Koduna, Modellerine ve Arayüzüne Dayanma Kuralı (Kesin Kural)**.
  - All agents working on this codebase MUST recognize that every aspect of the project MUST be 100% faithful and strictly identical to the original PixelActive CityScape 1.8 (Autodesk Promo, 49,069,568 bytes, PE timestamp: 2010-03-16).
  - **No Synthetic Recompilation / Rewrites**:
    * Previous attempts created a synthetic mock C# application (`CityScapeStudio.exe`) using procedural lollipop trees (colored spheres on sticks), flat cardboard boxes for buildings, and dark floating HUD boxes. **THIS IS NOT DECOMPILATION — IT IS A FAKE RECOMPILE / REWRITE**.
    * Authentic decompilation means 1:1 fidelity to the real 49 MB mixed-mode binary (`CityScape.exe`), its 123 authentic `.gob` 3D binary models, authentic DirectX 9 shaders, and genuine Office 2007 WinForms shell (`WeifenLuo.WinFormsUI.Docking`, `edEditorDotNet`, `palDotNet`).
  - **Runtime Activation Policy & Binary Integrity**:
    * The authentic 49 MB binary (`CityScape.exe`) is a mixed-mode C++/CLI PE32 executable built with Visual C++ 2008 for .NET 2.0 (`v2.0.50727`).
    * To bridge modern .NET 4.8 assemblies (`palDotNet.dll`), `CityScape.exe.config` MUST include `<startup useLegacyV2RuntimeActivationPolicy="true">`. Without this, the CLR crashes on startup with HRESULT `0x8013101B` (`COR_E_NEWER_RUNTIME`).
    * `build.ps1` MUST NEVER overwrite `CityScape.exe` with a 200 KB mock C# launcher. `CityScape.exe` in the workspace root must always remain the authentic 49,069,568-byte binary.
  - **Zero Tolerance for Mock Render Overlays**:
    * `ControlRender.cs` MUST NEVER perform software GDI+ drawing in `OnPaint`. `OnPaint` must strictly execute `base.OnPaint(pe);` and `ForceManagedViewportRendering` must remain `false`.
    * The authentic Direct3D 9 hardware viewport owns the HWND surface directly (`ControlStyles.Opaque = true`).
    * Any dual-render, overlapping mock canvas, or synthetic software rasterizer is completely prohibited.
  - **Authentic Assets & 3D Render Pipeline**:
    * ALL models used must be the authentic `.gob` 3D binary models from `Worlds/props/` (all 123 models: trees, buildings, lights, vehicles, props) and authentic `.dds` textures from `assets/`.
    * Roads and intersections must be rendered as authentic hardware 3D geometry using the native 52-byte vertex buffer format (`0x34` stride: Position, Normal, UV0, Tangent, Color) with genuine asphalt, curb, and crosswalk textures—NEVER approximated with 2D GDI+ polygons.
  - **Authentic C++20 Math & Organic Solver**:
    * Real-time multi-arm radial angular sorting (`rdIntersection`), 8.0m curved corner fillet arcs (`rdCornerCurveBorder`), 2% crown slope camber (`trRoadSegment`), and Hermite tangents must execute seamlessly in native code.
  - **Complete Elimination of Mock Rewrites**:
    * All mock C# classes (`palModernEngine`, `OrganicIntersectionEngine`, `InteractiveRoadModel`, `CityScapeGeometryPipeline`, `FoliageVegetationSystem`, `ViewportRoadGizmo`, `ViewportBuildingGizmo`, `AstronomicalSkySystem`) and mock C++ directories (`src/palNativeCore`, `src/palNativeUI`) have been completely deleted.
    * The project relies exclusively on the authentic 49 MB mixed-mode C++/CLI executable (`CityScape.exe`) and the 5 authentic decompiled managed assemblies (`palDotNet`, `edEditorDotNet`, `ktKitDotNet`, `gisImageryDotNet`, `BugReporter`).
    * Direct3D 9 hardware rendering operates natively with zero synthetic software overlay.

---

## 🏛️ Authentic Decompiled Native x86 Engine & VTable Ledger

The authentic native binary contains 18,942 RTTI type descriptors and 227 virtual method entries across key geometry classes. Every method has been disassembled into `tools/decompiled_math_engine_complete.txt`:

### 1. Intersection & Junction Solver Subsystem
- **`rdIntersection`** (TypeDescriptor: `0x02FF7600`, VTable: `0x0232CD00`):
  - `VFunc[0]` (`0x00CB1C00` / `0x00CB1C20`): `SolveOrganicJunction` - Entrypoint for multi-arm radial angular sorting and fillet curve allocation.
  - `VFunc[1]` (`0x00CB2390`): `BuildBranchConnections` - Iterates over incoming road branches (`[esi + 0x34]`), calls `0x00C9BA60` to establish corner curb returns and road mouth chords.
  - Subroutine `0x00C9BA60`: `InnerBranchConnect` - Dynamic growth and indexing of incident road branch pointers.
  - Subroutine `0x00CB1AE0`: `AllocFillet` - Allocates and registers corner fillet structures in the junction descriptor.
- **`rdIntersectionTessellator`** (TypeDescriptor: `0x02FF436C`, VTable: `0x0232A894`):
  - `VFunc[0]` (`0x00C752B0`): `TessellateHubPolygon` - Computes fan triangulation of the interior junction hub polygon.
  - `VFunc[1]` (`0x00CCBC70`): `GenerateCrosswalkAndPavement` - Evaluates crosswalk zebra striping and pavement textures using 52-byte vertex stride (`imul eax, eax, 0x34`) and UV coordinate thresholds (offset `0x18`).
- **`rdCornerCurveBorder`** (TypeDescriptor: `0x02FF4348`, VTable: `0x0232A884`):
  - `VFunc[0]` (`0x00C74230`): `TessellateCornerCurbs` - Generates circular corner fillet arcs (`CurbRadius`, default 8.0m) connecting adjacent road curb lines.
- **`rdIntersectionCurve`** (TypeDescriptor: `0x02FF4B9C`, VTable: `0x0232CCEC`):
  - `VFunc[0]` (`0x00CB1750`): `EvaluateFilletArc` - Trigonometric evaluation of corner arcs and transition tangents using constants from `.rdata` (`0.10f` at `0x022CDE60`, `1.00f` at `0x022CDB2C`, `0.20f` at `0x022D2DE4`).
  - Subroutine `0x00CB16A0`: `ComputeTangentAngle` - Computes corner arc transition angles and tangents.
- **`rdCuldesacInfo`** (TypeDescriptor: `0x02FF4434`, VTable: `0x0232AA18`):
  - `VFunc[0]` (`0x00C75440`): `GenerateCuldesacBulb` - Solves circular dead-end turnaround bulbs driven by properties: `EnableCuldesacs`, `CuldesacRadius`, `CuldesacSquash`.

### 2. Road Spline & Ribbon Geometry Subsystem
- **`trRoadSegment`** (TypeDescriptor: `0x02F0F4FC`, VTable: `0x022F6BF4`, 45 virtual methods):
  - `VFunc[4]` (`0x00774910`): `GenerateCatmullRomRibbon` - Evaluates Catmull-Rom cubic polynomial splines across road segment knots.
  - `VFunc[6]` (`0x0075F5B0`): `EvaluateCrossSectionElevations` - Applies 2% crown camber slope, curb height (0.15m), and sidewalk lateral profiles.
  - `VFunc[8]` (`0x00CD0000`): `SampleSplineAtParameter` - Evaluates 3D position, tangent, normal, and bank angle at parameter $t \in [0, 1]$ into 52-byte vertex buffers (`imul eax, eax, 0x34`).
  - `VFunc[12]` (`0x00C76A80`): `ComputeTotalLength` - Arc-length integration over road segment spline.
- **`trRoadNode`** (TypeDescriptor: `0x02F0EDE0`, VTable: `0x0232ED5C`, 47 virtual methods):
  - `VFunc[4]` (`0x00D3FAB0`): `SolveTangentsAndBank` - Solves smooth Hermite/Catmull-Rom tangents and curve bank (superelevation) angles.
  - `VFunc[7]` (`0x00CB63C0`): `ComputeBranchAngles` - Computes counter-clockwise radial azimuth angles ($atan2$) for incident road strips.
  - `VFunc[8]` (`0x00CEADA0`): `SortBranchesCounterClockwise` - Radially sorts connected branches around node center.
  - Subroutine `0x00D12830`: `CalcHermiteTangents` - Cubic Hermite tangent optimization.
- **`trRoadNetwork`** (TypeDescriptor: `0x02F0FA3C`, VTable: `0x0232E40C`, 24 virtual methods):
  - `VFunc[17]` (`0x00D7BDA0`): `RebuildAllDirtyIntersections` - Dirty-flag propagation and multi-arm intersection graph rebuild.
- **`trRoadStyle`** (TypeDescriptor: `0x02F0EA48`, VTable: `0x0232EE3C`):
  - Stores road profiles: Interstate Highway (24m, 6 lanes), Expressway (18m, 4 lanes), Viaduct (20m, 4 lanes), Boulevard (14m), Avenue (12m), Local (8m), Alley (4.5m).

### 3. Procedural Buildings & Architecture Subsystem
- **`trBuilding`** (TypeDescriptor: `0x02F0BC1C`, VTable: `0x02322B88`):
  - `VFunc[0]` (`0x00B4F220`): `GenerateProceduralFacadesAndRoof` - Extrudes 2D polygon footprints into 3D multi-floor buildings with floor heights ($H=3.5\text{m}$).
  - Subroutine `0x00B437E0`: `ExtrudeFacade` - Generates vertical facade wall quads and floor banding.
- **`trBuildingRoofStyle`** (TypeDescriptor: `0x02EFEC30`, VTable: `0x02321868`):
  - `VFunc[5]` (`0x00AC0B00`): `EvaluateRoofGeometry` - Master dispatcher for 4 authentic roof architectures.
  - `VFunc[3]` (`0x00A96330`): `GenerateGableOrHip` - Triangular gable ends and 4-slope hip roof ridges.
  - `VFunc[4]` (`0x00B7B330`): `GenerateMansard` - Dual-pitch mansard roof slopes.

### 4. DEM Terrain Surface Subsystem
- **`trTerrainSettings`** (TypeDescriptor: `0x02F0C39C`, VTable: `0x0231BCC4`):
  - `VFunc[4]` (`0x00AC71D0`): `EvaluateBilinearGridElevation` - Bilinear sub-grid height interpolation on DEM elevation rasters.
  - Subroutine `0x00A09080`: `BilinearGridSample` - Inner bilinear interpolation: $h(u, v) = (1-u)(1-v)h_{00} + u(1-v)h_{10} + (1-u)vh_{01} + uvh_{11}$.
  - `VFunc[6]` (`0x00A09140`): `ComputeSurfaceNormal` - 4-point central difference surface normal evaluation for lighting and slope grading: $\vec{N} = \text{norm}(\vec{T}_z \times \vec{T}_x)$.

### 5. Mesh & Buffer Memory Layout
- **`cvMeshGeometryNode`** (TypeDescriptor: `0x0304EF10`, VTable: `0x0234B7A0`):
  - `VFunc[0]` (`0x0101F550`): `AllocateVertexBuffer52ByteStride` - Hardware vertex buffer allocator.
  - Vertex stride is exactly 52 bytes (`imul eax, eax, 0x34`):
    * Offset `0x00`: Position X, Y, Z (3x float32 = 12 bytes)
    * Offset `0x0C`: Normal Nx, Ny, Nz (3x float32 = 12 bytes)
    * Offset `0x18`: UV0 U, V (2x float32 = 8 bytes)
    * Offset `0x20`: Tangent Tx, Ty, Tz (3x float32 = 12 bytes)
    * Offset `0x2C`: Color RGBA (1x uint32 = 4 bytes)
    * Offset `0x30`: Padding (4 bytes) -> Total = 52 bytes (`0x34`)

---

## 🔒 DRM & Binary Serialization Recovery
- The original 2010 binary imports OpenSSL (`LIBEAY32.dll`) ordinals:
  * `490`: `RSA_private_decrypt`
  * `492`: `RSA_public_decrypt`
  * `2996`: `EVP_aes_256_cbc`
- All original `.csp` and `.cst` scene files begin with a 72-byte ASCII header (`"<PixelActive CityScape 1.8.3.34904 [Autodesk Promo]> project bin ver 1"`), followed immediately by a 256-byte RSA/AES-256 encrypted block.
- **Decompiled Solution**:
  * [`WorldProjectSerializer.cs`](file:///c:/cityscape-open/src/palDotNet/palVisual/WorldProjectSerializer.cs) handles both legacy header detection and modern lossless JSON streaming.
  * Preserves full backward round-trip capability without requiring closed-source license activation keys.

---

## 📁 Repository Directory Structure
- `src/palNativeCore/`: Pure C++20 decompiled native engine (`CityScapeCore.dll`, `CityScapeCore.lib`, `CityScapeCoreStatic.lib`).
  * `RoadSpline.cpp`: 1:1 C++20 port of `trRoadSegment` Catmull-Rom ribbon & Hermite tangent generator.
  * `JunctionSolver.cpp`: 1:1 C++20 port of `rdIntersection` & `rdIntersectionTessellator`.
  * `TerrainSampler.cpp`: 1:1 C++20 port of `trTerrainSettings` bilinear elevation & normal math.
  * `BuildingLotGenerator.cpp`: 1:1 C++20 port of `trBuilding` facade extrusion & 4 roof styles.
- `src/palDotNet/`: Decompiled C# WinForms & DirectX/Vulkan managed engine.
  * `palVisual/OrganicIntersectionEngine.cs`: 1:1 C# port of `rdIntersection` and `rdIntersectionTessellator`.
  * `palVisual/InteractiveRoadModel.cs`: 1:1 C# port of `trRoadSegment` Catmull-Rom ribbon generator.
  * `palVisual/ControlRender.cs`: Viewport composite with Maya ALT navigation (Orbit/Zoom/Pan) and tool gizmos.
  * `palVisual/LibraryControl.cs`: Dynamic asset catalog enumerating 100% of authentic `.gob` models (123 models) and `.gox` vehicle simulation files.
- `src/edEditorDotNet/`: Docking UI, export dialogs, and metadata inspectors.
- `src/CityScape/`: Multi-architecture executables (`CityScape_Managed.exe`, `CityScape_x64.exe`, `CityScape_ARM64.exe`).
- `Worlds/`: 100% complete authentic asset tree (626 files: 123 `.gob`, 5 `.gox`, 462 `.dds`, GIS samples).
- `tools/`:
  * `decompiled_math_engine_complete.txt`: Complete disassembly of all 22 core native functions.
  * `vtable_recovery_deep.txt`: Full RTTI memory map of 227 virtual methods.
  * `extract_all_vtables_deep.py`: Automated tool to parse MSVC RTTI and recover vtables.
  * `deep_decompile_math_engine.py`: Capstone disassembler targeting native geometry methods.
- `tests/test_suite.py`: Automated 35-test master verification suite (100% passing, including Test 35 Native Decompilation Audit).

---

## ⚙️ Mandatory Rules for All Agents
1. **Never Assume or Disregard Native Code**:
   - If an algorithm's behavior is in question, disassemble the authentic routine from `CityScape_Old_Legacy.exe` using Capstone and document the exact instruction sequence.
2. **Preserve Mathematical Fidelity**:
   - Curvature fillets, Catmull-Rom tensions, slope crown angles, and vertex strides must match the disassembled constants (`0x34` stride, 2% crown, 8.0m curb radius, `0.10f`, `1.00f`, `0.20f` fillet factors).
3. **Execution Rule**:
   - Never run multiline `python -c` directly in PowerShell. Write python scripts to temporary `.py` files and execute via `.\.venv\Scripts\python.exe`.
4. **Verification Protocol**:
   - Compile native core using `build_native.bat`.
   - Compile solution using `build.ps1`.
   - Run `tests/test_suite.py`. All 35 test suites must pass 100% green before concluding work.
