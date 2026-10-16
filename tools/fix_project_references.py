import os
import re

src_dir = r"c:\cityscape-open\src"

for root, dirs, files in os.walk(src_dir):
    for file in files:
        if file.endswith(".csproj"):
            proj_path = os.path.join(root, file)
            with open(proj_path, "r", encoding="utf-8") as f:
                content = f.read()

            new_content = re.sub(
                r'<HintPath>[^<]*CityScape 1\.8 Autodesk Promo[\\/]([^<]+)</HintPath>',
                lambda m: f"<HintPath>..\\..\\lib\\{m.group(1)}</HintPath>",
                content
            )
            
            if new_content != content:
                with open(proj_path, "w", encoding="utf-8") as f:
                    f.write(new_content)
                print(f"Updated references in {file}")
