import hashlib
import os

files = [
    'CityScape_1.8_Decompiled_Edition_Complete.zip',
    'CityScape_1.8_Legacy_x86_Portable.zip',
    'CityScape_1.8_Modern_ARM64_Portable.zip',
    'CityScape_1.8_Modern_x64_Portable.zip',
    'CityScape_Blender_Addon.zip',
    'CityScape_UE5_Plugin.zip'
]

lines = []
for f in files:
    p = os.path.join('dist', f)
    if os.path.exists(p):
        h = hashlib.sha256()
        with open(p, 'rb') as fp:
            while chunk := fp.read(65536):
                h.update(chunk)
        lines.append(f"{h.hexdigest()}  {f}\n")

with open(os.path.join('dist', 'SHA256SUMS.txt'), 'w', encoding='utf-8') as fp:
    fp.writelines(lines)

print("Updated dist/SHA256SUMS.txt:")
for l in lines:
    print("  " + l.strip())
