import struct

with open("Worlds/props/natural/trees/tree_maple01.gob", "rb") as f:
    data = f.read()

print("tree_maple01 size:", len(data))
# find 0xFFFFFFFF
ff_matches = []
p = 0
while True:
    p = data.find(b"\xff\xff\xff\xff", p)
    if p == -1:
        break
    ff_matches.append(p)
    p += 4
print(f"Matches count: {len(ff_matches)}")
if ff_matches:
    print(f"First 10: {[hex(x) for x in ff_matches[:10]]}")
    diffs = [ff_matches[i+1] - ff_matches[i] for i in range(min(20, len(ff_matches)-1))]
    print("Diffs:", diffs)
