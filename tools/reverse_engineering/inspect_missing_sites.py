import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

def inspect_site(va, length_before=30, length_after=10):
    start = va - length_before
    off = va_to_off(start)
    chunk = data[off:off+length_before+length_after]
    print(f"\n==================== Call at 0x{va:08X} ====================")
    print("Hex before call:", data[va_to_off(va-length_before):va_to_off(va)].hex())
    print("Hex at call:", data[va_to_off(va):va_to_off(va+5)].hex())

for va in [0x0062D230, 0x0096127C, 0x00AE3A7B, 0x00B85D7F]:
    inspect_site(va)
