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

def read_u32(va):
    off = va_to_offset(va)
    if off is not None and off + 4 <= len(raw_data):
        return struct.unpack("<I", raw_data[off:off+4])[0]
    return None

# Find all RTTI type descriptors matching patterns
patterns = [
    ".?AVrd",
    ".?AVtrRoad",
    ".?AVtrTerrain",
    ".?AVtrBuilding",
    ".?AVcvMesh",
    ".?AVcvPoly",
    ".?AVrdIntersection",
    ".?AVrdCorner",
    ".?AVrdGeometry",
    ".?AVrdSpline"
]

found_classes = {}

for sec in pe.sections:
    if b".data" in sec.Name or b".rdata" in sec.Name:
        sec_data = raw_data[sec.PointerToRawData : sec.PointerToRawData + sec.SizeOfRawData]
        for pat in patterns:
            p_bytes = pat.encode('ascii')
            pos = 0
            while True:
                pos = sec_data.find(p_bytes, pos)
                if pos == -1:
                    break
                name_end = sec_data.find(b"\x00", pos)
                if name_end != -1:
                    full_name = sec_data[pos:name_end].decode('latin1', errors='ignore')
                    # Offset of TypeDescriptor is pos - 8 (in raw section)
                    td_raw_off = sec.PointerToRawData + pos - 8
                    td_rva = sec.VirtualAddress + (pos - 8)
                    td_va = image_base + td_rva
                    found_classes[full_name] = td_va
                pos += len(p_bytes)

print(f"Total matching RTTI classes found: {len(found_classes)}")
# Sort and print
for name in sorted(found_classes.keys()):
    print(f"  {name:<45} TD: 0x{found_classes[name]:08X}")
