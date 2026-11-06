import struct

with open(r"Worlds/props/urban/traffic_signs/signs_stop01.gob", "rb") as f:
    data = f.read()

# Let's search for ffffffff
pos = data.find(b"\xff\xff\xff\xff", 0x300)
print(f"ffffffff found at 0x{pos:X}")

# Let's see what is right before ffffffff:
# In Direct3D 9 FVF / vertex layouts:
# Vertex format is often:
# Position (3 floats = 12 bytes)
# Normal (3 floats = 12 bytes) or Tangent
# Color (0xFFFFFFFF = 4 bytes)
# UV (2 floats = 8 bytes)
# Total stride = 12 + 12 + 4 + 8 = 36 bytes (or 32, 40, 48, etc.)!

# If color is 0xFFFFFFFF at pos:
# then pos - 24 might be the start of vertex (pos - 12 pos, pos - 12 normal)!
vertex_start = pos - 24 # if 12 pos + 12 normal
print(f"Candidate vertex start: 0x{vertex_start:X}")

v_bytes = data[vertex_start : vertex_start + 48]
floats = struct.unpack_from("<6f", data, vertex_start)
print(f"Position: ({floats[0]:.4f}, {floats[1]:.4f}, {floats[2]:.4f})")
print(f"Normal:   ({floats[3]:.4f}, {floats[4]:.4f}, {floats[5]:.4f})")
color = struct.unpack_from("<I", data, vertex_start + 24)[0]
print(f"Color:    0x{color:08X}")
uv = struct.unpack_from("<2f", data, vertex_start + 28)
print(f"UV:       ({uv[0]:.4f}, {uv[1]:.4f})")
