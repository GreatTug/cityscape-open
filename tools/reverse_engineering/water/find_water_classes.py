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

def file_offset_to_va(offset):
    for sec in pe.sections:
        if sec.PointerToRawData <= offset < sec.PointerToRawData + sec.SizeOfRawData:
            return image_base + sec.VirtualAddress + (offset - sec.PointerToRawData)
    return offset

pos = 0
tds = []
while True:
    pos = data.find(b".?AV", pos)
    if pos == -1: break
    end = data.find(b"@@", pos)
    if end != -1 and end - pos < 60:
        name = data[pos+4:end].decode("ascii", errors="ignore")
        if "water" in name.lower():
            td_va = file_offset_to_va(pos - 8)
            tds.append((name, td_va))
    pos += 4

print(f"Found {len(tds)} water classes:")
for name, td_va in tds:
    print(f"\nClass: {name} (TD VA: 0x{td_va:08x})")
    col_pos = 0
    while True:
        col_pos = data.find(struct.pack("<I", td_va), col_pos)
        if col_pos == -1: break
        col_va = file_offset_to_va(col_pos - 12)
        col_pos += 4
        
        vt_pos = 0
        while True:
            vt_pos = data.find(struct.pack("<I", col_va), vt_pos)
            if vt_pos == -1: break
            vt_va = file_offset_to_va(vt_pos + 4)
            print(f"  vtable VA: 0x{vt_va:08x}")
            vt_pos += 4
            
            ref_pos = 0
            while True:
                ref_pos = data.find(struct.pack("<I", vt_va), ref_pos)
                if ref_pos == -1: break
                ref_va = file_offset_to_va(ref_pos)
                print(f"    vtable ref at VA: 0x{ref_va:08x}")
                ref_pos += 4
