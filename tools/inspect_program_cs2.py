with open("src/CityScape/Program.cs", "r", encoding="utf-8") as f:
    lines = f.readlines()

for idx in range(60, min(len(lines), 130)):
    print(f"{idx+1}: {lines[idx].strip()}")
