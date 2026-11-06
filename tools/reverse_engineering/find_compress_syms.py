import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']

for s in [b"inflate", b"deflate", b"uncompress", b"compress", b"zlib"]:
    idx = 0
    while True:
        idx = data.find(s, idx)
        if idx == -1:
            break
        va = pe['offset_to_va'](idx)
        print(f"'{s.decode()}' at RAW {hex(idx)}, VA {hex(va)}")
        idx += len(s)
