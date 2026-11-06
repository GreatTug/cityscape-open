with open(r"src\palDotNet\palVisual\ViewportRoadGizmo.cs", "r", encoding="utf-8") as f:
    lines = f.readlines()

for idx, line in enumerate(lines):
    if "RMB: Place" in line or "Road Tool [" in line or "HUD" in line or "DrawHUD" in line or "RenderHUD" in line:
        for j in range(max(0, idx - 10), min(len(lines), idx + 40)):
            print(f"{j+1}: {lines[j].strip()}")
        break
