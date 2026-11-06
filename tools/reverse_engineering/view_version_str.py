with open("CityScape.exe", "rb") as f:
    data = f.read()

off = 0x01FCB0F0
print(data[off:off+200])
