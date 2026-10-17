using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using palDotNet.palCore;
using palDotNet.palWidgets;

namespace palDotNet.palVisual;

public class AboutBox : Form
{
	[AttributeUsage(AttributeTargets.Class)]
	public class AboutBoxCreatorAttribute : Attribute
	{
	}

	private IContainer components;

	private System.Windows.Forms.TextBox textBoxDescription;

	private System.Windows.Forms.Label labelCompanyName;

	private System.Windows.Forms.Label labelCopyright;

	private System.Windows.Forms.Label labelVersion;

	private System.Windows.Forms.Label labelProductName;

	private System.Windows.Forms.Button buttonOk;

	private LinkLabel linkLabelWebsite;

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
		this.components = new System.ComponentModel.Container();
		this.textBoxDescription = new System.Windows.Forms.TextBox();
		this.labelCompanyName = new System.Windows.Forms.Label();
		this.labelCopyright = new System.Windows.Forms.Label();
		this.labelVersion = new System.Windows.Forms.Label();
		this.labelProductName = new System.Windows.Forms.Label();
		this.buttonOk = new System.Windows.Forms.Button();
		this.linkLabelWebsite = new System.Windows.Forms.LinkLabel();
		base.SuspendLayout();
		this.textBoxDescription.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.textBoxDescription.Location = new System.Drawing.Point(13, 92);
		this.textBoxDescription.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
		this.textBoxDescription.Multiline = true;
		this.textBoxDescription.Name = "textBoxDescription";
		this.textBoxDescription.ReadOnly = true;
		this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.textBoxDescription.Size = new System.Drawing.Size(378, 175);
		this.textBoxDescription.TabIndex = 23;
		this.textBoxDescription.TabStop = false;
		this.textBoxDescription.Text = "Description";
		this.labelCompanyName.BackColor = System.Drawing.Color.Transparent;
		this.labelCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelCompanyName.Location = new System.Drawing.Point(59, 66);
		this.labelCompanyName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
		this.labelCompanyName.MaximumSize = new System.Drawing.Size(0, 17);
		this.labelCompanyName.Name = "labelCompanyName";
		this.labelCompanyName.Size = new System.Drawing.Size(287, 17);
		this.labelCompanyName.TabIndex = 22;
		this.labelCompanyName.Text = "Company Name";
		this.labelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labelCopyright.BackColor = System.Drawing.Color.Transparent;
		this.labelCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelCopyright.Location = new System.Drawing.Point(59, 48);
		this.labelCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
		this.labelCopyright.MaximumSize = new System.Drawing.Size(0, 17);
		this.labelCopyright.Name = "labelCopyright";
		this.labelCopyright.Size = new System.Drawing.Size(287, 17);
		this.labelCopyright.TabIndex = 21;
		this.labelCopyright.Text = "Copyright";
		this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labelVersion.BackColor = System.Drawing.Color.Transparent;
		this.labelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelVersion.Location = new System.Drawing.Point(59, 30);
		this.labelVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
		this.labelVersion.MaximumSize = new System.Drawing.Size(0, 17);
		this.labelVersion.Name = "labelVersion";
		this.labelVersion.Size = new System.Drawing.Size(287, 17);
		this.labelVersion.TabIndex = 0;
		this.labelVersion.Text = "Version";
		this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labelProductName.BackColor = System.Drawing.Color.Transparent;
		this.labelProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelProductName.Location = new System.Drawing.Point(59, 12);
		this.labelProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
		this.labelProductName.MaximumSize = new System.Drawing.Size(0, 17);
		this.labelProductName.Name = "labelProductName";
		this.labelProductName.Size = new System.Drawing.Size(287, 17);
		this.labelProductName.TabIndex = 19;
		this.labelProductName.Text = "Product Name";
		this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.buttonOk.Location = new System.Drawing.Point(318, 280);
		this.buttonOk.Name = "buttonOk";
		this.buttonOk.Size = new System.Drawing.Size(75, 23);
		this.buttonOk.TabIndex = 24;
		this.buttonOk.Text = "OK";
		this.buttonOk.UseVisualStyleBackColor = true;
		this.buttonOk.Click += new System.EventHandler(okButton_Click);
		this.linkLabelWebsite.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.linkLabelWebsite.AutoSize = true;
		this.linkLabelWebsite.BackColor = System.Drawing.Color.Transparent;
		this.linkLabelWebsite.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
		this.linkLabelWebsite.Location = new System.Drawing.Point(12, 285);
		this.linkLabelWebsite.Name = "linkLabelWebsite";
		this.linkLabelWebsite.Size = new System.Drawing.Size(180, 13);
		this.linkLabelWebsite.TabIndex = 26;
		this.linkLabelWebsite.TabStop = true;
		this.linkLabelWebsite.Text = "https://github.com/cityscape-open";
		this.linkLabelWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabelWebsite_LinkClicked);
		this.BackColor = System.Drawing.SystemColors.Control;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		base.ClientSize = new System.Drawing.Size(405, 315);
		base.Controls.Add(this.linkLabelWebsite);
		base.Controls.Add(this.buttonOk);
		base.Controls.Add(this.textBoxDescription);
		base.Controls.Add(this.labelCompanyName);
		base.Controls.Add(this.labelCopyright);
		base.Controls.Add(this.labelVersion);
		base.Controls.Add(this.labelProductName);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "AboutBox";
		base.Padding = new System.Windows.Forms.Padding(9);
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "About PixelActive CityScape 1.8 (Decompiled Edition)";
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public AboutBox()
	{
		InitializeComponent();
		Text = "About PixelActive CityScape 1.8 (Decompiled Edition)";
		labelProductName.Text = "PixelActive CityScape 1.8 (Decompiled Edition)";
		labelVersion.Text = $"Version {AssemblyVersion} [Decompiled Edition]";
		labelCopyright.Text = "Original © PixelActive Inc. / Autodesk Inc. | Reconstructed 2026";
		labelCompanyName.Text = "Akademik Araştırma & Hobi Projesi";
		linkLabelWebsite.Text = "https://github.com/cityscape-open";
		textBoxDescription.Text =
			"PixelActive CityScape 1.8 (Decompiled Edition)\r\n\r\n" +
			"Bu yazılım, akademik araştırmalara uygun hale getirilmiş kar amacı gütmeyen bir hobi projesidir.\r\n" +
			"Yazılım arkeolojisi, tersine mühendislik ve 3D kentsel simülasyon araştırmaları için restore edilmiştir.\r\n\r\n" +
			"This software has been reconstructed solely for academic research, educational study, and non-commercial hobby experimentation.\r\n\r\n" +
			"All original copyrights and trademarks remain with PixelActive Inc. / Autodesk Inc.";
	}

	private void okButton_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void linkLabelWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		try
		{
			Process.Start("https://github.com/cityscape-open");
		}
		catch (Exception)
		{
		}
	}

	public static void Show(string dllName)
	{
		Assembly assembly = null;
		Form form = null;
		if (dllName != null && dllName != string.Empty)
		{
			try
			{
				string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
				if (!dllName.ToLower().EndsWith(".dll"))
				{
					dllName += ".dll";
				}
				dllName = Path.Combine(directoryName, dllName);
				assembly = Assembly.LoadFrom(dllName);
			}
			catch (Exception ex)
			{
				Output.WriteLine("failed to load dll '" + dllName + "': " + ex.Message);
			}
		}
		if (assembly != null)
		{
			AboutBoxCreatorAttribute aboutBoxCreatorAttribute = new AboutBoxCreatorAttribute();
			ComponentSearcher componentSearcher = new ComponentSearcher(aboutBoxCreatorAttribute.GetType(), assembly);
			foreach (Type item in componentSearcher)
			{
				form = Activator.CreateInstance(item, null) as Form;
				if (form != null)
				{
					break;
				}
			}
		}
		if (form == null)
		{
			form = new AboutBox();
		}
		form.ShowDialog();
	}
}
