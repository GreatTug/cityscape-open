import zlib

with open(r"c:\cityscape-open\testsaves\save1.cst", "rb") as f:
    data = f.read()

hdr_end = data.find(b'"', 1) + 1
payload = data[hdr_end:]

curr = 0
chunk_idx = 0
while curr < len(payload):
    # Try finding deflate stream
    found = False
    for off in range(curr, min(curr + 30, len(payload))):
        try:
            d = zlib.decompressobj(-zlib.MAX_WBITS)
            res = d.decompress(payload[off:])
            if res:
                consumed = len(payload[off:]) - len(d.unused_data)
                print(f"Chunk {chunk_idx}: off={off}, consumed={consumed}, decompressed_len={len(res)}, text/repr: {repr(res[:80])}")
                curr = off + consumed
                chunk_idx += 1
                found = True
                break
        except Exception:
            pass
    if not found:
        curr += 1
