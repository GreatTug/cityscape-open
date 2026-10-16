using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

public class ExportTextureTypes : UserControl, IDialogContainer
{
	private IContainer components;

	private GroupBox textureTypes;

	private DialogCheckBox UseSpecularDetailMaps;

	private DialogCheckBox UseSpecularMaps;

	private DialogCheckBox UseNormalMaps;

	private DialogCheckBox UseDiffuseDetailMaps;

	private DialogCheckBox UseDiffuseMaps;

	private SplitContainer splitContainer1;

	private DialogCheckBox UseTerrainBlendMaps;

	public DialogCheckBox DiffuseCheckBox => UseDiffuseMaps;

	public DialogCheckBox DiffuseDetailCheckBox => UseDiffuseDetailMaps;

	public DialogCheckBox NormalCheckBox => UseNormalMaps;

	public DialogCheckBox SpecularCheckBox => UseSpecularMaps;

	public DialogCheckBox SpecularDetailCheckBox => UseSpecularDetailMaps;

	public DialogCheckBox TerrainBlendCheckBox => UseTerrainBlendMaps;

	public ExportTextureTypes()
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
		this.textureTypes = new System.Windows.Forms.GroupBox();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.UseNormalMaps = new palDotNet.palDialog.DialogCheckBox();
		this.UseDiffuseDetailMaps = new palDotNet.palDialog.DialogCheckBox();
		this.UseDiffuseMaps = new palDotNet.palDialog.DialogCheckBox();
		this.UseTerrainBlendMaps = new palDotNet.palDialog.DialogCheckBox();
		this.UseSpecularDetailMaps = new palDotNet.palDialog.DialogCheckBox();
		this.UseSpecularMaps = new palDotNet.palDialog.DialogCheckBox();
		this.textureTypes.SuspendLayout();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		base.SuspendLayout();
		this.textureTypes.Controls.Add(this.splitContainer1);
		this.textureTypes.Dock = System.Windows.Forms.DockStyle.Fill;
		this.textureTypes.Location = new System.Drawing.Point(0, 0);
		this.textureTypes.Name = "textureTypes";
		this.textureTypes.Size = new System.Drawing.Size(300, 200);
		this.textureTypes.TabIndex = 22;
		this.textureTypes.TabStop = false;
		this.textureTypes.Text = "Texture export options";
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.Location = new System.Drawing.Point(3, 16);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.UseNormalMaps);
		this.splitContainer1.Panel1.Controls.Add(this.UseDiffuseDetailMaps);
		this.splitContainer1.Panel1.Controls.Add(this.UseDiffuseMaps);
		this.splitContainer1.Panel2.Controls.Add(this.UseTerrainBlendMaps);
		this.splitContainer1.Panel2.Controls.Add(this.UseSpecularDetailMaps);
		this.splitContainer1.Panel2.Controls.Add(this.UseSpecularMaps);
		this.splitContainer1.Size = new System.Drawing.Size(294, 181);
		this.splitContainer1.SplitterDistance = 147;
		this.splitContainer1.TabIndex = 10;
		this.UseNormalMaps.AutoSize = true;
		this.UseNormalMaps.Checked = true;
		this.UseNormalMaps.CheckState = System.Windows.Forms.CheckState.Checked;
		this.UseNormalMaps.Location = new System.Drawing.Point(3, 49);
		this.UseNormalMaps.Name = "UseNormalMaps";
		this.UseNormalMaps.SaveToConfig = true;
		this.UseNormalMaps.Size = new System.Drawing.Size(59, 17);
		this.UseNormalMaps.TabIndex = 2;
		this.UseNormalMaps.Text = "Normal";
		this.UseNormalMaps.UseVisualStyleBackColor = true;
		this.UseDiffuseDetailMaps.AutoSize = true;
		this.UseDiffuseDetailMaps.Checked = true;
		this.UseDiffuseDetailMaps.CheckState = System.Windows.Forms.CheckState.Checked;
		this.UseDiffuseDetailMaps.Location = new System.Drawing.Point(3, 26);
		this.UseDiffuseDetailMaps.Name = "UseDiffuseDetailMaps";
		this.UseDiffuseDetailMaps.SaveToConfig = true;
		this.UseDiffuseDetailMaps.Size = new System.Drawing.Size(87, 17);
		this.UseDiffuseDetailMaps.TabIndex = 1;
		this.UseDiffuseDetailMaps.Text = "Diffuse detail";
		this.UseDiffuseDetailMaps.UseVisualStyleBackColor = true;
		this.UseDiffuseMaps.AutoSize = true;
		this.UseDiffuseMaps.Checked = true;
		this.UseDiffuseMaps.CheckState = System.Windows.Forms.CheckState.Checked;
		this.UseDiffuseMaps.Location = new System.Drawing.Point(3, 3);
		this.UseDiffuseMaps.Name = "UseDiffuseMaps";
		this.UseDiffuseMaps.SaveToConfig = true;
		this.UseDiffuseMaps.Size = new System.Drawing.Size(59, 17);
		this.UseDiffuseMaps.TabIndex = 0;
		this.UseDiffuseMaps.Text = "Diffuse";
		this.UseDiffuseMaps.UseVisualStyleBackColor = true;
		this.UseTerrainBlendMaps.AutoSize = true;
		this.UseTerrainBlendMaps.Location = new System.Drawing.Point(3, 49);
		this.UseTerrainBlendMaps.Name = "UseTerrainBlendMaps";
		this.UseTerrainBlendMaps.SaveToConfig = true;
		this.UseTerrainBlendMaps.Size = new System.Drawing.Size(88, 17);
		this.UseTerrainBlendMaps.TabIndex = 5;
		this.UseTerrainBlendMaps.Text = "Terrain blend";
		this.UseTerrainBlendMaps.UseVisualStyleBackColor = true;
		this.UseSpecularDetailMaps.AutoSize = true;
		this.UseSpecularDetailMaps.Checked = true;
		this.UseSpecularDetailMaps.CheckState = System.Windows.Forms.CheckState.Checked;
		this.UseSpecularDetailMaps.Location = new System.Drawing.Point(3, 26);
		this.UseSpecularDetailMaps.Name = "UseSpecularDetailMaps";
		this.UseSpecularDetailMaps.SaveToConfig = true;
		this.UseSpecularDetailMaps.Size = new System.Drawing.Size(96, 17);
		this.UseSpecularDetailMaps.TabIndex = 4;
		this.UseSpecularDetailMaps.Text = "Specular detail";
		this.UseSpecularDetailMaps.UseVisualStyleBackColor = true;
		this.UseSpecularDetailMaps.Visible = false;
		this.UseSpecularMaps.AutoSize = true;
		this.UseSpecularMaps.Checked = true;
		this.UseSpecularMaps.CheckState = System.Windows.Forms.CheckState.Checked;
		this.UseSpecularMaps.Location = new System.Drawing.Point(3, 3);
		this.UseSpecularMaps.Name = "UseSpecularMaps";
		this.UseSpecularMaps.SaveToConfig = true;
		this.UseSpecularMaps.Size = new System.Drawing.Size(68, 17);
		this.UseSpecularMaps.TabIndex = 3;
		this.UseSpecularMaps.Text = "Specular";
		this.UseSpecularMaps.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.textureTypes);
		base.Name = "ExportTextureTypes";
		base.Size = new System.Drawing.Size(300, 200);
		this.textureTypes.ResumeLayout(false);
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel1.PerformLayout();
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.Panel2.PerformLayout();
		this.splitContainer1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
