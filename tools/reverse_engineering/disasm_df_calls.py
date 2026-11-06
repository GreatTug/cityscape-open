import capstone, sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
md = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_32)

def disasm_before(va, count_bytes=60):
    print(f"\n--- Disasm leading up to 0x{va:08X} ---")
    start_va = va - count_bytes
    off = pe['va_to_offset'](start_va)
    code = pe['data'][off:off+count_bytes+20]
    for insn in md.disasm(code, start_va):
        marker = "==>" if insn.address == va else "   "
        print(f"{marker} 0x{insn.address:08X}: {insn.bytes.hex():16} {insn.mnemonic:8} {insn.op_str}")

disasm_before(0x005DF28F, 50)
disasm_before(0x005DF61F, 50)
