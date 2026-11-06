with open("src/palDotNet/palVisual/OsmCitySceneBuilder.cs", "r", encoding="utf-8") as f:
    lines = f.readlines()

for idx, line in enumerate(lines):
    if any(k in line for k in ["PropMeshes", "GenerateProps", "Tree", "Light", "Vehicl"]):
        print(f"Line {idx+1}: {line.strip()}")
