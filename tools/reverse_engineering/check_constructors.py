import sys, struct
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

for va in [0x005DEBE0, 0x005DF280, 0x005DF610]:
    off = va_to_off(va)
    chunk = data[off:off+35]
    print(f"=== VA 0x{va:08X} ===")
    print("Hex:", chunk.hex())
    # find call
    for i in range(len(chunk)-5):
        if chunk[i] == 0xE8:
            rel = struct.unpack("<i", chunk[i+1:i+5])[0]
            tgt = va + i + 5 + rel
            print(f"  Call at 0x{va+i:08X} -> 0x{tgt:08X}")
