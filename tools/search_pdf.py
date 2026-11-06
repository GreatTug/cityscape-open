import re

def search_pdf(path, keywords):
    with open(path, "rb") as f:
        data = f.read()

    print(f"File size: {len(data):,} bytes")
    # Search for text streams
    # Many streams are FlateDecode compressed in PDF, but some plain text or strings may be present
    for kw in keywords:
        pos = 0
        matches = 0
        while True:
            idx = data.lower().find(kw.lower().encode('utf-8'), pos)
            if idx == -1:
                break
            matches += 1
            start = max(0, idx - 100)
            end = min(len(data), idx + 200)
            # print snippet
            snippet = data[start:end]
            # clean readable chars
            clean = "".join(chr(b) if 32 <= b <= 126 else " " for b in snippet)
            print(f"[{kw}] at {idx}: {clean.strip()[:180]}")
            pos = idx + len(kw) + 50
            if matches >= 5:
                break

search_pdf(r"docs\UserManual.pdf", ["power line", "measure tool", "prop tool", "scripting"])
