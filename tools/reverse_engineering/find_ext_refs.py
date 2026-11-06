import sys, struct
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

for s_va in [0x022EDF94, 0x022EDE70]:
    b = struct.pack("<I", s_va)
    idx = 0
    while True:
        idx = data.find(b, idx)
        if idx == -1: break
        va = pe['offset_to_va'](idx)
        print(f"VA 0x{s_va:08X} referenced at VA 0x{va:08X}")
        idx += 1
