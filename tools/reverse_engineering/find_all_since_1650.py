import os, datetime

start_time = datetime.datetime(2026, 10, 28, 16, 50).timestamp()

search_roots = [
    r"c:\cityscape-open",
    r"C:\Users\greattug",
]

found = []
for root_dir in search_roots:
    for root, dirs, files in os.walk(root_dir):
        # Skip noisy dirs
        if any(skip in root.lower() for skip in ['appdata\\local\\microsoft', 'appdata\\local\\temp', '.gemini', 'node_modules', '.git']):
            continue
        for f in files:
            p = os.path.join(root, f)
            try:
                mtime = os.path.getmtime(p)
                if mtime >= start_time:
                    found.append((mtime, os.path.getsize(p), p))
            except Exception:
                pass

found.sort(reverse=True)
print(f"Total files modified after 16:50 (excluding noise): {len(found)}")
for mtime, size, p in found[:50]:
    dt = datetime.datetime.fromtimestamp(mtime)
    print(f"{dt} | {size:>10} bytes | {p}")
