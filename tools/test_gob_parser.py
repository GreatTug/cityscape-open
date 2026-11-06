import struct

with open(r"Worlds/props/urban/traffic_signs/signs_stop01.gob", "rb") as f:
    data = f.read()

# Vertex buffer starts 24 bytes before first 0xFFFFFFFF
first_color = data.find(b"\xff\xff\xff\xff", 0x300)
vb_start = first_color - 24

# Let's count how many consecutive 36-byte vertices we have
vertices = []
pos = vb_start
while pos + 36 <= len(data):
    col = struct.unpack_from("<I", data, pos + 24)[0]
    if col != 0xFFFFFFFF:
        break
    px, py, pz = struct.unpack_from("<3f", data, pos)
    nx, ny, nz = struct.unpack_from("<3f", data, pos + 12)
    u, v = struct.unpack_from("<2f", data, pos + 28)
    vertices.append({
        'pos': (px, py, pz),
        'norm': (nx, ny, nz),
        'uv': (u, v)
    })
    pos += 36

print(f"Extracted {len(vertices)} vertices starting at 0x{vb_start:X}!")
print("First vertex:", vertices[0])
print("Last vertex:", vertices[-1])

# Index buffer follows the vertex buffer
# In Direct3D, indices are 16-bit unsigned ints (triangles: 3 indices per triangle)
ib_start = pos
print(f"Index buffer candidate start at 0x{ib_start:X}")
print("Bytes after VB:", data[ib_start : ib_start + 64].hex())

# Let's inspect uint16 indices
indices = []
for i in range(ib_start, min(len(data), ib_start + 2000), 2):
    idx = struct.unpack_from("<H", data, i)[0]
    if idx < len(vertices):
        indices.append(idx)
    else:
        break

print(f"Extracted {len(indices)} indices ({len(indices)//3} triangles)!")
print("First 12 indices:", indices[:12])
