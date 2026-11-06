import sys, struct
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
pe_bak = parse_pe("CityScape.exe.bak")
d_cur = pe['data']
d_bak = pe_bak['data']
va_to_off = pe['va_to_offset']

def dump_range(va_start, length):
    off = va_to_off(va_start)
    print(f"=== VA 0x{va_start:08X} (len {length}) ===")
    print("CURRENT:")
    print(d_cur[off:off+length].hex())
    print("ORIGINAL:")
    print(d_bak[off:off+length].hex())

dump_range(0x006F4CE0, 100)
dump_range(0x006F5C30, 100)
