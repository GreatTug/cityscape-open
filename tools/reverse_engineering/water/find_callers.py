import pefile
import capstone
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

def find_calls(target_va):
    print(f"Searching for calls/refs to 0x{target_va:08x}:")
    # search relative call E8 xx xx xx xx
    pos = 0
    while True:
        pos = data.find(b"\xe8", pos)
        if pos == -1: break
        curr_va = file_offset_to_va(pos)
        rel = struct.unpack("<i", data[pos+1:pos+5])[0]
        dest_va = curr_va + 5 + rel
        if dest_va == target_va:
            print(f"  Call at VA: 0x{curr_va:08x}")
        pos += 1
    # search direct pointer
    target = struct.pack("<I", target_va)
    pos = 0
    while True:
        pos = data.find(target, pos)
        if pos == -1: break
        curr_va = file_offset_to_va(pos)
        print(f"  Pointer at VA: 0x{curr_va:08x}")
        pos += 4

find_calls(0x00700400)
