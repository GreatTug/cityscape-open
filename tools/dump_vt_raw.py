import struct

exe_path = r"C:\CityScape 1.8 Autodesk Promo\CityScape.exe"
with open(exe_path, "rb") as f:
    data = f.read()

# Let's inspect the vtable entries at 0x1F6E93C and nearby vtables
vt_file = 0x1F6E93C
for offset in range(vt_file - 64, vt_file + 128, 4):
    val = struct.unpack_from("<I", data, offset)[0]
    print(f"File 0x{offset:X} -> 0x{val:X}")
