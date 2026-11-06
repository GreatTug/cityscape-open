with open("src/palDotNet/palVisual/OsmCitySceneBuilder.cs", "r", encoding="utf-8") as f:
    for line in f:
        if "ExportMeshData" in line:
            print(line.strip())
