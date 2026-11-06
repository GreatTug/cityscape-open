with open("src/palDotNet/palModernEngine/ModernExporters.cs", "r", encoding="utf-8") as f:
    lines = f.readlines()

for idx, line in enumerate(lines[:60]):
    print(line.strip())
