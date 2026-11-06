import struct
import pefile

pe = pefile.PE("CityScape_Old_Legacy.exe")
image_base = pe.OPTIONAL_HEADER.ImageBase

with open("CityScape_Old_Legacy.exe", "rb") as f:
    raw_data = f.read()

def va_to_offset(va):
    rva = va - image_base
    for sec in pe.sections:
        if sec.VirtualAddress <= rva < sec.VirtualAddress + sec.Misc_VirtualSize:
            return sec.PointerToRawData + (rva - sec.VirtualAddress)
    return None

def read_float(va):
    off = va_to_offset(va)
    if off is not None and off + 4 <= len(raw_data):
        return struct.unpack("<f", raw_data[off:off+4])[0]
    return None

def read_double(va):
    off = va_to_offset(va)
    if off is not None and off + 8 <= len(raw_data):
        return struct.unpack("<d", raw_data[off:off+8])[0]
    return None

vas = [
    0x22cde60,
    0x22cdb2c,
    0x22d2de4,
    0x22e7534,
    0x22e845c,
    0x232d5fc,
    0x232d5ec,
    0x22e8434,
    0x22e83fc,
    0x22e83f4,
    0x22e83e8,
    0x2308b18,
    0x232dfa8,
    0x22e83a8,
    0x22e8390,
    0x232df98,
    0x22e8370,
    0x22e8360
]

print("=== INSPECTING RDATA CONSTANTS ===")
for va in vas:
    f_val = read_float(va)
    d_val = read_double(va)
    off = va_to_offset(va)
    # Check if it's an ASCII string
    s_val = ""
    if off is not None:
        raw_s = raw_data[off:off+32]
        zero = raw_s.find(b"\x00")
        if zero != -1:
            try:
                candidate = raw_s[:zero].decode('ascii')
                if candidate.isprintable() and len(candidate) >= 2:
                    s_val = candidate
            except:
                pass
    print(f"VA: 0x{va:08X} (Offset: 0x{off:08X}) -> Float: {f_val:12.6f} | Double: {d_val:14.6f} | String: '{s_val}'")
