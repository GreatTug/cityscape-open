using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

public class ExportNavteq : ExportCustomBase
{
	private IContainer components;

	private GroupBox navteqGroup;

	private DialogPathLoader auxiliaryMetaDataPath;

	private DialogLabel navteqLabel;

	public override string ExporterTypeName => "NAVTEQ";

	public ExportNavteq()
	{
		InitializeComponent();
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
		this.navteqGroup = new System.Windows.Forms.GroupBox();
		this.navteqLabel = new palDotNet.palDialog.DialogLabel();
		this.auxiliaryMetaDataPath = new palDotNet.palDialog.DialogPathLoader();
		this.navteqGroup.SuspendLayout();
		base.SuspendLayout();
		this.navteqGroup.Controls.Add(this.navteqLabel);
		this.navteqGroup.Controls.Add(this.auxiliaryMetaDataPath);
		this.navteqGroup.Location = new System.Drawing.Point(4, 3);
		this.navteqGroup.Name = "navteqGroup";
		this.navteqGroup.Size = new System.Drawing.Size(492, 70);
		this.navteqGroup.TabIndex = 30;
		this.navteqGroup.TabStop = false;
		this.navteqGroup.Text = "Navteq options";
		this.navteqLabel.AutoSize = true;
		this.navteqLabel.Location = new System.Drawing.Point(6, 20);
		this.navteqLabel.Name = "navteqLabel";
		this.navteqLabel.Size = new System.Drawing.Size(111, 13);
		this.navteqLabel.TabIndex = 1;
		this.navteqLabel.Text = "Navteq meta data file:";
		this.auxiliaryMetaDataPath.BackColor = System.Drawing.Color.Transparent;
		this.auxiliaryMetaDataPath.BrowseType = palDotNet.palDialog.DialogPathLoader.EnumBrowseType.eOpen;
		this.auxiliaryMetaDataPath.CheckFileExists = false;
		this.auxiliaryMetaDataPath.Description = null;
		this.auxiliaryMetaDataPath.Filter = null;
		this.auxiliaryMetaDataPath.Location = new System.Drawing.Point(6, 40);
		this.auxiliaryMetaDataPath.Name = "auxiliaryMetaDataPath";
		this.auxiliaryMetaDataPath.Path = "";
		this.auxiliaryMetaDataPath.Size = new System.Drawing.Size(480, 24);
		this.auxiliaryMetaDataPath.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.navteqGroup);
		base.Name = "ExportNavteq";
		this.navteqGroup.ResumeLayout(false);
		this.navteqGroup.PerformLayout();
		base.ResumeLayout(false);
	}
}
