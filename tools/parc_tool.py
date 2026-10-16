#!/usr/bin/env python3
"""
CityScape PARC Archive Tool
---------------------------
Parses, inspects, and extracts assets from PixelActive Archive (.parc) packages.
"""

import sys
import os
import struct
import zlib
import datetime

def parse_parc(file_path):
    if not os.path.exists(file_path):
        print(f"Error: Archive not found: {file_path}")
        return None

    file_size = os.path.getsize(file_path)
    with open(file_path, "rb") as f:
        data = f.read()

    idx = 0
    version, folder_count = struct.unpack_from("<II", data, idx)
    idx += 8

    folders = []
    for _ in range(folder_count):
        flen = struct.unpack_from("<H", data, idx)[0]
        idx += 2
        fpath = data[idx:idx+flen].decode('latin1').rstrip('\x00')
        idx += flen
        folders.append(fpath)

    file_count = struct.unpack_from("<I", data, idx)[0]
    idx += 4

    files = []
    for _ in range(file_count):
        filetime, comp_size, uncomp_size, offset = struct.unpack_from("<QIII", data, idx)
        idx += 20
        namelen = struct.unpack_from("<H", data, idx)[0]
        idx += 2
        name = data[idx:idx+namelen].decode('latin1').rstrip('\x00')
        idx += namelen

        files.append({
            "name": name,
            "filetime": filetime,
            "comp_size": comp_size,
            "uncomp_size": uncomp_size,
            "offset": offset
        })

    return {
        "file_path": file_path,
        "file_size": file_size,
        "version": version,
        "folders": folders,
        "files": files,
        "data": data
    }

def inspect_parc(archive):
    print("========================================")
    print(f"  PARC Archive: {os.path.basename(archive['file_path'])}")
    print("========================================")
    print(f"  Size        : {archive['file_size']:,} bytes")
    print(f"  Version     : {archive['version']}")
    print(f"  Folders     : {len(archive['folders'])}")
    for f in archive['folders']:
        print(f"    - {f}")
    print(f"  Files Found : {len(archive['files'])}")
    print("----------------------------------------")
    for item in sorted(archive['files'], key=lambda x: x['name']):
        ratio = (item['comp_size'] / item['uncomp_size'] * 100) if item['uncomp_size'] > 0 else 0
        print(f"  - {item['name']:<40} [{item['comp_size']:>8,} -> {item['uncomp_size']:>8,} bytes ({ratio:>5.1f}%)] @ offset {item['offset']:<10}")
    print("========================================")

def extract_parc(archive, output_dir):
    os.makedirs(output_dir, exist_ok=True)
    print(f"\n---> Extracting {len(archive['files'])} assets to '{output_dir}'...")
    data = archive['data']
    extracted = 0
    total_bytes = 0

    for item in archive['files']:
        chunk = data[item['offset']:item['offset'] + item['comp_size']]
        if item['comp_size'] == item['uncomp_size']:
            decomp = chunk
        else:
            try:
                decomp = zlib.decompress(chunk)
            except Exception as e:
                print(f"  [ERROR] Failed to decompress {item['name']}: {e}")
                continue

        dest_path = os.path.join(output_dir, item['name'].replace('\\', os.sep))
        os.makedirs(os.path.dirname(dest_path), exist_ok=True)
        with open(dest_path, "wb") as out_f:
            out_f.write(decomp)

        extracted += 1
        total_bytes += len(decomp)

    print(f"[OK] Successfully extracted {extracted}/{len(archive['files'])} files ({total_bytes:,} bytes uncompressed).\n")
    return extracted

def verify_parc(archive):
    print(f"\n---> Verifying integrity of {len(archive['files'])} assets...")
    data = archive['data']
    verified = 0
    for item in archive['files']:
        chunk = data[item['offset']:item['offset'] + item['comp_size']]
        if item['comp_size'] == item['uncomp_size']:
            decomp = chunk
        else:
            try:
                decomp = zlib.decompress(chunk)
            except Exception as e:
                raise ValueError(f"Corrupt zlib stream in {item['name']}: {e}")
        if len(decomp) != item['uncomp_size']:
            raise ValueError(f"Size mismatch for {item['name']}: expected {item['uncomp_size']}, got {len(decomp)}")
        verified += 1
    print(f"[OK] All {verified} asset streams verified with 0 CRC/size mismatches.\n")
    return verified

def main():
    args = sys.argv[1:]
    default_parc = os.path.join(os.path.dirname(os.path.dirname(os.path.abspath(__file__))), "assets", "assets.parc")
    target_parc = default_parc
    extract_dir = None
    do_verify = False

    i = 0
    while i < len(args):
        arg = args[i]
        if arg in ("--extract", "-x") and i + 1 < len(args):
            extract_dir = args[i + 1]
            i += 2
        elif arg in ("--verify", "-v"):
            do_verify = True
            i += 1
        elif not arg.startswith("-"):
            target_parc = arg
            i += 1
        else:
            i += 1

    if not os.path.exists(target_parc):
        print(f"Error: Archive not found: {target_parc}")
        print(f"Usage: python parc_tool.py [archive.parc] [--extract <dir>] [--verify]")
        sys.exit(1)

    archive = parse_parc(target_parc)
    if not archive:
        sys.exit(1)

    inspect_parc(archive)

    if do_verify:
        try:
            verify_parc(archive)
        except Exception as e:
            print(f"[ERROR] Verification failed: {e}")
            sys.exit(1)

    if extract_dir:
        extracted = extract_parc(archive, extract_dir)
        if extracted != len(archive['files']):
            print(f"[ERROR] Extraction failed for some files.")
            sys.exit(1)

if __name__ == "__main__":
    main()
