using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using edEditorDotNet.Properties;

namespace palDotNet.palDialog;

public class SplashInfoPanel : UserControl, IDialogContainer
{
	private IContainer components;

	private PictureBox AutodeskChart;

	private DialogLabel VersionLabel;

	private DialogLabel TitleLabel;

	private Label LegalProtectionLabel;

	private Label CopyrightLabel;

	private DialogLabel AppTitleLabel;

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
		this.AutodeskChart = new System.Windows.Forms.PictureBox();
		this.VersionLabel = new palDotNet.palDialog.DialogLabel();
		this.TitleLabel = new palDotNet.palDialog.DialogLabel();
		this.LegalProtectionLabel = new System.Windows.Forms.Label();
		this.CopyrightLabel = new System.Windows.Forms.Label();
		this.AppTitleLabel = new palDotNet.palDialog.DialogLabel();
		((System.ComponentModel.ISupportInitialize)this.AutodeskChart).BeginInit();
		base.SuspendLayout();
		this.AutodeskChart.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.AutodeskChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.AutodeskChart.Image = edEditorDotNet.Properties.Resources.AutodeskChart;
		this.AutodeskChart.Location = new System.Drawing.Point(362, 0);
		this.AutodeskChart.Name = "AutodeskChart";
		this.AutodeskChart.Size = new System.Drawing.Size(358, 430);
		this.AutodeskChart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
		this.AutodeskChart.TabIndex = 11;
		this.AutodeskChart.TabStop = false;
		this.VersionLabel.AutoSize = true;
		this.VersionLabel.Location = new System.Drawing.Point(3, 43);
		this.VersionLabel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.VersionLabel.Name = "VersionLabel";
		this.VersionLabel.SaveToConfig = false;
		this.VersionLabel.Size = new System.Drawing.Size(81, 13);
		this.VersionLabel.TabIndex = 13;
		this.VersionLabel.Text = "Version: 1.8.0.0";
		this.TitleLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.TitleLabel.Location = new System.Drawing.Point(0, 0);
		this.TitleLabel.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
		this.TitleLabel.Name = "TitleLabel";
		this.TitleLabel.SaveToConfig = false;
		this.TitleLabel.Size = new System.Drawing.Size(360, 38);
		this.TitleLabel.TabIndex = 12;
		this.TitleLabel.Text = "Welcome to CityScape!";
		this.LegalProtectionLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.LegalProtectionLabel.Location = new System.Drawing.Point(3, 131);
		this.LegalProtectionLabel.Margin = new System.Windows.Forms.Padding(3, 0, 10, 0);
		this.LegalProtectionLabel.Name = "LegalProtectionLabel";
		this.LegalProtectionLabel.Size = new System.Drawing.Size(357, 65);
		this.LegalProtectionLabel.TabIndex = 14;
		this.LegalProtectionLabel.Text = "This is a promotional version of CityScape.\r\nSome of the tools and features have been limited or disabled.\r\n\r\nTo learn more about CityScape please visit our website.";
		this.CopyrightLabel.AutoSize = true;
		this.CopyrightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.CopyrightLabel.Location = new System.Drawing.Point(3, 95);
		this.CopyrightLabel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.CopyrightLabel.Name = "CopyrightLabel";
		this.CopyrightLabel.Size = new System.Drawing.Size(255, 13);
		this.CopyrightLabel.TabIndex = 18;
		this.CopyrightLabel.Text = "Copyright © 2010 PixelActive Inc. All rights reserved.";
		this.AppTitleLabel.AutoSize = true;
		this.AppTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.AppTitleLabel.Location = new System.Drawing.Point(3, 77);
		this.AppTitleLabel.Name = "AppTitleLabel";
		this.AppTitleLabel.SaveToConfig = false;
		this.AppTitleLabel.Size = new System.Drawing.Size(110, 13);
		this.AppTitleLabel.TabIndex = 19;
		this.AppTitleLabel.Text = "PixelActive CityScape";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Window;
		base.Controls.Add(this.AppTitleLabel);
		base.Controls.Add(this.CopyrightLabel);
		base.Controls.Add(this.LegalProtectionLabel);
		base.Controls.Add(this.VersionLabel);
		base.Controls.Add(this.TitleLabel);
		base.Controls.Add(this.AutodeskChart);
		base.Name = "SplashInfoPanel";
		base.Size = new System.Drawing.Size(720, 432);
		((System.ComponentModel.ISupportInitialize)this.AutodeskChart).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public SplashInfoPanel()
	{
		InitializeComponent();
	}
}
