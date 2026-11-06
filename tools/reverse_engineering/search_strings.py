with open("CityScape.exe", "rb") as f:
    data = f.read()

import re
for m in re.finditer(rb'project bin|terrain bin|bin ver', data):
    start = max(0, m.start() - 30)
    end = min(len(data), m.end() + 30)
    print(f"Offset 0x{m.start():08X}: {data[start:end]}")
