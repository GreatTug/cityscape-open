#!/usr/bin/env python3
"""
CityScape Project & Terrain Parser (CSP / CST)
----------------------------------------------
Reads and inspects PixelActive CityScape binary project (.csp) and terrain (.cst) files.
"""

import sys
import os
import struct

def parse_header(file_path):
    if not os.path.exists(file_path):
        print(f"Error: File not found: {file_path}")
        return None

    file_size = os.path.getsize(file_path)
    with open(file_path, "rb") as f:
        header = f.read(256)

    # Check for magic
    if header.startswith(b'"<PixelActive'):
        end_bracket = header.find(b'>')
        if end_bracket != -1:
            sig = header[1:end_bracket].decode('latin1', errors='ignore')
            type_str = header[end_bracket+1:end_bracket+32].decode('latin1', errors='ignore').strip()
            return {
                "file": os.path.basename(file_path),
                "size_bytes": file_size,
                "signature": sig,
                "type": type_str
            }

    return {
        "file": os.path.basename(file_path),
        "size_bytes": file_size,
        "signature": "Raw/Custom Binary",
        "type": "Unknown"
    }

def main():
    if len(sys.argv) < 2:
        print("Usage: python csp_reader.py <file.csp|file.cst|directory>")
        sys.exit(1)

    target = sys.argv[1]
    if os.path.isdir(target):
        for root, _, files in os.walk(target):
            for f in sorted(files):
                if f.lower().endswith((".csp", ".cst")):
                    info = parse_header(os.path.join(root, f))
                    if info:
                        print(f"[{info['file']}] {info['signature']} | Type: {info['type']} ({info['size_bytes']} bytes)")
    else:
        info = parse_header(target)
        if info:
            print("========================================")
            print(f"  CityScape Binary Inspector: {info['file']}")
            print("========================================")
            print(f"  File Size   : {info['size_bytes']:,} bytes")
            print(f"  Signature   : {info['signature']}")
            print(f"  Object Type : {info['type']}")
            print("========================================")

if __name__ == "__main__":
    main()
