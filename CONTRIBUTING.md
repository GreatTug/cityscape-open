# Contributing to CityScape Open Source

Welcome! We are excited to preserve and modernize the historical **PixelActive CityScape 1.8** urban generation suite.

## Development Setup

1. **Prerequisites**:
   * Windows 10 / 11
   * .NET Framework 4.8 Developer Pack & .NET SDK (10.0 or 8.0+)
   * Visual Studio 2022/2026 or MSBuild Tools
   * Python 3.10+ (for tools & parsers)

2. **Python Environment Setup**:
   ```powershell
   python -m venv .venv
   .\.venv\Scripts\activate
   pip install pefile dnfile dncil capstone
   ```

3. **Building the Solution**:
   * Via PowerShell:
     ```powershell
     .\build.ps1
     ```
   * Or via Batch:
     ```cmd
     build.bat
     ```
   * Or open `CityScape.sln` / `CityScape.slnx` in Visual Studio and build directly.

## Modernization Roadmap

* [x] Complete reverse engineering via REA MCP tools.
* [x] Decompile all managed .NET assemblies (`palDotNet`, `edEditorDotNet`, `ktKitDotNet`, `gisImageryDotNet`, `BugReporter`).
* [x] Fix decompilation syntax anomalies (CS0106).
* [x] Retarget and compile successfully on modern .NET 4.8 with 0 errors.
* [x] Create standalone Python CLI tools for `.csp`, `.cst`, and `.parc` inspection.
* [ ] Port rendering pipeline from Direct3D 9 to modern cross-platform graphics (Vulkan / Silk.NET / BGFX / Godot).
* [ ] Replace proprietary licensing checks with free open-source community defaults.
