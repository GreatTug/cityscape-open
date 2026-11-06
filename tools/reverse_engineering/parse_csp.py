import zlib

with open(r"C:\Users\greattug\Documents\3ds Max 2026\test.csp", "rb") as f:
    data = f.read()

hdr_end = data.find(b'"', 1) + 1
payload = data[hdr_end:]

curr = 0
chunk_idx = 0
while curr < len(payload):
    found = False
    for off in range(curr, min(curr + 30, len(payload))):
        try:
            d = zlib.decompressobj(-zlib.MAX_WBITS)
            res = d.decompress(payload[off:])
            if res:
                consumed = len(payload[off:]) - len(d.unused_data)
                print(f"CSP Chunk {chunk_idx}: off={off}, consumed={consumed}, decompressed_len={len(res)}, text/repr: {repr(res[:120])}")
                curr = off + consumed
                chunk_idx += 1
                found = True
                break
        except Exception:
            pass
    if not found:
        curr += 1
