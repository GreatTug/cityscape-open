import struct

with open(r"Worlds/props/natural/trees/tree_oak03.gob", "rb") as f:
    data = f.read()

# Let's inspect where binary chunks start and look for vertex / index buffers
print(f"Data length: {len(data)}")

# Let's search for floats or integers that match triangle indices or coordinates
# In tree_oak03: bbox is min=(-4.60, -0.27, -4.83), max=(5.19, 9.08, 5.37)
# Vertices will have coordinates in that range (-5.0 to +10.0)!
# Let's scan for candidate vertex buffers: continuous sequences of floats in [-10.0, 15.0]

candidate_vbs = []
for i in range(0x2B7, len(data) - 1000, 4):
    # Check if next 3 floats are in coordinate range
    try:
        x, y, z = struct.unpack_from("<3f", data, i)
        if -6.0 <= x <= 6.0 and -1.0 <= y <= 11.0 and -6.0 <= z <= 6.0:
            # Check if this holds for several consecutive vertices with standard strides (32, 36, 40, 48, 52, 64)
            for stride in [32, 36, 40, 44, 48, 52, 56, 64]:
                valid_count = 0
                for v in range(1, 10):
                    vx, vy, vz = struct.unpack_from("<3f", data, i + v * stride)
                    if -6.0 <= vx <= 6.0 and -1.0 <= vy <= 11.0 and -6.0 <= vz <= 6.0:
                        valid_count += 1
                if valid_count >= 8:
                    candidate_vbs.append((i, stride))
                    break
    except:
        pass

print(f"Candidate vertex buffers found: {len(candidate_vbs)}")
for pos, stride in candidate_vbs[:10]:
    print(f"  Pos: 0x{pos:06X}, Stride: {stride}")
    # Inspect first 3 vertices
    for v in range(3):
        vx, vy, vz = struct.unpack_from("<3f", data, pos + v * stride)
        print(f"    V{v}: ({vx:8.4f}, {vy:8.4f}, {vz:8.4f})")
    # Also inspect bytes just before pos to see vertex count
    count_candidates = struct.unpack_from("<8i", data, pos - 32)
    print(f"    Preceding ints: {count_candidates}")
    break
