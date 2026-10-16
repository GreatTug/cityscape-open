using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace palDotNet.palDialog;

public class RoadGenerationOptionsPanel : UserControl, IDialogContainer
{
	private IContainer components;

	private GroupBox RoadGenerationGroupBox;

	private DialogCheckGroupBox AutoResolveCrossingsCheckGroupBox;

	private DialogLabel ResolveCrossingsHeightMinLabel;

	private DialogLabel ResolveCrossingsMaxTunnelHeightLabel;

	private DialogLabel ResolveCrossingsMinClearanceLabel;

	private DialogNumericUpDownFloat ResolveCrossingsTunnelHeightMaxInput;

	private DialogNumericUpDownFloat ResolveCrossingsMinClearanceInput;

	private DialogNumericUpDownFloat ResolveCrossingsHeightMinInput;

	private GroupBox RoadSegmentsGroupBox;

	private DialogCheckBox MergeOverlappingSegmentsCheckBox;

	private DialogLabel ResolveQualityLabel;

	private DialogLabel ResolveQualityDisplay;

	private DialogTrackBar ResolveCrossingsQualityInput;

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
		this.RoadGenerationGroupBox = new System.Windows.Forms.GroupBox();
		this.RoadSegmentsGroupBox = new System.Windows.Forms.GroupBox();
		this.MergeOverlappingSegmentsCheckBox = new palDotNet.palDialog.DialogCheckBox();
		this.AutoResolveCrossingsCheckGroupBox = new palDotNet.palDialog.DialogCheckGroupBox();
		this.ResolveCrossingsHeightMinLabel = new palDotNet.palDialog.DialogLabel();
		this.ResolveCrossingsQualityInput = new palDotNet.palDialog.DialogTrackBar();
		this.ResolveQualityDisplay = new palDotNet.palDialog.DialogLabel();
		this.ResolveQualityLabel = new palDotNet.palDialog.DialogLabel();
		this.ResolveCrossingsMaxTunnelHeightLabel = new palDotNet.palDialog.DialogLabel();
		this.ResolveCrossingsMinClearanceLabel = new palDotNet.palDialog.DialogLabel();
		this.ResolveCrossingsHeightMinInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.ResolveCrossingsTunnelHeightMaxInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.ResolveCrossingsMinClearanceInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.RoadGenerationGroupBox.SuspendLayout();
		this.RoadSegmentsGroupBox.SuspendLayout();
		this.AutoResolveCrossingsCheckGroupBox.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ResolveCrossingsQualityInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ResolveCrossingsHeightMinInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ResolveCrossingsTunnelHeightMaxInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ResolveCrossingsMinClearanceInput).BeginInit();
		base.SuspendLayout();
		this.RoadGenerationGroupBox.Controls.Add(this.RoadSegmentsGroupBox);
		this.RoadGenerationGroupBox.Controls.Add(this.AutoResolveCrossingsCheckGroupBox);
		this.RoadGenerationGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.RoadGenerationGroupBox.Location = new System.Drawing.Point(0, 0);
		this.RoadGenerationGroupBox.Name = "RoadGenerationGroupBox";
		this.RoadGenerationGroupBox.Size = new System.Drawing.Size(400, 300);
		this.RoadGenerationGroupBox.TabIndex = 0;
		this.RoadGenerationGroupBox.TabStop = false;
		this.RoadGenerationGroupBox.Text = "Road generation";
		this.RoadSegmentsGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.RoadSegmentsGroupBox.Controls.Add(this.MergeOverlappingSegmentsCheckBox);
		this.RoadSegmentsGroupBox.Location = new System.Drawing.Point(6, 222);
		this.RoadSegmentsGroupBox.Name = "RoadSegmentsGroupBox";
		this.RoadSegmentsGroupBox.Size = new System.Drawing.Size(388, 72);
		this.RoadSegmentsGroupBox.TabIndex = 1;
		this.RoadSegmentsGroupBox.TabStop = false;
		this.RoadSegmentsGroupBox.Text = "Road segments";
		this.MergeOverlappingSegmentsCheckBox.AutoSize = true;
		this.MergeOverlappingSegmentsCheckBox.Checked = true;
		this.MergeOverlappingSegmentsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
		this.MergeOverlappingSegmentsCheckBox.Location = new System.Drawing.Point(33, 32);
		this.MergeOverlappingSegmentsCheckBox.Name = "MergeOverlappingSegmentsCheckBox";
		this.MergeOverlappingSegmentsCheckBox.Size = new System.Drawing.Size(162, 17);
		this.MergeOverlappingSegmentsCheckBox.TabIndex = 0;
		this.MergeOverlappingSegmentsCheckBox.Text = "Merge overlapping segments";
		this.MergeOverlappingSegmentsCheckBox.UseVisualStyleBackColor = true;
		this.AutoResolveCrossingsCheckGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.AutoResolveCrossingsCheckGroupBox.Controls.Add(this.ResolveCrossingsHeightMinLabel);
		this.AutoResolveCrossingsCheckGroupBox.Controls.Add(this.ResolveCrossingsQualityInput);
		this.AutoResolveCrossingsCheckGroupBox.Controls.Add(this.ResolveQualityDisplay);
		this.AutoResolveCrossingsCheckGroupBox.Controls.Add(this.ResolveQualityLabel);
		this.AutoResolveCrossingsCheckGroupBox.Controls.Add(this.ResolveCrossingsMaxTunnelHeightLabel);
		this.AutoResolveCrossingsCheckGroupBox.Controls.Add(this.ResolveCrossingsMinClearanceLabel);
		this.AutoResolveCrossingsCheckGroupBox.Controls.Add(this.ResolveCrossingsHeightMinInput);
		this.AutoResolveCrossingsCheckGroupBox.Controls.Add(this.ResolveCrossingsTunnelHeightMaxInput);
		this.AutoResolveCrossingsCheckGroupBox.Controls.Add(this.ResolveCrossingsMinClearanceInput);
		this.AutoResolveCrossingsCheckGroupBox.Location = new System.Drawing.Point(6, 19);
		this.AutoResolveCrossingsCheckGroupBox.Name = "AutoResolveCrossingsCheckGroupBox";
		this.AutoResolveCrossingsCheckGroupBox.Size = new System.Drawing.Size(388, 197);
		this.AutoResolveCrossingsCheckGroupBox.TabIndex = 0;
		this.AutoResolveCrossingsCheckGroupBox.TabStop = false;
		this.AutoResolveCrossingsCheckGroupBox.Text = "Auto-resolve crossings";
		this.ResolveCrossingsHeightMinLabel.AutoSize = true;
		this.ResolveCrossingsHeightMinLabel.Location = new System.Drawing.Point(30, 33);
		this.ResolveCrossingsHeightMinLabel.Name = "ResolveCrossingsHeightMinLabel";
		this.ResolveCrossingsHeightMinLabel.Size = new System.Drawing.Size(100, 13);
		this.ResolveCrossingsHeightMinLabel.TabIndex = 0;
		this.ResolveCrossingsHeightMinLabel.Text = "Height min (meters):";
		this.ResolveCrossingsQualityInput.LargeChange = 1;
		this.ResolveCrossingsQualityInput.Location = new System.Drawing.Point(183, 138);
		this.ResolveCrossingsQualityInput.Maximum = 2;
		this.ResolveCrossingsQualityInput.Name = "ResolveCrossingsQualityInput";
		this.ResolveCrossingsQualityInput.Size = new System.Drawing.Size(150, 45);
		this.ResolveCrossingsQualityInput.TabIndex = 1;
		this.ResolveCrossingsQualityInput.Value = 1;
		this.ResolveCrossingsQualityInput.ValueChanged += new System.EventHandler(ResolveCrossingsQualityInput_ValueChanged);
		this.ResolveQualityDisplay.Location = new System.Drawing.Point(33, 162);
		this.ResolveQualityDisplay.Name = "ResolveQualityDisplay";
		this.ResolveQualityDisplay.Size = new System.Drawing.Size(81, 13);
		this.ResolveQualityDisplay.TabIndex = 7;
		this.ResolveQualityDisplay.Text = "Normal";
		this.ResolveQualityDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.ResolveQualityLabel.AutoSize = true;
		this.ResolveQualityLabel.Location = new System.Drawing.Point(30, 138);
		this.ResolveQualityLabel.Name = "ResolveQualityLabel";
		this.ResolveQualityLabel.Size = new System.Drawing.Size(82, 13);
		this.ResolveQualityLabel.TabIndex = 6;
		this.ResolveQualityLabel.Text = "Resolve quality:";
		this.ResolveCrossingsMaxTunnelHeightLabel.AutoSize = true;
		this.ResolveCrossingsMaxTunnelHeightLabel.Location = new System.Drawing.Point(30, 104);
		this.ResolveCrossingsMaxTunnelHeightLabel.Name = "ResolveCrossingsMaxTunnelHeightLabel";
		this.ResolveCrossingsMaxTunnelHeightLabel.Size = new System.Drawing.Size(134, 13);
		this.ResolveCrossingsMaxTunnelHeightLabel.TabIndex = 4;
		this.ResolveCrossingsMaxTunnelHeightLabel.Text = "Max tunnel height (meters):";
		this.ResolveCrossingsMinClearanceLabel.AutoSize = true;
		this.ResolveCrossingsMinClearanceLabel.Location = new System.Drawing.Point(30, 68);
		this.ResolveCrossingsMinClearanceLabel.Name = "ResolveCrossingsMinClearanceLabel";
		this.ResolveCrossingsMinClearanceLabel.Size = new System.Drawing.Size(117, 13);
		this.ResolveCrossingsMinClearanceLabel.TabIndex = 2;
		this.ResolveCrossingsMinClearanceLabel.Text = "Min clearance (meters):";
		this.ResolveCrossingsHeightMinInput.DecimalPlaces = 3;
		this.ResolveCrossingsHeightMinInput.Location = new System.Drawing.Point(233, 31);
		this.ResolveCrossingsHeightMinInput.Maximum = new decimal(new int[4] { 1000000, 0, 0, 0 });
		this.ResolveCrossingsHeightMinInput.Minimum = new decimal(new int[4] { 1000000, 0, 0, -2147483648 });
		this.ResolveCrossingsHeightMinInput.Name = "ResolveCrossingsHeightMinInput";
		this.ResolveCrossingsHeightMinInput.Size = new System.Drawing.Size(100, 20);
		this.ResolveCrossingsHeightMinInput.TabIndex = 1;
		this.ResolveCrossingsTunnelHeightMaxInput.DecimalPlaces = 3;
		this.ResolveCrossingsTunnelHeightMaxInput.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.ResolveCrossingsTunnelHeightMaxInput.Location = new System.Drawing.Point(233, 102);
		palDotNet.palDialog.DialogNumericUpDownFloat resolveCrossingsTunnelHeightMaxInput = this.ResolveCrossingsTunnelHeightMaxInput;
		int[] bits = new int[4];
		resolveCrossingsTunnelHeightMaxInput.Maximum = new decimal(bits);
		this.ResolveCrossingsTunnelHeightMaxInput.Minimum = new decimal(new int[4] { 100, 0, 0, -2147483648 });
		this.ResolveCrossingsTunnelHeightMaxInput.Name = "ResolveCrossingsTunnelHeightMaxInput";
		this.ResolveCrossingsTunnelHeightMaxInput.Size = new System.Drawing.Size(100, 20);
		this.ResolveCrossingsTunnelHeightMaxInput.TabIndex = 5;
		this.ResolveCrossingsTunnelHeightMaxInput.Value = new decimal(new int[4] { 8, 0, 0, -2147483648 });
		this.ResolveCrossingsMinClearanceInput.DecimalPlaces = 3;
		this.ResolveCrossingsMinClearanceInput.Location = new System.Drawing.Point(233, 66);
		this.ResolveCrossingsMinClearanceInput.Maximum = new decimal(new int[4] { 10, 0, 0, 0 });
		this.ResolveCrossingsMinClearanceInput.Name = "ResolveCrossingsMinClearanceInput";
		this.ResolveCrossingsMinClearanceInput.Size = new System.Drawing.Size(100, 20);
		this.ResolveCrossingsMinClearanceInput.TabIndex = 3;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.RoadGenerationGroupBox);
		base.Margin = new System.Windows.Forms.Padding(6);
		base.Name = "RoadGenerationOptionsPanel";
		base.Size = new System.Drawing.Size(400, 300);
		this.RoadGenerationGroupBox.ResumeLayout(false);
		this.RoadSegmentsGroupBox.ResumeLayout(false);
		this.RoadSegmentsGroupBox.PerformLayout();
		this.AutoResolveCrossingsCheckGroupBox.ResumeLayout(false);
		this.AutoResolveCrossingsCheckGroupBox.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ResolveCrossingsQualityInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ResolveCrossingsHeightMinInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ResolveCrossingsTunnelHeightMaxInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ResolveCrossingsMinClearanceInput).EndInit();
		base.ResumeLayout(false);
	}

	public RoadGenerationOptionsPanel()
	{
		InitializeComponent();
		SyncResolveQuality();
	}

	private void ResolveCrossingsQualityInput_ValueChanged(object sender, EventArgs e)
	{
		SyncResolveQuality();
	}

	private void SyncResolveQuality()
	{
		switch (ResolveCrossingsQualityInput.Value)
		{
		case 0:
			ResolveQualityDisplay.Text = "Quick";
			break;
		case 1:
			ResolveQualityDisplay.Text = "Normal";
			break;
		case 2:
			ResolveQualityDisplay.Text = "Robust";
			break;
		}
	}
}
