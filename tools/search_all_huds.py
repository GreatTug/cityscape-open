import os, glob

files = glob.glob("src/**/*.cs", recursive=True) + glob.glob("src/**/*.cpp", recursive=True)
for f in files:
    with open(f, "r", encoding="utf-8", errors="ignore") as fp:
        for idx, line in enumerate(fp):
            if any(k in line for k in ["HudOverlay", "DrawHud", "DrawHUD", "FillRectangle(hudBrush"]):
                print(f"{f}:{idx+1}: {line.strip()}")
