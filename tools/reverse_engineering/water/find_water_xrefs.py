import struct
import pefile

pe = pefile.PE("CityScape.exe")
image_base = pe.OPTIONAL_HEADER.ImageBase
print(f"ImageBase: 0x{image_base:08x}")

with open("CityScape.exe", "rb") as f:
    data = f.read()

def find_xrefs(target_va):
    target_bytes = struct.pack("<I", target_va)
    idx = 0
    while True:
        pos = data.find(target_bytes, idx)
        if pos == -1:
            break
        yield pos
        idx = pos + 4

def file_offset_to_va(offset):
    for sec in pe.sections:
        if sec.PointerToRawData <= offset < sec.PointerToRawData + sec.SizeOfRawData:
            return image_base + sec.VirtualAddress + (offset - sec.PointerToRawData)
    return offset

def va_to_file_offset(va):
    rva = va - image_base
    for sec in pe.sections:
        if sec.VirtualAddress <= rva < sec.VirtualAddress + sec.Misc_VirtualSize:
            return sec.PointerToRawData + (rva - sec.VirtualAddress)
    return None

targets = [
    (0x01eea354, "Global Water"),
    (0x01eea364, "&Water"),
    (0x01eee1f0, "Global Water Plane"),
    (0x01ec860e, "globalwater"),
    (0x01ef8c1e, "DataWaterPlaneChanged"),
    (0x01ef8c36, "DataWaterPlaneHeightChanged")
]

for off, name in targets:
    va = file_offset_to_va(off)
    print(f"\n--- Targets for '{name}' at file offset 0x{off:08x} (VA: 0x{va:08x}) ---")
    xrefs = list(find_xrefs(va))
    print(f"Found {len(xrefs)} xrefs:")
    for xref in xrefs:
        xref_va = file_offset_to_va(xref)
        print(f"  file offset 0x{xref:08x} (VA: 0x{xref_va:08x})")
