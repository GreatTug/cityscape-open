import capstone, sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
md = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_32)

start_va = 0x004EF150
start_off = pe['va_to_offset'](start_va)
code = pe['data'][start_off:start_off+200]

for insn in md.disasm(code, start_va):
    print(f"0x{insn.address:08X}: {insn.bytes.hex():16} {insn.mnemonic:8} {insn.op_str}")
    if insn.mnemonic == 'ret' or insn.mnemonic == 'retf':
        break
