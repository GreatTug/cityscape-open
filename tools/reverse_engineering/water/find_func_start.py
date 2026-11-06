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

# Walk backwards from 0x006b4800 to find function prologue
func_start = None
for va in range(0x006b4800, 0x006b3000, -1):
    off = va_to_file_offset(va)
    if data[off:off+3] == b"\x55\x8b\xec" or (data[off] == 0x55 and data[off+1] == 0x8b) or (data[off-1] == 0xcc and data[off] == 0x6a) or (data[off-1] == 0xc3 and data[off] == 0x55) or (data[off-1] == 0xcc and data[off] == 0x55):
        func_start = va
        break

print(f"Function start for toolbar init: 0x{func_start:08x}")
off = va_to_file_offset(func_start)
code = data[off:off+100]
for instr in md.disasm(code, func_start):
    print(f"  0x{instr.address:08x}: {instr.mnemonic:10s} {instr.op_str}")
