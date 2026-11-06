import struct

exe_path = r"C:\CityScape 1.8 Autodesk Promo\CityScape.exe"
with open(exe_path, "rb") as f:
    data = f.read()

def va_to_str(va):
    if 0x1CBC000 <= va < 0x1CBC000 + 0x121A53E:
        foff = 0x18BA800 + (va - 0x1CBC000)
        s = data[foff:foff+40].split(b'\x00')[0]
        return s.decode('ascii', errors='ignore')
    return ""

off = 0x23709D8
for i in range(off, off + 200, 4):
    val = struct.unpack_from("<I", data, i)[0]
    s = va_to_str(val)
    if s:
        print(f"  0x{i:X} (VA 0x{val:X}) -> '{s}'")
