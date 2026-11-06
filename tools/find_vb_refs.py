import struct

exe_path = r"C:\CityScape 1.8 Autodesk Promo\CityScape.exe"
with open(exe_path, "rb") as f:
    data = f.read()

def file_to_va(foff):
    return 0x400000 + 0x1000 + (foff - 0x400) if foff < 0x18BA800 else 0x400000 + 0x1CBC000 + (foff - 0x18BA800)

target = b"gxVertexBuffer\x00"
str_off = data.find(target)
str_va = file_to_va(str_off)
print(f"gxVertexBuffer at File 0x{str_off:X}, VA 0x{str_va:X}")

pattern = struct.pack("<I", str_va)
matches = [m for m in range(len(data)) if data[m:m+4] == pattern]
print(f"References to gxVertexBuffer: {len(matches)}")
for m in matches:
    print(f"  Ref at File 0x{m:X}, VA 0x{file_to_va(m):X}")
