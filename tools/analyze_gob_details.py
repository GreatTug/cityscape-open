import struct
import os
import glob

files = glob.glob("Worlds/**/*.gob", recursive=True)
print(f"Total .gob files: {len(files)}")

sample_files = [
    "Worlds/props/infrastructure/streetlights/light_standard01.gob",
    "Worlds/props/infrastructure/traffic/trafficlight_single.gob",
    "Worlds/props/natural/trees/tree_oak01.gob",
    "Worlds/props/infrastructure/barriers/guardrail_metal.gob",
    "Worlds/props/infrastructure/power/transmission_pylon.gob",
    "Worlds/props/traffic/cars/sedan_midsize.gob"
]

for sf in sample_files:
    if not os.path.exists(sf):
        # find matching
        matched = [f for f in files if os.path.basename(sf).lower() in f.lower()]
        if matched:
            sf = matched[0]
        else:
            continue
    with open(sf, "rb") as f:
        data = f.read()
    print(f"\n--- {sf} ({len(data)} bytes) ---")
    print(f"Header: {data[:24]}")
    # Search for occurrences of gxVertexBufferData, gxIndexBufferData
    for marker in [b"gxVertexBufferData", b"gxIndexBufferData", b"gxModel", b"<ShaderVariables>", b"diffuseTexture"]:
        pos = data.find(marker)
        print(f"  Marker {marker.decode('latin1', errors='ignore')}: pos = 0x{pos:06X}" if pos != -1 else f"  Marker {marker.decode('latin1', errors='ignore')}: NOT found")
