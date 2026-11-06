import struct

exe_path = r"C:\CityScape 1.8 Autodesk Promo\CityScape.exe"
with open(exe_path, "rb") as f:
    data = f.read()

off = 0xF5EEE0
bytes_func = data[off:off+100]
print("Bytes of gxModelSerializerD3D virtual function:")
print(bytes_func.hex())

# Let's inspect call targets
for i in range(off, off + 60):
    if data[i] == 0xE8: # call rel32
        rel = struct.unpack_from("<i", data, i+1)[0]
        target = 0x135FAE0 + (i - off) + 5 + rel
        print(f"  Call at offset 0x{i:X} -> 0x{target:X}")
