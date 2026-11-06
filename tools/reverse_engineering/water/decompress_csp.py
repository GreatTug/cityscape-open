import zlib

with open(r"Worlds\sample.csp", "rb") as f:
    data = f.read()

# search for 78 9c or 78 01 or 78 da
for i in range(len(data) - 10):
    if data[i] == 0x78 and data[i+1] in (0x01, 0x9c, 0xda, 0x5e):
        try:
            d = zlib.decompress(data[i:])
            print(f"Decompressed at offset {i} (0x{i:x}): size = {len(d)}")
            if b"water" in d.lower():
                print(" Found 'water' in stream!")
                pos = 0
                while True:
                    p = d.lower().find(b"water", pos)
                    if p == -1: break
                    print("  ctx:", d[max(0, p-20):min(len(d), p+50)])
                    pos = p + 5
            break
        except Exception:
            pass
