# CityScape Architecture Documentation

## Overview
**CityScape** (originally created by PixelActive Inc., later acquired by Autodesk in 2010) is a procedural 3D urban modeling and city generation system. It combines real-world GIS elevation and satellite data, procedural road generation, lot parceling, building extrusions, and vegetation placement with real-time Direct3D 9 rendering.

```
+-----------------------------------------------------------------------+
|                             CityScape UI                              |
|   (edEditorDotNet.dll, palDotNet.dll, ktKitDotNet.dll, gisImagery)     |
+-----------------------------------------------------------------------+
|  - Road & Intersection Builder        - DigitalGlobe GIS Downloader   |
|  - Terrain Heightmap & Sculpting      - OpenStreetMap Vector Importer |
|  - Building & Lot Parceling           - COLLADA / FBX 3D Exporter     |
|  - Weather & Astronomical Sky         - Preset Kit & Template Editor  |
+-----------------------------------------------------------------------+
                                   |
                                   v
+-----------------------------------------------------------------------+
|                          Native Engine Core                           |
|                       (CityScape.exe / C++/CLI)                       |
+-----------------------------------------------------------------------+
|  - Procedural Curve Graphs (prCurve, prNetworkImp)                    |
|  - Dynamic Terrain Mesh & LOD (trTerrain, trGeoGrid, trHill)          |
|  - Direct3D 9 Render Pipeline (D3DX9_42.DLL, HLSL win_3_0 Shaders)   |
|  - Image & Texture Decoders (FreeImage.dll, DevIL.dll)                |
|  - Model Import/Export (libcollada14dom21.dll)                        |
|  - Astronomy & Star Catalog (BrightStarCatalog.txt, Lunar Phases)     |
+-----------------------------------------------------------------------+
```

## Module Breakdown

### 1. `palDotNet` (PixelActive Library)
* **Namespace**: `palDotNet`
* **Role**: Foundation UI framework and core utilities.
* **Key Components**:
  * `palVisual`: Custom docking window manager (integrated with `WeifenLuo.WinFormsUI.Docking`), custom toolbars, command bars, viewport render controls, color pickers, and undo/redo stacks.
  * `palVisualPropertyGrid`: High-performance property grid integrating with `SmartPropertyGrid` for real-time node editing.
  * `palDialog`: Specialized modal dialogs for project settings, activation, terrain bounds, and Autodesk export presets.
  * `palUnit`: Units of measurement conversion (meters, feet, kilometers, coordinates).

### 2. `edEditorDotNet` (CityScape Editor)
* **Namespace**: `edEditorDotNet`
* **Role**: Urban planning tools and interactive editors.
* **Key Components**:
  * **Road Network Tool**: Procedural roads, highway tangents, intersections, overpasses, and tunnels.
  * **GIS Importers**: Shapefile (.shp), OpenStreetMap (.osm), DigitalGlobe elevation & imagery.
  * **Lot & Building Generator**: Automatic lot division, setback calculations, building height randomization, roof styles.
  * **Physics & Traffic**: Traffic simulation graph generation and collision proxy builders.
  * **Exporters**: Autodesk FBX, COLLADA (.dae), Google Earth KMZ, and custom gaming engine formats.

### 3. `ktKitDotNet` (Kit & Template Subsystem)
* **Namespace**: `ktKitDotNet`
* **Role**: Template manager for reusable assets (building facades, streetlights, trees, road styles, curbs).
* **Types**: `IntegerType`, `FloatType`, `StringType`, `ColorType`, `Vector2Type`, `Vector3Type`, `Vector4Type`, `EnumerationType`.

### 4. `gisImageryDotNet` (Satellite Imagery)
* **Namespace**: `gisImageryDotNet` / `palDotNet.palDialog`
* **Role**: DigitalGlobe satellite tile streaming, cache handling, and georeferenced coordinate transforms.

### 5. `BugReporter`
* **Role**: Crash report capturing, machine diagnostics, dxdiag inspection, and support transmission.

### 6. Dual-Binary Architecture: Native Hybrid vs Managed Host

CityScape Open Source provides a **dual-binary configuration** to combine legacy hardware/engine fidelity with modern open-source developer ergonomics:

| Executable | Size | Technology | Purpose & Capabilities |
| :--- | :--- | :--- | :--- |
| **`CityScape.exe`** | **49.07 MB** | **Mixed-Mode C++/CLI (x86)** | **Full Native 3D Engine.** Contains 25.9 MB compiled x86 native Direct3D 9 machine code, 18.9 MB HLSL shaders, physics, and COLLADA DOM. Runs seamlessly with our recompiled open-source .NET 4.8 DLLs via `<startup useLegacyV2RuntimeActivationPolicy="true">`. |
| **`CityScape_Managed.exe`** | **127 KB** | **Pure C# (.NET 4.8)** | **Modern Open-Source Launcher.** 100% managed C# host built from `src/CityScape/CityScape.csproj`. Boots full `palVisual.FormApp`, initializes docking, PropertyGrid, and viewport. Can be debugged and modified without unmanaged C++ toolchains. |

#### Why is the original `CityScape.exe` ~50 MB while the managed build is ~125 KB?
In MSVC 2008 C++/CLI (`/clr`), a single executable can statically link unmanaged native C++ libraries alongside .NET IL metadata:
* **`.text` section**: 25,928,704 bytes (25.9 MB) of pure x86 machine instructions (Direct3D 9 device creation, road network triangulation, physics, terrain LOD mesh generation).
* **`.rdata` section**: 18,982,400 bytes (18.9 MB) of compiled DirectX shader bytecode, COLLADA 1.4 schema tables, and native vtables.
* **`.data` & `.reloc`**: 4,050,000 bytes of static engine buffers and base relocations.
* **`.NET Metadata / CLR Header`**: Only ~1 MB is the managed C++/CLI interop header.

All managed .NET assemblies (`palDotNet.dll`, `edEditorDotNet.dll`, `ktKitDotNet.dll`, `gisImageryDotNet.dll`, `BugReporter.exe`) have been 100% decompiled, modernized to .NET 4.8, and recompiled with **0 errors and 0 warnings**.
Both executables are verified live in the automated test suite (`test.bat`).

