import os, glob

files = glob.glob("src/**/*.cpp", recursive=True) + glob.glob("src/**/*.hpp", recursive=True)
for f in files:
    with open(f, "r", encoding="utf-8", errors="ignore") as fp:
        for idx, line in enumerate(fp):
            if any(k in line for k in ["Road Tool [", "RMB: Place", "DrawHud", "Telemetry"]):
                print(f"{f}:{idx+1}: {line.strip()}")
