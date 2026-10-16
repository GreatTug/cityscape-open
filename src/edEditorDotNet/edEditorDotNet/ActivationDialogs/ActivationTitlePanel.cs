using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using edEditorDotNet.Properties;
using palDotNet.palDialog;

namespace edEditorDotNet.ActivationDialogs;

public class ActivationTitlePanel : UserControl, IDialogContainer
{
	private IContainer components;

	private PictureBox pictureBox1;

	private Label TitleLabel;

	private DialogLabel CityScapeVersion;

	private Label VersionLabel;

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
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.TitleLabel = new System.Windows.Forms.Label();
		this.CityScapeVersion = new palDotNet.palDialog.DialogLabel();
		this.VersionLabel = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.pictureBox1.Image = edEditorDotNet.Properties.Resources.ActivationIcon64;
		this.pictureBox1.Location = new System.Drawing.Point(335, 0);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(64, 64);
		this.pictureBox1.TabIndex = 12;
		this.pictureBox1.TabStop = false;
		this.TitleLabel.AutoSize = true;
		this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.TitleLabel.Location = new System.Drawing.Point(1, 10);
		this.TitleLabel.Name = "TitleLabel";
		this.TitleLabel.Size = new System.Drawing.Size(314, 31);
		this.TitleLabel.TabIndex = 11;
		this.TitleLabel.Text = "CityScape Registration";
		this.CityScapeVersion.AutoSize = true;
		this.CityScapeVersion.Location = new System.Drawing.Point(49, 51);
		this.CityScapeVersion.Margin = new System.Windows.Forms.Padding(0, 5, 3, 0);
		this.CityScapeVersion.Name = "CityScapeVersion";
		this.CityScapeVersion.SaveToConfig = false;
		this.CityScapeVersion.Size = new System.Drawing.Size(40, 13);
		this.CityScapeVersion.TabIndex = 14;
		this.CityScapeVersion.Text = "1.8.0.0";
		this.VersionLabel.AutoSize = true;
		this.VersionLabel.Location = new System.Drawing.Point(4, 51);
		this.VersionLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.VersionLabel.Name = "VersionLabel";
		this.VersionLabel.Size = new System.Drawing.Size(45, 13);
		this.VersionLabel.TabIndex = 15;
		this.VersionLabel.Text = "Version:";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Window;
		base.Controls.Add(this.VersionLabel);
		base.Controls.Add(this.CityScapeVersion);
		base.Controls.Add(this.pictureBox1);
		base.Controls.Add(this.TitleLabel);
		base.Name = "ActivationTitlePanel";
		base.Size = new System.Drawing.Size(400, 80);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public ActivationTitlePanel()
	{
		InitializeComponent();
	}

	public string GetVersion()
	{
		return CityScapeVersion.Text;
	}
}
