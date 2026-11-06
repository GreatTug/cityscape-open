import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

def inspect_func(va, length=60):
    off = va_to_off(va)
    chunk = data[off:off+length]
    print(f"=== VA 0x{va:08X} (len {length}) ===")
    print("Hex:", chunk.hex())

inspect_func(0x006F45A0, 80)
inspect_func(0x006F5170, 60)
inspect_func(0x006F6150, 60)
inspect_func(0x006FD8F0, 60)
