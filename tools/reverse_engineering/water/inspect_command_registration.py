import pefile
import capstone
import re

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

def get_str(va):
    off = va_to_file_offset(va)
    if off is None or off >= len(data): return None
    s = []
    for b in data[off:off+64]:
        if b == 0: break
        if 32 <= b <= 126:
            s.append(chr(b))
        else:
            return None
    return "".join(s) if len(s) >= 1 else None

md = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_32)

start_va = 0x1550a00
off = va_to_file_offset(start_va)
code = data[off:off+2000]

print("=== Strings in Command Registration 0x1550a00-0x1551200 ===")
for instr in md.disasm(code, start_va):
    if instr.address > 0x1551200: break
    for h in re.findall(r"0x[0-9a-fA-F]+", instr.op_str):
        val = int(h, 16)
        st = get_str(val)
        if st and len(st) > 2:
            print(f"  0x{instr.address:08x}: {instr.mnemonic:8s} {instr.op_str:30s} -> \"{st}\"")
