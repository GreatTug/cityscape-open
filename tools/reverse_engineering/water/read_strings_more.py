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

def read_str_at(va):
    off = va_to_file_offset(va)
    if off is None:
        return "<invalid va>"
    end = data.find(b"\x00", off)
    if end == -1:
        end = off + 100
    return data[off:end].decode('ascii', errors='replace')

print("0x022ca5d4:", read_str_at(0x022ca5d4))
print("0x022e5940:", read_str_at(0x022e5940))
print("0x022eed0c:", read_str_at(0x022eed0c))
