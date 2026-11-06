import sys, struct
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

# Find all calls in 0x0070C000 - 0x0070C450
start_va = 0x0070C000
end_va = 0x0070C450

for va in range(start_va, end_va):
    off = va_to_off(va)
    if data[off] == 0xE8:
        rel = struct.unpack("<i", data[off+1:off+5])[0]
        tgt = va + 5 + rel
        print(f"0x{va:08X} -> call 0x{tgt:08X}")
