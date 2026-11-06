import capstone
import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
md = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_32)

def disasm_range(start_va, count=80):
    print(f"=== Disassembly from 0x{start_va:08X} ===")
    start_off = pe['va_to_offset'](start_va)
    code = pe['data'][start_off:start_off+count*6]
    c = 0
    for insn in md.disasm(code, start_va):
        print(f"  0x{insn.address:08X}: {insn.bytes.hex():16} {insn.mnemonic:8} {insn.op_str}")
        c += 1
        if c >= count:
            break

disasm_range(0x006F4B50, 60)
disasm_range(0x006F5930, 60)
