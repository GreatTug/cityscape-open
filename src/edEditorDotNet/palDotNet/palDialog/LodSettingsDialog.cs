using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace palDotNet.palDialog;

[CPPCodeGeneration(OutputFile = "..\\edEditor\\edLod\\LodSettingsDialog", Win32PCOnly = true)]
[DialogBoxCreator]
public class LodSettingsDialog : DialogBoxBase
{
	private int LodCount;

	private string LodCountDialogGuidStr;

	private string[] LodListItems;

	private string[] OverheadResolutionItems;

	private IContainer components;

	private Button mOkButton;

	private Button mCancelButton;

	private Button mSetLodCountButton;

	private DialogListBox mLodChooserListBox;

	private GroupBox RoadLodGroupBox;

	private DialogNumericUpDownInt BridgePostSlicesInput;

	private DialogNumericUpDownFloat MaxSegmentLengthInput;

	private DialogNumericUpDownFloat ExtrusionToleranceInput;

	private DialogNumericUpDownFloat PathToleranceInput;

	private DialogLabel BridgePostSlicesLabel;

	private DialogLabel MaxSegmentLengthLabel;

	private DialogLabel ExtrusionToleranceLabel;

	private DialogLabel PathToleranceLabel;

	private GroupBox HeightfieldLodGroupBox;

	private DialogNumericUpDownFloat ShorelineToleranceInput;

	private DialogLabel ShorelineToleranceLabel;

	private DialogLabel HeightToleranceLabel;

	private DialogNumericUpDownFloat HeightToleranceInput;

	private GroupBox TileLodGroupBox;

	private DialogCheckGroupBox OverheadRenderCheckGroupBox;

	private DialogLabel OverheadResolutionLabel;

	private DialogComboBox OverheadRenderResolutionComboBox;

	private DialogLabel CullDistanceLabel;

	private DialogNumericUpDownFloat CullDistanceInput;

	private GroupBox TerrainSettingsGroupBox;

	private DialogMessagePanel mMessagePanel;

	private DialogCheckBox GlobalDetailInOverheadCheckBox;

	private DialogCheckBox TrimRoadsInOverheadCheckBox;

	private GroupBox globalGroup;

	private DialogCheckBox PackProps;

	public Button SetLodCountButton => mSetLodCountButton;

	public DialogListBox LodChooserListBox => mLodChooserListBox;

	public float CullDistance
	{
		get
		{
			return (float)CullDistanceInput.Value;
		}
		set
		{
			CullDistanceInput.Value = (decimal)value;
		}
	}

	public bool OverheadRender
	{
		get
		{
			return OverheadRenderCheckGroupBox.Checked;
		}
		set
		{
			OverheadRenderCheckGroupBox.Checked = value;
		}
	}

	public int OverheadResolutionIndex
	{
		get
		{
			return OverheadRenderResolutionComboBox.SelectedIndex;
		}
		set
		{
			OverheadRenderResolutionComboBox.SelectedIndex = value;
		}
	}

	public bool TrimRoadsInOverhead
	{
		get
		{
			return TrimRoadsInOverheadCheckBox.Checked;
		}
		set
		{
			TrimRoadsInOverheadCheckBox.Checked = value;
		}
	}

	public bool GlobalDetailInOverhead
	{
		get
		{
			return GlobalDetailInOverheadCheckBox.Checked;
		}
		set
		{
			GlobalDetailInOverheadCheckBox.Checked = value;
		}
	}

	public float HeightTolerance
	{
		get
		{
			return (float)HeightToleranceInput.Value;
		}
		set
		{
			HeightToleranceInput.Value = (decimal)value;
		}
	}

	public float ShorelineTolerance
	{
		get
		{
			return (float)ShorelineToleranceInput.Value;
		}
		set
		{
			ShorelineToleranceInput.Value = (decimal)value;
		}
	}

	public float PathTolerance
	{
		get
		{
			return (float)PathToleranceInput.Value;
		}
		set
		{
			PathToleranceInput.Value = (decimal)value;
		}
	}

	public float ExtrusionTolerance
	{
		get
		{
			return (float)ExtrusionToleranceInput.Value;
		}
		set
		{
			ExtrusionToleranceInput.Value = (decimal)value;
		}
	}

