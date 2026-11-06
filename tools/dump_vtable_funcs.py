import struct

exe_path = r"C:\CityScape 1.8 Autodesk Promo\CityScape.exe"
with open(exe_path, "rb") as f:
    data = f.read()

def va_to_file(va):
    # .text: VA=0x401000, VSz=0x18BA22B, FileOff=0x400
    if 0x401000 <= va < 0x401000 + 0x18BA22B:
        return 0x400 + (va - 0x401000)
    # .rdata: VA=0x1CBC000, VSz=0x121A53E, FileOff=0x18BA800
    if 0x1CBC000 <= va < 0x1CBC000 + 0x121A53E:
        return 0x18BA800 + (va - 0x1CBC000)
    return None

vt_file = 0x1F6E93C
print("Virtual methods in gxModelSerializerD3D vtable:")
for i in range(25):
    entry_va = struct.unpack_from("<I", data, vt_file + i * 4)[0]
    entry_file = va_to_file(entry_va)
    if entry_file is not None and 0x401000 <= entry_va < 0x1CBC000:
        print(f"  [{i}] VA: 0x{entry_va:X} (File: 0x{entry_file:X})")
    else:
        break
