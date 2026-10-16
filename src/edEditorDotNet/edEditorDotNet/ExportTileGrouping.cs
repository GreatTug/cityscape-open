using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

public class ExportTileGrouping : UserControl, IDialogContainer
{
	private const string m_AllString = "Single file per tile (ex. tile001.<ext>)";

	private const string m_SeparateString = "Export LODs individually (ex. tile001_LOD0.<ext>)";

	private string m_Extension;

	private IContainer components;

	private GroupBox tileGroupingPerFile;

	private DialogRadioButton exportLodsIndividually;

	private DialogRadioButton exportSingleFile;

	private DialogRadioButton ProxyLOD;

	private DialogCheckBox PropSeparateLOD;

	[Browsable(true)]
	public string Extension
	{
		get
		{
			return m_Extension;
		}
		set
		{
			m_Extension = value;
			exportSingleFile.Text = "Single file per tile (ex. tile001.<ext>)".Replace("<ext>", m_Extension);
			exportLodsIndividually.Text = "Export LODs individually (ex. tile001_LOD0.<ext>)".Replace("<ext>", m_Extension);
		}
	}

	public RadioButton ProxyLodRadioButton => ProxyLOD;

	public RadioButton ExportSingleFileRadioButton => exportSingleFile;

	public RadioButton ExportLodsIndividuallyRadioButton => exportLodsIndividually;

	public CheckBox ExportLodsIndividuallyApplyToPropsCheckBox => PropSeparateLOD;

	public ExportTileGrouping()
	{
		InitializeComponent();
		exportSingleFile.Text = "Single file per tile (ex. tile001.<ext>)".Replace("<ext>", m_Extension);
		exportLodsIndividually.Text = "Export LODs individually (ex. tile001_LOD0.<ext>)".Replace("<ext>", m_Extension);
		exportLodsIndividually.CheckedChanged += exportLodsIndividually_CheckedChanged;
		PropSeparateLOD.Enabled = exportLodsIndividually.Checked;
	}

	private void exportLodsIndividually_CheckedChanged(object sender, EventArgs e)
	{
		PropSeparateLOD.Enabled = exportLodsIndividually.Checked;
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
		this.tileGroupingPerFile = new System.Windows.Forms.GroupBox();
		this.PropSeparateLOD = new palDotNet.palDialog.DialogCheckBox();
		this.ProxyLOD = new palDotNet.palDialog.DialogRadioButton();
		this.exportLodsIndividually = new palDotNet.palDialog.DialogRadioButton();
		this.exportSingleFile = new palDotNet.palDialog.DialogRadioButton();
		this.tileGroupingPerFile.SuspendLayout();
		base.SuspendLayout();
		this.tileGroupingPerFile.Controls.Add(this.PropSeparateLOD);
		this.tileGroupingPerFile.Controls.Add(this.ProxyLOD);
		this.tileGroupingPerFile.Controls.Add(this.exportLodsIndividually);
		this.tileGroupingPerFile.Controls.Add(this.exportSingleFile);
		this.tileGroupingPerFile.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tileGroupingPerFile.Location = new System.Drawing.Point(0, 0);
		this.tileGroupingPerFile.Name = "tileGroupingPerFile";
		this.tileGroupingPerFile.Size = new System.Drawing.Size(506, 153);
		this.tileGroupingPerFile.TabIndex = 19;
		this.tileGroupingPerFile.TabStop = false;
		this.tileGroupingPerFile.Text = "LOD grouping";
		this.PropSeparateLOD.AutoSize = true;
		this.PropSeparateLOD.Location = new System.Drawing.Point(26, 65);
		this.PropSeparateLOD.Name = "PropSeparateLOD";
		this.PropSeparateLOD.SaveToConfig = true;
		this.PropSeparateLOD.Size = new System.Drawing.Size(93, 17);
		this.PropSeparateLOD.TabIndex = 2;
		this.PropSeparateLOD.Text = "Apply to props";
		this.PropSeparateLOD.UseVisualStyleBackColor = true;
		this.ProxyLOD.AutoSize = true;
		this.ProxyLOD.Location = new System.Drawing.Point(7, 88);
		this.ProxyLOD.Name = "ProxyLOD";
		this.ProxyLOD.SaveToConfig = true;
		this.ProxyLOD.Size = new System.Drawing.Size(153, 17);
		this.ProxyLOD.TabIndex = 3;
		this.ProxyLOD.TabStop = true;
		this.ProxyLOD.Text = "Proxy LOD (single LOD file)";
		this.ProxyLOD.UseVisualStyleBackColor = true;
		this.exportLodsIndividually.AutoSize = true;
		this.exportLodsIndividually.Location = new System.Drawing.Point(7, 42);
		this.exportLodsIndividually.Name = "exportLodsIndividually";
		this.exportLodsIndividually.SaveToConfig = true;
		this.exportLodsIndividually.Size = new System.Drawing.Size(251, 17);
		this.exportLodsIndividually.TabIndex = 1;
		this.exportLodsIndividually.TabStop = true;
		this.exportLodsIndividually.Text = "Export LODs individually (ex. tile001_LOD0.dae)";
		this.exportLodsIndividually.UseVisualStyleBackColor = true;
		this.exportSingleFile.AutoSize = true;
		this.exportSingleFile.Checked = true;
		this.exportSingleFile.Location = new System.Drawing.Point(7, 19);
		this.exportSingleFile.Name = "exportSingleFile";
		this.exportSingleFile.SaveToConfig = true;
		this.exportSingleFile.Size = new System.Drawing.Size(182, 17);
		this.exportSingleFile.TabIndex = 0;
		this.exportSingleFile.TabStop = true;
		this.exportSingleFile.Text = "Single file per tile (ex. tile001.dae)";
		this.exportSingleFile.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.tileGroupingPerFile);
		base.Name = "ExportTileGrouping";
		base.Size = new System.Drawing.Size(506, 153);
		this.tileGroupingPerFile.ResumeLayout(false);
		this.tileGroupingPerFile.PerformLayout();
		base.ResumeLayout(false);
	}
}
