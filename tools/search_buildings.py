with open("src/palNativeCore/src/CityScapeCore.cpp", "r", encoding="utf-8") as f:
    lines = f.readlines()

for idx, line in enumerate(lines):
    if "m_buildings" in line:
        print(f"Line {idx+1}: {line.strip()}")
