import struct
import os

for fname in ["1.cst", "2.cst"]:
    if os.path.exists(fname):
        with open(fname, "rb") as f:
            data = f.read(128)
            print(f"=== {fname} ({os.path.getsize(fname)} bytes) ===")
            print("Hex:", data[:32].hex())
            # try to unpack header
            if len(data) >= 32:
                print("Floats:", struct.unpack("<8f", data[:32]))
                print("Ints:", struct.unpack("<8i", data[:32]))
