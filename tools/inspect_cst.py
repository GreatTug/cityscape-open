import os

cst_path = r"c:\cityscape-open\1.cst"
if os.path.exists(cst_path):
    with open(cst_path, "rb") as f:
        data = f.read(128)
        print("CST Header ASCII:", "".join([chr(b) if 32 <= b <= 126 else '.' for b in data[:80]]))
