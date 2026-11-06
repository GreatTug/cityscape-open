import pefile
import struct
import capstone

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

# In 0x006b4835: mov dword ptr [esi + 0x3c], ebx
# Let's search all occurrences of [esi + 0x3c] or [reg + 0x3c] around 0x006b4000 - 0x006b6000
md = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_32)

def search_toolbar_usage(offset_field):
    print(f"\nSearching for [esi + 0x{offset_field:x}]:")
    for va in range(0x006b4000, 0x006b8000, 1):
        off = va_to_file_offset(va)
        if off and off + 10 < len(data):
            for instr in md.disasm(data[off:off+10], va):
                if f"+ 0x{offset_field:x}" in instr.op_str:
                    print(f"  0x{instr.address:08x}: {instr.mnemonic:10s} {instr.op_str}")
                break

search_toolbar_usage(0x3c)
