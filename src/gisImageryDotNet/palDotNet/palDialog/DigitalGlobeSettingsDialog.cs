using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using gisImageryDotNet.Properties;

namespace palDotNet.palDialog;

[DialogBoxCreator]
[CPPCodeGeneration(OutputFile = "..\\gisImagery\\gisDigitalGlobe\\DigitalGlobeSettingsDialog")]
public class DigitalGlobeSettingsDialog : DialogBoxBase
{
	private bool ValidClientId;

	private IContainer components;

	private DialogLabel ClientIdLabel;

	private Button mCancelButton;

	private Button mOkButton;

	private GroupBox ClientInfoGroup;

	private DialogLabel LayerStackLabel;

	private DialogTextBox ClientIdTextBox;

	private DialogLabel ClientIdInfoLabel;

	private DialogComboBox LayerStackComboBox;

	private Button ValidateClientIdButton;

	private GroupBox LayerStackGroupBox;

	private DialogLabel ValidationInfoLabel;

	private DialogLabel LayerStackInfoLabel;

	private PictureBox ValidationInfoIcon;

	private Panel ValidationInfoPanel;

	private DialogLabel HiddenAppIdLabel;

	private PictureBox ValidatedIcon;

	private DialogLabel ValidatedIconLabel;

	public Button ValidateClientIdBtn => ValidateClientIdButton;

	public DigitalGlobeSettingsDialog()
	{
		InitializeComponent();
		SyncClientInfo();
		SyncLayerStack();
		SyncOkButton();
	}

	private void ClientIdTextBox_TextChanged(object sender, EventArgs e)
	{
		ValidClientId = false;
		ValidateClientIdButton.Enabled = true;
		ClearValidatedIcon();
		SyncClientInfo();
		SyncLayerStack();
		SyncOkButton();
	}

	private void ValidateClientIdButton_Click(object sender, EventArgs e)
	{
		Cursor current = Cursor.Current;
		Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
		ClearValidatedIcon();
		if (PopulateLayerStackComboBox())
		{
			ValidClientId = true;
			ValidateClientIdButton.Enabled = false;
			SetSuccessfulValidatedIcon();
		}
		else
		{
			ValidClientId = false;
			SetFailedValidatedIcon();
		}
		SyncLayerStack();
		SyncOkButton();
		Cursor.Current = current;
	}

	private void SyncClientInfo()
	{
		string text = ClientIdTextBox.Text;
		if (text.Length == 10)
		{
			ValidateClientIdButton.Enabled = true;
		}
		else
		{
			ValidateClientIdButton.Enabled = false;
		}
	}

	private void ClearValidatedIcon()
	{
		ValidatedIcon.Image = null;
		ValidatedIconLabel.Text = "";
	}

	private void SetSuccessfulValidatedIcon()
	{
		ValidatedIcon.Image = Resources.Check16;
		ValidatedIconLabel.Text = "Validated!";
	}

	private void SetFailedValidatedIcon()
	{
		ValidatedIcon.Image = Resources.Error16;
		ValidatedIconLabel.Text = "Failed Validation!";
	}

	private string[] RetrieveLayerStackItems()
	{
		string text = "http://image.globexplorer.com/gexservlets/meta";
		text += "?cmd=clientinfo";
		text = text + "&appid=" + HiddenAppIdLabel.Text;
		text = text + "&id=" + ClientIdTextBox.Text;
		text += "&type=ls";
		try
		{
			WebRequest webRequest = WebRequest.Create(text);
			using HttpWebResponse httpWebResponse = webRequest.GetResponse() as HttpWebResponse;
			if (httpWebResponse.ContentType != "text/plain;charset=ISO-8859-1")
			{
				return null;
			}
			using Stream stream = httpWebResponse.GetResponseStream();
			StreamReader streamReader = new StreamReader(stream);
			bool flag = false;
			List<string> list = new List<string>();
			while (streamReader.Peek() != -1)
			{
				string text2 = streamReader.ReadLine();
				if (!string.IsNullOrEmpty(text2))
				{
					if (text2.ToLower().Contains("error"))
					{
						flag = true;
						break;
					}
					list.Add(text2.Trim());
				}
			}
			streamReader.Close();
			if (flag)
			{
				return null;
			}
			return list.ToArray();
		}
		catch (Exception)
		{
			return null;
		}
	}

