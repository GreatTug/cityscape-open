import sys, struct
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

# Look for vtable assignments inside 0x00961260 to 0x00961400
off = va_to_off(0x00961260)
chunk = data[off:off+300]

for i in range(len(chunk)-6):
    if chunk[i] == 0xC7 and chunk[i+1] in (0x06, 0x07, 0x03, 0x01, 0x46, 0x47):
        imm = struct.unpack("<I", chunk[i+2:i+6])[0]
        va = 0x00961260 + i
        print(f"0x{va:08X}: mov [reg], 0x{imm:08X}")
