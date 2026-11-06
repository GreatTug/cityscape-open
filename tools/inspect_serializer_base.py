import struct
import pefile

pe = pefile.PE("CityScape_Old_Legacy.exe")
image_base = pe.OPTIONAL_HEADER.ImageBase

with open("CityScape_Old_Legacy.exe", "rb") as f:
    raw_data = f.read()

def va_to_offset(va):
    rva = va - image_base
    for sec in pe.sections:
        if sec.VirtualAddress <= rva < sec.VirtualAddress + sec.Misc_VirtualSize:
            return sec.PointerToRawData + (rva - sec.VirtualAddress)
    return None

def read_u32(va):
    off = va_to_offset(va)
    if off is not None and off + 4 <= len(raw_data):
        return struct.unpack("<I", raw_data[off:off+4])[0]
    return None

vt_va = 0x0236EA98
for i in range(15):
    fn = read_u32(vt_va + i * 4)
    if fn:
        print(f"[{i:2d}] 0x{fn:08X}")
