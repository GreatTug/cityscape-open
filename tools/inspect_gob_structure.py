import struct

with open(r"Worlds/props/natural/trees/tree_oak03.gob", "rb") as f:
    data = f.read()

print(f"File size: {len(data)} bytes")
print("First 64 bytes:", data[:64])

# Find occurrences of gxVertexBufferData and gxIndexBufferData
vb_pos = data.find(b"gxVertexBufferData")
ib_pos = data.find(b"gxIndexBufferData")
print(f"gxVertexBufferData pos: 0x{vb_pos:X}")
print(f"gxIndexBufferData pos: 0x{ib_pos:X}")

# Let's inspect bytes around vb_pos
if vb_pos != -1:
    chunk = data[vb_pos : vb_pos + 128]
    print("vb chunk:", chunk)

# Let's search for floats or counts
# In gxVertexBufferData serializer:
# Usually there is vertex format/flags, vertex count, vertex stride, and vertex raw bytes
