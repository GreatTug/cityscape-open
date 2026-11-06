import sys, struct
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

# Dump assembly in 0x0070C180 - 0x0070C320
# Let's inspect conditional jumps, calls, and variables
off = va_to_off(0x0070C180)
chunk = data[off:off+350]

# Print hex with VAs
for i in range(0, len(chunk), 16):
    va = 0x0070C180 + i
    h = ' '.join(f"{b:02x}" for b in chunk[i:i+16])
    print(f"0x{va:08X}: {h}")
