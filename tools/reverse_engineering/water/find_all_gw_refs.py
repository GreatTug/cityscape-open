import pefile
import struct
import capstone

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

target = struct.pack("<I", 0x22ebb54)
pos = 0
refs = []
while True:
    pos = data.find(target, pos)
    if pos == -1: break
    ref_va = file_offset_to_va(pos)
    refs.append(ref_va)
    pos += 4

print(f"Total references to 'Global Water' (0x22ebb54): {len(refs)}")
for r in refs:
    print(f"  VA: 0x{r:08x}")
