import os

def dump_file(path):
    print(f"=== {path} ===")
    if not os.path.exists(path):
        print("Does not exist.")
        return
    with open(path, "rb") as f:
        data = f.read()
    print(f"Size: {len(data)} bytes")
    # Try text first
    try:
        text = data.decode("utf-8")
        print("Text content:")
        print(text)
    except Exception:
        try:
            text = data.decode("latin-1")
            print("Latin-1 text content (preview 500 chars):")
            print(repr(text[:500]))
        except Exception as e:
            print(f"Decode error: {e}")
    # Hex dump first 128 bytes
    hex_str = " ".join(f"{b:02X}" for b in data[:128])
    print(f"Hex (first 128 bytes):\n{hex_str}")

dump_file(r"C:\Users\greattug\Documents\3ds Max 2026\test.csp")
dump_file(r"C:\cityscape-open\testsaves\save1.cst")
