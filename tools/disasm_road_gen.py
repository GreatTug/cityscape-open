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

# Function at 0x00D412E0 is rdGeometryGenerator::GenerateRoadGeometry or similar
func_va = 0x00D412E0
code = raw_data[va_to_offset(func_va) : va_to_offset(func_va) + 400]
print(f"Disassembly of function at 0x{func_va:08X}:")
for insn in md.disasm(code, func_va):
    print(f"  0x{insn.address:08X}: {insn.mnemonic:7s} {insn.op_str}")
    if insn.mnemonic in ["ret", "retn"]:
        break
