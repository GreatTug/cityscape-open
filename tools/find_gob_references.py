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

# Find ".gob" in raw_data
pos = 0
matches = []
while True:
    pos = raw_data.find(b".gob", pos)
    if pos == -1:
        break
    matches.append(pos)
    pos += 4

print(f"Total .gob occurrences in binary: {len(matches)}")
for p in matches[:15]:
    # Check section
    for sec in pe.sections:
        if sec.PointerToRawData <= p < sec.PointerToRawData + sec.SizeOfRawData:
            rva = sec.VirtualAddress + (p - sec.PointerToRawData)
            va = image_base + rva
            context = raw_data[max(0, p - 16) : min(len(raw_data), p + 24)]
            print(f"Offset 0x{p:06X} (VA 0x{va:08X}, Section {sec.Name.decode().strip()}): {context}")
            break
