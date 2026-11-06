import re

with open("CityScape.exe", "rb") as f:
    data = f.read()

# Find mangled symbols starting with ?
mangled = re.findall(rb"\?[A-Za-z0-9_@$?]+", data)
print(f"Total mangled tokens: {len(mangled)}")

keywords = [
    b"rdGeometryGenerator",
    b"cvStandardExporter",
    b"cvMeshGeometryNode",
    b"cvPolyhedronGeometryNode",
    b"coColladaExport",
    b"trTerrainExporter",
    b"fbExporterFbx",
    b"ofOpenFlightExport",
    b"edExportRoadAccessor",
    b"edExportRoadNetworkAccessor",
    b"edExportSegment",
    b"edTrafficExport",
    b"cvExporter",
    b"gxVertexBuffer",
    b"gxIndexBuffer"
]

matches_by_kw = {}
for kw in keywords:
    matches_by_kw[kw.decode('ascii')] = set()

for m in mangled:
    for kw in keywords:
        if kw in m:
            try:
                s = m.decode('ascii')
                matches_by_kw[kw.decode('ascii')].add(s)
            except:
                pass

with open("tools/mangled_methods.txt", "w", encoding="utf-8") as out:
    for kw, syms in matches_by_kw.items():
        out.write(f"\n====================================\n")
        out.write(f"=== {kw} ({len(syms)} symbols) ===\n")
        out.write(f"====================================\n")
        for sym in sorted(syms):
            out.write(f"{sym}\n")

print("Saved demangled search to tools/mangled_methods.txt")
