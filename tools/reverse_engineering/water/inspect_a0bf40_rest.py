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

start_va = 0x00a0bf89
off = va_to_file_offset(start_va)
code = data[off:off+250]
for instr in md.disasm(code, start_va):
    if instr.address > 0x00a0c020: break
    st = ""
    if "0x" in instr.op_str:
        words = instr.op_str.split()
        for w in words:
            if "0x" in w:
                try:
                    val = int(w.rstrip(",]").split("0x")[1], 16)
                    s = get_str(val)
                    if s: st = f" -> \"{s}\""
                except:
                    pass
    print(f"  0x{instr.address:08x}: {instr.mnemonic:10s} {instr.op_str:30s}{st}")