	private bool PopulateLayerStackComboBox()
	{
		LayerStackComboBox.Items.Clear();
		string[] array = RetrieveLayerStackItems();
		if (array == null)
		{
			return false;
		}
		LayerStackComboBox.Items.AddRange(array);
		LayerStackComboBox.SelectedIndex = -1;
		return true;
	}

	private void LayerStackComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		SyncOkButton();
	}

	private void SyncLayerStack()
	{
		if (ValidClientId)
		{
			LayerStackGroupBox.Enabled = true;
		}
		else
		{
			LayerStackGroupBox.Enabled = false;
		}
	}

	private void SyncOkButton()
	{
		if (!ValidClientId)
		{
			mOkButton.Enabled = false;
			AllowSubmitDialog = false;
		}
		else if (LayerStackComboBox.SelectedIndex < 0)
		{
			mOkButton.Enabled = false;
			AllowSubmitDialog = false;
		}
		else
		{
			mOkButton.Enabled = true;
			AllowSubmitDialog = true;
		}
	}

	private void mOkButton_Click(object sender, EventArgs e)
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
		this.ClientIdLabel = new palDotNet.palDialog.DialogLabel();
		this.mCancelButton = new System.Windows.Forms.Button();
		this.mOkButton = new System.Windows.Forms.Button();
		this.ClientInfoGroup = new System.Windows.Forms.GroupBox();
		this.ValidatedIconLabel = new palDotNet.palDialog.DialogLabel();
		this.ValidatedIcon = new System.Windows.Forms.PictureBox();
		this.ValidationInfoPanel = new System.Windows.Forms.Panel();
		this.ValidationInfoIcon = new System.Windows.Forms.PictureBox();
		this.ValidationInfoLabel = new palDotNet.palDialog.DialogLabel();
		this.ValidateClientIdButton = new System.Windows.Forms.Button();
		this.ClientIdInfoLabel = new palDotNet.palDialog.DialogLabel();
		this.ClientIdTextBox = new palDotNet.palDialog.DialogTextBox();
		this.LayerStackLabel = new palDotNet.palDialog.DialogLabel();
		this.LayerStackComboBox = new palDotNet.palDialog.DialogComboBox();
		this.LayerStackGroupBox = new System.Windows.Forms.GroupBox();
		this.LayerStackInfoLabel = new palDotNet.palDialog.DialogLabel();
		this.HiddenAppIdLabel = new palDotNet.palDialog.DialogLabel();
		this.ClientInfoGroup.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ValidatedIcon).BeginInit();
		this.ValidationInfoPanel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ValidationInfoIcon).BeginInit();
		this.LayerStackGroupBox.SuspendLayout();
		base.SuspendLayout();
		this.ClientIdLabel.AutoSize = true;
		this.ClientIdLabel.Location = new System.Drawing.Point(30, 30);
		this.ClientIdLabel.Name = "ClientIdLabel";
		this.ClientIdLabel.SaveToConfig = false;
		this.ClientIdLabel.Size = new System.Drawing.Size(50, 13);
		this.ClientIdLabel.TabIndex = 0;
		this.ClientIdLabel.Text = "Client ID:";
		this.mCancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.mCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.mCancelButton.Location = new System.Drawing.Point(307, 319);
		this.mCancelButton.Name = "mCancelButton";
		this.mCancelButton.Size = new System.Drawing.Size(75, 23);
		this.mCancelButton.TabIndex = 1;
		this.mCancelButton.Text = "Cancel";
		this.mCancelButton.UseVisualStyleBackColor = true;
		this.mOkButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.mOkButton.Location = new System.Drawing.Point(226, 319);
		this.mOkButton.Name = "mOkButton";
		this.mOkButton.Size = new System.Drawing.Size(75, 23);
		this.mOkButton.TabIndex = 2;
		this.mOkButton.Text = "OK";
		this.mOkButton.UseVisualStyleBackColor = true;
		this.mOkButton.Click += new System.EventHandler(mOkButton_Click);
		this.ClientInfoGroup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ClientInfoGroup.Controls.Add(this.ValidatedIconLabel);
		this.ClientInfoGroup.Controls.Add(this.ValidatedIcon);
		this.ClientInfoGroup.Controls.Add(this.ValidationInfoPanel);
		this.ClientInfoGroup.Controls.Add(this.ValidateClientIdButton);
		this.ClientInfoGroup.Controls.Add(this.ClientIdInfoLabel);
		this.ClientInfoGroup.Controls.Add(this.ClientIdTextBox);
		this.ClientInfoGroup.Controls.Add(this.ClientIdLabel);
		this.ClientInfoGroup.Location = new System.Drawing.Point(12, 12);
		this.ClientInfoGroup.Name = "ClientInfoGroup";
		this.ClientInfoGroup.Size = new System.Drawing.Size(370, 155);
		this.ClientInfoGroup.TabIndex = 3;
		this.ClientInfoGroup.TabStop = false;
		this.ClientInfoGroup.Text = "Client Info";
		this.ValidatedIconLabel.AutoSize = true;
		this.ValidatedIconLabel.Location = new System.Drawing.Point(250, 89);
		this.ValidatedIconLabel.Name = "ValidatedIconLabel";
		this.ValidatedIconLabel.SaveToConfig = false;
		this.ValidatedIconLabel.Size = new System.Drawing.Size(84, 13);
		this.ValidatedIconLabel.TabIndex = 10;
		this.ValidatedIconLabel.Text = "Failed Validation";
		this.ValidatedIcon.Image = gisImageryDotNet.Properties.Resources.Error16;
		this.ValidatedIcon.Location = new System.Drawing.Point(228, 88);
		this.ValidatedIcon.Name = "ValidatedIcon";
		this.ValidatedIcon.Size = new System.Drawing.Size(16, 16);
		this.ValidatedIcon.TabIndex = 9;
		this.ValidatedIcon.TabStop = false;
		this.ValidationInfoPanel.Controls.Add(this.ValidationInfoIcon);
		this.ValidationInfoPanel.Controls.Add(this.ValidationInfoLabel);
		this.ValidationInfoPanel.Location = new System.Drawing.Point(33, 115);
		this.ValidationInfoPanel.Name = "ValidationInfoPanel";
		this.ValidationInfoPanel.Size = new System.Drawing.Size(311, 26);
		this.ValidationInfoPanel.TabIndex = 8;
		this.ValidationInfoIcon.Image = gisImageryDotNet.Properties.Resources.Info16;
		this.ValidationInfoIcon.Location = new System.Drawing.Point(0, 5);
		this.ValidationInfoIcon.Name = "ValidationInfoIcon";
		this.ValidationInfoIcon.Size = new System.Drawing.Size(16, 16);
		this.ValidationInfoIcon.TabIndex = 8;
		this.ValidationInfoIcon.TabStop = false;
		this.ValidationInfoLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ValidationInfoLabel.Location = new System.Drawing.Point(23, 1);
		this.ValidationInfoLabel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
		this.ValidationInfoLabel.Name = "ValidationInfoLabel";
		this.ValidationInfoLabel.SaveToConfig = false;
		this.ValidationInfoLabel.Size = new System.Drawing.Size(285, 24);
		this.ValidationInfoLabel.TabIndex = 7;
		this.ValidationInfoLabel.Text = "Validating your Client ID requires internet connection.";
		this.ValidationInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.ValidateClientIdButton.Location = new System.Drawing.Point(128, 84);
		this.ValidateClientIdButton.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
		this.ValidateClientIdButton.Name = "ValidateClientIdButton";
		this.ValidateClientIdButton.Size = new System.Drawing.Size(87, 23);
		this.ValidateClientIdButton.TabIndex = 5;
		this.ValidateClientIdButton.Text = "Validate";
		this.ValidateClientIdButton.UseVisualStyleBackColor = true;
		this.ValidateClientIdButton.Click += new System.EventHandler(ValidateClientIdButton_Click);
		this.ClientIdInfoLabel.AutoSize = true;
		this.ClientIdInfoLabel.Location = new System.Drawing.Point(125, 53);
		this.ClientIdInfoLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
		this.ClientIdInfoLabel.Name = "ClientIdInfoLabel";
		this.ClientIdInfoLabel.SaveToConfig = false;
		this.ClientIdInfoLabel.Size = new System.Drawing.Size(144, 13);
		this.ClientIdInfoLabel.TabIndex = 4;
		this.ClientIdInfoLabel.Text = "Client ID is a 10-digit number.";
		this.ClientIdTextBox.Location = new System.Drawing.Point(128, 27);
		this.ClientIdTextBox.Name = "ClientIdTextBox";
		this.ClientIdTextBox.SaveToConfig = true;
		this.ClientIdTextBox.Size = new System.Drawing.Size(116, 20);
		this.ClientIdTextBox.TabIndex = 1;
		this.ClientIdTextBox.TextChanged += new System.EventHandler(ClientIdTextBox_TextChanged);
		this.LayerStackLabel.AutoSize = true;
		this.LayerStackLabel.Location = new System.Drawing.Point(32, 34);
		this.LayerStackLabel.Name = "LayerStackLabel";
		this.LayerStackLabel.SaveToConfig = false;
		this.LayerStackLabel.Size = new System.Drawing.Size(67, 13);
		this.LayerStackLabel.TabIndex = 2;
		this.LayerStackLabel.Text = "Layer Stack:";
		this.LayerStackComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.LayerStackComboBox.FormattingEnabled = true;
		this.LayerStackComboBox.Location = new System.Drawing.Point(128, 31);
		this.LayerStackComboBox.Name = "LayerStackComboBox";
		this.LayerStackComboBox.SaveToConfig = true;
		this.LayerStackComboBox.Size = new System.Drawing.Size(160, 21);
		this.LayerStackComboBox.TabIndex = 6;
		this.LayerStackComboBox.SelectedIndexChanged += new System.EventHandler(LayerStackComboBox_SelectedIndexChanged);
		this.LayerStackGroupBox.Controls.Add(this.LayerStackInfoLabel);
		this.LayerStackGroupBox.Controls.Add(this.LayerStackLabel);
		this.LayerStackGroupBox.Controls.Add(this.LayerStackComboBox);
		this.LayerStackGroupBox.Location = new System.Drawing.Point(12, 173);
		this.LayerStackGroupBox.Name = "LayerStackGroupBox";
		this.LayerStackGroupBox.Size = new System.Drawing.Size(370, 116);
		this.LayerStackGroupBox.TabIndex = 7;
		this.LayerStackGroupBox.TabStop = false;
		this.LayerStackGroupBox.Text = "Layer Stack";
		this.LayerStackInfoLabel.Location = new System.Drawing.Point(32, 64);
		this.LayerStackInfoLabel.Name = "LayerStackInfoLabel";
		this.LayerStackInfoLabel.SaveToConfig = false;
		this.LayerStackInfoLabel.Size = new System.Drawing.Size(312, 40);
		this.LayerStackInfoLabel.TabIndex = 7;
		this.LayerStackInfoLabel.Text = "A \"Layer Stack\" provides access to certain collections of images and geographic areas.";
		this.LayerStackInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.HiddenAppIdLabel.AutoSize = true;
		this.HiddenAppIdLabel.Location = new System.Drawing.Point(9, 324);
		this.HiddenAppIdLabel.Name = "HiddenAppIdLabel";
		this.HiddenAppIdLabel.SaveToConfig = false;
		this.HiddenAppIdLabel.Size = new System.Drawing.Size(69, 13);
		this.HiddenAppIdLabel.TabIndex = 9;
		this.HiddenAppIdLabel.Text = "HiddenAppId";
		this.HiddenAppIdLabel.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.mCancelButton;
		base.ClientSize = new System.Drawing.Size(394, 354);
		base.Controls.Add(this.HiddenAppIdLabel);
		base.Controls.Add(this.LayerStackGroupBox);
		base.Controls.Add(this.ClientInfoGroup);
		base.Controls.Add(this.mOkButton);
		base.Controls.Add(this.mCancelButton);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "DigitalGlobeSettingsDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "DigitalGlobe Settings";
		this.ClientInfoGroup.ResumeLayout(false);
		this.ClientInfoGroup.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ValidatedIcon).EndInit();
		this.ValidationInfoPanel.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ValidationInfoIcon).EndInit();
		this.LayerStackGroupBox.ResumeLayout(false);
		this.LayerStackGroupBox.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
