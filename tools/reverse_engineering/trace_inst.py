import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe
pe = parse_pe('CityScape.exe')
data = pe['data']
va_to_off = pe['va_to_offset']

# Start around 0x0070C390
# Let's find 8b 8d a8 00 00 00
pat = bytes.fromhex("8b8da8000000")
idx = data.find(pat)
va = pe['offset_to_va'](idx)
print(f"Pattern found at VA 0x{va:08X}")

chunk = data[idx:idx+40]
print("Chunk hex:", chunk.hex())

# Let's trace bytes:
# 8b 8d a8 00 00 00 (6 bytes): mov ecx, [ebp+0xa8] -> at va
# 89 4c 24 18 (4 bytes): mov [esp+0x18], ecx -> at va + 6
# 8b 95 ac 00 00 00 (6 bytes): mov edx, [ebp+0xac] -> at va + 10
# 89 54 24 1c (4 bytes): mov [esp+0x1c], edx -> at va + 16
# 8d 4c 24 18 (4 bytes): lea ecx, [esp+0x18] -> at va + 20
# 51 (1 byte): push ecx -> at va + 24
# 8d 48 74 (3 bytes): lea ecx, [eax+0x74] -> at va + 25
# 90 90 90 90 90 90 (6 bytes): NOPs -> at va + 28
# e8 xx xx xx xx (5 bytes): call -> at va + 34
call_va = va + 34
import struct
rel = struct.unpack("<i", data[idx+35:idx+39])[0]
target = call_va + 5 + rel
print(f"Call at 0x{call_va:08X}: e8 {rel:08X} -> target VA 0x{target:08X}")
