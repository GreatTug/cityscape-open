using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace palDotNet.palDialog;

[DialogBoxCreator]
[CPPCodeGeneration(OutputFile = "..\\edEditor\\edDialogs\\NewProjectDialog")]
public class NewProjectDialog : DialogBoxBase
{
	private IContainer components;

	private Button okButton;

	private Button cancelButton;

	private Label locationLabel;

	private Label nameLabel;

	private GroupBox ProjectGroupBox;

	private DialogPathLoader ProjectLocation;

	private DialogTextBox NameTextBox;

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
		this.cancelButton = new System.Windows.Forms.Button();
		this.locationLabel = new System.Windows.Forms.Label();
		this.nameLabel = new System.Windows.Forms.Label();
		this.ProjectGroupBox = new System.Windows.Forms.GroupBox();
		this.NameTextBox = new palDotNet.palDialog.DialogTextBox();
		this.ProjectLocation = new palDotNet.palDialog.DialogPathLoader();
		this.ProjectGroupBox.SuspendLayout();
		base.SuspendLayout();
		this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.okButton.Location = new System.Drawing.Point(326, 142);
		this.okButton.Name = "okButton";
		this.okButton.Size = new System.Drawing.Size(75, 23);
		this.okButton.TabIndex = 1;
		this.okButton.Text = "OK";
		this.okButton.UseVisualStyleBackColor = true;
		this.okButton.Click += new System.EventHandler(okButton_Click);
		this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cancelButton.Location = new System.Drawing.Point(407, 142);
		this.cancelButton.Name = "cancelButton";
		this.cancelButton.Size = new System.Drawing.Size(75, 23);
		this.cancelButton.TabIndex = 2;
		this.cancelButton.Text = "Cancel";
		this.cancelButton.UseVisualStyleBackColor = true;
		this.locationLabel.AutoSize = true;
		this.locationLabel.Location = new System.Drawing.Point(21, 65);
		this.locationLabel.Name = "locationLabel";
		this.locationLabel.Size = new System.Drawing.Size(51, 13);
		this.locationLabel.TabIndex = 2;
		this.locationLabel.Text = "&Location:";
		this.nameLabel.AutoSize = true;
		this.nameLabel.Location = new System.Drawing.Point(21, 33);
		this.nameLabel.Name = "nameLabel";
		this.nameLabel.Size = new System.Drawing.Size(38, 13);
		this.nameLabel.TabIndex = 0;
		this.nameLabel.Text = "&Name:";
		this.ProjectGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ProjectGroupBox.Controls.Add(this.NameTextBox);
		this.ProjectGroupBox.Controls.Add(this.ProjectLocation);
		this.ProjectGroupBox.Controls.Add(this.locationLabel);
		this.ProjectGroupBox.Controls.Add(this.nameLabel);
		this.ProjectGroupBox.Location = new System.Drawing.Point(12, 12);
		this.ProjectGroupBox.Name = "ProjectGroupBox";
		this.ProjectGroupBox.Size = new System.Drawing.Size(470, 105);
		this.ProjectGroupBox.TabIndex = 0;
		this.ProjectGroupBox.TabStop = false;
		this.ProjectGroupBox.Text = "Project";
		this.NameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.NameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.NameTextBox.Location = new System.Drawing.Point(96, 30);
		this.NameTextBox.Name = "NameTextBox";
		this.NameTextBox.SaveToConfig = true;
		this.NameTextBox.Size = new System.Drawing.Size(305, 20);
		this.NameTextBox.TabIndex = 1;
		this.NameTextBox.TextChanged += new System.EventHandler(NameTextBox_TextChanged);
		this.ProjectLocation.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ProjectLocation.BackColor = System.Drawing.Color.Transparent;
		this.ProjectLocation.BrowseType = palDotNet.palDialog.DialogPathLoader.EnumBrowseType.eDirectories;
		this.ProjectLocation.CheckFileExists = false;
		this.ProjectLocation.Description = "Select a project location";
		this.ProjectLocation.Filter = null;
		this.ProjectLocation.Location = new System.Drawing.Point(96, 60);
		this.ProjectLocation.Name = "ProjectLocation";
		this.ProjectLocation.Path = "";
		this.ProjectLocation.SaveToConfig = true;
		this.ProjectLocation.Size = new System.Drawing.Size(345, 24);
		this.ProjectLocation.TabIndex = 3;
		this.ProjectLocation.PathChanged += new System.EventHandler(ProjectLocation_PathChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cancelButton;
		base.ClientSize = new System.Drawing.Size(494, 177);
		base.Controls.Add(this.ProjectGroupBox);
		base.Controls.Add(this.cancelButton);
		base.Controls.Add(this.okButton);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "NewProjectDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "New Project";
		this.ProjectGroupBox.ResumeLayout(false);
		this.ProjectGroupBox.PerformLayout();
		base.ResumeLayout(false);
	}

	public NewProjectDialog()
	{
		InitializeComponent();
		ProjectLocation.LoadedFromSettings = true;
		SyncOkButton();
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		NameTextBox.Focus();
	}

	private void NameTextBox_TextChanged(object sender, EventArgs e)
	{
		SyncOkButton();
	}

	private void ProjectLocation_PathChanged(object sender, EventArgs e)
	{
		SyncOkButton();
	}

	private bool IsValidInput()
	{
		if (NameTextBox.Text.Length == 0 || ProjectLocation.Path.Length == 0)
		{
			return false;
		}
		return true;
	}

	private void SyncOkButton()
	{
		AllowSubmitDialog = IsValidInput();
		okButton.Enabled = AllowSubmitDialog;
	}

	private void okButton_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
	}
}
