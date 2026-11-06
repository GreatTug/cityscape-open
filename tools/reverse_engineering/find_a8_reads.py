import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

# Search for accesses to +0xA8
# e.g., 8b xx a8 00 00 00 or f3 0f 10 xx a8 00 00 00
for va in range(0x00600000, 0x00710000):
    off = va_to_off(va)
    # mov reg, [reg + 0xa8]
    if data[off] == 0x8B and data[off+2:off+6] == b'\xa8\x00\x00\x00':
        print(f"0x{va:08X}: mov reg, [reg + 0xA8] (byte: {data[off+1]:02X})")
    # movss xmm, [reg + 0xa8]
    elif data[off:off+4] == bytes.fromhex("f30f10") and data[off+5:off+9] == b'\xa8\x00\x00\x00':
        print(f"0x{va:08X}: movss xmm, [reg + 0xA8] (byte: {data[off+4]:02X})")
