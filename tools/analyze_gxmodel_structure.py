import struct

with open(r"Worlds/props/urban/traffic_signs/signs_stop01.gob", "rb") as f:
    data = f.read()

# Let's inspect bytes between 0x0088 and 0x0350
print("--- Header of gxModel at 0x0088 ---")
# 0x0088: "gxModel\0" -> 8 bytes
pos = 0x0088 + 8
guid = data[pos : pos + 36].decode('ascii', errors='ignore')
print("GUID:", guid)
pos += 36

# Let's inspect uints and floats after GUID
ints = struct.unpack_from("<16I", data, pos)
print("Ints after GUID:", [f"0x{x:08X} ({x})" for x in ints])

# Find where RenderStates starts
rs_pos = data.find(b"RenderStates\x00")
print(f"RenderStates at 0x{rs_pos:X}")
pos = rs_pos + len("RenderStates\x00")

# RenderStates block
# Let's print the next 128 bytes after RenderStates in hex and ints
print("Hex after RenderStates:")
print(data[pos : pos + 128].hex())
ints_after_rs = struct.unpack_from("<16I", data, pos)
print("Ints after RenderStates:", [f"0x{x:08X} ({x})" for x in ints_after_rs])
