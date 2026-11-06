import os

found = []
for root, dirs, files in os.walk(r"src\edEditorDotNet"):
    for file in files:
        if file.endswith(".cs"):
            p = os.path.join(root, file)
            with open(p, "r", encoding="utf-8", errors="ignore") as f:
                content = f.read()
            if "water" in content.lower():
                found.append(p)

print(f"Files containing 'water' in edEditorDotNet: {len(found)}")
for p in found:
    print(" ", p)
