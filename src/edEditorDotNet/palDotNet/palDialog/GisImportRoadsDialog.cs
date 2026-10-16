using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace palDotNet.palDialog;

[CPPCodeGeneration(OutputFile = "..\\edEditor\\edGISBorkDialogs\\GisImportRoadsDialog", Win32PCOnly = true)]
[DialogBoxCreator]
public class GisImportRoadsDialog : DialogBoxBase
{
	private string CustomizeTemplateDialogGuidStr;

	private bool ValidRoadsFilePath;

	private IContainer components;

	private Button CancelBtn;

	private Button OKBtn;

	private TabPage ShapefileTab;

	private Button ModifyCustomTemplateButton;

	private GroupBox RoadsFileGroup;

	private DialogPathLoader RoadsFilePath;

	private DialogLabel RoadFileInfoLabel;

	private TabControl TabControl;

	private TabPage RoadOptionsTab;

	private RoadGenerationOptionsPanel RoadGenerationOptionsPanel;

	private GroupBox RoadAttributesGroupBox;

	private DialogRadioButton UseCustomAttributesRadioButton;

	private DialogLabel AttributeMappingTemplateInfoLabel1;

	private DialogRadioButton UseDefaultAttributesRadioButton;

	private DialogCheckBox HiddenWasCustomizedCheckBox;

	private DialogMessagePanel mMessagePanel;

	public DialogPathLoader RoadsPath => RoadsFilePath;

	public Button ModifyCustomTemplateBtn => ModifyCustomTemplateButton;

	public DialogMessagePanel MessagePanel => mMessagePanel;

	public event RoadsDialogOnShownHandler RoadsDialogOnShown;

	public GisImportRoadsDialog()
	{
		InitializeComponent();
		InvalidateRoadsFilePath();
	}

	private void GisImportRoadsDialog_Load(object sender, EventArgs e)
	{
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		if (RoadsDialogOnShown != null)
		{
			RoadsDialogOnShown(this, e);
		}
		UseDefaultAttributesRadioButton.Checked = true;
		HiddenWasCustomizedCheckBox.Checked = false;
		SyncRoadAttributesGroupBox();
		SyncOkButton();
	}

	public void SetCustomizeTemplateDialogStr(string dialogGuidStr)
	{
		CustomizeTemplateDialogGuidStr = dialogGuidStr;
	}

	public void ResetCustomDialogSettings()
	{
		if (!string.IsNullOrEmpty(CustomizeTemplateDialogGuidStr))
		{
			Guid guid = new Guid(CustomizeTemplateDialogGuidStr);
			DialogBoxBase dialogBoxBase = DialogBoxBase.FindByGuid(guid);
			if (dialogBoxBase is GisImportRoadsCustomDialog gisImportRoadsCustomDialog)
			{
				gisImportRoadsCustomDialog.InitAttributeTabs();
				gisImportRoadsCustomDialog.ResetAttributeTabs();
			}
		}
	}

	public void InvalidateRoadsFilePath()
	{
		ValidRoadsFilePath = false;
		HiddenWasCustomizedCheckBox.Checked = false;
		SyncRoadAttributesGroupBox();
		SyncOkButton();
	}

	public void ValidateRoadsFilePath()
	{
		ValidRoadsFilePath = true;
		SyncRoadAttributesGroupBox();
		SyncOkButton();
	}

	private void UseDefaultAttributesRadioButton_CheckedChanged(object sender, EventArgs e)
	{
		SyncRoadAttributesGroupBox();
	}

	private void UseCustomAttributesRadioButton_CheckedChanged(object sender, EventArgs e)
	{
		SyncRoadAttributesGroupBox();
	}

	private void SyncRoadAttributesGroupBox()
	{
		RoadAttributesGroupBox.Enabled = ValidRoadsFilePath;
		ModifyCustomTemplateButton.Enabled = UseCustomAttributesRadioButton.Checked;
	}

	private void ModifyCustomTemplateButton_Click(object sender, EventArgs e)
	{
		Guid guid = new Guid(CustomizeTemplateDialogGuidStr);
		DialogBoxBase dialogBoxBase = DialogBoxBase.FindByGuid(guid);
		DialogResult dialogResult = dialogBoxBase.ShowDialogFromParentDialog();
		if (dialogResult == DialogResult.OK)
		{
			HiddenWasCustomizedCheckBox.Checked = true;
		}
		else
		{
			HiddenWasCustomizedCheckBox.Checked = false;
		}
	}

