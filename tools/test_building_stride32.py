import struct

with open("Worlds/props/buildings/downtown0010_0.gob", "rb") as f:
    data = f.read()

pos = 0x1a6b
for i in range(5):
    raw = data[pos:pos+32]
    # let's try various unpackings
    # 3 floats pos, 3 floats norm, 1 uint32 col, 2 half-floats uv?
    px, py, pz, nx, ny, nz = struct.unpack_from("<6f", raw, 0)
    col = struct.unpack_from("<I", raw, 24)[0]
    uv_raw = struct.unpack_from("<2H", raw, 28)
    print(f"Vert {i}: pos=({px:.3f}, {py:.3f}, {pz:.3f}), norm=({nx:.3f}, {ny:.3f}, {nz:.3f}), col=0x{col:08X}, uv16={uv_raw}")
    pos += 32
