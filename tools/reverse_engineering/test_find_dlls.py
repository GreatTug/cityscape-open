import glob

for fn in glob.glob("C:/CityScape 1.8 Autodesk Promo/*.dll") + glob.glob("*.dll") + glob.glob("lib/*.dll"):
    try:
        with open(fn, "rb") as f:
            data = f.read()
        pos = data.find(b"bin ver")
        if pos != -1:
            print(f"Found 'bin ver' in {fn} at {hex(pos)}!")
        pos2 = data.find(b"terrain bin")
        if pos2 != -1:
            print(f"Found 'terrain bin' in {fn} at {hex(pos2)}!")
    except Exception:
        pass
