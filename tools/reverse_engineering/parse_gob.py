import re

with open(r"c:\cityscape-open\Worlds\props\natural\trees\tree_oak03.gob", "rb") as f:
    data = f.read()

print(f"Total size: {len(data)}")
strings = []
for m in re.finditer(b'[\x20-\x7e]{4,}', data):
    strings.append((m.start(), m.group().decode('ascii', errors='replace')))

print(f"Found {len(strings)} ASCII strings:")
for pos, s in strings[:40]:
    print(f"0x{pos:06x}: {s}")
