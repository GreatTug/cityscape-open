import os

dir_worlds = r"c:\cityscape-open\Worlds"

file_types = {}
for root, _, files in os.walk(dir_worlds):
    for f in files:
        ext = os.path.splitext(f)[1].lower()
        file_types[ext] = file_types.get(ext, 0) + 1

print("File types in Worlds:", file_types)

for root, dirs, files in os.walk(dir_worlds):
    for d in dirs:
        print(f"Dir: {os.path.relpath(os.path.join(root, d), dir_worlds)}")
