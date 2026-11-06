import pefile
import capstone
import struct
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

start_va = 0x006b4400
off = va_to_file_offset(start_va)
code = data[off:off+3500]

print("=== All Toolbars in 0x006b4400-0x006b5200 ===")
for instr in md.disasm(code, start_va):
    if instr.address > 0x006b5200: break
    if instr.mnemonic == "call" and ("0x655970" in instr.op_str or "0x655a10" in instr.op_str):
        print(f"\n--- Call at 0x{instr.address:08x}: {instr.op_str} ---")
    hexes = re.findall(r"0x[0-9a-fA-F]+", instr.op_str)
    for h in hexes:
        val = int(h, 16)
        st = get_str(val)
        if st and len(st) > 1 and not st.startswith("$Dll:"):
            print(f"  0x{instr.address:08x}: {instr.mnemonic:8s} {instr.op_str:35s} -> \"{st}\"")
