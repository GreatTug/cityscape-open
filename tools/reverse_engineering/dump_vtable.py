import sys, struct
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

vtable_va = 0x022E2AE8
off = va_to_off(vtable_va)

print(f"=== Vtable at 0x{vtable_va:08X} ===")
for i in range(25):
    entry = struct.unpack("<I", data[off + i*4 : off + (i+1)*4])[0]
    print(f"  [{i:2d}] +0x{i*4:02X}: 0x{entry:08X}")
