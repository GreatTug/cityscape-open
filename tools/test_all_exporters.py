import subprocess
import os

formats = [
    ("COLLADA DAE", "test_scene.dae"),
    ("glTF 2.0 Binary (GLB)", "test_scene.glb"),
    ("glTF 2.0 JSON", "test_scene.gltf"),
    ("Pixar OpenUSD", "test_scene.usda"),
    ("Wavefront OBJ", "test_scene.obj")
]

for name, fname in formats:
    if os.path.exists(fname):
        os.remove(fname)
    res = subprocess.run(["CityScape_Managed.exe", f"--export={fname}"], capture_output=True, text=True)
    assert res.returncode == 0, f"Export for {name} failed with code {res.returncode}"
    assert os.path.exists(fname), f"File {fname} was not created!"
    size = os.path.getsize(fname)
    print(f"[PASS] {name:<25} -> {fname:<18} ({size:>10,} bytes)")

print("\nAll 5 3D geometry export formats verified successfully!")
