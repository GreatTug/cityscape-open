import struct
import os

parc_path = r"c:\cityscape-open\assets\assets.parc"
if os.path.exists(parc_path):
    with open(parc_path, "rb") as f:
        header = f.read(64)
        print("Header bytes:", header[:16])
        print("Hex:", header[:16].hex())
        try:
            magic = header[:4].decode('ascii', errors='ignore')
            print("Magic ascii:", magic)
        except Exception:
            pass