	public float MaxSegmentLength
	{
		get
		{
			return (float)MaxSegmentLengthInput.Value;
		}
		set
		{
			MaxSegmentLengthInput.Value = (decimal)value;
		}
	}

	public int BridgePostSlices
	{
		get
		{
			return (int)BridgePostSlicesInput.Value;
		}
		set
		{
			BridgePostSlicesInput.Value = value;
		}
	}

	public Button OkButton => mOkButton;

	public event LodValueChangedHandler LodValueChanged;

	public LodSettingsDialog()
	{
		InitializeComponent();
	}

	public int GetLodCount()
	{
		return LodCount;
	}

	public void SetLodCount(int lodCount)
	{
		LodCount = lodCount;
	}

	public void SetLodCountDialogGuidStr(string dialogGuidStr)
	{
		LodCountDialogGuidStr = dialogGuidStr;
	}

	public DialogResult ShowLodCountDialog()
	{
		Guid guid = new Guid(LodCountDialogGuidStr);
		DialogBoxBase dialogBoxBase = DialogBoxBase.FindByGuid(guid);
		if (dialogBoxBase == null)
		{
			return DialogResult.None;
		}
		if (!(dialogBoxBase is LodCountDialog lodCountDialog))
		{
			return DialogResult.None;
		}
		lodCountDialog.Init(LodCount);
		DialogResult dialogResult = lodCountDialog.ShowDialogFromParentDialog();
		if (dialogResult == DialogResult.OK)
		{
			LodCount = lodCountDialog.GetLodCount();
		}
		return dialogResult;
	}

	public void GenerateLodListItems(int lodCount)
	{
		if (lodCount < 1)
		{
			return;
		}
		LodListItems = new string[lodCount];
		if (lodCount == 1)
		{
			LodListItems[0] = "Single LOD";
		}
		else
		{
			int num = lodCount - 1;
			LodListItems[0] = "LOD 0 (Highest)";
			for (int i = 1; i < num; i++)
			{
				LodListItems[i] = "LOD " + i;
			}
			LodListItems[num] = "LOD " + num + " (Lowest)";
		}
		mLodChooserListBox.DataSource = LodListItems;
		mLodChooserListBox.SelectedIndex = 0;
	}

	public void InitOverheadResolutionItems(string[] itemNames)
	{
		OverheadResolutionItems = itemNames;
		OverheadRenderResolutionComboBox.DataSource = OverheadResolutionItems;
		OverheadRenderResolutionComboBox.SelectedIndex = -1;
	}

	private void OverheadRenderCheckGroupBox_CheckedChanged(object sender, EventArgs e)
	{
		int selectedIndex = mLodChooserListBox.SelectedIndex;
		if (selectedIndex != -1)
		{
			if (OverheadRenderCheckGroupBox.Checked && OverheadRenderResolutionComboBox.SelectedIndex < 0)
			{
				OverheadRenderResolutionComboBox.SelectedIndex = 0;
			}
			TrimRoadsInOverheadCheckBox.Enabled = OverheadRenderCheckGroupBox.Checked;
			GlobalDetailInOverheadCheckBox.Enabled = OverheadRenderCheckGroupBox.Checked;
		}
	}

	public void SetCullDistanceRange(float cdMin, float cdMax, float cdInc)
	{
		CullDistanceInput.Minimum = (decimal)cdMin;
		CullDistanceInput.Maximum = (decimal)cdMax;
		CullDistanceInput.Increment = (decimal)cdInc;
	}

	public void SetHeightToleranceRange(float htMin, float htMax, float htInc)
	{
		HeightToleranceInput.Minimum = (decimal)htMin;
		HeightToleranceInput.Maximum = (decimal)htMax;
		HeightToleranceInput.Increment = (decimal)htInc;
	}

	public void SetShorelineToleranceRange(float stMin, float stMax, float stInc)
	{
		ShorelineToleranceInput.Minimum = (decimal)stMin;
		ShorelineToleranceInput.Maximum = (decimal)stMax;
		ShorelineToleranceInput.Increment = (decimal)stInc;
	}

