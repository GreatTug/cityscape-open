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
		Text = "About PixelActive CityScape 1.8 (Decompiled Edition)";
		labelProductName.Text = "PixelActive CityScape 1.8 (Decompiled Edition)";
		labelVersion.Text = $"Version {AssemblyVersion} [Decompiled Edition]";
		labelCopyright.Text = "Original © PixelActive Inc. / Autodesk Inc. | Reconstructed 2026";
		labelCompanyName.Text = "Akademik Araştırma & Hobi Projesi (Academic Research & Hobby Project)";
		EulaTitleLabel.Text = "Akademik Araştırma ve Hobi Projesi Lisans Bildirimi";
		LegalInfoLabel.Text = "Bu yazılım, akademik araştırmalara uygun hale getirilmiş kar amacı gütmeyen bir hobi projesidir. 3D kentsel modelleme motoru mimarisini ve veri yapılarını incelemek amacıyla restore edilmiştir.\n(Non-commercial hobby project adapted for academic research & software preservation.)";
		ThirdPartyInfoLabel.Text = "Bu proje ticari kazanç amacı taşımamaktadır. PixelActive ve Autodesk markaları orijinal hak sahiplerine aittir.";
		linkLabelWebsite.Text = "https://github.com/cityscape-open";

		string license = Resources.AboutText;
		if (string.IsNullOrEmpty(license) || license.StartsWith("PIXELACTIVE INC. END USER"))
		{
			license = GetAcademicHobbyLicense();
		}
		richTextBoxDescription.Text = license;
	}

	public static string GetAcademicHobbyLicense()
	{
		return
			"PIXELACTIVE CITYSCAPE 1.8 - DECOMPILED EDITION\r\n" +
			"AKADEMİK ARAŞTIRMA VE HOBİ PROJESİ LİSANSI / BİLDİRİMİ\r\n" +
			"======================================================================\r\n\r\n" +
			"1. PROJE NİTELİĞİ VE KAPSAMI (PROJECT NATURE & SCOPE)\r\n" +
			"Bu yazılım, 2010 yılında desteği ve satışı sonlandırılmış olan PixelActive CityScape 1.8\r\n" +
			"yazılımının yazılım arkeolojisi, tersine mühendislik ve bilgisayar grafikleri araştırmaları\r\n" +
			"kapsamında incelenmesi amacıyla geliştirilmiş bir HOBİ PROJESİDİR.\r\n\r\n" +
			"2. AKADEMİK ARAŞTIRMA VE EĞİTİM AMACI (ACADEMIC RESEARCH & EDUCATION)\r\n" +
			"Bu sürüm (\"Decompiled Edition\"), yalnızca akademik araştırmalara, eğitim çalışmalarına\r\n" +
			"ve kâr amacı gütmeyen hobi deneylerine uygun hale getirilmiştir:\r\n" +
			"  - Herhangi bir ticari kazanç, lisans satışı veya ticari dağıtım amacı gütmez.\r\n" +
			"  - Bilgisayar bilimleri araştırmacıları, öğrenciler ve 3D simülasyon geliştiricilerinin\r\n" +
			"    prosedürel yol ağı üretimi, arazi deformasyonu ve kentsel modelleme mimarisini\r\n" +
			"    ve tescilli veri formatlarını (.csp, .cst, .parc) incelemesine olanak tanır.\r\n" +
			"  - Modern işletim sistemleri (Windows 10/11 x64, .NET Framework 4.8) üzerinde kararlı\r\n" +
			"    çalışabilirlik sağlamak için açık kaynak restorasyon çalışmaları yapılmıştır.\r\n\r\n" +
			"3. ORİJİNAL TELİF HAKLARI VE TİCARİ MARKALAR (INTELLECTUAL PROPERTY & TRADEMARKS)\r\n" +
			"  - \"PixelActive\", \"CityScape\" ve ilgili tüm orijinal logo, marka, patent ve materyaller\r\n" +
			"    orijinal hak sahiplerine (PixelActive Inc. ve Autodesk Inc.) aittir.\r\n" +
			"  - Bu proje bağımsız bir eğitim/araştırma projesi olup, orijinal telif sahipleriyle\r\n" +
			"    resmi bir bağlantısı, sponsorluğu veya ticari ortaklığı bulunmamaktadır.\r\n" +
			"  - Yazılımın incelenmesi; birlikte çalışabilirlik (interoperability) ve akademik eğitim\r\n" +
			"    istisnaları çerçevesinde gerçekleştirilmektedir.\r\n\r\n" +
			"4. GARANTİ REDDİ (DISCLAIMER OF WARRANTY)\r\n" +
			"Bu yazılım 'OLDUĞU GİBİ' (AS-IS) sunulmaktadır. Yazılımın kullanımından veya araştırmalardan\r\n" +
			"doğabilecek hiçbir doğrudan ya da dolaylı zarardan geliştiriciler veya katkıda bulunanlar\r\n" +
			"sorumlu tutulamaz.\r\n\r\n" +
			"======================================================================\r\n" +
			"ENGLISH SUMMARY:\r\n" +
			"PixelActive CityScape 1.8 (Decompiled Edition) is an educational hobby project\r\n" +
			"reconstructed strictly for academic research, reverse engineering studies, and software\r\n" +
			"preservation. Non-commercial use only. All original proprietary assets and trademarks\r\n" +
			"remain the property of their respective owners (PixelActive Inc. / Autodesk Inc.).\r\n\r\n" +
			"======================================================================\r\n" +
			"THIRD PARTY COMPONENTS:\r\n" +
			"- DockPanelSuite: Copyright (c) 2007 Weifen Luo (MIT License)\r\n" +
			"- FreeImage: FreeImage Public License (FIPL) / GPL\r\n" +
			"- libcpuid: Copyright (c) 2009 libcpuid contributors (BSD License)\r\n" +
			"- Squish: Copyright (c) 2006 Simon Brown (MIT License)\r\n";
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
		this.labelCompanyName.Size = new System.Drawing.Size(413, 17);
		this.labelCompanyName.TabIndex = 3;
		this.labelCompanyName.Text = "Company Name";
		this.labelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labelCompanyName.UseMnemonic = false;
		this.labelCopyright.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.labelCopyright.BackColor = System.Drawing.Color.Transparent;
		this.labelCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelCopyright.Location = new System.Drawing.Point(29, 157);
		this.labelCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 2);
		this.labelCopyright.MaximumSize = new System.Drawing.Size(0, 17);
		this.labelCopyright.Name = "labelCopyright";
		this.labelCopyright.Size = new System.Drawing.Size(413, 17);
		this.labelCopyright.TabIndex = 2;
		this.labelCopyright.Text = "Copyright";
		this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labelCopyright.UseMnemonic = false;
		this.labelVersion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.labelVersion.BackColor = System.Drawing.Color.Transparent;
		this.labelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labelVersion.Location = new System.Drawing.Point(29, 119);
		this.labelVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 2);
		this.labelVersion.MaximumSize = new System.Drawing.Size(0, 17);
		this.labelVersion.Name = "labelVersion";
		this.labelVersion.Size = new System.Drawing.Size(413, 17);
		this.labelVersion.TabIndex = 1;
		this.labelVersion.Text = "Version";
		this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labelProductName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.labelProductName.BackColor = System.Drawing.Color.Transparent;
		this.labelProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labelProductName.Location = new System.Drawing.Point(29, 100);
		this.labelProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 2);
		this.labelProductName.MaximumSize = new System.Drawing.Size(0, 17);
		this.labelProductName.Name = "labelProductName";
		this.labelProductName.Size = new System.Drawing.Size(413, 17);
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
		this.linkLabelWebsite.Size = new System.Drawing.Size(180, 13);
		this.linkLabelWebsite.TabIndex = 6;
		this.linkLabelWebsite.TabStop = true;
		this.linkLabelWebsite.Text = "https://github.com/cityscape-open";
		this.linkLabelWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabelWebsite_LinkClicked);
		this.richTextBoxDescription.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.richTextBoxDescription.BackColor = System.Drawing.Color.White;
		this.richTextBoxDescription.ForeColor = System.Drawing.Color.FromArgb(20, 20, 20);
		this.richTextBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.richTextBoxDescription.Font = new System.Drawing.Font("Consolas", 8.0f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.richTextBoxDescription.Location = new System.Drawing.Point(29, 306);
		this.richTextBoxDescription.Name = "richTextBoxDescription";
		this.richTextBoxDescription.ReadOnly = true;
		this.richTextBoxDescription.Size = new System.Drawing.Size(413, 192);
		this.richTextBoxDescription.TabIndex = 5;
		this.richTextBoxDescription.Text = "";
		this.EulaTitleLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.EulaTitleLabel.AutoSize = true;
		this.EulaTitleLabel.BackColor = System.Drawing.Color.Transparent;
		this.EulaTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.EulaTitleLabel.Location = new System.Drawing.Point(29, 288);
		this.EulaTitleLabel.Name = "EulaTitleLabel";
		this.EulaTitleLabel.Size = new System.Drawing.Size(413, 13);
		this.EulaTitleLabel.TabIndex = 4;
		this.EulaTitleLabel.Text = "Akademik Araştırma ve Hobi Projesi Lisans Bildirimi";
		this.EulaTitleLabel.UseMnemonic = false;
		this.LegalInfoLabel.BackColor = System.Drawing.Color.Transparent;
		this.LegalInfoLabel.Location = new System.Drawing.Point(29, 178);
		this.LegalInfoLabel.Name = "LegalInfoLabel";
		this.LegalInfoLabel.Size = new System.Drawing.Size(413, 68);
		this.LegalInfoLabel.TabIndex = 26;
		this.LegalInfoLabel.Text = resources.GetString("LegalInfoLabel.Text");
		this.LegalInfoLabel.UseMnemonic = false;
		this.ThirdPartyInfoLabel.BackColor = System.Drawing.Color.Transparent;
		this.ThirdPartyInfoLabel.Location = new System.Drawing.Point(29, 248);
		this.ThirdPartyInfoLabel.Name = "ThirdPartyInfoLabel";
		this.ThirdPartyInfoLabel.Size = new System.Drawing.Size(413, 36);
		this.ThirdPartyInfoLabel.TabIndex = 27;
		this.ThirdPartyInfoLabel.Text = "Bu proje ticari kazanç amacı taşımamaktadır. PixelActive ve Autodesk markaları orijinal hak sahiplerine aittir.";
		this.ThirdPartyInfoLabel.UseMnemonic = false;
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
