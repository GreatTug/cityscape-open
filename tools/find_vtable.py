import struct

exe_path = r"C:\CityScape 1.8 Autodesk Promo\CityScape.exe"
with open(exe_path, "rb") as f:
    data = f.read()

# RTTI type descriptor is at 0x2C78A44 + something:
# The string is ".?AVgxModelSerializerD3D@@"
rtti_sym = b".?AVgxModelSerializerD3D@@\x00"
sym_off = data.find(rtti_sym)
print(f"RTTI Type Descriptor at file 0x{sym_off:X}")
# The type descriptor address is VA of (sym_off - 8)
# Let's calculate VA
# sym_off is around 0x2C78A68, in .data or .rdata
def file_to_va(foff):
    # .rdata: FileOff=0x18BA800, VA=0x1CBC000
    # .data: FileOff=0x2AD4E00, VA=0x2ED7000
    if 0x18BA800 <= foff < 0x18BA800 + 0x121A600:
        return 0x1CBC000 + (foff - 0x18BA800)
    elif 0x2AD4E00 <= foff < 0x2AD4E00 + 0x1FF200:
        return 0x2ED7000 + (foff - 0x2AD4E00)
    return None

type_va = file_to_va(sym_off - 8)
print(f"type_va = 0x{type_va:X}")

# Search for RTTI Complete Object Locator referencing type_va
pat = struct.pack("<I", type_va)
col_matches = [m for m in range(len(data)) if data[m:m+4] == pat]
print(f"Matches for type_va in data: {len(col_matches)}")
for col in col_matches:
    # A complete object locator has type_va at offset +12
    col_addr = file_to_va(col - 12)
    if col_addr:
        print(f"  COL at VA 0x{col_addr:X}")
        # Search for vtable pointing to col_addr
        pat2 = struct.pack("<I", col_addr)
        vt_matches = [m for m in range(len(data)) if data[m:m+4] == pat2]
        for vt in vt_matches:
            vt_va = file_to_va(vt + 4)
            print(f"    Vtable at VA 0x{vt_va:X}, file 0x{vt+4:X}")
