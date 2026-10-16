using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using edEditorDotNet.Properties;

namespace palDotNet.palDialog;

[DialogBoxCreator]
[CPPCodeGeneration(OutputFile = "..\\edEditor\\edDialogs\\SplashDialog", Win32PCOnly = false)]
public class SplashDialog : DialogBoxBase
{
	private IContainer components;

	private Button okButton;

	private LinkLabel linkLabelWebsite;

	private Panel panel1;

	private SplashInfoPanel SplashInfoPanel;

	public SplashDialog()
	{
		InitializeComponent();
	}

	private void linkLabelWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		try
		{
			Process.Start("http://pixelactive3d.com");
		}
		catch (Exception)
		{
		}
	}

	private void okButton_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
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
		this.okButton = new System.Windows.Forms.Button();
		this.linkLabelWebsite = new System.Windows.Forms.LinkLabel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.SplashInfoPanel = new palDotNet.palDialog.SplashInfoPanel();
		base.SuspendLayout();
		this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.okButton.Location = new System.Drawing.Point(670, 592);
		this.okButton.Name = "okButton";
		this.okButton.Size = new System.Drawing.Size(75, 23);
		this.okButton.TabIndex = 0;
		this.okButton.Text = "OK";
		this.okButton.UseVisualStyleBackColor = true;
		this.okButton.Click += new System.EventHandler(okButton_Click);
		this.linkLabelWebsite.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.linkLabelWebsite.AutoSize = true;
		this.linkLabelWebsite.BackColor = System.Drawing.Color.Transparent;
		this.linkLabelWebsite.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
		this.linkLabelWebsite.Location = new System.Drawing.Point(7, 597);
		this.linkLabelWebsite.Name = "linkLabelWebsite";
		this.linkLabelWebsite.Size = new System.Drawing.Size(123, 13);
		this.linkLabelWebsite.TabIndex = 7;
		this.linkLabelWebsite.TabStop = true;
		this.linkLabelWebsite.Text = "http://pixelactive3d.com";
		this.linkLabelWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabelWebsite_LinkClicked);
		this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel1.BackgroundImage = edEditorDotNet.Properties.Resources.SplashBanner;
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Location = new System.Drawing.Point(-1, -1);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(766, 121);
		this.panel1.TabIndex = 9;
		this.SplashInfoPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.SplashInfoPanel.BackColor = System.Drawing.SystemColors.Window;
		this.SplashInfoPanel.Location = new System.Drawing.Point(10, 133);
		this.SplashInfoPanel.Margin = new System.Windows.Forms.Padding(5, 10, 5, 0);
		this.SplashInfoPanel.Name = "SplashInfoPanel";
		this.SplashInfoPanel.Size = new System.Drawing.Size(740, 434);
		this.SplashInfoPanel.TabIndex = 11;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Window;
		base.ClientSize = new System.Drawing.Size(764, 632);
		base.Controls.Add(this.SplashInfoPanel);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.linkLabelWebsite);
		base.Controls.Add(this.okButton);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "SplashDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Welcome to CityScape!";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
