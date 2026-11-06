with open("CityScape.exe", "rb") as f:
    data = f.read()

terms = [
    b"StandardExportDialog",
    b"AutodeskExportDialog",
    b"DefaultExportDialog",
    b"edConvert",
    b"cvStandardExporter",
    b"cvMeshGeometryNode",
    b"coColladaExport",
    b"fbExporterFbx",
    b"ofOpenFlightExport",
    b"trTerrainExporter",
    b"rdGeometryGenerator"
]

for t in terms:
    pos = 0
    count = 0
    while True:
        pos = data.find(t, pos)
        if pos == -1:
            break
        count += 1
        pos += len(t)
    print(f"{t.decode('ascii')}: {count} occurrences")
