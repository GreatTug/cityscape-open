import struct

exe_path = r"C:\CityScape 1.8 Autodesk Promo\CityScape.exe"
with open(exe_path, "rb") as f:
    data = f.read()

off = 0x23709F8
print(f"Around 0x{off:X}:")
for i in range(off - 32, off + 64, 4):
    val = struct.unpack_from("<I", data, i)[0]
    print(f"  0x{i:X}: 0x{val:X}")
