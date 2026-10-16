using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace palDotNet.palDialog;

[DialogBoxCreator]
[CPPCodeGeneration(OutputFile = "..\\palPal\\palDialog\\DialogBoxView")]
public class DialogBoxView : DialogBoxBase
{
	private IContainer components;

	private DialogWidgetHolder ViewHolder;

	private Button buttonOK;

	private Button buttonCancel;

	private DialogLabel Label;

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
		this.ViewHolder = new palDotNet.palDialog.DialogWidgetHolder();
		this.buttonOK = new System.Windows.Forms.Button();
		this.buttonCancel = new System.Windows.Forms.Button();
		this.Label = new palDotNet.palDialog.DialogLabel();
		base.SuspendLayout();
		this.ViewHolder.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ViewHolder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.ViewHolder.ContainsSelection = false;
		this.ViewHolder.Location = new System.Drawing.Point(12, 27);
		this.ViewHolder.Name = "ViewHolder";
		this.ViewHolder.Size = new System.Drawing.Size(468, 298);
		this.ViewHolder.TabIndex = 0;
		this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.buttonOK.Location = new System.Drawing.Point(334, 331);
		this.buttonOK.Name = "buttonOK";
		this.buttonOK.Size = new System.Drawing.Size(70, 23);
		this.buttonOK.TabIndex = 5;
		this.buttonOK.Text = "OK";
		this.buttonOK.UseVisualStyleBackColor = true;
		this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.buttonCancel.Location = new System.Drawing.Point(410, 331);
		this.buttonCancel.Name = "buttonCancel";
		this.buttonCancel.Size = new System.Drawing.Size(70, 23);
		this.buttonCancel.TabIndex = 4;
		this.buttonCancel.Text = "Cancel";
		this.buttonCancel.UseVisualStyleBackColor = true;
		this.Label.AutoSize = true;
		this.Label.Location = new System.Drawing.Point(13, 8);
		this.Label.Name = "Label";
		this.Label.Size = new System.Drawing.Size(33, 13);
		this.Label.TabIndex = 6;
		this.Label.Text = "Label";
		base.AcceptButton = this.buttonOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.buttonCancel;
		base.ClientSize = new System.Drawing.Size(492, 366);
		base.Controls.Add(this.Label);
		base.Controls.Add(this.buttonOK);
		base.Controls.Add(this.buttonCancel);
		base.Controls.Add(this.ViewHolder);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.Name = "DialogBoxView";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "DialogBoxPanels";
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public DialogBoxView()
	{
		InitializeComponent();
	}
}
