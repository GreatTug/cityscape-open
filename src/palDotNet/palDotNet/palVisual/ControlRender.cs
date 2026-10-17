using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Permissions;
using System.Windows.Forms;
using palDotNet.palWidgets;
using palDotNet.palModernEngine;

namespace palDotNet.palVisual;

public class ControlRender : DragDropControl, IRenderWindowVisual
{
	private const int WM_PAINT = 15;

	private static bool TheProgressBarRenderWindowEnabled = true;

	private RenderWindow TheRenderWindow;

	private bool ValidSent;

	private bool ProgressBarStarted;

	private bool AppPaused;

	private IntPtr LastHandle;

	private int LastXRes;

	private int LastYRes;

	private static List<ControlRender> ControlRenders = new List<ControlRender>();

	public static TerrainSurfaceData ActiveTerrain { get; set; } = TerrainSurfaceData.CreateDefault();
	public static List<GisBuildingInstance> ActiveBuildings { get; } = new List<GisBuildingInstance>();
	public static SpatialQuadtree<GisBuildingInstance> BuildingSpatialIndex { get; private set; }
	public static CdlodTerrainQuadtree TerrainCdlodTree { get; private set; }

	public static void SetActiveBuildings(IEnumerable<GisBuildingInstance> buildings)
	{
		ActiveBuildings.Clear();
		if (buildings != null)
		{
			ActiveBuildings.AddRange(buildings);
		}

		float halfX = Math.Max(1000f, (ActiveTerrain?.SizeX ?? 2000f) * 0.5f);
		float halfZ = Math.Max(1000f, (ActiveTerrain?.SizeY ?? 2000f) * 0.5f);
		var worldBounds = new BoundingBox3D(-halfX, -100f, -halfZ, halfX, 2000f, halfZ);
		BuildingSpatialIndex = new SpatialQuadtree<GisBuildingInstance>(worldBounds, 6, 16);
		if (buildings != null)
		{
			foreach (var b in buildings)
			{
				BuildingSpatialIndex.Insert(b);
			}
		}

		foreach (ControlRender cr in ControlRenders)
		{
			cr.Invalidate();
		}
	}

	public static void SetActiveTerrain(TerrainSurfaceData terrain)
	{
		if (terrain == null) return;
		ActiveTerrain = terrain;
		TerrainCdlodTree = new CdlodTerrainQuadtree(terrain, 4);
		foreach (ControlRender cr in ControlRenders)
		{
			cr.ResetCamera();
			cr.Invalidate();
		}
	}

	private Point mLastMouseLocation;
	private bool mIsOrbiting = false;
	private bool mIsPanning = false;

	private float mCameraYaw = -35.0f;
	private float mCameraPitch = 24.0f;
	private float mCameraDistance = 0f;
	private float mCameraPanX = 0f;
	private float mCameraPanY = 0f;

	public void ResetCamera()
	{
		mCameraYaw = -35.0f;
		mCameraPitch = 24.0f;
		mCameraDistance = 0f;
		mCameraPanX = 0f;
		mCameraPanY = 0f;
	}

	private MouseButtons CurrentMouseButtons;

	private int LastX = int.MaxValue;

	private int LastY = int.MaxValue;

	private IContainer components;

	private ProgressBar progressBar1;

	private System.Windows.Forms.Label labelProgressBar;

	private Panel panel1;

	private HScrollBar hScrollBar1;

	private VScrollBar vScrollBar1;

	public Cursors.EnumCursor CurrentCursor
	{
		set
		{
			Cursors.SetCurrent(this, value);
		}
	}

	public static bool ProgressBarRenderWindowEnabled
	{
		get
		{
			return TheProgressBarRenderWindowEnabled;
		}
		set
		{
			TheProgressBarRenderWindowEnabled = value;
			if (!TheProgressBarRenderWindowEnabled)
			{
				UpdateProgressBar(startBar: false, null);
			}
		}
	}

	[ReadOnly(true)]
	[Browsable(false)]
	public RenderWindow RenderWindow
	{
		get
		{
			return TheRenderWindow;
		}
		set
		{
			if (TheRenderWindow != null)
			{
				TheRenderWindow.Visual = null;
				TheRenderWindow.Hwnd = IntPtr.Zero;
			}
			TheRenderWindow = value;
			if (TheRenderWindow != null)
			{
				TheRenderWindow.Visual = this;
				TheRenderWindow.Hwnd = base.Handle;
			}
		}
	}

	public int GetValue(bool isHorizontal)
	{
		if (!isHorizontal)
		{
			return vScrollBar1.Value;
		}
		return hScrollBar1.Value;
	}

	public void SetVisible(bool isHorizontal, bool visible)
	{
		if (isHorizontal)
		{
			hScrollBar1.Visible = visible;
		}
		else
		{
			vScrollBar1.Visible = visible;
		}
	}

