import struct
import glob

models = glob.glob("Worlds/**/*.gob", recursive=True)[:15]

for m in models:
    with open(m, "rb") as f:
        data = f.read()

    # Look for 0xFFFFFFFF
    first_col = data.find(b"\xff\xff\xff\xff", 0x100)
    if first_col == -1:
        print(f"{m}: No 0xFFFFFFFF")
        continue

    # vertex start is first_col - 24
    vb_start = first_col - 24
    # read the uint32 right before vb_start
    if vb_start >= 4:
        count = struct.unpack_from("<I", data, vb_start - 4)[0]
    else:
        count = -1

    # verify vertices
    valid_verts = 0
    p = vb_start
    while p + 36 <= len(data):
        col = struct.unpack_from("<I", data, p + 24)[0]
        if col != 0xFFFFFFFF:
            break
        valid_verts += 1
        p += 36

    # check index buffer right after vertex buffer
    # p is end of vertices
    ib_start = p
    ib_count = struct.unpack_from("<I", data, ib_start)[0] if ib_start + 4 <= len(data) else 0

    print(f"{m}:")
    print(f"  Count uint32 before VB: {count}, Actual parsed 36B verts: {valid_verts}")
    print(f"  VB start: 0x{vb_start:X}, End: 0x{p:X}")
    print(f"  At end of VB (0x{p:X}): uint32 = {ib_count}")
    # preview triangles
    tri_preview = []
    for t in range(min(5, (len(data) - (p + 4)) // 6)):
        i0, i1, i2 = struct.unpack_from("<3H", data, p + 4 + t * 6)
        tri_preview.append((i0, i1, i2))
    print(f"  Triangles preview: {tri_preview}")
