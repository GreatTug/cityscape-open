using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet.ExportDialogs;

[DialogBoxCreator]
public class ExportMetaData : DialogBoxBase
{
	private IContainer components;

	private GroupBox groupBox1;

	private Button OKButton;

	private Button buttonCancel;

	private DialogPathLoader ExportPath;

	private ListView TemplateList;

	private ColumnHeader TemplateListColumn;

	public ListView TemplateListBox => TemplateList;

	public string ExportPathString => ExportPath.TextBox.Text;

	public ExportMetaData()
	{
		InitializeComponent();
		TemplateListColumn.Width = TemplateList.ClientSize.Width;
	}

	private void OKButton_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
		Close();
	}

	public void Initialize()
	{
		OKButton.Enabled = false;
		AllowSubmitDialog = false;
	}

	private void TemplateList_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (TemplateList.SelectedItems.Count == 0)
		{
			OKButton.Enabled = false;
			AllowSubmitDialog = false;
		}
		else
		{
			OKButton.Enabled = true;
			AllowSubmitDialog = true;
		}
	}

	private void TemplateList_ClientSizeChanged(object sender, EventArgs e)
	{
		TemplateListColumn.Width = TemplateList.ClientSize.Width;
	}

	private void TemplateList_SizeChanged(object sender, EventArgs e)
	{
		TemplateListColumn.Width = 0;
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
		this.TemplateList = new System.Windows.Forms.ListView();
		this.TemplateListColumn = new System.Windows.Forms.ColumnHeader();
		this.OKButton = new System.Windows.Forms.Button();
		this.buttonCancel = new System.Windows.Forms.Button();
		this.ExportPath = new palDotNet.palDialog.DialogPathLoader();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.TemplateList);
		this.groupBox1.Location = new System.Drawing.Point(13, 13);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(376, 321);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Meta data templates";
		this.TemplateList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { this.TemplateListColumn });
		this.TemplateList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
		this.TemplateList.HideSelection = false;
		this.TemplateList.LabelWrap = false;
		this.TemplateList.Location = new System.Drawing.Point(6, 19);
		this.TemplateList.Name = "TemplateList";
		this.TemplateList.Size = new System.Drawing.Size(364, 296);
		this.TemplateList.Sorting = System.Windows.Forms.SortOrder.Ascending;
		this.TemplateList.TabIndex = 0;
		this.TemplateList.UseCompatibleStateImageBehavior = false;
		this.TemplateList.View = System.Windows.Forms.View.Details;
		this.TemplateList.ClientSizeChanged += new System.EventHandler(TemplateList_ClientSizeChanged);
		this.TemplateList.SelectedIndexChanged += new System.EventHandler(TemplateList_SelectedIndexChanged);
		this.TemplateList.SizeChanged += new System.EventHandler(TemplateList_SizeChanged);
		this.OKButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.OKButton.Enabled = false;
		this.OKButton.Location = new System.Drawing.Point(233, 370);
		this.OKButton.Name = "OKButton";
		this.OKButton.Size = new System.Drawing.Size(75, 23);
		this.OKButton.TabIndex = 1;
		this.OKButton.Text = "OK";
		this.OKButton.UseVisualStyleBackColor = true;
		this.OKButton.Click += new System.EventHandler(OKButton_Click);
		this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.buttonCancel.Location = new System.Drawing.Point(314, 370);
		this.buttonCancel.Name = "buttonCancel";
		this.buttonCancel.Size = new System.Drawing.Size(75, 23);
		this.buttonCancel.TabIndex = 2;
		this.buttonCancel.Text = "Cancel";
		this.buttonCancel.UseVisualStyleBackColor = true;
		this.ExportPath.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ExportPath.BackColor = System.Drawing.Color.Transparent;
		this.ExportPath.BrowseType = palDotNet.palDialog.DialogPathLoader.EnumBrowseType.eSave;
		this.ExportPath.CheckFileExists = false;
		this.ExportPath.Description = null;
		this.ExportPath.Filter = "Comma Separateed Values (*.csv)|*.csv";
		this.ExportPath.Location = new System.Drawing.Point(12, 340);
		this.ExportPath.Name = "ExportPath";
		this.ExportPath.Path = "";
		this.ExportPath.Size = new System.Drawing.Size(377, 24);
		this.ExportPath.TabIndex = 3;
		base.AcceptButton = this.OKButton;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(401, 405);
		base.Controls.Add(this.ExportPath);
		base.Controls.Add(this.buttonCancel);
		base.Controls.Add(this.OKButton);
		base.Controls.Add(this.groupBox1);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.Name = "ExportMetaData";
		this.Text = "Export Meta Data";
		this.groupBox1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
