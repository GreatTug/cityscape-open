using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

public class ExportTextureSaving : UserControl, IDialogContainer
{
	private IContainer components;

	private GroupBox textureSaving;

	private DialogComboBox TextureFormat;

	private DialogCheckBox SaveSourceTextures;

	private DialogCheckBox SaveEmbeddedTextures;

	private DialogLabel TextureFormatLabel;

	private DialogLabel TextureOutputLabel;

	private DialogPathLoader TexturePath;

	private CheckBox overwriteTextures;

	private DialogCheckBox TexturePathUseExportPath;

	protected bool TexturesWillBeSaved
	{
		get
		{
			if (!SaveEmbeddedTextures.Checked)
			{
				return SaveSourceTextures.Checked;
			}
			return true;
		}
	}

	public CheckBox SaveEmbeddedTexturesCheckBox => SaveEmbeddedTextures;

	public CheckBox SaveSourceTexturesCheckBox => SaveSourceTextures;

	public ComboBox TextureFormatComboBox => TextureFormat;

	public CheckBox TexturePathUseExportPathCheckBox => TexturePathUseExportPath;

	public CheckBox OverwriteTexturesCheckBox => overwriteTextures;

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
		this.textureSaving = new System.Windows.Forms.GroupBox();
		this.TexturePathUseExportPath = new palDotNet.palDialog.DialogCheckBox();
		this.overwriteTextures = new System.Windows.Forms.CheckBox();
		this.TextureOutputLabel = new palDotNet.palDialog.DialogLabel();
		this.TexturePath = new palDotNet.palDialog.DialogPathLoader();
		this.TextureFormatLabel = new palDotNet.palDialog.DialogLabel();
		this.TextureFormat = new palDotNet.palDialog.DialogComboBox();
		this.SaveSourceTextures = new palDotNet.palDialog.DialogCheckBox();
		this.SaveEmbeddedTextures = new palDotNet.palDialog.DialogCheckBox();
		this.textureSaving.SuspendLayout();
		base.SuspendLayout();
		this.textureSaving.Controls.Add(this.TexturePathUseExportPath);
		this.textureSaving.Controls.Add(this.overwriteTextures);
		this.textureSaving.Controls.Add(this.TextureOutputLabel);
		this.textureSaving.Controls.Add(this.TexturePath);
		this.textureSaving.Controls.Add(this.TextureFormatLabel);
		this.textureSaving.Controls.Add(this.TextureFormat);
		this.textureSaving.Controls.Add(this.SaveSourceTextures);
		this.textureSaving.Controls.Add(this.SaveEmbeddedTextures);
		this.textureSaving.Dock = System.Windows.Forms.DockStyle.Fill;
		this.textureSaving.Location = new System.Drawing.Point(0, 0);
		this.textureSaving.Name = "textureSaving";
		this.textureSaving.Size = new System.Drawing.Size(360, 154);
		this.textureSaving.TabIndex = 22;
		this.textureSaving.TabStop = false;
		this.textureSaving.Text = "Texture saving";
		this.TexturePathUseExportPath.AutoSize = true;
		this.TexturePathUseExportPath.Checked = true;
		this.TexturePathUseExportPath.CheckState = System.Windows.Forms.CheckState.Checked;
		this.TexturePathUseExportPath.Location = new System.Drawing.Point(25, 75);
		this.TexturePathUseExportPath.Name = "TexturePathUseExportPath";
		this.TexturePathUseExportPath.SaveToConfig = true;
		this.TexturePathUseExportPath.Size = new System.Drawing.Size(101, 17);
		this.TexturePathUseExportPath.TabIndex = 3;
		this.TexturePathUseExportPath.Text = "Use export path";
		this.TexturePathUseExportPath.UseVisualStyleBackColor = true;
		this.TexturePathUseExportPath.CheckedChanged += new System.EventHandler(TexturePathUseExportPath_CheckedChanged);
		this.overwriteTextures.AutoSize = true;
		this.overwriteTextures.Location = new System.Drawing.Point(6, 131);
		this.overwriteTextures.Name = "overwriteTextures";
		this.overwriteTextures.Size = new System.Drawing.Size(130, 17);
		this.overwriteTextures.TabIndex = 9;
		this.overwriteTextures.Text = "Overwrite existing files";
		this.overwriteTextures.UseVisualStyleBackColor = true;
		this.overwriteTextures.Visible = false;
		this.TextureOutputLabel.AutoSize = true;
		this.TextureOutputLabel.Location = new System.Drawing.Point(6, 59);
		this.TextureOutputLabel.Name = "TextureOutputLabel";
		this.TextureOutputLabel.SaveToConfig = false;
		this.TextureOutputLabel.Size = new System.Drawing.Size(71, 13);
		this.TextureOutputLabel.TabIndex = 8;
		this.TextureOutputLabel.Text = "Output folder:";
		this.TexturePath.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TexturePath.BackColor = System.Drawing.Color.Transparent;
		this.TexturePath.BrowseType = palDotNet.palDialog.DialogPathLoader.EnumBrowseType.eDirectories;
		this.TexturePath.CheckFileExists = false;
		this.TexturePath.Description = null;
		this.TexturePath.Filter = null;
		this.TexturePath.Location = new System.Drawing.Point(21, 93);
		this.TexturePath.Name = "TexturePath";
		this.TexturePath.Path = "";
		this.TexturePath.SaveToConfig = true;
		this.TexturePath.Size = new System.Drawing.Size(333, 24);
		this.TexturePath.TabIndex = 4;
		this.TextureFormatLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TextureFormatLabel.AutoSize = true;
		this.TextureFormatLabel.Location = new System.Drawing.Point(274, 20);
		this.TextureFormatLabel.Name = "TextureFormatLabel";
		this.TextureFormatLabel.SaveToConfig = false;
		this.TextureFormatLabel.Size = new System.Drawing.Size(78, 13);
		this.TextureFormatLabel.TabIndex = 4;
		this.TextureFormatLabel.Text = "Texture format:";
		this.TextureFormat.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.TextureFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.TextureFormat.FormattingEnabled = true;
		this.TextureFormat.Location = new System.Drawing.Point(277, 40);
		this.TextureFormat.Name = "TextureFormat";
		this.TextureFormat.SaveToConfig = true;
		this.TextureFormat.Size = new System.Drawing.Size(77, 21);
		this.TextureFormat.TabIndex = 2;
		this.SaveSourceTextures.AutoSize = true;
		this.SaveSourceTextures.Location = new System.Drawing.Point(6, 39);
		this.SaveSourceTextures.Name = "SaveSourceTextures";
		this.SaveSourceTextures.SaveToConfig = true;
		this.SaveSourceTextures.Size = new System.Drawing.Size(126, 17);
		this.SaveSourceTextures.TabIndex = 1;
		this.SaveSourceTextures.Text = "Save source textures";
		this.SaveSourceTextures.UseVisualStyleBackColor = true;
		this.SaveSourceTextures.CheckedChanged += new System.EventHandler(SaveSourceTextures_CheckedChanged);
		this.SaveEmbeddedTextures.AutoSize = true;
		this.SaveEmbeddedTextures.Checked = true;
		this.SaveEmbeddedTextures.CheckState = System.Windows.Forms.CheckState.Checked;
		this.SaveEmbeddedTextures.Location = new System.Drawing.Point(6, 16);
		this.SaveEmbeddedTextures.Name = "SaveEmbeddedTextures";
		this.SaveEmbeddedTextures.SaveToConfig = true;
		this.SaveEmbeddedTextures.Size = new System.Drawing.Size(144, 17);
		this.SaveEmbeddedTextures.TabIndex = 0;
		this.SaveEmbeddedTextures.Text = "Save embedded textures";
		this.SaveEmbeddedTextures.UseVisualStyleBackColor = true;
		this.SaveEmbeddedTextures.CheckedChanged += new System.EventHandler(SaveEmbeddedTextures_CheckedChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.textureSaving);
		base.Name = "ExportTextureSaving";
		base.Size = new System.Drawing.Size(360, 154);
		this.textureSaving.ResumeLayout(false);
		this.textureSaving.PerformLayout();
		base.ResumeLayout(false);
	}

	public ExportTextureSaving()
	{
		InitializeComponent();
		SetTextureFormatEnable();
	}

	private void SetTextureFormatEnable()
	{
		bool texturesWillBeSaved = TexturesWillBeSaved;
		TextureFormat.Enabled = texturesWillBeSaved;
		TextureFormatLabel.Enabled = texturesWillBeSaved;
		TextureOutputLabel.Enabled = texturesWillBeSaved;
		TexturePath.Enabled = texturesWillBeSaved && !TexturePathUseExportPath.Checked;
	}

	private void SaveEmbeddedTextures_CheckedChanged(object sender, EventArgs e)
	{
		SetTextureFormatEnable();
	}

	private void SaveSourceTextures_CheckedChanged(object sender, EventArgs e)
	{
		SetTextureFormatEnable();
	}

	private void TexturePathUseExportPath_CheckedChanged(object sender, EventArgs e)
	{
		SetTextureFormatEnable();
	}
}
