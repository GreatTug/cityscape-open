#!/usr/bin/env python3
"""
CityScape PARC Archive Tool
---------------------------
Lists and extracts metadata from PixelActive Archive (.parc) packages.
"""

import sys
import os
import re

def inspect_parc(file_path):
    if not os.path.exists(file_path):
        print(f"Error: Archive not found: {file_path}")
        return

    file_size = os.path.getsize(file_path)
    with open(file_path, "rb") as f:
        data = f.read()

    pattern = re.compile(b'([a-zA-Z0-9_./\\\\]{3,}\.(dds|DDS|txt|fxo|fx|bmp|png|xml))')
    matches = list(set(m.group(0).decode('latin1', errors='ignore') for m in pattern.finditer(data)))

    print("========================================")
    print(f"  PARC Archive: {os.path.basename(file_path)}")
    print("========================================")
    print(f"  Size        : {file_size:,} bytes")
    print(f"  Files Found : {len(matches)}")
    print("----------------------------------------")
    for f in sorted(matches):
        print(f"  - {f}")
    print("========================================")

def main():
    if len(sys.argv) < 2:
        default_parc = r"c:\cityscape-open\assets\assets.parc"
        if os.path.exists(default_parc):
            inspect_parc(default_parc)
        else:
            print("Usage: python parc_tool.py <archive.parc>")
    else:
        inspect_parc(sys.argv[1])

if __name__ == "__main__":
    main()
