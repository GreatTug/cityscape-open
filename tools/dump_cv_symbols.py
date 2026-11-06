import re
import pefile

pe = pefile.PE("CityScape.exe")
image_base = pe.OPTIONAL_HEADER.ImageBase

with open("CityScape.exe", "rb") as f:
    data = f.read()

# Let's search for all mangled names with cvMeshGeometryNode or cvGeometryNode or cvStandardExporter
mangled = re.findall(rb"\?[A-Za-z0-9_@$?]+", data)

target_symbols = []
for m in mangled:
    if b"cvMeshGeometryNode" in m or b"cvGeometryNode" in m or b"cvStandardExporter" in m or b"cvExporter" in m:
        try:
            target_symbols.append(m.decode('ascii'))
        except:
            pass

import ctypes
from ctypes import wintypes
dbghelp = ctypes.windll.dbghelp
undecorate = dbghelp.UnDecorateSymbolName
undecorate.argtypes = [ctypes.c_char_p, ctypes.c_char_p, wintypes.DWORD, wintypes.DWORD]
undecorate.restype = wintypes.DWORD

def demangle(sym):
    buf = ctypes.create_string_buffer(1024)
    if undecorate(sym.encode('ascii'), buf, 1024, 0) > 0:
        return buf.value.decode('ascii', errors='ignore')
    return sym

results = [(sym, demangle(sym)) for sym in sorted(set(target_symbols))]

with open("tools/cv_demangled.txt", "w", encoding="utf-8") as out:
    for sym, dem in results:
        out.write(f"{sym}\n   -> {dem}\n")

print(f"Demangled {len(results)} cv exporter/geometry symbols to tools/cv_demangled.txt")
