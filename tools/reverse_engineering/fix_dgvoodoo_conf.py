import re

for conf_path in [r"c:\cityscape-open\dgVoodoo.conf", r"c:\cityscape-open\bin\Release\net48\dgVoodoo.conf", r"C:\CityScape 1.8 Autodesk Promo\dgVoodoo.conf"]:
    try:
        with open(conf_path, "r", encoding="utf-8", errors="ignore") as f:
            content = f.read()
        
        # Replace CaptureMouse = true with CaptureMouse = false
        content = re.sub(r'CaptureMouse\s*=\s*true', 'CaptureMouse                         = false', content, flags=re.IGNORECASE)
        # Replace FreeMouse = false with FreeMouse = true
        content = re.sub(r'FreeMouse\s*=\s*false', 'FreeMouse                            = true', content, flags=re.IGNORECASE)
        
        with open(conf_path, "w", encoding="utf-8") as f:
            f.write(content)
        print(f"Updated {conf_path}")
    except Exception as e:
        print(f"Error {conf_path}: {e}")
