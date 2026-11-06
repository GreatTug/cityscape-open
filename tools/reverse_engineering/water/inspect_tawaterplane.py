import pefile
import struct
import capstone

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

# Find .?AVtaWaterPlane@@
target = b".?AVtaWaterPlane@@"
pos = data.find(target)
td_va = file_offset_to_va(pos - 8)
print(f"taWaterPlane TypeDescriptor VA: 0x{td_va:08x}")

col_pos = data.find(struct.pack("<I", td_va))
while col_pos != -1:
    col_va = file_offset_to_va(col_pos - 12)
    vt_pos = data.find(struct.pack("<I", col_va))
    if vt_pos != -1:
        vtable_va = file_offset_to_va(vt_pos + 4)
        print(f"Found vtable for taWaterPlane at VA: 0x{vtable_va:08x}")
        # Disassemble constructor/destructor and methods
        off = va_to_file_offset(vtable_va)
        for i in range(15):
            fn_va = struct.unpack("<I", data[off+i*4:off+i*4+4])[0]
            print(f"  vtable[{i}]: 0x{fn_va:08x}")
    col_pos = data.find(struct.pack("<I", td_va), col_pos + 4)
