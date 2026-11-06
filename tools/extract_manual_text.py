import re
import zlib

def extract_pdf_text_streams(pdf_path, max_streams=200):
    with open(pdf_path, "rb") as f:
        data = f.read()

    stream_re = re.compile(rb'stream[\r\n]+(.*?)[\r\n]+endstream', re.DOTALL)
    decompressed_texts = []
    
    for i, m in enumerate(stream_re.finditer(data)):
        stream_data = m.group(1)
        try:
            decomp = zlib.decompress(stream_data)
            # check if text drawing commands present: (text) Tj or ' or "
            text_matches = re.findall(rb'\((.*?)\)\s*Tj', decomp)
            if text_matches:
                page_text = " ".join(t.decode('latin1', errors='ignore') for t in text_matches)
                decompressed_texts.append(page_text)
        except Exception:
            pass
        if len(decompressed_texts) >= max_streams:
            break

    print(f"Extracted {len(decompressed_texts)} text blocks.")
    for kw in ["Power Line", "Measure Tool", "Prop Tool", "Scripting", "Material Tool"]:
        found = 0
        print(f"\n=== KEYWORD: {kw} ===")
        for block in decompressed_texts:
            if kw.lower() in block.lower():
                print(" -> " + block[:300].strip())
                found += 1
                if found >= 3:
                    break

extract_pdf_text_streams(r"docs\UserManual.pdf")
