with open("CityScape.exe", "rb") as f:
    data = f.read()

import re, struct, sys
sys.path.append('.')
from scratch.pe_helper import parse_pe
pe = parse_pe("CityScape.exe")

for m in re.finditer(rb'serialize[^\x00]*trTerrainSettings', data):
    off = m.start()
    va = pe['offset_to_va'](off)
    print(f"Demangled: {data[off:off+100].decode('latin1', 'replace')}")
    print(f"  RAW {hex(off)}, VA {hex(va)}")
