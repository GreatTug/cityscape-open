# CityScape Open Source (1.8 Autodesk Promo Edition)

[![Build Status](https://img.shields.io/badge/Build-Passing-brightgreen.svg)](#building-the-project)
[![Target](https://img.shields.io/badge/.NET%20Framework-4.8-blue.svg)](#requirements)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![Reverse Engineered](https://img.shields.io/badge/Decompiled%20With-REA%20%26%20ILSpy-purple.svg)](docs/REVERSE_ENGINEERING_REPORT.md)

Open-source reconstruction and preservation of **PixelActive CityScape 1.8** (Build 1.8.3.34904, Autodesk Promo Edition) — the legendary procedural 3D urban generation engine acquired by Autodesk in 2010.

---

## 🇹🇷 Türkçe Özet

Bu depo, PixelActive / Autodesk tarafından 2010 yılında geliştirilen **CityScape 1.8** kentsel modelleme ve 3B şehir simülasyonu yazılımının tersine mühendislik ile açık kaynak hale getirilmiş, modernize edilmiş ve derlenebilir sürümünü içerir:
- **REA (Reverse Engineer Anything)** ve **ILSpy** kullanılarak tüm .NET kütüphaneleri ve arayüz modülleri decompile edilip modernize edilmiştir.
- Eski `.NET 2.0` bağımlılıkları güncel **.NET 4.8** hedefine yükseltilmiştir.
- `CityScape` (uygulama başlatıcı), `palDotNet`, `edEditorDotNet`, `ktKitDotNet`, `gisImageryDotNet` ve `BugReporter` projelerinin tamamı **0 hata** ile derlenmekte ve `bin/Release/net48` dizinine tam bağımlılıklarıyla paketlenmektedir.
- `.csp` (proje) ve `.cst` (arazi) ikili dosyalarını okuyabilen Python araçları eklenmiştir.

---

## 🏛️ Architecture & Modules

```
c:\cityscape-open
├── CityScape.sln                 # Classic Visual Studio Solution
├── CityScape.slnx                # Modern Visual Studio / .NET 10 Solution
├── build.ps1 / build.bat         # Automated build scripts
├── src/
│   ├── CityScape/                # Application Entry Point & Modern Launcher (.NET 4.8 x86)
│   ├── palDotNet/                # PixelActive Core Library (Docking, Custom Property Grid, Math, GIS)
│   ├── edEditorDotNet/           # CityScape Editor Suite (Roads, Buildings, Lots, Weather, Terrain)
│   ├── ktKitDotNet/              # Architectural Kit & Template System
│   ├── gisImageryDotNet/         # Satellite & GIS Imagery Streamer
│   ├── BugReporter/              # Diagnostics & Crash Reporter
│   └── CityScape_exe/            # Native / C++/CLI Engine Core (3,295 decompiled C# types)
├── lib/                          # Shipped runtime libraries (DirectX 9, FreeImage, Collada, etc.)
├── assets/                       # Shipped shaders, sky/star catalogs, textures
├── Worlds/                       # Sample 3D worlds, terrains (.cst), heightmaps, props
├── docs/                         # Architecture, file formats, and reverse engineering report
└── tools/                        # Python parsers (.csp, .cst, .parc inspector)
```

---

## ⚡ Quick Start & Building

### 1. Requirements
* Windows 10 / 11
* .NET SDK & .NET Framework 4.8 Developer Pack
* Visual Studio 2022 / 2026 or MSBuild Tools
* Python 3.10+ (for utility tools)

### 2. Setup Virtual Environment
```powershell
python -m venv .venv
.\.venv\Scripts\activate
pip install pefile dnfile dncil capstone
```

### 3. Build with One Click
```powershell
.\build.ps1
```
Or with Windows command prompt:
```cmd
build.bat
```

All 6 assemblies (including `CityScape.exe` launcher) will be compiled and staged with all 3rd-party dependencies in `bin/Release/net48/`.

---

## 🛠️ Python Utilities & Tools

Inspect any CityScape binary file format directly:

```powershell
# Inspect project (.csp) or terrain (.cst) files
python tools/csp_reader.py Worlds/

# Inspect packed asset archive (.parc)
python tools/parc_tool.py assets/assets.parc
```

---

## 📖 Documentation
* [Architecture Guide](docs/ARCHITECTURE.md)
* [Binary File Formats (.csp, .cst, .parc)](docs/FILE_FORMATS.md)
* [Reverse Engineering & Decompilation Report](docs/REVERSE_ENGINEERING_REPORT.md)
* [Original QuickStart Guide (PDF)](docs/QuickStart.pdf)
* [Original User Manual (PDF)](docs/UserManual.pdf)

---

## 📄 License
This preservation project is released under the [MIT License](LICENSE) for research, educational, and interoperability purposes.
