import struct

with open(r"Worlds/props/urban/traffic_signs/signs_stop01.gob", "rb") as f:
    data = f.read()

print(f"Total size: {len(data)} bytes")

# Print all strings with their positions
pos = 0
while pos < len(data):
    # Find next null-terminated printable ASCII string >= 3 chars
    m = 0
    if 32 <= data[pos] <= 126:
        start = pos
        while pos < len(data) and 32 <= data[pos] <= 126:
            pos += 1
        if pos < len(data) and data[pos] == 0 and (pos - start) >= 3:
            s = data[start:pos].decode('ascii')
            print(f"0x{start:04X}: '{s}'")
    pos += 1
