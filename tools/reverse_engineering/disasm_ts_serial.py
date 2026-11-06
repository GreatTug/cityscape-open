import capstone, sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
md = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_32)

def disasm(start_va, count=25):
    print(f"\n=== Disasm of 0x{start_va:08X} ===")
    start_off = pe['va_to_offset'](start_va)
    code = pe['data'][start_off:start_off+count*5]
    for insn in md.disasm(code, start_va):
        print(f"  0x{insn.address:08X}: {insn.bytes.hex():16} {insn.mnemonic:8} {insn.op_str}")

disasm(0x00741780, 25)
disasm(0x0076E0C0, 25)
