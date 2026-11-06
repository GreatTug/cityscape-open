import os, glob, re

for fn in ["CityScape.exe"] + glob.glob("*.dll") + glob.glob("lib/*.dll"):
    try:
        with open(fn, "rb") as f:
            data = f.read()
        for m in re.finditer(rb'PixelActive', data):
            start = max(0, m.start() - 20)
            end = min(len(data), m.end() + 40)
            print(f"[{fn}] 0x{m.start():08X}: {data[start:end]}")
    except Exception:
        pass
