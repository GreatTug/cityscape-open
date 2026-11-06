import pypdf

reader = pypdf.PdfReader(r"docs\UserManual.pdf")

def save_pages(start_page, end_page, out_path):
    with open(out_path, "w", encoding="utf-8") as f:
        for p in range(start_page - 1, end_page):
            text = reader.pages[p].extract_text()
            f.write(f"\n\n=== PAGE {p + 1} ===\n\n")
            f.write(text)

save_pages(65, 90, r"scratch\manual_chapter5_geography.txt")
save_pages(91, 134, r"scratch\manual_chapter6_roads.txt")
print("Saved chapter 5 and 6 text successfully.")
