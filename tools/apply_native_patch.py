import os

patches = [
    ("Func1 Validation", 0x52520, b"\x8b\x4c\x24\x04", b"\xb0\x01\xc3\x90"),
    ("Func2 Create",     0x2f40e0, b"\x8b\x44\x24\x10", b"\xe9\x0b\x01\x00\x00"),
    ("Func3 TileInit",   0x2f5030, b"\x8b\x44\x24\x0c", b"\xe9\xd8\x00\x00\x00"),
    ("Const 1600.0f #1", 0x1ecc10c, b"\x00\x00\xc8\x44", b"\x00\x00\xbe\x4c"),
    ("Const 1600.0f #2", 0x2c4d204, b"\x00\x00\xc8\x44", b"\x00\x00\xbe\x4c"),
]

targets = [r"lib\CityScape.exe", "CityScape.exe", r"bin\Release\net48\CityScape.exe"]
for target in targets:
    if not os.path.exists(target):
        continue
    with open(target, "r+b") as f:
        for name, off, orig, rep in patches:
            f.seek(off)
            cur = f.read(len(orig))
            if cur == orig:
                f.seek(off)
                f.write(rep)
                print(f"[{target}] Applied patch: {name} at 0x{off:x} ({orig.hex()} -> {rep.hex()})")
            elif cur == rep:
                print(f"[{target}] Already patched: {name} at 0x{off:x}")
            else:
                raise ValueError(f"[{target}] Unexpected bytes at 0x{off:x}: {cur.hex()}")
    print(f"{target} unlocked successfully!")
