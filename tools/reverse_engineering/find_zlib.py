import struct, sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']

target = b"1.2.3" # typical zlib version
idx = data.find(target)
if idx != -1:
    print(f"zlib version string at RAW {hex(idx)}, VA {hex(pe['offset_to_va'](idx))}")
    print(data[idx-30:idx+30])
