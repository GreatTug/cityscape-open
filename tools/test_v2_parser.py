import glob
import struct

def parse_model_v2(fpath):
    with open(fpath, "rb") as f:
        data = f.read()

    # Find runs of 0xFFFFFFFF with stride 32, 36, or 88
    # Search for candidate strides
    best_stride = None
    best_vb_start = None
    best_count = 0

    for test_stride in [32, 36, 88]:
        # find matching chains of at least 10 vertices
        for start_col in range(0x100, min(len(data), 0x20000), 2):
            if data[start_col:start_col+4] == b"\xff\xff\xff\xff":
                # count how many consecutive matches at test_stride
                chain = 1
                curr = start_col + test_stride
                while curr + 4 <= len(data) and data[curr:curr+4] == b"\xff\xff\xff\xff":
                    chain += 1
                    curr += test_stride
                if chain > 10 and chain > best_count:
                    best_count = chain
                    best_stride = test_stride
                    best_vb_start = start_col - (24 if test_stride in [32, 36] else 0)

    if not best_stride or best_count < 10:
        return None, f"No valid vertex chain (best={best_count})"

    stride = best_stride
    pos = best_vb_start
    vertices = []

    for _ in range(best_count):
        if stride == 36:
            px, py, pz = struct.unpack_from("<3f", data, pos)
            nx, ny, nz = struct.unpack_from("<3f", data, pos + 12)
            u, v = struct.unpack_from("<2f", data, pos + 28)
        elif stride == 32:
            px, py, pz, _ = struct.unpack_from("<4e", data, pos + 8)
            nx, ny, nz, _ = struct.unpack_from("<4e", data, pos + 16)
            u, v = struct.unpack_from("<2e", data, pos + 28)
        elif stride == 88:
            px, py, pz = struct.unpack_from("<3f", data, pos - 20)
            nx, ny, nz = struct.unpack_from("<3f", data, pos + 20)
            u, v = struct.unpack_from("<2f", data, pos + 68)

        vertices.append((px, py, pz, nx, ny, nz, u, v))
        pos += stride

    # Search for index buffer
    num_verts = len(vertices)
    triangles = []
    # search around pos and throughout data
    for offset in range(pos, min(len(data) - 6, pos + 0x8000), 2):
        i0, i1, i2 = struct.unpack_from("<3H", data, offset)
        if i0 < num_verts and i1 < num_verts and i2 < num_verts and len({i0, i1, i2}) == 3:
            # check next 4
            valid = True
            for t in range(4):
                t0, t1, t2 = struct.unpack_from("<3H", data, offset + t * 6)
                if not (t0 < num_verts and t1 < num_verts and t2 < num_verts and len({t0, t1, t2}) == 3):
                    valid = False
                    break
            if valid:
                q = offset
                while q + 6 <= len(data):
                    t0, t1, t2 = struct.unpack_from("<3H", data, q)
                    if t0 < num_verts and t1 < num_verts and t2 < num_verts:
                        triangles.append((t0, t1, t2))
                        q += 6
                    else:
                        break
                break

    return {
        'stride': stride,
        'verts': len(vertices),
        'tris': len(triangles),
        'bbox_x': (min(v[0] for v in vertices), max(v[0] for v in vertices)),
        'bbox_y': (min(v[1] for v in vertices), max(v[1] for v in vertices)),
        'bbox_z': (min(v[2] for v in vertices), max(v[2] for v in vertices)),
    }, None

models = glob.glob("Worlds/**/*.gob", recursive=True)
passed = 0
failed = []

for m in models:
    res, err = parse_model_v2(m)
    if res and res['verts'] > 0 and res['tris'] > 0:
        passed += 1
    else:
        failed.append((m, err, res))

print(f"\nResult: {passed} / {len(models)} models parsed with VERTICES AND TRIANGLES!")
if failed:
    print(f"Failed count ({len(failed)}):")
    for f in failed[:10]:
        print(" ", f)
