import pefile
import capstone
import struct

pe = pefile.PE("CityScape.exe")
image_base = pe.OPTIONAL_HEADER.ImageBase

with open("CityScape.exe", "rb") as f:
    data = f.read()

def va_to_file_offset(va):
    rva = va - image_base
    for sec in pe.sections:
        if sec.VirtualAddress <= rva < sec.VirtualAddress + sec.Misc_VirtualSize:
            return sec.PointerToRawData + (rva - sec.VirtualAddress)
    return None

def file_offset_to_va(offset):
    for sec in pe.sections:
        if sec.PointerToRawData <= offset < sec.PointerToRawData + sec.SizeOfRawData:
            return image_base + sec.VirtualAddress + (offset - sec.PointerToRawData)
    return offset

md = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_32)

# Find function start before 0x00aa0b40
start_va = 0x00aa0b40
# Walk backwards to find push ebp / sub esp / etc.
for va in range(0x00aa0b40, 0x00aa0a00, -1):
    off = va_to_file_offset(va)
    if data[off:off+3] == b"\x55\x8b\xec" or (data[off] == 0x55 and data[off+1] == 0x8b) or (data[off-1] == 0xcc and data[off] == 0x55) or (data[off-1] == 0xc3):
        start_va = va
        break

print(f"Function candidate start: 0x{start_va:08x}")

# Find references to start_va or 0x00aa0b40
for va in (start_va, 0x00aa0b40):
    pos = 0
    target = struct.pack("<I", va)
    while True:
        pos = data.find(target, pos)
        if pos == -1: break
        ref_va = file_offset_to_va(pos)
        print(f"Reference to 0x{va:08x} at VA: 0x{ref_va:08x}")
        pos += 4

# Disassemble from start_va
off = va_to_file_offset(start_va)
code = data[off:off+300]
print(f"\nDisassembly from 0x{start_va:08x}:")
for i, instr in enumerate(md.disasm(code, start_va)):
    if i >= 40: break
    print(f"  0x{instr.address:08x}: {instr.mnemonic:10s} {instr.op_str}")
