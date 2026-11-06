import struct

exe_path = r"C:\CityScape 1.8 Autodesk Promo\CityScape.exe"
with open(exe_path, "rb") as f:
    data = f.read()

def file_to_va(foff):
    return 0x400000 + 0x1CBC000 + (foff - 0x18BA800)

off = data.find(b"NumVertices\x00")
va = file_to_va(off)
print(f"NumVertices string at VA 0x{va:X}")

pat = struct.pack("<I", va)
matches = [m for m in range(len(data)) if data[m:m+4] == pat]
print(f"References to NumVertices: {len(matches)}")
for m in matches:
    print(f"  Ref at file 0x{m:X}")
