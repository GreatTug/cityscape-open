with open("src/palDotNet/palDotNet/palVisual/ControlRender.cs", "r", encoding="utf-8") as f:
    lines = f.readlines()

for idx, line in enumerate(lines):
    if any(k in line for k in ["ActivePropMeshes", "ActiveBuildings", "DrawBuilding", "DrawProp", "OnPaint"]):
        print(f"Line {idx+1}: {line.strip()}")
