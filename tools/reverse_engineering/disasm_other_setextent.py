import capstone, sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
md = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_32)

def disasm_call(va):
    print(f"\n=== Call at 0x{va:08X} ===")
    start_va = va - 20
    off = pe['va_to_offset'](start_va)
    code = pe['data'][off:off+40]
    for insn in md.disasm(code, start_va):
        marker = "==>" if insn.address == va else "   "
        print(f"{marker} 0x{insn.address:08X}: {insn.bytes.hex():16} {insn.mnemonic:8} {insn.op_str}")

disasm_call(0x0046664A)
disasm_call(0x00AE3A7B)
disasm_call(0x00B85D7F)
