import struct

with open(r"Worlds/props/natural/trees/tree_oak03.gob", "rb") as f:
    data = f.read()

# Let's search for actual 3D positions with non-zero coordinates
candidates = []
for i in range(0x2B7, len(data) - 500, 4):
    try:
        x, y, z = struct.unpack_from("<3f", data, i)
        # Tree has dimensions min=(-4.60, -0.27, -4.83), max=(5.19, 9.08, 5.37)
        if -5.0 <= x <= 5.5 and 0.1 <= y <= 9.5 and -5.0 <= z <= 5.5:
            if abs(x) > 0.01 and abs(z) > 0.01:
                # Check strides 32, 36, 40, 44, 48, 52, 56, 64
                for stride in [32, 36, 40, 44, 48, 52, 56, 64]:
                    matches = 0
                    for v in range(1, 6):
                        vx, vy, vz = struct.unpack_from("<3f", data, i + v * stride)
                        if -5.0 <= vx <= 5.5 and 0.0 <= vy <= 9.5 and -5.0 <= vz <= 5.5:
                            matches += 1
                    if matches >= 5:
                        candidates.append((i, stride))
                        break
    except:
        pass

print(f"Non-zero candidates: {len(candidates)}")
seen_strides = set()
for pos, stride in candidates:
    if stride not in seen_strides:
        seen_strides.add(stride)
        print(f"\nStride {stride} at 0x{pos:06X}:")
        for v in range(4):
            vx, vy, vz = struct.unpack_from("<3f", data, pos + v * stride)
            print(f"  V{v}: ({vx:8.4f}, {vy:8.4f}, {vz:8.4f})")
        # Check bytes preceding pos
        print(f"  Preceding 32 bytes (hex): {data[pos-32:pos].hex()}")
        # Check ints preceding pos
        ints = struct.unpack_from("<8I", data, pos - 32)
        print(f"  Preceding uints: {ints}")
