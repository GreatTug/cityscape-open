import pefile
import capstone
import struct

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

# In toolbar init around 0x006b4800, let's see how "Global Water" button is added
start_va = 0x006b4800
off = va_to_file_offset(start_va)
code = data[off:off+350]
print(f"=== Toolbar Init around 0x{start_va:08x} ===")
for instr in md.disasm(code, start_va):
    if instr.address > 0x006b4900: break
    print(f"  0x{instr.address:08x}: {instr.mnemonic:10s} {instr.op_str}")
