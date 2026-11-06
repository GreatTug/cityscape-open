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
    return "".join(s) if len(s) >= 2 else None

md = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_32)

start_va = 0x007009e0
off = va_to_file_offset(start_va)
code = data[off:off+2500]

print("=== Strings in 0x007009e0-0x00701000 ===")
for instr in md.disasm(code, start_va):
    if instr.address > 0x00701000: break
    hexes = re.findall(r"0x[0-9a-fA-F]+", instr.op_str)
    for h in hexes:
        val = int(h, 16)
        st = get_str(val)
        if st and len(st) > 1:
            print(f"  0x{instr.address:08x}: {instr.mnemonic:8s} {instr.op_str:35s} -> \"{st}\"")
