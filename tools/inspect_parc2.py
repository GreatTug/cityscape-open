import struct
import os

parc_path = r"c:\cityscape-open\assets\assets.parc"
with open(parc_path, "rb") as f:
    data = f.read()

print(f"Total size: {len(data)} bytes")
# Let's inspect strings and offsets in the header
idx = 0
version, count = struct.unpack_from("<II", data, 0)
print(f"Header: version={version}, count={count}")
idx = 8
entries = []
for i in range(min(count, 50)):
    if idx >= len(data):
        break
    str_len = struct.unpack_from("<H", data, idx)[0]
    idx += 2
    name = data[idx:idx+str_len].decode('latin1', errors='ignore')
    idx += str_len
    # check following data
    print(f"Entry {i}: name={name}, at offset={idx}")
    # Let's see what follows each name
    idx += 8  # peek
    if i >= 10:
        break
