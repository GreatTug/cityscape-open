import struct

with open("CityScape.exe", "rb") as f:
    data = f.read()

import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")

for target in [b"project bin ver 1", b"terrain bin ver 1", b"project bin", b"terrain bin"]:
    idx = 0
    found = []
    while True:
        idx = data.find(target, idx)
        if idx == -1:
            break
        va = pe['offset_to_va'](idx)
        print(f"Found '{target.decode()}' at RAW 0x{idx:08X}, VA 0x{va:08X}")
        found.append((idx, va))
        idx += 1
    
    for raw_off, va in found:
        va_bytes = struct.pack("<I", va)
        ridx = 0
        while True:
            ridx = data.find(va_bytes, ridx)
            if ridx == -1:
                break
            ref_va = pe['offset_to_va'](ridx)
            print(f"  VA 0x{va:08X} referenced from RAW 0x{ridx:08X}, VA 0x{ref_va:08X}")
            ridx += 1
