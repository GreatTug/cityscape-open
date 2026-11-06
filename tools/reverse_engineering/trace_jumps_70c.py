import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

# Trace from 0x0070C200 to 0x0070C340
# Decode instructions and jump targets
off_start = va_to_off(0x0070C200)

i = 0
while i < 0x140:
    va = 0x0070C200 + i
    off = off_start + i
    b = data[off]
    
    # Check conditional jumps
    if b == 0x74: # je rel8
        rel = int.from_bytes(data[off+1:off+2], 'little', signed=True)
        tgt = va + 2 + rel
        print(f"0x{va:08X}: JE  0x{tgt:08X}")
        i += 2
    elif b == 0x75: # jne rel8
        rel = int.from_bytes(data[off+1:off+2], 'little', signed=True)
        tgt = va + 2 + rel
        print(f"0x{va:08X}: JNE 0x{tgt:08X}")
        i += 2
    elif b == 0xEB: # jmp rel8
        rel = int.from_bytes(data[off+1:off+2], 'little', signed=True)
        tgt = va + 2 + rel
        print(f"0x{va:08X}: JMP 0x{tgt:08X}")
        i += 2
    elif b == 0x0F and data[off+1] in (0x84, 0x85, 0x86, 0x87, 0x82, 0x83): # jcc rel32
        rel = int.from_bytes(data[off+2:off+6], 'little', signed=True)
        tgt = va + 6 + rel
        jname = {0x84:'JE', 0x85:'JNE', 0x86:'JBE', 0x87:'JA', 0x82:'JB', 0x83:'JAE'}[data[off+1]]
        print(f"0x{va:08X}: {jname} 0x{tgt:08X}")
        i += 6
    elif b == 0xE8: # call rel32
        rel = int.from_bytes(data[off+1:off+5], 'little', signed=True)
        tgt = va + 5 + rel
        print(f"0x{va:08X}: CALL 0x{tgt:08X}")
        i += 5
    elif b == 0xE9: # jmp rel32
        rel = int.from_bytes(data[off+1:off+5], 'little', signed=True)
        tgt = va + 5 + rel
        print(f"0x{va:08X}: JMP 0x{tgt:08X}")
        i += 5
    else:
        i += 1
