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

def find_type_descriptor(class_pattern):
    # Search for .?AV<pattern>
    pat = f".?AV{class_pattern}".encode('ascii')
    results = []
    pos = 0
    while True:
        pos = raw_data.find(pat, pos)
        if pos == -1:
            break
        # Read full type name
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

def find_col(td_va):
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
                        return image_base + sec.VirtualAddress + col_offset
                pos += 4
    return None

def find_vtable(col_va):
    col_bytes = struct.pack("<I", col_va)
    for sec in pe.sections:
        if b".rdata" in sec.Name:
            sec_data = raw_data[sec.PointerToRawData : sec.PointerToRawData + sec.SizeOfRawData]
            pos = 0
            while True:
                pos = sec_data.find(col_bytes, pos)
                if pos == -1:
                    break
                return image_base + sec.VirtualAddress + pos + 4
    return None

def get_vtable_methods(vtable_va, max_methods=30):
    methods = []
    curr = vtable_va
    for _ in range(max_methods):
        fn_va = read_u32(curr)
        if fn_va is None:
            break
        is_code = False
        for sec in pe.sections:
            if b".text" in sec.Name and (image_base + sec.VirtualAddress <= fn_va < image_base + sec.VirtualAddress + sec.Misc_VirtualSize):
                is_code = True
                break
        if not is_code:
            break
        methods.append(fn_va)
        curr += 4
    return methods

search_targets = [
    "rdGeometryGenerator",
    "rdIntersection",
    "rdCornerCurveBorder",
    "trRoad",
    "trRoadSegment",
    "trRoadNode",
    "trTerrain",
    "trBuilding"
]

print("=== RECOVERING ROAD & INTERSECTION VTABLES ===")
for target in search_targets:
    tds = find_type_descriptor(target)
    for name, td_va in tds[:4]:
        col_va = find_col(td_va)
        if not col_va:
            continue
        vtable_va = find_vtable(col_va)
        if not vtable_va:
            continue
        methods = get_vtable_methods(vtable_va)
        print(f"\n[+] Class {name}:")
        print(f"    TypeDescriptor: 0x{td_va:08X}")
        print(f"    COL:            0x{col_va:08X}")
        print(f"    VTable:         0x{vtable_va:08X} ({len(methods)} virtual methods)")
        for idx, m in enumerate(methods[:8]):
            print(f"      [{idx:2d}] 0x{m:08X}")
