# Reverse Engineering & Decompilation Report

## Toolchain & Methodology
* **Inspection Engine**: [REA (Reverse Engineer Anything)](https://github.com/morluto/rea) via MCP Protocol.
* **Decompilation**: `ilspycmd` (ILSpy Engine v11.0) & `dnfile` / `pefile` AST inspection.
* **Target Binary**: `C:\CityScape 1.8 Autodesk Promo` (Build 1.8.3.34904, released March 2010).

---

## Artifact Analysis Summary

| Binary | Classification | Architecture | Types | Methods | Status |
|---|---|---|---|---|---|
| `palDotNet.dll` | Pure CIL (.NET) | AnyCPU / x86 | 330 | 2,295 | Recompiled & Verified (.NET 4.8) |
| `edEditorDotNet.dll` | Pure CIL (.NET) | AnyCPU / x86 | 76 | 816 | Recompiled & Verified (.NET 4.8) |
| `ktKitDotNet.dll` | Pure CIL (.NET) | AnyCPU / x86 | 36 | 246 | Recompiled & Verified (.NET 4.8) |
| `gisImageryDotNet.dll` | Pure CIL (.NET) | AnyCPU / x86 | 4 | 31 | Recompiled & Verified (.NET 4.8) |
| `BugReporter.exe` | Pure CIL (.NET) | AnyCPU / x86 | 10 | 45 | Recompiled & Verified (.NET 4.8) |
| `CityScape.exe` | Mixed C++/CLI & Launcher | x86 (PE32) | 4,440 | 5,573 | Decompiled (3,295 C# source files) + Modern Launcher |

---

## Modernization & Fixes Applied

### 1. Pointer-Arithmetic Reference Addition Fixes (CS1525)
* **Root Cause**: In C++/CLI MSIL, string slice calculations (`ptr + offset`) decompiled by ILSpy generated illegal C# expressions such as:
  ```csharp
  (sbyte*)((ref *(_003F*)num) + (ref System.Runtime.CompilerServices.Unsafe.As<...>(ref target)))
  ```
* **Solution**: Reconstructed clean unsafe pointer arithmetic:
  ```csharp
  ((sbyte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref target) + num)
  ```
* **Files Fixed**:
  - `src/CityScape_exe/-Module-.cs` (23 occurrences)
  - `src/CityScape_exe/edActivationHelpers.cs` (4 occurrences)
  - `src/CityScape_exe/ktTemplateCollectionEditor.cs` (2 occurrences)
  - `src/CityScape_exe/ktTemplateEditor.cs` (2 occurrences)

### 2. C++/CLI Value-Type Metadata Normalization (CS0106 & CS0592)
* **CS0106**: Stripped invalid `static struct` declarations generated when ILSpy decompiled empty C++ structures marked `abstract sealed`.
* **CS0592**: Removed `[NativeCppClass]` attributes from C# `enum` types where C# compilers reject class/struct-only attributes on enumeration types.
* **CS0591**: Replaced empty `[DllImport("")]` attributes on internal native machine-code stubs with explicit module targeting.

### 3. Mixed C++/CLI Architecture & Modern Launcher Solution
* **Architectural Reality**: `CityScape.exe` was originally built using MSVC++ with `/clr` mixed-mode compilation. The resulting binary contains both native x86 machine instructions (the DirectX 9 renderer, Collada parser, physics) and CLR metadata calling `_WinMainCRTStartup()`.
* **Open Source Strategy**: 
  - Preserved all 3,295 decompiled C++/CLI source files in `src/CityScape_exe/` with syntax error corrections.
  - Implemented a clean, first-class launcher in `src/CityScape/` targeting `.NET Framework 4.8 (x86)` that links directly against `palDotNet.dll`, `edEditorDotNet.dll`, `ktKitDotNet.dll`, and `gisImageryDotNet.dll`.
  - Added CLI diagnostics (`--diagnostics`), versioning (`--version`), and help options.
  - The entire solution now compiles with **0 warnings and 0 errors** across all projects.

### 4. Explicit Interface Modifier Syntax (CS0106)
* ILSpy decompiled explicit interface implementations with `virtual` keywords (e.g., `virtual void IFormApp.BringToFront()`).
* Stripped invalid modifiers across all managed libraries.

### 5. Automated Distribution Staging
* `build.ps1` compiles all 6 projects into `Release` mode and automatically stages a runnable distribution into `bin/Release/net48/`, bundling all required 3rd-party dependencies (`DirectX 9`, `FreeImage`, `SmartPropertyGrid`, `WeifenLuo.WinFormsUI.Docking`, `Xheo.Licensing`).

### 6. Runtime WidgetMgr & Stream Initialization Fix
* **Fatal Functional Bug**: In the initial launcher implementation, `WidgetMgr.Instance.EventQuit` was accessed before `WidgetMgr` was instantiated, and `FormApp..ctor` similarly requires `WidgetMgr.Instance` to configure event delegates. This caused immediate `NullReferenceException` on startup.
* **Solution**: Explicitly instantiated `WidgetMgr` along with its input/output stream adapters (`SerializerMemoryStreamReader` and `SerializerMemoryStreamWriter`) in `src/CityScape/Program.cs` before any UI or event registration.

### 7. Layout Persistence & Viewport Null Safety (CS8602)
* **Root Cause**: `DocumentRenderView.Guid` assumed `ControlRender.RenderWindow` was always non-null. During window layout deserialization, accessing `TheViewDocument.Guid` before native viewport attachment threw `NullReferenceException`.
* **Solution**: Updated `DocumentRenderView.cs` with null-safe property evaluation:
  ```csharp
  public override Guid Guid => (ControlRender?.RenderWindow != null) ? ControlRender.RenderWindow.Guid : Guid.Empty;
  ```

### 8. Remote Assembly CAS Policy Compatibility
* **Issue**: On modern Windows systems, assemblies marked with Mark-of-the-Web (ZoneId 3) failed to load via .NET Framework 4.8 with HRESULT `0x80131515`.
* **Solution**: Added `<runtime><loadFromRemoteSources enabled="true"/></runtime>` to `CityScape.exe.config`.

### 9. Extended C++/CLI Decompilation Error Repair (src/CityScape_exe/)
* Repaired 176 remaining `[NativeCppClass]` attributes on enums across the decompiled tree (CS0592).
* Normalized 221 unmanaged function pointer calling convention annotations (`delegate* unmanaged[Cdecl, Cdecl]` -> `delegate*`) in `src/CityScape_exe/-Module-.cs` for .NET Framework 4.8 compliance (CS8889).
* Removed 10 redundant/duplicate method overloads in `src/CityScape_exe/-Module-.cs` (CS0111).
* Added `System.Runtime.CompilerServices.Unsafe` PackageReference to `src/CityScape_exe/CityScape.csproj`.

---

## Verification & Parity Audit

* **Type Parity**: 100% of domain types and namespaces from original DLLs are preserved in decompiled assemblies.
* **Resources & Icons**: 28 toolbar action icons, 3 application icons, and all dialog `.resx` resources verified intact.
* **Binary Tools**: `tools/csp_reader.py` and `tools/parc_tool.py` verified with live sample files in `Worlds/` and `assets/`.
* **Runtime Verification**: `tests/test_suite.py` validates all 12 distribution binaries, CLI flags, format parsers, and headless FormApp / WidgetMgr runtime instantiation without exceptions.
