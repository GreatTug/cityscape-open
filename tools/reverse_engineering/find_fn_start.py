import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']

off = pe['va_to_offset'](0x006F5C20)
fn_start = off
while fn_start > off - 0x2000:
    if data[fn_start-1] in (0xcc, 0xc3) or (data[fn_start-3] == 0xc2 and data[fn_start-1] == 0xcc):
        break
    fn_start -= 1

va_start = pe['offset_to_va'](fn_start)
print(f"Function start: offset {hex(fn_start)}, VA {hex(va_start)}, length {off - fn_start}")
