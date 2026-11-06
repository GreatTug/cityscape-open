import os, glob

for root, dirs, files in os.walk(r"c:\cityscape-open"):
    for f in files:
        if f.lower().endswith(('.dll', '.exe')):
            path = os.path.join(root, f)
            try:
                with open(path, 'rb') as fp:
                    c = fp.read()
                    if b"bin ver" in c or b"terrain bin" in c:
                        print(f"Found in {path}!")
            except Exception:
                pass
