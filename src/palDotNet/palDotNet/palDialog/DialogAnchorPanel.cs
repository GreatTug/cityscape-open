using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using palDotNet.Properties;

namespace palDotNet.palDialog;

public class DialogAnchorPanel : UserControl, IDialogValue
{
	public enum AnchorValues
	{
		eTopLeft,
		eTopCenter,
		eTopRight,
		eMiddleLeft,
		eMiddleCenter,
		eMiddleRight,
		eBottomLeft,
		eBottomCenter,
		eBottomRight
	}

	public enum AnchorModes
	{
		eGrow,
		eShrink
	}

	private IContainer components;

	private RadioButton mTopLeft;

	private RadioButton mTopCenter;

	private RadioButton mTopRight;

	private RadioButton mMiddleRight;

	private RadioButton mMiddleCenter;

	private RadioButton mMiddleLeft;

	private RadioButton mBottomRight;

	private RadioButton mBottomCenter;

	private RadioButton mBottomLeft;

	private Panel ButtonGridPanel;

	private DialogComboBox AnchorComboBox;

	private DialogLabel AnchorLabel;

	private AnchorValues mCurrentAnchorValue;

	private AnchorModes mAnchorModeX;

	private AnchorModes mAnchorModeY;

	private bool mIsHandlingInternal;

	public AnchorModes AnchorModeX
	{
		get
		{
			return mAnchorModeX;
		}
		set
		{
			mAnchorModeX = value;
			SyncButtonGridImages();
		}
	}

	public AnchorModes AnchorModeY
	{
		get
		{
			return mAnchorModeY;
		}
		set
		{
			mAnchorModeY = value;
			SyncButtonGridImages();
		}
	}

	[ReadOnly(true)]
	[DialogValueType(EnumDialogValueTypes.eString)]
	public object DialogValue
	{
		get
		{
			if (AnchorComboBox.SelectedIndex < 0 || AnchorComboBox.Items[AnchorComboBox.SelectedIndex] == null)
			{
				return "";
			}
			return AnchorComboBox.Items[AnchorComboBox.SelectedIndex];
		}
		set
		{
			LastRequestedValue = string.Empty;
			for (int i = 0; i < AnchorComboBox.Items.Count; i++)
			{
				if (AnchorComboBox.Items[i].Equals(value))
				{
					AnchorComboBox.SelectedIndex = i;
					return;
				}
			}
			LastRequestedValue = (string)value;
		}
	}

	[ReadOnly(true)]
	public bool LoadedFromSettings { get; set; }

	public bool SaveToConfig { get; set; }

	[ReadOnly(true)]
	public string LastRequestedValue { get; private set; }

