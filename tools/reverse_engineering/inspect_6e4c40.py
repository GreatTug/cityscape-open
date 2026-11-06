import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

# Dump 0x006E4C40 for 250 bytes
off = va_to_off(0x006E4C40)
chunk = data[off:off+250]

print("Length:", len(chunk))
# We will write a small disassembler loop in python for basic x86 instructions
# or use a python library if available.
# Let's inspect calls and memory accesses inside 0x006E4C40:
i = 0
while i < len(chunk):
    b = chunk[i]
    va = 0x006E4C40 + i
    if b == 0xE8: # call
        import struct
        rel = struct.unpack("<i", chunk[i+1:i+5])[0]
        tgt = va + 5 + rel
        print(f"0x{va:08X}: CALL 0x{tgt:08X}")
        i += 5
    elif b == 0xE9: # jmp
        import struct
        rel = struct.unpack("<i", chunk[i+1:i+5])[0]
        tgt = va + 5 + rel
        print(f"0x{va:08X}: JMP 0x{tgt:08X}")
        i += 5
    elif b == 0xC3: # ret
        print(f"0x{va:08X}: RET")
        i += 1
    elif b == 0xC2: # ret imm16
        imm = int.from_bytes(chunk[i+1:i+3], 'little')
        print(f"0x{va:08X}: RET {imm}")
        i += 3
    else:
        i += 1
