with open(r"Worlds/props/urban/traffic_signs/signs_stop01.gob", "rb") as f:
    data = f.read()

pos = 0x300
ffs = []
while True:
    pos = data.find(b"\xff\xff\xff\xff", pos)
    if pos == -1 or pos > 0x1C00:
        break
    ffs.append(pos)
    pos += 1

print(f"Total 0xFFFFFFFF found: {len(ffs)}")
diffs = [ffs[i+1] - ffs[i] for i in range(min(20, len(ffs)-1))]
print("Differences between consecutive 0xFFFFFFFF:", diffs)
