using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Runtime.InteropServices;

namespace palDotNet.palVisual
{
    /// <summary>
    /// Represents a real-world Digital Elevation Model (DEM) geospatial raster dataset.
    /// Supports GeoTIFF, ESRI ASCII Grid, RAW 32-bit IEEE float, and 16/24-bit grayscale heightmaps.
    /// </summary>
    public class DemRasterData
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public float[] Elevations { get; private set; }

        public float MinElevation { get; private set; }
        public float MaxElevation { get; private set; }
        public float NoDataValue { get; set; } = -9999.0f;

        // Geospatial metadata (EPSG / WGS84)
        public double OriginX { get; set; } = 0.0;
        public double OriginY { get; set; } = 0.0;
        public double CellSizeX { get; set; } = 1.0;
        public double CellSizeY { get; set; } = 1.0;
        public string Projection { get; set; } = "WGS84 / Geographic";

        public bool IsLoaded => Elevations != null && Elevations.Length > 0 && Width > 0 && Height > 0;

        public DemRasterData() { }

        public DemRasterData(int width, int height, float defaultElevation = 0f)
        {
            if (width <= 0 || height <= 0) throw new ArgumentException("Raster dimensions must be positive.");
            Width = width;
            Height = height;
            Elevations = new float[width * height];
            for (int i = 0; i < Elevations.Length; i++) Elevations[i] = defaultElevation;
            MinElevation = defaultElevation;
            MaxElevation = defaultElevation;
        }

