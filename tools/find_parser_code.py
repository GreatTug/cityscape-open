import struct

exe_path = r"C:\CityScape 1.8 Autodesk Promo\CityScape.exe"
with open(exe_path, "rb") as f:
    data = f.read()

# CityScape.exe base address is usually 0x00400000
# Let's find PE sections
pe_offset = struct.unpack_from("<I", data, 0x3C)[0]
num_sections = struct.unpack_from("<H", data, pe_offset + 6)[0]
opt_hdr_size = struct.unpack_from("<H", data, pe_offset + 20)[0]
img_base = struct.unpack_from("<I", data, pe_offset + 52)[0]

print(f"Image base: 0x{img_base:X}, Sections: {num_sections}")
sec_hdr_start = pe_offset + 24 + opt_hdr_size
sections = []
for i in range(num_sections):
    sec = data[sec_hdr_start + i * 40 : sec_hdr_start + (i + 1) * 40]
    name = sec[:8].strip(b'\x00').decode()
    vsize, va, rsize, roffset = struct.unpack_from("<4I", sec, 8)
    sections.append((name, va, vsize, roffset, rsize))
    print(f"  {name}: VA=0x{img_base+va:X}, VSz=0x{vsize:X}, FileOff=0x{roffset:X}, FileSz=0x{rsize:X}")

def rva_to_file(rva):
    for name, va, vsize, roffset, rsize in sections:
        if va <= rva < va + vsize:
            return roffset + (rva - va)
    return None

def file_to_va(foff):
    for name, va, vsize, roffset, rsize in sections:
        if roffset <= foff < roffset + rsize:
            return img_base + va + (foff - roffset)
    return None

# Find "NumIndices"
target_str = b"NumIndices\x00"
str_off = data.find(target_str)
str_va = file_to_va(str_off)
print(f"NumIndices at File 0x{str_off:X}, VA 0x{str_va:X}")

# Search for pushes or references to str_va in code section
pattern = struct.pack("<I", str_va)
matches = [m for m in range(len(data)) if data[m:m+4] == pattern]
print(f"References to NumIndices string: {len(matches)}")
for m in matches:
    print(f"  Ref at File 0x{m:X}, VA 0x{file_to_va(m):X}")