	protected override CreateParams CreateParams
	{
		get
		{
			CreateParams createParams = base.CreateParams;
			createParams.ExStyle |= 32;
			return createParams;
		}
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
		this.mTopLeft = new System.Windows.Forms.RadioButton();
		this.mTopCenter = new System.Windows.Forms.RadioButton();
		this.mTopRight = new System.Windows.Forms.RadioButton();
		this.mMiddleRight = new System.Windows.Forms.RadioButton();
		this.mMiddleCenter = new System.Windows.Forms.RadioButton();
		this.mMiddleLeft = new System.Windows.Forms.RadioButton();
		this.mBottomRight = new System.Windows.Forms.RadioButton();
		this.mBottomCenter = new System.Windows.Forms.RadioButton();
		this.mBottomLeft = new System.Windows.Forms.RadioButton();
		this.ButtonGridPanel = new System.Windows.Forms.Panel();
		this.AnchorLabel = new palDotNet.palDialog.DialogLabel();
		this.AnchorComboBox = new palDotNet.palDialog.DialogComboBox();
		this.ButtonGridPanel.SuspendLayout();
		base.SuspendLayout();
		this.mTopLeft.Appearance = System.Windows.Forms.Appearance.Button;
		this.mTopLeft.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.mTopLeft.Image = palDotNet.Properties.Resources.Anchor_TopLeft;
		this.mTopLeft.Location = new System.Drawing.Point(1, 1);
		this.mTopLeft.Margin = new System.Windows.Forms.Padding(0);
		this.mTopLeft.Name = "mTopLeft";
		this.mTopLeft.Size = new System.Drawing.Size(30, 30);
		this.mTopLeft.TabIndex = 0;
		this.mTopLeft.TabStop = true;
		this.mTopLeft.UseVisualStyleBackColor = true;
		this.mTopLeft.CheckedChanged += new System.EventHandler(HandleCheckedChanged);
		this.mTopCenter.Appearance = System.Windows.Forms.Appearance.Button;
		this.mTopCenter.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.mTopCenter.Image = palDotNet.Properties.Resources.Anchor_TopMiddle;
		this.mTopCenter.Location = new System.Drawing.Point(31, 1);
		this.mTopCenter.Margin = new System.Windows.Forms.Padding(0);
		this.mTopCenter.Name = "mTopCenter";
		this.mTopCenter.Size = new System.Drawing.Size(30, 30);
		this.mTopCenter.TabIndex = 1;
		this.mTopCenter.TabStop = true;
		this.mTopCenter.UseVisualStyleBackColor = true;
		this.mTopCenter.CheckedChanged += new System.EventHandler(HandleCheckedChanged);
		this.mTopRight.Appearance = System.Windows.Forms.Appearance.Button;
		this.mTopRight.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.mTopRight.Image = palDotNet.Properties.Resources.Anchor_TopRight;
		this.mTopRight.Location = new System.Drawing.Point(61, 1);
		this.mTopRight.Margin = new System.Windows.Forms.Padding(0);
		this.mTopRight.Name = "mTopRight";
		this.mTopRight.Size = new System.Drawing.Size(30, 30);
		this.mTopRight.TabIndex = 2;
		this.mTopRight.TabStop = true;
		this.mTopRight.UseVisualStyleBackColor = true;
		this.mTopRight.CheckedChanged += new System.EventHandler(HandleCheckedChanged);
		this.mMiddleRight.Appearance = System.Windows.Forms.Appearance.Button;
		this.mMiddleRight.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.mMiddleRight.Image = palDotNet.Properties.Resources.Anchor_MiddleRight;
		this.mMiddleRight.Location = new System.Drawing.Point(61, 31);
		this.mMiddleRight.Margin = new System.Windows.Forms.Padding(0);
		this.mMiddleRight.Name = "mMiddleRight";
		this.mMiddleRight.Size = new System.Drawing.Size(30, 30);
		this.mMiddleRight.TabIndex = 5;
		this.mMiddleRight.TabStop = true;
		this.mMiddleRight.UseVisualStyleBackColor = true;
		this.mMiddleRight.CheckedChanged += new System.EventHandler(HandleCheckedChanged);
		this.mMiddleCenter.Appearance = System.Windows.Forms.Appearance.Button;
		this.mMiddleCenter.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.mMiddleCenter.Image = palDotNet.Properties.Resources.Anchor_MiddleGrid;
		this.mMiddleCenter.Location = new System.Drawing.Point(31, 31);
		this.mMiddleCenter.Margin = new System.Windows.Forms.Padding(0);
		this.mMiddleCenter.Name = "mMiddleCenter";
		this.mMiddleCenter.Size = new System.Drawing.Size(30, 30);
		this.mMiddleCenter.TabIndex = 4;
		this.mMiddleCenter.TabStop = true;
		this.mMiddleCenter.UseVisualStyleBackColor = true;
		this.mMiddleCenter.CheckedChanged += new System.EventHandler(HandleCheckedChanged);
		this.mMiddleLeft.Appearance = System.Windows.Forms.Appearance.Button;
		this.mMiddleLeft.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.mMiddleLeft.Image = palDotNet.Properties.Resources.Anchor_MiddleLeft;
		this.mMiddleLeft.Location = new System.Drawing.Point(1, 31);
		this.mMiddleLeft.Margin = new System.Windows.Forms.Padding(0);
		this.mMiddleLeft.Name = "mMiddleLeft";
		this.mMiddleLeft.Size = new System.Drawing.Size(30, 30);
		this.mMiddleLeft.TabIndex = 3;
		this.mMiddleLeft.TabStop = true;
		this.mMiddleLeft.UseVisualStyleBackColor = true;
		this.mMiddleLeft.CheckedChanged += new System.EventHandler(HandleCheckedChanged);
		this.mBottomRight.Appearance = System.Windows.Forms.Appearance.Button;
		this.mBottomRight.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.mBottomRight.Image = palDotNet.Properties.Resources.Anchor_BottomRight;
		this.mBottomRight.Location = new System.Drawing.Point(61, 61);
		this.mBottomRight.Margin = new System.Windows.Forms.Padding(0);
		this.mBottomRight.Name = "mBottomRight";
		this.mBottomRight.Size = new System.Drawing.Size(30, 30);
		this.mBottomRight.TabIndex = 8;
		this.mBottomRight.TabStop = true;
		this.mBottomRight.UseVisualStyleBackColor = true;
		this.mBottomRight.CheckedChanged += new System.EventHandler(HandleCheckedChanged);
		this.mBottomCenter.Appearance = System.Windows.Forms.Appearance.Button;
		this.mBottomCenter.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.mBottomCenter.Image = palDotNet.Properties.Resources.Anchor_BottomMiddle;
		this.mBottomCenter.Location = new System.Drawing.Point(31, 61);
		this.mBottomCenter.Margin = new System.Windows.Forms.Padding(0);
		this.mBottomCenter.Name = "mBottomCenter";
		this.mBottomCenter.Size = new System.Drawing.Size(30, 30);
		this.mBottomCenter.TabIndex = 7;
		this.mBottomCenter.TabStop = true;
		this.mBottomCenter.UseVisualStyleBackColor = true;
		this.mBottomCenter.CheckedChanged += new System.EventHandler(HandleCheckedChanged);
		this.mBottomLeft.Appearance = System.Windows.Forms.Appearance.Button;
		this.mBottomLeft.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.mBottomLeft.Image = palDotNet.Properties.Resources.Anchor_BottomLeft;
		this.mBottomLeft.Location = new System.Drawing.Point(1, 61);
		this.mBottomLeft.Margin = new System.Windows.Forms.Padding(0);
		this.mBottomLeft.Name = "mBottomLeft";
		this.mBottomLeft.Size = new System.Drawing.Size(30, 30);
		this.mBottomLeft.TabIndex = 6;
		this.mBottomLeft.TabStop = true;
		this.mBottomLeft.UseVisualStyleBackColor = true;
		this.mBottomLeft.CheckedChanged += new System.EventHandler(HandleCheckedChanged);
		this.ButtonGridPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.ButtonGridPanel.BackColor = System.Drawing.SystemColors.Control;
		this.ButtonGridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.ButtonGridPanel.Controls.Add(this.mTopLeft);
		this.ButtonGridPanel.Controls.Add(this.mBottomRight);
		this.ButtonGridPanel.Controls.Add(this.mTopCenter);
		this.ButtonGridPanel.Controls.Add(this.mBottomCenter);
		this.ButtonGridPanel.Controls.Add(this.mTopRight);
		this.ButtonGridPanel.Controls.Add(this.mBottomLeft);
		this.ButtonGridPanel.Controls.Add(this.mMiddleLeft);
		this.ButtonGridPanel.Controls.Add(this.mMiddleRight);
		this.ButtonGridPanel.Controls.Add(this.mMiddleCenter);
		this.ButtonGridPanel.Location = new System.Drawing.Point(148, 3);
		this.ButtonGridPanel.Name = "ButtonGridPanel";
		this.ButtonGridPanel.Size = new System.Drawing.Size(94, 94);
		this.ButtonGridPanel.TabIndex = 2;
		this.AnchorLabel.AutoSize = true;
		this.AnchorLabel.BackColor = System.Drawing.SystemColors.Control;
		this.AnchorLabel.Location = new System.Drawing.Point(1, 3);
		this.AnchorLabel.Name = "AnchorLabel";
		this.AnchorLabel.Size = new System.Drawing.Size(44, 13);
		this.AnchorLabel.TabIndex = 0;
		this.AnchorLabel.Text = "Anchor:";
		this.AnchorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.AnchorComboBox.FormattingEnabled = true;
		this.AnchorComboBox.Items.AddRange(new object[9] { "Top Left", "Top Center", "Top Right", "Middle Left", "Middle Center", "Middle Right", "Bottom Left", "Bottom Center", "Bottom Right" });
		this.AnchorComboBox.Location = new System.Drawing.Point(3, 22);
		this.AnchorComboBox.Name = "AnchorComboBox";
		this.AnchorComboBox.Size = new System.Drawing.Size(121, 21);
		this.AnchorComboBox.TabIndex = 1;
		this.AnchorComboBox.SelectedIndexChanged += new System.EventHandler(AnchorComboBox_SelectedIndexChanged);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.Controls.Add(this.AnchorLabel);
		base.Controls.Add(this.AnchorComboBox);
		base.Controls.Add(this.ButtonGridPanel);
		base.Name = "DialogAnchorPanel";
		base.Size = new System.Drawing.Size(243, 100);
		this.ButtonGridPanel.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public DialogAnchorPanel()
	{
		SaveToConfig = true;
		InitializeComponent();
		mIsHandlingInternal = false;
		SetDefaultConfig();
	}

	public void SetDefaultConfig()
	{
		mCurrentAnchorValue = AnchorValues.eMiddleCenter;
		mAnchorModeX = AnchorModes.eGrow;
		mAnchorModeY = AnchorModes.eGrow;
		SyncPanel();
	}

	private void AnchorComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (!mIsHandlingInternal)
		{
			mCurrentAnchorValue = (AnchorValues)AnchorComboBox.SelectedIndex;
			SyncPanel();
		}
	}

