import struct

with open("Worlds/props/vehicles/sedan.gob", "rb") as f:
    data = f.read()

# in sedan.gob, there are 316 vertices
# let's find indices
num_verts = 316
# search after pos
# Let's find any uint16 array of triangles
for off in range(0x1000, len(data) - 6, 2):
    i0, i1, i2 = struct.unpack_from("<3H", data, off)
    if i0 < num_verts and i1 < num_verts and i2 < num_verts and len({i0, i1, i2}) == 3:
        # test if subsequent 5 triplets are also valid
        valid = True
        for t in range(5):
            t0, t1, t2 = struct.unpack_from("<3H", data, off + t * 6)
            if not (t0 < num_verts and t1 < num_verts and t2 < num_verts and len({t0, t1, t2}) == 3):
                valid = False
                break
        if valid:
            print(f"Index buffer found at 0x{off:X}!")
            # count total
            q = off
            tris = 0
            while q + 6 <= len(data):
                t0, t1, t2 = struct.unpack_from("<3H", data, q)
                if t0 < num_verts and t1 < num_verts and t2 < num_verts:
                    tris += 1
                    q += 6
                else:
                    break
            print(f"Total triangles: {tris}")
            break
