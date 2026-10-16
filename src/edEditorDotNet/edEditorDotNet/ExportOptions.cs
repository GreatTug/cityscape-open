using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

public class ExportOptions : UserControl, IDialogContainer
{
	public class MergingOption
	{
		private DialogCheckBox checkBox;

		private bool merge;

		public bool Merge
		{
			get
			{
				if (checkBox.Checked)
				{
					return merge;
				}
				return true;
			}
			set
			{
				merge = value;
			}
		}

		public string Name => checkBox.Name + "Merge";

		public MergingOption(DialogCheckBox cb)
		{
			checkBox = cb;
			merge = false;
		}
	}

	private IContainer components;

	private GroupBox sceneOptions;

	private DialogCheckBox exportBuildingPlans;

	private DialogCheckBox exportNavMesh;

	private DialogCheckBox exportProps;

	private DialogCheckBox exportGraphics;

	private DialogCheckBox exportPhysics;

	private DialogCheckBox exportTraffic;

	private DialogCheckBox exportLighting;

	private DialogCheckBox exportSingleElementFile;

	private DialogCheckBox exportData;

	private SplitContainer splitContainer1;

	private DialogCheckBox ExportTrafficEdgeCurves;

	private MergingOption[] merging;

	public MergingOption[] MergingOptions => merging;

	public DialogCheckBox ExportGraphicsCheckBox => exportGraphics;

	public DialogCheckBox ExportPhysicsCheckBox => exportPhysics;

	public DialogCheckBox ExportPropsCheckBox => exportProps;

	public DialogCheckBox ExportNavMeshCheckBox => exportNavMesh;

	public DialogCheckBox ExportBuildingPlansCheckBox => exportBuildingPlans;

	public DialogCheckBox ExportLightingCheckBox => exportLighting;

	public DialogCheckBox ExportTrafficCheckBox => exportTraffic;

	public DialogCheckBox ExportDataCheckBox => exportData;

	public CheckBox MergeToSingleFileCheckBox => exportSingleElementFile;

	public CheckBox ExportTrafficEdgeCurvesCheckBox => ExportTrafficEdgeCurves;

