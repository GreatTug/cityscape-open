import capstone
import pefile
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

# Find where "Water" toolbar is created
# In tools/disasm_toolbar_init.py, we saw 0x006b4800 was setting up Water toolbar buttons!
# Let's inspect the entire function around 0x006b4700 - 0x006b4b00
md = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_32)

def disasm_func(start_va, num_instr=120):
    off = va_to_file_offset(start_va)
    code = data[off:off+num_instr*5]
    print(f"=== Disassembly from 0x{start_va:08x} ===")
    for i, instr in enumerate(md.disasm(code, start_va)):
        if i >= num_instr:
            break
        print(f"  0x{instr.address:08x}: {instr.mnemonic:10s} {instr.op_str}")

disasm_func(0x006b4800, 100)
