with open("Worlds/props/urban/street_lights/traffic_light_single.gob", "rb") as f:
    data = f.read()

pos = 0x41A + 18
print(f"Bytes after last XML at 0x{pos:X}:")
print(data[pos:pos+300])
