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

vt_va = 0x022e60dc
off = va_to_file_offset(vt_va)
print(f"=== edWaterMode vtable at 0x{vt_va:08x} ===")
for i in range(25):
    fn_va = struct.unpack("<I", data[off+i*4:off+i*4+4])[0]
    print(f"  vtable[{i}]: 0x{fn_va:08x}")
    # disassemble first 3 instructions
    foff = va_to_file_offset(fn_va)
    if foff:
        for instr in md.disasm(data[foff:foff+20], fn_va):
            st = ""
            for w in instr.op_str.split():
                if "0x" in w:
                    try:
                        val = int(w.rstrip(",]").split("0x")[1], 16)
                        s = get_str(val)
                        if s: st = f" -> \"{s}\""
                    except: pass
            print(f"     0x{instr.address:08x}: {instr.mnemonic:10s} {instr.op_str:25s}{st}")
            break
