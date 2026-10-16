#!/usr/bin/env python3
"""
CityScape Project & Terrain Parser (CSP / CST)
----------------------------------------------
Reads and inspects PixelActive CityScape binary project (.csp) and terrain (.cst) files.
"""

import sys
import os
import uuid

def parse_header(file_path):
    if not os.path.exists(file_path):
        print(f"Error: File not found: {file_path}")
        return None

    file_size = os.path.getsize(file_path)
    with open(file_path, "rb") as f:
        data = f.read(512)

    # Check for magic
    if data.startswith(b'"<PixelActive'):
        end_bracket = data.find(b'>')
        closing_quote = data.find(b'"', 1)
        if end_bracket != -1 and closing_quote != -1:
            sig = data[1:end_bracket].decode('latin1', errors='ignore').lstrip('<')
            type_str = data[end_bracket+1:closing_quote].decode('latin1', errors='ignore').strip()
            
            # 16-byte GUID directly follows closing quote
            guid_str = "None"
            payload_offset = closing_quote + 1
            if len(data) >= payload_offset + 16:
                guid_bytes = data[payload_offset:payload_offset+16]
                try:
                    guid_str = str(uuid.UUID(bytes_le=guid_bytes))
                except Exception:
                    guid_str = guid_bytes.hex()

            return {
                "file": os.path.basename(file_path),
                "path": file_path,
                "size_bytes": file_size,
                "signature": f"PixelActive {sig}" if not sig.startswith("PixelActive") else sig,
                "type": type_str,
                "guid": guid_str,
                "payload_bytes": max(0, file_size - payload_offset)
            }

    return {
        "file": os.path.basename(file_path),
        "path": file_path,
        "size_bytes": file_size,
        "signature": "Raw/Custom Binary",
        "type": "Unknown",
        "guid": "N/A",
        "payload_bytes": file_size
    }

def inspect_file(info):
    print("========================================")
    print(f"  CityScape Binary Inspector: {info['file']}")
    print("========================================")
    print(f"  File Size     : {info['size_bytes']:,} bytes")
    print(f"  Signature     : {info['signature']}")
    print(f"  Object Type   : {info['type']}")
    print(f"  Session GUID  : {info['guid']}")
    print(f"  Payload Size  : {info['payload_bytes']:,} bytes")
    print("========================================")

def main():
    if len(sys.argv) < 2:
        print("Usage: python csp_reader.py <file.csp|file.cst|directory>")
        sys.exit(1)

    target = sys.argv[1]
    if not os.path.exists(target):
        print(f"Error: Path not found: {target}")
        sys.exit(1)

    if os.path.isdir(target):
        print("========================================")
        print(f"  Scanning Directory: {target}")
        print("========================================")
        count = 0
        for root, _, files in os.walk(target):
            for f in sorted(files):
                if f.lower().endswith((".csp", ".cst")):
                    info = parse_header(os.path.join(root, f))
                    if info:
                        print(f"  [{info['file']}] {info['signature']} | Type: {info['type']} ({info['size_bytes']:,} bytes, GUID: {info['guid']})")
                        count += 1
        print(f"\n[OK] Scanned and verified {count} world/terrain files.\n")
    else:
        info = parse_header(target)
        if not info:
            sys.exit(1)
        inspect_file(info)

if __name__ == "__main__":
    main()
