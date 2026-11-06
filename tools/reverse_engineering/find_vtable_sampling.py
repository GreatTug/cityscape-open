import struct, sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")

# Let's find xrefs to 0x00466620
off_list = []
target_va = 0x00466620
target_bytes = struct.pack("<I", target_va)

idx = 0
while True:
    idx = pe['data'].find(target_bytes, idx)
    if idx == -1:
        break
    va = pe['offset_to_va'](idx)
    print(f"Xref to SetSamplingDistance (0x00466620) at VA 0x{va:08X} (offset 0x{idx:08X})")
    idx += 4
