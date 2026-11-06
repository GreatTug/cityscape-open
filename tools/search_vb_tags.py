import re

exe_path = r"C:\CityScape 1.8 Autodesk Promo\CityScape.exe"
with open(exe_path, "rb") as f:
    data = f.read()

for w in [b"NumVertices", b"VertexSize", b"Vertices", b"VertexFormat", b"FVF", b"Stream"]:
    matches = [m.start() for m in re.finditer(re.escape(w), data)]
    print(f"Matches for {w.decode()}: {len(matches)}")
    for m in matches[:5]:
        snippet = data[m-10:m+40].split(b'\x00')[0]
        print(f"  0x{m:X}: {snippet}")
