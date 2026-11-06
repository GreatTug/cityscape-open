with open("src/palNativeCore/src/CityScapeCore.cpp", "r", encoding="utf-8") as f:
    lines = f.readlines()

for idx, line in enumerate(lines):
    if any(k in line for k in ["GeneratePropsMesh", "GenerateFoliageMesh", "GenerateTrafficMesh", "GeneratePowerGridMesh", "GenerateBuildingMesh"]):
        print(f"Line {idx+1}: {line.strip()}")
