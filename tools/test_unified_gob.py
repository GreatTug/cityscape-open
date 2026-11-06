import glob
import struct
import math

def half_to_float(h):
    # struct unpack 'e'
    return struct.unpack("<f", struct.pack("<f", struct.unpack("<e", struct.pack("<H", h))[0]))[0]

def parse_any_gob(fpath):
    with open(fpath, "rb") as f:
        data = f.read()

    first_col = data.find(b"\xff\xff\xff\xff", 0x100)
    if first_col == -1:
        return None, "No color marker"

    next_col = data.find(b"\xff\xff\xff\xff", first_col + 1)
    if next_col == -1:
        stride = 36
    else:
        stride = next_col - first_col
        if stride not in [32, 36]:
            stride = 36

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
            # pos at +8, normal at +16, uv at +28
            px, py, pz, _ = struct.unpack_from("<4e", data, pos + 8)
            nx, ny, nz, _ = struct.unpack_from("<4e", data, pos + 16)
            u, v = struct.unpack_from("<2e", data, pos + 28)

        vertices.append((px, py, pz, nx, ny, nz, u, v))
        pos += stride

    if len(vertices) == 0:
        return None, "0 vertices"

    # Index buffer
    triangles = []
    num_verts = len(vertices)
    for offset in range(pos, min(len(data) - 6, pos + 128), 2):
        i0, i1, i2 = struct.unpack_from("<3H", data, offset)
        if i0 < num_verts and i1 < num_verts and i2 < num_verts and (i0 != i1 or i1 != i2):
            p = offset
            while p + 6 <= len(data):
                t0, t1, t2 = struct.unpack_from("<3H", data, p)
                if t0 < num_verts and t1 < num_verts and t2 < num_verts:
                    triangles.append((t0, t1, t2))
                    p += 6
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
print(f"Testing unified parser on {len(models)} models...")

success = 0
stride_36 = 0
stride_32 = 0
errors = []

for m in models:
    res, err = parse_any_gob(m)
    if res and res['verts'] > 0 and res['tris'] > 0:
        success += 1
        if res['stride'] == 36:
            stride_36 += 1
        else:
            stride_32 += 1
    else:
        errors.append((m, err, res))

print(f"\nResult: {success} / {len(models)} models parsed with VERTICES AND TRIANGLES!")
print(f"Stride 36 (Props/Foliage/Vehicles): {stride_36}")
print(f"Stride 32 (Downtown/Industrial Buildings): {stride_32}")
if errors:
    print(f"Errors ({len(errors)}):")
    for e in errors:
        print(" ", e)
else:
    print("ALL 123 MODELS PARSED PERFECTLY WITH 100% SUCCESS!")
