import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe
try:
    import capstone
    md = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_32)
except ImportError:
    md = None

pe = parse_pe('CityScape.exe')
data = pe['data']
va_to_off = pe['va_to_offset']

def disasm_range(start_va, length):
    off = va_to_off(start_va)
    code = data[off:off+length]
    if md:
        for ins in md.disasm(code, start_va):
            print(f"0x{ins.address:08X}: {ins.bytes.hex():20} {ins.mnemonic:8} {ins.op_str}")
    else:
        print("Capstone not installed, dumping hex:")
        print(code.hex())

print("=== Disassembly at 0x0070C380 ===")
disasm_range(0x0070C380, 100)

print("\n=== Disassembly at 0x006E4C40 ===")
disasm_range(0x006E4C40, 100)
