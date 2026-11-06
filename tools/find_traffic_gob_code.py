import struct
import pefile
from capstone import Cs, CS_ARCH_X86, CS_MODE_32

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

target_va = 0x0234FA8D
target_bytes = struct.pack("<I", target_va)

pos = 0
found = []
while True:
    pos = raw_data.find(target_bytes, pos)
    if pos == -1:
        break
    found.append(pos)
    pos += 4

print(f"References to 0x{target_va:08X}: {len(found)}")
for p in found:
    for sec in pe.sections:
        if sec.PointerToRawData <= p < sec.PointerToRawData + sec.SizeOfRawData:
            rva = sec.VirtualAddress + (p - sec.PointerToRawData)
            va = image_base + rva
            print(f"  At VA 0x{va:08X} in section {sec.Name.decode().strip()}")
            break
