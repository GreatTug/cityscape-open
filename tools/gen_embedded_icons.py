import os

ICON_DIR = os.path.join("src", "edEditorDotNet")
OUT_HEADER = os.path.join("src", "palNativeUI", "include", "EmbeddedToolbarIcons.hpp")

ICON_NAMES = [
    "GeographyGrid",
    "GeographyGridPaint",
    "GeographyCircle",
    "GeographyPolygon",
    "GeographyGlobalPaint",
    "RoadRoad",
    "RoadTangent",
    "RoadHeight",
    "Lot",
    "Building",
    "WaterGlobal",
    "WaterPolygon",
    "WaterGraph",
    "Fence",
    "Powerline",
    "PropPoint",
    "PropRange",
    "PropCircle",
    "PropPolygon",
    "PropBlock",
    "MaterialPaint",
    "MaterialSeed",
    "SkySky",
    "SkyClouds",
    "Traffic",
    "Physics",
    "Measure",
    "LOD",
    "navteq",
    "AbstractPoints",
    "AbstractGraph",
    "AbstractPolygons",
]

def main():
    os.makedirs(os.path.dirname(OUT_HEADER), exist_ok=True)
    lines = []
    lines.append("#pragma once")
    lines.append("#include <cstdint>")
    lines.append("#include <cstddef>")
    lines.append("")
    lines.append("// Auto-generated from authentic PixelActive CityScape 1.8 MainToolBarIcons (*.png)")
    lines.append("namespace CityScapeUI {")
    lines.append("")
    lines.append("struct EmbeddedIconBlob {")
    lines.append("    const char* name;")
    lines.append("    const uint8_t* data;")
    lines.append("    size_t size;")
    lines.append("};")
    lines.append("")

    entries = []
    for name in ICON_NAMES:
        fname = f"edEditorDotNet.MainToolBarIcons.{name}.png"
        fpath = os.path.join(ICON_DIR, fname)
        if not os.path.exists(fpath):
            raise FileNotFoundError(f"Missing toolbar icon: {fpath}")
        with open(fpath, "rb") as f:
            raw = f.read()
        arr_name = f"kIcon_{name}"
        hex_bytes = ", ".join(f"0x{b:02X}" for b in raw)
        lines.append(f"inline constexpr uint8_t {arr_name}[{len(raw)}] = {{ {hex_bytes} }};")
        entries.append((name, arr_name, len(raw)))

    lines.append("")
    lines.append(f"inline constexpr size_t kEmbeddedToolbarIconCount = {len(entries)};")
    lines.append("inline constexpr EmbeddedIconBlob kEmbeddedToolbarIcons[] = {")
    for name, arr_name, sz in entries:
        lines.append(f'    {{ "{name}", {arr_name}, {sz} }},')
    lines.append("};")
    lines.append("")
    lines.append("} // namespace CityScapeUI")
    lines.append("")

    with open(OUT_HEADER, "w", encoding="utf-8") as out:
        out.write("\n".join(lines))

    print(f"[OK] Generated {OUT_HEADER} with {len(entries)} embedded PNG toolbar icons.")

if __name__ == "__main__":
    main()
