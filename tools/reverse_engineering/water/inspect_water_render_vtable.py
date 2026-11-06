import pefile
import capstone

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

def disasm_func(start_va, count=30):
    off = va_to_file_offset(start_va)
    code = data[off:off+count*5]
    print(f"\n=== Disassembly from 0x{start_va:08x} ===")
    for i, instr in enumerate(md.disasm(code, start_va)):
        if i >= count: break
        print(f"  0x{instr.address:08x}: {instr.mnemonic:10s} {instr.op_str}")

disasm_func(0x00ae4cb0, 25)
disasm_func(0x00a0bf40, 25)
disasm_func(0x00b50750, 25)
disasm_func(0x00b65260, 25)
