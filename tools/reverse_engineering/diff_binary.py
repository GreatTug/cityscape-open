import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe_cur = parse_pe("CityScape.exe")
pe_bak = parse_pe("CityScape.exe.bak")

d_cur = pe_cur['data']
d_bak = pe_bak['data']

diffs = []
in_diff = False
start_idx = 0

for i in range(len(d_cur)):
    if d_cur[i] != d_bak[i]:
        if not in_diff:
            in_diff = True
            start_idx = i
    else:
        if in_diff:
            diffs.append((start_idx, i))
            in_diff = False

if in_diff:
    diffs.append((start_idx, len(d_cur)))

print(f"Total diff blocks: {len(diffs)}")
for s, e in diffs:
    va = pe_cur['offset_to_va'](s)
    cur_bytes = d_cur[s:e]
    bak_bytes = d_bak[s:e]
    print(f"Block at VA 0x{va:08X} (len {e-s}):")
    print(f"  BAK (Orig): {bak_bytes.hex()}")
    print(f"  CUR (Pat) : {cur_bytes.hex()}")
