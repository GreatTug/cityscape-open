with open(r"src\palDotNet\palVisual\ViewportBuildingGizmo.cs", "r", encoding="utf-8") as f:
    lines = f.readlines()

for idx in [415, 510]:
    for j in range(max(0, idx - 5), min(len(lines), idx + 25)):
        print(f"{j+1}: {lines[j].strip()}")
    print("---")
