import pefile
from capstone import Cs, CS_ARCH_X86, CS_MODE_32

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

md = Cs(CS_ARCH_X86, CS_MODE_32)

def disassemble_func(va, max_bytes=128):
    off = va_to_offset(va)
    if off is None:
        return f"; Invalid VA 0x{va:08X}"
    code = raw_data[off : off + max_bytes]
    lines = []
    for insn in md.disasm(code, va):
        lines.append(f"0x{insn.address:08X}:  {insn.mnemonic:7s} {insn.op_str}")
        if insn.mnemonic in ["ret", "retn"]:
            break
    return "\n".join(lines)

targets = [
    ("cvPolyhedronGeometryNode::VFunc3", 0x00FF9580),
    ("cvPolyhedronGeometryNode::VFunc4", 0x01001F00),
    ("cvPolyhedronGeometryNode::VFunc5", 0x00FF9590),
    ("cvPolyhedronGeometryNode::VFunc6", 0x00FF95A0),
    ("cvPolyhedronGeometryNode::VFunc7", 0x01001FA0),
    ("cvStandardExporter::ExportBuilding_Collada", 0x01190540),
    ("cvStandardExporter::ExportProps_Collada", 0x011992F0)
]

for name, va in targets:
    print(f"\n=== {name} (0x{va:08X}) ===")
    print(disassemble_func(va))
