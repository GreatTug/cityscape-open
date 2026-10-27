using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace palDotNet.palDialog;

[DialogBoxCreator]
[CPPCodeGeneration(OutputFile = "..\\edEditor\\edTerrainSettings\\TerrainSettingsDialog", Win32PCOnly = true)]
public class TerrainSettingsDialog : DialogBoxBase
{
	private IContainer components;

	private Button okButton;

	private Button cancelButton;

	private Label ProjectFileLabel;

	private GroupBox ProjectGroupBox;

	private DialogCheckBox CreateNewProjectCheckBox;

	private ToolTip toolTip1;

	private DialogPathLoader ProjectFilePath;

	private TerrainSettingsPanel mTerrainSettingsPanel;

	private DialogMessagePanel MessagePanel;

	private Size DialogDefaultSize;

	private bool ProjectPanelEnabled;

	private string DefaultProjectPath = "";

	private Cursor mOldCursor;

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
		this.components = new System.ComponentModel.Container();
		this.okButton = new System.Windows.Forms.Button();
		this.cancelButton = new System.Windows.Forms.Button();
		this.ProjectFileLabel = new System.Windows.Forms.Label();
		this.ProjectGroupBox = new System.Windows.Forms.GroupBox();
		this.ProjectFilePath = new palDotNet.palDialog.DialogPathLoader();
		this.CreateNewProjectCheckBox = new palDotNet.palDialog.DialogCheckBox();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.mTerrainSettingsPanel = new palDotNet.palDialog.TerrainSettingsPanel();
		this.MessagePanel = new palDotNet.palDialog.DialogMessagePanel();
		this.ProjectGroupBox.SuspendLayout();
		base.SuspendLayout();
		this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.okButton.Location = new System.Drawing.Point(654, 494);
		this.okButton.Name = "okButton";
		this.okButton.Size = new System.Drawing.Size(75, 23);
		this.okButton.TabIndex = 2;
		this.okButton.Text = "OK";
		this.okButton.UseVisualStyleBackColor = true;
		this.okButton.Click += new System.EventHandler(okButton_Click);
		this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cancelButton.Location = new System.Drawing.Point(736, 494);
		this.cancelButton.Name = "cancelButton";
		this.cancelButton.Size = new System.Drawing.Size(75, 23);
		this.cancelButton.TabIndex = 3;
		this.cancelButton.Text = "Cancel";
		this.cancelButton.UseVisualStyleBackColor = true;
		this.ProjectFileLabel.AutoSize = true;
		this.ProjectFileLabel.Location = new System.Drawing.Point(20, 62);
		this.ProjectFileLabel.Name = "ProjectFileLabel";
		this.ProjectFileLabel.Size = new System.Drawing.Size(59, 13);
		this.ProjectFileLabel.TabIndex = 1;
		this.ProjectFileLabel.Text = "&Project file:";
		this.ProjectGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ProjectGroupBox.Controls.Add(this.ProjectFilePath);
		this.ProjectGroupBox.Controls.Add(this.CreateNewProjectCheckBox);
		this.ProjectGroupBox.Controls.Add(this.ProjectFileLabel);
		this.ProjectGroupBox.Location = new System.Drawing.Point(12, 372);
		this.ProjectGroupBox.Name = "ProjectGroupBox";
		this.ProjectGroupBox.Size = new System.Drawing.Size(800, 100);
		this.ProjectGroupBox.TabIndex = 1;
		this.ProjectGroupBox.TabStop = false;
		this.ProjectGroupBox.Text = "Project";
		this.ProjectFilePath.BackColor = System.Drawing.Color.Transparent;
		this.ProjectFilePath.BrowseType = palDotNet.palDialog.DialogPathLoader.EnumBrowseType.eOpen;
		this.ProjectFilePath.CheckFileExists = true;
		this.ProjectFilePath.Description = null;
		this.ProjectFilePath.Filter = "CityScape Project file (*.csp)|*.csp|Legacy CityScape Project file (*.paw)|*.paw|All files (*.*)|*.*";
		this.ProjectFilePath.Location = new System.Drawing.Point(124, 57);
		this.ProjectFilePath.Name = "ProjectFilePath";
		this.ProjectFilePath.Path = "";
		this.ProjectFilePath.SaveToConfig = true;
		this.ProjectFilePath.Size = new System.Drawing.Size(650, 24);
		this.ProjectFilePath.TabIndex = 2;
		this.ProjectFilePath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(ProjectFilePath_KeyPress);
		this.ProjectFilePath.PathChanged += new System.EventHandler(ProjectFilePath_PathChanged);
		this.CreateNewProjectCheckBox.AutoSize = true;
		this.CreateNewProjectCheckBox.Location = new System.Drawing.Point(23, 28);
		this.CreateNewProjectCheckBox.Name = "CreateNewProjectCheckBox";
		this.CreateNewProjectCheckBox.SaveToConfig = true;
		this.CreateNewProjectCheckBox.Size = new System.Drawing.Size(115, 17);
		this.CreateNewProjectCheckBox.TabIndex = 0;
		this.CreateNewProjectCheckBox.Text = "Create new project";
		this.CreateNewProjectCheckBox.UseVisualStyleBackColor = true;
		this.CreateNewProjectCheckBox.CheckedChanged += new System.EventHandler(CreateNewProjectCheckBox_CheckedChanged);
		this.mTerrainSettingsPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.mTerrainSettingsPanel.Location = new System.Drawing.Point(12, 12);
		this.mTerrainSettingsPanel.Name = "mTerrainSettingsPanel";
		this.mTerrainSettingsPanel.Size = new System.Drawing.Size(800, 350);
		this.mTerrainSettingsPanel.TabIndex = 0;
		this.MessagePanel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.MessagePanel.Location = new System.Drawing.Point(16, 494);
		this.MessagePanel.Name = "MessagePanel";
		this.MessagePanel.Size = new System.Drawing.Size(550, 20);
		this.MessagePanel.TabIndex = 5;
		this.MessagePanel.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cancelButton;
		base.ClientSize = new System.Drawing.Size(823, 529);
		base.Controls.Add(this.MessagePanel);
		base.Controls.Add(this.mTerrainSettingsPanel);
		base.Controls.Add(this.ProjectGroupBox);
		base.Controls.Add(this.cancelButton);
		base.Controls.Add(this.okButton);
		this.DoubleBuffered = true;
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "TerrainSettingsDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "New Terrain";
		this.ProjectGroupBox.ResumeLayout(false);
		this.ProjectGroupBox.PerformLayout();
		base.ResumeLayout(false);
	}

	public TerrainSettingsDialog()
	{
		InitializeComponent();
		mTerrainSettingsPanel.TerrainValueChanged += mTerrainSettingsPanel_TerrainValueChanged;
		DialogDefaultSize = base.Size;
		EnableProjectPanel(bEnabled: false);
		ProjectFilePath.LoadedFromSettings = true;
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		mTerrainSettingsPanel.OnShown();
		ClearMessageInfo();
		if (string.IsNullOrEmpty(ProjectFilePath.Path))
		{
			ProjectFilePath.Path = DefaultProjectPath;
		}
		SyncProjectFilePath();
		SyncOkButton();
	}

	public TerrainSettingsPanel GetTerrainSettingsPanel()
	{
		return mTerrainSettingsPanel;
	}

	private void mTerrainSettingsPanel_TerrainValueChanged(object sender, EventArgs e)
	{
		ClearMessageInfo();
		SyncOkButton();
	}

	public void EnableProjectPanel(bool bEnabled)
	{
		ProjectPanelEnabled = bEnabled;
		if (ProjectPanelEnabled)
		{
			ProjectGroupBox.Show();
			base.Height = DialogDefaultSize.Height;
		}
		else
		{
			ProjectGroupBox.Hide();
			base.Height = DialogDefaultSize.Height - ProjectGroupBox.Height;
		}
	}

	public void SetDefaultProjectPath(string defaultProjectPath)
	{
		DefaultProjectPath = defaultProjectPath;
	}

	private void ProjectFilePath_PathChanged(object sender, EventArgs e)
	{
		ClearMessageInfo();
		SyncOkButton();
	}

	private void CreateNewProjectCheckBox_CheckedChanged(object sender, EventArgs e)
	{
		ClearMessageInfo();
		SyncProjectFilePath();
		SyncOkButton();
	}

	private void SyncProjectFilePath()
	{
		ProjectFilePath.Enabled = !CreateNewProjectCheckBox.Checked;
	}

	private bool IsValidProject()
	{
		if (!ProjectPanelEnabled)
		{
			return true;
		}
		if (CreateNewProjectCheckBox.Checked)
		{
			return true;
		}
		if (ProjectFilePath.Path.Length == 0)
		{
			return false;
		}
		return true;
	}

	public void BeginWaitCursor()
	{
		mOldCursor = Cursor;
		Cursor = System.Windows.Forms.Cursors.WaitCursor;
	}

	public void EndWaitCursor()
	{
		Cursor = mOldCursor;
		mOldCursor = null;
	}

	public void ShowErrorMessage(string msg)
	{
		MessagePanel.ErrorMessage(msg);
	}

	public void ShowWarningMessage(string msg)
	{
		MessagePanel.WarningMessage(msg);
	}

	public void ClearMessageInfo()
	{
		MessagePanel.ClearMessage();
	}

	private void SyncOkButton()
	{
		if (!mTerrainSettingsPanel.IsDataValid())
		{
			AllowSubmitDialog = false;
			ShowErrorMessage(mTerrainSettingsPanel.GetValidationErrorString());
		}
		else if (!IsValidProject())
		{
			AllowSubmitDialog = false;
			ShowErrorMessage("Project file has not been set.");
		}
		else
		{
			AllowSubmitDialog = true;
		}
		okButton.Enabled = AllowSubmitDialog;
	}

	private void okButton_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
	}

	private void ProjectFilePath_KeyPress(object sender, KeyPressEventArgs e)
	{
		ClearMessageInfo();
		SyncOkButton();
	}
}
