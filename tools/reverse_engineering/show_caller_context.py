import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

def show_caller(va):
    off = va_to_off(va - 30)
    print(f"Around 0x{va:08X}:")
    for i in range(va - 30, va + 10):
        o = va_to_off(i)
        print(f"  0x{i:08X}: {data[o]:02X}")

print("=== Caller at 0x006F5189 (in OpenTerrain) ===")
# Let's inspect bytes before 0x006F5189:
off = va_to_off(0x006F5170)
print(data[off:off+35].hex())

print("\n=== Caller at 0x006F616F (in Scene::Load) ===")
off = va_to_off(0x006F6150)
print(data[off:off+35].hex())
