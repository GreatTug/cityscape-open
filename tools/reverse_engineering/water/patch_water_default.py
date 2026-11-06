import os
import shutil

offsets = [0x00759603 + 3, 0x0076128d + 3, 0x00764322 + 3]
paths = ["lib/CityScape.exe", "CityScape.exe", "bin/Release/net48/CityScape.exe"]

for p in paths:
    if not os.path.exists(p):
        print(f"Skipping {p} (does not exist)")
        continue
    with open(p, "rb") as f:
        data = bytearray(f.read())
    
    # Check current bytes
    for off in offsets:
        current = data[off]
        print(f"{p} at 0x{off:08x}: current=0x{current:02x}")
        assert current in (0, 1), f"Unexpected byte at 0x{off:08x}: 0x{current:02x}"
        data[off] = 1

    with open(p, "wb") as f:
        f.write(data)
    print(f"Successfully patched {p} with Global Water Enabled = true by default!")
