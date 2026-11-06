import subprocess

res = subprocess.run(["CityScape_Managed.exe", "--export=test_scene.dae"], capture_output=True, text=True)
print("Returncode:", res.returncode)
print("Stdout:", res.stdout)
print("Stderr:", res.stderr)
