import struct, sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']

target_va = 0x00ECB7F0
print(f"Searching calls to 0x{target_va:08X}...")

call_count = 0
for i in range(len(data) - 5):
    if data[i] == 0xE8: # relative CALL
        rel = struct.unpack_from("<i", data, i+1)[0]
        insn_va = pe['offset_to_va'](i)
        if insn_va:
            dest_va = (insn_va + 5 + rel) & 0xFFFFFFFF
            if dest_va == target_va:
                call_count += 1
                print(f"Call #{call_count} at VA 0x{insn_va:08X} (offset 0x{i:08X})")
