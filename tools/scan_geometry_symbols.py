import re
import pefile

pe = pefile.PE("CityScape.exe", fast_load=True)
pe.parse_data_directories()

with open("CityScape.exe", "rb") as f:
    data = f.read()

# Search for interesting symbols / class names in RTTI and ascii strings
patterns = [
    rb"\.?\?AV[A-Za-z0-9_]+@@",  # MSVC type_info name
    rb"[a-zA-Z0-9_]+Exporter[a-zA-Z0-9_]*",
    rb"cv[A-Z][a-zA-Z0-9_]+",
    rb"coCollada[a-zA-Z0-9_]+",
    rb"fbExporter[a-zA-Z0-9_]+",
    rb"ofOpenFlight[a-zA-Z0-9_]+",
    rb"[a-zA-Z0-9_]*Geometry[a-zA-Z0-9_]*",
    rb"[a-zA-Z0-9_]*VertexBuffer[a-zA-Z0-9_]*",
    rb"[a-zA-Z0-9_]*IndexBuffer[a-zA-Z0-9_]*",
]

results = set()
for pat in patterns:
    matches = re.findall(pat, data)
    for m in matches:
        try:
            s = m.decode('ascii')
            results.add(s)
        except:
            pass

print(f"Total matching tokens found: {len(results)}")

# Group by category
cv_classes = sorted([s for s in results if s.startswith("cv")])
export_classes = sorted([s for s in results if "Export" in s])
geom_classes = sorted([s for s in results if "Geom" in s or "Mesh" in s or "Buffer" in s])
rtti_types = sorted([s for s in results if s.startswith(".?AV")])

with open("tools/geometry_pipeline_scan.txt", "w", encoding="utf-8") as out:
    out.write("=== CV CLASSES ===\n")
    for s in cv_classes:
        out.write(f"  {s}\n")
    out.write("\n=== EXPORT CLASSES ===\n")
    for s in export_classes:
        out.write(f"  {s}\n")
    out.write("\n=== GEOMETRY / MESH / BUFFER CLASSES ===\n")
    for s in geom_classes:
        out.write(f"  {s}\n")
    out.write("\n=== RTTI TYPES ===\n")
    for s in rtti_types[:100]:
        out.write(f"  {s}\n")

print("Saved scan to tools/geometry_pipeline_scan.txt")
