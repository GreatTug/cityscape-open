import os
import sys
import struct

patches = [
    # Func 1: ValidateTerrain -> return true (al = 1)
    ("Func1 Validation", 0x52520, b"\x8b\x4c\x24\x04", b"\xb0\x01\xc3\x90"),
    # Func 2: Nop 4 reject jumps and jump to success
    ("Func2 ja1",        0x2f4102, b"\x77\x2d", b"\x90\x90"),
    ("Func2 ja2",        0x2f4107, b"\x77\x28", b"\x90\x90"),
    ("Func2 ja3",        0x2f4121, b"\x77\x0e", b"\x90\x90"),
    ("Func2 ja4",        0x2f4126, b"\x77\x09", b"\x90\x90"),
    ("Func2 jbe->jmp",   0x2f412b, b"\x0f\x86\xbf\x00\x00\x00", b"\xe9\xc0\x00\x00\x00\x90"),
    # Func 3: Nop 4 reject jumps and jump to success
    ("Func3 ja1",        0x2f5052, b"\x77\x2f", b"\x90\x90"),
    ("Func3 ja2",        0x2f5057, b"\x77\x2a", b"\x90\x90"),
    ("Func3 ja3",        0x2f5075, b"\x77\x0c", b"\x90\x90"),
    ("Func3 ja4",        0x2f5079, b"\x77\x08", b"\x90\x90"),
    ("Func3 jbe->jmp",   0x2f507d, b"\x0f\x86\x8a\x00\x00\x00", b"\xe9\x8b\x00\x00\x00\x90"),
    # Float 1600.0f in .rdata -> 10,000,000.0f
    ("Const 1600.0f",    0x1ecc10c, b"\x00\x00\xc8\x44", b"\x80\x96\x18\x4b"),
    # Native MaxTerrainSize in Terrain Properties (edTerrainCommand 0x62cfc0): 1600 -> 10,000,000
    ("MaxTerrainSize Properties", 0x22c451, b"\x40\x06\x00\x00", b"\x80\x96\x98\x00"),
    # Native MaxTerrainSize in New Terrain (0x70be00): 1600 -> 10,000,000
    ("MaxTerrainSize NewTerrain", 0x30b4ba, b"\x40\x06\x00\x00", b"\x80\x96\x98\x00"),
    # Synchronize New Terrain Global Grid (the white operatable surface) with TerrainSizeX & TerrainSizeY
    # instead of hardcoding to 160 * SamplingDistance (1600m).
    ("NewTerrain GlobalGrid Extent Sync", 0x30b7a1,
     b"\xf3\x0f\x2a\x40\x70\xd9\x58\x6c\xf3\x0f\x59\x44\x24\x30\x8d\x4c\x24\x18\x51\x0f\x28\xc8\x8d\x48\x74\xf3\x0f\x11\x4c\x24\x1c\xf3\x0f\x11\x44\x24\x20",
     bytes.fromhex("D9586C8B8DA8000000894C24188B95AC0000008954241C8D4C2418518D4874909090909090")),
    # Dynamic Elevation Grid (Board) Extent Sync via Code Cave at 0x006E4C40 (All Loads & Creations)
    ("CallSite 0x006E4D96 Hook", 0x2e4196,
     b"\xe8\xb5\x46\xf4\xff",
     b"\xe8\x95\x64\x5d\x01"),
    ("GlobalGrid Extent Cave",   0x18ba630,
     b"\x00" * 61,
     bytes.fromhex("E81BE296FE5356578B567C85D2742A8B424885C074238B485485C9741C8B426485C074158B504489516C8D80A80000005083C174E8870521FF5F5E5BC3")),
    # Dynamic Elevation Grid (Board) Extent Sync in Terrain Properties (Options -> Terrain Settings)
    ("TerrainProperties GlobalGrid Extent CallSite", 0x22c630,
     b"\xe8\xbb\xe5\x89\x00",
     bytes.fromhex("E86BE06801")),
    ("TerrainProperties GlobalGrid Extent Cave", 0x18ba6a0,
     b"\x00" * 85,
     bytes.fromhex("ff742404e8470521ff535657e8000000005a81c2676e43018b1285d274318b527c85d2742a8b424885c074238b485485c9741c8b461885c074158b504489516c8d80a80000005083c174e8010521ff5f5e5bc20400")),
    # Viewport prTileBoard (White workable elevation surface) Extent Sync via Code Cave at 0x01CBB300
    ("Viewport prTileBoard CallSite 0x00AE3A00 Hook", 0x6e2e00,
     b"\x83\xec\x08\x56\x8b\xf1",
     bytes.fromhex("e9fb781d0190")),
    ("Viewport prTileBoard Extent Sync Cave", 0x18ba700,
     b"\x00" * 182,
     bytes.fromhex("83ec08568bf18b86f400000085c074268b406485c0741ff30f1080a8000000f30f1088ac0000000f57d20f2fc276070f2fca7602eb268b86a400000085c074168b885001000049f30f2ac1f30f594424100f28c8eb060f57c00f57c9f30f11442404f30f114c24080f2e86840000007a0d750b0f2e8e880000007a0274248b96d800000085d274088b4a24e8e0a4e0fe8b86dc00000085c074088b4824e8cea4e0fe8d4c2404518d4e48e8410421ff5e83c408c20400")),
    # Remove "[Autodesk Promo]" branding from window titles (NUL padded, same length)
    ("Title 1.8",        0x1fcb1b4, b"PixelActive CityScape 1.8 [Autodesk Promo]", b"PixelActive CityScape 1.8".ljust(42, b"\x00")),
    ("Title short",      0x222b721, b"CityScape [Autodesk Promo]", b"CityScape".ljust(26, b"\x00")),
    # Version stamp written into saved projects (same length, NUL padded)
    ("Version stamp",    0x1fcb0f8, b"PixelActive CityScape 1.8.3.34904 [Autodesk Promo]", b"PixelActive CityScape 1.8.3.34904".ljust(50, b"\x00")),
    # Promotional-limit message -> bare feature name
    ("Promo limit msg",  0x1ee7250, b"%s is not supported in this promotional version of CityScape.", b"%s".ljust(61, b"\x00")),
    # Autodesk copyright line
    ("Autodesk copyright", 0x1fddd14, b" Copyright (C) 1997-2008 Autodesk Inc. and/or its licensors.", b"\x00" * 60),
]

