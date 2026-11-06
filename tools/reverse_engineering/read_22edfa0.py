import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
off = pe['va_to_offset'](0x22EDFA0)
end = data.find(b'\x00', off)
print("String at 0x22EDFA0:", repr(data[off:end]))
