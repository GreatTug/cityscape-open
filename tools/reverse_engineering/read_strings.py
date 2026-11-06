import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe
pe = parse_pe('CityScape.exe')
data = pe['data']
va_to_off = pe['va_to_offset']
for s_va in [0x022C7604, 0x022C75F4, 0x022C988C, 0x022C9A28, 0x022EEF8C]:
    off = va_to_off(s_va)
    null_idx = data.find(b'\x00', off)
    s = data[off:null_idx].decode('latin1', 'replace')
    print(f"VA 0x{s_va:08X}: {s}")
