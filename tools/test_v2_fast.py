import glob
import struct

def parse_model_fast(fpath):
    with open(fpath, "rb") as f:
        data = f.read()

    # Find all 0xFFFFFFFF
    matches = []
    p = 0
    while True:
        p = data.find(b"\xff\xff\xff\xff", p)
        if p == -1:
            break
        matches.append(p)
        p += 4

    if not matches:
        return None, "No color markers"

    # Find best stride among 32, 36, 88
    # Count transitions
    match_set = set(matches)
    best_stride = None
    best_start = None
    best_count = 0

    for test_stride in [32, 36, 88]:
        for m in matches:
            if (m - test_stride) not in match_set:
                # possible chain start
                chain = 1
                curr = m + test_stride
                while curr in match_set:
                    chain += 1
                    curr += test_stride
                if chain > 5 and chain > best_count:
                    best_count = chain
                    best_stride = test_stride
                    best_start = m

    if not best_stride:
        return None, "No chain found"

    stride = best_stride
    vb_start = best_start - (24 if stride in [32, 36] else 0)
    pos = vb_start
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

    num_verts = len(vertices)
    triangles = []
    # search index buffer
    for offset in range(pos, min(len(data) - 6, pos + 0x4000), 2):
        i0, i1, i2 = struct.unpack_from("<3H", data, offset)
        if i0 < num_verts and i1 < num_verts and i2 < num_verts and len({i0, i1, i2}) == 3:
            # verify next 3
            valid = True
            for t in range(min(3, (len(data) - offset) // 6)):
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
    }, None

models = glob.glob("Worlds/**/*.gob", recursive=True)
passed = 0
failed = []

for m in models:
    res, err = parse_model_fast(m)
    if res and res['verts'] > 0 and res['tris'] > 0:
        passed += 1
    else:
        failed.append((m, err, res))

print(f"\nResult: {passed} / {len(models)} models parsed with full vertices and triangles in <1 second!")
if failed:
    print(f"Failed count ({len(failed)}):")
    for f in failed:
        print(" ", f)
