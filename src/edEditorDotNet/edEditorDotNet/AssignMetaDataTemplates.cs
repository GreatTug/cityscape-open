using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

[DialogBoxCreator]
public class AssignMetaDataTemplates : DialogBoxBase
{
	private IContainer components;

	private Button buttonOK;

	private Button buttonCancel;

	private SplitContainer splitContainer1;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private Button RemoveTemplates;

	private Button AddTemplates;

	private ListView AssignedTemplates;

	private ListView TemplateLibrary;

	private ColumnHeader AssignedTemplatesColumn;

	private ColumnHeader TemplateLibraryColumn;

	public Button RemoveTemplatesButton => RemoveTemplates;

	public Button AddTemplatesButton => AddTemplates;

	public ListView AssignedTemplatesList => AssignedTemplates;

	public ListView TemplateLibraryList => TemplateLibrary;

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
		this.buttonOK = new System.Windows.Forms.Button();
		this.buttonCancel = new System.Windows.Forms.Button();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.AssignedTemplates = new System.Windows.Forms.ListView();
		this.AssignedTemplatesColumn = new System.Windows.Forms.ColumnHeader();
		this.RemoveTemplates = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.TemplateLibrary = new System.Windows.Forms.ListView();
		this.TemplateLibraryColumn = new System.Windows.Forms.ColumnHeader();
		this.AddTemplates = new System.Windows.Forms.Button();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.buttonOK.Location = new System.Drawing.Point(426, 426);
		this.buttonOK.Name = "buttonOK";
		this.buttonOK.Size = new System.Drawing.Size(75, 23);
		this.buttonOK.TabIndex = 0;
		this.buttonOK.Text = "OK";
		this.buttonOK.UseVisualStyleBackColor = true;
		this.buttonOK.Click += new System.EventHandler(buttonOK_Click);
		this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.buttonCancel.Location = new System.Drawing.Point(507, 426);
		this.buttonCancel.Name = "buttonCancel";
		this.buttonCancel.Size = new System.Drawing.Size(75, 23);
		this.buttonCancel.TabIndex = 1;
		this.buttonCancel.Text = "Cancel";
		this.buttonCancel.UseVisualStyleBackColor = true;
		this.splitContainer1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.splitContainer1.Location = new System.Drawing.Point(12, 12);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
		this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
		this.splitContainer1.Size = new System.Drawing.Size(570, 408);
		this.splitContainer1.SplitterDistance = 285;
		this.splitContainer1.TabIndex = 2;
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.AssignedTemplates);
		this.groupBox1.Controls.Add(this.RemoveTemplates);
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(282, 408);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Assigned templates";
		this.AssignedTemplates.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.AssignedTemplates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { this.AssignedTemplatesColumn });
		this.AssignedTemplates.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
		this.AssignedTemplates.LabelWrap = false;
		this.AssignedTemplates.Location = new System.Drawing.Point(6, 19);
		this.AssignedTemplates.Name = "AssignedTemplates";
		this.AssignedTemplates.Size = new System.Drawing.Size(270, 354);
		this.AssignedTemplates.Sorting = System.Windows.Forms.SortOrder.Ascending;
		this.AssignedTemplates.TabIndex = 4;
		this.AssignedTemplates.UseCompatibleStateImageBehavior = false;
		this.AssignedTemplates.View = System.Windows.Forms.View.Details;
		this.AssignedTemplates.ClientSizeChanged += new System.EventHandler(AssignedTemplates_ClientSizeChanged);
		this.AssignedTemplates.SelectedIndexChanged += new System.EventHandler(AssignedTemplates_SelectedIndexChanged);
		this.AssignedTemplates.SizeChanged += new System.EventHandler(AssignedTemplates_SizeChanged);
		this.RemoveTemplates.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.RemoveTemplates.Location = new System.Drawing.Point(201, 379);
		this.RemoveTemplates.Name = "RemoveTemplates";
		this.RemoveTemplates.Size = new System.Drawing.Size(75, 23);
		this.RemoveTemplates.TabIndex = 0;
		this.RemoveTemplates.Text = "Remove";
		this.RemoveTemplates.UseVisualStyleBackColor = true;
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.TemplateLibrary);
		this.groupBox2.Controls.Add(this.AddTemplates);
		this.groupBox2.Location = new System.Drawing.Point(-1, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(282, 408);
		this.groupBox2.TabIndex = 0;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Template library";
		this.TemplateLibrary.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TemplateLibrary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { this.TemplateLibraryColumn });
		this.TemplateLibrary.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
		this.TemplateLibrary.LabelWrap = false;
		this.TemplateLibrary.Location = new System.Drawing.Point(6, 19);
		this.TemplateLibrary.Name = "TemplateLibrary";
		this.TemplateLibrary.Size = new System.Drawing.Size(270, 354);
		this.TemplateLibrary.Sorting = System.Windows.Forms.SortOrder.Ascending;
		this.TemplateLibrary.TabIndex = 5;
		this.TemplateLibrary.UseCompatibleStateImageBehavior = false;
		this.TemplateLibrary.View = System.Windows.Forms.View.Details;
		this.TemplateLibrary.ItemActivate += new System.EventHandler(TemplateLibrary_ItemActivate);
		this.TemplateLibrary.ClientSizeChanged += new System.EventHandler(TemplateLibrary_ClientSizeChanged);
		this.TemplateLibrary.SelectedIndexChanged += new System.EventHandler(TemplateLibrary_SelectedIndexChanged);
		this.TemplateLibrary.SizeChanged += new System.EventHandler(TemplateLibrary_SizeChanged);
		this.AddTemplates.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.AddTemplates.Location = new System.Drawing.Point(201, 379);
		this.AddTemplates.Name = "AddTemplates";
		this.AddTemplates.Size = new System.Drawing.Size(75, 23);
		this.AddTemplates.TabIndex = 0;
		this.AddTemplates.Text = "Add";
		this.AddTemplates.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.buttonCancel;
		base.ClientSize = new System.Drawing.Size(594, 461);
		base.Controls.Add(this.splitContainer1);
		base.Controls.Add(this.buttonCancel);
		base.Controls.Add(this.buttonOK);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.Name = "AssignMetaDataTemplates";
		this.Text = "Assign Meta Data Templates";
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public AssignMetaDataTemplates()
	{
		InitializeComponent();
		AssignedTemplatesColumn.Width = AssignedTemplates.ClientSize.Width;
		TemplateLibraryColumn.Width = TemplateLibrary.ClientSize.Width;
	}

	private void buttonOK_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
		Close();
	}

	private void AssignedTemplates_SelectedIndexChanged(object sender, EventArgs e)
	{
		int count = AssignedTemplates.SelectedIndices.Count;
		if (count <= 0)
		{
			RemoveTemplates.Enabled = false;
		}
		else
		{
			RemoveTemplates.Enabled = true;
		}
	}

	private void TemplateLibrary_SelectedIndexChanged(object sender, EventArgs e)
	{
		int count = TemplateLibrary.SelectedIndices.Count;
		if (count <= 0)
		{
			AddTemplates.Enabled = false;
		}
		else
		{
			AddTemplates.Enabled = true;
		}
	}

	private void TemplateLibrary_ItemActivate(object sender, EventArgs e)
	{
		AddTemplates.PerformClick();
	}

	private void AssignedTemplates_ClientSizeChanged(object sender, EventArgs e)
	{
		AssignedTemplatesColumn.Width = AssignedTemplates.ClientSize.Width;
	}

	private void TemplateLibrary_ClientSizeChanged(object sender, EventArgs e)
	{
		TemplateLibraryColumn.Width = TemplateLibrary.ClientSize.Width;
	}

	private void AssignedTemplates_SizeChanged(object sender, EventArgs e)
	{
		AssignedTemplatesColumn.Width = 0;
	}

	private void TemplateLibrary_SizeChanged(object sender, EventArgs e)
	{
		TemplateLibraryColumn.Width = 0;
	}
}
