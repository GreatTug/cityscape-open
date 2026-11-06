import ctypes
from ctypes import wintypes

dbghelp = ctypes.windll.dbghelp
undecorate = dbghelp.UnDecorateSymbolName
undecorate.argtypes = [ctypes.c_char_p, ctypes.c_char_p, wintypes.DWORD, wintypes.DWORD]
undecorate.restype = wintypes.DWORD

UNDNAME_COMPLETE = 0x0000

def demangle(sym):
    buffer = ctypes.create_string_buffer(1024)
    res = undecorate(sym.encode('ascii'), buffer, 1024, UNDNAME_COMPLETE)
    if res > 0:
        return buffer.value.decode('ascii', errors='ignore')
    return sym

with open("tools/mangled_methods.txt", "r", encoding="utf-8") as f:
    lines = f.readlines()

with open("tools/demangled_methods.txt", "w", encoding="utf-8") as out:
    for line in lines:
        line = line.strip()
        if not line:
            out.write("\n")
            continue
        if line.startswith("==="):
            out.write(line + "\n")
            continue
        dem = demangle(line)
        out.write(f"{line}  -->  {dem}\n")

print("Demangled symbols saved to tools/demangled_methods.txt")