	public bool AlwaysAllowMergeToSingleFile { get; set; }

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
		this.sceneOptions = new System.Windows.Forms.GroupBox();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.exportSingleElementFile = new palDotNet.palDialog.DialogCheckBox();
		this.exportGraphics = new palDotNet.palDialog.DialogCheckBox();
		this.exportLighting = new palDotNet.palDialog.DialogCheckBox();
		this.exportProps = new palDotNet.palDialog.DialogCheckBox();
		this.exportTraffic = new palDotNet.palDialog.DialogCheckBox();
		this.ExportTrafficEdgeCurves = new palDotNet.palDialog.DialogCheckBox();
		this.exportData = new palDotNet.palDialog.DialogCheckBox();
		this.exportNavMesh = new palDotNet.palDialog.DialogCheckBox();
		this.exportBuildingPlans = new palDotNet.palDialog.DialogCheckBox();
		this.exportPhysics = new palDotNet.palDialog.DialogCheckBox();
		this.sceneOptions.SuspendLayout();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		base.SuspendLayout();
		this.sceneOptions.Controls.Add(this.splitContainer1);
		this.sceneOptions.Dock = System.Windows.Forms.DockStyle.Fill;
		this.sceneOptions.Location = new System.Drawing.Point(0, 0);
		this.sceneOptions.Name = "sceneOptions";
		this.sceneOptions.Size = new System.Drawing.Size(300, 200);
		this.sceneOptions.TabIndex = 21;
		this.sceneOptions.TabStop = false;
		this.sceneOptions.Text = "Export options";
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.Location = new System.Drawing.Point(3, 16);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.exportSingleElementFile);
		this.splitContainer1.Panel1.Controls.Add(this.exportGraphics);
		this.splitContainer1.Panel1.Controls.Add(this.exportLighting);
		this.splitContainer1.Panel1.Controls.Add(this.exportProps);
		this.splitContainer1.Panel1.Controls.Add(this.exportTraffic);
		this.splitContainer1.Panel2.Controls.Add(this.ExportTrafficEdgeCurves);
		this.splitContainer1.Panel2.Controls.Add(this.exportData);
		this.splitContainer1.Panel2.Controls.Add(this.exportNavMesh);
		this.splitContainer1.Panel2.Controls.Add(this.exportBuildingPlans);
		this.splitContainer1.Panel2.Controls.Add(this.exportPhysics);
		this.splitContainer1.Size = new System.Drawing.Size(294, 181);
		this.splitContainer1.SplitterDistance = 147;
		this.splitContainer1.TabIndex = 19;
		this.exportSingleElementFile.AutoSize = true;
		this.exportSingleElementFile.Location = new System.Drawing.Point(3, 108);
		this.exportSingleElementFile.Name = "exportSingleElementFile";
		this.exportSingleElementFile.SaveToConfig = true;
		this.exportSingleElementFile.Size = new System.Drawing.Size(114, 17);
		this.exportSingleElementFile.TabIndex = 4;
		this.exportSingleElementFile.Text = "Merge to single file";
		this.exportSingleElementFile.UseVisualStyleBackColor = true;
		this.exportGraphics.AutoSize = true;
		this.exportGraphics.Checked = true;
		this.exportGraphics.CheckState = System.Windows.Forms.CheckState.Checked;
		this.exportGraphics.Location = new System.Drawing.Point(3, 3);
		this.exportGraphics.Name = "exportGraphics";
		this.exportGraphics.SaveToConfig = true;
		this.exportGraphics.Size = new System.Drawing.Size(109, 17);
		this.exportGraphics.TabIndex = 0;
		this.exportGraphics.Text = "Terrain and roads";
		this.exportGraphics.UseVisualStyleBackColor = true;
		this.exportLighting.AutoSize = true;
		this.exportLighting.Location = new System.Drawing.Point(3, 72);
		this.exportLighting.Name = "exportLighting";
		this.exportLighting.SaveToConfig = true;
		this.exportLighting.Size = new System.Drawing.Size(63, 17);
		this.exportLighting.TabIndex = 3;
		this.exportLighting.Text = "Lighting";
		this.exportLighting.UseVisualStyleBackColor = true;
		this.exportProps.AutoSize = true;
		this.exportProps.Checked = true;
		this.exportProps.CheckState = System.Windows.Forms.CheckState.Checked;
		this.exportProps.Location = new System.Drawing.Point(3, 26);
		this.exportProps.Name = "exportProps";
		this.exportProps.SaveToConfig = true;
		this.exportProps.Size = new System.Drawing.Size(53, 17);
		this.exportProps.TabIndex = 1;
		this.exportProps.Text = "Props";
		this.exportProps.UseVisualStyleBackColor = true;
		this.exportTraffic.AutoSize = true;
		this.exportTraffic.Location = new System.Drawing.Point(3, 49);
		this.exportTraffic.Name = "exportTraffic";
		this.exportTraffic.SaveToConfig = true;
		this.exportTraffic.Size = new System.Drawing.Size(56, 17);
		this.exportTraffic.TabIndex = 2;
		this.exportTraffic.Text = "Traffic";
		this.exportTraffic.UseVisualStyleBackColor = true;
		this.ExportTrafficEdgeCurves.AutoSize = true;
		this.ExportTrafficEdgeCurves.Location = new System.Drawing.Point(4, 108);
		this.ExportTrafficEdgeCurves.Name = "ExportTrafficEdgeCurves";
		this.ExportTrafficEdgeCurves.SaveToConfig = true;
		this.ExportTrafficEdgeCurves.Size = new System.Drawing.Size(118, 17);
		this.ExportTrafficEdgeCurves.TabIndex = 9;
		this.ExportTrafficEdgeCurves.Text = "Traffic edge curves";
		this.ExportTrafficEdgeCurves.UseVisualStyleBackColor = true;
		this.exportData.AutoSize = true;
		this.exportData.Location = new System.Drawing.Point(4, 72);
		this.exportData.Name = "exportData";
		this.exportData.SaveToConfig = true;
		this.exportData.Size = new System.Drawing.Size(89, 17);
		this.exportData.TabIndex = 8;
		this.exportData.Text = "Abstract data";
		this.exportData.UseVisualStyleBackColor = true;
		this.exportNavMesh.AutoSize = true;
		this.exportNavMesh.Location = new System.Drawing.Point(4, 49);
		this.exportNavMesh.Name = "exportNavMesh";
		this.exportNavMesh.SaveToConfig = true;
		this.exportNavMesh.Size = new System.Drawing.Size(105, 17);
		this.exportNavMesh.TabIndex = 7;
		this.exportNavMesh.Text = "Navigation mesh";
		this.exportNavMesh.UseVisualStyleBackColor = true;
		this.exportBuildingPlans.AutoSize = true;
		this.exportBuildingPlans.Location = new System.Drawing.Point(4, 3);
		this.exportBuildingPlans.Name = "exportBuildingPlans";
		this.exportBuildingPlans.SaveToConfig = true;
		this.exportBuildingPlans.Size = new System.Drawing.Size(91, 17);
		this.exportBuildingPlans.TabIndex = 5;
		this.exportBuildingPlans.Text = "Building plans";
		this.exportBuildingPlans.UseVisualStyleBackColor = true;
		this.exportPhysics.AutoSize = true;
		this.exportPhysics.Location = new System.Drawing.Point(4, 26);
		this.exportPhysics.Name = "exportPhysics";
		this.exportPhysics.SaveToConfig = true;
		this.exportPhysics.Size = new System.Drawing.Size(62, 17);
		this.exportPhysics.TabIndex = 6;
		this.exportPhysics.Text = "Physics";
		this.exportPhysics.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.sceneOptions);
		base.Name = "ExportOptions";
		base.Size = new System.Drawing.Size(300, 200);
		this.sceneOptions.ResumeLayout(false);
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel1.PerformLayout();
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.Panel2.PerformLayout();
		this.splitContainer1.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public ExportOptions()
	{
		InitializeComponent();
		exportGraphics.CheckedChanged += export_CheckedChanged;
		exportPhysics.CheckedChanged += export_CheckedChanged;
		exportProps.CheckedChanged += export_CheckedChanged;
		exportNavMesh.CheckedChanged += export_CheckedChanged;
		exportBuildingPlans.CheckedChanged += export_CheckedChanged;
		exportLighting.CheckedChanged += export_CheckedChanged;
		exportTraffic.CheckedChanged += export_CheckedChanged;
		exportData.CheckedChanged += export_CheckedChanged;
		merging = new MergingOption[8];
		merging[0] = new MergingOption(exportGraphics);
		merging[1] = new MergingOption(exportPhysics);
		merging[2] = new MergingOption(exportProps);
		merging[3] = new MergingOption(exportNavMesh);
		merging[4] = new MergingOption(exportBuildingPlans);
		merging[5] = new MergingOption(exportLighting);
		merging[6] = new MergingOption(exportTraffic);
		merging[7] = new MergingOption(exportData);
	}

	public void UpdateUI()
	{
		UpdateEnabledButtons();
	}

	private void UpdateEnabledButtons()
	{
		bool flag = true;
		MergingOption[] array = merging;
		foreach (MergingOption mergingOption in array)
		{
			if (!mergingOption.Merge)
			{
				flag = false;
				break;
			}
		}
		if (flag || AlwaysAllowMergeToSingleFile)
		{
			exportSingleElementFile.Enabled = true;
			return;
		}
		exportSingleElementFile.Enabled = false;
		exportSingleElementFile.Checked = false;
	}

	private void export_CheckedChanged(object sender, EventArgs e)
	{
		UpdateEnabledButtons();
	}
}
