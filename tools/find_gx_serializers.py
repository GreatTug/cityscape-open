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

def find_type_descriptor(class_pattern):
    pat = f".?AV{class_pattern}".encode('ascii')
    results = []
    pos = 0
    while True:
        pos = raw_data.find(pat, pos)
        if pos == -1:
            break
        name_end = raw_data.find(b"\x00", pos)
        name = raw_data[pos:name_end].decode('latin1', errors='ignore')
        td_offset = pos - 8
        for sec in pe.sections:
            if sec.PointerToRawData <= td_offset < sec.PointerToRawData + sec.SizeOfRawData:
                rva = sec.VirtualAddress + (td_offset - sec.PointerToRawData)
                td_va = image_base + rva
                results.append((name, td_va))
                break
        pos += len(pat)
    return results

targets = ["gxModel", "gxModelSerializerPC", "gxVertexBufferSerializerPC", "gxIndexBufferSerializerPC"]
for t in targets:
    tds = find_type_descriptor(t)
    for name, td_va in tds:
        print(f"Class {name:<35} TD: 0x{td_va:08X}")
