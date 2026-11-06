import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

# Let's inspect function calls inside 0x006F4A70 (from 0x006F4A70 to 0x006F5000)
# and 0x006F5630 (from 0x006F5630 to 0x006F6000)
def find_calls(start_va, end_va):
    calls = []
    for va in range(start_va, end_va):
        off = va_to_off(va)
        if data[off] == 0xE8: # call rel32
            import struct
            rel = struct.unpack("<i", data[off+1:off+5])[0]
            target = va + 5 + rel
            calls.append((va, target))
    return calls

print("Calls in OpenTerrain (0x006F4A70 - 0x006F5000):")
for va, tgt in find_calls(0x006F4A70, 0x006F5000):
    print(f"  0x{va:08X} -> call 0x{tgt:08X}")

print("\nCalls in Scene::Load (0x006F5630 - 0x006F5F00):")
for va, tgt in find_calls(0x006F5630, 0x006F5F00):
    print(f"  0x{va:08X} -> call 0x{tgt:08X}")
