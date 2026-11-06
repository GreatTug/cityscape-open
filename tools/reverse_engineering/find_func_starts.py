import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

pe = parse_pe("CityScape.exe")
data = pe['data']
va_to_off = pe['va_to_offset']

def find_func_start(va):
    for cur in range(va, va - 0x1000, -1):
        off = va_to_off(cur)
        # Check for function prologue or alignment padding before it
        if data[off-1:off] == b'\xcc' or data[off-1:off] == b'\x90':
            if data[off:off+3] in (b'\x55\x8b\xec', b'\x83\xec', b'\x53\x56', b'\x64\xa1\x00'):
                return cur
            if data[off:off+1] in (b'\x55', b'\x6a', b'\x53'):
                return cur
    return None

for caller in [0x006F45B9, 0x006F5189, 0x006F616F, 0x006FD914]:
    fstart = find_func_start(caller)
    print(f"Caller VA 0x{caller:08X} -> Function start approx: 0x{fstart:08X}" if fstart else f"Caller VA 0x{caller:08X} -> unknown start")
