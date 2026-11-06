import pefile
import ctypes
from ctypes import wintypes

pe = pefile.PE("CityScape.exe")
dbghelp = ctypes.windll.dbghelp
undecorate = dbghelp.UnDecorateSymbolName
undecorate.argtypes = [ctypes.c_char_p, ctypes.c_char_p, wintypes.DWORD, wintypes.DWORD]
undecorate.restype = wintypes.DWORD

def demangle(sym):
    buf = ctypes.create_string_buffer(2048)
    if undecorate(sym.encode('ascii'), buf, 2048, 0) > 0:
        return buf.value.decode('ascii', errors='ignore')
    return sym

exports = []
if hasattr(pe, 'DIRECTORY_ENTRY_EXPORT'):
    for exp in pe.DIRECTORY_ENTRY_EXPORT.symbols:
        if exp.name:
            mangled = exp.name.decode('ascii', errors='ignore')
            exports.append((mangled, demangle(mangled), exp.address))

print(f"Total exported symbols: {len(exports)}")

# Let's filter into relevant categories
categories = {
    "geometry": [],
    "export": [],
    "terrain": [],
    "road": [],
    "building": [],
    "mesh": [],
    "collada": [],
    "fbx": [],
    "material": []
}

for mangled, dem, addr in exports:
    dem_lower = dem.lower()
    entry = f"0x{addr:08X} | {dem} (mangled: {mangled})\n"
    if "geom" in dem_lower:
        categories["geometry"].append(entry)
    if "export" in dem_lower:
        categories["export"].append(entry)
    if "terrain" in dem_lower or "tile" in dem_lower:
        categories["terrain"].append(entry)
    if "road" in dem_lower or "intersection" in dem_lower or "lane" in dem_lower:
        categories["road"].append(entry)
    if "building" in dem_lower or "lot" in dem_lower or "roof" in dem_lower:
        categories["building"].append(entry)
    if "mesh" in dem_lower or "vertex" in dem_lower or "index" in dem_lower:
        categories["mesh"].append(entry)
    if "collada" in dem_lower or "dae" in dem_lower:
        categories["collada"].append(entry)
    if "fbx" in dem_lower:
        categories["fbx"].append(entry)
    if "material" in dem_lower or "texture" in dem_lower or "shader" in dem_lower:
        categories["material"].append(entry)

with open("tools/exported_pipeline_symbols.txt", "w", encoding="utf-8") as out:
    for cat, items in categories.items():
        out.write(f"\n==================================================\n")
        out.write(f"=== CATEGORY: {cat.upper()} ({len(items)} symbols) ===\n")
        out.write(f"==================================================\n")
        for it in items[:150]:  # sample top 150 per category
            out.write(it)

print("Saved category breakdown to tools/exported_pipeline_symbols.txt")
