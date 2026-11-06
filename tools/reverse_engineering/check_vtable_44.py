import struct, sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']

vt_va = 0x22E3EC4
vt_off = pe['va_to_offset'](vt_va)

method_va = struct.unpack_from("<I", data, vt_off + 0x44)[0]
print(f"trGlobalGridFloatField vtable + 0x44 = VA 0x{method_va:08X}")
