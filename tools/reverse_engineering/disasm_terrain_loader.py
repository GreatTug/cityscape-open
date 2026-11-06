import capstone
import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
md = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_32)

# Disassemble from 0x1609300 to 0x1609600
start_va = 0x1609300
end_va = 0x1609600
start_off = pe['va_to_offset'](start_va)
end_off = pe['va_to_offset'](end_va)
code = pe['data'][start_off:end_off]

for insn in md.disasm(code, start_va):
    print(f"0x{insn.address:08X}: {insn.bytes.hex():16} {insn.mnemonic:8} {insn.op_str}")
