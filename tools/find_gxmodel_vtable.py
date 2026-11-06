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

def find_col_and_vtable(td_va):
    td_bytes = struct.pack("<I", td_va)
    for sec in pe.sections:
        if b".rdata" in sec.Name:
            sec_data = raw_data[sec.PointerToRawData : sec.PointerToRawData + sec.SizeOfRawData]
            pos = 0
            while True:
                pos = sec_data.find(td_bytes, pos)
                if pos == -1:
                    break
                col_offset = pos - 12
                if col_offset >= 0:
                    sig = struct.unpack("<I", sec_data[col_offset:col_offset+4])[0]
                    if sig == 0:
                        col_va = image_base + sec.VirtualAddress + col_offset
                        col_bytes = struct.pack("<I", col_va)
                        vt_pos = sec_data.find(col_bytes)
                        if vt_pos != -1:
                            vt_va = image_base + sec.VirtualAddress + vt_pos + 4
                            return col_va, vt_va
                pos += 4
    return None, None

def read_u32(va):
    off = va_to_offset(va)
    if off is not None and off + 4 <= len(raw_data):
        return struct.unpack("<I", raw_data[off:off+4])[0]
    return None

col, vt = find_col_and_vtable(0x02F392A0) # gxModel
print(f"gxModel COL: 0x{col:08X}, VTable: 0x{vt:08X}")
if vt:
    for i in range(25):
        fn = read_u32(vt + i * 4)
        print(f"  [{i:2d}] 0x{fn:08X}")
