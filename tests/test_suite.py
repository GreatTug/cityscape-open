import os
import sys
import subprocess

def test_all():
    print("========================================")
    print("  CityScape 1.8 Open Source Test Suite")
    print("========================================")

    # 1. Check Output Binaries
    dist_dir = r"bin\Release\net48"
    expected_binaries = [
        "palDotNet.dll",
        "edEditorDotNet.dll",
        "ktKitDotNet.dll",
        "gisImageryDotNet.dll",
        "BugReporter.exe",
        "CityScape.exe",
        "CityScape.exe.config",
        "D3DX9_42.DLL",
        "FreeImage.dll",
        "libcollada14dom21.dll",
        "SmartPropertyGrid.dll",
        "WeifenLuo.WinFormsUI.Docking.dll"
    ]
    print("\n[TEST 1] Verifying distribution binaries...")
    for f in expected_binaries:
        path = os.path.join(dist_dir, f)
        assert os.path.exists(path), f"Missing binary: {path}"
        print(f"  [PASS] {f} ({os.path.getsize(path)} bytes)")

    # 2. Test CityScape.exe CLI flags
    exe_path = os.path.join(dist_dir, "CityScape.exe")
    print("\n[TEST 2] Verifying CityScape.exe CLI execution...")
    for flag in ["--version", "--diagnostics"]:
        res = subprocess.run([exe_path, flag], capture_output=True, text=True)
        assert res.returncode == 0, f"Flag {flag} failed with code {res.returncode}"
        print(f"  [PASS] CityScape.exe {flag} exited 0")

    # 3. Test csp_reader.py on sample worlds
    print("\n[TEST 3] Verifying tools/csp_reader.py...")
    res = subprocess.run([sys.executable, "tools/csp_reader.py", "Worlds/sample.csp"], capture_output=True, text=True)
    assert res.returncode == 0, f"csp_reader failed: {res.stderr}"
    assert "PixelActive CityScape 1.8.3.34904" in res.stdout, "Unexpected signature"
    print("  [PASS] tools/csp_reader.py successfully parsed sample.csp")

    # 4. Test parc_tool.py on assets
    print("\n[TEST 4] Verifying tools/parc_tool.py...")
    res = subprocess.run([sys.executable, "tools/parc_tool.py", "assets/assets.parc"], capture_output=True, text=True)
    assert res.returncode == 0, f"parc_tool failed: {res.stderr}"
    assert "Files Found : 110" in res.stdout, "Unexpected asset count"
    print("  [PASS] tools/parc_tool.py successfully verified 110 asset streams")

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

    print("\n========================================")
    print("  ALL 5 TEST SUITES PASSED SUCCESSFULLY")
    print("========================================")

if __name__ == "__main__":
    test_all()