	private void HandleCheckedChanged(object sender, EventArgs e)
	{
		if (!mIsHandlingInternal)
		{
			if (mTopLeft.Checked)
			{
				mCurrentAnchorValue = AnchorValues.eTopLeft;
			}
			else if (mTopCenter.Checked)
			{
				mCurrentAnchorValue = AnchorValues.eTopCenter;
			}
			else if (mTopRight.Checked)
			{
				mCurrentAnchorValue = AnchorValues.eTopRight;
			}
			else if (mMiddleLeft.Checked)
			{
				mCurrentAnchorValue = AnchorValues.eMiddleLeft;
			}
			else if (mMiddleCenter.Checked)
			{
				mCurrentAnchorValue = AnchorValues.eMiddleCenter;
			}
			else if (mMiddleRight.Checked)
			{
				mCurrentAnchorValue = AnchorValues.eMiddleRight;
			}
			else if (mBottomLeft.Checked)
			{
				mCurrentAnchorValue = AnchorValues.eBottomLeft;
			}
			else if (mBottomCenter.Checked)
			{
				mCurrentAnchorValue = AnchorValues.eBottomCenter;
			}
			else if (mBottomRight.Checked)
			{
				mCurrentAnchorValue = AnchorValues.eBottomRight;
			}
			SyncPanel();
		}
	}

