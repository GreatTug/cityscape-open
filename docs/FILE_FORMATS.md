# CityScape File Format Specifications

This document outlines the reverse-engineered binary specifications for CityScape proprietary formats.

---

## 1. CityScape Project (`.csp`)

* **Magic Header**: ASCII string:
  ```text
  "<PixelActive CityScape 1.8.3.34904 [Autodesk Promo]> project bin ver 1"
  ```
  *(or `project bin ver 3` in later demo/commercial releases)*
* **Header Structure**:
  * Bytes `0..N`: Null-terminated or bracketed signature string identifying software version and schema.
  * Followed by 16-byte session GUID.
  * Serialized scene graph nodes:
    * Layer groups (Roads, Buildings, Vegetation, Water, Terrain, Props).
    * Coordinate system & Georeference projection (UTM, WGS84 coordinates).
    * Camera positions, sun position, ambient parameters.
    * Asset references (textures, models, materials).

---

## 2. CityScape Terrain (`.cst`)

* **Magic Header**: ASCII string:
  ```text
  "<PixelActive CityScape 1.8.3.34904 [Autodesk Promo]> terrain bin ver 1"
  ```
* **Payload**:
  * Dimension block: Grid width, length, spacing (meters per sample).
  * Elevation array: 32-bit floating-point height field grid.
  * Surface material layer weights (grass, rock, pavement, sand, asphalt).
  * Dirty tile / modified patch indexing table (`trDirtyTileStore`).

---

## 3. PixelActive Archive (`.parc`)

* **Role**: High-speed packed asset bundle for engine runtime.
* **Header**:
  * `uint32_t Version` (e.g., `0x00000002`)
  * `uint32_t EntryCount` (Number of records)
* **Entry Record**:
  * `uint16_t NameLength`
  * `char Name[NameLength]` (Relative path, e.g. `textures/moon/moon_phase01.dds`)
  * `uint32_t Flags`
  * `uint32_t Offset`
  * `uint32_t CompressedSize`
  * `uint32_t UncompressedSize`
* **Contents**:
  * Compiled Direct3D 9 HLSL shaders (`.fxo` - ambient occlusion, calibration, blending).
  * DirectDraw Surface textures (`.dds` - sky dome, lunar phases, lens flares).
  * Text databases (`BrightStarCatalog.txt` for stellar sky positioning).
