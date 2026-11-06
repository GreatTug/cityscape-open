with open("src/palDotNet/palModernEngine/CityScapeGeometryPipeline.cs", "r", encoding="utf-8") as f:
    lines = f.readlines()

for idx, line in enumerate(lines):
    if "GenerateTreeProp" in line:
        for j in range(max(0, idx - 5), min(len(lines), idx + 35)):
            print(f"{j+1}: {lines[j].strip()}")
        break
