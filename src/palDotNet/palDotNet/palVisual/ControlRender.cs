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

	// Data models maintained for subsystem compatibility
	public static TerrainSurfaceData ActiveTerrain { get; set; } = TerrainSurfaceData.CreateDefault();
	public static List<GisBuildingInstance> ActiveBuildings { get; } = new List<GisBuildingInstance>();
	public static SpatialQuadtree<GisBuildingInstance> BuildingSpatialIndex { get; private set; }
	public static CdlodTerrainQuadtree TerrainCdlodTree { get; private set; }
	public static CdlodAsyncMeshWorker AsyncMeshWorker { get; } = new CdlodAsyncMeshWorker();

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

	public static void SetActiveTerrain(TerrainSurfaceData terrain, bool resetCamera = false)
	{
		if (terrain == null) return;
		ActiveTerrain = terrain;
		TerrainCdlodTree = new CdlodTerrainQuadtree(terrain, 4);
		AsyncMeshWorker.ClearCache();
		foreach (ControlRender cr in ControlRenders)
		{
			cr.Invalidate();
		}
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
		if (CurrentMouseButtons != Control.MouseButtons)
		{
			CurrentMouseButtons = Control.MouseButtons;
			OnMouseGeneral(e, setFocus: true);
		}
		base.OnMouseDown(e);
	}

	protected override void OnMouseUp(MouseEventArgs e)
	{
		if (CurrentMouseButtons != Control.MouseButtons)
		{
			CurrentMouseButtons = Control.MouseButtons;
			OnMouseGeneral(e, setFocus: false);
		}
		base.OnMouseUp(e);
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
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
		if (WidgetMgr.Instance != null)
		{
			WidgetMgr.Instance.SendKeyInfo(e.KeyData & Keys.KeyCode, WidgetBase.CoreMessages.eKeyPressed);
		}
		base.OnKeyDown(e);
	}

	public void ResetCamera()
	{
		// Camera reset handled via RenderWindow / WidgetMgr in native engine
	}

	public void FitCameraToExtents()
	{
		// Camera extents framing handled via RenderWindow / WidgetMgr in native engine
	}

	protected override void OnKeyUp(KeyEventArgs e)
	{
		if (WidgetMgr.Instance != null)
		{
			WidgetMgr.Instance.SendKeyInfo(e.KeyData & Keys.KeyCode, WidgetBase.CoreMessages.eKeyReleased);
		}
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
