import capstone
import pefile

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

md = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_32)

def disasm_at(va, length=64):
    off = va_to_file_offset(va)
    if off is None:
        print(f"Could not convert VA 0x{va:08x}")
        return
    code = data[off:off+length]
    print(f"\nDisassembly at VA: 0x{va:08x} (file offset: 0x{off:08x}):")
    for instr in md.disasm(code, va):
        print(f"  0x{instr.address:08x}: {instr.mnemonic:10s} {instr.op_str}")

disasm_at(0x006b4800, 100)
disasm_at(0x007004a0, 100)
disasm_at(0x00aa0b50, 100)
disasm_at(0x00713400, 100)
disasm_at(0x00713b80, 100)
