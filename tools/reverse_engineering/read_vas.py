import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']

for va in [0x22C7604, 0x22C75F4, 0x22EEF8C, 0x22C9A28, 0x22C99F4, 0x22C99FC]:
    off = pe['va_to_offset'](va)
    # read null terminated string
    end = data.find(b'\x00', off)
    s = data[off:end]
    print(f"VA 0x{va:08X} (offset 0x{off:08X}): {repr(s)}")
