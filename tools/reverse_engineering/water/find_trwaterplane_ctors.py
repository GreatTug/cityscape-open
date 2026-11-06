import pefile
import struct

pe = pefile.PE("CityScape.exe")
image_base = pe.OPTIONAL_HEADER.ImageBase

with open("CityScape.exe", "rb") as f:
    data = f.read()

def file_offset_to_va(offset):
    for sec in pe.sections:
        if sec.PointerToRawData <= offset < sec.PointerToRawData + sec.SizeOfRawData:
            return image_base + sec.VirtualAddress + (offset - sec.PointerToRawData)
    return offset

target = struct.pack("<I", 0x02322ccc)
pos = 0
while True:
    pos = data.find(target, pos)
    if pos == -1: break
    va = file_offset_to_va(pos)
    print("Reference to trWaterPlane vtable at VA:", hex(va))
    pos += 4
