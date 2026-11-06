import sys, struct
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

# Search for "terrain bin ver 1"
pat = b"terrain bin ver 1"
idx = data.find(pat)
if idx != -1:
    va = pe['offset_to_va'](idx)
    print(f"Pattern found at VA 0x{va:08X}")
    # find references
    b = struct.pack("<I", va)
    i = 0
    while True:
        i = data.find(b, i)
        if i == -1: break
        rva = pe['offset_to_va'](i)
        print(f"  Referenced from VA 0x{rva:08X}")
        i += 1
else:
    print("Pattern not found!")
