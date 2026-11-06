import capstone
import pefile

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

md = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_32)

def disasm_func_from_prologue(target_va):
    off = va_to_file_offset(target_va)
    # Search backwards for push ebp / mov ebp, esp or sub esp, ...
    curr = off
    while curr > off - 500:
        if data[curr:curr+3] == b"\x55\x8b\xec": # push ebp; mov ebp, esp
            break
        if data[curr:curr+2] == b"\x83\xec" and data[curr-1] in (0x55, 0x56, 0x57, 0x53):
            break
        curr -= 1
    start_va = pe.OPTIONAL_HEADER.ImageBase + pe.sections[0].VirtualAddress + (curr - pe.sections[0].PointerToRawData)
    print(f"\nDisassembling function around 0x{target_va:08x} (found start at ~0x{start_va:08x}):")
    code = data[curr:curr + 300]
    for instr in md.disasm(code, start_va):
        marker = "===>" if instr.address == target_va else "    "
        print(f"{marker} 0x{instr.address:08x}: {instr.mnemonic:10s} {instr.op_str}")

disasm_func_from_prologue(0x007004c5)
disasm_func_from_prologue(0x00aa0b67)
disasm_func_from_prologue(0x00713423)
