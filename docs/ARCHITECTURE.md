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

### 6. Native Core & Serialization
* **Engine Framework**: `arBaseArchive`, `arPCBinaryReadArchive`, `arBinaryWriteArchive`.
* **Geometry Engine**: `prCurveGraph`, `prMultiCurve`, `prNetworkNode`, `prProjectionPlane`, `trTerrainExporter`.
