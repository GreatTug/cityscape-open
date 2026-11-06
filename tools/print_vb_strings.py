exe_path = r"C:\CityScape 1.8 Autodesk Promo\CityScape.exe"
with open(exe_path, "rb") as f:
    data = f.read()

off = 0x1F70A00
chunk = data[off - 200 : off + 300]
for s in chunk.split(b'\x00'):
    if len(s) > 1:
        try:
            print("  ", s.decode('ascii'))
        except:
            pass
