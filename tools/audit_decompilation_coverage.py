import os
import re
import struct
import pefile

pe = pefile.PE("CityScape_Old_Legacy.exe")
image_base = pe.OPTIONAL_HEADER.ImageBase

with open("CityScape_Old_Legacy.exe", "rb") as f:
    raw_data = f.read()

def va_to_offset(va):
    rva = va - image_base
    for sec in pe.sections:
        if sec.VirtualAddress <= rva < sec.VirtualAddress + sec.Misc_VirtualSize:
            return sec.PointerToRawData + (rva - sec.VirtualAddress)
    return None

# 1. Scan all RTTI classes in the binary
all_rtti = set()
for sec in pe.sections:
    if b".data" in sec.Name or b".rdata" in sec.Name:
        sec_data = raw_data[sec.PointerToRawData : sec.PointerToRawData + sec.SizeOfRawData]
        pos = 0
        while True:
            pos = sec_data.find(b".?AV", pos)
            if pos == -1:
                break
            end = sec_data.find(b"@@", pos)
            if end != -1 and end - pos < 120:
                name = sec_data[pos+4:end].decode('latin1', errors='ignore')
                all_rtti.add(name)
            pos += 4

# Group into subsystems
subsystems = {
    "Road & Junction Core (rd*)": [c for c in all_rtti if c.startswith("rd")],
    "Terrain & Urban Layout (tr*)": [c for c in all_rtti if c.startswith("tr")],
    "Core Vision / Mesh Engine (cv*)": [c for c in all_rtti if c.startswith("cv")],
    "PixelActive Framework (pa* / pal*)": [c for c in all_rtti if c.startswith("pa") or c.startswith("pal")],
    "Editor & UI Controls (ed*)": [c for c in all_rtti if c.startswith("ed")],
    "External / Utility (std*, etc.)": [c for c in all_rtti if not (c.startswith("rd") or c.startswith("tr") or c.startswith("cv") or c.startswith("pa") or c.startswith("pal") or c.startswith("ed"))]
}

print(f"Total Authentic RTTI Classes Found: {len(all_rtti)}")
for sub, lst in subsystems.items():
    print(f"  - {sub}: {len(lst)} classes")

# 2. Check source code in src/palNativeCore and src/palDotNet
cpp_headers = []
for root, dirs, files in os.walk("src/palNativeCore"):
    for f in files:
        if f.endswith((".hpp", ".h", ".cpp")):
            cpp_headers.append(os.path.join(root, f))

cs_sources = []
for root, dirs, files in os.walk("src"):
    for f in files:
        if f.endswith(".cs"):
            cs_sources.append(os.path.join(root, f))

print(f"\nReconstructed Codebase Scope:")
print(f"  - C++20 Native Source Files: {len(cpp_headers)}")
print(f"  - C# Managed Source Files:   {len(cs_sources)}")

# 3. Analyze specific functional coverage
print("\n=== SUBSYSTEM-BY-SUBSYSTEM DECOMPILATION AUDIT ===")

# Audit Road & Junction
rd_classes = sorted(subsystems["Road & Junction Core (rd*)"])
tr_road_classes = [c for c in subsystems["Terrain & Urban Layout (tr*)"] if "Road" in c]
tr_terrain_classes = [c for c in subsystems["Terrain & Urban Layout (tr*)"] if "Terrain" in c]
tr_building_classes = [c for c in subsystems["Terrain & Urban Layout (tr*)"] if "Building" in c]

print(f"\n1. Road & Intersection Engine:")
print(f"   Original rd* classes in binary: {len(rd_classes)}")
print(f"   Original trRoad* classes in binary: {len(tr_road_classes)}")
key_road_classes = [
    "rdIntersection", "rdIntersectionTessellator", "rdCornerCurveBorder",
    "rdIntersectionCurve", "rdCuldesacInfo", "rdLaneTessellator",
    "rdSidewalkTessellator", "rdRegionTessellator", "rdPolygonRegionTessellator",
    "rdNodeExtruder", "rdSegmentExtruder", "rdMergeSplitRegionBuilder",
    "trRoad", "trRoadSegment", "trRoadNode", "trRoadNetwork", "trRoadStyle", "trRoadBlock"
]
for k in key_road_classes:
    status = "FOUND in binary" if k in all_rtti else "NOT found in binary"
    print(f"     * {k:<30} -> {status}")

print(f"\n2. Terrain & DEM Engine:")
print(f"   Original trTerrain* classes in binary: {len(tr_terrain_classes)}")
key_terrain_classes = ["trTerrain", "trTerrainSettings", "trTerrainSettingsEditController", "trTerrainSettingsWrapper"]
for k in key_terrain_classes:
    status = "FOUND in binary" if k in all_rtti else "NOT found in binary"
    print(f"     * {k:<30} -> {status}")

print(f"\n3. Procedural Buildings & Architecture Engine:")
print(f"   Original trBuilding* classes in binary: {len(tr_building_classes)}")
key_bldg_classes = ["trBuilding", "trBuildingCollection", "trBuildingElement", "trBuildingRoofStyle", "trBuildingStyle", "trBuildingStyleLibrary"]
for k in key_bldg_classes:
    status = "FOUND in binary" if k in all_rtti else "NOT found in binary"
    print(f"     * {k:<30} -> {status}")

print(f"\n4. Mesh Geometry & Vertex Buffer Engine:")
cv_mesh_classes = [c for c in subsystems["Core Vision / Mesh Engine (cv*)"] if "Mesh" in c or "Poly" in c]
for k in ["cvMeshGeometryNode", "cvMeshModelNode", "cvPolygonDataNode", "cvPolyhedronGeometryNode"]:
    status = "FOUND in binary" if k in all_rtti else "NOT found in binary"
    print(f"     * {k:<30} -> {status}")
