import pefile
import struct
import sys

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

rdata_sec = None
text_sec = None
for sec in pe.sections:
    name = sec.Name.rstrip(b'\x00').decode('latin-1')
    if name == '.rdata':
        rdata_sec = sec
    elif name == '.text':
        text_sec = sec

pos = 0
results = []
while True:
    pos = data.find(b".?AV", pos)
    if pos == -1: break
    end = data.find(b"@@", pos)
    if end != -1 and end - pos < 60:
        name = data[pos+4:end].decode("ascii", errors="ignore")
        if "water" in name.lower():
            td_va = file_offset_to_va(pos - 8)
            results.append((name, td_va))
    pos += 4

print(f"Found {len(results)} water classes:")
for name, td_va in results:
    # search COL in rdata
    rdata_bytes = data[rdata_sec.PointerToRawData : rdata_sec.PointerToRawData + rdata_sec.SizeOfRawData]
    col_p = 0
    target_td = struct.pack("<I", td_va)
    while True:
        col_p = rdata_bytes.find(target_td, col_p)
        if col_p == -1: break
        col_va = image_base + rdata_sec.VirtualAddress + (col_p - 12)
        col_p += 4
        # search vtable in rdata
        target_col = struct.pack("<I", col_va)
        vt_p = 0
        while True:
            vt_p = rdata_bytes.find(target_col, vt_p)
            if vt_p == -1: break
            vt_va = image_base + rdata_sec.VirtualAddress + (vt_p + 4)
            print(f"  Class {name:25s} -> vtable 0x{vt_va:08x}")
            vt_p += 4

sys.stdout.flush()
