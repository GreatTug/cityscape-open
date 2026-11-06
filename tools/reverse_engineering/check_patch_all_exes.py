import sys
sys.path.append('.')
from scratch.pe_helper import parse_pe

def check_exe(path):
    print(f"=== Checking {path} ===")
    try:
        pe = parse_pe(path)
        off = pe['va_to_offset'](0x70C3A1)
        b = pe['data'][off : off + 34]
        print(f"File offset: {hex(off)}")
        print(f"Bytes at 0x0070C3A1 ({len(b)} bytes): {b.hex().upper()}")
    except Exception as e:
        print(f"Error: {e}")

check_exe(r"C:\cityscape-open\CityScape.exe.bak")
check_exe(r"c:\cityscape-open\CityScape.exe")
check_exe(r"C:\CityScape 1.8 Autodesk Promo\CityScape.exe")
