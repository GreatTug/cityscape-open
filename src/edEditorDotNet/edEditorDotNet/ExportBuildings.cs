using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

public class ExportBuildings : UserControl, IDialogContainer
{
	private IContainer components;

	private GroupBox buildingOptions;

	private DialogCheckBox SplitBuildingsOnTileBounds;

	public DialogCheckBox SplitBuildingsOnTileBoundsCheckBox => SplitBuildingsOnTileBounds;

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
		this.buildingOptions = new System.Windows.Forms.GroupBox();
		this.SplitBuildingsOnTileBounds = new palDotNet.palDialog.DialogCheckBox();
		this.buildingOptions.SuspendLayout();
		base.SuspendLayout();
		this.buildingOptions.Controls.Add(this.SplitBuildingsOnTileBounds);
		this.buildingOptions.Dock = System.Windows.Forms.DockStyle.Fill;
		this.buildingOptions.Location = new System.Drawing.Point(0, 0);
		this.buildingOptions.Name = "buildingOptions";
		this.buildingOptions.Size = new System.Drawing.Size(360, 200);
		this.buildingOptions.TabIndex = 22;
		this.buildingOptions.TabStop = false;
		this.buildingOptions.Text = "Building options";
		this.SplitBuildingsOnTileBounds.AutoSize = true;
		this.SplitBuildingsOnTileBounds.Checked = true;
		this.SplitBuildingsOnTileBounds.CheckState = System.Windows.Forms.CheckState.Checked;
		this.SplitBuildingsOnTileBounds.Location = new System.Drawing.Point(6, 19);
		this.SplitBuildingsOnTileBounds.Name = "SplitBuildingsOnTileBounds";
		this.SplitBuildingsOnTileBounds.SaveToConfig = true;
		this.SplitBuildingsOnTileBounds.Size = new System.Drawing.Size(115, 17);
		this.SplitBuildingsOnTileBounds.TabIndex = 9;
		this.SplitBuildingsOnTileBounds.Text = "Split on tile bounds";
		this.SplitBuildingsOnTileBounds.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.buildingOptions);
		base.Name = "ExportBuildings";
		base.Size = new System.Drawing.Size(360, 200);
		this.buildingOptions.ResumeLayout(false);
		this.buildingOptions.PerformLayout();
		base.ResumeLayout(false);
	}

	public ExportBuildings()
	{
		InitializeComponent();
	}
}
