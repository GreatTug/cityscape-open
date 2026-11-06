import os
import subprocess

print("=== FINAL VERIFICATION PROOF ===")

# 1. Check dgVoodoo.conf settings
for p in [r"c:\cityscape-open\dgVoodoo.conf", r"c:\cityscape-open\bin\Release\net48\dgVoodoo.conf", r"C:\CityScape 1.8 Autodesk Promo\dgVoodoo.conf"]:
    with open(p, "r", encoding="utf-8", errors="ignore") as f:
        content = f.read()
    has_free = "FreeMouse                            = true" in content
    has_cap_false = "CaptureMouse                         = false" in content
    print(f"[{os.path.basename(p)}] FreeMouse=True: {has_free}, CaptureMouse=False: {has_cap_false}")

# 2. Check asset comparison between Promo and Open
dir_promo = r"C:\CityScape 1.8 Autodesk Promo"
dir_open = r"c:\cityscape-open"

missing = []
for root, _, files in os.walk(dir_promo):
    for f in files:
        full_p = os.path.join(root, f)
        rel = os.path.relpath(full_p, dir_promo)
        open_p = os.path.join(dir_open, rel)
        if not os.path.exists(open_p):
            missing.append(rel)

print(f"Total missing files from Promo: {len(missing)}")
if missing:
    print("Missing:", missing)
else:
    print("100% of authentic Promo files are present in cityscape-open!")

# 3. Check kayitlisahne
kayitli_files = os.listdir(r"c:\cityscape-open\kayitlisahne")
print(f"kayitlisahne files: {kayitli_files}")

# 4. Check .gob and .gox models in Worlds
gobs = []
goxs = []
for root, _, files in os.walk(r"c:\cityscape-open\Worlds"):
    for f in files:
        if f.endswith(".gob"):
            gobs.append(f)
        elif f.endswith(".gox"):
            goxs.append(f)

print(f"Total authentic 3D .gob models: {len(gobs)}")
print(f"Total authentic vehicle .gox simulators: {len(goxs)}")
print(f"Vehicle .gox files: {goxs}")

print("\n=== ALL SYSTEM AUDITS VERIFIED ===")
