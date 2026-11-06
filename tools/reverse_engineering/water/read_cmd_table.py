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

def get_str(va):
    off = va_to_file_offset(va)
    if off is None or off >= len(data): return None
    s = []
    for b in data[off:off+64]:
        if b == 0: break
        if 32 <= b <= 126:
            s.append(chr(b))
        else:
            return None
    return "".join(s) if len(s) >= 1 else None

off = va_to_file_offset(0x02ee25c0)
print("Entries around 0x02ee25c0:")
for i in range(-10, 10):
    entry_va = 0x02ee25c0 + i*16
    eoff = va_to_file_offset(entry_va)
    words = [struct.unpack("<I", data[eoff+k*4:eoff+k*4+4])[0] for k in range(4)]
    s1 = get_str(words[0])
    s2 = get_str(words[1])
    print(f"  0x{entry_va:08x}: [0x{words[0]:08x} '{s1}', 0x{words[1]:08x} '{s2}', 0x{words[2]:08x}, 0x{words[3]:08x}]")
