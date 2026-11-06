import struct
import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']

target = b"Failed to load terrain file"
idx = data.find(target)
va = pe['offset_to_va'](idx)
print(f"'{target.decode()}' at RAW {hex(idx)}, VA {hex(va)}")

# Find references to this VA
va_bytes = struct.pack("<I", va)
ridx = 0
while True:
    ridx = data.find(va_bytes, ridx)
    if ridx == -1:
        break
    ref_va = pe['offset_to_va'](ridx)
    print(f"  Referenced from RAW {hex(ridx)}, VA {hex(ref_va)}")
    ridx += 1
