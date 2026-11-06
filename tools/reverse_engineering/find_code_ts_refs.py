import struct, sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']

# Let's search for the string "trTerrainSettings" in code/rdata
target = b"trTerrainSettings"
idx = 0
found = []
while True:
    idx = data.find(target, idx)
    if idx == -1:
        break
    va = pe['offset_to_va'](idx)
    # Check if this VA is referenced in .text
    va_b = struct.pack("<I", va)
    ridx = 0
    while True:
        ridx = data.find(va_b, ridx)
        if ridx == -1:
            break
        ref_va = pe['offset_to_va'](ridx)
        if ref_va and ref_va < 0x01CBC000: # in .text
            print(f"'{target.decode()}' at VA 0x{va:08X} referenced from code VA 0x{ref_va:08X}")
            found.append(ref_va)
        ridx += 1
    idx += 1
