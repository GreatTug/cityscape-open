import struct, sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']

off = 0x01FCB0F8
va = pe['offset_to_va'](off)
print(f"VA: {hex(va)}")

va_b = struct.pack("<I", va)
idx = 0
while True:
    idx = data.find(va_b, idx)
    if idx == -1:
        break
    ref_va = pe['offset_to_va'](idx)
    print(f"  Referenced from RAW {hex(idx)}, VA {hex(ref_va)}")
    idx += 1
