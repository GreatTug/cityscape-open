import struct, sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']

for target_va in [0x22c97fe, 0x22c9896, 0x22c9962, 0x22ec8fc, 0x22f7002, 0x22f7852]:
    va_b = struct.pack("<I", target_va)
    idx = 0
    while True:
        idx = data.find(va_b, idx)
        if idx == -1:
            break
        ref_va = pe['offset_to_va'](idx)
        print(f"VA 0x{target_va:08X} referenced from VA 0x{ref_va:08X}")
        idx += 1
