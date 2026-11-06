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

def file_offset_to_va(offset):
    for sec in pe.sections:
        if sec.PointerToRawData <= offset < sec.PointerToRawData + sec.SizeOfRawData:
            return image_base + sec.VirtualAddress + (offset - sec.PointerToRawData)
    return offset

# Find any references to 0x00713400 or functions around it
for va_cand in range(0x00713000, 0x00714000, 4):
    target_bytes = struct.pack("<I", va_cand)
    pos = 0
    while True:
        pos = data.find(target_bytes, pos)
        if pos == -1:
            break
        print(f"Ref to 0x{va_cand:08x} at file offset 0x{pos:08x} (VA: 0x{file_offset_to_va(pos):08x})")
        pos += 4
