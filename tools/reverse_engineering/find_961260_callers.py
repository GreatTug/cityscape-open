import sys, struct
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

target_va = 0x00961260
calls = []
for i in range(len(data)-5):
    if data[i] == 0xE8:
        rel = struct.unpack("<i", data[i+1:i+5])[0]
        va = pe['offset_to_va'](i)
        if va + 5 + rel == target_va:
            calls.append(va)

print(f"Callers of 0x{target_va:08X}:")
for c in calls:
    print(f"  VA 0x{c:08X}")