	private bool IsValidInput()
	{
		if (!ValidRoadsFilePath)
		{
			return false;
		}
		return true;
	}

	private void SyncOkButton()
	{
		AllowSubmitDialog = IsValidInput();
		OKBtn.Enabled = AllowSubmitDialog;
	}

	private void OKBtn_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
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
		this.CancelBtn = new System.Windows.Forms.Button();
		this.OKBtn = new System.Windows.Forms.Button();
		this.ShapefileTab = new System.Windows.Forms.TabPage();
		this.HiddenWasCustomizedCheckBox = new palDotNet.palDialog.DialogCheckBox();
		this.RoadAttributesGroupBox = new System.Windows.Forms.GroupBox();
		this.UseDefaultAttributesRadioButton = new palDotNet.palDialog.DialogRadioButton();
		this.UseCustomAttributesRadioButton = new palDotNet.palDialog.DialogRadioButton();
		this.ModifyCustomTemplateButton = new System.Windows.Forms.Button();
		this.AttributeMappingTemplateInfoLabel1 = new palDotNet.palDialog.DialogLabel();
		this.RoadsFileGroup = new System.Windows.Forms.GroupBox();
		this.RoadsFilePath = new palDotNet.palDialog.DialogPathLoader();
		this.RoadFileInfoLabel = new palDotNet.palDialog.DialogLabel();
		this.TabControl = new System.Windows.Forms.TabControl();
		this.RoadOptionsTab = new System.Windows.Forms.TabPage();
		this.RoadGenerationOptionsPanel = new palDotNet.palDialog.RoadGenerationOptionsPanel();
		this.mMessagePanel = new palDotNet.palDialog.DialogMessagePanel();
		this.ShapefileTab.SuspendLayout();
		this.RoadAttributesGroupBox.SuspendLayout();
		this.RoadsFileGroup.SuspendLayout();
		this.TabControl.SuspendLayout();
		this.RoadOptionsTab.SuspendLayout();
		base.SuspendLayout();
		this.CancelBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.CancelBtn.Location = new System.Drawing.Point(407, 379);
		this.CancelBtn.Name = "CancelBtn";
		this.CancelBtn.Size = new System.Drawing.Size(75, 23);
		this.CancelBtn.TabIndex = 2;
		this.CancelBtn.Text = "Cancel";
		this.CancelBtn.UseVisualStyleBackColor = true;
		this.OKBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.OKBtn.Location = new System.Drawing.Point(326, 379);
		this.OKBtn.Name = "OKBtn";
		this.OKBtn.Size = new System.Drawing.Size(75, 23);
		this.OKBtn.TabIndex = 1;
		this.OKBtn.Text = "OK";
		this.OKBtn.UseVisualStyleBackColor = true;
		this.OKBtn.Click += new System.EventHandler(OKBtn_Click);
		this.ShapefileTab.Controls.Add(this.HiddenWasCustomizedCheckBox);
		this.ShapefileTab.Controls.Add(this.RoadAttributesGroupBox);
		this.ShapefileTab.Controls.Add(this.RoadsFileGroup);
		this.ShapefileTab.Location = new System.Drawing.Point(4, 22);
		this.ShapefileTab.Name = "ShapefileTab";
		this.ShapefileTab.Padding = new System.Windows.Forms.Padding(3);
		this.ShapefileTab.Size = new System.Drawing.Size(461, 319);
		this.ShapefileTab.TabIndex = 0;
		this.ShapefileTab.Text = "Shapefile";
		this.ShapefileTab.UseVisualStyleBackColor = true;
		this.HiddenWasCustomizedCheckBox.AutoSize = true;
		this.HiddenWasCustomizedCheckBox.Location = new System.Drawing.Point(28, 290);
		this.HiddenWasCustomizedCheckBox.Name = "HiddenWasCustomizedCheckBox";
		this.HiddenWasCustomizedCheckBox.SaveToConfig = true;
		this.HiddenWasCustomizedCheckBox.Size = new System.Drawing.Size(136, 17);
		this.HiddenWasCustomizedCheckBox.TabIndex = 2;
		this.HiddenWasCustomizedCheckBox.TabStop = false;
		this.HiddenWasCustomizedCheckBox.Text = "HiddenWasCustomized";
		this.HiddenWasCustomizedCheckBox.UseVisualStyleBackColor = true;
		this.HiddenWasCustomizedCheckBox.Visible = false;
		this.RoadAttributesGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.RoadAttributesGroupBox.Controls.Add(this.UseDefaultAttributesRadioButton);
		this.RoadAttributesGroupBox.Controls.Add(this.UseCustomAttributesRadioButton);
		this.RoadAttributesGroupBox.Controls.Add(this.ModifyCustomTemplateButton);
		this.RoadAttributesGroupBox.Controls.Add(this.AttributeMappingTemplateInfoLabel1);
		this.RoadAttributesGroupBox.Location = new System.Drawing.Point(6, 152);
		this.RoadAttributesGroupBox.Name = "RoadAttributesGroupBox";
		this.RoadAttributesGroupBox.Size = new System.Drawing.Size(449, 127);
		this.RoadAttributesGroupBox.TabIndex = 1;
		this.RoadAttributesGroupBox.TabStop = false;
		this.RoadAttributesGroupBox.Text = "Road attributes";
		this.UseDefaultAttributesRadioButton.AutoSize = true;
		this.UseDefaultAttributesRadioButton.Location = new System.Drawing.Point(37, 58);
		this.UseDefaultAttributesRadioButton.Name = "UseDefaultAttributesRadioButton";
		this.UseDefaultAttributesRadioButton.SaveToConfig = true;
		this.UseDefaultAttributesRadioButton.Size = new System.Drawing.Size(125, 17);
		this.UseDefaultAttributesRadioButton.TabIndex = 1;
		this.UseDefaultAttributesRadioButton.TabStop = true;
		this.UseDefaultAttributesRadioButton.Text = "Use default attributes";
		this.UseDefaultAttributesRadioButton.UseVisualStyleBackColor = true;
		this.UseDefaultAttributesRadioButton.CheckedChanged += new System.EventHandler(UseDefaultAttributesRadioButton_CheckedChanged);
		this.UseCustomAttributesRadioButton.AutoSize = true;
		this.UseCustomAttributesRadioButton.Location = new System.Drawing.Point(37, 84);
		this.UseCustomAttributesRadioButton.Name = "UseCustomAttributesRadioButton";
		this.UseCustomAttributesRadioButton.SaveToConfig = true;
		this.UseCustomAttributesRadioButton.Size = new System.Drawing.Size(127, 17);
		this.UseCustomAttributesRadioButton.TabIndex = 2;
		this.UseCustomAttributesRadioButton.TabStop = true;
		this.UseCustomAttributesRadioButton.Text = "Use custom attributes";
		this.UseCustomAttributesRadioButton.UseVisualStyleBackColor = true;
		this.UseCustomAttributesRadioButton.CheckedChanged += new System.EventHandler(UseCustomAttributesRadioButton_CheckedChanged);
		this.ModifyCustomTemplateButton.Location = new System.Drawing.Point(192, 81);
		this.ModifyCustomTemplateButton.Name = "ModifyCustomTemplateButton";
		this.ModifyCustomTemplateButton.Size = new System.Drawing.Size(108, 23);
		this.ModifyCustomTemplateButton.TabIndex = 3;
		this.ModifyCustomTemplateButton.Text = "Customize...";
		this.ModifyCustomTemplateButton.UseVisualStyleBackColor = true;
		this.ModifyCustomTemplateButton.Click += new System.EventHandler(ModifyCustomTemplateButton_Click);
		this.AttributeMappingTemplateInfoLabel1.AutoSize = true;
		this.AttributeMappingTemplateInfoLabel1.Location = new System.Drawing.Point(19, 27);
		this.AttributeMappingTemplateInfoLabel1.Name = "AttributeMappingTemplateInfoLabel1";
		this.AttributeMappingTemplateInfoLabel1.SaveToConfig = false;
		this.AttributeMappingTemplateInfoLabel1.Size = new System.Drawing.Size(283, 13);
		this.AttributeMappingTemplateInfoLabel1.TabIndex = 0;
		this.AttributeMappingTemplateInfoLabel1.Text = "Choose an attribute mapping template to use during import.";
		this.RoadsFileGroup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.RoadsFileGroup.Controls.Add(this.mMessagePanel);
		this.RoadsFileGroup.Controls.Add(this.RoadsFilePath);
		this.RoadsFileGroup.Controls.Add(this.RoadFileInfoLabel);
		this.RoadsFileGroup.Location = new System.Drawing.Point(6, 6);
		this.RoadsFileGroup.Name = "RoadsFileGroup";
		this.RoadsFileGroup.Size = new System.Drawing.Size(449, 140);
		this.RoadsFileGroup.TabIndex = 0;
		this.RoadsFileGroup.TabStop = false;
		this.RoadsFileGroup.Text = "Roads file";
		this.RoadsFilePath.BackColor = System.Drawing.Color.Transparent;
		this.RoadsFilePath.BrowseType = palDotNet.palDialog.DialogPathLoader.EnumBrowseType.eOpen;
		this.RoadsFilePath.CheckFileExists = true;
		this.RoadsFilePath.Description = null;
		this.RoadsFilePath.Filter = "Shapefile (*.shp)|*.shp";
		this.RoadsFilePath.Location = new System.Drawing.Point(22, 60);
		this.RoadsFilePath.Name = "RoadsFilePath";
		this.RoadsFilePath.Path = "";
		this.RoadsFilePath.SaveToConfig = true;
		this.RoadsFilePath.Size = new System.Drawing.Size(400, 24);
		this.RoadsFilePath.TabIndex = 1;
		this.RoadFileInfoLabel.AutoSize = true;
		this.RoadFileInfoLabel.Location = new System.Drawing.Point(19, 30);
		this.RoadFileInfoLabel.Name = "RoadFileInfoLabel";
		this.RoadFileInfoLabel.SaveToConfig = false;
		this.RoadFileInfoLabel.Size = new System.Drawing.Size(260, 13);
		this.RoadFileInfoLabel.TabIndex = 0;
		this.RoadFileInfoLabel.Text = "Specify a feature shapefile (*.shp) with road geometry.";
		this.TabControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TabControl.Controls.Add(this.ShapefileTab);
		this.TabControl.Controls.Add(this.RoadOptionsTab);
		this.TabControl.Location = new System.Drawing.Point(13, 13);
		this.TabControl.Name = "TabControl";
		this.TabControl.SelectedIndex = 0;
		this.TabControl.Size = new System.Drawing.Size(469, 345);
		this.TabControl.TabIndex = 0;
		this.RoadOptionsTab.Controls.Add(this.RoadGenerationOptionsPanel);
		this.RoadOptionsTab.Location = new System.Drawing.Point(4, 22);
		this.RoadOptionsTab.Name = "RoadOptionsTab";
		this.RoadOptionsTab.Size = new System.Drawing.Size(461, 319);
		this.RoadOptionsTab.TabIndex = 1;
		this.RoadOptionsTab.Text = "Road Options";
		this.RoadOptionsTab.UseVisualStyleBackColor = true;
		this.RoadGenerationOptionsPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.RoadGenerationOptionsPanel.BackColor = System.Drawing.Color.Transparent;
		this.RoadGenerationOptionsPanel.Location = new System.Drawing.Point(6, 6);
		this.RoadGenerationOptionsPanel.Margin = new System.Windows.Forms.Padding(6);
		this.RoadGenerationOptionsPanel.Name = "RoadGenerationOptionsPanel";
		this.RoadGenerationOptionsPanel.Size = new System.Drawing.Size(449, 307);
		this.RoadGenerationOptionsPanel.TabIndex = 0;
		this.mMessagePanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.mMessagePanel.Location = new System.Drawing.Point(22, 100);
		this.mMessagePanel.Name = "mMessagePanel";
		this.mMessagePanel.Size = new System.Drawing.Size(400, 20);
		this.mMessagePanel.TabIndex = 2;
		this.mMessagePanel.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.CancelBtn;
		base.ClientSize = new System.Drawing.Size(494, 414);
		base.Controls.Add(this.TabControl);
		base.Controls.Add(this.OKBtn);
		base.Controls.Add(this.CancelBtn);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "GisImportRoadsDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "GIS Mode - Import Roads";
		base.Load += new System.EventHandler(GisImportRoadsDialog_Load);
		this.ShapefileTab.ResumeLayout(false);
		this.ShapefileTab.PerformLayout();
		this.RoadAttributesGroupBox.ResumeLayout(false);
		this.RoadAttributesGroupBox.PerformLayout();
		this.RoadsFileGroup.ResumeLayout(false);
		this.RoadsFileGroup.PerformLayout();
		this.TabControl.ResumeLayout(false);
		this.RoadOptionsTab.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
