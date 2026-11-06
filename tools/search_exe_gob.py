import re

exe_path = r"C:\CityScape 1.8 Autodesk Promo\CityScape.exe"
with open(exe_path, "rb") as f:
    data = f.read()

# Search for strings related to gxModel, .gob, serializer, etc.
keywords = [b"gxModelSerializer", b"gxModel", b"gxVertexBuffer", b"gxIndexBuffer", b"gxMesh", b"gxGeometry"]
for kw in keywords:
    matches = [m.start() for m in re.finditer(re.escape(kw), data)]
    print(f"Matches for {kw.decode()}: {len(matches)}")
    for m in matches[:5]:
        start = max(0, m - 40)
        end = min(len(data), m + 60)
        s = data[start:end]
        # print printable characters
        clean = "".join(chr(b) if 32 <= b < 127 else "." for b in s)
        print(f"  0x{m:X}: {clean}")
