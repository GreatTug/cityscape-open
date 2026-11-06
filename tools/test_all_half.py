import struct

with open("Worlds/props/buildings/downtown0010_0.gob", "rb") as f:
    data = f.read()

pos = 0x1a6b
for i in range(8):
    raw = data[pos:pos+32]
    halfs = struct.unpack_from("<16e", raw, 0)
    print(f"Vert {i:2d}: {[round(x, 3) for x in halfs]}")
    pos += 32
