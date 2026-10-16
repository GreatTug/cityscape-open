import zlib

parc_path = r"c:\cityscape-open\assets\assets.parc"
with open(parc_path, "rb") as f:
    data = f.read()

# Let's search for zlib headers 78 9c or 78 da
for offset in range(0, min(1000, len(data) - 2)):
    if data[offset:offset+2] in [b'\x78\x9c', b'\x78\xda', b'\x78\x01', b'\x78\x5e']:
        try:
            decomp = zlib.decompress(data[offset:offset+65536])
            print(f"ZLIB DECOMPRESSED successfully at offset {offset}! Unpacked size: {len(decomp)}")
            print("Preview:", decomp[:64])
            break
        except Exception:
            pass
