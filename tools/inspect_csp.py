import os
import struct

csp_path = r"c:\cityscape-open\Worlds\sample.csp"
if os.path.exists(csp_path):
    with open(csp_path, "rb") as f:
        data = f.read(256)
        print("CSP Header length:", len(data))
        print("Hex:", data[:32].hex())
        print("ASCII:", "".join([chr(b) if 32 <= b <= 126 else '.' for b in data[:64]]))
