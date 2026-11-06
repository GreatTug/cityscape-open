import struct, sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']

target_va = 0x00466620
va_b = struct.pack("<I", target_va)

idx = 0
while True:
    idx = data.find(va_b, idx)
    if idx == -1:
        break
    ref_va = pe['offset_to_va'](idx)
    print(f"Pointer at RAW {hex(idx)}, VA {hex(ref_va)}")
    idx += 1
