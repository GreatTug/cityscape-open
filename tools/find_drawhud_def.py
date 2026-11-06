with open(r"src\palDotNet\palVisual\ViewportRoadGizmo.cs", "r", encoding="utf-8") as f:
    lines = f.readlines()

for idx, line in enumerate(lines):
    if "void DrawHudOverlay" in line:
        for j in range(idx, min(len(lines), idx + 50)):
            print(f"{j+1}: {lines[j].strip()}")
        break
