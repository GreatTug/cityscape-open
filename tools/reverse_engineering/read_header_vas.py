import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']

for va in [0x23CC92C, 0x23CC8D0, 0x23CC8C0, 0x22E9C54]:
    off = pe['va_to_offset'](va)
    end = data.find(b'\x00', off)
    s = data[off:end]
    print(f"VA 0x{va:08X}: {repr(s)}")
