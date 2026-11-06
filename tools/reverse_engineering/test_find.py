with open("CityScape.exe", "rb") as f:
    data = f.read()

for s in [b"bin ver", b"project bin", b"terrain bin", b"ver 1", b"bin ver 1", b"project", b"terrain"]:
    pos = data.find(s)
    print(f"'{s.decode()}': pos={pos}")
