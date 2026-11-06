with open("CityScape.exe", "rb") as f:
    data = f.read()

off = 0x01FCB000
print(data[off:off+500])
