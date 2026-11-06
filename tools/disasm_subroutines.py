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

md = Cs(CS_ARCH_X86, CS_MODE_32)

funcs = [
    ("rdIntersection::InnerBranchConnect", 0x00C9BA60),
    ("rdIntersection::AllocFillet", 0x00CB1AE0),
    ("rdIntersection::ComputeTangentAngle", 0x00CB16A0),
    ("trRoadNode::CalcHermiteTangents", 0x00D12830),
    ("trRoadNode::RadialSortArms", 0x00C960F0),
    ("trRoadNode::SetNodeAngles", 0x00EA5040),
    ("trTerrain::BilinearGridSample", 0x00A09080),
    ("trBuilding::ExtrudeFacade", 0x00B437E0),
    ("trBuildingRoofStyle::GenerateGableOrHip", 0x00A96330),
    ("trBuildingRoofStyle::GenerateMansard", 0x00B7B330)
]

print("=== DISASSEMBLING INTERNAL NATIVE GEOMETRY SUBROUTINES ===")
for name, va in funcs:
    off = va_to_offset(va)
    print(f"\n--- {name} (VA: 0x{va:08X}, Offset: 0x{off:08X}) ---")
    if off is None:
        print("  Outside sections")
        continue
    chunk = raw_data[off : off + 512]
    count = 0
    for insn in md.disasm(chunk, va):
        print(f"  0x{insn.address:08X}:  {insn.mnemonic:8s} {insn.op_str}")
        count += 1
        if insn.mnemonic in ["ret", "retn"] and count > 8:
            break
        if count >= 35:
            print("  ...")
            break
