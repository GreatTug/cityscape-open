import struct

with open(r"Worlds/props/natural/trees/tree_oak03.gob", "rb") as f:
    data = f.read()

pos = 0
header = data[:18]
print("Header:", header)
pos = 18

def read_cstr(offset):
    end = data.find(b"\x00", offset)
    if end != -1:
        return data[offset:end].decode('latin1', errors='ignore'), end + 1
    return "", offset

while pos < 200:
    name, next_pos = read_cstr(pos)
    print(f"Offset 0x{pos:04X}: String '{name}'")
    pos = next_pos
    if name == "gxCullBox":
        # 6 floats: minX, minY, minZ, maxX, maxY, maxZ
        bbox = struct.unpack_from("<6f", data, pos)
        print(f"  gxCullBox: min=({bbox[0]:.2f}, {bbox[1]:.2f}, {bbox[2]:.2f}), max=({bbox[3]:.2f}, {bbox[4]:.2f}, {bbox[5]:.2f})")
        pos += 24