	public void SetValue(bool isHorizontal, int barValue)
	{
		if (isHorizontal)
		{
			hScrollBar1.Value = barValue;
		}
		else
		{
			vScrollBar1.Value = barValue;
		}
	}

	public void SetLargeChange(bool isHorizontal, int change)
	{
		if (isHorizontal)
		{
			hScrollBar1.LargeChange = change;
		}
		else
		{
			vScrollBar1.LargeChange = change;
		}
	}

	public void SetSmallChange(bool isHorizontal, int change)
	{
		if (isHorizontal)
		{
			hScrollBar1.SmallChange = change;
		}
		else
		{
			vScrollBar1.SmallChange = change;
		}
	}

	public void SetLimits(bool isHorizontal, int min, int max)
	{
		if (isHorizontal)
		{
			hScrollBar1.Minimum = min;
			hScrollBar1.Maximum = max;
		}
		else
		{
			vScrollBar1.Minimum = min;
			vScrollBar1.Maximum = max;
		}
	}

	public ControlRender()
	{
		InitializeComponent();
		LastHandle = base.Handle;
		SetStyle(ControlStyles.AllPaintingInWmPaint, value: true);
		SetStyle(ControlStyles.UserPaint, value: true);
		SetStyle(ControlStyles.Opaque, value: true);
		base.Controls.Remove(panel1);
		ControlRenders.Add(this);
		base.PreviewKeyDown += ControlRender_PreviewKeyDown;
		base.DragDrop += base.DragDropControl_DragDrop;
	}

	public void SetNotVisible()
	{
		ControlRenders.Remove(this);
	}

	private void ControlRender_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
	{
		switch (e.KeyCode)
		{
		case Keys.Left:
		case Keys.Up:
		case Keys.Right:
		case Keys.Down:
			e.IsInputKey = true;
			break;
		}
	}

	public void SendResize()
	{
		if (WidgetMgr.Instance != null && RenderWindow != null)
		{
			_ = RenderWindow.Hwnd;
			if (TheRenderWindow != null)
			{
				TheRenderWindow.Hwnd = base.Handle;
			}
			if (AppPaused)
			{
				WidgetMgr.Instance.UnpauseApp();
				AppPaused = false;
			}
			LastXRes = base.Width;
			LastYRes = base.Height;
		}
	}

	public static void UpdateProgressBar(bool startBar, string textMsg)
	{
		foreach (ControlRender controlRender in ControlRenders)
		{
			controlRender.UpdateProgressBarInternal(startBar, textMsg);
		}
	}

