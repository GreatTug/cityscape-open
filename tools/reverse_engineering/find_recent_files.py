import os, glob

folders = [
    r'c:\cityscape-open',
    r'C:\Users\greattug\Documents',
    r'C:\Users\greattug\Desktop',
    r'C:\Users\greattug\AppData\Local\PixelActive_Inc',
    r'C:\CityScape 1.8 Autodesk Promo'
]

results = []
for folder in folders:
    if not os.path.exists(folder):
        continue
    for root, dirs, files in os.walk(folder):
        for f in files:
            ext = os.path.splitext(f)[1].lower()
            if ext in ('.csp', '.cst') or 'test' in f.lower() or 'save' in f.lower():
                path = os.path.join(root, f)
                try:
                    stat = os.stat(path)
                    results.append((stat.st_mtime, stat.st_size, path))
                except Exception:
                    pass

results.sort(reverse=True)
print(f"Found {len(results)} files matching criteria:")
for mtime, size, path in results[:30]:
    import datetime
    dt = datetime.datetime.fromtimestamp(mtime)
    print(f"{dt} | {size:>10} bytes | {path}")
