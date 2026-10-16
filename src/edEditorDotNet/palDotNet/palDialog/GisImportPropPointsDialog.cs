using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace palDotNet.palDialog;

[DialogBoxCreator]
[CPPCodeGeneration(OutputFile = "..\\edEditor\\edGISBorkDialogs\\GisImportPropPointsDialog", Win32PCOnly = true)]
public class GisImportPropPointsDialog : DialogBoxBase
{
	private bool ValidPropsFilePath;

	private IContainer components;

	private TabControl tabControl1;

	private TabPage ShapefileTab;

	private TabPage TypeMapTab;

	private Button CancelBtn;

	private Button OKBtn;

	private GroupBox ShapefileGroupBox;

	private DialogLabel PropsShapefilePathLabel;

	private DialogPathLoader PropsShapefilePathInput;

	private DialogCheckGroupBox PropTypeMappingCheckGroupBox;

	private DialogLabel PropTypeFieldNameLabel;

	private DialogComboBox PropTypeFieldNameComboBox;

	private DialogStyleGridChooser TypeStyleGridChooser;

	private Panel TypeMapPanel;

	private DialogMessagePanel mMessagePanel;

	public DialogPathLoader PropsShapefilePath => PropsShapefilePathInput;

	public DialogStyleGridChooser TypeStyleChooser => TypeStyleGridChooser;

	public DialogMessagePanel MessagePanel => mMessagePanel;

	public event PropPointsDialogOnShownHandler PropPointsDialogOnShown;

	public GisImportPropPointsDialog()
	{
		InitializeComponent();
		InvalidatePropsFilePath();
		PropTypeMappingCheckGroupBox.DisableChildrenIfUnchecked = false;
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		ResetAttributeTabs();
		if (PropPointsDialogOnShown != null)
		{
			PropPointsDialogOnShown(this, e);
		}
		SyncAttributeTabs();
		SyncOkButton();
	}

	public void SyncAttributeTabs()
	{
		bool validPropsFilePath = ValidPropsFilePath;
		TypeMapPanel.Enabled = validPropsFilePath;
	}

	public void ResetAttributeTabs()
	{
		TypeMapTab_Reset();
	}

	public void InvalidatePropsFilePath()
	{
		ValidPropsFilePath = false;
		SyncAttributeTabs();
		SyncOkButton();
	}

	public void ValidatePropsFilePath()
	{
		ValidPropsFilePath = true;
		SyncAttributeTabs();
		SyncOkButton();
	}

	private void TypeMapTab_Reset()
	{
		PropTypeMappingCheckGroupBox.Checked = false;
	}

	public void SetPropTypeFieldNameItems(string[] itemNames)
	{
		PropTypeFieldNameComboBox.Items.Clear();
		PropTypeFieldNameComboBox.Items.AddRange(itemNames);
		PropTypeFieldNameComboBox.SelectedIndex = -1;
		SyncPropStyleChooser();
	}

	public void ClearPropTypeFieldNameItems()
	{
		PropTypeFieldNameComboBox.Items.Clear();
		SyncPropStyleChooser();
	}

	public void ClearPropTypeStyles()
	{
		TypeStyleGridChooser.ClearStyles();
	}

	public string GetPropTypeFieldName()
	{
		return PropTypeFieldNameComboBox.Text;
	}

