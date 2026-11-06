using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using edEditorDotNet.Properties;

namespace palDotNet.palVisual;

[AboutBox.AboutBoxCreator]
public class EditorAboutBox : Form
{
	private IContainer components;

	private Label labelCompanyName;

	private Label labelCopyright;

	private Label labelVersion;

	private Label labelProductName;

	private Button buttonOk;

	private PictureBox pictureBox1;

	private LinkLabel linkLabelWebsite;

	private RichTextBox richTextBoxDescription;

	private Label EulaTitleLabel;

	private Label LegalInfoLabel;

	private Label ThirdPartyInfoLabel;

	public string AssemblyTitle
	{
		get
		{
			object[] customAttributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), inherit: false);
			if (customAttributes.Length > 0)
			{
				AssemblyTitleAttribute assemblyTitleAttribute = (AssemblyTitleAttribute)customAttributes[0];
				if (assemblyTitleAttribute.Title != "")
				{
					return assemblyTitleAttribute.Title;
				}
			}
			return Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().CodeBase);
		}
	}

	public string AssemblyVersion => Assembly.GetEntryAssembly().GetName().Version.ToString();

	public string AssemblyDescription
	{
		get
		{
			object[] customAttributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), inherit: false);
			if (customAttributes.Length == 0)
			{
				return "";
			}
			return ((AssemblyDescriptionAttribute)customAttributes[0]).Description;
		}
	}

	public string AssemblyProduct
	{
		get
		{
			object[] customAttributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), inherit: false);
			if (customAttributes.Length == 0)
			{
				return "";
			}
			return ((AssemblyProductAttribute)customAttributes[0]).Product;
		}
	}

	public string AssemblyCopyright
	{
		get
		{
			object[] customAttributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), inherit: false);
			if (customAttributes.Length == 0)
			{
				return "";
			}
			return ((AssemblyCopyrightAttribute)customAttributes[0]).Copyright;
		}
	}

	public string AssemblyCompany
	{
		get
		{
			object[] customAttributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), inherit: false);
			if (customAttributes.Length == 0)
			{
				return "";
			}
			return ((AssemblyCompanyAttribute)customAttributes[0]).Company;
		}
	}

	public EditorAboutBox()
	{
		InitializeComponent();
		Text = $"About {AssemblyTitle}";
		labelProductName.Text = AssemblyProduct;
		labelVersion.Text = $"Version {AssemblyVersion}";
		labelCopyright.Text = $"{AssemblyCopyright} All rights reserved.";
		labelCompanyName.Text = AssemblyCompany;
		richTextBoxDescription.Text = Resources.AboutText;
	}

	private void okButton_Click(object sender, EventArgs e)
	{
		Close();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(palDotNet.palVisual.EditorAboutBox));
		this.labelCompanyName = new System.Windows.Forms.Label();
		this.labelCopyright = new System.Windows.Forms.Label();
		this.labelVersion = new System.Windows.Forms.Label();
		this.labelProductName = new System.Windows.Forms.Label();
		this.buttonOk = new System.Windows.Forms.Button();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.linkLabelWebsite = new System.Windows.Forms.LinkLabel();
		this.richTextBoxDescription = new System.Windows.Forms.RichTextBox();
		this.EulaTitleLabel = new System.Windows.Forms.Label();
		this.LegalInfoLabel = new System.Windows.Forms.Label();
		this.ThirdPartyInfoLabel = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.labelCompanyName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.labelCompanyName.BackColor = System.Drawing.Color.Transparent;
		this.labelCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelCompanyName.Location = new System.Drawing.Point(29, 138);
		this.labelCompanyName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 2);
		this.labelCompanyName.MaximumSize = new System.Drawing.Size(0, 17);
		this.labelCompanyName.Name = "labelCompanyName";
		this.labelCompanyName.Size = new System.Drawing.Size(287, 17);
		this.labelCompanyName.TabIndex = 3;
		this.labelCompanyName.Text = "Company Name";
		this.labelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labelCopyright.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.labelCopyright.BackColor = System.Drawing.Color.Transparent;
		this.labelCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelCopyright.Location = new System.Drawing.Point(29, 157);
		this.labelCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 2);
		this.labelCopyright.MaximumSize = new System.Drawing.Size(0, 17);
		this.labelCopyright.Name = "labelCopyright";
		this.labelCopyright.Size = new System.Drawing.Size(287, 17);
		this.labelCopyright.TabIndex = 2;
		this.labelCopyright.Text = "Copyright";
		this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labelVersion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.labelVersion.BackColor = System.Drawing.Color.Transparent;
		this.labelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelVersion.Location = new System.Drawing.Point(29, 119);
		this.labelVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 2);
		this.labelVersion.MaximumSize = new System.Drawing.Size(0, 17);
		this.labelVersion.Name = "labelVersion";
		this.labelVersion.Size = new System.Drawing.Size(287, 17);
		this.labelVersion.TabIndex = 1;
		this.labelVersion.Text = "Version";
		this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labelProductName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.labelProductName.BackColor = System.Drawing.Color.Transparent;
		this.labelProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelProductName.Location = new System.Drawing.Point(29, 100);
		this.labelProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 2);
		this.labelProductName.MaximumSize = new System.Drawing.Size(0, 17);
		this.labelProductName.Name = "labelProductName";
		this.labelProductName.Size = new System.Drawing.Size(287, 17);
		this.labelProductName.TabIndex = 0;
		this.labelProductName.Text = "Product Name";
		this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.buttonOk.Location = new System.Drawing.Point(367, 507);
		this.buttonOk.Name = "buttonOk";
		this.buttonOk.Size = new System.Drawing.Size(75, 23);
		this.buttonOk.TabIndex = 7;
		this.buttonOk.Text = "OK";
		this.buttonOk.UseVisualStyleBackColor = true;
		this.buttonOk.Click += new System.EventHandler(okButton_Click);
		this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.pictureBox1.Image = edEditorDotNet.Properties.Resources.AboutLogo;
		this.pictureBox1.InitialImage = edEditorDotNet.Properties.Resources.AboutLogo;
		this.pictureBox1.Location = new System.Drawing.Point(4, 3);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(463, 80);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox1.TabIndex = 25;
		this.pictureBox1.TabStop = false;
		this.linkLabelWebsite.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.linkLabelWebsite.AutoSize = true;
		this.linkLabelWebsite.BackColor = System.Drawing.Color.Transparent;
		this.linkLabelWebsite.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
		this.linkLabelWebsite.Location = new System.Drawing.Point(26, 512);
		this.linkLabelWebsite.Name = "linkLabelWebsite";
		this.linkLabelWebsite.Size = new System.Drawing.Size(123, 13);
		this.linkLabelWebsite.TabIndex = 6;
		this.linkLabelWebsite.TabStop = true;
		this.linkLabelWebsite.Text = "http://pixelactive3d.com";
		this.linkLabelWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabelWebsite_LinkClicked);
		this.richTextBoxDescription.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.richTextBoxDescription.BackColor = System.Drawing.SystemColors.Control;
		this.richTextBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.richTextBoxDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.richTextBoxDescription.Location = new System.Drawing.Point(29, 336);
		this.richTextBoxDescription.Name = "richTextBoxDescription";
		this.richTextBoxDescription.ReadOnly = true;
		this.richTextBoxDescription.Size = new System.Drawing.Size(413, 150);
		this.richTextBoxDescription.TabIndex = 5;
		this.richTextBoxDescription.Text = "";
		this.EulaTitleLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.EulaTitleLabel.AutoSize = true;
		this.EulaTitleLabel.BackColor = System.Drawing.Color.Transparent;
		this.EulaTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.EulaTitleLabel.Location = new System.Drawing.Point(29, 317);
		this.EulaTitleLabel.Name = "EulaTitleLabel";
		this.EulaTitleLabel.Size = new System.Drawing.Size(171, 13);
		this.EulaTitleLabel.TabIndex = 4;
		this.EulaTitleLabel.Text = "End-User License Agreement";
		this.LegalInfoLabel.BackColor = System.Drawing.Color.Transparent;
		this.LegalInfoLabel.Location = new System.Drawing.Point(29, 198);
		this.LegalInfoLabel.Name = "LegalInfoLabel";
		this.LegalInfoLabel.Size = new System.Drawing.Size(374, 60);
		this.LegalInfoLabel.TabIndex = 26;
		this.LegalInfoLabel.Text = resources.GetString("LegalInfoLabel.Text");
		this.ThirdPartyInfoLabel.BackColor = System.Drawing.Color.Transparent;
		this.ThirdPartyInfoLabel.Location = new System.Drawing.Point(29, 262);
		this.ThirdPartyInfoLabel.Name = "ThirdPartyInfoLabel";
		this.ThirdPartyInfoLabel.Size = new System.Drawing.Size(374, 38);
		this.ThirdPartyInfoLabel.TabIndex = 27;
		this.ThirdPartyInfoLabel.Text = "This computer program includes certain third party software components subject to licenses and notices as set forth in the End User License Agreement. ";
		this.EulaTitleLabel.Visible = false;
		this.LegalInfoLabel.Visible = false;
		this.ThirdPartyInfoLabel.Visible = false;
		this.BackColor = System.Drawing.Color.White;
		this.BackgroundImage = edEditorDotNet.Properties.Resources.backgroundgradient;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		base.ClientSize = new System.Drawing.Size(471, 542);
		base.Controls.Add(this.ThirdPartyInfoLabel);
		base.Controls.Add(this.LegalInfoLabel);
		base.Controls.Add(this.richTextBoxDescription);
		base.Controls.Add(this.EulaTitleLabel);
		base.Controls.Add(this.linkLabelWebsite);
		base.Controls.Add(this.pictureBox1);
		base.Controls.Add(this.buttonOk);
		base.Controls.Add(this.labelCompanyName);
		base.Controls.Add(this.labelCopyright);
		base.Controls.Add(this.labelVersion);
		base.Controls.Add(this.labelProductName);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "EditorAboutBox";
		base.Padding = new System.Windows.Forms.Padding(9);
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "EditorAboutBox";
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
