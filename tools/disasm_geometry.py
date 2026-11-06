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

def disassemble_func(va, max_bytes=256):
    off = va_to_offset(va)
    if off is None:
        return f"; Invalid VA 0x{va:08X}"
    code = raw_data[off : off + max_bytes]
    lines = []
    for insn in md.disasm(code, va):
        lines.append(f"0x{insn.address:08X}:  {insn.mnemonic:7s} {insn.op_str}")
        if insn.mnemonic in ["ret", "retn"] or (insn.mnemonic == "jmp" and not insn.op_str.startswith("0x")):
            break
    return "\n".join(lines)

targets = [
    ("cvMeshGeometryNode::VFunc0", 0x0101F550),
    ("cvMeshGeometryNode::VFunc4", 0x01007BF0),
    ("cvMeshGeometryNode::VFunc5", 0x01007B80),
    ("cvMeshGeometryNode::VFunc6", 0x01007B90),
    ("cvMeshGeometryNode::VFunc7", 0x01007BA0),
    ("cvStandardExporter::ExportMesh_Collada", 0x0119BE10),
    ("cvStandardExporter::ExportMesh_FBX", 0x0115FB40),
    ("cvStandardExporter::ExportPolyhedron_Collada", 0x0119C6A0),
    ("edExportRoadAccessor::VFunc0", 0x008BFF70)
]

with open("tools/disassembled_routines.txt", "w", encoding="utf-8") as out:
    for name, va in targets:
        out.write(f"\n========================================\n")
        out.write(f"=== {name} (VA: 0x{va:08X}) ===\n")
        out.write(f"========================================\n")
        out.write(disassemble_func(va, max_bytes=300) + "\n")

print("Disassembly saved to tools/disassembled_routines.txt")
