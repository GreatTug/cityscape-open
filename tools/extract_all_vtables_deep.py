import struct
import pefile
from capstone import Cs, CS_ARCH_X86, CS_MODE_32

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

md = Cs(CS_ARCH_X86, CS_MODE_32)

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
                        # Now find vtable pointing to col_va
                        col_bytes = struct.pack("<I", col_va)
                        vt_pos = sec_data.find(col_bytes)
                        if vt_pos != -1:
                            vt_va = image_base + sec.VirtualAddress + vt_pos + 4
                            return col_va, vt_va
                pos += 4
    return None, None

def get_vtable_methods(vtable_va, max_methods=50):
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

key_classes = [
    (".?AVrdIntersection@@", 0x02FF7600),
    (".?AVrdIntersectionTessellator@@", 0x02FF436C),
    (".?AVrdCornerCurveBorder@@", 0x02FF4348),
    (".?AVrdIntersectionCurve@@", 0x02FF4B9C),
    (".?AVrdCuldesacInfo@@", 0x02FF4434),
    (".?AVrdLaneTessellator@@", 0x02FF44E0),
    (".?AVrdSidewalkTessellator@@", 0x02FF846C),
    (".?AVrdRegionTessellator@@", 0x02FF4164),
    (".?AVrdPolygonRegionTessellator@@", 0x02FF42DC),
    (".?AVrdNodeExtruder@@", 0x03000090),
    (".?AVrdSegmentExtruder@@", 0x02FFEFB4),
    (".?AVrdMergeSplitRegionBuilder@@", 0x03004FCC),
    (".?AVtrRoadSegment@@", 0x02F0F4FC),
    (".?AVtrRoadNode@@", 0x02F0EDE0),
    (".?AVtrRoadNetwork@@", 0x02F0FA3C),
    (".?AVtrRoadStyle@@", 0x02F0EA48),
    (".?AVtrRoadBlock@@", 0x02FF4638),
    (".?AVtrTerrain@@", 0x02F0ACA0),
    (".?AVtrTerrainSettings@@", 0x02F0C39C),
    (".?AVtrBuilding@@", 0x02F0BC1C),
    (".?AVtrBuildingRoofStyle@@", 0x02EFEC30),
    (".?AVtrBuildingStyle@@", 0x02EE36C8),
    (".?AVcvMeshGeometryNode@@", 0x0304EF10),
    (".?AVcvPolygonDataNode@@", 0x0304ED58)
]

report_lines = []
report_lines.append("================================================================================")
report_lines.append("  PIXELACTIVE CITYSCAPE 1.8 NATIVE VTABLE & METHOD RECOVERY REPORT")
report_lines.append("================================================================================\n")

all_methods_to_disasm = []

for name, td_va in key_classes:
    col_va, vt_va = find_col_and_vtable(td_va)
    report_lines.append(f"Class: {name}")
    report_lines.append(f"  TypeDescriptor: 0x{td_va:08X}")
    if vt_va:
        methods = get_vtable_methods(vt_va)
        report_lines.append(f"  COL:            0x{col_va:08X}")
        report_lines.append(f"  VTable:         0x{vt_va:08X} ({len(methods)} methods)")
        for idx, m in enumerate(methods):
            report_lines.append(f"    [{idx:2d}] 0x{m:08X}")
            all_methods_to_disasm.append((f"{name}::VFunc_{idx}", m))
    else:
        report_lines.append("  VTable:         [Not Found / Abstract / Inherited]")
    report_lines.append("")

output_file = "tools/vtable_recovery_deep.txt"
with open(output_file, "w", encoding="utf-8") as f:
    f.write("\n".join(report_lines))

print(f"Report written to {output_file}. Found {len(all_methods_to_disasm)} virtual methods across key classes.")
