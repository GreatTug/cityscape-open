import os
import re

print("=== CHECKING FOR CODE STUBS, UNIMPLEMENTED METHODS & TODOS ===")

stub_patterns = [
    re.compile(r"NotImplementedException", re.IGNORECASE),
    re.compile(r"throw new System\.NotImplementedException", re.IGNORECASE),
    re.compile(r"//\s*TODO", re.IGNORECASE),
    re.compile(r"//\s*FIXME", re.IGNORECASE),
    re.compile(r"return null;\s*//\s*stub", re.IGNORECASE),
    re.compile(r"\{\s*\}\s*//\s*empty", re.IGNORECASE)
]

found_stubs = []

# Scan src/palNativeCore
for root, dirs, files in os.walk("src/palNativeCore"):
    for f in files:
        if f.endswith((".cpp", ".hpp", ".h")):
            path = os.path.join(root, f)
            with open(path, "r", encoding="utf-8", errors="ignore") as file:
                lines = file.readlines()
                for idx, line in enumerate(lines):
                    for pat in stub_patterns:
                        if pat.search(line):
                            found_stubs.append((path, idx + 1, line.strip()))

# Scan src/palDotNet
for root, dirs, files in os.walk("src/palDotNet"):
    for f in files:
        if f.endswith(".cs"):
            path = os.path.join(root, f)
            with open(path, "r", encoding="utf-8", errors="ignore") as file:
                lines = file.readlines()
                for idx, line in enumerate(lines):
                    for pat in stub_patterns:
                        if pat.search(line):
                            # Filter out false positives in third party or comments that are just documentation
                            found_stubs.append((path, idx + 1, line.strip()))

print(f"Total potential stubs/TODOs found: {len(found_stubs)}")
for path, line_no, content in found_stubs[:40]:
    rel = os.path.relpath(path, ".")
    print(f"  [{rel}:{line_no}] {content}")
