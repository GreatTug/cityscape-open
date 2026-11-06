import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']

for va in [0x22EE210, 0x22EDE20]:
    off = pe['va_to_offset'](va)
    end = data.find(b'\x00', off)
    s = data[off:end]
    print(f"VA 0x{va:08X}: {repr(s)}")
