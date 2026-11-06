import os, xml.etree.ElementTree as ET

local_app_data = os.environ.get("LOCALAPPDATA")
pa_dir = os.path.join(local_app_data, "PixelActive_Inc")

for root, dirs, files in os.walk(pa_dir):
    for f in files:
        if f.endswith(".config"):
            p = os.path.join(root, f)
            print(f"\n=== {p} ===")
            with open(p, "r", encoding="utf-8") as cf:
                content = cf.read()
            # print lines containing Key and Value
            lines = content.splitlines()
            for i, line in enumerate(lines):
                if "<Key" in line:
                    key = line.strip()
                    val = lines[i+1].strip() if i+1 < len(lines) else ""
                    print(f"  {key} -> {val}")
