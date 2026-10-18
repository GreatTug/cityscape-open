import os
import sys

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
]

targets = ["CityScape.exe", r"bin\Release\net48\CityScape.exe"]
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
                print(f"[{target}] Already unlocked: {name} at 0x{off:x}")
            else:
                raise ValueError(f"[{target}] Unexpected bytes at 0x{off:x}: {cur.hex()}")
    print(f"[OK] {target} native 1600m & multi-tile limits completely unlocked!")
