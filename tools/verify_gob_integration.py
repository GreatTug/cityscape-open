import os
import subprocess

print("Running test_native_core to verify authentic .gob integration...")
res = subprocess.run([r"build_native_x64\Release\test_native_core.exe"], capture_output=True, text=True)
print("Exit code:", res.returncode)
for line in res.stdout.splitlines():
    if "TEST 9" in line or "TEST 11" in line or "TEST 13" in line or "TEST 18" in line or "Streetlight" in line or "Tree" in line or "Sedan" in line or "vertices" in line:
        print(" ", line)
