with open("src/palDotNet/palModernEngine/CityScapeGeometryPipeline.cs", "r", encoding="utf-8") as f:
    lines = f.readlines()

for idx in range(790, min(len(lines), 820)):
    print(f"{idx+1}: {lines[idx].strip()}")
