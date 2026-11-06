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

# search for "globalwater"
pos = 0
while True:
    pos = data.find(b"globalwater", pos)
    if pos == -1: break
    gw_va = file_offset_to_va(pos)
    print(f"Found 'globalwater' string at VA: 0x{gw_va:08x}")
    # find references to this string pointer
    target = struct.pack("<I", gw_va)
    rpos = 0
    while True:
        rpos = data.find(target, rpos)
        if rpos == -1: break
        ref_va = file_offset_to_va(rpos)
        print(f"  Reference at VA: 0x{ref_va:08x}")
        # disassemble code around ref_va
        off = va_to_file_offset(ref_va - 20)
        code = data[off:off+100]
        for instr in md.disasm(code, ref_va - 20):
            print(f"    0x{instr.address:08x}: {instr.mnemonic:10s} {instr.op_str}")
        rpos += 4
    pos += 4
