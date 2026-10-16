using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.Properties;
using palDotNet.palWidgets;

namespace palDotNet.palVisual;

public class CommandBar : UserControl
{
	public delegate void WriteLineDel(string line, Color color);

	private const string SettingsKey = "87B275C3-5F69-4e5d-86B2-1011D8C4B3F5";

	private static uint NumCommandBars;

	private palDotNet.palWidgets.CommandBar WidgetCommandBar;

	public static WriteLineDel WriteLineDelegate;

	private CommandBarSettings TheConfigSettings;

	private MenuRecentPathList RecentPathList;

	private IContainer components;

	private System.Windows.Forms.Label label1;

	private ToolStripContainer toolStripContainer1;

	private System.Windows.Forms.ToolStrip toolStrip1;

	private ToolStripSplitButton toolStripSplitButtonLoadScript;

	private ToolStripLabel toolStripLabel1;

	private ToolStripSpringTextBox toolStripTextBox1;

	private OpenFileDialog openFileDialogLoadScript;

	public CommandBar()
	{
		InitializeComponent();
		palDotNet.palWidgets.CommandBar.EventCreateCommandBar += CreateCommandBar;
		RecentPathList = new MenuRecentPathList(20u, "87B275C3-5F69-4e5d-86B2-1011D8C4B3F5", toolStripSplitButtonLoadScript.DropDownItems, HandleSplitLoadScript);
	}

	private void CreateCommandBar(object sender, EventArgs e)
	{
		if (NumCommandBars++ != 0)
		{
			throw new NotSupportedException("Only 1 command bar supported");
		}
		WidgetCommandBar = sender as palDotNet.palWidgets.CommandBar;
		WidgetCommandBar.ShowHideDelegate = delegate(bool show)
		{
			SplitterPanel splitterPanel = base.Parent as SplitterPanel;
			bool flag = false;
			if (splitterPanel != null)
			{
				SplitContainer splitContainer = splitterPanel.Parent as SplitContainer;
				if (splitContainer.Panel1 == splitterPanel)
				{
					splitContainer.Panel1Collapsed = !show;
				}
				else if (splitContainer.Panel2 == splitterPanel)
				{
					splitContainer.Panel2Collapsed = !show;
				}
				else
				{
					flag = true;
				}
			}
			else
			{
				flag = true;
			}
		};
	}

	private void textBox1_KeyDown(object sender, KeyEventArgs e)
	{
		Keys keyCode = e.KeyCode;
		if (keyCode == Keys.Return)
		{
			SendCommand(toolStripTextBox1.Text);
			toolStripTextBox1.Text = string.Empty;
		}
	}

	private void toolStripSplitButtonLoadScript_Click(object sender, EventArgs e)
	{
		DialogResult dialogResult = openFileDialogLoadScript.ShowDialog();
		if (dialogResult == DialogResult.OK)
		{
			HandleSplitLoadScript(openFileDialogLoadScript.FileName);
			RecentPathList.AddFileToList(openFileDialogLoadScript.FileName);
		}
	}

	private void HandleSplitLoadScript(string path)
	{
		string command = "runcommandscript \"" + path + "\"";
		SendCommand(command);
	}

	private void SendCommand(string command)
	{
		if (WriteLineDelegate != null)
		{
			WriteLineDelegate("> " + command, Color.LightBlue);
		}
		WidgetCommandBar.SendCommand(command);
		string value = command.Trim();
		if (string.IsNullOrEmpty(value))
		{
			return;
		}
		int count = toolStripTextBox1.AutoCompleteCustomSource.Count;
		if (count >= 20)
		{
			for (int i = 0; i < count - 20 + 1; i++)
			{
				toolStripTextBox1.AutoCompleteCustomSource.RemoveAt(0);
			}
		}
		toolStripTextBox1.AutoCompleteCustomSource.Add(value);
	}

	public void SaveComponentSettings()
	{
		if (TheConfigSettings == null)
		{
			TheConfigSettings = new CommandBarSettings();
		}
		TheConfigSettings.AutoCompleteItems = new string[toolStripTextBox1.AutoCompleteCustomSource.Count];
		int num = 0;
		foreach (string item in toolStripTextBox1.AutoCompleteCustomSource)
		{
			TheConfigSettings.AutoCompleteItems[num++] = item;
		}
		TheConfigSettings.Save();
	}

