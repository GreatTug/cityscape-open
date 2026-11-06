exe_path = r"C:\CityScape 1.8 Autodesk Promo\CityScape.exe"
with open(exe_path, "rb") as f:
    data = f.read()

off = data.find(b"Can only read buffers from the host platform.")
chunk = data[off - 1200 : off]
for s in chunk.split(b'\x00'):
    if len(s) > 2:
        try:
            print("  ", s.decode('ascii'))
        except:
            pass
