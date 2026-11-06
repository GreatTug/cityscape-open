import os, glob

files = glob.glob("src/palDotNet/**/*.cs", recursive=True)
for f in files:
    with open(f, "r", encoding="utf-8", errors="ignore") as fp:
        txt = fp.read()
        if "ExportMeshData" in txt:
            print(f"Found in {f}")
