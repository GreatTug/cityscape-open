import struct, sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
target_va = 0x00466620
target_bytes = struct.pack("<I", target_va)

pos = 0
while True:
    pos = data.find(target_bytes, pos)
    if pos == -1:
        break
    va = pe['offset_to_va'](pos)
    print(f"Found {hex(target_va)} at file offset {hex(pos)}, VA {hex(va) if va else 'None'}")
    pos += 1
