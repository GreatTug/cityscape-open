import os

local_app_data = os.environ.get("LOCALAPPDATA")
pa_dir = os.path.join(local_app_data, "PixelActive_Inc")
print(f"Checking {pa_dir}...")

if os.path.exists(pa_dir):
    for root, dirs, files in os.walk(pa_dir):
        for f in files:
            if f.endswith(".config"):
                p = os.path.join(root, f)
                print(f"\n=== Found config: {p} ===")
                with open(p, "r", encoding="utf-8") as cf:
                    for line in cf:
                        if any(k in line for k in ["TerrainSize", "TileSize", "SamplingDistance", "TileCount"]):
                            print("  ", line.strip())
else:
    print("Does not exist.")
