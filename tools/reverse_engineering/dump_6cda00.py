import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

# Dump 100 bytes from 0x006CDA00
off = va_to_off(0x006CDA00)
chunk = data[off:off+100]

for i in range(0, len(chunk), 16):
    va = 0x006CDA00 + i
    h = ' '.join(f"{b:02x}" for b in chunk[i:i+16])
    print(f"0x{va:08X}: {h}")
