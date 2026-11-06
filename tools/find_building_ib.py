import struct

with open("Worlds/props/buildings/downtown0010_0.gob", "rb") as f:
    data = f.read()

num_verts = 47244
# let's search from offset 1,500,000 onwards for uint16 triplets
for off in range(1500000, len(data) - 100, 2):
    i0, i1, i2 = struct.unpack_from("<3H", data, off)
    if i0 < num_verts and i1 < num_verts and i2 < num_verts and len({i0, i1, i2}) == 3:
        # check next 10 triplets
        valid = True
        for t in range(10):
            t0, t1, t2 = struct.unpack_from("<3H", data, off + t * 6)
            if not (t0 < num_verts and t1 < num_verts and t2 < num_verts and len({t0, t1, t2}) == 3):
                valid = False
                break
        if valid:
            print(f"Downtown0010_0 index buffer found at 0x{off:X} (offset {off})!")
            break
