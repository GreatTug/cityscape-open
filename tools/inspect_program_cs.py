with open("src/CityScape/Program.cs", "r", encoding="utf-8") as f:
    lines = f.readlines()

for idx, line in enumerate(lines[:60]):
    print(f"{idx+1}: {line.strip()}")
