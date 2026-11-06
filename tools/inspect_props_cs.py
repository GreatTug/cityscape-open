with open("src/palDotNet/palModernEngine/CityScapeGeometryPipeline.cs", "r", encoding="utf-8") as f:
    lines = f.readlines()

for idx in range(660, min(len(lines), 720)):
    print(f"{idx+1}: {lines[idx].strip()}")
