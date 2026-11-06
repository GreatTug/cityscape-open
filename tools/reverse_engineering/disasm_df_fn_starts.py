import capstone, sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
md = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_32)

for target_va in [0x005DF28F, 0x005DF61F]:
    off = pe['va_to_offset'](target_va)
    fn_start = off
    while fn_start > off - 0x500:
        if data[fn_start-1] in (0xcc, 0xc3):
            break
        fn_start -= 1
    va_start = pe['offset_to_va'](fn_start)
    print(f"=== Function start: 0x{va_start:08X} for target 0x{target_va:08X} ===")
    code = data[fn_start : off + 20]
    for insn in md.disasm(code, va_start):
        marker = "==>" if insn.address == target_va else "   "
        print(f"{marker} 0x{insn.address:08X}: {insn.bytes.hex():16} {insn.mnemonic:8} {insn.op_str}")
