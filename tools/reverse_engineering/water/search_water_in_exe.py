import struct
import pefile

with open("CityScape.exe", "rb") as f:
    data = f.read()

def find_all(pattern):
    idx = 0
    while True:
        pos = data.find(pattern, idx)
        if pos == -1:
            break
        yield pos
        idx = pos + len(pattern)

for pat in [b"waterplane", b"water.fxo", b"trWaterPlane", b"taWaterPlane", b"edWaterMode", b"Global Water", b"water"]:
    matches = list(find_all(pat))
    print(f"Pattern {pat}: {len(matches)} matches")
    for m in matches[:5]:
        ctx = data[max(0, m-20):min(len(data), m+50)]
        print(f"  at 0x{m:08x}: {ctx}")
