import pefile
import re

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

# search for strings like "runcommandscript"
pos = data.find(b"runcommandscript")
print("pos of runcommandscript:", pos)

# search for "water" commands or strings
words = re.findall(b"[a-zA-Z0-9_]{3,30}", data)
water_words = set(w.decode('latin-1') for w in words if b"water" in w.lower())
print("Water words found:", len(water_words))
for w in sorted(water_words):
    if len(w) > 4:
        print(" ", w)
