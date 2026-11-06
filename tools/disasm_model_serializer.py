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

md = Cs(CS_ARCH_X86, CS_MODE_32)

off = va_to_offset(0x0135FAE0)
print(f"Disassembly of 0x0135FAE0 (offset 0x{off:X}):")
chunk = raw_data[off : off + 512]
for insn in md.disasm(chunk, 0x0135FAE0):
    print(f"  0x{insn.address:08X}:  {insn.mnemonic:8s} {insn.op_str}")
    if insn.mnemonic in ["ret", "retn"]:
        break
