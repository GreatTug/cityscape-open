import subprocess
import os
import sys

exe_path = "test_export_dialogs.exe"
if not os.path.exists(exe_path):
    alt = os.path.join("bin", "Release", "net48", "test_export_dialogs.exe")
    if os.path.exists(alt):
        exe_path = alt

res = subprocess.run([exe_path], capture_output=True, text=True)
print("Returncode:", res.returncode)
print("Output:\n" + res.stdout)
if res.stderr:
    print("Stderr:\n" + res.stderr)
sys.exit(res.returncode)
