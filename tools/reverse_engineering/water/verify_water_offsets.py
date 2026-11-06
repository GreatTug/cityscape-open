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

targets = [0x00b5a203, 0x00b61e8d, 0x00b64f22]
for va in targets:
    off = va_to_file_offset(va)
    bs = data[off:off+4]
    print(f"VA: 0x{va:08x} -> FileOffset: 0x{off:08x} -> Bytes: {bs.hex()} (expected: c6465000)")
