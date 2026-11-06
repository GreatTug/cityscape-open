import pypdf
import os

pdf_path = r"docs\UserManual.pdf"
if os.path.exists(pdf_path):
    reader = pypdf.PdfReader(pdf_path)
    print(f"Total pages: {len(reader.pages)}")
    outline = reader.outline
    def print_outline(items, indent=0):
        for item in items:
            if isinstance(item, list):
                print_outline(item, indent + 2)
            else:
                title = getattr(item, 'title', str(item))
                print(" " * indent + f"- {title}")
    if outline:
        print_outline(outline)
    else:
        # extract text from first 10 pages
        for i in range(min(10, len(reader.pages))):
            text = reader.pages[i].extract_text()
            if "Table of Contents" in text or "Contents" in text:
                print(f"--- Page {i+1} ---")
                print(text[:1500])
