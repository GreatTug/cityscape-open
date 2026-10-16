using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

public class ExportCoordinate : UserControl, IDialogContainer
{
	private IContainer components;

	private GroupBox meshMerging;

	private DialogRadioButton YAxisUp;

	private DialogRadioButton ZAxisUp;

	private DialogCheckBox ZeroOutMinHeight;

	public DialogRadioButton YAxisUpRadioButton => YAxisUp;

	public DialogRadioButton ZAxisUpRadioButton => ZAxisUp;

	public DialogCheckBox ZeroOutMinHeightCheckBox => ZeroOutMinHeight;

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
		this.ZeroOutMinHeight = new palDotNet.palDialog.DialogCheckBox();
		this.YAxisUp = new palDotNet.palDialog.DialogRadioButton();
		this.ZAxisUp = new palDotNet.palDialog.DialogRadioButton();
		this.meshMerging.SuspendLayout();
		base.SuspendLayout();
		this.meshMerging.Controls.Add(this.ZeroOutMinHeight);
		this.meshMerging.Controls.Add(this.YAxisUp);
		this.meshMerging.Controls.Add(this.ZAxisUp);
		this.meshMerging.Dock = System.Windows.Forms.DockStyle.Fill;
		this.meshMerging.Location = new System.Drawing.Point(0, 0);
		this.meshMerging.Name = "meshMerging";
		this.meshMerging.Size = new System.Drawing.Size(360, 150);
		this.meshMerging.TabIndex = 22;
		this.meshMerging.TabStop = false;
		this.meshMerging.Text = "Coordinate system";
		this.ZeroOutMinHeight.AutoSize = true;
		this.ZeroOutMinHeight.Location = new System.Drawing.Point(6, 75);
		this.ZeroOutMinHeight.Name = "ZeroOutMinHeight";
		this.ZeroOutMinHeight.SaveToConfig = true;
		this.ZeroOutMinHeight.Size = new System.Drawing.Size(141, 17);
		this.ZeroOutMinHeight.TabIndex = 2;
		this.ZeroOutMinHeight.Text = "Zero out minimum height";
		this.ZeroOutMinHeight.UseVisualStyleBackColor = true;
		this.YAxisUp.AutoSize = true;
		this.YAxisUp.Checked = true;
		this.YAxisUp.Location = new System.Drawing.Point(6, 19);
		this.YAxisUp.Name = "YAxisUp";
		this.YAxisUp.SaveToConfig = true;
		this.YAxisUp.Size = new System.Drawing.Size(68, 17);
		this.YAxisUp.TabIndex = 0;
		this.YAxisUp.TabStop = true;
		this.YAxisUp.Text = "Y axis up";
		this.YAxisUp.UseVisualStyleBackColor = true;
		this.ZAxisUp.AutoSize = true;
		this.ZAxisUp.Location = new System.Drawing.Point(6, 42);
		this.ZAxisUp.Name = "ZAxisUp";
		this.ZAxisUp.SaveToConfig = true;
		this.ZAxisUp.Size = new System.Drawing.Size(68, 17);
		this.ZAxisUp.TabIndex = 1;
		this.ZAxisUp.TabStop = true;
		this.ZAxisUp.Text = "Z axis up";
		this.ZAxisUp.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.meshMerging);
		base.Name = "ExportCoordinate";
		base.Size = new System.Drawing.Size(360, 150);
		this.meshMerging.ResumeLayout(false);
		this.meshMerging.PerformLayout();
		base.ResumeLayout(false);
	}

	public ExportCoordinate()
	{
		InitializeComponent();
	}

	public void UpdateUI()
	{
		if (YAxisUp.Enabled && !ZAxisUp.Enabled)
		{
			YAxisUp.Checked = true;
		}
		if (!YAxisUp.Enabled && ZAxisUp.Enabled)
		{
			ZAxisUp.Checked = true;
		}
	}
}