	public void SetPathToleranceRange(float ptMin, float ptMax, float ptInc)
	{
		PathToleranceInput.Minimum = (decimal)ptMin;
		PathToleranceInput.Maximum = (decimal)ptMax;
		PathToleranceInput.Increment = (decimal)ptInc;
	}

	public void SetExtrusionToleranceRange(float etMin, float etMax, float etInc)
	{
		ExtrusionToleranceInput.Minimum = (decimal)etMin;
		ExtrusionToleranceInput.Maximum = (decimal)etMax;
		ExtrusionToleranceInput.Increment = (decimal)etInc;
	}

	public void SetMaxSegmentLengthRange(float mslMin, float mslMax, float mslInc)
	{
		MaxSegmentLengthInput.Minimum = (decimal)mslMin;
		MaxSegmentLengthInput.Maximum = (decimal)mslMax;
		MaxSegmentLengthInput.Increment = (decimal)mslInc;
	}

	public void SetBridgePostSlicesRange(int bpsMin, int bpsMax, int bpsInc)
	{
		BridgePostSlicesInput.Minimum = bpsMin;
		BridgePostSlicesInput.Maximum = bpsMax;
		BridgePostSlicesInput.Increment = bpsInc;
	}

	public void SetOkResult()
	{
		base.DialogResult = DialogResult.OK;
	}

	protected virtual void OnLodValueChanged(EventArgs e)
	{
		if (LodValueChanged != null)
		{
			LodValueChanged(this, e);
		}
	}

	private void TerrainSettings_ValueChanged(object sender, EventArgs e)
	{
		OnLodValueChanged(new EventArgs());
	}

	public void PostDialogError(string msg)
	{
		mMessagePanel.ErrorMessage(msg);
		mOkButton.Enabled = false;
	}

