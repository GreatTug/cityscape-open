import struct

exe_path = r"C:\CityScape 1.8 Autodesk Promo\CityScape.exe"
with open(exe_path, "rb") as f:
    data = f.read()

for off in [0xF691E0, 0xF7DF00]:
    print(f"\n--- Code at 0x{off:X} ---")
    chunk = data[off : off + 200]
    print(chunk.hex())
    for i in range(off, off + 200, 4):
        val = struct.unpack_from("<I", data, i)[0]
        if 0x1CBC000 <= val < 0x2ED7000:
            rdata_off = 0x18BA800 + (val - 0x1CBC000)
            if 0 <= rdata_off < len(data):
                s = data[rdata_off:rdata_off+40].split(b'\x00')[0]
                if len(s) > 1 and all(32 <= b < 127 for b in s):
                    print(f"  0x{i:X} -> '{s.decode()}'")