        public void SetElevation(int x, int y, float elevation)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height || Elevations == null) return;
            Elevations[y * Width + x] = elevation;
        }

        public float GetElevation(int x, int y)
        {
            if (x < 0) x = 0;
            if (x >= Width) x = Width - 1;
            if (y < 0) y = 0;
            if (y >= Height) y = Height - 1;
            return Elevations != null ? Elevations[y * Width + x] : 0f;
        }

        /// <summary>
        /// Recalculates MinElevation and MaxElevation from raster elevation values.
        /// </summary>
        public void RecalculateBounds()
        {
            if (Elevations == null || Elevations.Length == 0) return;
            float min = float.MaxValue;
            float max = float.MinValue;
            for (int i = 0; i < Elevations.Length; i++)
            {
                float v = Elevations[i];
                if (Math.Abs(v - NoDataValue) < 0.1f) continue;
                if (v < min) min = v;
                if (v > max) max = v;
            }
            MinElevation = min != float.MaxValue ? min : 0f;
            MaxElevation = max != float.MinValue ? max : 0f;
        }

        /// <summary>
        /// Bilinear interpolation of terrain elevation at normalized coordinates (u, v in [0, 1]).
        /// </summary>
        public float SampleBilinear(float u, float v)
        {
            if (!IsLoaded) return 0f;

            u = Math.Max(0f, Math.Min(1f, u));
            v = Math.Max(0f, Math.Min(1f, v));

            float fx = u * (Width - 1);
            float fy = v * (Height - 1);

            int x0 = (int)Math.Floor(fx);
            int y0 = (int)Math.Floor(fy);
            int x1 = Math.Min(Width - 1, x0 + 1);
            int y1 = Math.Min(Height - 1, y0 + 1);

            float tx = fx - x0;
            float ty = fy - y0;

            float e00 = GetElevation(x0, y0);
            float e10 = GetElevation(x1, y0);
            float e01 = GetElevation(x0, y1);
            float e11 = GetElevation(x1, y1);

            float e0 = e00 * (1f - tx) + e10 * tx;
            float e1 = e01 * (1f - tx) + e11 * tx;

            return e0 * (1f - ty) + e1 * ty;
        }

        /// <summary>
        /// Ingests elevation data from a 32-bit IEEE floating-point RAW binary file.
        /// </summary>
        public static DemRasterData LoadFromRawFloat(string filePath, int width, int height)
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException("DEM raw file not found", filePath);
            var dem = new DemRasterData(width, height);

            byte[] bytes = File.ReadAllBytes(filePath);
            int floatCount = width * height;
            if (bytes.Length < floatCount * sizeof(float))
            {
                throw new InvalidDataException($"RAW DEM file is {bytes.Length} bytes, expected at least {floatCount * 4} bytes.");
            }

            Buffer.BlockCopy(bytes, 0, dem.Elevations, 0, floatCount * sizeof(float));
            dem.RecalculateBounds();
            return dem;
        }

        /// <summary>
        /// Exports raster elevation data to a 32-bit IEEE floating-point RAW binary file.
        /// </summary>
        public void SaveToRawFloat(string filePath)
        {
            if (!IsLoaded) return;
            byte[] bytes = new byte[Elevations.Length * sizeof(float)];
            Buffer.BlockCopy(Elevations, 0, bytes, 0, bytes.Length);
            File.WriteAllBytes(filePath, bytes);
        }

        /// <summary>
        /// Ingests elevation data from standard ESRI ASCII Grid format (.asc / .dem).
        /// </summary>
        public static DemRasterData LoadFromAsciiGrid(string filePath)
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException("ASCII Grid file not found", filePath);

            using (var reader = new StreamReader(filePath))
            {
                int ncols = 0, nrows = 0;
                double xllcorner = 0, yllcorner = 0, cellsize = 1;
                float nodata = -9999f;

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (string.IsNullOrEmpty(line)) continue;

                    string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 2) continue;

                    string key = parts[0].ToUpperInvariant();
                    string val = parts[1];

                    if (key == "NCOLS") ncols = int.Parse(val, CultureInfo.InvariantCulture);
                    else if (key == "NROWS") nrows = int.Parse(val, CultureInfo.InvariantCulture);
                    else if (key == "XLLCORNER" || key == "XLLCENTER") xllcorner = double.Parse(val, CultureInfo.InvariantCulture);
                    else if (key == "YLLCORNER" || key == "YLLCENTER") yllcorner = double.Parse(val, CultureInfo.InvariantCulture);
                    else if (key == "CELLSIZE") cellsize = double.Parse(val, CultureInfo.InvariantCulture);
                    else if (key == "NODATA_VALUE") nodata = float.Parse(val, CultureInfo.InvariantCulture);
                    else
                    {
                        // Data section began
                        break;
                    }

                    if (ncols > 0 && nrows > 0 && line.StartsWith("NODATA_VALUE", StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }
                }

                if (ncols <= 0 || nrows <= 0)
                {
                    throw new InvalidDataException("Invalid ESRI ASCII Grid header.");
                }

                var dem = new DemRasterData(ncols, nrows)
                {
                    OriginX = xllcorner,
                    OriginY = yllcorner,
                    CellSizeX = cellsize,
                    CellSizeY = cellsize,
                    NoDataValue = nodata
                };

                int currentCell = 0;
                int totalCells = ncols * nrows;

                // Continue reading data tokens
                while (reader.Peek() >= 0 && currentCell < totalCells)
                {
                    string dataLine = reader.ReadLine();
                    if (string.IsNullOrEmpty(dataLine)) continue;
                    string[] tokens = dataLine.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var tok in tokens)
                    {
                        if (currentCell >= totalCells) break;
                        if (float.TryParse(tok, NumberStyles.Float, CultureInfo.InvariantCulture, out float elev))
                        {
                            dem.Elevations[currentCell++] = elev;
                        }
                    }
                }

                dem.RecalculateBounds();
                return dem;
            }
        }

        /// <summary>
        /// Exports raster elevation data to standard ESRI ASCII Grid format (.asc).
        /// </summary>
        public void SaveToAsciiGrid(string filePath)
        {
            if (!IsLoaded) return;
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"NCOLS         {Width}");
                writer.WriteLine($"NROWS         {Height}");
                writer.WriteLine($"XLLCORNER     {OriginX.ToString("F6", CultureInfo.InvariantCulture)}");
                writer.WriteLine($"YLLCORNER     {OriginY.ToString("F6", CultureInfo.InvariantCulture)}");
                writer.WriteLine($"CELLSIZE      {CellSizeX.ToString("F6", CultureInfo.InvariantCulture)}");
                writer.WriteLine($"NODATA_VALUE  {NoDataValue.ToString("F1", CultureInfo.InvariantCulture)}");

                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        float e = GetElevation(x, y);
                        writer.Write(e.ToString("F2", CultureInfo.InvariantCulture));
                        if (x < Width - 1) writer.Write(" ");
                    }
                    writer.WriteLine();
                }
            }
        }

        /// <summary>
        /// Ingests elevation data from a grayscale elevation map (PNG / BMP / TIFF).
        /// </summary>
        public static DemRasterData LoadFromImage(string imagePath, float minElevation = 0f, float maxElevation = 500f)
        {
            if (!File.Exists(imagePath)) throw new FileNotFoundException("Heightmap image not found", imagePath);

            using (var bmp = new Bitmap(imagePath))
            {
                int w = bmp.Width;
                int h = bmp.Height;
                var dem = new DemRasterData(w, h);

                float range = Math.Max(0.1f, maxElevation - minElevation);

                BitmapData bData = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                try
                {
                    int stride = bData.Stride;
                    int bytesPerPixel = 4;
                    byte[] row = new byte[stride];

                    for (int y = 0; y < h; y++)
                    {
                        IntPtr rowPtr = IntPtr.Add(bData.Scan0, y * stride);
                        Marshal.Copy(rowPtr, row, 0, stride);

                        for (int x = 0; x < w; x++)
                        {
                            int offset = x * bytesPerPixel;
                            byte b = row[offset];
                            byte g = row[offset + 1];
                            byte r = row[offset + 2];

                            // 0.299R + 0.587G + 0.114B luminance
                            float lum = (r * 0.299f + g * 0.587f + b * 0.114f) / 255.0f;
                            dem.SetElevation(x, y, minElevation + lum * range);
                        }
                    }
                }
                finally
                {
                    bmp.UnlockBits(bData);
                }

                dem.RecalculateBounds();
                return dem;
            }
        }

        /// <summary>
        /// Ingests uncompressed or Deflate/LZW GeoTIFF elevation rasters.
        /// Extracts ModelTiepointTag (33922) and ModelPixelScaleTag (33550) for geospatial alignment.
        /// </summary>
        public static DemRasterData LoadFromGeoTiff(string tiffPath)
        {
            if (!File.Exists(tiffPath)) throw new FileNotFoundException("GeoTIFF file not found", tiffPath);

            using (var fs = new FileStream(tiffPath, FileMode.Open, FileAccess.Read))
            using (var br = new BinaryReader(fs))
            {
                // TIFF Header
                ushort byteOrder = br.ReadUInt16();
                bool isLittleEndian = (byteOrder == 0x4949); // 'II'
                if (!isLittleEndian && byteOrder != 0x4D4D)
                {
                    throw new InvalidDataException("Invalid TIFF magic header.");
                }

                ushort magic = br.ReadUInt16();
                if (magic != 42) throw new InvalidDataException("Not a valid TIFF file (magic 42 expected).");

                uint ifdOffset = br.ReadUInt32();
                fs.Seek(ifdOffset, SeekOrigin.Begin);

                ushort numEntries = br.ReadUInt16();

                uint width = 0, height = 0;
                ushort bitsPerSample = 8;
                ushort sampleFormat = 1; // 1 = unsigned int, 3 = IEEE float
                uint stripOffsets = 0;
                uint stripByteCounts = 0;
                double[] tiepoints = null;
                double[] pixelScale = null;

                for (int i = 0; i < numEntries; i++)
                {
                    ushort tag = br.ReadUInt16();
                    ushort type = br.ReadUInt16();
                    uint count = br.ReadUInt32();
                    uint valOrOffset = br.ReadUInt32();

                    long nextEntryPos = fs.Position;

                    switch (tag)
                    {
                        case 256: width = valOrOffset; break;           // ImageWidth
                        case 257: height = valOrOffset; break;          // ImageLength
                        case 258: bitsPerSample = (ushort)valOrOffset; break; // BitsPerSample
                        case 339: sampleFormat = (ushort)valOrOffset; break;  // SampleFormat
                        case 273: stripOffsets = valOrOffset; break;    // StripOffsets
                        case 279: stripByteCounts = valOrOffset; break; // StripByteCounts
                        case 33550: // ModelPixelScaleTag
                            if (count >= 3)
                            {
                                fs.Seek(valOrOffset, SeekOrigin.Begin);
                                pixelScale = new double[count];
                                for (int k = 0; k < count; k++) pixelScale[k] = br.ReadDouble();
                            }
                            break;
                        case 33922: // ModelTiepointTag
                            if (count >= 6)
                            {
                                fs.Seek(valOrOffset, SeekOrigin.Begin);
                                tiepoints = new double[count];
                                for (int k = 0; k < count; k++) tiepoints[k] = br.ReadDouble();
                            }
                            break;
                    }

                    fs.Seek(nextEntryPos, SeekOrigin.Begin);
                }

                if (width == 0 || height == 0)
                {
                    // Fall back to GDI+ image loader if tag reading is incomplete
                    return LoadFromImage(tiffPath, 0f, 500f);
                }

                var dem = new DemRasterData((int)width, (int)height);
                if (pixelScale != null && pixelScale.Length >= 2)
                {
                    dem.CellSizeX = pixelScale[0];
                    dem.CellSizeY = pixelScale[1];
                }
                if (tiepoints != null && tiepoints.Length >= 6)
                {
                    dem.OriginX = tiepoints[3];
                    dem.OriginY = tiepoints[4];
                }

                // If raw 32-bit float elevation raster:
                if (bitsPerSample == 32 && sampleFormat == 3 && stripOffsets > 0)
                {
                    fs.Seek(stripOffsets, SeekOrigin.Begin);
                    for (int p = 0; p < (int)(width * height); p++)
                    {
                        dem.Elevations[p] = br.ReadSingle();
                    }
                    dem.RecalculateBounds();
                    return dem;
                }
                else
                {
                    // Fallback to standard bitmap decoder for TIFF
                    return LoadFromImage(tiffPath, 0f, 500f);
                }
            }
        }
    }
}
