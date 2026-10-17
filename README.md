# CityScape Open Source (1.8 Decompiled Edition - Modernized)

[![Build Status](https://img.shields.io/badge/Build-Passing-brightgreen.svg)](#building-the-project)
[![Target](https://img.shields.io/badge/.NET%20Framework-4.8-blue.svg)](#requirements)
[![Graphics Backends](https://img.shields.io/badge/Graphics-DirectX%2012%20%7C%20Vulkan%201.3%20%7C%20DirectX%209-orange.svg)](#modern-3d-graphics-architecture)
[![Architectures](https://img.shields.io/badge/Architectures-x64%20%7C%20ARM64%20%7C%20x86-purple.svg)](#multi-architecture-targets)
[![License](https://img.shields.io/badge/License-MIT%20%2F%20Academic%20Research-yellow.svg)](LICENSE)
[![Reverse Engineered](https://img.shields.io/badge/Decompiled%20With-REA%20%26%20ILSpy-purple.svg)](docs/REVERSE_ENGINEERING_REPORT.md)

Open-source reconstruction, modernization, and preservation of **PixelActive CityScape 1.8** (Build 1.8.3.34904, Autodesk Promo Edition) — the legendary procedural 3D urban generation engine acquired by Autodesk in 2010.

> [!NOTE]
> **Academic & Hobby Project Notice**: This project is a non-commercial educational, hobby, and academic research preservation endeavor. It is designed for studying procedural geometry generation, urban synthesis algorithms, and real-time terrain rendering architectures.

---

## 🇹🇷 Türkçe Özet

Bu depo, PixelActive / Autodesk tarafından 2010 yılında geliştirilen **CityScape 1.8** kentsel modelleme ve 3B şehir simülasyonu yazılımının tersine mühendislik ile açık kaynak hale getirilmiş, modernize edilmiş ve derlenebilir sürümünü içerir:
- **REA (Reverse Engineer Anything)** ve **ILSpy** kullanılarak tüm .NET kütüphaneleri ve arayüz modülleri decompile edilip modernize edilmiştir.
- **Çoklu Grafik Altyapısı (Multi-Backend 3D Engine)**:
  - **Microsoft DirectX 12 (D3D12 / DXGI 1.6)**: HLSL SM6+ DXIL derleme, açık kök imzaları (Root Signatures), tanımlayıcı tabloları (Descriptor Tables) ve modern flip-model swapchain.
  - **Khronos Vulkan 1.3**: Yerel SPIR-V bytecode işleme, dinamik çizim (`VK_KHR_dynamic_rendering`) ve optimize `VkDescriptorSetLayout` yönetimi.
  - **DirectX 9 Legacy Geriye Dönük Uyumluluk**: Orijinal Direct3D 9 motoru, eski bilgisayarlar için %100 kararlı fallback olarak korunmuştur.
- **Çoklu Donanım Mimarisi Desteği (Multi-Architecture Targets)**:
  - **x64 Modern (`CityScape_x64.exe`)**: 64-bit bellek adresleme, dinamik `RemotePtr.Use64Bit` serileştirme ve modern Windows 11 entegrasyonu.
  - **ARM64 Modern (`CityScape_ARM64.exe`)**: Snapdragon X Elite ve ARM64 Windows cihazlar için yerel derlenmiş yürütülebilir dosya.
  - **x86 Legacy (`CityScape.exe` - 49.07 MB & `CityScape_Managed.exe`)**: Kilitleri açılmış 10.000m+ dev arazi sistemi.
- **Modern Shader & Render Pipeline**:
  - **Cook-Torrance GGX PBR**: Gerçek zamanlı mikro yüzey metalik/pürüzlülük ışıklandırma modeli.
  - **CDLOD (Continuous Distance Level-of-Detail)**: Araziler için sürekli ve pürüzsüz adaptif teğetsel tessellation.
  - **Slope-Based Multi-Layer Splatting**: Eğim açısına göre kaya, çimen ve toprak dokularını gerçekçi harmanlama.
  - **Gerstner Waves**: 3B çok harmonikli okyanus ve su yüzeyi dalga simülasyonu.
- **Modern 3B Dışa Aktarıcılar (Modern 3D Exporters)**:
  - **Khronos glTF 2.0 / GLB Binary**: Blender, Unreal Engine 5, Unity ve Three.js için tam ikili BIN/JSON tamponları.
  - **Pixar USD / USDA**: Pixar Universal Scene Description ve NVIDIA Omniverse uyumluluğu.
  - **Wavefront OBJ + PBR MTL**: Geleneksel 3B yazılımlar için normal ve roughness/metallic destekli materyaller.
- **Otomatik Taşınabilir Paketleme (`package.bat` / `tools/package.ps1`)**:
  - `dist/CityScape_1.8_Modern_x64_Portable.zip`
  - `dist/CityScape_1.8_Modern_ARM64_Portable.zip`
  - `dist/CityScape_1.8_Legacy_x86_Portable.zip`
  - `dist/CityScape_1.8_Decompiled_Edition_Complete.zip`
  - SHA-256 kriptografik doğrulama defteri (`dist/SHA256SUMS.txt`).

---

## 🏛️ Architecture & Modules

```
c:\cityscape-open
├── CityScape.sln                 # Classic Visual Studio Solution
├── CityScape.slnx                # Modern Visual Studio / .NET 10 Solution
├── build.ps1 / build.bat         # Automated build engine (x86, x64, ARM64)
├── package.ps1 / package.bat     # Portable distribution packager & SHA-256 ledger
├── test.bat                      # 9-suite automated verification runner
├── src/
│   ├── CityScape/                # Application Entry Point & Modern Launchers (x86, x64, ARM64)
│   ├── palDotNet/                # PixelActive Core Library
│   │   └── palModernEngine/      # Modern 3D Engine (DX12, Vulkan, PBR, CDLOD, Exporters)
│   ├── edEditorDotNet/           # CityScape Editor Suite (Roads, Buildings, Lots, Weather, Terrain)
│   ├── ktKitDotNet/              # Architectural Kit & Template System
│   ├── gisImageryDotNet/         # Satellite & GIS Imagery Streamer
│   ├── BugReporter/              # Diagnostics & Crash Reporter
│   └── CityScape_exe/            # Native / C++/CLI Engine Core (3,295 decompiled C# types)
├── lib/                          # Shipped runtime libraries (DirectX 9, FreeImage, Collada, etc.)
├── assets/                       # Shipped shaders, sky/star catalogs, textures
├── Worlds/                       # Sample 3D worlds, terrains (.cst), heightmaps, props
├── dist/                         # Staged portable zip releases and SHA256SUMS.txt
├── docs/                         # Architecture, file formats, and reverse engineering report
└── tools/                        # Automation & verification tools (.csp, .cst, .parc inspector)
```

---

## ⚡ Quick Start & Building

### 1. Requirements
* Windows 10 / 11 (x64 or ARM64)
* .NET SDK & .NET Framework 4.8 Developer Pack
* Visual Studio 2022 / 2026 or MSBuild Tools
* Python 3.10+ (for utility tools and test runner)

### 2. Build All Targets with One Click
```cmd
build.bat
```
Or with PowerShell:
```powershell
.\build.ps1
```
All assemblies and multi-architecture targets (`CityScape_x64.exe`, `CityScape_ARM64.exe`, `CityScape_Managed.exe`, `CityScape.exe`) will compile with 0 errors and 0 warnings.

### 3. Package Portable Distribution Archives
```cmd
package.bat
```
Generates 4 self-contained standalone `.zip` distribution packages with SHA-256 hashes in `dist/`.

### 4. Run Full 9-Suite Verification
```cmd
test.bat
```
Validates staged binaries, CLI flags, PE hardware targets, CSP world parsing, PARC asset extraction, FormApp runtime lifecycle, deep subsystem assertions, live GUI windows, and portable zip distributions.

---

## 🎮 Command-Line Usage

```cmd
# Launch modern 64-bit engine with DirectX 12
CityScape_x64.exe --backend=dx12

# Launch modern 64-bit engine with Vulkan 1.3
CityScape_x64.exe --backend=vulkan

# Print detailed GPU hardware diagnostics
CityScape_x64.exe --diagnostics

# Launch legacy 32-bit DirectX 9 engine
CityScape_Managed.exe --backend=dx9
```

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

## 📄 License & Academic Hobby Disclaimer
This project is released under the [MIT License](LICENSE) strictly as an open-source educational, hobby, and academic research preservation effort. All original trademarks and product names belong to their respective owners (PixelActive Inc. / Autodesk Inc.).
