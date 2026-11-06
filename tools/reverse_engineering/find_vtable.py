import sys, struct
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

# In constructor 0x005DEBC0, let's find the vtable store (mov dword ptr [esi], offset vtable)
off = va_to_off(0x005DEB00)
chunk = data[off:off+300]

# Look for c7 06 (mov dword ptr [esi], imm32) or c7 07 (edi) or c7 03 (ebx)
for i in range(len(chunk)-6):
    if chunk[i] == 0xC7 and chunk[i+1] in (0x06, 0x07, 0x03, 0x01):
        imm = struct.unpack("<I", chunk[i+2:i+6])[0]
        va = 0x005DEB00 + i
        print(f"0x{va:08X}: mov [reg], 0x{imm:08X}")
