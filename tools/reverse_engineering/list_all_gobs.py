import os

bld_dir = r"c:\cityscape-open\Worlds\props\buildings"
print("Buildings:")
for f in sorted(os.listdir(bld_dir)):
    print(f"  {f} ({os.path.getsize(os.path.join(bld_dir, f))} bytes)")

print("\nOther props:")
for root, dirs, files in os.walk(r"c:\cityscape-open\Worlds\props"):
    if root == bld_dir:
        continue
    for f in sorted(files):
        if f.endswith((".gob", ".gox")):
            rel = os.path.relpath(os.path.join(root, f), r"c:\cityscape-open\Worlds\props")
            print(f"  {rel}")
