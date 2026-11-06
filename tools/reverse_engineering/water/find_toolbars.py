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

def read_str(va):
    off = va_to_file_offset(va)
    if off is None: return ""
    end = data.find(b"\x00", off)
    return data[off:end].decode('ascii', errors='replace')

# Look at calls to 0x655970 (ToolBar ctor) in the function
for va in range(0x006b4000, 0x006b4b00):
    off = va_to_file_offset(va)
    if off and data[off:off+5] == b"\xe8" + ((0x655970 - (va + 5)) & 0xffffffff).to_bytes(4, 'little', signed=False):
        print(f"Call to ToolBar ctor (0x655970) at VA 0x{va:08x}")
        # Disassemble 15 instructions before this call
        code = data[off-60:off+5]
        for instr in md.disasm(code, va-60):
            print(f"  0x{instr.address:08x}: {instr.mnemonic:10s} {instr.op_str}")

