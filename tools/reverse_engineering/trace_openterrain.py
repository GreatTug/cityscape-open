import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

# Trace instructions in 0x006F4A70 - 0x006F4D00
# Look for calls, file reads, and object creation
off = va_to_off(0x006F4A70)
chunk = data[off:off+0x290]

# Print calls and interesting instructions
i = 0
while i < len(chunk):
    va = 0x006F4A70 + i
    b = chunk[i]
    if b == 0xE8:
        import struct
        rel = struct.unpack("<i", chunk[i+1:i+5])[0]
        tgt = va + 5 + rel
        print(f"0x{va:08X}: CALL 0x{tgt:08X}")
        i += 5
    elif b == 0xE9:
        import struct
        rel = struct.unpack("<i", chunk[i+1:i+5])[0]
        tgt = va + 5 + rel
        print(f"0x{va:08X}: JMP  0x{tgt:08X}")
        i += 5
    else:
        i += 1
