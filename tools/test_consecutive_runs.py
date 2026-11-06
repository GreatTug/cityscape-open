import glob
import struct

def parse_model(fpath):
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

    # Look for a run of consecutive matches with delta == 32 or 36
    best_run = []
    current_run = []
    stride = 36

    for i in range(len(matches) - 1):
        delta = matches[i+1] - matches[i]
        if delta in [32, 36]:
            if not current_run:
                current_run = [matches[i], matches[i+1]]
                stride = delta
            elif delta == stride and matches[i] == current_run[-1]:
                current_run.append(matches[i+1])
            else:
                if len(current_run) > len(best_run):
                    best_run = current_run
                current_run = [matches[i], matches[i+1]]
                stride = delta
        else:
            if len(current_run) > len(best_run):
                best_run = current_run
            current_run = []

    if len(current_run) > len(best_run):
        best_run = current_run

    if len(best_run) < 3:
        return None, f"No run found (len={len(best_run)})"

    stride = best_run[1] - best_run[0]
    first_col = best_run[0]
    vb_start = first_col - 24

    vertices = []
    pos = vb_start
    while pos + stride <= len(data):
        col = struct.unpack_from("<I", data, pos + 24)[0]
        if col != 0xFFFFFFFF:
            break
        if stride == 36:
            px, py, pz = struct.unpack_from("<3f", data, pos)
            nx, ny, nz = struct.unpack_from("<3f", data, pos + 12)
            u, v = struct.unpack_from("<2f", data, pos + 28)
        else: # stride == 32
            px, py, pz, _ = struct.unpack_from("<4e", data, pos + 8)
            nx, ny, nz, _ = struct.unpack_from("<4e", data, pos + 16)
            u, v = struct.unpack_from("<2e", data, pos + 28)

        vertices.append((px, py, pz, nx, ny, nz, u, v))
        pos += stride

    num_verts = len(vertices)
    triangles = []
    for offset in range(pos, min(len(data) - 6, pos + 256), 2):
        i0, i1, i2 = struct.unpack_from("<3H", data, offset)
        if i0 < num_verts and i1 < num_verts and i2 < num_verts and (i0 != i1 or i1 != i2):
            q = offset
            while q + 6 <= len(data):
                t0, t1, t2 = struct.unpack_from("<3H", data, q)
                if t0 < num_verts and t1 < num_verts and t2 < num_verts:
                    triangles.append((t0, t1, t2))
                    q += 6
                else:
                    break
            if len(triangles) >= 1:
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
    res, err = parse_model(m)
    if res and res['verts'] > 0 and res['tris'] > 0:
        passed += 1
    else:
        failed.append((m, err, res))

print(f"Unified Consecutive Run Parser Result: {passed} / {len(models)} models parsed!")
if failed:
    print(f"Failed ({len(failed)}):")
    for f in failed:
        print(" ", f)
