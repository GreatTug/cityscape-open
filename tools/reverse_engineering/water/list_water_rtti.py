import re

with open("CityScape.exe", "rb") as f:
    data = f.read()

# RTTI type descriptors look like .?AV<class_name>@@
matches = re.findall(b"\\.\\?AV[a-zA-Z0-9_<>]+Water[a-zA-Z0-9_<>]*@@", data)
print(f"Total matching RTTI types: {len(matches)}")
for m in sorted(set(matches)):
    print(" ", m.decode('ascii'))
