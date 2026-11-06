import struct

with open("Worlds/props/buildings/downtown0010_0.gob", "rb") as f:
    data = f.read()

pos = 0x1a6b
# Let's inspect raw floats or ints for 5 vertices
for i in range(5):
    raw = data[pos:pos+32]
    floats = struct.unpack_from("<8f", raw, 0)
    uints = struct.unpack_from("<8I", raw, 0)
    print(f"Vert {i}:")
    print("  as floats:", [f"{x:.4f}" for x in floats])
    print("  as uint32:", [f"0x{x:08X}" for x in uints])
    pos += 32
