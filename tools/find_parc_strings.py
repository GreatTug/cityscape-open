import re

exe_path = r"C:\CityScape 1.8 Autodesk Promo\CityScape.exe"
with open(exe_path, "rb") as f:
    data = f.read()

matches = re.findall(b'[\x20-\x7E]{4,}\.parc', data, re.IGNORECASE)
print("PARC references in CityScape.exe:")
for m in set(matches):
    print(" -", m.decode('ascii', errors='ignore'))
