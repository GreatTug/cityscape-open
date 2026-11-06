import os

path = r"C:\Users\greattug\Documents\3ds Max 2026\test.csp"
with open(path, "rb") as f:
    data = f.read()

print("Length:", len(data))
print("Data raw:")
print(repr(data))

# Also search for strings
import re
print("Strings >= 3 chars:")
for s in re.findall(rb'[\x20-\x7e]{3,}', data):
    print(" ", s.decode('latin1'))
