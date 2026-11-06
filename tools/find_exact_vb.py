import struct

with open("Worlds/props/natural/trees/tree_oak03.gob", "rb") as f:
    data = f.read()

# Let's find RenderStates
rs = data.find(b"RenderStates")
print("RenderStates at:", hex(rs))
print("Bytes after RenderStates (128 bytes):")
snippet = data[rs:rs+128]
print(snippet)

# Let's look for float arrays (vertices)
# In 3D models, vertex coordinates are typically in range [-50, 50]
# Normals are in range [-1, 1]
# Diffuse color is often 0xFFFFFFFF or similar
# Let's scan for where vertices actually start!
for i in range(rs, rs + 1000, 4):
    fvals = struct.unpack_from("<4f", data, i)
    if all(-100.0 < x < 100.0 for x in fvals):
        # could be vertices
        print(f"Candidate vertex float pos at 0x{i:X}: {fvals}")
        # check if next 36 bytes look like another vertex
        next_fvals = struct.unpack_from("<4f", data, i + 36)
        print(f"  +36: {next_fvals}")
        break
