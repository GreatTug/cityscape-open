import sys, struct
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

# Let's search for references to 0x00961260 in virtual tables or code
for delta in range(-16, 17):
    target = 0x00961260 + delta
    b = struct.pack("<I", target)
    idx = data.find(b)
    if idx != -1:
        va = pe['offset_to_va'](idx)
        print(f"Target 0x{target:08X} found at VA 0x{va:08X}")
