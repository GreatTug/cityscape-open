using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

public class ExportFilmbox : ExportCustomBase
{
	private IContainer components;

	private GroupBox groupBox1;

	private DialogCheckBox exportFilmboxEmbedTextures;

	public override bool ShouldForceEmbeddedProps => true;

	public override string ExporterTypeName => "Filmbox";

	public CheckBox EmbedTexturesCheckBox => exportFilmboxEmbedTextures;

	public ExportFilmbox()
	{
		InitializeComponent();
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.exportFilmboxEmbedTextures = new palDotNet.palDialog.DialogCheckBox();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.exportFilmboxEmbedTextures);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(499, 385);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Filmbox options";
		this.exportFilmboxEmbedTextures.AutoSize = true;
		this.exportFilmboxEmbedTextures.Location = new System.Drawing.Point(6, 19);
		this.exportFilmboxEmbedTextures.Name = "exportFilmboxEmbedTextures";
		this.exportFilmboxEmbedTextures.Size = new System.Drawing.Size(99, 17);
		this.exportFilmboxEmbedTextures.TabIndex = 3;
		this.exportFilmboxEmbedTextures.Text = "Embed textures";
		this.exportFilmboxEmbedTextures.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.groupBox1);
		base.Name = "ExportFilmbox";
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
