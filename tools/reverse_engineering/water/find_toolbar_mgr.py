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

# search calls or pointers to 0x006b3cd0
target_va = 0x006b3cd0
pos = 0
while True:
    pos = data.find(b"\xe8", pos)
    if pos == -1: break
    curr_va = file_offset_to_va(pos)
    rel = struct.unpack("<i", data[pos+1:pos+5])[0]
    if curr_va + 5 + rel == target_va:
        print(f"Call to 0x006b3cd0 at VA: 0x{curr_va:08x}")
    pos += 1

target = struct.pack("<I", target_va)
pos = 0
while True:
    pos = data.find(target, pos)
    if pos == -1: break
    ref_va = file_offset_to_va(pos)
    print(f"Pointer to 0x006b3cd0 at VA: 0x{ref_va:08x}")
    pos += 4
