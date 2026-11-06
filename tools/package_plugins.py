import os
import zipfile
import shutil

def package_plugins():
    root_dir = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
    dist_dir = os.path.join(root_dir, "dist")
    os.makedirs(dist_dir, exist_ok=True)

    print("=========================================================")
    print("  Packaging CityScape Blender Addon & UE5 Plugin Packages")
    print("=========================================================")

    # 1. Blender Addon
    blender_zip_path = os.path.join(dist_dir, "CityScape_Blender_Addon.zip")
    blender_script = os.path.join(root_dir, "src", "palNativeCore", "bindings", "python", "cityscape_blender_addon.py")
    dll_path = os.path.join(root_dir, "bin", "Release", "net48", "CityScapeCore.dll")

    print("\n[1/2] Creating Blender Addon Package...")
    with zipfile.ZipFile(blender_zip_path, 'w', zipfile.ZIP_DEFLATED) as zf:
        zf.write(blender_script, "cityscape/__init__.py")
        if os.path.exists(dll_path):
            zf.write(dll_path, "cityscape/CityScapeCore.dll")
        # Add a README for Blender users
        readme_blender = (
            "CityScape Procedural Urban Generator - Blender 4.x / 5 Add-on\n"
            "=============================================================\n"
            "Installation:\n"
            "1. Open Blender 4.x or 5.\n"
            "2. Go to Edit > Preferences > Add-ons > Install...\n"
            "3. Select this CityScape_Blender_Addon.zip file.\n"
            "4. Enable 'CityScape Procedural Urban Generator'.\n"
            "5. Press 'N' in 3D Viewport > Open 'CityScape' tab > Click 'Generate 3D City'!\n"
        )
        zf.writestr("cityscape/README.txt", readme_blender)
    print(f"  -> Created {blender_zip_path} ({os.path.getsize(blender_zip_path):,} bytes)")

    # 2. Unreal Engine 5 Plugin
    ue5_zip_path = os.path.join(dist_dir, "CityScape_UE5_Plugin.zip")
    ue5_src_dir = os.path.join(root_dir, "src", "palNativeCore", "unreal")

    print("\n[2/2] Creating Unreal Engine 5 Plugin Package...")
    with zipfile.ZipFile(ue5_zip_path, 'w', zipfile.ZIP_DEFLATED) as zf:
        for root, dirs, files in os.walk(ue5_src_dir):
            for file in files:
                full_path = os.path.join(root, file)
                rel_path = os.path.relpath(full_path, ue5_src_dir)
                zf.write(full_path, os.path.join("CityScapeUE5", rel_path))

        # Include C++ Core headers and lib in ThirdParty
        core_include = os.path.join(root_dir, "src", "palNativeCore", "include")
        for root, dirs, files in os.walk(core_include):
            for file in files:
                full_path = os.path.join(root, file)
                rel_path = os.path.relpath(full_path, core_include)
                zf.write(full_path, os.path.join("CityScapeUE5", "Source", "ThirdParty", "CityScapeCore", "include", rel_path))

        lib_path = os.path.join(root_dir, "bin", "Release", "net48", "CityScapeCoreStatic.lib")
        if os.path.exists(lib_path):
            zf.write(lib_path, os.path.join("CityScapeUE5", "Source", "ThirdParty", "CityScapeCore", "lib", "Win64", "CityScapeCoreStatic.lib"))

        readme_ue5 = (
            "CityScape Procedural Urban Engine - Unreal Engine 5 Plugin\n"
            "==========================================================\n"
            "Installation:\n"
            "1. Copy the 'CityScapeUE5' folder into your Unreal Engine project's 'Plugins/' directory.\n"
            "2. Open your project in Unreal Engine 5.\n"
            "3. Unreal will prompt to compile missing modules > Click Yes.\n"
            "4. Drag and drop 'CityScapeRoadActor' into your level or access C++ API via CityScapeCore!\n"
        )
        zf.writestr("CityScapeUE5/README.txt", readme_ue5)
    print(f"  -> Created {ue5_zip_path} ({os.path.getsize(ue5_zip_path):,} bytes)")

    print("\n[SUCCESS] Both Blender Add-on and UE5 Plugin packaged successfully.")

if __name__ == "__main__":
    package_plugins()
