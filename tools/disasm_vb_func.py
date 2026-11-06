import struct

exe_path = r"C:\CityScape 1.8 Autodesk Promo\CityScape.exe"
with open(exe_path, "rb") as f:
    data = f.read()

off = 0xF5258B
for i in range(off - 1000, off, 4):
    val = struct.unpack_from("<I", data, i)[0]
    if 0x1CBC000 <= val < 0x2ED7000:
        rdata_off = 0x18BA800 + (val - 0x1CBC000)
        if 0 <= rdata_off < len(data):
            s = data[rdata_off:rdata_off+40].split(b'\x00')[0]
            if len(s) > 2 and all(32 <= b < 127 for b in s):
                print(f"  Ref at 0x{i:X} -> '{s.decode()}'")
