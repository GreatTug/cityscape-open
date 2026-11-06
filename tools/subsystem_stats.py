import os

def analyze_subsystems():
    systems = {
        "A. Çekirdek Geometri & Yol/Kavşak/Bina (src/palNativeCore)": {
            "type": "C++",
            "paths": ["src/palNativeCore/include", "src/palNativeCore/src"]
        },
        "B. Genişletilmiş Usulü Şehir Sistemleri (Köprü, Su, Çit, Bitki, Trafik, Elektrik, vb.)": {
            "type": "C#",
            "paths": [
                "src/palDotNet/palVisual/BridgePillarSystem.cs",
                "src/palDotNet/palVisual/WaterBodiesSystem.cs",
                "src/palDotNet/palVisual/FenceBarrierSystem.cs",
                "src/palDotNet/palVisual/FoliageVegetationSystem.cs",
                "src/palDotNet/palVisual/TrafficSimulationSystem.cs",
                "src/palDotNet/palVisual/PowerLineSystem.cs",
                "src/palDotNet/palVisual/StreetPropSystem.cs",
                "src/palDotNet/palVisual/AstronomicalSkySystem.cs",
                "src/palDotNet/palVisual/WeatherAtmosphereSystem.cs",
                "src/palDotNet/palVisual/UrbanPhysicsAndMetadataSystem.cs",
                "src/palDotNet/palVisual/TerrainSculptSystem.cs",
                "src/palDotNet/palVisual/UrbanScriptingEngine.cs",
                "src/palDotNet/palVisual/UrbanUndoRedoManager.cs",
                "src/palDotNet/palVisual/MeasureToolSystem.cs",
                "src/palDotNet/palVisual/MaterialPaletteSystem.cs",
                "src/palDotNet/palVisual/FirstPersonCameraController.cs",
                "src/palDotNet/palVisual/OpenStreetMapService.cs",
                "src/palDotNet/palVisual/OsmCitySceneBuilder.cs",
                "src/palDotNet/palVisual/InteractiveRoadModel.cs",
                "src/palDotNet/palVisual/LotParcelingModel.cs"
            ]
        },
        "C. 3D Render & DirectX/Vulkan Grafik Hattı": {
            "type": "C#",
            "paths": [
                "src/palDotNet/palDotNet/palVisual/ControlRender.cs",
                "src/palDotNet/palModernEngine/ModernExporters.cs",
                "src/palDotNet/palModernEngine/CityScapeGeometryPipeline.cs"
            ]
        },
        "D. Windows Forms UI & Editör Arayüzü (Docking, Pencereler, Diyaloglar)": {
            "type": "C#",
            "paths": [
                "src/edEditorDotNet",
                "src/palDotNet/palDotNet/palVisual/FormApp.cs",
                "src/palDotNet/palDialog"
            ]
        },
        "E. Legacy Çekirdek, Decompile Edilmiş Motor & Serileştirme": {
            "type": "C#",
            "paths": [
                "src/ktKitDotNet",
                "src/gisImageryDotNet",
                "src/CityScape"
            ]
        }
    }

    print("=== SUBSYSTEM BREAKDOWN ===")
    results = {}
    for name, data in systems.items():
        total_lines = 0
        code_lines = 0
        file_count = 0
        for p in data["paths"]:
            if os.path.isfile(p):
                file_count += 1
                with open(p, 'r', encoding='utf-8', errors='ignore') as f:
                    lines = f.readlines()
                    total_lines += len(lines)
                    code_lines += sum(1 for l in lines if l.strip() and not l.strip().startswith('//'))
            elif os.path.isdir(p):
                for root, dirs, files in os.walk(p):
                    for file in files:
                        if file.endswith(('.cs', '.cpp', '.hpp', '.h')):
                            file_count += 1
                            with open(os.path.join(root, file), 'r', encoding='utf-8', errors='ignore') as f:
                                lines = f.readlines()
                                total_lines += len(lines)
                                code_lines += sum(1 for l in lines if l.strip() and not l.strip().startswith('//'))
        results[name] = {"type": data["type"], "files": file_count, "total": total_lines, "code": code_lines}
        print(f"[{data['type']}] {name}: {file_count} files, {code_lines} code lines")

if __name__ == '__main__':
    analyze_subsystems()
