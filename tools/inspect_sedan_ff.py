with open("Worlds/props/vehicles/sedan_traffic.gob", "rb") as f:
    data = f.read()

pos = 0
matches = []
while True:
    pos = data.find(b"\xff\xff\xff\xff", pos)
    if pos == -1:
        break
    matches.append(pos)
    pos += 4

print(f"Sedan traffic: {len(matches)} occurrences of 0xFFFFFFFF")
for m in matches[:10]:
    print(f"  0x{m:X}")
    # inspect 32 bytes before and after
    print("    before:", data[max(0, m-24):m].hex())
