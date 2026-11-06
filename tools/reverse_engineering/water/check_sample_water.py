import zlib
import os

def check_csp(path):
    print("Checking:", path)
    with open(path, "rb") as f:
        data = f.read()
    # decompressed stream starts at 0x100
    try:
        decomp = zlib.decompress(data[0x100:])
        print(" Decompressed size:", len(decomp))
        # Look for waterplane or trWaterPlane
        has_wp = b"trWaterPlane" in decomp
        has_tawp = b"taWaterPlane" in decomp
        print(f" Contains trWaterPlane: {has_wp}, taWaterPlane: {has_tawp}")
        if has_wp:
            pos = decomp.find(b"trWaterPlane")
            print(" Context around trWaterPlane:", decomp[max(0, pos-40):min(len(decomp), pos+80)])
    except Exception as e:
        print(" Decompress failed:", e)

check_csp(r"Worlds\sample.csp")
check_csp(r"Worlds\demo.csp")
check_csp(r"Worlds\pro.csp")
