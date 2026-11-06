# CityScape Global Water & Water Surface Reverse Engineering Toolkit

This directory contains the deep reverse engineering, disassembly, and binary inspection scripts developed to locate, understand, and fix the Global Water Plane and water polygon systems in PixelActive CityScape 1.8.

## Key Discoveries & Architecture

1. **`trWaterPlane` Native Class**:
   - `[edi + 0x50]`: `Enabled` (BOOL)
   - `[edi + 0x34]`: `Height` (FLOAT, sea level elevation, default `0.0f`)
   - `[edi + 0x54]`: `SamplingDist` (FLOAT, default `20.0f`)
   - `[edi + 0x58]`: `HeightFudge` (FLOAT, default `0.0f`)

2. **Root Cause of Missing Water**:
   - The three native constructors of `trWaterPlane` (`0x00b5a1d0`, `0x00b61e70`, `0x00b64f00`) explicitly set `[esi + 0x50] = 0` (`c6 46 50 00` = `Enabled = false`).
   - The render visibility test at `0x00a0b9b0` (called by scene render at `0x00ae4c10`) checks `cmp byte ptr [edi + 0x50], 0`. When false, it returns immediately without rendering water.
   - CityScape lacked a top-level menu or default elevation UI to toggle or elevate the water plane.

3. **The Solution**:
   - Native surgical binary patch: instruction `c6 46 50 00` -> `c6 46 50 01` at offsets `0x00759603`, `0x0076128d`, `0x00764322`, ensuring `trWaterPlane.Enabled = true` by default.
   - Top-level `&Water` UI menu in `FormApp.cs` with hotkey forwarding (`'W'` for Water Mode, `'P'` for Lake Polygons), sea-level presets dialog, and Material Browser integration.
