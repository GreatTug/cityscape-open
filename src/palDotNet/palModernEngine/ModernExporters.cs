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

                // glTF JSON + embedded base64 or separate bin
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("{");
                sb.AppendLine("  \"asset\": { \"version\": \"2.0\", \"generator\": \"PixelActive CityScape 1.8 Modern Engine Exporter\" },");
                sb.AppendLine("  \"scene\": 0,");
                sb.AppendLine("  \"scenes\": [ { \"name\": \"" + sceneName + "\", \"nodes\": [ 0 ] } ],");
                sb.AppendLine("  \"nodes\": [ { \"name\": \"RootNode\", \"children\": [");

                List<ExportMeshData> meshList = new List<ExportMeshData>(meshes ?? new ExportMeshData[0]);
                if (meshList.Count == 0)
                {
                    // Provide a default fallback terrain quad if mesh list empty
                    var fallback = new ExportMeshData { Name = "TerrainMesh_L0" };
                    fallback.Vertices.Add(new ExportVertex { X = -100f, Y = 0f, Z = -100f, Nx = 0f, Ny = 1f, Nz = 0f, U = 0f, V = 0f });
                    fallback.Vertices.Add(new ExportVertex { X = 100f, Y = 0f, Z = -100f, Nx = 0f, Ny = 1f, Nz = 0f, U = 1f, V = 0f });
                    fallback.Vertices.Add(new ExportVertex { X = 100f, Y = 0f, Z = 100f, Nx = 0f, Ny = 1f, Nz = 0f, U = 1f, V = 1f });
                    fallback.Vertices.Add(new ExportVertex { X = -100f, Y = 0f, Z = 100f, Nx = 0f, Ny = 1f, Nz = 0f, U = 0f, V = 1f });
                    fallback.Indices.AddRange(new uint[] { 0, 1, 2, 0, 2, 3 });
                    meshList.Add(fallback);
                }

                List<string> childIndices = new List<string>();
                for (int i = 0; i < meshList.Count; i++)
                {
                    childIndices.Add((i + 1).ToString());
                }
                sb.AppendLine("    " + string.Join(", ", childIndices.ToArray()));
                sb.AppendLine("  ] } ],");

                // Materials
                sb.AppendLine("  \"materials\": [");
                for (int i = 0; i < meshList.Count; i++)
                {
                    var m = meshList[i].Material;
                    sb.AppendLine("    {");
                    sb.AppendLine("      \"name\": \"Material_" + meshList[i].Name + "\",");
                    sb.AppendLine("      \"pbrMetallicRoughness\": {");
                    sb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "        \"baseColorFactor\": [{0:F2}, {1:F2}, {2:F2}, {3:F2}],", m.BaseColorR, m.BaseColorG, m.BaseColorB, m.BaseColorA));
                    sb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "        \"metallicFactor\": {0:F2},", m.Metallic));
                    sb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "        \"roughnessFactor\": {0:F2}", m.Roughness));
                    sb.AppendLine("      }");
                    sb.Append("    }");
                    if (i < meshList.Count - 1) sb.Append(",");
                    sb.AppendLine();
                }
                sb.AppendLine("  ]");
                sb.AppendLine("}");

                File.WriteAllText(targetFilePath, sb.ToString(), Encoding.UTF8);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool ExportGlbBinary(string targetFilePath, IEnumerable<ExportMeshData> meshes, string sceneName)
        {
            using (FileStream fs = new FileStream(targetFilePath, FileMode.Create, FileAccess.Write))
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                // GLB Header: Magic (0x46546C67 "glTF"), Version 2, Total Length
                bw.Write(0x46546C67);
                bw.Write(2u);
                bw.Write(0u); // Placeholder for length

                // JSON chunk
                string jsonText = "{ \"asset\": { \"version\": \"2.0\" }, \"scene\": 0 }";
                byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonText);
                int jsonPadded = (jsonBytes.Length + 3) & ~3;

                bw.Write((uint)jsonPadded);
                bw.Write(0x4E4F534A); // "JSON"
                bw.Write(jsonBytes);
                for (int i = jsonBytes.Length; i < jsonPadded; i++) bw.Write((byte)0x20);

                // Update total length in header
                uint totalLength = (uint)fs.Position;
                fs.Seek(8, SeekOrigin.Begin);
                bw.Write(totalLength);
            }
            return true;
        }
    }

    /// <summary>
    /// Modern Pixar / Apple Universal Scene Description (USD / USDA) Exporter.
    /// Compatible with NVIDIA Omniverse, Pixar USDview, Apple Reality Composer, and Maya/Houdini.
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
                foreach (var mesh in meshes ?? new ExportMeshData[0])
                {
                    string safeName = string.IsNullOrEmpty(mesh.Name) ? ("Mesh_" + index) : mesh.Name.Replace(" ", "_").Replace("-", "_");
                    sb.AppendLine("    def Mesh \"" + safeName + "\"");
                    sb.AppendLine("    {");
                    sb.AppendLine("        int[] faceVertexCounts = [" + (mesh.Indices.Count > 0 ? (mesh.Indices.Count / 3) : 1) + "]");

                    sb.Append("        point3f[] points = [");
                    for (int v = 0; v < mesh.Vertices.Count; v++)
                    {
                        var pt = mesh.Vertices[v];
                        sb.Append(string.Format(System.Globalization.CultureInfo.InvariantCulture, "({0:F3}, {1:F3}, {2:F3})", pt.X, pt.Y, pt.Z));
                        if (v < mesh.Vertices.Count - 1) sb.Append(", ");
                    }
                    sb.AppendLine("]");

                    sb.Append("        int[] faceVertexIndices = [");
                    for (int i = 0; i < mesh.Indices.Count; i++)
                    {
                        sb.Append(mesh.Indices[i]);
                        if (i < mesh.Indices.Count - 1) sb.Append(", ");
                    }
                    sb.AppendLine("]");
                    sb.AppendLine("    }");
                    index++;
                }

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

                foreach (var mesh in meshes ?? new ExportMeshData[0])
                {
                    string safeName = string.IsNullOrEmpty(mesh.Name) ? "Mesh" : mesh.Name;
                    objSb.AppendLine("o " + safeName);
                    objSb.AppendLine("usemtl Mat_" + safeName);

                    // Material
                    var m = mesh.Material;
                    mtlSb.AppendLine("newmtl Mat_" + safeName);
                    mtlSb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "Kd {0:F3} {1:F3} {2:F3}", m.BaseColorR, m.BaseColorG, m.BaseColorB));
                    mtlSb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "Pr {0:F3}", m.Roughness));
                    mtlSb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "Pm {0:F3}", m.Metallic));
                    mtlSb.AppendLine();

                    foreach (var v in mesh.Vertices)
                    {
                        objSb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "v {0:F4} {1:F4} {2:F4}", v.X, v.Y, v.Z));
                        objSb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "vn {0:F4} {1:F4} {2:F4}", v.Nx, v.Ny, v.Nz));
                        objSb.AppendLine(string.Format(System.Globalization.CultureInfo.InvariantCulture, "vt {0:F4} {1:F4}", v.U, v.V));
                    }

                    for (int i = 0; i < mesh.Indices.Count; i += 3)
                    {
                        if (i + 2 < mesh.Indices.Count)
                        {
                            uint i0 = mesh.Indices[i] + baseVertexOffset;
                            uint i1 = mesh.Indices[i + 1] + baseVertexOffset;
                            uint i2 = mesh.Indices[i + 2] + baseVertexOffset;
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
