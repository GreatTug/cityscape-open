import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

for va in [0x0062D230, 0x00AE3A7B, 0x00B85D7F]:
    print(f"\n=== Context for call at 0x{va:08X} ===")
    off = va_to_off(va - 40)
    chunk = data[off:off+60]
    for i in range(0, len(chunk), 16):
        cva = va - 40 + i
        print(f"0x{cva:08X}: " + ' '.join(f"{b:02x}" for b in chunk[i:i+16]))
