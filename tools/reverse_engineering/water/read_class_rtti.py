import pefile
import struct

pe = pefile.PE("CityScape.exe")
image_base = pe.OPTIONAL_HEADER.ImageBase

with open("CityScape.exe", "rb") as f:
    data = f.read()

def va_to_file_offset(va):
    rva = va - image_base
    for sec in pe.sections:
        if sec.VirtualAddress <= rva < sec.VirtualAddress + sec.Misc_VirtualSize:
            return sec.PointerToRawData + (rva - sec.VirtualAddress)
    return None

def read_str_at(va):
    off = va_to_file_offset(va)
    if off is None:
        return "<invalid va>"
    end = data.find(b"\x00", off)
    if end == -1: end = off + 100
    return data[off:end].decode('ascii', errors='replace')

# Look at 0x2321a00 vtable RTTI: vtable[-1] is RTTI Complete Object Locator
vtable_va = 0x2321a00
off = va_to_file_offset(vtable_va)
col_va = struct.unpack("<I", data[off-4:off])[0]
print(f"COL VA: 0x{col_va:08x}")
col_off = va_to_file_offset(col_va)
# in MSVC x86, CompleteObjectLocator has type_descriptor at col+0x0c
td_va = struct.unpack("<I", data[col_off+12:col_off+16])[0]
print(f"TD VA: 0x{td_va:08x}")
td_off = va_to_file_offset(td_va)
# type_descriptor name is at td+8
name = data[td_off+8:data.find(b"\x00", td_off+8)].decode('ascii', errors='replace')
print("Class Name:", name)
