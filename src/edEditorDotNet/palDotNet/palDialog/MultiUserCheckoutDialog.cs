using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace palDotNet.palDialog;

[CPPCodeGeneration(OutputFile = "..\\edEditor\\edMultiUser\\MultiUserCheckoutDialog")]
[DialogBoxCreator]
public class MultiUserCheckoutDialog : DialogBoxBase
{
	private IContainer components;

	private DialogComboBox librarySelectionBox;

	private DialogLabel dialogLabel1;

	private DialogLabel dialogLabel2;

	private DialogComboBox terrainSelectionBox;

	private DialogTextBox newLibraryName;

	private DialogLabel dialogLabel3;

	private DialogLabel dialogLabel4;

	private DialogTextBox newTerrainName;

	private DialogCheckBox createNewLibraryToggle;

	private DialogCheckBox createNewTerrainToggle;

	private Button cancelBtn;

	private Button okButton;

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
		this.librarySelectionBox = new palDotNet.palDialog.DialogComboBox();
		this.dialogLabel1 = new palDotNet.palDialog.DialogLabel();
		this.dialogLabel2 = new palDotNet.palDialog.DialogLabel();
		this.terrainSelectionBox = new palDotNet.palDialog.DialogComboBox();
		this.newLibraryName = new palDotNet.palDialog.DialogTextBox();
		this.dialogLabel3 = new palDotNet.palDialog.DialogLabel();
		this.dialogLabel4 = new palDotNet.palDialog.DialogLabel();
		this.newTerrainName = new palDotNet.palDialog.DialogTextBox();
		this.createNewLibraryToggle = new palDotNet.palDialog.DialogCheckBox();
		this.createNewTerrainToggle = new palDotNet.palDialog.DialogCheckBox();
		this.cancelBtn = new System.Windows.Forms.Button();
		this.okButton = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.librarySelectionBox.FormattingEnabled = true;
		this.librarySelectionBox.Location = new System.Drawing.Point(91, 24);
		this.librarySelectionBox.Name = "librarySelectionBox";
		this.librarySelectionBox.Size = new System.Drawing.Size(132, 21);
		this.librarySelectionBox.TabIndex = 1;
		this.dialogLabel1.AutoSize = true;
		this.dialogLabel1.Location = new System.Drawing.Point(12, 27);
		this.dialogLabel1.Name = "dialogLabel1";
		this.dialogLabel1.Size = new System.Drawing.Size(75, 13);
		this.dialogLabel1.TabIndex = 3;
		this.dialogLabel1.Text = "Project: (.paw)";
		this.dialogLabel2.AutoSize = true;
		this.dialogLabel2.Location = new System.Drawing.Point(243, 27);
		this.dialogLabel2.Name = "dialogLabel2";
		this.dialogLabel2.Size = new System.Drawing.Size(84, 13);
		this.dialogLabel2.TabIndex = 5;
		this.dialogLabel2.Text = "Terrain: (.terrain)";
		this.terrainSelectionBox.FormattingEnabled = true;
		this.terrainSelectionBox.Location = new System.Drawing.Point(333, 24);
		this.terrainSelectionBox.Name = "terrainSelectionBox";
		this.terrainSelectionBox.Size = new System.Drawing.Size(132, 21);
		this.terrainSelectionBox.TabIndex = 4;
		this.newLibraryName.Enabled = false;
		this.newLibraryName.Location = new System.Drawing.Point(135, 79);
		this.newLibraryName.Name = "newLibraryName";
		this.newLibraryName.Size = new System.Drawing.Size(132, 20);
		this.newLibraryName.TabIndex = 6;
		this.newLibraryName.TextChanged += new System.EventHandler(newLibraryName_TextChanged);
		this.dialogLabel3.AutoSize = true;
		this.dialogLabel3.Location = new System.Drawing.Point(57, 82);
		this.dialogLabel3.Name = "dialogLabel3";
		this.dialogLabel3.Size = new System.Drawing.Size(70, 13);
		this.dialogLabel3.TabIndex = 7;
		this.dialogLabel3.Text = "Library name:";
		this.dialogLabel4.AutoSize = true;
		this.dialogLabel4.Location = new System.Drawing.Point(57, 131);
		this.dialogLabel4.Name = "dialogLabel4";
		this.dialogLabel4.Size = new System.Drawing.Size(72, 13);
		this.dialogLabel4.TabIndex = 9;
		this.dialogLabel4.Text = "Terrain name:";
		this.newTerrainName.Enabled = false;
		this.newTerrainName.Location = new System.Drawing.Point(135, 128);
		this.newTerrainName.Name = "newTerrainName";
		this.newTerrainName.Size = new System.Drawing.Size(132, 20);
		this.newTerrainName.TabIndex = 8;
		this.newTerrainName.TextChanged += new System.EventHandler(newTerrainName_TextChanged);
		this.createNewLibraryToggle.AutoSize = true;
		this.createNewLibraryToggle.Location = new System.Drawing.Point(13, 62);
		this.createNewLibraryToggle.Name = "createNewLibraryToggle";
		this.createNewLibraryToggle.Size = new System.Drawing.Size(115, 17);
		this.createNewLibraryToggle.TabIndex = 10;
		this.createNewLibraryToggle.Text = "Create new project";
		this.createNewLibraryToggle.UseVisualStyleBackColor = true;
		this.createNewLibraryToggle.CheckedChanged += new System.EventHandler(createNewLibraryToggle_CheckedChanged);
		this.createNewTerrainToggle.AutoSize = true;
		this.createNewTerrainToggle.Location = new System.Drawing.Point(12, 111);
		this.createNewTerrainToggle.Name = "createNewTerrainToggle";
		this.createNewTerrainToggle.Size = new System.Drawing.Size(112, 17);
		this.createNewTerrainToggle.TabIndex = 11;
		this.createNewTerrainToggle.Text = "Create new terrain";
		this.createNewTerrainToggle.UseVisualStyleBackColor = true;
		this.createNewTerrainToggle.CheckedChanged += new System.EventHandler(createNewTerrainToggle_CheckedChanged);
		this.cancelBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cancelBtn.Location = new System.Drawing.Point(392, 161);
		this.cancelBtn.Name = "cancelBtn";
		this.cancelBtn.Size = new System.Drawing.Size(75, 23);
		this.cancelBtn.TabIndex = 13;
		this.cancelBtn.Text = "Cancel";
		this.cancelBtn.UseVisualStyleBackColor = true;
		this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.okButton.Location = new System.Drawing.Point(311, 161);
		this.okButton.Name = "okButton";
		this.okButton.Size = new System.Drawing.Size(75, 23);
		this.okButton.TabIndex = 12;
		this.okButton.Text = "OK";
		this.okButton.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(481, 196);
		base.Controls.Add(this.cancelBtn);
		base.Controls.Add(this.okButton);
		base.Controls.Add(this.createNewTerrainToggle);
		base.Controls.Add(this.createNewLibraryToggle);
		base.Controls.Add(this.dialogLabel4);
		base.Controls.Add(this.newTerrainName);
		base.Controls.Add(this.dialogLabel3);
		base.Controls.Add(this.newLibraryName);
		base.Controls.Add(this.dialogLabel2);
		base.Controls.Add(this.terrainSelectionBox);
		base.Controls.Add(this.dialogLabel1);
		base.Controls.Add(this.librarySelectionBox);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "MultiUserCheckoutDialog";
		this.Text = "MultiUserCheckoutDialog";
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public MultiUserCheckoutDialog()
	{
		InitializeComponent();
		librarySelectionBox.Enabled = !createNewLibraryToggle.Checked;
		newLibraryName.Enabled = createNewLibraryToggle.Checked;
		librarySelectionBox.Enabled = !createNewLibraryToggle.Checked;
		newLibraryName.Enabled = createNewLibraryToggle.Checked;
		SyncOkButton();
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		SyncOkButton();
	}

