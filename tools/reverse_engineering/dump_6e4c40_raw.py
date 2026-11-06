# Minimal x86 disassembler for 0x006E4C40
# We can use python to inspect the raw instructions or print them nicely.
import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']
off = va_to_off(0x006E4C40)
chunk = data[off:off+180]

# Let's print each byte offset and hex
for i in range(0, len(chunk), 16):
    va = 0x006E4C40 + i
    h = ' '.join(f"{b:02x}" for b in chunk[i:i+16])
    print(f"0x{va:08X}: {h}")
