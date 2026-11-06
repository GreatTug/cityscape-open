with open("CityScape.exe", "rb") as f:
    data = f.read()

import re, struct, sys
sys.path.append('.')
from scratch.pe_helper import parse_pe
pe = parse_pe("CityScape.exe")

for pattern in [rb'SaveTerrain', rb'SaveProject', rb'Save Terrain', rb'Save Project', rb'Saving terrain', rb'Saving project', rb'Failed to save']:
    for m in re.finditer(pattern, data, re.IGNORECASE):
        off = m.start()
        va = pe['offset_to_va'](off)
        start = max(0, off - 10)
        end = min(len(data), off + len(m.group()) + 20)
        print(f"Match '{m.group().decode('latin1', 'replace')}' at RAW {hex(off)}, VA {hex(va)}: {repr(data[start:end])}")
