import capstone, sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
md = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_32)

off = pe['va_to_offset'](0x0046664A)
fn_start = off
while fn_start > off - 0x100:
    if data[fn_start-1] in (0xcc, 0xc3) or (data[fn_start-3] == 0xc2 and data[fn_start-1] == 0xcc):
        break
    fn_start -= 1
va_start = pe['offset_to_va'](fn_start)
print(f"Function start: 0x{va_start:08X}")

code = data[fn_start:off+20]
for insn in md.disasm(code, va_start):
    print(f"0x{insn.address:08X}: {insn.bytes.hex():16} {insn.mnemonic:8} {insn.op_str}")
