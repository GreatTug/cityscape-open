import sys, struct
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

# Search for strings related to saving terrain or cst
strings = [b".cst", b".csp", b"SaveTerrain", b"SaveProject"]
for s in strings:
    idx = 0
    while True:
        idx = data.find(s, idx)
        if idx == -1: break
        va = pe['offset_to_va'](idx)
        print(f"String '{s.decode('latin1')}' at VA 0x{va:08X}")
        idx += 1
