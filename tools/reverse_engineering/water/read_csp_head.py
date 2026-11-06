with open(r"Worlds\sample.csp", "rb") as f:
    data = f.read(256)
print(data[:100])
print(data[100:200].hex())
