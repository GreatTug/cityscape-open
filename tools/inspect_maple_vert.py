import struct

with open("Worlds/props/natural/trees/tree_maple01.gob", "rb") as f:
    data = f.read()

off = 0x1339c
# inspect 88 bytes before and around 0x1339c
print("Around 0x1339c:")
for delta in range(-64, 96, 4):
    fval = struct.unpack_from("<f", data, off + delta)[0]
    uval = struct.unpack_from("<I", data, off + delta)[0]
    print(f"  {delta:+3d} (0x{off+delta:X}): float={fval:12.4f}, uint=0x{uval:08X}")
