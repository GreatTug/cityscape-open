import json
import urllib.request
import urllib.error
import sys

def consult_jeb():
    prompt = """You are Jeb, a senior graphics and engine systems architect reviewing CityScape 1.8 open-source modernization.
Current Architecture Status:
1. Native 1600m terrain boundaries bypassed via machine code patches in CityScape.exe (validation at 0x52520, terrain creation at 0x2f40e0, tile init at 0x2f5030, float constants at 0x1ecc10c and 0x2c4d204 expanded to 100,000,000m).
2. Modern 3D physical terrain engine supporting 100,000m x 4,800m extents with procedural multi-octave relief evaluation, metric axis rulers, tile demarcations, and real-time glassmorphism HUD.
3. Multi-architecture compilation: x86 legacy, modern x64, and modern ARM64 targets with PE headers verified.
4. Modern Graphics Backends: DirectX 12 modern (SM6+ DXIL, root signature, mesh shaders), Vulkan 1.3 (SPIR-V bytecode, descriptor sets), DirectX 9 legacy fallback with dynamic in-app switching and telemetry.
5. Modern Exporters: glTF 2.0 / GLB binary, Pixar OpenUSD (.usda), Wavefront OBJ + PBR MTL, 24-bit PNG heightmaps, 32-bit float RAW DEM.
6. Modern Shader Pipeline: Preetham/Nishita atmospheric sky scattering, 4-split Cascaded Shadow Maps (CSM) with PCF, Gerstner ocean waves.
7. Procedural GIS / OSM building extrusions with floor-by-floor zoning, parametric roof styles, and PBR facade materials.
8. Spatial Quadtree scene graph (SpatialQuadtree<T>) with 6-plane 3D View Frustum culling (ViewFrustum3D), slab raycasting, and sub-millisecond radius querying.
9. 10/10 automated verification test suites passing 100% (324 deep subsystem assertions).

Provide a critical evaluation:
1. Critique any potential bottlenecks or edge cases in rendering or managing 100km+ massive terrain worlds.
2. Recommend the top 3 highest-priority next engine enhancements (e.g., GeoTIFF / DEM GIS real-world heightmap ingestion, CDLOD multi-resolution terrain chunk mesh quadtree, road network spline procedural generation, or compute shader terrain tessellation).
3. State whether the engine is ready for production terrain visualization.
Keep your response focused, technical, and actionable.
"""

    req_data = {
        "model": "jeb:27b",
        "prompt": prompt,
        "stream": False,
        "options": {
            "temperature": 0.4,
            "num_predict": 1024
        }
    }

    url = "http://localhost:11434/api/generate"
    req = urllib.request.Request(
        url,
        data=json.dumps(req_data).encode("utf-8"),
        headers={"Content-Type": "application/json"}
    )

    try:
        print("[OLLAMA] Connecting to local jeb:27b model on http://localhost:11434...")
        with urllib.request.urlopen(req, timeout=180) as resp:
            data = json.loads(resp.read().decode("utf-8"))
            response_text = data.get("response", "")
            print("\n================== JEB:27B ARCHITECTURAL REVIEW ==================")
            print(response_text)
            print("==================================================================")
            with open("tools/jeb_review.txt", "w", encoding="utf-8") as rf:
                rf.write(response_text)
            return 0
    except Exception as e:
        print(f"[OLLAMA ERROR] Failed to query jeb:27b: {e}")
        return 1

if __name__ == "__main__":
    sys.exit(consult_jeb())
