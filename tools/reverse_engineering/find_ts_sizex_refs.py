import struct, sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']

print("Searching references to TerrainSettings.TerrainSizeX (+0xa8)...")
# [reg + 0xa8] -> 8b .. a8 00 00 00 or 89 .. a8 00 00 00 or f3 0f 10 .. a8 00 00 00 or f3 0f 11 .. a8 00 00 00
import re
pattern = rb'(\x8b|\x89|\xf3\x0f\x10|\xf3\x0f\x11|\xd9|\xdd)[\x40-\xbf]\xa8\x00\x00\x00'
for m in re.finditer(pattern, data):
    off = m.start()
    va = pe['offset_to_va'](off)
    print(f"Match at VA 0x{va:08X} (offset 0x{off:08X}): {data[off:off+10].hex(' ')}")
