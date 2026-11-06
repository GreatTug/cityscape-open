import capstone, sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
md = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_32)

def disasm(va, count=15):
    print(f"\n--- Disasm at 0x{va:08X} ---")
    off = pe['va_to_offset'](va - 20)
    code = pe['data'][off:off+80]
    for insn in md.disasm(code, va - 20):
        marker = "==>" if insn.address == va else "   "
        print(f"{marker} 0x{insn.address:08X}: {insn.bytes.hex():16} {insn.mnemonic:8} {insn.op_str}")

disasm(0x005DEBEF)
disasm(0x005DF28F)
disasm(0x005DF61F)
disasm(0x0096127C)
