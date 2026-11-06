import capstone, sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
md = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_32)

def disasm_func(va):
    print(f"\n=== Disasm of 0x{va:08X} ===")
    off = pe['va_to_offset'](va)
    code = pe['data'][off:off+30]
    for insn in md.disasm(code, va):
        print(f"  0x{insn.address:08X}: {insn.bytes.hex():16} {insn.mnemonic:8} {insn.op_str}")
        if insn.mnemonic in ('ret', 'retf'):
            break

disasm_func(0x005CFD70)
disasm_func(0x005CFD10)
disasm_func(0x005CFC80)
disasm_func(0x005CFC20)
disasm_func(0x005D0040)
