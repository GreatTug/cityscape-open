with open("CityScape.exe", "rb") as f:
    data = f.read()

off = 0x0219EAE4 - 0x100
print(data[off:off+300])
