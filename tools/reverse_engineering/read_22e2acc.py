import struct, sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']

off = pe['va_to_offset'](0x22E2ACC)
fval = struct.unpack_from("<f", data, off)[0]
uval = struct.unpack_from("<I", data, off)[0]
print(f"Float at 0x22E2ACC: {fval} (hex: {hex(uval)})")
