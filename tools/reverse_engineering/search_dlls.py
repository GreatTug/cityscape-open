import os, glob, re

for dll in glob.glob("*.dll") + glob.glob("C:/CityScape 1.8 Autodesk Promo/*.dll"):
    try:
        with open(dll, "rb") as f:
            data = f.read()
        for m in re.finditer(rb'project bin|terrain bin|bin ver', data):
            start = max(0, m.start() - 30)
            end = min(len(data), m.end() + 30)
            print(f"[{dll}] Offset 0x{m.start():08X}: {data[start:end]}")
    except Exception as e:
        pass