	private void PropTypeFieldNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		SyncPropStyleChooser();
	}

	private void PropTypeMappingCheckGroupBox_CheckStateChanged(object sender, EventArgs e)
	{
		if (PropTypeMappingCheckGroupBox.Checked)
		{
			PropTypeFieldNameComboBox.Enabled = true;
			SyncPropStyleChooser();
		}
		else
		{
			PropTypeFieldNameComboBox.Enabled = false;
			TypeStyleGridChooser.Enabled = false;
		}
	}

	private void SyncPropStyleChooser()
	{
		if (PropTypeMappingCheckGroupBox.Checked && PropTypeFieldNameComboBox.SelectedIndex != -1)
		{
			TypeStyleGridChooser.Enabled = true;
		}
		else
		{
			TypeStyleGridChooser.Enabled = false;
		}
	}

	private bool IsValidInput()
	{
		if (!ValidPropsFilePath)
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
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.ShapefileTab = new System.Windows.Forms.TabPage();
		this.ShapefileGroupBox = new System.Windows.Forms.GroupBox();
		this.mMessagePanel = new palDotNet.palDialog.DialogMessagePanel();
		this.PropsShapefilePathLabel = new palDotNet.palDialog.DialogLabel();
		this.PropsShapefilePathInput = new palDotNet.palDialog.DialogPathLoader();
		this.TypeMapTab = new System.Windows.Forms.TabPage();
		this.TypeMapPanel = new System.Windows.Forms.Panel();
		this.PropTypeMappingCheckGroupBox = new palDotNet.palDialog.DialogCheckGroupBox();
		this.PropTypeFieldNameLabel = new palDotNet.palDialog.DialogLabel();
		this.PropTypeFieldNameComboBox = new palDotNet.palDialog.DialogComboBox();
		this.TypeStyleGridChooser = new palDotNet.palDialog.DialogStyleGridChooser();
		this.CancelBtn = new System.Windows.Forms.Button();
		this.OKBtn = new System.Windows.Forms.Button();
		this.tabControl1.SuspendLayout();
		this.ShapefileTab.SuspendLayout();
		this.ShapefileGroupBox.SuspendLayout();
		this.TypeMapTab.SuspendLayout();
		this.TypeMapPanel.SuspendLayout();
		this.PropTypeMappingCheckGroupBox.SuspendLayout();
		base.SuspendLayout();
		this.tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tabControl1.Controls.Add(this.ShapefileTab);
		this.tabControl1.Controls.Add(this.TypeMapTab);
		this.tabControl1.Location = new System.Drawing.Point(13, 13);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(469, 350);
		this.tabControl1.TabIndex = 0;
		this.ShapefileTab.Controls.Add(this.ShapefileGroupBox);
		this.ShapefileTab.Location = new System.Drawing.Point(4, 22);
		this.ShapefileTab.Name = "ShapefileTab";
		this.ShapefileTab.Padding = new System.Windows.Forms.Padding(3);
		this.ShapefileTab.Size = new System.Drawing.Size(461, 324);
		this.ShapefileTab.TabIndex = 0;
		this.ShapefileTab.Text = "Shapefile";
		this.ShapefileTab.UseVisualStyleBackColor = true;
		this.ShapefileGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ShapefileGroupBox.Controls.Add(this.mMessagePanel);
		this.ShapefileGroupBox.Controls.Add(this.PropsShapefilePathLabel);
		this.ShapefileGroupBox.Controls.Add(this.PropsShapefilePathInput);
		this.ShapefileGroupBox.Location = new System.Drawing.Point(7, 7);
		this.ShapefileGroupBox.Name = "ShapefileGroupBox";
		this.ShapefileGroupBox.Size = new System.Drawing.Size(448, 140);
		this.ShapefileGroupBox.TabIndex = 0;
		this.ShapefileGroupBox.TabStop = false;
		this.ShapefileGroupBox.Text = "Props file";
		this.mMessagePanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.mMessagePanel.Location = new System.Drawing.Point(22, 100);
		this.mMessagePanel.Name = "mMessagePanel";
		this.mMessagePanel.Size = new System.Drawing.Size(400, 20);
		this.mMessagePanel.TabIndex = 2;
		this.mMessagePanel.TabStop = false;
		this.PropsShapefilePathLabel.AutoSize = true;
		this.PropsShapefilePathLabel.Location = new System.Drawing.Point(19, 30);
		this.PropsShapefilePathLabel.Name = "PropsShapefilePathLabel";
		this.PropsShapefilePathLabel.SaveToConfig = false;
		this.PropsShapefilePathLabel.Size = new System.Drawing.Size(297, 13);
		this.PropsShapefilePathLabel.TabIndex = 0;
		this.PropsShapefilePathLabel.Text = "Specify a feature shapefile (*.shp) with prop placement points:";
		this.PropsShapefilePathInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.PropsShapefilePathInput.BackColor = System.Drawing.Color.Transparent;
		this.PropsShapefilePathInput.BrowseType = palDotNet.palDialog.DialogPathLoader.EnumBrowseType.eOpen;
		this.PropsShapefilePathInput.CheckFileExists = true;
		this.PropsShapefilePathInput.Description = null;
		this.PropsShapefilePathInput.Filter = "Shapefile (*.shp)|*.shp";
		this.PropsShapefilePathInput.Location = new System.Drawing.Point(22, 60);
		this.PropsShapefilePathInput.Name = "PropsShapefilePathInput";
		this.PropsShapefilePathInput.Path = "";
		this.PropsShapefilePathInput.SaveToConfig = true;
		this.PropsShapefilePathInput.Size = new System.Drawing.Size(400, 24);
		this.PropsShapefilePathInput.TabIndex = 1;
		this.TypeMapTab.Controls.Add(this.TypeMapPanel);
		this.TypeMapTab.Location = new System.Drawing.Point(4, 22);
		this.TypeMapTab.Name = "TypeMapTab";
		this.TypeMapTab.Padding = new System.Windows.Forms.Padding(3);
		this.TypeMapTab.Size = new System.Drawing.Size(461, 324);
		this.TypeMapTab.TabIndex = 1;
		this.TypeMapTab.Text = "Type Map";
		this.TypeMapTab.UseVisualStyleBackColor = true;
		this.TypeMapPanel.Controls.Add(this.PropTypeMappingCheckGroupBox);
		this.TypeMapPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TypeMapPanel.Location = new System.Drawing.Point(3, 3);
		this.TypeMapPanel.Name = "TypeMapPanel";
		this.TypeMapPanel.Size = new System.Drawing.Size(455, 318);
		this.TypeMapPanel.TabIndex = 1;
		this.PropTypeMappingCheckGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.PropTypeMappingCheckGroupBox.Controls.Add(this.PropTypeFieldNameLabel);
		this.PropTypeMappingCheckGroupBox.Controls.Add(this.PropTypeFieldNameComboBox);
		this.PropTypeMappingCheckGroupBox.Controls.Add(this.TypeStyleGridChooser);
		this.PropTypeMappingCheckGroupBox.Location = new System.Drawing.Point(3, 3);
		this.PropTypeMappingCheckGroupBox.Name = "PropTypeMappingCheckGroupBox";
		this.PropTypeMappingCheckGroupBox.SaveToConfig = true;
		this.PropTypeMappingCheckGroupBox.Size = new System.Drawing.Size(449, 312);
		this.PropTypeMappingCheckGroupBox.TabIndex = 0;
		this.PropTypeMappingCheckGroupBox.TabStop = false;
		this.PropTypeMappingCheckGroupBox.Text = "Prop type mapping";
		this.PropTypeMappingCheckGroupBox.CheckStateChanged += new System.EventHandler(PropTypeMappingCheckGroupBox_CheckStateChanged);
		this.PropTypeFieldNameLabel.AutoSize = true;
		this.PropTypeFieldNameLabel.Location = new System.Drawing.Point(50, 32);
		this.PropTypeFieldNameLabel.Name = "PropTypeFieldNameLabel";
		this.PropTypeFieldNameLabel.SaveToConfig = false;
		this.PropTypeFieldNameLabel.Size = new System.Drawing.Size(77, 13);
		this.PropTypeFieldNameLabel.TabIndex = 0;
		this.PropTypeFieldNameLabel.Text = "Prop type field:";
		this.PropTypeFieldNameComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.PropTypeFieldNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.PropTypeFieldNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.PropTypeFieldNameComboBox.FormattingEnabled = true;
		this.PropTypeFieldNameComboBox.Location = new System.Drawing.Point(191, 29);
		this.PropTypeFieldNameComboBox.Name = "PropTypeFieldNameComboBox";
		this.PropTypeFieldNameComboBox.SaveToConfig = true;
		this.PropTypeFieldNameComboBox.Size = new System.Drawing.Size(160, 21);
		this.PropTypeFieldNameComboBox.Sorted = true;
		this.PropTypeFieldNameComboBox.TabIndex = 1;
		this.PropTypeFieldNameComboBox.SelectedIndexChanged += new System.EventHandler(PropTypeFieldNameComboBox_SelectedIndexChanged);
		this.TypeStyleGridChooser.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TypeStyleGridChooser.Location = new System.Drawing.Point(13, 70);
		this.TypeStyleGridChooser.Margin = new System.Windows.Forms.Padding(10);
		this.TypeStyleGridChooser.MinimumSize = new System.Drawing.Size(0, 140);
		this.TypeStyleGridChooser.Name = "TypeStyleGridChooser";
		this.TypeStyleGridChooser.Size = new System.Drawing.Size(423, 229);
		this.TypeStyleGridChooser.TabIndex = 2;
		this.CancelBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.CancelBtn.Location = new System.Drawing.Point(407, 389);
		this.CancelBtn.Name = "CancelBtn";
		this.CancelBtn.Size = new System.Drawing.Size(75, 23);
		this.CancelBtn.TabIndex = 2;
		this.CancelBtn.Text = "Cancel";
		this.CancelBtn.UseVisualStyleBackColor = true;
		this.OKBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.OKBtn.Location = new System.Drawing.Point(326, 389);
		this.OKBtn.Name = "OKBtn";
		this.OKBtn.Size = new System.Drawing.Size(75, 23);
		this.OKBtn.TabIndex = 1;
		this.OKBtn.Text = "OK";
		this.OKBtn.UseVisualStyleBackColor = true;
		this.OKBtn.Click += new System.EventHandler(OKBtn_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.CancelBtn;
		base.ClientSize = new System.Drawing.Size(494, 424);
		base.Controls.Add(this.OKBtn);
		base.Controls.Add(this.CancelBtn);
		base.Controls.Add(this.tabControl1);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "GisImportPropPointsDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "GIS Mode - Import Point Props";
		this.tabControl1.ResumeLayout(false);
		this.ShapefileTab.ResumeLayout(false);
		this.ShapefileGroupBox.ResumeLayout(false);
		this.ShapefileGroupBox.PerformLayout();
		this.TypeMapTab.ResumeLayout(false);
		this.TypeMapPanel.ResumeLayout(false);
		this.PropTypeMappingCheckGroupBox.ResumeLayout(false);
		this.PropTypeMappingCheckGroupBox.PerformLayout();
		base.ResumeLayout(false);
	}
}