	private void UpdateProgressBarInternal(bool startBar, string textMsg)
	{
		if (ProgressBarStarted != startBar || !(labelProgressBar.Text == textMsg))
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint, !startBar);
			SetStyle(ControlStyles.UserPaint, !startBar);
			labelProgressBar.Text = (startBar ? textMsg : string.Empty);
			if (startBar)
			{
				base.Controls.Add(panel1);
				Invalidate();
			}
			else
			{
				base.UseWaitCursor = false;
				base.Controls.Remove(panel1);
			}
			ProgressBarStarted = startBar;
		}
	}

	protected override void OnPaint(PaintEventArgs pe)
	{
		base.OnPaint(pe);
		if (TheRenderWindow == null)
		{
			Graphics g = pe.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;

			int w = Width;
			int h = Height;
			if (w <= 0 || h <= 0) return;

			// Professional dark slate gradient background (standard 3D modeling viewport)
			using (LinearGradientBrush bgBrush = new LinearGradientBrush(
				ClientRectangle,
				Color.FromArgb(38, 44, 52),
				Color.FromArgb(20, 24, 28),
				LinearGradientMode.Vertical))
			{
				g.FillRectangle(bgBrush, ClientRectangle);
			}

			Render3DTerrain(g, w, h);
		}
	}

	private void Render3DTerrain(Graphics g, int w, int h)
	{
		var terrain = ActiveTerrain ?? TerrainSurfaceData.CreateDefault();
		float sx = terrain.SizeX;
		float sy = terrain.SizeY;
		float diag = (float)Math.Sqrt(sx * sx + sy * sy);
		float camDist = mCameraDistance > 0f ? mCameraDistance : Math.Max(100f, diag * 1.30f);
		float focal = (float)Math.Min(w, h) * 1.25f;

		float yawRad = (float)(mCameraYaw * Math.PI / 180.0);
		float pitchRad = (float)(mCameraPitch * Math.PI / 180.0);
		float cosYaw = (float)Math.Cos(yawRad);
		float sinYaw = (float)Math.Sin(yawRad);
		float cosPitch = (float)Math.Cos(pitchRad);
		float sinPitch = (float)Math.Sin(pitchRad);

		PointF Project(float x, float y, float z)
		{
			float x1 = x * cosYaw - z * sinYaw;
			float z1 = x * sinYaw + z * cosYaw;
			float y1 = y;

			float y2 = y1 * cosPitch - z1 * sinPitch;
			float z2 = y1 * sinPitch + z1 * cosPitch;

			float z3 = z2 + camDist;
			if (z3 < 1.0f) z3 = 1.0f;

			float scale = focal / z3;
			float cx = (w * 0.5f) + mCameraPanX;
			float cy = (h * 0.55f) + mCameraPanY;

			return new PointF(cx + x1 * scale, cy - y2 * scale);
		}

		// 1. Ground Plane Foundation (infinite reference grid)
		using (Pen groundPen = new Pen(Color.FromArgb(40, 50, 62), 1f))
		{
			float gridExtent = Math.Max(sx, sy) * 1.6f;
			float step = gridExtent / 16.0f;
			for (float gx = -gridExtent; gx <= gridExtent; gx += step)
			{
				PointF pStart = Project(gx, -15f, -gridExtent);
				PointF pEnd = Project(gx, -15f, gridExtent);
				g.DrawLine(groundPen, pStart, pEnd);
			}
			for (float gz = -gridExtent; gz <= gridExtent; gz += step)
			{
				PointF pStart = Project(-gridExtent, -15f, gz);
				PointF pEnd = Project(gridExtent, -15f, gz);
				g.DrawLine(groundPen, pStart, pEnd);
			}
		}

		// 2. Extrude terrain corners to base -25m to give it physical volume and depth
		PointF b00 = Project(-sx * 0.5f, -25f, -sy * 0.5f);
		PointF b10 = Project(sx * 0.5f, -25f, -sy * 0.5f);
		PointF b11 = Project(sx * 0.5f, -25f, sy * 0.5f);
		PointF b01 = Project(-sx * 0.5f, -25f, sy * 0.5f);

		PointF t00 = Project(-sx * 0.5f, terrain.EvaluateElevation(-sx * 0.5f, -sy * 0.5f), -sy * 0.5f);
		PointF t10 = Project(sx * 0.5f, terrain.EvaluateElevation(sx * 0.5f, -sy * 0.5f), -sy * 0.5f);
		PointF t11 = Project(sx * 0.5f, terrain.EvaluateElevation(sx * 0.5f, sy * 0.5f), sy * 0.5f);
		PointF t01 = Project(-sx * 0.5f, terrain.EvaluateElevation(-sx * 0.5f, sy * 0.5f), sy * 0.5f);

		using (SolidBrush skirtBrush = new SolidBrush(Color.FromArgb(32, 38, 44)))
		using (Pen skirtPen = new Pen(Color.FromArgb(55, 68, 80), 1.5f))
		{
			PointF[] frontSkirt = new PointF[] { t01, t11, b11, b01 };
			g.FillPolygon(skirtBrush, frontSkirt);
			g.DrawPolygon(skirtPen, frontSkirt);

			PointF[] leftSkirt = new PointF[] { t00, t01, b01, b00 };
			g.FillPolygon(skirtBrush, leftSkirt);
			g.DrawPolygon(skirtPen, leftSkirt);
		}

		// Camera vectors & Frustum setup
		float fwdX = sinYaw * cosPitch;
		float fwdY = sinPitch;
		float fwdZ = cosYaw * cosPitch;

		float camX = -fwdX * camDist;
		float camY = -fwdY * camDist;
		float camZ = -fwdZ * camDist;
		float farClip = Math.Max(15000f, diag * 3.0f);
		float aspect = (float)w / Math.Max(1, h);

		var frustum = ViewFrustum3D.CreateFromOrbitProjection(mCameraYaw, mCameraPitch, camDist, w, h, focal, 1.0f, farClip);

		// 3. CDLOD Multi-Resolution Hierarchical Terrain Mesh (Frustum-Culled Chunks)
		if (TerrainCdlodTree == null || TerrainCdlodTree.Terrain != terrain)
		{
			TerrainCdlodTree = new CdlodTerrainQuadtree(terrain, 4);
		}

		int terrainChunksTested = 0;
		int terrainChunksCulled = 0;
		var cdlodChunks = TerrainCdlodTree.SelectVisibleChunks(frustum, camX, camY, camZ, out terrainChunksTested, out terrainChunksCulled);

		using (Pen meshPen = new Pen(Color.FromArgb(40, 180, 220, 255), 1f))
		{
			foreach (var chunk in cdlodChunks)
			{
				float chunkLenX = chunk.Node.MaxX - chunk.Node.MinX;
				float chunkLenZ = chunk.Node.MaxZ - chunk.Node.MinZ;
				float aspectXZ = chunkLenX / Math.Max(1.0f, chunkLenZ);

				int baseDiv = Math.Max(4, 16 >> Math.Min(3, chunk.LodLevel));
				int divX = Math.Min(48, Math.Max(4, (int)Math.Round(baseDiv * Math.Sqrt(Math.Max(1.0, aspectXZ)))));
				int divZ = Math.Min(48, Math.Max(2, (int)Math.Round(baseDiv * Math.Sqrt(Math.Max(1.0, 1.0 / Math.Max(0.01, aspectXZ))))));

				float stepX = chunkLenX / divX;
				float stepZ = chunkLenZ / divZ;

				for (int j = 0; j < divZ; j++)
				{
					float z0 = chunk.Node.MinZ + j * stepZ;
					float z1 = chunk.Node.MinZ + (j + 1) * stepZ;

					for (int i = 0; i < divX; i++)
					{
						float x0 = chunk.Node.MinX + i * stepX;
						float x1 = chunk.Node.MinX + (i + 1) * stepX;

						float e00 = terrain.EvaluateElevation(x0, z0);
						float e10 = terrain.EvaluateElevation(x1, z0);
						float e11 = terrain.EvaluateElevation(x1, z1);
						float e01 = terrain.EvaluateElevation(x0, z1);

						PointF p00 = Project(x0, e00, z0);
						PointF p10 = Project(x1, e10, z0);
						PointF p11 = Project(x1, e11, z1);
						PointF p01 = Project(x0, e01, z1);

						float avgElev = (e00 + e10 + e11 + e01) * 0.25f;
						float elevRatio = Math.Max(0f, Math.Min(1f, avgElev / Math.Max(1f, terrain.ElevationMax)));

						// PBR terrain splat coloring (valley green -> rocky ridge -> peak)
						int r = (int)(42 + elevRatio * 85);
						int gCol = (int)(75 + (1f - elevRatio) * 35 + elevRatio * 25);
						int bCol = (int)(55 + elevRatio * 60);

						float lightFactor = 0.88f + 0.12f * (float)Math.Cos((x0 + z0) * 0.001);
						r = Math.Min(255, (int)(r * lightFactor));
						gCol = Math.Min(255, (int)(gCol * lightFactor));
						bCol = Math.Min(255, (int)(bCol * lightFactor));

						using (SolidBrush quadBrush = new SolidBrush(Color.FromArgb(r, gCol, bCol)))
						{
							PointF[] quad = new PointF[] { p00, p10, p11, p01 };
							g.FillPolygon(quadBrush, quad);
							g.DrawPolygon(meshPen, quad);
						}
					}
				}
			}
		}

		// 3.5. 3D Procedural / GIS Extruded Buildings with Spatial Quadtree Frustum Culling
		int renderedBuildings = 0;
		int nodesTested = 0;
		int nodesCulled = 0;

		if (ActiveBuildings != null && ActiveBuildings.Count > 0)
		{
			if (BuildingSpatialIndex == null)
			{
				float halfX = Math.Max(1000f, sx * 0.5f);
				float halfZ = Math.Max(1000f, sy * 0.5f);
				var worldBounds = new BoundingBox3D(-halfX, -100f, -halfZ, halfX, 2000f, halfZ);
				BuildingSpatialIndex = new SpatialQuadtree<GisBuildingInstance>(worldBounds, 6, 16);
				foreach (var b in ActiveBuildings)
				{
					BuildingSpatialIndex.Insert(b);
				}
			}

			var visibleBuildings = BuildingSpatialIndex.QueryFrustum(frustum, out nodesTested, out nodesCulled);

			using (Pen buildingWirePen = new Pen(Color.FromArgb(70, 90, 110), 1f))
			{
				foreach (var bld in visibleBuildings)
				{
					renderedBuildings++;
					var poly = bld.Footprint.Polygon;
					int polyN = poly.Count;
					if (polyN < 3) continue;

					float bY = bld.BaseElevation;
					float tY = bld.BaseElevation + bld.TotalHeight;

					PointF[] topPoints = new PointF[polyN];
					for (int p = 0; p < polyN; p++)
					{
						topPoints[p] = Project(poly[p].X, tY, poly[p].Y);
					}

					for (int p = 0; p < polyN; p++)
					{
						int next = (p + 1) % polyN;
						PointF p0_bot = Project(poly[p].X, bY, poly[p].Y);
						PointF p1_bot = Project(poly[next].X, bY, poly[next].Y);
						PointF p1_top = topPoints[next];
						PointF p0_top = topPoints[p];

						float dx = poly[next].X - poly[p].X;
						float dz = poly[next].Y - poly[p].Y;
						float normX = dz;
						float normZ = -dx;
						float nLen = (float)Math.Sqrt(normX * normX + normZ * normZ);
						float shade = 0.75f;
						if (nLen > 0.0001f)
						{
							normX /= nLen;
							normZ /= nLen;
							shade = Math.Max(0.4f, Math.Min(1.0f, (normX * 0.6f + normZ * 0.6f + 0.5f) * 0.85f));
						}

						Color wallCol = Color.FromArgb(
							(int)(bld.FacadeColor.R * shade),
							(int)(bld.FacadeColor.G * shade),
							(int)(bld.FacadeColor.B * shade));

						using (SolidBrush wallBrush = new SolidBrush(wallCol))
						{
							PointF[] wallQuad = new PointF[] { p0_bot, p1_bot, p1_top, p0_top };
							g.FillPolygon(wallBrush, wallQuad);
							g.DrawPolygon(buildingWirePen, wallQuad);
						}
					}

					using (SolidBrush roofBrush = new SolidBrush(bld.RoofColor))
					{
						g.FillPolygon(roofBrush, topPoints);
						g.DrawPolygon(buildingWirePen, topPoints);
					}
				}
			}
		}

		// 4. Physical Tile Demarcations in glowing cyan
		using (Pen tilePen = new Pen(Color.FromArgb(0, 235, 255), 2.2f))
		using (Font tileFont = new Font("Segoe UI", 8.5f, FontStyle.Bold))
		using (SolidBrush tileBrush = new SolidBrush(Color.FromArgb(180, 240, 255)))
		{
			for (int tx = 0; tx <= terrain.TileCountX; tx++)
			{
				float xPos = -sx * 0.5f + tx * terrain.TileSizeX;
				if (xPos > sx * 0.5f + 1f) continue;
				PointF pA = Project(xPos, 0f, -sy * 0.5f);
				PointF pB = Project(xPos, 0f, sy * 0.5f);
				g.DrawLine(tilePen, pA, pB);
			}

			for (int ty = 0; ty <= terrain.TileCountY; ty++)
			{
				float zPos = -sy * 0.5f + ty * terrain.TileSizeY;
				if (zPos > sy * 0.5f + 1f) continue;
				PointF pA = Project(-sx * 0.5f, 0f, zPos);
				PointF pB = Project(sx * 0.5f, 0f, zPos);
				g.DrawLine(tilePen, pA, pB);
			}

			for (int ty = 0; ty < terrain.TileCountY; ty++)
			{
				for (int tx = 0; tx < terrain.TileCountX; tx++)
				{
					float cxTile = -sx * 0.5f + (tx + 0.5f) * terrain.TileSizeX;
					float czTile = -sy * 0.5f + (ty + 0.5f) * terrain.TileSizeY;
					if (cxTile > sx * 0.5f || czTile > sy * 0.5f) continue;
					PointF pCenter = Project(cxTile, terrain.EvaluateElevation(cxTile, czTile) + 5f, czTile);
					string label = string.Format("Tile [{0},{1}]\n{2:N0}m × {3:N0}m", tx + 1, ty + 1, terrain.TileSizeX, terrain.TileSizeY);
					g.DrawString(label, tileFont, tileBrush, pCenter.X - 35, pCenter.Y - 15);
				}
			}
		}

		// 5. Metric Axis Rulers
		using (Pen rulerPen = new Pen(Color.FromArgb(255, 200, 50), 2f))
		using (Font rulerFont = new Font("Segoe UI", 8f, FontStyle.Regular))
		using (SolidBrush rulerBrush = new SolidBrush(Color.FromArgb(255, 220, 100)))
		{
			int numTicksX = 5;
			for (int i = 0; i <= numTicksX; i++)
			{
				float frac = (float)i / numTicksX;
				float x = -sx * 0.5f + frac * sx;
				float z = sy * 0.5f;
				PointF pEdge = Project(x, 0f, z);
				PointF pTick = Project(x, -12f, z + sy * 0.02f);
				g.DrawLine(rulerPen, pEdge, pTick);
				float meterVal = frac * sx;
				string text = meterVal >= 1000f ? string.Format("{0:N1} km", meterVal / 1000f) : string.Format("{0:N0} m", meterVal);
				g.DrawString(text, rulerFont, rulerBrush, pTick.X - 18, pTick.Y + 2);
			}

			int numTicksZ = 3;
			for (int j = 0; j <= numTicksZ; j++)
			{
				float frac = (float)j / numTicksZ;
				float x = -sx * 0.5f;
				float z = -sy * 0.5f + frac * sy;
				PointF pEdge = Project(x, 0f, z);
				PointF pTick = Project(x - sx * 0.02f, -12f, z);
				g.DrawLine(rulerPen, pEdge, pTick);
				float meterVal = frac * sy;
				string text = meterVal >= 1000f ? string.Format("{0:N1} km", meterVal / 1000f) : string.Format("{0:N0} m", meterVal);
				g.DrawString(text, rulerFont, rulerBrush, pTick.X - 45, pTick.Y - 8);
			}
		}

		// 6. 3D Principal Axis Tripod Indicator (Bottom-Left)
		int axX = 65, axY = h - 65;
		PointF axCenter = new PointF(axX, axY);
		PointF pAxisX = new PointF(axX + cosYaw * 36f, axY - sinYaw * sinPitch * 36f);
		PointF pAxisZ = new PointF(axX - sinYaw * 36f, axY - cosYaw * sinPitch * 36f);
		PointF pAxisY = new PointF(axX, axY - cosPitch * 36f);

		using (Pen pX = new Pen(Color.FromArgb(240, 70, 70), 2.5f))
		using (Pen pY = new Pen(Color.FromArgb(70, 220, 70), 2.5f))
		using (Pen pZ = new Pen(Color.FromArgb(70, 130, 255), 2.5f))
		using (Font axFont = new Font("Segoe UI", 8f, FontStyle.Bold))
		using (SolidBrush bWhite = new SolidBrush(Color.White))
		{
			g.DrawLine(pX, axCenter, pAxisX);
			g.DrawLine(pY, axCenter, pAxisY);
			g.DrawLine(pZ, axCenter, pAxisZ);
			g.DrawString("X", axFont, bWhite, pAxisX.X + 2, pAxisX.Y - 8);
			g.DrawString("Y", axFont, bWhite, pAxisY.X - 4, pAxisY.Y - 14);
			g.DrawString("Z", axFont, bWhite, pAxisZ.X + 2, pAxisZ.Y - 8);
		}

		// 7. Glassmorphism Viewport HUD Overlay
		int hudW = Math.Min(640, w - 32);
		int hudH = 118;
		Rectangle hudRect = new Rectangle(16, 16, hudW, hudH);
		using (SolidBrush hudBg = new SolidBrush(Color.FromArgb(215, 24, 28, 34)))
		using (Pen hudBorder = new Pen(Color.FromArgb(60, 80, 100), 1.2f))
		{
			g.FillRectangle(hudBg, hudRect);
			g.DrawRectangle(hudBorder, hudRect);
		}

		using (Font titleFont = new Font("Segoe UI", 10.5f, FontStyle.Bold))
		using (Font textFont = new Font("Segoe UI", 8.5f, FontStyle.Regular))
		using (SolidBrush whiteBrush = new SolidBrush(Color.FromArgb(240, 245, 250)))
		using (SolidBrush cyanBrush = new SolidBrush(Color.FromArgb(0, 220, 255)))
		using (SolidBrush greenBrush = new SolidBrush(Color.FromArgb(120, 240, 140)))
		using (SolidBrush goldBrush = new SolidBrush(Color.FromArgb(255, 215, 80)))
		using (SolidBrush dimBrush = new SolidBrush(Color.FromArgb(160, 180, 200)))
		{
			g.DrawString("PixelActive CityScape 1.8 Viewport [Perspective 3D Terrain Mode]", titleFont, whiteBrush, 26, 22);
			g.DrawString(string.Format("Physical Extents: {0:N0}m × {1:N0}m | Physical Area: {2:F2} km² (Aspect: {3:F1}:1)",
				terrain.SizeX, terrain.SizeY, terrain.AreaSquareKilometers, terrain.AspectRatio), textFont, cyanBrush, 26, 43);
			g.DrawString(string.Format("Tile Grid: {0} × {1} (Tile Size: {2:N0}m × {3:N0}m) | Sampling: {4:F2}m ({5:N0} samples)",
				terrain.TileCountX, terrain.TileCountY, terrain.TileSizeX, terrain.TileSizeY, terrain.SamplingDistance, terrain.TotalSamplePoints), textFont, greenBrush, 26, 62);
			g.DrawString(string.Format("3D Engine: {0} [{1}] | CDLOD Quadtree: {2} chunks ({3} culled) | Camera: Yaw={4:F0}°, Pitch={5:F0}°",
				GraphicsBackendManager.ActiveBackendType, GraphicsBackendManager.CurrentArchitecture, cdlodChunks.Count, terrainChunksCulled, mCameraYaw, mCameraPitch), textFont, goldBrush, 26, 81);
			string bldStat = ActiveBuildings.Count > 0
				? string.Format("3D Buildings: {0:N0} vis / {1:N0} tot (Culled Nodes: {2}/{3})", renderedBuildings, ActiveBuildings.Count, nodesCulled, nodesTested)
				: "3D Buildings: 0";
			g.DrawString(string.Format("Camera Distance: {0:N0}m | {1} | Controls: LMB=Orbit | RMB/Shift=Pan", camDist, bldStat), textFont, dimBrush, 26, 100);
		}
	}

	[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	protected override void WndProc(ref Message m)
	{
		if (WidgetMgr.Instance != null)
		{
			int msg = m.Msg;
			if (msg == 15 && RenderWindow != null)
			{
				if (!ValidSent)
				{
					RenderWindow.SendIsValid(valid: true);
					ValidSent = true;
				}
				RenderWindow.WriteMessage(169u);
			}
		}
		base.WndProc(ref m);
	}

	protected override void OnHandleCreated(EventArgs e)
	{
		SendResize();
		base.OnHandleCreated(e);
		LastHandle = base.Handle;
		if (RenderWindow != null)
		{
			RenderWindow.SendIsValid(valid: true);
		}
	}

	protected override void OnHandleDestroyed(EventArgs e)
	{
		if (RenderWindow != null)
		{
			_ = RenderWindow.Hwnd;
		}
		if (WidgetMgr.Instance != null && !WidgetMgr.Instance.FormDisposing)
		{
			RenderWindow.SendIsValid(valid: false);
		}
		base.OnHandleDestroyed(e);
	}

	private void OnMouseGeneral(MouseEventArgs e, bool setFocus)
	{
		if (RenderWindow != null)
		{
			_ = RenderWindow.Hwnd;
			if (RenderWindow.IsMainRenderWindow && setFocus)
			{
				Focus();
			}
			RenderWindow.SendMouseInfo(CurrentMouseButtons, e.X, e.Y, e.Delta);
		}
	}

	protected override void OnMouseDown(MouseEventArgs e)
	{
		mLastMouseLocation = e.Location;
		if (TheRenderWindow == null)
		{
			if (e.Button == MouseButtons.Left && (ModifierKeys & Keys.Shift) == Keys.None)
			{
				mIsOrbiting = true;
			}
			else if (e.Button == MouseButtons.Middle || (e.Button == MouseButtons.Left && (ModifierKeys & Keys.Shift) != Keys.None) || e.Button == MouseButtons.Right)
			{
				mIsPanning = true;
			}
		}

		if (CurrentMouseButtons != Control.MouseButtons)
		{
			CurrentMouseButtons = Control.MouseButtons;
			OnMouseGeneral(e, setFocus: true);
		}
		base.OnMouseDown(e);
	}

	protected override void OnMouseUp(MouseEventArgs e)
	{
		if (TheRenderWindow == null)
		{
			mIsOrbiting = false;
			mIsPanning = false;
		}

		if (CurrentMouseButtons != Control.MouseButtons)
		{
			CurrentMouseButtons = Control.MouseButtons;
			OnMouseGeneral(e, setFocus: false);
		}
		base.OnMouseUp(e);
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		if (TheRenderWindow == null && (mIsOrbiting || mIsPanning))
		{
			int dx = e.X - mLastMouseLocation.X;
			int dy = e.Y - mLastMouseLocation.Y;
			mLastMouseLocation = e.Location;

			if (mIsOrbiting)
			{
				mCameraYaw += dx * 0.45f;
				mCameraPitch = Math.Max(4.0f, Math.Min(88.0f, mCameraPitch + dy * 0.45f));
				Invalidate();
			}
			else if (mIsPanning)
			{
				mCameraPanX += dx * 1.0f;
				mCameraPanY += dy * 1.0f;
				Invalidate();
			}
		}

		if (LastX != e.X || LastY != e.Y)
		{
			if (Control.MouseButtons == MouseButtons.None && CurrentMouseButtons != MouseButtons.None)
			{
				CurrentMouseButtons = MouseButtons.None;
			}
			OnMouseGeneral(e, setFocus: true);
			base.OnMouseMove(e);
			LastX = e.X;
			LastY = e.Y;
		}
	}

	protected override void OnMouseLeave(EventArgs e)
	{
		if (TheRenderWindow == null)
		{
			mIsOrbiting = false;
			mIsPanning = false;
		}
		MouseEventArgs e2 = new MouseEventArgs(Control.MouseButtons, 0, LastX, LastY, 0);
		if (Control.MouseButtons == MouseButtons.None && CurrentMouseButtons != MouseButtons.None)
		{
			CurrentMouseButtons = MouseButtons.None;
		}
		OnMouseGeneral(e2, setFocus: false);
		base.OnMouseLeave(e);
	}

	protected override void OnMouseWheel(MouseEventArgs e)
	{
		if (TheRenderWindow == null)
		{
			float effDist = mCameraDistance > 0f ? mCameraDistance : (float)Math.Sqrt(ActiveTerrain.SizeX * ActiveTerrain.SizeX + ActiveTerrain.SizeY * ActiveTerrain.SizeY) * 1.30f;
			if (e.Delta > 0)
			{
				effDist *= 0.85f;
			}
			else
			{
				effDist *= 1.18f;
			}
			mCameraDistance = Math.Max(10.0f, effDist);
			Invalidate();
		}
		OnMouseGeneral(e, setFocus: true);
		base.OnMouseWheel(e);
	}

	private void ControlRender_Resize(object sender, EventArgs e)
	{
		if ((WidgetMgr.Instance == null || !WidgetMgr.Instance.FormDisposing) && (LastHandle != base.Handle || LastXRes != base.Width || LastYRes != base.Height))
		{
			SendResize();
		}
	}

	protected override void OnKeyDown(KeyEventArgs e)
	{
		Focus();
		WidgetMgr.Instance.SendKeyInfo(e.KeyData & Keys.KeyCode, WidgetBase.CoreMessages.eKeyPressed);
		base.OnKeyDown(e);
	}

	protected override void OnKeyUp(KeyEventArgs e)
	{
		WidgetMgr.Instance.SendKeyInfo(e.KeyData & Keys.KeyCode, WidgetBase.CoreMessages.eKeyReleased);
		base.OnKeyUp(e);
	}

	protected override void OnEnter(EventArgs e)
	{
		if (RenderWindow != null)
		{
			RenderWindow.WriteMessage(172u);
			Keys[] array = new Keys[3]
			{
				Keys.Control,
				Keys.Alt,
				Keys.Shift
			};
			Keys[] array2 = array;
			foreach (Keys keys in array2)
			{
				if ((Control.ModifierKeys & keys) != Keys.None)
				{
					WidgetMgr.Instance.SendKeyInfo(keys, WidgetBase.CoreMessages.eKeyDown);
				}
			}
		}
		base.OnEnter(e);
	}

	protected override void OnLeave(EventArgs e)
	{
		if (RenderWindow != null && !WidgetMgr.Instance.FormDisposing)
		{
			RenderWindow.WriteMessage(173u);
		}
		base.OnLeave(e);
	}

	protected override bool IsInputKey(Keys keyData)
	{
		if (keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Tab)
		{
			return true;
		}
		return base.IsInputKey(keyData);
	}

	private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
	{
		if (TheRenderWindow != null)
		{
			TheRenderWindow.SendSliderValueChanged(e.ScrollOrientation == ScrollOrientation.HorizontalScroll, e.NewValue);
		}
	}

	private void ScrollBar_ValueChanged(object sender, EventArgs e)
	{
		if (TheRenderWindow != null)
		{
			if (sender == hScrollBar1)
			{
				TheRenderWindow.SendSliderValueChanged(isHorizontal: true, hScrollBar1.Value);
			}
			else
			{
				TheRenderWindow.SendSliderValueChanged(isHorizontal: false, vScrollBar1.Value);
			}
		}
	}

	protected override void DragDropHelper(bool dragFinished, DragEventArgs e)
	{
		Point point = PointToClient(Cursor.Position);
		RenderWindow.SendMouseInfo(Control.MouseButtons, point.X, point.Y, 0);
		DragDropSendMessage(RenderWindow, e, dragFinished, sendCheck: true, null);
	}

	protected override void OnDragOver(DragEventArgs drgevent)
	{
		base.OnDragOver(drgevent);
		HandleDragOver(checkHover: false, drgevent);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			ControlRenders.Remove(this);
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.progressBar1 = new System.Windows.Forms.ProgressBar();
		this.labelProgressBar = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
		this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.progressBar1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.progressBar1.Location = new System.Drawing.Point(15, 38);
		this.progressBar1.MaximumSize = new System.Drawing.Size(0, 20);
		this.progressBar1.MinimumSize = new System.Drawing.Size(0, 20);
		this.progressBar1.Name = "progressBar1";
		this.progressBar1.Size = new System.Drawing.Size(760, 20);
		this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
		this.progressBar1.TabIndex = 0;
		this.labelProgressBar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.labelProgressBar.Location = new System.Drawing.Point(15, 19);
		this.labelProgressBar.Name = "labelProgressBar";
		this.labelProgressBar.Size = new System.Drawing.Size(733, 16);
		this.labelProgressBar.TabIndex = 1;
		this.labelProgressBar.Text = "label1";
		this.panel1.AllowDrop = true;
		this.panel1.Controls.Add(this.labelProgressBar);
		this.panel1.Controls.Add(this.progressBar1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(792, 170);
		this.panel1.TabIndex = 2;
		this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.hScrollBar1.LargeChange = 1;
		this.hScrollBar1.Location = new System.Drawing.Point(0, 170);
		this.hScrollBar1.Maximum = 0;
		this.hScrollBar1.Name = "hScrollBar1";
		this.hScrollBar1.Size = new System.Drawing.Size(792, 17);
		this.hScrollBar1.TabIndex = 3;
		this.hScrollBar1.Visible = false;
		this.hScrollBar1.ValueChanged += new System.EventHandler(ScrollBar_ValueChanged);
		this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(ScrollBar_Scroll);
		this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
		this.vScrollBar1.LargeChange = 1;
		this.vScrollBar1.Location = new System.Drawing.Point(792, 0);
		this.vScrollBar1.Maximum = 0;
		this.vScrollBar1.Name = "vScrollBar1";
		this.vScrollBar1.Size = new System.Drawing.Size(17, 187);
		this.vScrollBar1.TabIndex = 2;
		this.vScrollBar1.Visible = false;
		this.vScrollBar1.ValueChanged += new System.EventHandler(ScrollBar_ValueChanged);
		this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(ScrollBar_Scroll);
		this.AllowDrop = true;
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.hScrollBar1);
		base.Controls.Add(this.vScrollBar1);
		base.Name = "ControlRender";
		base.Size = new System.Drawing.Size(809, 187);
		base.Resize += new System.EventHandler(ControlRender_Resize);
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
