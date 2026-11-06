import os
import glob
import struct
import re

files = glob.glob("Worlds/**/*.gob", recursive=True)
print(f"Testing .gob parser on all {len(files)} models...")

success_count = 0
failed = []

def parse_gob(filepath):
    with open(filepath, "rb") as f:
        data = f.read()
    
    if not data.startswith(b"[Ver 7:gx0.3:cr1]"):
        return None, "Invalid header"
    
    # Extract textures from XML
    textures = {}
    xml_start = data.find(b"<ShaderVariables>")
    xml_end = data.find(b"</ShaderVariables>")
    if xml_start != -1 and xml_end != -1:
        xml_chunk = data[xml_start:xml_end].decode('utf-8', errors='ignore')
        diff_m = re.search(r"<diffuseTexture>\s*<!\[CDATA\[(.*?)\]\]>\s*</diffuseTexture>", xml_chunk)
        if diff_m:
            textures['diffuse'] = diff_m.group(1).strip()
        norm_m = re.search(r"<normalTexture>\s*<!\[CDATA\[(.*?)\]\]>\s*</normalTexture>", xml_chunk)
        if norm_m:
            textures['normal'] = norm_m.group(1).strip()

    # Search for vertex buffer: find 0xFFFFFFFF color markers
    # In GOB format, vertex stride is typically 36 or 40 or 44 or 48 or 52 bytes
    first_col = data.find(b"\xff\xff\xff\xff", 0x200)
    if first_col == -1:
        return None, "No vertex color marker found"
    
    # Determine stride
    next_col = data.find(b"\xff\xff\xff\xff", first_col + 1)
    if next_col == -1:
        stride = 36
    else:
        stride = next_col - first_col
        if stride not in [32, 36, 40, 44, 48, 52, 64]:
            stride = 36
            
    vb_start = first_col - 24
    if vb_start < 0:
        return None, "Invalid VB start"
        
    vertices = []
    pos = vb_start
    while pos + stride <= len(data):
        col = struct.unpack_from("<I", data, pos + 24)[0]
        if col != 0xFFFFFFFF:
            break
        px, py, pz = struct.unpack_from("<3f", data, pos)
        nx, ny, nz = struct.unpack_from("<3f", data, pos + 12)
        u, v = struct.unpack_from("<2f", data, pos + 28)
        vertices.append((px, py, pz, nx, ny, nz, u, v))
        pos += stride
        
    if len(vertices) == 0:
        return None, "0 vertices extracted"
        
    # Search for index buffer after vertex buffer
    # Skip potential padding / submesh headers (typically 4 to 32 bytes)
    triangles = []
    num_verts = len(vertices)
    
    for offset in range(pos, min(len(data) - 6, pos + 64), 2):
        # Test if valid triangle list starts here
        idx0, idx1, idx2 = struct.unpack_from("<3H", data, offset)
        if idx0 < num_verts and idx1 < num_verts and idx2 < num_verts and (idx0 != idx1 or idx1 != idx2):
            # Parse triangles
            p = offset
            while p + 6 <= len(data):
                i0, i1, i2 = struct.unpack_from("<3H", data, p)
                if i0 < num_verts and i1 < num_verts and i2 < num_verts:
                    triangles.append((i0, i1, i2))
                    p += 6
                else:
                    break
            if len(triangles) >= 1:
                break

    return {
        'vertex_count': len(vertices),
        'triangle_count': len(triangles),
        'textures': textures,
        'stride': stride
    }, None

for f in files:
    res, err = parse_gob(f)
    if res and res['vertex_count'] > 0:
        success_count += 1
    else:
        failed.append((f, err))

print(f"\nResult: Successfully parsed {success_count}/{len(files)} .gob models!")
if failed:
    print(f"Failed count: {len(failed)}")
    for f, err in failed[:10]:
        print(f"  {f}: {err}")
