using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palWidgets;

namespace palDotNet.palVisual;

public class ViewOutput : ViewTool
{
	private IContainer components;

	private RichTextBox RichTextBox;

	private Timer Timer;

	private SplitContainer splitContainer1;

	private CommandBar commandBar1;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem copyToolStripMenuItem;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem clearAllToolStripMenuItem;

	private ToolStripMenuItem selectAllToolStripMenuItem;

	private ViewOutput()
	{
		InitializeComponent();
		Timer.Start();
	}

	public ViewOutput(Guid guid)
	{
		InitializeComponent();
		TheGuid = guid;
		PalDockContent.DockContents[guid] = this;
		Timer.Start();
	}

	private void Timer_Tick(object sender, EventArgs e)
	{
		Output.Read(out var lines, out var modes);
		if (lines == null)
		{
			return;
		}
		RichTextBox.SuspendLayout();
		for (int i = 0; i < lines.Count; i++)
		{
			RichTextBox.AppendText(lines[i]);
			bool flag = modes[i] == Output.EnumModes.eError;
			bool flag2 = modes[i] == Output.EnumModes.eWarning;
			if (flag || flag2)
			{
				RichTextBox.SelectionStart = RichTextBox.TextLength - lines[i].Length;
				RichTextBox.SelectionLength = lines[i].Length;
				RichTextBox.SelectionColor = (flag ? Color.Red : Color.Yellow);
				RichTextBox.SelectionStart = RichTextBox.TextLength;
			}
			else if (modes[i] == Output.EnumModes.eClearScreen)
			{
				RichTextBox.Clear();
			}
		}
		RichTextBox.Select(RichTextBox.TextLength, 0);
		RichTextBox.ResumeLayout();
	}

	public void WriteLine(string line, Color color)
	{
		RichTextBox.SuspendLayout();
		RichTextBox.AppendText(line + Environment.NewLine);
		RichTextBox.SelectionStart = RichTextBox.TextLength - (line.Length + 1);
		RichTextBox.SelectionLength = line.Length;
		RichTextBox.SelectionColor = color;
		RichTextBox.SelectionStart = RichTextBox.TextLength;
		RichTextBox.Select(RichTextBox.TextLength, 0);
		RichTextBox.ResumeLayout();
	}

	public override void LoadComponentSettings()
	{
		commandBar1.LoadComponentSettings();
	}

	private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
	{
		RichTextBox.Clear();
	}

	private void copyToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!string.IsNullOrEmpty(RichTextBox.SelectedText))
		{
			Clipboard.SetText(RichTextBox.SelectedText);
		}
	}

	private void RichTextBox_MouseDown(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Right)
		{
			Point point = RichTextBox.PointToScreen(e.Location);
			contextMenuStrip1.Show(point.X, point.Y);
		}
	}

	private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
	{
		RichTextBox.SelectAll();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(palDotNet.palVisual.ViewOutput));
		this.RichTextBox = new System.Windows.Forms.RichTextBox();
		this.Timer = new System.Windows.Forms.Timer(this.components);
		this.commandBar1 = new palDotNet.palVisual.CommandBar();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		this.contextMenuStrip1.SuspendLayout();
		base.SuspendLayout();
		this.RichTextBox.AutoWordSelection = true;
		this.RichTextBox.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.RichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.RichTextBox.Font = new System.Drawing.Font("Courier New", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.RichTextBox.ForeColor = System.Drawing.Color.White;
		this.RichTextBox.HideSelection = false;
		this.RichTextBox.Location = new System.Drawing.Point(0, 0);
		this.RichTextBox.Name = "RichTextBox";
		this.RichTextBox.ReadOnly = true;
		this.RichTextBox.Size = new System.Drawing.Size(731, 281);
		this.RichTextBox.TabIndex = 0;
		this.RichTextBox.Text = "";
		this.RichTextBox.WordWrap = false;
		this.RichTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(RichTextBox_MouseDown);
		this.Timer.Interval = 33;
		this.Timer.Tick += new System.EventHandler(Timer_Tick);
		this.commandBar1.BackColor = System.Drawing.SystemColors.Control;
		this.commandBar1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.commandBar1.ForeColor = System.Drawing.SystemColors.ControlText;
		this.commandBar1.Location = new System.Drawing.Point(0, 0);
		this.commandBar1.Name = "commandBar1";
		this.commandBar1.Size = new System.Drawing.Size(731, 26);
		this.commandBar1.TabIndex = 0;
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
		this.splitContainer1.IsSplitterFixed = true;
		this.splitContainer1.Location = new System.Drawing.Point(0, 0);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.splitContainer1.Panel1.Controls.Add(this.RichTextBox);
		this.splitContainer1.Panel2.Controls.Add(this.commandBar1);
		this.splitContainer1.Size = new System.Drawing.Size(731, 311);
		this.splitContainer1.SplitterDistance = 281;
		this.splitContainer1.TabIndex = 1;
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.copyToolStripMenuItem, this.toolStripSeparator1, this.selectAllToolStripMenuItem, this.clearAllToolStripMenuItem });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(123, 76);
		this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
		this.copyToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
		this.copyToolStripMenuItem.Text = "Cop&y";
		this.copyToolStripMenuItem.Click += new System.EventHandler(copyToolStripMenuItem_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(119, 6);
		this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
		this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
		this.selectAllToolStripMenuItem.Text = "&Select All";
		this.selectAllToolStripMenuItem.Click += new System.EventHandler(selectAllToolStripMenuItem_Click);
		this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
		this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
		this.clearAllToolStripMenuItem.Text = "Clear Al&l";
		this.clearAllToolStripMenuItem.Click += new System.EventHandler(clearAllToolStripMenuItem_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(731, 311);
		base.Controls.Add(this.splitContainer1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "ViewOutput";
		base.TabText = "Output";
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.ResumeLayout(false);
		this.contextMenuStrip1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