	public void ResetComponentSettings()
	{
		if (TheConfigSettings == null)
		{
			TheConfigSettings = new CommandBarSettings();
		}
		TheConfigSettings.Reload();
	}

	public void LoadComponentSettings()
	{
		if (TheConfigSettings == null)
		{
			TheConfigSettings = new CommandBarSettings();
		}
		if (TheConfigSettings.AutoCompleteItems != null)
		{
			toolStripTextBox1.AutoCompleteCustomSource.AddRange(TheConfigSettings.AutoCompleteItems);
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			SaveComponentSettings();
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.label1 = new System.Windows.Forms.Label();
		this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
		this.toolStrip1 = new System.Windows.Forms.ToolStrip();
		this.toolStripSplitButtonLoadScript = new System.Windows.Forms.ToolStripSplitButton();
		this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
		this.toolStripTextBox1 = new palDotNet.palVisual.ToolStripSpringTextBox();
		this.openFileDialogLoadScript = new System.Windows.Forms.OpenFileDialog();
		this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
		this.toolStripContainer1.SuspendLayout();
		this.toolStrip1.SuspendLayout();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(-98, 10);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(56, 13);
		this.label1.TabIndex = 3;
		this.label1.Text = "command:";
		this.toolStripContainer1.BottomToolStripPanelVisible = false;
		this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(548, 3);
		this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.toolStripContainer1.LeftToolStripPanelVisible = false;
		this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
		this.toolStripContainer1.Name = "toolStripContainer1";
		this.toolStripContainer1.RightToolStripPanelVisible = false;
		this.toolStripContainer1.Size = new System.Drawing.Size(548, 26);
		this.toolStripContainer1.TabIndex = 6;
		this.toolStripContainer1.Text = "toolStripContainer1";
		this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
		this.toolStrip1.AllowMerge = false;
		this.toolStrip1.CanOverflow = false;
		this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
		this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
		this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.toolStripSplitButtonLoadScript, this.toolStripLabel1, this.toolStripTextBox1 });
		this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
		this.toolStrip1.Location = new System.Drawing.Point(0, 0);
		this.toolStrip1.Name = "toolStrip1";
		this.toolStrip1.ShowItemToolTips = false;
		this.toolStrip1.Size = new System.Drawing.Size(548, 23);
		this.toolStrip1.Stretch = true;
		this.toolStrip1.TabIndex = 0;
		this.toolStripSplitButtonLoadScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		this.toolStripSplitButtonLoadScript.Image = palDotNet.Properties.Resources.Open16;
		this.toolStripSplitButtonLoadScript.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripSplitButtonLoadScript.Name = "toolStripSplitButtonLoadScript";
		this.toolStripSplitButtonLoadScript.Size = new System.Drawing.Size(32, 20);
		this.toolStripSplitButtonLoadScript.Text = "toolStripSplitButton1";
		this.toolStripSplitButtonLoadScript.ToolTipText = "Load command script";
		this.toolStripSplitButtonLoadScript.ButtonClick += new System.EventHandler(toolStripSplitButtonLoadScript_Click);
		this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
		this.toolStripLabel1.AutoSize = false;
		this.toolStripLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.toolStripLabel1.Name = "toolStripLabel1";
		this.toolStripLabel1.Size = new System.Drawing.Size(67, 20);
		this.toolStripLabel1.Text = "Command:";
		this.toolStripTextBox1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
		this.toolStripTextBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.toolStripTextBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
		this.toolStripTextBox1.Name = "toolStripTextBox1";
		this.toolStripTextBox1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
		this.toolStripTextBox1.Size = new System.Drawing.Size(415, 23);
		this.toolStripTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox1_KeyDown);
		this.openFileDialogLoadScript.DefaultExt = "csscript.txt";
		this.openFileDialogLoadScript.Filter = "CityScape Script Files|*.csscript.txt|All Files|*.*";
		this.openFileDialogLoadScript.SupportMultiDottedExtensions = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.toolStripContainer1);
		base.Controls.Add(this.label1);
		base.Name = "CommandBar";
		base.Size = new System.Drawing.Size(548, 26);
		this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
		this.toolStripContainer1.TopToolStripPanel.PerformLayout();
		this.toolStripContainer1.ResumeLayout(false);
		this.toolStripContainer1.PerformLayout();
		this.toolStrip1.ResumeLayout(false);
		this.toolStrip1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
