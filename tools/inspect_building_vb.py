with open("Worlds/props/buildings/downtown0010_0.gob", "rb") as f:
    data = f.read()

print("Downtown0010_0 size:", len(data))
# find all occurrences of 0xFFFFFFFF
ff_matches = []
pos = 0
while True:
    pos = data.find(b"\xff\xff\xff\xff", pos)
    if pos == -1:
        break
    ff_matches.append(pos)
    pos += 4

print(f"Total 0xFFFFFFFF occurrences: {len(ff_matches)}")
if ff_matches:
    print(f"First 10 matches: {[hex(x) for x in ff_matches[:10]]}")
    # check differences between consecutive matches
    diffs = [ff_matches[i+1] - ff_matches[i] for i in range(min(20, len(ff_matches)-1))]
    print("Diffs between matches:", diffs)
