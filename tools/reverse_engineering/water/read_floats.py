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

def read_float(va):
    off = va_to_file_offset(va)
    return struct.unpack("<f", data[off:off+4])[0]

print("0x22cde70:", read_float(0x22cde70))
print("0x22e8e88:", read_float(0x22e8e88))
