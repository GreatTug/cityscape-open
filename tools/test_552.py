import struct

with open("Worlds/props/urban/street_lights/traffic_light_single.gob", "rb") as f:
    data = f.read()

# Let's search for the vertex buffer
# In traffic_light_single.gob, total size is 42044 bytes
# If stride is 36, 552 vertices would be 552 * 36 = 19,872 bytes!
# And 19872 bytes + index buffer is ~40 KB, which MATCHES 42044 BYTES EXACTLY!
print(f"552 vertices * 36 bytes = {552 * 36} bytes")

# Let's find where 552 is:
idx = data.find(struct.pack("<I", 552))
while idx != -1:
    print(f"552 found at offset 0x{idx:X}")
    # Inspect next bytes
    print("  Bytes:", data[idx:idx+48].hex())
    idx = data.find(struct.pack("<I", 552), idx + 1)
