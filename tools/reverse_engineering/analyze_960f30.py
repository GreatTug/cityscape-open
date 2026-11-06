import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

# Let's inspect instructions from 0x00961240 to 0x00961290
off = va_to_off(0x00961240)
chunk = data[off:off+80]

print("Hex around 0x0096127C:")
print(chunk.hex())

# Let's also see what 0x00960F30 does from its entry point
off_entry = va_to_off(0x00960F30)
print("\nHex at entry 0x00960F30:")
print(data[off_entry:off_entry+60].hex())