default_targets = [
    "CityScape.exe",
    "CityScape_Old_Legacy.exe",
    r"bin\Release\net48\CityScape.exe",
    r"C:\CityScape 1.8 Autodesk Promo\CityScape.exe",
]

targets = sys.argv[1:] if len(sys.argv) > 1 else default_targets
for target in targets:
    if not os.path.exists(target):
        continue
    # Only patch original 32-bit native binary (~32MB+)
    size = os.path.getsize(target)
    if size < 20_000_000:
        continue
    try:
        with open(target, "r+b") as f:
            # 1. Enable Large Address Aware (LAA - 4GB Virtual Memory on 64-bit Windows)
            f.seek(0x3C)
            e_lfanew_bytes = f.read(4)
            if len(e_lfanew_bytes) == 4:
                e_lfanew = struct.unpack("<I", e_lfanew_bytes)[0]
                f.seek(e_lfanew)
                pe_magic = f.read(4)
                if pe_magic == b"PE\x00\x00":
                    char_offset = e_lfanew + 22
                    f.seek(char_offset)
                    characteristics = struct.unpack("<H", f.read(2))[0]
                    if not (characteristics & 0x0020):
                        new_characteristics = characteristics | 0x0020
                        f.seek(char_offset)
                        f.write(struct.pack("<H", new_characteristics))
                        print(f"[{target}] Enabled Large Address Aware (4GB RAM): 0x{characteristics:04X} -> 0x{new_characteristics:04X}")
                    else:
                        print(f"[{target}] Large Address Aware (4GB RAM) already enabled: 0x{characteristics:04X}")

            # 2. Apply surgical native binary patches
            for name, off, orig, rep in patches:
                if off + len(orig) > size:
                    continue
                f.seek(off)
                cur = f.read(len(orig))
                buggy_sync = bytes.fromhex("D9586C8B8DA8000000894C241C8B95AC000000895424208D4C2418518D4874909090909090")
                is_prev_cave = (name == "TerrainProperties GlobalGrid Extent Cave" and cur.startswith(b"\xE8\x4B\x05\x21\xFF"))
                if cur == orig or (name == "NewTerrain GlobalGrid Extent Sync" and cur == buggy_sync) or is_prev_cave:
                    f.seek(off)
                    f.write(rep)
                    print(f"[{target}] Applied patch: {name} at 0x{off:x} ({cur.hex()[:20]}... -> {rep.hex()[:20]}...)")
                elif cur == rep:
                    pass # Already patched
                else:
                    print(f"[{target}] Skipping {name} at 0x{off:x} - bytes mismatch ({cur.hex()[:20]}... != {orig.hex()[:20]}...)")
        print(f"[OK] {target} processed successfully!")
    except Exception as ex:
        print(f"[WARNING] Could not patch {target}: {ex}")
