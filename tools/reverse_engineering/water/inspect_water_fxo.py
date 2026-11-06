import struct
import os
import re

def inspect_fxo(path):
    print("=" * 60)
    print("INSPECTING:", path)
    if not os.path.exists(path):
        print("File does not exist!")
        return
    with open(path, "rb") as f:
        data = f.read()
    print("Size:", len(data))
    
    # Find all ASCII strings of length >= 3
    strings = re.findall(b"[\x20-\x7e]{3,}", data)
    print("Strings found (sample):")
    interesting = []
    for s in strings:
        s_str = s.decode('ascii', errors='ignore')
        if any(w in s_str.lower() for w in ['tex', 'water', 'sampler', 'tech', 'vs_', 'ps_', 'mat', 'bump', 'refl', 'view', 'proj', 'world', 'fog']):
            interesting.append(s_str)
    for s in interesting[:50]:
        print(" ", s)

inspect_fxo(r"assets\extracted\win_3_0\waterplane.fxo")
inspect_fxo(r"assets\extracted\win_3_0\water.fxo")
