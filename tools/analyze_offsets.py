import pefile

pe = pefile.PE("CityScape.exe")
image_base = pe.OPTIONAL_HEADER.ImageBase

with open("CityScape.exe", "rb") as f:
    data = f.read()

def rva_to_va(rva):
    return image_base + rva

def file_offset_to_va(offset):
    for sec in pe.sections:
        if sec.PointerToRawData <= offset < sec.PointerToRawData + sec.SizeOfRawData:
            rva = sec.VirtualAddress + (offset - sec.PointerToRawData)
            return rva_to_va(rva), sec.Name.decode('ascii', errors='ignore').strip('\x00')
    return None, None

terms = [
    b"trTerrainExporter",
    b"rdGeometryGenerator",
    b"StandardExportDialog",
    b"AutodeskExportDialog",
    b"DefaultExportDialog",
    b"cvMeshGeometryNode",
    b"coColladaExport"
]

with open("tools/export_offsets.txt", "w", encoding="utf-8") as out:
    for t in terms:
        out.write(f"\n=== {t.decode('ascii')} ===\n")
        pos = 0
        while True:
            pos = data.find(t, pos)
            if pos == -1:
                break
            va, sec = file_offset_to_va(pos)
            va_str = f"0x{va:08X}" if va else "unknown"
            # Get surrounding ascii string
            start = max(0, pos - 40)
            end = min(len(data), pos + len(t) + 40)
            snippet = data[start:end].replace(b'\x00', b' ')
            out.write(f"Offset 0x{pos:08X} | VA {va_str} | Sec {sec} | Snippet: {snippet}\n")
            pos += len(t)

print("Export offsets written to tools/export_offsets.txt")
