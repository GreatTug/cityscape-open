exe_path = r"C:\CityScape 1.8 Autodesk Promo\CityScape.exe"
with open(exe_path, "rb") as f:
    data = f.read()

off = data.find(b"Internal error: vertex buffer not set.")
print("Offset in file:", hex(off))
chunk = data[off - 500 : off + 500]
for s in chunk.split(b'\x00'):
    if len(s) > 2:
        try:
            print("  ", s.decode('ascii'))
        except:
            pass
