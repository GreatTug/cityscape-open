import pefile
import re

with open("bin/Release/net48/edEditorDotNet.dll", "rb") as f:
    data = f.read()

# Search for WaterGlobal.png
idx = data.find(b"WaterGlobal.png")
print("Found WaterGlobal.png in edEditorDotNet.dll:", idx)

# Find all occurrences of MainToolBarIcons
matches = re.findall(b"MainToolBarIcons\\.[a-zA-Z0-9_]+\\.png", data)
print(f"Total MainToolBarIcons found: {len(matches)}")
for m in sorted(set(matches)):
    print(" ", m.decode('ascii'))