	private void SyncPanel()
	{
		mIsHandlingInternal = true;
		AnchorComboBox.SelectedIndex = (int)mCurrentAnchorValue;
		switch (mCurrentAnchorValue)
		{
		case AnchorValues.eTopLeft:
			mTopLeft.Checked = true;
			break;
		case AnchorValues.eTopCenter:
			mTopCenter.Checked = true;
			break;
		case AnchorValues.eTopRight:
			mTopRight.Checked = true;
			break;
		case AnchorValues.eMiddleLeft:
			mMiddleLeft.Checked = true;
			break;
		case AnchorValues.eMiddleCenter:
			mMiddleCenter.Checked = true;
			break;
		case AnchorValues.eMiddleRight:
			mMiddleRight.Checked = true;
			break;
		case AnchorValues.eBottomLeft:
			mBottomLeft.Checked = true;
			break;
		case AnchorValues.eBottomCenter:
			mBottomCenter.Checked = true;
			break;
		case AnchorValues.eBottomRight:
			mBottomRight.Checked = true;
			break;
		}
		SyncButtonGridImages();
		mIsHandlingInternal = false;
	}

	private void SyncButtonGridImages()
	{
		ClearButtonGridImages();
		Bitmap[] array = new Bitmap[9]
		{
			null,
			null,
			null,
			null,
			Resources.Anchor_MiddleGrid,
			null,
			null,
			null,
			null
		};
		if (mAnchorModeX == AnchorModes.eGrow)
		{
			array[3] = Resources.Anchor_MiddleLeft;
			array[5] = Resources.Anchor_MiddleRight;
		}
		else
		{
			array[3] = Resources.Anchor_MiddleRight;
			array[5] = Resources.Anchor_MiddleLeft;
		}
		if (mAnchorModeY == AnchorModes.eGrow)
		{
			array[1] = Resources.Anchor_TopMiddle;
			array[7] = Resources.Anchor_BottomMiddle;
		}
		else
		{
			array[1] = Resources.Anchor_BottomMiddle;
			array[7] = Resources.Anchor_TopMiddle;
		}
		if (mAnchorModeX == AnchorModes.eGrow && mAnchorModeY == AnchorModes.eGrow)
		{
			array[0] = Resources.Anchor_TopLeft;
			array[2] = Resources.Anchor_TopRight;
			array[6] = Resources.Anchor_BottomLeft;
			array[8] = Resources.Anchor_BottomRight;
		}
		else
		{
			array[0] = Resources.Anchor_BottomRight;
			array[2] = Resources.Anchor_BottomLeft;
			array[6] = Resources.Anchor_TopRight;
			array[8] = Resources.Anchor_TopLeft;
		}
		if (mTopLeft.Checked)
		{
			mTopLeft.Image = array[4];
			mTopCenter.Image = array[5];
			mMiddleLeft.Image = array[7];
			mMiddleCenter.Image = array[8];
		}
		else if (mTopCenter.Checked)
		{
			mTopLeft.Image = array[3];
			mTopCenter.Image = array[4];
			mTopRight.Image = array[5];
			mMiddleLeft.Image = array[6];
			mMiddleCenter.Image = array[7];
			mMiddleRight.Image = array[8];
		}
		else if (mTopRight.Checked)
		{
			mTopCenter.Image = array[3];
			mTopRight.Image = array[4];
			mMiddleCenter.Image = array[6];
			mMiddleRight.Image = array[7];
		}
		else if (mMiddleLeft.Checked)
		{
			mTopLeft.Image = array[1];
			mTopCenter.Image = array[2];
			mMiddleLeft.Image = array[4];
			mMiddleCenter.Image = array[5];
			mBottomLeft.Image = array[7];
			mBottomCenter.Image = array[8];
		}
		else if (mMiddleCenter.Checked)
		{
			mTopLeft.Image = array[0];
			mTopCenter.Image = array[1];
			mTopRight.Image = array[2];
			mMiddleLeft.Image = array[3];
			mMiddleCenter.Image = array[4];
			mMiddleRight.Image = array[5];
			mBottomLeft.Image = array[6];
			mBottomCenter.Image = array[7];
			mBottomRight.Image = array[8];
		}
		else if (mMiddleRight.Checked)
		{
			mTopCenter.Image = array[0];
			mTopRight.Image = array[1];
			mMiddleCenter.Image = array[3];
			mMiddleRight.Image = array[4];
			mBottomCenter.Image = array[6];
			mBottomRight.Image = array[7];
		}
		else if (mBottomLeft.Checked)
		{
			mMiddleLeft.Image = array[1];
			mMiddleCenter.Image = array[2];
			mBottomLeft.Image = array[4];
			mBottomCenter.Image = array[5];
		}
		else if (mBottomCenter.Checked)
		{
			mMiddleLeft.Image = array[0];
			mMiddleCenter.Image = array[1];
			mMiddleRight.Image = array[2];
			mBottomLeft.Image = array[3];
			mBottomCenter.Image = array[4];
			mBottomRight.Image = array[5];
		}
		else if (mBottomRight.Checked)
		{
			mMiddleCenter.Image = array[0];
			mMiddleRight.Image = array[1];
			mBottomCenter.Image = array[3];
			mBottomRight.Image = array[4];
		}
	}

	private void ClearButtonGridImages()
	{
		mTopLeft.Image = null;
		mTopCenter.Image = null;
		mTopRight.Image = null;
		mMiddleLeft.Image = null;
		mMiddleCenter.Image = null;
		mMiddleRight.Image = null;
		mBottomLeft.Image = null;
		mBottomCenter.Image = null;
		mBottomRight.Image = null;
	}

	public void AddToolTip(ToolTip toolTip, string dialogName, string varName)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (object item in AnchorComboBox.Items)
		{
			if (stringBuilder.Length != 0)
			{
				stringBuilder.Append("|");
			}
			stringBuilder.Append(item.ToString());
		}
		string caption = string.Concat("script command: ", Environment.NewLine, "'", dialogName, " -", varName, " <", stringBuilder, ">'");
		toolTip.SetToolTip(this, caption);
	}

	protected void InvalidateEx()
	{
		if (base.Parent != null)
		{
			Rectangle rc = new Rectangle(base.Location, base.Size);
			base.Parent.Invalidate(rc, invalidateChildren: true);
		}
	}

	protected override void OnPaintBackground(PaintEventArgs pevent)
	{
	}
}
