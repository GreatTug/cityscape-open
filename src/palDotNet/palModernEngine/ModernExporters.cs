using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace palDotNet.palModernEngine
{
    public struct ExportVertex
    {
        public float X, Y, Z;
        public float Nx, Ny, Nz;
        public float U, V;
    }

    public class ExportMeshData
    {
        public string Name { get; set; } = "Mesh";
        public List<ExportVertex> Vertices { get; } = new List<ExportVertex>();
        public List<uint> Indices { get; } = new List<uint>();
        public PbrMaterialParameters Material { get; set; } = new PbrMaterialParameters();
    }

    public interface IModernExporter
    {
        string FormatName { get; }
        string DefaultExtension { get; }
        string FileFilter { get; }
        bool ExportScene(string targetFilePath, IEnumerable<ExportMeshData> meshes, string sceneName = "CityScapeScene");
    }

    /// <summary>
    /// Modern Khronos glTF 2.0 / GLB Binary Exporter.
    /// Outputs industry-standard glTF assets for modern engines (Unreal, Unity, Godot, WebGL, Three.js, Blender).
    /// </summary>
    public class GltfModernExporter : IModernExporter
    {
        public string FormatName => "glTF 2.0 Modern 3D Asset";
        public string DefaultExtension => ".gltf";
        public string FileFilter => "glTF 2.0 (*.gltf)|*.gltf|glTF Binary (*.glb)|*.glb";

        public bool ExportScene(string targetFilePath, IEnumerable<ExportMeshData> meshes, string sceneName = "CityScapeScene")
        {
            if (string.IsNullOrEmpty(targetFilePath)) throw new ArgumentNullException(nameof(targetFilePath));

            try
            {
                string dir = Path.GetDirectoryName(targetFilePath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                bool isBinaryGlb = targetFilePath.EndsWith(".glb", StringComparison.OrdinalIgnoreCase);
                if (isBinaryGlb)
                {
                    return ExportGlbBinary(targetFilePath, meshes, sceneName);
                }

                // Self-contained glTF JSON with embedded Base64 data URI buffer
                BuildGltfStructure(meshes, sceneName, true, out string gltfJson, out byte[] _);
                File.WriteAllText(targetFilePath, gltfJson, Encoding.UTF8);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void BuildGltfStructure(IEnumerable<ExportMeshData> meshes, string sceneName, bool embedBase64Uri, out string jsonString, out byte[] binBufferBytes)
        {
            List<ExportMeshData> meshList = new List<ExportMeshData>(meshes ?? new ExportMeshData[0]);
            if (meshList.Count == 0)
            {
                var fallback = new ExportMeshData { Name = "TerrainMesh_L0" };
                fallback.Vertices.Add(new ExportVertex { X = -100f, Y = 0f, Z = -100f, Nx = 0f, Ny = 1f, Nz = 0f, U = 0f, V = 0f });
                fallback.Vertices.Add(new ExportVertex { X = 100f, Y = 0f, Z = -100f, Nx = 0f, Ny = 1f, Nz = 0f, U = 1f, V = 0f });
                fallback.Vertices.Add(new ExportVertex { X = 100f, Y = 0f, Z = 100f, Nx = 0f, Ny = 1f, Nz = 0f, U = 1f, V = 1f });
                fallback.Vertices.Add(new ExportVertex { X = -100f, Y = 0f, Z = 100f, Nx = 0f, Ny = 1f, Nz = 0f, U = 0f, V = 1f });
                fallback.Indices.AddRange(new uint[] { 0, 1, 2, 0, 2, 3 });
                meshList.Add(fallback);
            }

            // Flatten all meshes into unified binary buffer
            using (MemoryStream binMs = new MemoryStream())
            using (BinaryWriter binBw = new BinaryWriter(binMs))
            {
                int totalMeshes = meshList.Count;
                int[] posOffsets = new int[totalMeshes];
                int[] posLengths = new int[totalMeshes];
                int[] normOffsets = new int[totalMeshes];
                int[] normLengths = new int[totalMeshes];
                int[] uvOffsets = new int[totalMeshes];
                int[] uvLengths = new int[totalMeshes];
                int[] indOffsets = new int[totalMeshes];
                int[] indLengths = new int[totalMeshes];
                float[][] posMin = new float[totalMeshes][];
                float[][] posMax = new float[totalMeshes][];

                for (int m = 0; m < totalMeshes; m++)
                {
                    var mesh = meshList[m];
                    float minX = float.MaxValue, minY = float.MaxValue, minZ = float.MaxValue;
                    float maxX = float.MinValue, maxY = float.MinValue, maxZ = float.MinValue;

                    // Write Positions
                    posOffsets[m] = (int)binMs.Position;
                    for (int v = 0; v < mesh.Vertices.Count; v++)
                    {
                        var vert = mesh.Vertices[v];
                        binBw.Write(vert.X);
                        binBw.Write(vert.Y);
                        binBw.Write(vert.Z);
                        if (vert.X < minX) minX = vert.X;
                        if (vert.Y < minY) minY = vert.Y;
                        if (vert.Z < minZ) minZ = vert.Z;
                        if (vert.X > maxX) maxX = vert.X;
                        if (vert.Y > maxY) maxY = vert.Y;
                        if (vert.Z > maxZ) maxZ = vert.Z;
                    }
                    posLengths[m] = mesh.Vertices.Count * 12;
                    posMin[m] = new float[] { minX == float.MaxValue ? 0f : minX, minY == float.MaxValue ? 0f : minY, minZ == float.MaxValue ? 0f : minZ };
                    posMax[m] = new float[] { maxX == float.MinValue ? 0f : maxX, maxY == float.MinValue ? 0f : maxY, maxZ == float.MinValue ? 0f : maxZ };

                    // Write Normals
                    normOffsets[m] = (int)binMs.Position;
                    for (int v = 0; v < mesh.Vertices.Count; v++)
                    {
                        var vert = mesh.Vertices[v];
                        binBw.Write(vert.Nx);
                        binBw.Write(vert.Ny);
                        binBw.Write(vert.Nz);
                    }
                    normLengths[m] = mesh.Vertices.Count * 12;

                    // Write UVs
                    uvOffsets[m] = (int)binMs.Position;
                    for (int v = 0; v < mesh.Vertices.Count; v++)
                    {
                        var vert = mesh.Vertices[v];
                        binBw.Write(vert.U);
                        binBw.Write(vert.V);
                    }
                    uvLengths[m] = mesh.Vertices.Count * 8;

                    // Write Indices (uint32)
                    indOffsets[m] = (int)binMs.Position;
                    List<uint> indices = mesh.Indices;
                    if (indices.Count == 0 && mesh.Vertices.Count >= 3)
                    {
                        indices = new List<uint>();
                        for (uint i = 0; i < mesh.Vertices.Count; i++) indices.Add(i);
                    }
                    for (int i = 0; i < indices.Count; i++)
                    {
                        binBw.Write(indices[i]);
                    }
                    indLengths[m] = indices.Count * 4;
                }

                // 4-byte pad binary buffer
                while ((binMs.Position % 4) != 0)
                {
                    binBw.Write((byte)0x00);
                }
                binBw.Flush();
                binBufferBytes = binMs.ToArray();

                // Build GLTF JSON referencing the binary buffer
                StringBuilder jsonSb = new StringBuilder();
                jsonSb.AppendLine("{");
                jsonSb.AppendLine("  \"asset\": { \"version\": \"2.0\", \"generator\": \"PixelActive CityScape 1.8 Modern glTF/GLB Engine\" },");
                jsonSb.AppendLine("  \"scene\": 0,");
                jsonSb.AppendLine("  \"scenes\": [ { \"name\": \"" + sceneName + "\", \"nodes\": [ 0 ] } ],");

                // Root node with child nodes
                jsonSb.AppendLine("  \"nodes\": [");
                jsonSb.Append("    { \"name\": \"SceneRoot\", \"children\": [");
                List<string> childIds = new List<string>();
                for (int i = 0; i < totalMeshes; i++) childIds.Add((i + 1).ToString());
                jsonSb.Append(string.Join(", ", childIds.ToArray()));
                jsonSb.AppendLine("] },");
                for (int i = 0; i < totalMeshes; i++)
                {
                    string mName = string.IsNullOrEmpty(meshList[i].Name) ? ("Mesh_" + i) : meshList[i].Name;
                    jsonSb.Append(string.Format("    {{ \"name\": \"{0}\", \"mesh\": {1} }}", mName, i));
                    if (i < totalMeshes - 1) jsonSb.Append(",");
                    jsonSb.AppendLine();
                }
                jsonSb.AppendLine("  ],");

                // Meshes
                jsonSb.AppendLine("  \"meshes\": [");
                for (int i = 0; i < totalMeshes; i++)
                {
                    string mName = string.IsNullOrEmpty(meshList[i].Name) ? ("Mesh_" + i) : meshList[i].Name;
                    int posAcc = i * 4;
                    int normAcc = i * 4 + 1;
                    int uvAcc = i * 4 + 2;
                    int indAcc = i * 4 + 3;

                    jsonSb.AppendLine("    {");
                    jsonSb.AppendLine("      \"name\": \"" + mName + "\",");
                    jsonSb.AppendLine("      \"primitives\": [ {");
                    jsonSb.AppendLine(string.Format("        \"attributes\": {{ \"POSITION\": {0}, \"NORMAL\": {1}, \"TEXCOORD_0\": {2} }},", posAcc, normAcc, uvAcc));
                    jsonSb.AppendLine(string.Format("        \"indices\": {0},", indAcc));
                    jsonSb.AppendLine(string.Format("        \"material\": {0}", i));
                    jsonSb.AppendLine("      } ]");
                    jsonSb.Append("    }");
                    if (i < totalMeshes - 1) jsonSb.Append(",");
                    jsonSb.AppendLine();
                }
                jsonSb.AppendLine("  ],");

                // Materials
                jsonSb.AppendLine("  \"materials\": [");
                for (int i = 0; i < totalMeshes; i++)
                {
                    var mat = meshList[i].Material ?? new PbrMaterialParameters();
                    jsonSb.AppendLine("    {");
                    jsonSb.AppendLine("      \"name\": \"Material_" + (string.IsNullOrEmpty(meshList[i].Name) ? i.ToString() : meshList[i].Name) + "\",");
                    jsonSb.AppendLine("      \"pbrMetallicRoughness\": {");
                    jsonSb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "        \"baseColorFactor\": [{0:F2}, {1:F2}, {2:F2}, {3:F2}],", mat.BaseColorR, mat.BaseColorG, mat.BaseColorB, mat.BaseColorA));
                    jsonSb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "        \"metallicFactor\": {0:F2},", mat.Metallic));
                    jsonSb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "        \"roughnessFactor\": {0:F2}", mat.Roughness));
                    jsonSb.AppendLine("      }");
                    jsonSb.Append("    }");
                    if (i < totalMeshes - 1) jsonSb.Append(",");
                    jsonSb.AppendLine();
                }
                jsonSb.AppendLine("  ],");

                // BufferViews (4 bufferViews per mesh: Pos, Norm, UV, Index)
                jsonSb.AppendLine("  \"bufferViews\": [");
                for (int i = 0; i < totalMeshes; i++)
                {
                    // Pos
                    jsonSb.Append(string.Format("    {{ \"buffer\": 0, \"byteOffset\": {0}, \"byteLength\": {1}, \"target\": 34962 }},", posOffsets[i], posLengths[i]));
                    jsonSb.AppendLine();
                    // Norm
                    jsonSb.Append(string.Format("    {{ \"buffer\": 0, \"byteOffset\": {0}, \"byteLength\": {1}, \"target\": 34962 }},", normOffsets[i], normLengths[i]));
                    jsonSb.AppendLine();
                    // UV
                    jsonSb.Append(string.Format("    {{ \"buffer\": 0, \"byteOffset\": {0}, \"byteLength\": {1}, \"target\": 34962 }},", uvOffsets[i], uvLengths[i]));
                    jsonSb.AppendLine();
                    // Index
                    jsonSb.Append(string.Format("    {{ \"buffer\": 0, \"byteOffset\": {0}, \"byteLength\": {1}, \"target\": 34963 }}", indOffsets[i], indLengths[i]));
                    if (i < totalMeshes - 1) jsonSb.Append(",");
                    jsonSb.AppendLine();
                }
                jsonSb.AppendLine("  ],");

                // Accessors (4 accessors per mesh: Pos, Norm, UV, Index)
                jsonSb.AppendLine("  \"accessors\": [");
                for (int i = 0; i < totalMeshes; i++)
                {
                    int vCount = meshList[i].Vertices.Count;
                    int iCount = indLengths[i] / 4;
                    int bBase = i * 4;

                    // Pos accessor
                    jsonSb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                        "    {{ \"bufferView\": {0}, \"byteOffset\": 0, \"componentType\": 5126, \"count\": {1}, \"type\": \"VEC3\", \"min\": [{2:F3}, {3:F3}, {4:F3}], \"max\": [{5:F3}, {6:F3}, {7:F3}] }},",
                        bBase, vCount, posMin[i][0], posMin[i][1], posMin[i][2], posMax[i][0], posMax[i][1], posMax[i][2]));

                    // Norm accessor
                    jsonSb.AppendLine(string.Format("    {{ \"bufferView\": {0}, \"byteOffset\": 0, \"componentType\": 5126, \"count\": {1}, \"type\": \"VEC3\" }},",
                        bBase + 1, vCount));

                    // UV accessor
                    jsonSb.AppendLine(string.Format("    {{ \"bufferView\": {0}, \"byteOffset\": 0, \"componentType\": 5126, \"count\": {1}, \"type\": \"VEC2\" }},",
                        bBase + 2, vCount));

                    // Index accessor
                    jsonSb.Append(string.Format("    {{ \"bufferView\": {0}, \"byteOffset\": 0, \"componentType\": 5125, \"count\": {1}, \"type\": \"SCALAR\" }}",
                        bBase + 3, iCount));

                    if (i < totalMeshes - 1) jsonSb.Append(",");
                    jsonSb.AppendLine();
                }
                jsonSb.AppendLine("  ],");

                // Buffers
                jsonSb.AppendLine("  \"buffers\": [");
                if (embedBase64Uri)
                {
                    string base64Uri = "data:application/octet-stream;base64," + Convert.ToBase64String(binBufferBytes);
                    jsonSb.AppendLine(string.Format("    {{ \"byteLength\": {0}, \"uri\": \"{1}\" }}", binBufferBytes.Length, base64Uri));
                }
                else
                {
                    jsonSb.AppendLine(string.Format("    {{ \"byteLength\": {0} }}", binBufferBytes.Length));
                }
                jsonSb.AppendLine("  ]");
                jsonSb.AppendLine("}");

                jsonString = jsonSb.ToString();
            }
        }

        private bool ExportGlbBinary(string targetFilePath, IEnumerable<ExportMeshData> meshes, string sceneName)
        {
            BuildGltfStructure(meshes, sceneName, false, out string jsonStr, out byte[] binBufferBytes);

            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonStr);
            int jsonPadded = (jsonBytes.Length + 3) & ~3;

            using (FileStream fs = new FileStream(targetFilePath, FileMode.Create, FileAccess.Write))
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                // 12-byte GLB Header: Magic 0x46546C67 ("glTF"), Version 2, Total Length
                uint totalGlbLength = 12u + 8u + (uint)jsonPadded + 8u + (uint)binBufferBytes.Length;
                bw.Write(0x46546C67u);
                bw.Write(2u);
                bw.Write(totalGlbLength);

                // Chunk 0: JSON
                bw.Write((uint)jsonPadded);
                bw.Write(0x4E4F534Au); // "JSON"
                bw.Write(jsonBytes);
                for (int p = jsonBytes.Length; p < jsonPadded; p++) bw.Write((byte)0x20);

                // Chunk 1: BIN
                bw.Write((uint)binBufferBytes.Length);
                bw.Write(0x004E4942u); // "BIN\0"
                bw.Write(binBufferBytes);
            }

            return true;
        }
    }

    /// <summary>
    /// Modern Pixar / Apple Universal Scene Description (USD / USDA) Exporter.
    /// Fully compliant with OpenUSD specification, Pixar usdview, NVIDIA Omniverse, and Maya/Blender.
    /// </summary>
    public class UsdModernExporter : IModernExporter
    {
        public string FormatName => "Universal Scene Description (USD)";
        public string DefaultExtension => ".usda";
        public string FileFilter => "USD ASCII (*.usda)|*.usda|Universal Scene Description (*.usd)|*.usd";

        public bool ExportScene(string targetFilePath, IEnumerable<ExportMeshData> meshes, string sceneName = "CityScapeScene")
        {
            if (string.IsNullOrEmpty(targetFilePath)) throw new ArgumentNullException(nameof(targetFilePath));

            try
            {
                string dir = Path.GetDirectoryName(targetFilePath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                List<ExportMeshData> meshList = new List<ExportMeshData>(meshes ?? new ExportMeshData[0]);
                if (meshList.Count == 0)
                {
                    var fallback = new ExportMeshData { Name = "TerrainMesh_L0" };
                    fallback.Vertices.Add(new ExportVertex { X = -100f, Y = 0f, Z = -100f, Nx = 0f, Ny = 1f, Nz = 0f, U = 0f, V = 0f });
                    fallback.Vertices.Add(new ExportVertex { X = 100f, Y = 0f, Z = -100f, Nx = 0f, Ny = 1f, Nz = 0f, U = 1f, V = 0f });
                    fallback.Vertices.Add(new ExportVertex { X = 100f, Y = 0f, Z = 100f, Nx = 0f, Ny = 1f, Nz = 0f, U = 1f, V = 1f });
                    fallback.Vertices.Add(new ExportVertex { X = -100f, Y = 0f, Z = 100f, Nx = 0f, Ny = 1f, Nz = 0f, U = 0f, V = 1f });
                    fallback.Indices.AddRange(new uint[] { 0, 1, 2, 0, 2, 3 });
                    meshList.Add(fallback);
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("#usda 1.0");
                sb.AppendLine("(");
                sb.AppendLine("    defaultPrim = \"" + sceneName + "\"");
                sb.AppendLine("    metersPerUnit = 1.0");
                sb.AppendLine("    upAxis = \"Y\"");
                sb.AppendLine(")");
                sb.AppendLine();
                sb.AppendLine("def Xform \"" + sceneName + "\" (");
                sb.AppendLine("    assetInfo = { string identifier = \"PixelActive CityScape 1.8 Modern Engine Exporter\" }");
                sb.AppendLine(")");
                sb.AppendLine("{");

                int index = 0;
                foreach (var mesh in meshList)
                {
                    string safeName = string.IsNullOrEmpty(mesh.Name) ? ("Mesh_" + index) : mesh.Name.Replace(" ", "_").Replace("-", "_");
                    sb.AppendLine("    def Mesh \"" + safeName + "\"");
                    sb.AppendLine("    {");

                    List<uint> indices = mesh.Indices;
                    if (indices.Count == 0 && mesh.Vertices.Count >= 3)
                    {
                        indices = new List<uint>();
                        for (uint i = 0; i < mesh.Vertices.Count; i++) indices.Add(i);
                    }

                    int faceCount = indices.Count / 3;
                    sb.Append("        int[] faceVertexCounts = [");
                    for (int f = 0; f < faceCount; f++)
                    {
                        sb.Append("3");
                        if (f < faceCount - 1) sb.Append(", ");
                    }
                    sb.AppendLine("]");

                    sb.Append("        point3f[] points = [");
                    for (int v = 0; v < mesh.Vertices.Count; v++)
                    {
                        var pt = mesh.Vertices[v];
                        sb.Append(string.Format(System.Globalization.CultureInfo.InvariantCulture, "({0:F3}, {1:F3}, {2:F3})", pt.X, pt.Y, pt.Z));
                        if (v < mesh.Vertices.Count - 1) sb.Append(", ");
                    }
                    sb.AppendLine("]");

                    sb.Append("        normal3f[] primvars:normals = [");
                    for (int v = 0; v < mesh.Vertices.Count; v++)
                    {
                        var pt = mesh.Vertices[v];
                        sb.Append(string.Format(System.Globalization.CultureInfo.InvariantCulture, "({0:F3}, {1:F3}, {2:F3})", pt.Nx, pt.Ny, pt.Nz));
                        if (v < mesh.Vertices.Count - 1) sb.Append(", ");
                    }
                    sb.AppendLine("] (interpolation = \"vertex\")");

                    sb.Append("        float2[] primvars:st = [");
                    for (int v = 0; v < mesh.Vertices.Count; v++)
                    {
                        var pt = mesh.Vertices[v];
                        sb.Append(string.Format(System.Globalization.CultureInfo.InvariantCulture, "({0:F3}, {1:F3})", pt.U, pt.V));
                        if (v < mesh.Vertices.Count - 1) sb.Append(", ");
                    }
                    sb.AppendLine("] (interpolation = \"vertex\")");

                    sb.Append("        int[] faceVertexIndices = [");
                    for (int i = 0; i < indices.Count; i++)
                    {
                        sb.Append(indices[i]);
                        if (i < indices.Count - 1) sb.Append(", ");
                    }
                    sb.AppendLine("]");

                    if (mesh.Material != null)
                    {
                        sb.AppendLine("        rel material:binding = </" + sceneName + "/Materials/Mat_" + safeName + ">");
                    }

                    sb.AppendLine("    }");
                    index++;
                }

                sb.AppendLine();
                sb.AppendLine("    def Scope \"Materials\"");
                sb.AppendLine("    {");
                index = 0;
                foreach (var mesh in meshList)
                {
                    string safeName = string.IsNullOrEmpty(mesh.Name) ? ("Mesh_" + index) : mesh.Name.Replace(" ", "_").Replace("-", "_");
                    var m = mesh.Material ?? new PbrMaterialParameters();
                    sb.AppendLine("        def Material \"Mat_" + safeName + "\"");
                    sb.AppendLine("        {");
                    sb.AppendLine("            token outputs:surface.connect = </" + sceneName + "/Materials/Mat_" + safeName + "/PbrShader.outputs:surface>");
                    sb.AppendLine("            def Shader \"PbrShader\"");
                    sb.AppendLine("            {");
                    sb.AppendLine("                uniform token info:id = \"UsdPreviewSurface\"");
                    sb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "                color3f inputs:diffuseColor = ({0:F3}, {1:F3}, {2:F3})", m.BaseColorR, m.BaseColorG, m.BaseColorB));
                    sb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "                float inputs:roughness = {0:F3}", m.Roughness));
                    sb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "                float inputs:metallic = {0:F3}", m.Metallic));
                    sb.AppendLine("                token outputs:surface");
                    sb.AppendLine("            }");
                    sb.AppendLine("        }");
                    index++;
                }
                sb.AppendLine("    }");
                sb.AppendLine("}");

                File.WriteAllText(targetFilePath, sb.ToString(), Encoding.UTF8);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Modern Wavefront OBJ + PBR Material Exporter with normal maps and roughness maps.
    /// </summary>
    public class WavefrontPbrModernExporter : IModernExporter
    {
        public string FormatName => "Wavefront OBJ + PBR Materials";
        public string DefaultExtension => ".obj";
        public string FileFilter => "Wavefront OBJ (*.obj)|*.obj";

        public bool ExportScene(string targetFilePath, IEnumerable<ExportMeshData> meshes, string sceneName = "CityScapeScene")
        {
            if (string.IsNullOrEmpty(targetFilePath)) throw new ArgumentNullException(nameof(targetFilePath));

            try
            {
                string dir = Path.GetDirectoryName(targetFilePath);
                string baseName = Path.GetFileNameWithoutExtension(targetFilePath);
                string mtlFile = baseName + ".mtl";
                string mtlPath = Path.Combine(dir, mtlFile);

                StringBuilder objSb = new StringBuilder();
                StringBuilder mtlSb = new StringBuilder();

                objSb.AppendLine("# PixelActive CityScape 1.8 Modern Exporter");
                objSb.AppendLine("mtllib " + mtlFile);

                mtlSb.AppendLine("# PixelActive CityScape Modern PBR Material Library");

                uint baseVertexOffset = 1;

                List<ExportMeshData> meshList = new List<ExportMeshData>(meshes ?? new ExportMeshData[0]);
                if (meshList.Count == 0)
                {
                    var fallback = new ExportMeshData { Name = "TerrainMesh_L0" };
                    fallback.Vertices.Add(new ExportVertex { X = -100f, Y = 0f, Z = -100f, Nx = 0f, Ny = 1f, Nz = 0f, U = 0f, V = 0f });
                    fallback.Vertices.Add(new ExportVertex { X = 100f, Y = 0f, Z = -100f, Nx = 0f, Ny = 1f, Nz = 0f, U = 1f, V = 0f });
                    fallback.Vertices.Add(new ExportVertex { X = 100f, Y = 0f, Z = 100f, Nx = 0f, Ny = 1f, Nz = 0f, U = 1f, V = 1f });
                    fallback.Vertices.Add(new ExportVertex { X = -100f, Y = 0f, Z = 100f, Nx = 0f, Ny = 1f, Nz = 0f, U = 0f, V = 1f });
                    fallback.Indices.AddRange(new uint[] { 0, 1, 2, 0, 2, 3 });
                    meshList.Add(fallback);
                }

                foreach (var mesh in meshList)
                {
                    string safeName = string.IsNullOrEmpty(mesh.Name) ? "Mesh" : mesh.Name;
                    objSb.AppendLine("o " + safeName);
                    objSb.AppendLine("usemtl Mat_" + safeName);

                    // Material
                    var m = mesh.Material ?? new PbrMaterialParameters();
                    mtlSb.AppendLine("newmtl Mat_" + safeName);
                    mtlSb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "Kd {0:F3} {1:F3} {2:F3}", m.BaseColorR, m.BaseColorG, m.BaseColorB));
                    mtlSb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "Pr {0:F3}", m.Roughness));
                    mtlSb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "Pm {0:F3}", m.Metallic));
                    if (!string.IsNullOrEmpty(m.AlbedoMapPath))
                    {
                        mtlSb.AppendLine("map_Kd " + m.AlbedoMapPath);
                    }
                    if (!string.IsNullOrEmpty(m.NormalMapPath))
                    {
                        mtlSb.AppendLine("map_Bump " + m.NormalMapPath);
                    }
                    if (!string.IsNullOrEmpty(m.MetallicRoughnessMapPath))
                    {
                        mtlSb.AppendLine("map_Pr " + m.MetallicRoughnessMapPath);
                    }
                    mtlSb.AppendLine();

                    foreach (var v in mesh.Vertices)
                    {
                        objSb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "v {0:F4} {1:F4} {2:F4}", v.X, v.Y, v.Z));
                        objSb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "vn {0:F4} {1:F4} {2:F4}", v.Nx, v.Ny, v.Nz));
                        objSb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "vt {0:F4} {1:F4}", v.U, v.V));
                    }

                    List<uint> indices = mesh.Indices;
                    if (indices.Count == 0 && mesh.Vertices.Count >= 3)
                    {
                        indices = new List<uint>();
                        for (uint i = 0; i < mesh.Vertices.Count; i++) indices.Add(i);
                    }

                    for (int i = 0; i < indices.Count; i += 3)
                    {
                        if (i + 2 < indices.Count)
                        {
                            uint i0 = indices[i] + baseVertexOffset;
                            uint i1 = indices[i + 1] + baseVertexOffset;
                            uint i2 = indices[i + 2] + baseVertexOffset;
                            objSb.AppendLine(string.Format("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}", i0, i1, i2));
                        }
                    }

                    baseVertexOffset += (uint)mesh.Vertices.Count;
                }

                File.WriteAllText(targetFilePath, objSb.ToString(), Encoding.UTF8);
                File.WriteAllText(mtlPath, mtlSb.ToString(), Encoding.UTF8);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
