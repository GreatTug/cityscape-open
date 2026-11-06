import sys, struct
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

# Dump 0x006F4510 to 0x006F45E0
off = va_to_off(0x006F4510)
chunk = data[off:off+0xD0]

i = 0
while i < len(chunk):
    va = 0x006F4510 + i
    b = chunk[i]
    if b == 0xE8:
        rel = struct.unpack("<i", chunk[i+1:i+5])[0]
        tgt = va + 5 + rel
        print(f"0x{va:08X}: CALL 0x{tgt:08X}")
        i += 5
    elif b in (0xC3, 0xC2):
        print(f"0x{va:08X}: RET")
        i += 1 if b == 0xC3 else 3
    else:
        i += 1
