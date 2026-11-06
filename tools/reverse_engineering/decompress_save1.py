import zlib

path = r"C:\cityscape-open\testsaves\save1.cst"
with open(path, "rb") as f:
    data = f.read()

print(f"Total length: {len(data)}")
# Find second quote
quote2 = data.find(b'"', 1)
print("Header:", data[:quote2+1])
raw_payload = data[quote2+1:]
print("Payload length:", len(raw_payload))

# Try decompressing at various offsets
for off in range(len(raw_payload)):
    # Standard zlib
    try:
        dec = zlib.decompress(raw_payload[off:])
        print(f"Zlib decompress SUCCESS at offset {off}! Decompressed size: {len(dec)}")
        with open("c:/cityscape-open/save1_decompressed.bin", "wb") as out_f:
            out_f.write(dec)
        break
    except Exception:
        pass
    # Raw deflate
    try:
        dec = zlib.decompress(raw_payload[off:], -zlib.MAX_WBITS)
        print(f"Raw deflate SUCCESS at offset {off}! Decompressed size: {len(dec)}")
        with open("c:/cityscape-open/save1_decompressed.bin", "wb") as out_f:
            out_f.write(dec)
        break
    except Exception:
        pass
else:
    print("Could not decompress with zlib/deflate.")
