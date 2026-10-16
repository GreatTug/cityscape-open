import os
import re

src_dir = r"c:\cityscape-open\src"

# Pattern matching 'virtual' on explicit interface implementations, e.g.
# virtual void IFormApp.BringToFront()
# virtual void IDisposable.Dispose()
pattern = re.compile(r'\bvirtual\s+([a-zA-Z0-9_<>[\]?]+\s+([a-zA-Z0-9_]+)\.([a-zA-Z0-9_]+)\s*\()')

fixed_count = 0
for root, dirs, files in os.walk(src_dir):
    for file in files:
        if file.endswith(".cs"):
            file_path = os.path.join(root, file)
            with open(file_path, "r", encoding="utf-8") as f:
                content = f.read()

            new_content = pattern.sub(r'\1', content)
            if new_content != content:
                with open(file_path, "w", encoding="utf-8") as f:
                    f.write(new_content)
                print(f"Fixed virtual explicit interface in {file}")
                fixed_count += 1

print(f"Total files fixed: {fixed_count}")
