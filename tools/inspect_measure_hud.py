with open(r"src\palDotNet\palVisual\MeasureToolSystem.cs", "r", encoding="utf-8") as f:
    lines = f.readlines()

for idx in range(165, min(len(lines), 210)):
    print(f"{idx+1}: {lines[idx].strip()}")
