using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

public class ExportMeshMerging : UserControl, IDialogContainer
{
	private IContainer components;

	private GroupBox meshMerging;

	private DialogCheckBox mergeRoads;

	private DialogCheckBox mergeTerrain;

	private DialogCheckBox mergeAll;

	public CheckBox MergeAll => mergeAll;

	public CheckBox MergeTerrainCheckBox => mergeTerrain;

	public CheckBox MergeRoadsCheckBox => mergeRoads;

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
		this.meshMerging = new System.Windows.Forms.GroupBox();
		this.mergeAll = new palDotNet.palDialog.DialogCheckBox();
		this.mergeRoads = new palDotNet.palDialog.DialogCheckBox();
		this.mergeTerrain = new palDotNet.palDialog.DialogCheckBox();
		this.meshMerging.SuspendLayout();
		base.SuspendLayout();
		this.meshMerging.Controls.Add(this.mergeAll);
		this.meshMerging.Controls.Add(this.mergeRoads);
		this.meshMerging.Controls.Add(this.mergeTerrain);
		this.meshMerging.Dock = System.Windows.Forms.DockStyle.Fill;
		this.meshMerging.Location = new System.Drawing.Point(0, 0);
		this.meshMerging.Name = "meshMerging";
		this.meshMerging.Size = new System.Drawing.Size(300, 200);
		this.meshMerging.TabIndex = 22;
		this.meshMerging.TabStop = false;
		this.meshMerging.Text = "Mesh merging";
		this.mergeAll.AutoSize = true;
		this.mergeAll.Location = new System.Drawing.Point(6, 19);
		this.mergeAll.Name = "mergeAll";
		this.mergeAll.SaveToConfig = true;
		this.mergeAll.Size = new System.Drawing.Size(69, 17);
		this.mergeAll.TabIndex = 0;
		this.mergeAll.Text = "Merge all";
		this.mergeAll.UseVisualStyleBackColor = true;
		this.mergeAll.CheckedChanged += new System.EventHandler(mergeAll_CheckedChanged);
		this.mergeRoads.AutoSize = true;
		this.mergeRoads.Checked = true;
		this.mergeRoads.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mergeRoads.Location = new System.Drawing.Point(25, 62);
		this.mergeRoads.Name = "mergeRoads";
		this.mergeRoads.SaveToConfig = true;
		this.mergeRoads.Size = new System.Drawing.Size(85, 17);
		this.mergeRoads.TabIndex = 2;
		this.mergeRoads.Text = "Merge roads";
		this.mergeRoads.UseVisualStyleBackColor = true;
		this.mergeTerrain.AutoSize = true;
		this.mergeTerrain.Checked = true;
		this.mergeTerrain.CheckState = System.Windows.Forms.CheckState.Checked;
		this.mergeTerrain.Location = new System.Drawing.Point(25, 39);
		this.mergeTerrain.Name = "mergeTerrain";
		this.mergeTerrain.SaveToConfig = true;
		this.mergeTerrain.Size = new System.Drawing.Size(88, 17);
		this.mergeTerrain.TabIndex = 1;
		this.mergeTerrain.Text = "Merge terrain";
		this.mergeTerrain.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.meshMerging);
		base.Name = "ExportMeshMerging";
		base.Size = new System.Drawing.Size(300, 200);
		this.meshMerging.ResumeLayout(false);
		this.meshMerging.PerformLayout();
		base.ResumeLayout(false);
	}

	public ExportMeshMerging()
	{
		InitializeComponent();
	}

	private void mergeAll_CheckedChanged(object sender, EventArgs e)
	{
		mergeTerrain.Enabled = !mergeAll.Checked;
		mergeRoads.Enabled = !mergeAll.Checked;
	}
}
