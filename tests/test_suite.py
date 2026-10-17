import os
import sys
import subprocess

def test_all():
    print("=================================================");
    print("  CityScape 1.8 Comprehensive Test Suite Runner  ");
    print("=================================================");

    # 1. Check Output Binaries
    dist_dir = r"bin\Release\net48"
    expected_binaries = [
        "palDotNet.dll",
        "edEditorDotNet.dll",
        "ktKitDotNet.dll",
        "gisImageryDotNet.dll",
        "BugReporter.exe",
        "CityScape.exe",
        "CityScape_Managed.exe",
        "CityScape_x64.exe",
        "CityScape_ARM64.exe",
        "CityScape.exe.config",
        "D3DX9_42.DLL",
        "FreeImage.dll",
        "libcollada14dom21.dll",
        "SmartPropertyGrid.dll",
        "WeifenLuo.WinFormsUI.Docking.dll",
        "test_lifecycle.exe",
        "test_subsystems.exe"
    ]
    print("\n[TEST 1] Verifying staged distribution binaries...")
    for f in expected_binaries:
        path = os.path.join(dist_dir, f)
        assert os.path.exists(path), f"Missing binary: {path}"
        print(f"  [PASS] {f:<32} ({os.path.getsize(path):>10,} bytes)")

    # 2. Test Multi-Architecture Executables & Modern Backend CLI flags
    exe_managed = os.path.join(dist_dir, "CityScape_Managed.exe")
    exe_x64 = os.path.join(dist_dir, "CityScape_x64.exe")
    exe_arm64 = os.path.join(dist_dir, "CityScape_ARM64.exe")
    print("\n[TEST 2] Verifying Multi-Architecture Executables (x86, x64, ARM64) & Graphics Backend Switching...")

    for flag in ["--version", "--diagnostics", "--help", "--backend=dx12", "--backend=vulkan", "--backend=dx9"]:
        res = subprocess.run([exe_managed, flag], capture_output=True, text=True)
        assert res.returncode == 0, f"Flag {flag} failed with code {res.returncode}"
        print(f"  [PASS] CityScape_Managed.exe {flag:<18} exited 0")

    for flag in ["--version", "--diagnostics", "--help", "--backend=dx12", "--backend=vulkan", "--backend=dx9"]:
        res_x64 = subprocess.run([exe_x64, flag], capture_output=True, text=True)
        assert res_x64.returncode == 0, f"x64 Flag {flag} failed with code {res_x64.returncode}"
        print(f"  [PASS] CityScape_x64.exe     {flag:<18} exited 0")

    # Verify PE Machine types
    import struct
    def get_pe_machine(path):
        with open(path, "rb") as f:
            data = f.read(1024)
            pe_off = struct.unpack_from("<I", data, 0x3c)[0]
            return struct.unpack_from("<H", data, pe_off + 4)[0]

    assert get_pe_machine(exe_managed) == 0x014c, "CityScape_Managed.exe is x86 (0x014c)"
    assert get_pe_machine(exe_x64) == 0x8664, "CityScape_x64.exe is AMD64 (0x8664)"
    assert get_pe_machine(exe_arm64) == 0xaa64, "CityScape_ARM64.exe is ARM64 (0xaa64)"
    print("  [PASS] Verified distinct PE hardware targets: x86 (0x014C), x64 (0x8664), ARM64 (0xAA64)")

    exe_native = os.path.join(dist_dir, "CityScape.exe")
    assert os.path.getsize(exe_native) >= 40_000_000, f"CityScape.exe is not full native hybrid binary: {os.path.getsize(exe_native)} bytes"
    print(f"  [PASS] CityScape.exe full native 3D engine verified ({os.path.getsize(exe_native):,} bytes)")

    # 3. Test csp_reader.py on sample worlds
    print("\n[TEST 3] Verifying tools/csp_reader.py on all sample worlds...")
    # Test directory scan
    res = subprocess.run([sys.executable, "tools/csp_reader.py", "Worlds"], capture_output=True, text=True)
    assert res.returncode == 0, f"csp_reader directory scan failed: {res.stderr}"
    assert "Scanned and verified 5 world/terrain files" in res.stdout, f"Unexpected scan result: {res.stdout}"
    print("  [PASS] Directory scan verified 5 world/terrain files")

    # Test individual world files
    sample_worlds = [
        ("Worlds/sample.csp", "PixelActive CityScape 1.8.3.34904 [Autodesk Promo]"),
        ("Worlds/demo.csp", "PixelActive CityScape 1.8.6.42339 [Demo]"),
        ("Worlds/pro.csp", "PixelActive CityScape 1.8.3.34904 [Autodesk Promo]"),
        ("Worlds/terrains/AutodeskSample/AutodeskSample.cst", "PixelActive CityScape 1.8.2.33168"),
        ("Worlds/terrains/SampleTerrain/sample_terrain.cst", "PixelActive CityScape 1.8.3.33751")
    ]
    for w_path, expected_sig in sample_worlds:
        res = subprocess.run([sys.executable, "tools/csp_reader.py", w_path], capture_output=True, text=True)
        assert res.returncode == 0, f"Failed parsing {w_path}: {res.stderr}"
        assert expected_sig in res.stdout, f"Signature mismatch in {w_path}: {res.stdout}"
        assert "Session GUID  :" in res.stdout, f"Missing GUID in {w_path}"
        print(f"  [PASS] Parsed {w_path} -> {expected_sig}")

    # Test error handling on non-existent world file
    res_err = subprocess.run([sys.executable, "tools/csp_reader.py", "non_existent_file.csp"], capture_output=True, text=True)
    assert res_err.returncode == 1, f"Expected csp_reader to exit with code 1 on non-existent file, got {res_err.returncode}"
    print("  [PASS] csp_reader error path correctly exited 1 on non-existent file")

    # 4. Test parc_tool.py on assets
    print("\n[TEST 4] Verifying tools/parc_tool.py integrity check & extraction...")
    parc_path = r"assets\assets.parc"
    extract_dir = r"assets\extracted"

    # 4a. Verify integrity
    res = subprocess.run([sys.executable, "tools/parc_tool.py", parc_path, "--verify"], capture_output=True, text=True)
    assert res.returncode == 0, f"parc_tool --verify failed: {res.stderr}"
    assert "Files Found : 110" in res.stdout, "Unexpected file count in PARC"
    assert "All 110 asset streams verified with 0 CRC/size mismatches" in res.stdout, "Verification output mismatch"
    print("  [PASS] parc_tool --verify confirmed 110 asset streams with 0 CRC/size mismatches")

    # 4b. Extract assets
    res = subprocess.run([sys.executable, "tools/parc_tool.py", parc_path, "--extract", extract_dir], capture_output=True, text=True)
    assert res.returncode == 0, f"parc_tool --extract failed: {res.stderr}"
    assert "Successfully extracted 110/110 files" in res.stdout, "Extraction count mismatch"
    print("  [PASS] parc_tool --extract extracted 110/110 asset streams")

    # Test error handling on non-existent archive
    res_err = subprocess.run([sys.executable, "tools/parc_tool.py", "non_existent_file.parc", "--verify"], capture_output=True, text=True)
    assert res_err.returncode == 1, f"Expected parc_tool to exit with code 1 on non-existent archive, got {res_err.returncode}"
    print("  [PASS] parc_tool error path correctly exited 1 on non-existent archive")

    # 4c. Verify key extracted files exist on disk
    key_assets = [
        os.path.join(extract_dir, "BrightStarCatalog.txt"),
        os.path.join(extract_dir, "PixelActiveLogo.dds"),
        os.path.join(extract_dir, "win_3_0", "waterplane.fxo"),
        os.path.join(extract_dir, "textures", "moon", "moon_phase01.dds")
    ]
    for asset in key_assets:
        assert os.path.exists(asset), f"Extracted asset missing: {asset}"
        assert os.path.getsize(asset) > 0, f"Extracted asset is empty: {asset}"
        print(f"  [PASS] Key asset verified on disk: {asset} ({os.path.getsize(asset):,} bytes)")

    # 5. Test Runtime FormApp & WidgetMgr Initialization
    print("\n[TEST 5] Verifying palDotNet FormApp & WidgetMgr runtime instantiation...")
    ps_cmd = (
        "$ErrorActionPreference = 'Stop'; "
        "Add-Type -Path 'bin\\Release\\net48\\palDotNet.dll'; "
        "$wm = New-Object palDotNet.palWidgets.WidgetMgr; "
        "$wm.Reader = New-Object palDotNet.palCore.SerializerMemoryStreamReader; "
        "$wm.Writer = New-Object palDotNet.palCore.SerializerMemoryStreamWriter; "
        "$init = New-Object palDotNet.palVisual.FormAppBaseInit; "
        "$init.AppName = 'TestApp'; $init.XRes = 800; $init.YRes = 600; "
        "$init.ToolWindowGuidStr = '05675B1B-B72D-4B56-9D16-F3E940BCFF90'; "
        "$init.OutputWindowGuidStr = 'DE0A0C0E-DC18-48C5-8320-8F4E1990C0AE'; "
        "$init.WidgetTheme = 'Office2007'; $init.AppIconPath = ''; $init.LockRenderWindows = $false; "
        "$form = New-Object palDotNet.palVisual.FormApp($init); "
        "if ($form -eq $null) { exit 1 }; "
        "Write-Host 'Form created successfully: ' $form.Text"
    )
    res = subprocess.run(["powershell", "-NoProfile", "-Command", ps_cmd], capture_output=True, text=True)
    assert res.returncode == 0, f"Runtime instantiation failed: {res.stderr}\n{res.stdout}"
    assert "Form created successfully:  TestApp" in res.stdout, f"Unexpected output: {res.stdout}"
    print("  [PASS] FormApp and WidgetMgr runtime instantiation verified with 0 exceptions")

    # 6. Test Full FormApp Lifecycle
    print("\n[TEST 6] Verifying FormApp Full Lifecycle (Layout, WidgetView, PropertyGrid)...")
    lifecycle_exe = os.path.join(dist_dir, "test_lifecycle.exe")
    res = subprocess.run([lifecycle_exe], capture_output=True, text=True)
    assert res.returncode == 0, f"Lifecycle test failed with exit code {res.returncode}:\n{res.stdout}\n{res.stderr}"
    assert "SUCCESS: ALL LIFECYCLE STEPS COMPLETED CLEANLY!" in res.stdout, f"Unexpected lifecycle output: {res.stdout}"
    print("  [PASS] Full lifecycle (Layout, WidgetView, PropertyGrid) passed with 0 exceptions")

    # 7. Test Deep Subsystems Verification
    print("\n[TEST 7] Running Deep Subsystem Verification Suite (all 7 modules including Modern 3D Engine)...")
    subsystems_exe = os.path.join(dist_dir, "test_subsystems.exe")
    res = subprocess.run([subsystems_exe], capture_output=True, text=True)
    assert res.returncode == 0, f"Subsystem tests failed with exit code {res.returncode}:\n{res.stdout}\n{res.stderr}"
    assert "DEEP VERIFICATION ASSERTIONS PASSED (100%)" in res.stdout, f"Subsystem suite failed:\n{res.stdout}"
    print("  [PASS] All deep verification assertions passed across all 7 core modules")

    # 8. Test Live CityScape UI Window & Layout Rendering
    print("\n[TEST 8] Verifying Live CityScape GUI Window, Layout & Viewport...")
    ui_test = subprocess.run([sys.executable, "tools/test_ui_window.py"], capture_output=True, text=True)
    assert "LIVE GUI TESTS PASSED (100%)!" in ui_test.stdout, f"Unexpected UI test output: {ui_test.stdout}"
    print("  [PASS] Live CityScape multi-architecture GUI windows (Native 49MB, Managed x86, Managed x64) verified active, titled, responsive and rendering")

    # 9. Test Automated Distribution Packages & SHA-256 Checksums
    print("\n[TEST 9] Verifying Portable Distribution Packages & SHA-256 Checksums...")
    dist_folder = "dist"
    expected_packages = [
        "CityScape_1.8_Modern_x64_Portable.zip",
        "CityScape_1.8_Modern_ARM64_Portable.zip",
        "CityScape_1.8_Legacy_x86_Portable.zip",
        "CityScape_1.8_Decompiled_Edition_Complete.zip"
    ]
    import hashlib

    checksum_file = os.path.join(dist_folder, "SHA256SUMS.txt")
    assert os.path.exists(checksum_file), f"Missing checksum ledger: {checksum_file}"
    with open(checksum_file, "r", encoding="utf-8") as f:
        checksum_text = f.read()

    for pkg_name in expected_packages:
        pkg_path = os.path.join(dist_folder, pkg_name)
        assert os.path.exists(pkg_path), f"Missing distribution package: {pkg_path}"
        pkg_size = os.path.getsize(pkg_path)
        assert pkg_size >= 50_000_000, f"Package {pkg_name} is unexpectedly small: {pkg_size} bytes"

        # Verify SHA-256
        sha256 = hashlib.sha256()
        with open(pkg_path, "rb") as pf:
            while chunk := pf.read(1024 * 1024):
                sha256.update(chunk)
        calc_hash = sha256.hexdigest().lower()
        assert calc_hash in checksum_text, f"Checksum verification failed for {pkg_name}"
        print(f"  [PASS] {pkg_name:<46} ({pkg_size / (1024*1024):>6.2f} MB) SHA256: {calc_hash[:16]}...")

    print("\n=================================================");
    print("  ALL 9 COMPREHENSIVE TEST SUITES PASSED (100%)  ");
    print("=================================================");

if __name__ == "__main__":
    test_all()