	private void createNewLibraryToggle_CheckedChanged(object sender, EventArgs e)
	{
		librarySelectionBox.Enabled = !createNewLibraryToggle.Checked;
		newLibraryName.Enabled = createNewLibraryToggle.Checked;
		SyncOkButton();
	}

	private void createNewTerrainToggle_CheckedChanged(object sender, EventArgs e)
	{
		terrainSelectionBox.Enabled = !createNewTerrainToggle.Checked;
		newTerrainName.Enabled = createNewTerrainToggle.Checked;
		SyncOkButton();
	}

	private void SyncOkButton()
	{
		if (createNewLibraryToggle.Checked && newLibraryName.Text.Length == 0)
		{
			okButton.Enabled = false;
		}
		else if (createNewTerrainToggle.Checked && newTerrainName.Text.Length == 0)
		{
			okButton.Enabled = false;
		}
		else if (!createNewLibraryToggle.Checked && librarySelectionBox.DialogValue.ToString().Length == 0)
		{
			okButton.Enabled = false;
		}
		else if (!createNewTerrainToggle.Checked && terrainSelectionBox.DialogValue.ToString().Length == 0)
		{
			okButton.Enabled = false;
		}
		else
		{
			okButton.Enabled = true;
		}
	}

	private void newLibraryName_TextChanged(object sender, EventArgs e)
	{
		SyncOkButton();
	}

	private void newTerrainName_TextChanged(object sender, EventArgs e)
	{
		SyncOkButton();
	}
}
