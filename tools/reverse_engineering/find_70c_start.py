import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

# Find function start before 0x0070C3A0
for va in range(0x0070C3A0, 0x0070B000, -1):
    off = va_to_off(va)
    if data[off-1:off] == b'\xcc' or data[off-1:off] == b'\x90':
        if data[off:off+3] in (b'\x55\x8b\xec', b'\x83\xec', b'\x53\x56', b'\x64\xa1\x00'):
            print(f"Function containing 0x0070C3A0 starts at: 0x{va:08X}")
            break
