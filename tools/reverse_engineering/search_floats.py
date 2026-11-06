import struct

def search_floats(path):
    print(f"\n=== Searching floats in {path} ===")
    with open(path, "rb") as f:
        data = f.read()
    
    for i in range(len(data) - 3):
        # 4-byte float
        fval = struct.unpack_from("<f", data, i)[0]
        if fval in [1600.0, 2500.0, 2000.0, 3000.0, 5000.0, 512.0, 256.0, 2.0]:
            print(f"  Offset 0x{i:04X} (+{i:4d}): float {fval}")
        # 8-byte double
        if i <= len(data) - 8:
            dval = struct.unpack_from("<d", data, i)[0]
            if dval in [1600.0, 2500.0, 2000.0, 3000.0, 5000.0, 512.0, 256.0, 2.0]:
                print(f"  Offset 0x{i:04X} (+{i:4d}): double {dval}")

search_floats(r"C:\cityscape-open\testsaves\save1.cst")
search_floats(r"C:\Users\greattug\Documents\3ds Max 2026\test.csp")
