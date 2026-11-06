import struct

with open("Worlds/props/buildings/downtown0010_0.gob", "rb") as f:
    data = f.read()

pos = 0x1a6b
for i in range(10):
    raw = data[pos:pos+32]
    px, py, pz, nx, ny, nz = struct.unpack_from("<6f", raw, 0)
    col = struct.unpack_from("<I", raw, 24)[0]
    u, v = struct.unpack_from("<2e", raw, 28)
    print(f"Vert {i:2d}: pos=({px:7.3f}, {py:7.3f}, {pz:7.3f}), norm=({nx:6.3f}, {ny:6.3f}, {nz:6.3f}), uv=({u:6.3f}, {v:6.3f})")
    pos += 32
