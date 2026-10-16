import os

search_term = "assets.parc"
src_dir = r"c:\cityscape-open\src"

found = []
for root, dirs, files in os.walk(src_dir):
    for f in files:
        if f.endswith(".cs"):
            path = os.path.join(root, f)
            try:
                with open(path, "r", encoding="utf-8", errors="ignore") as fh:
                    content = fh.read()
                    if search_term.lower() in content.lower():
                        found.append(path)
            except Exception:
                pass

print(f"Found '{search_term}' in:")
for p in found:
    print(" -", p)
