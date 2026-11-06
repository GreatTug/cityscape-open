import os
import struct
import re

def inspect_file(fpath):
    print(f"=== Inspecting {fpath} ===")
    with open(fpath, "rb") as f:
        data = f.read()

    # Find all ASCII strings of length >= 4
    strings = re.findall(b"[a-zA-Z0-9_]{4,}", data)
    print("Interesting strings found:", [s.decode() for s in strings if any(k in s.lower() for k in [b"vertex", b"index", b"mesh", b"buffer", b"shader", b"cull", b"geom", b"data", b"mat"])][:15])

    # Let's see chunks starting with length or tag
    # gxModel, gxVertexBuffer, etc.
    for kw in [b"gxModel", b"gxVertexBuffer", b"gxIndexBuffer", b"gxCullBox", b"gmAssetData", b"ShaderVariables"]:
        idx = 0
        while True:
            idx = data.find(kw, idx)
            if idx == -1:
                break
            context = data[max(0, idx - 16):min(len(data), idx + 32)]
            print(f"  Found '{kw.decode()}' at offset 0x{idx:X}: {context.hex()}")
            idx += len(kw)

inspect_file("Worlds/props/natural/trees/tree_oak03.gob")
inspect_file("Worlds/props/vehicles/sedan_traffic.gob")
