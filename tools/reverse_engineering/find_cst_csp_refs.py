with open("CityScape.exe", "rb") as f:
    data = f.read()

import re, struct
import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe
pe = parse_pe("CityScape.exe")

for m in re.finditer(rb'\.cst|\.csp|terrain bin|project bin', data, re.IGNORECASE):
    off = m.start()
    va = pe['offset_to_va'](off)
    print(f"Match '{m.group().decode()}' at RAW {hex(off)}, VA {hex(va)}")
    # Find refs
    va_b = struct.pack("<I", va)
    ridx = 0
    while True:
        ridx = data.find(va_b, ridx)
        if ridx == -1:
            break
        ref_va = pe['offset_to_va'](ridx)
        print(f"  Referenced from RAW {hex(ridx)}, VA {hex(ref_va)}")
        ridx += 1
