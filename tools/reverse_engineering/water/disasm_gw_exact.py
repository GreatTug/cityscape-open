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

def disasm_exact(start_va, count=80):
    off = va_to_file_offset(start_va)
    code = data[off:off+count*5]
    print(f"\n=================== VA 0x{start_va:08x} ===================")
    for i, instr in enumerate(md.disasm(code, start_va)):
        if i >= count:
            break
        print(f"  0x{instr.address:08x}: {instr.mnemonic:10s} {instr.op_str}")

disasm_exact(0x00700480, 80)
disasm_exact(0x00aa0b40, 80)