	public void ClearDialogErrors()
	{
		mMessagePanel.ClearMessage();
		mOkButton.Enabled = true;
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
		this.mOkButton = new System.Windows.Forms.Button();
		this.mCancelButton = new System.Windows.Forms.Button();
		this.mSetLodCountButton = new System.Windows.Forms.Button();
		this.mLodChooserListBox = new palDotNet.palDialog.DialogListBox();
		this.RoadLodGroupBox = new System.Windows.Forms.GroupBox();
		this.BridgePostSlicesInput = new palDotNet.palDialog.DialogNumericUpDownInt();
		this.MaxSegmentLengthInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.ExtrusionToleranceInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.PathToleranceInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.BridgePostSlicesLabel = new palDotNet.palDialog.DialogLabel();
		this.MaxSegmentLengthLabel = new palDotNet.palDialog.DialogLabel();
		this.ExtrusionToleranceLabel = new palDotNet.palDialog.DialogLabel();
		this.PathToleranceLabel = new palDotNet.palDialog.DialogLabel();
		this.HeightfieldLodGroupBox = new System.Windows.Forms.GroupBox();
		this.ShorelineToleranceInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.ShorelineToleranceLabel = new palDotNet.palDialog.DialogLabel();
		this.HeightToleranceLabel = new palDotNet.palDialog.DialogLabel();
		this.HeightToleranceInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.TileLodGroupBox = new System.Windows.Forms.GroupBox();
		this.CullDistanceLabel = new palDotNet.palDialog.DialogLabel();
		this.CullDistanceInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.OverheadRenderCheckGroupBox = new palDotNet.palDialog.DialogCheckGroupBox();
		this.GlobalDetailInOverheadCheckBox = new palDotNet.palDialog.DialogCheckBox();
		this.OverheadRenderResolutionComboBox = new palDotNet.palDialog.DialogComboBox();
		this.OverheadResolutionLabel = new palDotNet.palDialog.DialogLabel();
		this.TrimRoadsInOverheadCheckBox = new palDotNet.palDialog.DialogCheckBox();
		this.TerrainSettingsGroupBox = new System.Windows.Forms.GroupBox();
		this.mMessagePanel = new palDotNet.palDialog.DialogMessagePanel();
		this.globalGroup = new System.Windows.Forms.GroupBox();
		this.PackProps = new palDotNet.palDialog.DialogCheckBox();
		this.RoadLodGroupBox.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.BridgePostSlicesInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.MaxSegmentLengthInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ExtrusionToleranceInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.PathToleranceInput).BeginInit();
		this.HeightfieldLodGroupBox.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ShorelineToleranceInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.HeightToleranceInput).BeginInit();
		this.TileLodGroupBox.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.CullDistanceInput).BeginInit();
		this.OverheadRenderCheckGroupBox.SuspendLayout();
		this.TerrainSettingsGroupBox.SuspendLayout();
		this.globalGroup.SuspendLayout();
		base.SuspendLayout();
		this.mOkButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.mOkButton.Location = new System.Drawing.Point(346, 564);
		this.mOkButton.Name = "mOkButton";
		this.mOkButton.Size = new System.Drawing.Size(75, 23);
		this.mOkButton.TabIndex = 3;
		this.mOkButton.Text = "OK";
		this.mOkButton.UseVisualStyleBackColor = true;
		this.mCancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.mCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.mCancelButton.Location = new System.Drawing.Point(427, 564);
		this.mCancelButton.Name = "mCancelButton";
		this.mCancelButton.Size = new System.Drawing.Size(75, 23);
		this.mCancelButton.TabIndex = 4;
		this.mCancelButton.Text = "Cancel";
		this.mCancelButton.UseVisualStyleBackColor = true;
		this.mSetLodCountButton.Location = new System.Drawing.Point(14, 28);
		this.mSetLodCountButton.Name = "mSetLodCountButton";
		this.mSetLodCountButton.Size = new System.Drawing.Size(130, 23);
		this.mSetLodCountButton.TabIndex = 0;
		this.mSetLodCountButton.Text = "Set LOD Count...";
		this.mSetLodCountButton.UseVisualStyleBackColor = true;
		this.mLodChooserListBox.FormattingEnabled = true;
		this.mLodChooserListBox.IntegralHeight = false;
		this.mLodChooserListBox.Items.AddRange(new object[3] { "LOD 0", "LOD 1", "LOD 2" });
		this.mLodChooserListBox.Location = new System.Drawing.Point(14, 71);
		this.mLodChooserListBox.Name = "mLodChooserListBox";
		this.mLodChooserListBox.SaveToConfig = true;
		this.mLodChooserListBox.Size = new System.Drawing.Size(130, 407);
		this.mLodChooserListBox.Sorted = true;
		this.mLodChooserListBox.TabIndex = 1;
		this.RoadLodGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.RoadLodGroupBox.Controls.Add(this.BridgePostSlicesInput);
		this.RoadLodGroupBox.Controls.Add(this.MaxSegmentLengthInput);
		this.RoadLodGroupBox.Controls.Add(this.ExtrusionToleranceInput);
		this.RoadLodGroupBox.Controls.Add(this.PathToleranceInput);
		this.RoadLodGroupBox.Controls.Add(this.BridgePostSlicesLabel);
		this.RoadLodGroupBox.Controls.Add(this.MaxSegmentLengthLabel);
		this.RoadLodGroupBox.Controls.Add(this.ExtrusionToleranceLabel);
		this.RoadLodGroupBox.Controls.Add(this.PathToleranceLabel);
		this.RoadLodGroupBox.Location = new System.Drawing.Point(166, 318);
		this.RoadLodGroupBox.Name = "RoadLodGroupBox";
		this.RoadLodGroupBox.Size = new System.Drawing.Size(310, 160);
		this.RoadLodGroupBox.TabIndex = 5;
		this.RoadLodGroupBox.TabStop = false;
		this.RoadLodGroupBox.Text = "Road LOD";
		this.BridgePostSlicesInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.BridgePostSlicesInput.Location = new System.Drawing.Point(210, 124);
		this.BridgePostSlicesInput.Maximum = new decimal(new int[4] { 10000000, 0, 0, 0 });
		this.BridgePostSlicesInput.Name = "BridgePostSlicesInput";
		this.BridgePostSlicesInput.SaveToConfig = true;
		this.BridgePostSlicesInput.Size = new System.Drawing.Size(80, 20);
		this.BridgePostSlicesInput.TabIndex = 7;
		this.BridgePostSlicesInput.ValueChanged += new System.EventHandler(TerrainSettings_ValueChanged);
		this.MaxSegmentLengthInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.MaxSegmentLengthInput.DecimalPlaces = 2;
		this.MaxSegmentLengthInput.Location = new System.Drawing.Point(210, 92);
		this.MaxSegmentLengthInput.Maximum = new decimal(new int[4] { 10000000, 0, 0, 0 });
		this.MaxSegmentLengthInput.Name = "MaxSegmentLengthInput";
		this.MaxSegmentLengthInput.SaveToConfig = true;
		this.MaxSegmentLengthInput.Size = new System.Drawing.Size(80, 20);
		this.MaxSegmentLengthInput.TabIndex = 5;
		this.MaxSegmentLengthInput.ValueChanged += new System.EventHandler(TerrainSettings_ValueChanged);
		this.ExtrusionToleranceInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.ExtrusionToleranceInput.DecimalPlaces = 2;
		this.ExtrusionToleranceInput.Location = new System.Drawing.Point(210, 60);
		this.ExtrusionToleranceInput.Maximum = new decimal(new int[4] { 10000000, 0, 0, 0 });
		this.ExtrusionToleranceInput.Name = "ExtrusionToleranceInput";
		this.ExtrusionToleranceInput.SaveToConfig = true;
		this.ExtrusionToleranceInput.Size = new System.Drawing.Size(80, 20);
		this.ExtrusionToleranceInput.TabIndex = 3;
		this.ExtrusionToleranceInput.ValueChanged += new System.EventHandler(TerrainSettings_ValueChanged);
		this.PathToleranceInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.PathToleranceInput.DecimalPlaces = 2;
		this.PathToleranceInput.Location = new System.Drawing.Point(210, 28);
		this.PathToleranceInput.Maximum = new decimal(new int[4] { 10000000, 0, 0, 0 });
		this.PathToleranceInput.Name = "PathToleranceInput";
		this.PathToleranceInput.SaveToConfig = true;
		this.PathToleranceInput.Size = new System.Drawing.Size(80, 20);
		this.PathToleranceInput.TabIndex = 1;
		this.PathToleranceInput.ValueChanged += new System.EventHandler(TerrainSettings_ValueChanged);
		this.BridgePostSlicesLabel.AutoSize = true;
		this.BridgePostSlicesLabel.Location = new System.Drawing.Point(18, 126);
		this.BridgePostSlicesLabel.Name = "BridgePostSlicesLabel";
		this.BridgePostSlicesLabel.SaveToConfig = false;
		this.BridgePostSlicesLabel.Size = new System.Drawing.Size(92, 13);
		this.BridgePostSlicesLabel.TabIndex = 6;
		this.BridgePostSlicesLabel.Text = "Bridge post slices:";
		this.MaxSegmentLengthLabel.AutoSize = true;
		this.MaxSegmentLengthLabel.Location = new System.Drawing.Point(18, 94);
		this.MaxSegmentLengthLabel.Name = "MaxSegmentLengthLabel";
		this.MaxSegmentLengthLabel.SaveToConfig = false;
		this.MaxSegmentLengthLabel.Size = new System.Drawing.Size(145, 13);
		this.MaxSegmentLengthLabel.TabIndex = 4;
		this.MaxSegmentLengthLabel.Text = "Max segment length (meters):";
		this.ExtrusionToleranceLabel.AutoSize = true;
		this.ExtrusionToleranceLabel.Location = new System.Drawing.Point(18, 62);
		this.ExtrusionToleranceLabel.Name = "ExtrusionToleranceLabel";
		this.ExtrusionToleranceLabel.SaveToConfig = false;
		this.ExtrusionToleranceLabel.Size = new System.Drawing.Size(140, 13);
		this.ExtrusionToleranceLabel.TabIndex = 2;
		this.ExtrusionToleranceLabel.Text = "Extrusion tolerance (meters):";
		this.PathToleranceLabel.AutoSize = true;
		this.PathToleranceLabel.Location = new System.Drawing.Point(18, 30);
		this.PathToleranceLabel.Name = "PathToleranceLabel";
		this.PathToleranceLabel.SaveToConfig = false;
		this.PathToleranceLabel.Size = new System.Drawing.Size(119, 13);
		this.PathToleranceLabel.TabIndex = 0;
		this.PathToleranceLabel.Text = "Path tolerance (meters):";
		this.HeightfieldLodGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.HeightfieldLodGroupBox.Controls.Add(this.ShorelineToleranceInput);
		this.HeightfieldLodGroupBox.Controls.Add(this.ShorelineToleranceLabel);
		this.HeightfieldLodGroupBox.Controls.Add(this.HeightToleranceLabel);
		this.HeightfieldLodGroupBox.Controls.Add(this.HeightToleranceInput);
		this.HeightfieldLodGroupBox.Location = new System.Drawing.Point(166, 212);
		this.HeightfieldLodGroupBox.Name = "HeightfieldLodGroupBox";
		this.HeightfieldLodGroupBox.Size = new System.Drawing.Size(310, 100);
		this.HeightfieldLodGroupBox.TabIndex = 4;
		this.HeightfieldLodGroupBox.TabStop = false;
		this.HeightfieldLodGroupBox.Text = "Heightfield LOD";
		this.ShorelineToleranceInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.ShorelineToleranceInput.DecimalPlaces = 2;
		this.ShorelineToleranceInput.Location = new System.Drawing.Point(210, 60);
		this.ShorelineToleranceInput.Maximum = new decimal(new int[4] { 10000000, 0, 0, 0 });
		this.ShorelineToleranceInput.Name = "ShorelineToleranceInput";
		this.ShorelineToleranceInput.SaveToConfig = true;
		this.ShorelineToleranceInput.Size = new System.Drawing.Size(80, 20);
		this.ShorelineToleranceInput.TabIndex = 3;
		this.ShorelineToleranceInput.ValueChanged += new System.EventHandler(TerrainSettings_ValueChanged);
		this.ShorelineToleranceLabel.AutoSize = true;
		this.ShorelineToleranceLabel.Location = new System.Drawing.Point(18, 62);
		this.ShorelineToleranceLabel.Name = "ShorelineToleranceLabel";
		this.ShorelineToleranceLabel.SaveToConfig = false;
		this.ShorelineToleranceLabel.Size = new System.Drawing.Size(141, 13);
		this.ShorelineToleranceLabel.TabIndex = 2;
		this.ShorelineToleranceLabel.Text = "Shoreline tolerance (meters):";
		this.HeightToleranceLabel.AutoSize = true;
		this.HeightToleranceLabel.Location = new System.Drawing.Point(18, 30);
		this.HeightToleranceLabel.Name = "HeightToleranceLabel";
		this.HeightToleranceLabel.SaveToConfig = false;
		this.HeightToleranceLabel.Size = new System.Drawing.Size(128, 13);
		this.HeightToleranceLabel.TabIndex = 0;
		this.HeightToleranceLabel.Text = "Height tolerance (meters):";
		this.HeightToleranceInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.HeightToleranceInput.DecimalPlaces = 2;
		this.HeightToleranceInput.Location = new System.Drawing.Point(210, 28);
		this.HeightToleranceInput.Maximum = new decimal(new int[4] { 10000000, 0, 0, 0 });
		this.HeightToleranceInput.Name = "HeightToleranceInput";
		this.HeightToleranceInput.SaveToConfig = true;
		this.HeightToleranceInput.Size = new System.Drawing.Size(80, 20);
		this.HeightToleranceInput.TabIndex = 1;
		this.HeightToleranceInput.ValueChanged += new System.EventHandler(TerrainSettings_ValueChanged);
		this.TileLodGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TileLodGroupBox.Controls.Add(this.CullDistanceLabel);
		this.TileLodGroupBox.Controls.Add(this.CullDistanceInput);
		this.TileLodGroupBox.Location = new System.Drawing.Point(166, 28);
		this.TileLodGroupBox.Name = "TileLodGroupBox";
		this.TileLodGroupBox.Size = new System.Drawing.Size(310, 65);
		this.TileLodGroupBox.TabIndex = 2;
		this.TileLodGroupBox.TabStop = false;
		this.TileLodGroupBox.Text = "Tile LOD";
		this.CullDistanceLabel.AutoSize = true;
		this.CullDistanceLabel.Location = new System.Drawing.Point(18, 30);
		this.CullDistanceLabel.Name = "CullDistanceLabel";
		this.CullDistanceLabel.SaveToConfig = false;
		this.CullDistanceLabel.Size = new System.Drawing.Size(141, 13);
		this.CullDistanceLabel.TabIndex = 0;
		this.CullDistanceLabel.Text = "Transition Distance (meters):";
		this.CullDistanceInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.CullDistanceInput.Location = new System.Drawing.Point(210, 28);
		this.CullDistanceInput.Maximum = new decimal(new int[4] { 10000000, 0, 0, 0 });
		this.CullDistanceInput.Name = "CullDistanceInput";
		this.CullDistanceInput.SaveToConfig = true;
		this.CullDistanceInput.Size = new System.Drawing.Size(80, 20);
		this.CullDistanceInput.TabIndex = 1;
		this.CullDistanceInput.ValueChanged += new System.EventHandler(TerrainSettings_ValueChanged);
		this.OverheadRenderCheckGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.OverheadRenderCheckGroupBox.Controls.Add(this.GlobalDetailInOverheadCheckBox);
		this.OverheadRenderCheckGroupBox.Controls.Add(this.OverheadRenderResolutionComboBox);
		this.OverheadRenderCheckGroupBox.Controls.Add(this.OverheadResolutionLabel);
		this.OverheadRenderCheckGroupBox.Controls.Add(this.TrimRoadsInOverheadCheckBox);
		this.OverheadRenderCheckGroupBox.Location = new System.Drawing.Point(166, 99);
		this.OverheadRenderCheckGroupBox.Name = "OverheadRenderCheckGroupBox";
		this.OverheadRenderCheckGroupBox.SaveToConfig = true;
		this.OverheadRenderCheckGroupBox.Size = new System.Drawing.Size(310, 107);
		this.OverheadRenderCheckGroupBox.TabIndex = 3;
		this.OverheadRenderCheckGroupBox.TabStop = false;
		this.OverheadRenderCheckGroupBox.Text = "Overhead render";
		this.OverheadRenderCheckGroupBox.CheckedChanged += new System.EventHandler(OverheadRenderCheckGroupBox_CheckedChanged);
		this.GlobalDetailInOverheadCheckBox.AutoSize = true;
		this.GlobalDetailInOverheadCheckBox.Location = new System.Drawing.Point(21, 80);
		this.GlobalDetailInOverheadCheckBox.Name = "GlobalDetailInOverheadCheckBox";
		this.GlobalDetailInOverheadCheckBox.SaveToConfig = true;
		this.GlobalDetailInOverheadCheckBox.Size = new System.Drawing.Size(154, 17);
		this.GlobalDetailInOverheadCheckBox.TabIndex = 3;
		this.GlobalDetailInOverheadCheckBox.Text = "Apply Global Detail Texture";
		this.GlobalDetailInOverheadCheckBox.UseVisualStyleBackColor = true;
		this.OverheadRenderResolutionComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.OverheadRenderResolutionComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.OverheadRenderResolutionComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.OverheadRenderResolutionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.OverheadRenderResolutionComboBox.FormattingEnabled = true;
		this.OverheadRenderResolutionComboBox.Location = new System.Drawing.Point(190, 27);
		this.OverheadRenderResolutionComboBox.Name = "OverheadRenderResolutionComboBox";
		this.OverheadRenderResolutionComboBox.SaveToConfig = true;
		this.OverheadRenderResolutionComboBox.Size = new System.Drawing.Size(100, 21);
		this.OverheadRenderResolutionComboBox.TabIndex = 1;
		this.OverheadResolutionLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.OverheadResolutionLabel.AutoSize = true;
		this.OverheadResolutionLabel.Location = new System.Drawing.Point(18, 30);
		this.OverheadResolutionLabel.Name = "OverheadResolutionLabel";
		this.OverheadResolutionLabel.SaveToConfig = false;
		this.OverheadResolutionLabel.Size = new System.Drawing.Size(105, 13);
		this.OverheadResolutionLabel.TabIndex = 0;
		this.OverheadResolutionLabel.Text = "Overhead resolution:";
		this.TrimRoadsInOverheadCheckBox.AutoSize = true;
		this.TrimRoadsInOverheadCheckBox.Location = new System.Drawing.Point(21, 57);
		this.TrimRoadsInOverheadCheckBox.Name = "TrimRoadsInOverheadCheckBox";
		this.TrimRoadsInOverheadCheckBox.SaveToConfig = true;
		this.TrimRoadsInOverheadCheckBox.Size = new System.Drawing.Size(80, 17);
		this.TrimRoadsInOverheadCheckBox.TabIndex = 2;
		this.TrimRoadsInOverheadCheckBox.Text = "Trim Roads";
		this.TrimRoadsInOverheadCheckBox.UseVisualStyleBackColor = true;
		this.TerrainSettingsGroupBox.Controls.Add(this.mSetLodCountButton);
		this.TerrainSettingsGroupBox.Controls.Add(this.mLodChooserListBox);
		this.TerrainSettingsGroupBox.Controls.Add(this.OverheadRenderCheckGroupBox);
		this.TerrainSettingsGroupBox.Controls.Add(this.TileLodGroupBox);
		this.TerrainSettingsGroupBox.Controls.Add(this.RoadLodGroupBox);
		this.TerrainSettingsGroupBox.Controls.Add(this.HeightfieldLodGroupBox);
		this.TerrainSettingsGroupBox.Location = new System.Drawing.Point(12, 12);
		this.TerrainSettingsGroupBox.Name = "TerrainSettingsGroupBox";
		this.TerrainSettingsGroupBox.Size = new System.Drawing.Size(490, 496);
		this.TerrainSettingsGroupBox.TabIndex = 0;
		this.TerrainSettingsGroupBox.TabStop = false;
		this.TerrainSettingsGroupBox.Text = "Terrain Settings";
		this.mMessagePanel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.mMessagePanel.Location = new System.Drawing.Point(12, 567);
		this.mMessagePanel.Name = "mMessagePanel";
		this.mMessagePanel.Size = new System.Drawing.Size(307, 20);
		this.mMessagePanel.TabIndex = 2;
		this.mMessagePanel.TabStop = false;
		this.globalGroup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.globalGroup.Controls.Add(this.PackProps);
		this.globalGroup.Location = new System.Drawing.Point(12, 514);
		this.globalGroup.Name = "globalGroup";
		this.globalGroup.Size = new System.Drawing.Size(490, 44);
		this.globalGroup.TabIndex = 5;
		this.globalGroup.TabStop = false;
		this.globalGroup.Text = "Global LOD Settings";
		this.PackProps.AutoSize = true;
		this.PackProps.Checked = true;
		this.PackProps.CheckState = System.Windows.Forms.CheckState.Checked;
		this.PackProps.Location = new System.Drawing.Point(14, 19);
		this.PackProps.Name = "PackProps";
		this.PackProps.SaveToConfig = true;
		this.PackProps.Size = new System.Drawing.Size(90, 17);
		this.PackProps.TabIndex = 3;
		this.PackProps.Text = "Prop Packing";
		this.PackProps.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.mCancelButton;
		base.ClientSize = new System.Drawing.Size(514, 599);
		base.Controls.Add(this.globalGroup);
		base.Controls.Add(this.mMessagePanel);
		base.Controls.Add(this.TerrainSettingsGroupBox);
		base.Controls.Add(this.mCancelButton);
		base.Controls.Add(this.mOkButton);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "LodSettingsDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "LOD Settings";
		this.RoadLodGroupBox.ResumeLayout(false);
		this.RoadLodGroupBox.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.BridgePostSlicesInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.MaxSegmentLengthInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ExtrusionToleranceInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.PathToleranceInput).EndInit();
		this.HeightfieldLodGroupBox.ResumeLayout(false);
		this.HeightfieldLodGroupBox.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ShorelineToleranceInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.HeightToleranceInput).EndInit();
		this.TileLodGroupBox.ResumeLayout(false);
		this.TileLodGroupBox.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.CullDistanceInput).EndInit();
		this.OverheadRenderCheckGroupBox.ResumeLayout(false);
		this.OverheadRenderCheckGroupBox.PerformLayout();
		this.TerrainSettingsGroupBox.ResumeLayout(false);
		this.globalGroup.ResumeLayout(false);
		this.globalGroup.PerformLayout();
		base.ResumeLayout(false);
	}
}
