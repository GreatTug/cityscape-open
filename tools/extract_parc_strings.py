import re

parc_path = r"c:\cityscape-open\assets\assets.parc"
with open(parc_path, "rb") as f:
    data = f.read()

strings = re.findall(b'[a-zA-Z0-9_./\\\\]{5,}\.[a-zA-Z0-9]{2,4}', data)
print(f"Total file-like strings found in assets.parc: {len(strings)}")
print("Sample files:")
for s in set(strings[:40]):
    print(" -", s.decode('ascii', errors='ignore'))
