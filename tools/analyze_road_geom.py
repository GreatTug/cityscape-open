import pefile
from capstone import Cs, CS_ARCH_X86, CS_MODE_32

pe = pefile.PE("CityScape.exe")
image_base = pe.OPTIONAL_HEADER.ImageBase

with open("CityScape.exe", "rb") as f:
    raw_data = f.read()

def va_to_offset(va):
    rva = va - image_base
    for sec in pe.sections:
        if sec.VirtualAddress <= rva < sec.VirtualAddress + sec.Misc_VirtualSize:
            return sec.PointerToRawData + (rva - sec.VirtualAddress)
    return None

# Find references to string "rdGeometryGenerator"
target_str_pos = raw_data.find(b"rdGeometryGenerator::RoadNetwork_Modified")
print("RoadNetwork_Modified string at file offset:", hex(target_str_pos))

# In .rdata, find VA of string
rdata_sec = None
for s in pe.sections:
    if s.PointerToRawData <= target_str_pos < s.PointerToRawData + s.SizeOfRawData:
        rdata_sec = s
        break

str_va = image_base + rdata_sec.VirtualAddress + (target_str_pos - rdata_sec.PointerToRawData)
print("String VA:", hex(str_va))

# Now search .text for references (push str_va)
import struct
str_va_bytes = struct.pack("<I", str_va)

text_sec = None
for s in pe.sections:
    if b".text" in s.Name:
        text_sec = s
        break

text_data = raw_data[text_sec.PointerToRawData : text_sec.PointerToRawData + text_sec.SizeOfRawData]
pos = 0
refs = []
while True:
    pos = text_data.find(str_va_bytes, pos)
    if pos == -1:
        break
    ref_va = image_base + text_sec.VirtualAddress + pos
    refs.append(ref_va)
    pos += 4

print(f"References found: {len(refs)}")
for r in refs:
    print(f"  Reference at VA: 0x{r:08X}")

md = Cs(CS_ARCH_X86, CS_MODE_32)
# Disassemble around first reference
if refs:
    func_va = refs[0] - 100
    code = raw_data[va_to_offset(func_va) : va_to_offset(func_va) + 200]
    print(f"\nDisassembly around 0x{refs[0]:08X}:")
    for insn in md.disasm(code, func_va):
        print(f"  0x{insn.address:08X}: {insn.mnemonic:7s} {insn.op_str}")
