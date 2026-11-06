import re

def extract_pdf_headings(path):
    with open(path, "rb") as f:
        data = f.read()

    titles = re.findall(rb'/Title\s*\(([^)]+)\)', data)
    print(f"Total bookmarks in {path}: {len(titles)}")
    for t in titles:
        # Decode UTF-16BE if BOM present, else latin1
        raw = t
        if raw.startswith(b'\xfe\xff'):
            try:
                print(" -", raw[2:].decode('utf-16-be'))
                continue
            except:
                pass
        try:
            print(" -", raw.decode('latin1'))
        except:
            print(" -", repr(raw))

print("=== UserManual.pdf Bookmarks ===")
extract_pdf_headings(r"docs\UserManual.pdf")
