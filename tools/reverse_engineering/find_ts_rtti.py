with open("CityScape.exe", "rb") as f:
    data = f.read()

import re, struct, sys
sys.path.append('.')
from scratch.pe_helper import parse_pe
pe = parse_pe("CityScape.exe")

for m in re.finditer(rb'TerrainSettings@@', data):
    off = m.start()
    va = pe['offset_to_va'](off)
    print(f"RTTI name match at RAW {hex(off)}, VA {hex(va)}: {data[off-10:off+30]}")
