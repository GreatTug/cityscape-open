with open(r"Worlds/props/urban/traffic_signs/signs_stop01.gob", "rb") as f:
    data = f.read()

# Let's see what strings or data appear around 0x0350 to 0x0500
pos = 0x300
print("Data around 0x300 - 0x500:")
print(data[0x300:0x400].hex())
print(data[0x400:0x500].hex())
