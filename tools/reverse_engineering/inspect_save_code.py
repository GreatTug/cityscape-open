import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

def inspect_code(va, length=60):
    off = va_to_off(va)
    print(f"\n=== VA 0x{va:08X} ===")
    chunk = data[off:off+length]
    for i in range(0, len(chunk), 16):
        cva = va + i
        print(f"0x{cva:08X}: " + ' '.join(f"{b:02x}" for b in chunk[i:i+16]))

inspect_code(0x006D8E20, 80)
inspect_code(0x006DEA90, 80)
