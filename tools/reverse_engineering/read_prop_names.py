import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']

for va in [0x22c74a4, 0x22c74cc, 0x22c7504, 0x22c7524, 0x22c733c]:
    off = pe['va_to_offset'](va)
    end = data.find(b'\x00', off)
    s = data[off:end]
    print(f"VA 0x{va:08X}: {repr(s)}")
