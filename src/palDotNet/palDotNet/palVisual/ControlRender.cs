using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Permissions;
using System.Windows.Forms;
using palDotNet.palWidgets;

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
				Color.FromArgb(42, 48, 56),
				Color.FromArgb(24, 28, 34),
				LinearGradientMode.Vertical))
			{
				g.FillRectangle(bgBrush, ClientRectangle);
			}

			int cx = w / 2;
			int cy = h / 2 + 30;

			// Draw 3D perspective ground grid
			using (Pen gridPen = new Pen(Color.FromArgb(50, 60, 72), 1f))
			using (Pen axisPenX = new Pen(Color.FromArgb(180, 70, 70), 1.5f))
			using (Pen axisPenZ = new Pen(Color.FromArgb(70, 140, 70), 1.5f))
			{
				// Radial perspective grid lines
				for (int i = -14; i <= 14; i++)
				{
					int xTop = cx + i * 32;
					int xBottom = cx + i * 85;
					g.DrawLine(gridPen, xTop, cy - 110, xBottom, h);
				}
				// Horizontal depth rings
				for (int y = cy - 90; y < h; y += 26)
				{
					int spread = (int)((y - (cy - 110)) * 3.2);
					g.DrawLine(gridPen, Math.Max(0, cx - spread), y, Math.Min(w, cx + spread), y);
				}
				// Principal axes
				g.DrawLine(axisPenX, cx - 350, cy, cx + 350, cy);
				g.DrawLine(axisPenZ, cx, cy - 110, cx, h);
			}

			// Viewport HUD Overlay
			using (Font titleFont = new Font("Segoe UI", 11f, FontStyle.Bold))
			using (Font subFont = new Font("Segoe UI", 8.5f, FontStyle.Regular))
			using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(220, 225, 230)))
			using (SolidBrush dimBrush = new SolidBrush(Color.FromArgb(130, 145, 160)))
			using (SolidBrush accentBrush = new SolidBrush(Color.FromArgb(90, 190, 255)))
			{
				g.DrawString("CityScape 1.8 Viewport [Perspective]", titleFont, textBrush, 16, 16);
				g.DrawString("Resolution: " + w + " x " + h + " | Camera: Orbit Mode", subFont, dimBrush, 16, 36);
				g.DrawString("[Ready - Open World (.csp) or Terrain (.cst) from File menu]", subFont, accentBrush, 16, 54);
			}
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
