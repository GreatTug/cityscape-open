import sys, struct
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

b_addr = struct.pack("<I", 0x00961260)
idx = 0
while True:
    idx = data.find(b_addr, idx)
    if idx == -1:
        break
    va = pe['offset_to_va'](idx)
    print(f"Address 0x00961260 found at VA 0x{va:08X}")
    idx += 1
