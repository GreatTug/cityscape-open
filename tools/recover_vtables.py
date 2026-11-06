import struct
import pefile

pe = pefile.PE("CityScape.exe")
image_base = pe.OPTIONAL_HEADER.ImageBase

with open("CityScape.exe", "rb") as f:
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

# Find TypeDescriptor for a given class name
def find_type_descriptor(class_name):
    target = f".?AV{class_name}@@".encode('ascii')
    pos = raw_data.find(target)
    if pos != -1:
        # TypeDescriptor starts 8 bytes before name (vftable ptr + spare)
        td_offset = pos - 8
        for sec in pe.sections:
            if sec.PointerToRawData <= td_offset < sec.PointerToRawData + sec.SizeOfRawData:
                rva = sec.VirtualAddress + (td_offset - sec.PointerToRawData)
                return image_base + rva
    return None

# Find RTTI Complete Object Locator pointing to this TypeDescriptor
def find_col(td_va):
    td_bytes = struct.pack("<I", td_va)
    # CompleteObjectLocator has signature 0, offset 0, cdOffset 0, pTypeDescriptor, pClassDescriptor
    # Look for td_bytes in .rdata
    for sec in pe.sections:
        if b".rdata" in sec.Name:
            sec_data = raw_data[sec.PointerToRawData : sec.PointerToRawData + sec.SizeOfRawData]
            pos = 0
            while True:
                pos = sec_data.find(td_bytes, pos)
                if pos == -1:
                    break
                # In COL, pTypeDescriptor is at offset 12 (0x0C)
                col_offset = pos - 12
                if col_offset >= 0:
                    col_va = image_base + sec.VirtualAddress + col_offset
                    # Check signature == 0
                    sig = struct.unpack("<I", sec_data[col_offset:col_offset+4])[0]
                    if sig == 0:
                        return col_va
                pos += 4
    return None

# Find VTable pointing to COL
def find_vtable(col_va):
    col_bytes = struct.pack("<I", col_va)
    # The vtable has a pointer to COL at index -1, then virtual function pointers follow
    for sec in pe.sections:
        if b".rdata" in sec.Name:
            sec_data = raw_data[sec.PointerToRawData : sec.PointerToRawData + sec.SizeOfRawData]
            pos = 0
            while True:
                pos = sec_data.find(col_bytes, pos)
                if pos == -1:
                    break
                vtable_va = image_base + sec.VirtualAddress + pos + 4
                return vtable_va
    return None

def get_vtable_methods(vtable_va, max_methods=50):
    methods = []
    curr = vtable_va
    for _ in range(max_methods):
        fn_va = read_u32(curr)
        if fn_va is None:
            break
        # Check if fn_va points into .text section
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

target_classes = [
    "cvGeometryNode",
    "cvMeshGeometryNode",
    "cvPolyhedronGeometryNode",
    "cvStandardExporter",
    "cvExporter",
    "coColladaExport",
    "fbExporterFbx",
    "ofOpenFlightExport",
    "trTerrainExporter",
    "edTrafficExporter",
    "edExportRoadAccessor",
    "edExportSegment",
    "edExportNode",
    "gxVertexBuffer",
    "gxIndexBuffer"
]

with open("tools/vtable_recovery.txt", "w", encoding="utf-8") as out:
    for cls in target_classes:
        td_va = find_type_descriptor(cls)
        if not td_va:
            out.write(f"[-] Class {cls}: TypeDescriptor not found\n")
            continue
        col_va = find_col(td_va)
        if not col_va:
            out.write(f"[-] Class {cls}: COL not found (TD: 0x{td_va:08X})\n")
            continue
        vt_va = find_vtable(col_va)
        if not vt_va:
            out.write(f"[-] Class {cls}: VTable not found (COL: 0x{col_va:08X})\n")
            continue
        methods = get_vtable_methods(vt_va)
        out.write(f"[+] Class {cls}:\n")
        out.write(f"    TypeDescriptor: 0x{td_va:08X}\n")
        out.write(f"    COL:            0x{col_va:08X}\n")
        out.write(f"    VTable:         0x{vt_va:08X}\n")
        out.write(f"    Methods ({len(methods)}):\n")
        for i, m in enumerate(methods):
            out.write(f"      [{i:2d}] 0x{m:08X}\n")
        out.write("\n")

print("Vtable recovery completed. Results in tools/vtable_recovery.txt")
