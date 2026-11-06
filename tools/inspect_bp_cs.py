with open("src/palDotNet/palModernEngine/CityScapeGeometryPipeline.cs", "r", encoding="utf-8") as f:
    lines = f.readlines()

for idx, line in enumerate(lines):
    if "class BuildingParameters" in line or "struct BuildingParameters" in line:
        for j in range(idx, min(len(lines), idx + 25)):
            print(f"{j+1}: {lines[j].strip()}")
        break
