import pefile

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

menu_names = [b"&File", b"&Edit", b"&View", b"&Tools", b"&Layout", b"&Help"]
for m in menu_names:
    p = 0
    cnt = 0
    while True:
        p = data.find(m + b"\x00", p)
        if p == -1: break
        cnt += 1
        p += len(m) + 1
    print(f"Menu string {m.decode('latin-1')}: found {cnt} occurrences")
