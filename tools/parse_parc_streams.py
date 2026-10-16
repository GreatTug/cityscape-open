import os
import re

parc_path = r"c:\cityscape-open\assets\assets.parc"
out_dir = r"c:\cityscape-open\assets\extracted"
os.makedirs(out_dir, exist_ok=True)

with open(parc_path, "rb") as f:
    data = f.read()

# Let's locate embedded files by signature or metadata
# DDS files start with b'DDS ' (0x20534444)
# Text files like BrightStarCatalog.txt
# FXO shaders start with b'DXBC' or b'\xff\xfe' or Direct3D shader magic

# Let's search for filenames in the archive table
# We noticed earlier: 2-byte length + filename + flags/offset/size
pattern = re.compile(b'([a-zA-Z0-9_./\\\\]{3,}\.(dds|DDS|txt|fxo|fx|bmp|png|xml))')
matches = list(pattern.finditer(data))
print(f"Found {len(matches)} filename matches in data")

# Let's extract DDS files directly by magic
dds_magic = b'DDS \x7c\x00\x00\x00'
dds_offsets = []
start = 0
while True:
    pos = data.find(dds_magic, start)
    if pos == -1:
        break
    dds_offsets.append(pos)
    start = pos + 4

print(f"Found {len(dds_offsets)} DDS file streams")
