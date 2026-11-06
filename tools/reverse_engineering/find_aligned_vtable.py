import struct, sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']

target_va = 0x00466620
target_bytes = struct.pack("<I", target_va)

for i in range(0, len(data) - 4, 4):
    if data[i:i+4] == target_bytes:
        va = pe['offset_to_va'](i)
        print(f"4-byte aligned reference to 0x{target_va:08X} at VA 0x{va:08X} (offset 0x{i:08X})")
