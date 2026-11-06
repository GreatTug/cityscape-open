# Contributing to CityScape Open Source (1.8 Decompiled Edition)

Welcome! We are preserving, reverse engineering, and modernizing the historic **PixelActive CityScape 1.8** (Autodesk Promo Edition, Build 1.8.3.34904) procedural urban synthesis engine.

Whether you are fixing decompilation bugs, extracting native math routines, creating modern 3D exporters, or writing documentation, your contributions are welcome!

---

## 🏛️ Guiding Architectural Mandate

Before contributing code, please review our core architectural rules:

1. **100% Fidelity to the Authentic 1.8 Engine**:
   - The project relies on authentic decompilation of the mixed-mode C++/CLI executable (`CityScape.exe`) and the 5 managed assemblies (`palDotNet`, `edEditorDotNet`, `ktKitDotNet`, `gisImageryDotNet`, `BugReporter`).
   - **No Synthetic Mock Rewrites**: Do not replace authentic systems with synthetic 2D GDI+ overlays or placeholder lollipop trees. Viewport rendering belongs to the Direct3D 9 / Vulkan HWND hardware surface.
2. **Preserve Mathematical & Structural Constants**:
   - Native 52-byte vertex stride (`0x34`: Position, Normal, UV0, Tangent, Color).
   - Authentic road parameters: 2% crown slope camber, 8.0m curb fillet radius, Hermite & Catmull-Rom spline tangents.
   - 123 authentic `.gob` 3D models and 110 shaders/textures in `assets.parc`.
3. **Clean Code & Zero Warning Builds**:
   - All C# projects in `src/` must compile with **0 errors and 0 warnings** under MSBuild (.NET Framework 4.8).

---

## 💻 Development Environment Setup

### 1. Prerequisites
* **Operating System**: Windows 10 or Windows 11 (x64 / ARM64).
* **Compiler & Build Tools**: Visual Studio 2022 / 2026 or Visual Studio Build Tools with:
  * `.NET desktop development` workload
  * `.NET Framework 4.8 Developer Pack`
* **Python Runtime**: Python 3.10+ (for test suite, parsers, and RE tools).

### 2. Python Environment Setup
```powershell
python -m venv .venv
.\.venv\Scripts\activate
pip install pefile dnfile dncil capstone
```

### 3. One-Click Building
```cmd
build.bat
```
Or with PowerShell:
```powershell
.\build.ps1 -Rebuild
```
This automatically restores NuGet packages, compiles all 5 assemblies, stages output to `bin\Release\net48\`, mirrors files to the workspace root, and applies surgical native patches to `CityScape.exe`.

### 4. Running the Master Test Suite
```cmd
test.bat
```
Runs the 11-suite automated master verification script validating binary integrity, LAA, world parsing, asset streams, FormApp lifecycle, .gob catalog, GUI window initialization, and distribution packages.

---

## 📁 Repository Structure Map

```
cityscape-open/
├── src/                               # 100% Decompiled & Modernized C# Assemblies
│   ├── palDotNet/                     # Core geometric & visual subsystem
│   ├── edEditorDotNet/                # WinForms Editor UI, toolbars, docking panels
│   ├── ktKitDotNet/                   # Architectural template & kit management
│   ├── gisImageryDotNet/              # Satellite & GIS raster streaming
│   ├── BugReporter/                   # Diagnostic & crash reporting tool
│   └── CityScape_exe/                 # Decompiled classes from mixed-mode binary
├── lib/                               # Runtime dependencies (DirectX 9, FreeImage, Collada, OpenSSL)
│   └── Microsoft.VC90.CRT/            # Private SxS MSVC 2008 runtime (zero install required)
├── assets/                            # Shaders, celestial catalogs, textures, assets.parc
├── Worlds/                            # Sample worlds (.csp), terrains (.cst), 123 .gob 3D models
├── docs/                              # Technical documentation, architecture guides, reports
│   └── images/                        # High-resolution screenshots and UI visual assets
├── tools/                             # Automation, verification & packaging tools
│   └── reverse_engineering/           # 140+ disassembly, vtable recovery, and analysis scripts
├── tests/                             # Automated test suite (test_suite.py)
├── build.bat / build.ps1              # 1-Click build engine
├── package.bat                        # Automated portable release packager
├── test.bat                           # Master test suite runner
├── switch_graphics.bat                # 3D graphics backend switcher (Vulkan 1.3 / DX12 / D3D9)
├── CityScape.exe                      # Authentic 49 MB mixed-mode native 3D engine binary
└── CityScape.sln                      # Visual Studio Solution
```

---

## 🛠️ Contribution Areas & Roadmap

### High-Priority Areas for Community Help:
1. **Decompiling Native Routines to Clean C++20 / C#**:
   - Porting disassembled algorithms from `tools/decompiled_math_engine_complete.txt` (`rdIntersection`, `trRoadSegment`, `trBuilding`, `trTerrainSettings`) into clean, standalone C++20 or C# modules.
2. **Modern 3D Exporters**:
   - Adding native glTF 2.0 (`.glb`) and Universal Scene Description (`.usd` / `.usda`) scene exporters alongside the existing COLLADA `.dae` pipeline.
3. **OpenStreetMap & GIS Enhancements**:
   - Expanding GIS ingestion in `gisImageryDotNet` for modern Overpass API / GeoJSON road networks.
4. **Cross-Platform Graphics Backend**:
   - Long-term goal: decoupling the HWND-bound Direct3D 9 viewport to modern Vulkan / Silk.NET / BGFX for Linux and macOS portability.

---

## 🔄 Pull Request Guidelines

1. **Create a Topic Branch**:
   ```bash
   git checkout -b feature/your-feature-name
   ```
2. **Ensure Clean Build**:
   ```cmd
   build.bat -Rebuild
   ```
3. **Verify All Tests Pass**:
   ```cmd
   test.bat
   ```
4. **Commit with Clear Conventional Messages**:
   - `feat(roads): implement Catmull-Rom spline tension parameter`
   - `fix(terrain): prevent overflow on 10,000m extents`
   - `docs(handover): document rdIntersection vtable offsets`
5. **Open a Pull Request**:
   - Describe changes clearly and reference any disassembled function addresses or manual chapters.
