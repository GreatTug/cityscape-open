import pefile
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

off = va_to_file_offset(0x00a0ba00)
# search backwards for function prologue
curr = off
while curr > off - 500:
    if data[curr:curr+3] == b"\x55\x8b\xec": # push ebp; mov ebp, esp
        break
    if data[curr] == 0x55 and data[curr+1:curr+3] == b"\x83\xec":
        break
    curr -= 1

fn_start_va = file_offset_to_va(curr)
print(f"Function start VA: 0x{fn_start_va:08x}")

# Find callers of fn_start_va
target_bytes = (fn_start_va - image_base).to_bytes(4, 'little')
for i in range(len(data)-5):
    if data[i] == 0xe8:
        rel = int.from_bytes(data[i+1:i+5], 'little', signed=True)
        call_va = file_offset_to_va(i)
        dest_va = call_va + 5 + rel
        if dest_va == fn_start_va:
            print(f"Direct call from 0x{call_va:08x}")
