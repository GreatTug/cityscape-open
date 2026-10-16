using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace palDotNet.palDialog;

public class TerrainSettingsPanel : UserControl, IDialogContainer
{
	private IContainer components;

	private DialogCheckGroupBox GeolocatedTerrainCheckGroupBox;

	private DialogNumericUpDownFloat TerrainOriginLatitudeInput;

	private DialogNumericUpDownFloat TerrainOriginLongitudeInput;

	private DialogLabel TerrainOriginLatitudeLabel;

	private DialogLabel TerrainOriginLongitudeLabel;

	private GroupBox TerrainPropertiesGroupBox;

	private DialogLabel TileResolutionYLabel;

	private DialogLabel TileResolutionXLabel;

	private DialogNumericUpDownFloat TileSizeYInput;

	private DialogLabel TileSizeYLabel;

	private DialogNumericUpDownFloat TileSizeXInput;

	private DialogLabel TileSizeXLabel;

	private Button SetRegionFromFileBtn;

	private DialogLabel TerrainSizeXLabel;

	private DialogNumericUpDownFloat TerrainSizeYInput;

	private DialogLabel TerrainSizeYLabel;

	private DialogNumericUpDownFloat TerrainSizeXInput;

	private DialogNumericUpDownInt TileCountYInput;

	private DialogNumericUpDownInt TileCountXInput;

	private DialogNumericUpDownFloat RegionNorthLatitudeInput;

	private DialogLabel RegionNorthLatitudeLabel;

	private DialogNumericUpDownFloat RegionSouthLatitudeInput;

	private DialogLabel RegionSouthLatitudeLabel;

	private DialogNumericUpDownFloat RegionWestLongitudeInput;

	private DialogLabel RegionWestLongitudeLabel;

	private DialogNumericUpDownFloat RegionEastLongitudeInput;

	private DialogLabel RegionEastLongitudeLabel;

	private Panel panel1;

	private DialogLabel RegionInfoLabel2;

	private DialogLabel RegionInfoLabel1;

	private DialogLabel TerrainOriginLabel;

	private Panel panel2;

	private DialogLabel MaxTerrainSizeLabel;

	private DialogNumericUpDownFloat SamplingDistanceInput;

	private DialogLabel SamplingDistanceLabel;

	private Button ClearRegionBtn;

	private DialogLabel MaxTerrainSizeInfo;

	private DialogMessagePanel GeoRegionMessagePanel;

	private bool ForceSingleTile;

	private bool EnableComputeTileCountValues = true;

	public DialogCheckGroupBox GeolocatedCheckGroupBox
	{
		get
		{
			return GeolocatedTerrainCheckGroupBox;
		}
		set
		{
			GeolocatedTerrainCheckGroupBox = value;
		}
	}

	public DialogNumericUpDownFloat TerrainSizeX
	{
		get
		{
			return TerrainSizeXInput;
		}
		set
		{
			TerrainSizeXInput = value;
		}
	}

	public DialogNumericUpDownFloat TerrainSizeY
	{
		get
		{
			return TerrainSizeYInput;
		}
		set
		{
			TerrainSizeYInput = value;
		}
	}

	public DialogNumericUpDownFloat TerrainOriginLongitude
	{
		get
		{
			return TerrainOriginLongitudeInput;
		}
		set
		{
			TerrainOriginLongitudeInput = value;
		}
	}

	public DialogNumericUpDownFloat TerrainOriginLatitude
	{
		get
		{
			return TerrainOriginLatitudeInput;
		}
		set
		{
			TerrainOriginLatitudeInput = value;
		}
	}

	public DialogNumericUpDownFloat RegionNorthLatitude
	{
		get
		{
			return RegionNorthLatitudeInput;
		}
		set
		{
			RegionNorthLatitudeInput = value;
		}
	}

	public DialogNumericUpDownFloat RegionSouthLatitude
	{
		get
		{
			return RegionSouthLatitudeInput;
		}
		set
		{
			RegionSouthLatitudeInput = value;
		}
	}

	public DialogNumericUpDownFloat RegionWestLongitude
	{
		get
		{
			return RegionWestLongitudeInput;
		}
		set
		{
			RegionWestLongitudeInput = value;
		}
	}

	public DialogNumericUpDownFloat RegionEastLongitude
	{
		get
		{
			return RegionEastLongitudeInput;
		}
		set
		{
			RegionEastLongitudeInput = value;
		}
	}

	public Button SetRegionFromFileButton => SetRegionFromFileBtn;

	public Button ClearGeolocationValuesButton => ClearRegionBtn;

	public event TerrainValueChangedHandler TerrainValueChanged;

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
		this.TerrainPropertiesGroupBox = new System.Windows.Forms.GroupBox();
		this.MaxTerrainSizeInfo = new palDotNet.palDialog.DialogLabel();
		this.SamplingDistanceInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.SamplingDistanceLabel = new palDotNet.palDialog.DialogLabel();
		this.MaxTerrainSizeLabel = new palDotNet.palDialog.DialogLabel();
		this.TileCountYInput = new palDotNet.palDialog.DialogNumericUpDownInt();
		this.TileCountXInput = new palDotNet.palDialog.DialogNumericUpDownInt();
		this.TerrainSizeYInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.TerrainSizeYLabel = new palDotNet.palDialog.DialogLabel();
		this.TerrainSizeXInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.TerrainSizeXLabel = new palDotNet.palDialog.DialogLabel();
		this.GeolocatedTerrainCheckGroupBox = new palDotNet.palDialog.DialogCheckGroupBox();
		this.RegionInfoLabel1 = new palDotNet.palDialog.DialogLabel();
		this.RegionInfoLabel2 = new palDotNet.palDialog.DialogLabel();
		this.SetRegionFromFileBtn = new System.Windows.Forms.Button();
		this.panel2 = new System.Windows.Forms.Panel();
		this.TerrainOriginLongitudeInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.TerrainOriginLongitudeLabel = new palDotNet.palDialog.DialogLabel();
		this.TerrainOriginLatitudeInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.TerrainOriginLatitudeLabel = new palDotNet.palDialog.DialogLabel();
		this.TerrainOriginLabel = new palDotNet.palDialog.DialogLabel();
		this.GeoRegionMessagePanel = new palDotNet.palDialog.DialogMessagePanel();
		this.ClearRegionBtn = new System.Windows.Forms.Button();
		this.panel1 = new System.Windows.Forms.Panel();
		this.RegionWestLongitudeLabel = new palDotNet.palDialog.DialogLabel();
		this.RegionNorthLatitudeLabel = new palDotNet.palDialog.DialogLabel();
		this.RegionEastLongitudeLabel = new palDotNet.palDialog.DialogLabel();
		this.RegionSouthLatitudeInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.RegionNorthLatitudeInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.RegionEastLongitudeInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.RegionSouthLatitudeLabel = new palDotNet.palDialog.DialogLabel();
		this.RegionWestLongitudeInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.TileResolutionYLabel = new palDotNet.palDialog.DialogLabel();
		this.TileResolutionXLabel = new palDotNet.palDialog.DialogLabel();
		this.TileSizeYInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.TileSizeYLabel = new palDotNet.palDialog.DialogLabel();
		this.TileSizeXInput = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.TileSizeXLabel = new palDotNet.palDialog.DialogLabel();
		this.TerrainPropertiesGroupBox.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.SamplingDistanceInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TileCountYInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TileCountXInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TerrainSizeYInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TerrainSizeXInput).BeginInit();
		this.GeolocatedTerrainCheckGroupBox.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TerrainOriginLongitudeInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TerrainOriginLatitudeInput).BeginInit();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.RegionSouthLatitudeInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.RegionNorthLatitudeInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.RegionEastLongitudeInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.RegionWestLongitudeInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TileSizeYInput).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TileSizeXInput).BeginInit();
		base.SuspendLayout();
		this.TerrainPropertiesGroupBox.Controls.Add(this.MaxTerrainSizeInfo);
		this.TerrainPropertiesGroupBox.Controls.Add(this.SamplingDistanceInput);
		this.TerrainPropertiesGroupBox.Controls.Add(this.SamplingDistanceLabel);
		this.TerrainPropertiesGroupBox.Controls.Add(this.MaxTerrainSizeLabel);
		this.TerrainPropertiesGroupBox.Controls.Add(this.TileCountYInput);
		this.TerrainPropertiesGroupBox.Controls.Add(this.TileCountXInput);
		this.TerrainPropertiesGroupBox.Controls.Add(this.TerrainSizeYInput);
		this.TerrainPropertiesGroupBox.Controls.Add(this.TerrainSizeYLabel);
		this.TerrainPropertiesGroupBox.Controls.Add(this.TerrainSizeXInput);
		this.TerrainPropertiesGroupBox.Controls.Add(this.TerrainSizeXLabel);
		this.TerrainPropertiesGroupBox.Controls.Add(this.GeolocatedTerrainCheckGroupBox);
		this.TerrainPropertiesGroupBox.Controls.Add(this.TileResolutionYLabel);
		this.TerrainPropertiesGroupBox.Controls.Add(this.TileResolutionXLabel);
		this.TerrainPropertiesGroupBox.Controls.Add(this.TileSizeYInput);
		this.TerrainPropertiesGroupBox.Controls.Add(this.TileSizeYLabel);
		this.TerrainPropertiesGroupBox.Controls.Add(this.TileSizeXInput);
		this.TerrainPropertiesGroupBox.Controls.Add(this.TileSizeXLabel);
		this.TerrainPropertiesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TerrainPropertiesGroupBox.Location = new System.Drawing.Point(0, 0);
		this.TerrainPropertiesGroupBox.Name = "TerrainPropertiesGroupBox";
		this.TerrainPropertiesGroupBox.Size = new System.Drawing.Size(800, 350);
		this.TerrainPropertiesGroupBox.TabIndex = 0;
		this.TerrainPropertiesGroupBox.TabStop = false;
		this.TerrainPropertiesGroupBox.Text = "Terrain Properties";
		this.MaxTerrainSizeInfo.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.MaxTerrainSizeInfo.AutoSize = true;
		this.MaxTerrainSizeInfo.Location = new System.Drawing.Point(182, 320);
		this.MaxTerrainSizeInfo.Name = "MaxTerrainSizeInfo";
		this.MaxTerrainSizeInfo.SaveToConfig = false;
		this.MaxTerrainSizeInfo.Size = new System.Drawing.Size(70, 13);
		this.MaxTerrainSizeInfo.TabIndex = 15;
		this.MaxTerrainSizeInfo.Text = "--- km x --- km";
		this.SamplingDistanceInput.DecimalPlaces = 2;
		this.SamplingDistanceInput.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.SamplingDistanceInput.Location = new System.Drawing.Point(185, 259);
		this.SamplingDistanceInput.Name = "SamplingDistanceInput";
		this.SamplingDistanceInput.SaveToConfig = true;
		this.SamplingDistanceInput.Size = new System.Drawing.Size(100, 20);
		this.SamplingDistanceInput.TabIndex = 13;
		this.SamplingDistanceInput.Value = new decimal(new int[4] { 2, 0, 0, 0 });
		this.SamplingDistanceLabel.AutoSize = true;
		this.SamplingDistanceLabel.Location = new System.Drawing.Point(25, 261);
		this.SamplingDistanceLabel.Name = "SamplingDistanceLabel";
		this.SamplingDistanceLabel.SaveToConfig = false;
		this.SamplingDistanceLabel.Size = new System.Drawing.Size(136, 13);
		this.SamplingDistanceLabel.TabIndex = 12;
		this.SamplingDistanceLabel.Text = "Sampling distance (meters):";
		this.MaxTerrainSizeLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.MaxTerrainSizeLabel.AutoSize = true;
		this.MaxTerrainSizeLabel.Location = new System.Drawing.Point(25, 320);
		this.MaxTerrainSizeLabel.Name = "MaxTerrainSizeLabel";
		this.MaxTerrainSizeLabel.SaveToConfig = false;
		this.MaxTerrainSizeLabel.Size = new System.Drawing.Size(83, 13);
		this.MaxTerrainSizeLabel.TabIndex = 14;
		this.MaxTerrainSizeLabel.Text = "Max terrain size:";
		this.TileCountYInput.Enabled = false;
		this.TileCountYInput.Location = new System.Drawing.Point(185, 192);
		this.TileCountYInput.Maximum = new decimal(new int[4] { 10000, 0, 0, 0 });
		this.TileCountYInput.Name = "TileCountYInput";
		this.TileCountYInput.SaveToConfig = true;
		this.TileCountYInput.Size = new System.Drawing.Size(100, 20);
		this.TileCountYInput.TabIndex = 11;
		this.TileCountXInput.Enabled = false;
		this.TileCountXInput.Location = new System.Drawing.Point(185, 165);
		this.TileCountXInput.Maximum = new decimal(new int[4] { 10000, 0, 0, 0 });
		this.TileCountXInput.Name = "TileCountXInput";
		this.TileCountXInput.SaveToConfig = true;
		this.TileCountXInput.Size = new System.Drawing.Size(100, 20);
		this.TileCountXInput.TabIndex = 9;
		this.TerrainSizeYInput.DecimalPlaces = 2;
		this.TerrainSizeYInput.Location = new System.Drawing.Point(185, 61);
		this.TerrainSizeYInput.Maximum = new decimal(new int[4] { 10000000, 0, 0, 0 });
		this.TerrainSizeYInput.Name = "TerrainSizeYInput";
		this.TerrainSizeYInput.SaveToConfig = true;
		this.TerrainSizeYInput.Size = new System.Drawing.Size(100, 20);
		this.TerrainSizeYInput.TabIndex = 3;
		this.TerrainSizeYInput.Value = new decimal(new int[4] { 1024, 0, 0, 0 });
		this.TerrainSizeYLabel.AutoSize = true;
		this.TerrainSizeYLabel.Location = new System.Drawing.Point(25, 63);
		this.TerrainSizeYLabel.Name = "TerrainSizeYLabel";
		this.TerrainSizeYLabel.SaveToConfig = false;
		this.TerrainSizeYLabel.Size = new System.Drawing.Size(114, 13);
		this.TerrainSizeYLabel.TabIndex = 2;
		this.TerrainSizeYLabel.Text = "Terrain size Y (meters):";
		this.TerrainSizeXInput.DecimalPlaces = 2;
		this.TerrainSizeXInput.Location = new System.Drawing.Point(185, 35);
		this.TerrainSizeXInput.Maximum = new decimal(new int[4] { 10000000, 0, 0, 0 });
		this.TerrainSizeXInput.Name = "TerrainSizeXInput";
		this.TerrainSizeXInput.SaveToConfig = true;
		this.TerrainSizeXInput.Size = new System.Drawing.Size(100, 20);
		this.TerrainSizeXInput.TabIndex = 1;
		this.TerrainSizeXInput.Value = new decimal(new int[4] { 1024, 0, 0, 0 });
		this.TerrainSizeXLabel.AutoSize = true;
		this.TerrainSizeXLabel.Location = new System.Drawing.Point(25, 37);
		this.TerrainSizeXLabel.Name = "TerrainSizeXLabel";
		this.TerrainSizeXLabel.SaveToConfig = false;
		this.TerrainSizeXLabel.Size = new System.Drawing.Size(114, 13);
		this.TerrainSizeXLabel.TabIndex = 0;
		this.TerrainSizeXLabel.Text = "Terrain size X (meters):";
		this.GeolocatedTerrainCheckGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.GeolocatedTerrainCheckGroupBox.Checked = false;
		this.GeolocatedTerrainCheckGroupBox.CheckState = System.Windows.Forms.CheckState.Unchecked;
		this.GeolocatedTerrainCheckGroupBox.Controls.Add(this.RegionInfoLabel1);
		this.GeolocatedTerrainCheckGroupBox.Controls.Add(this.RegionInfoLabel2);
		this.GeolocatedTerrainCheckGroupBox.Controls.Add(this.SetRegionFromFileBtn);
		this.GeolocatedTerrainCheckGroupBox.Controls.Add(this.panel2);
		this.GeolocatedTerrainCheckGroupBox.Controls.Add(this.GeoRegionMessagePanel);
		this.GeolocatedTerrainCheckGroupBox.Controls.Add(this.ClearRegionBtn);
		this.GeolocatedTerrainCheckGroupBox.Controls.Add(this.panel1);
		this.GeolocatedTerrainCheckGroupBox.Location = new System.Drawing.Point(314, 19);
		this.GeolocatedTerrainCheckGroupBox.Name = "GeolocatedTerrainCheckGroupBox";
		this.GeolocatedTerrainCheckGroupBox.SaveToConfig = true;
		this.GeolocatedTerrainCheckGroupBox.Size = new System.Drawing.Size(480, 325);
		this.GeolocatedTerrainCheckGroupBox.TabIndex = 16;
		this.GeolocatedTerrainCheckGroupBox.TabStop = false;
		this.GeolocatedTerrainCheckGroupBox.Text = "Geolocated terrain";
		this.RegionInfoLabel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.RegionInfoLabel1.Location = new System.Drawing.Point(22, 274);
		this.RegionInfoLabel1.Name = "RegionInfoLabel1";
		this.RegionInfoLabel1.SaveToConfig = false;
		this.RegionInfoLabel1.Size = new System.Drawing.Size(433, 20);
		this.RegionInfoLabel1.TabIndex = 5;
		this.RegionInfoLabel1.Text = "Latitude: [-90, 90]  Longitude: [-180, 180]";
		this.RegionInfoLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.RegionInfoLabel2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.RegionInfoLabel2.Location = new System.Drawing.Point(22, 294);
		this.RegionInfoLabel2.Name = "RegionInfoLabel2";
		this.RegionInfoLabel2.SaveToConfig = false;
		this.RegionInfoLabel2.Size = new System.Drawing.Size(433, 20);
		this.RegionInfoLabel2.TabIndex = 6;
		this.RegionInfoLabel2.Text = "Use negative values to indicate Southern and Western Hemisphere.";
		this.RegionInfoLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.SetRegionFromFileBtn.Location = new System.Drawing.Point(25, 31);
		this.SetRegionFromFileBtn.Name = "SetRegionFromFileBtn";
		this.SetRegionFromFileBtn.Size = new System.Drawing.Size(160, 23);
		this.SetRegionFromFileBtn.TabIndex = 0;
		this.SetRegionFromFileBtn.Text = "Set Region From File...";
		this.SetRegionFromFileBtn.UseVisualStyleBackColor = true;
		this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel2.Controls.Add(this.TerrainOriginLongitudeInput);
		this.panel2.Controls.Add(this.TerrainOriginLongitudeLabel);
		this.panel2.Controls.Add(this.TerrainOriginLatitudeInput);
		this.panel2.Controls.Add(this.TerrainOriginLatitudeLabel);
		this.panel2.Controls.Add(this.TerrainOriginLabel);
		this.panel2.Location = new System.Drawing.Point(25, 70);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(430, 40);
		this.panel2.TabIndex = 2;
		this.TerrainOriginLongitudeInput.DecimalPlaces = 6;
		this.TerrainOriginLongitudeInput.Location = new System.Drawing.Point(303, 10);
		this.TerrainOriginLongitudeInput.Maximum = new decimal(new int[4] { 180, 0, 0, 0 });
		this.TerrainOriginLongitudeInput.Minimum = new decimal(new int[4] { 180, 0, 0, -2147483648 });
		this.TerrainOriginLongitudeInput.Name = "TerrainOriginLongitudeInput";
		this.TerrainOriginLongitudeInput.SaveToConfig = true;
		this.TerrainOriginLongitudeInput.Size = new System.Drawing.Size(100, 20);
		this.TerrainOriginLongitudeInput.TabIndex = 4;
		this.TerrainOriginLongitudeLabel.AutoSize = true;
		this.TerrainOriginLongitudeLabel.Location = new System.Drawing.Point(269, 12);
		this.TerrainOriginLongitudeLabel.Name = "TerrainOriginLongitudeLabel";
		this.TerrainOriginLongitudeLabel.SaveToConfig = false;
		this.TerrainOriginLongitudeLabel.Size = new System.Drawing.Size(28, 13);
		this.TerrainOriginLongitudeLabel.TabIndex = 3;
		this.TerrainOriginLongitudeLabel.Text = "Lon:";
		this.TerrainOriginLatitudeInput.DecimalPlaces = 6;
		this.TerrainOriginLatitudeInput.Location = new System.Drawing.Point(138, 10);
		this.TerrainOriginLatitudeInput.Maximum = new decimal(new int[4] { 90, 0, 0, 0 });
		this.TerrainOriginLatitudeInput.Minimum = new decimal(new int[4] { 90, 0, 0, -2147483648 });
		this.TerrainOriginLatitudeInput.Name = "TerrainOriginLatitudeInput";
		this.TerrainOriginLatitudeInput.SaveToConfig = true;
		this.TerrainOriginLatitudeInput.Size = new System.Drawing.Size(100, 20);
		this.TerrainOriginLatitudeInput.TabIndex = 2;
		this.TerrainOriginLatitudeLabel.AutoSize = true;
		this.TerrainOriginLatitudeLabel.Location = new System.Drawing.Point(107, 12);
		this.TerrainOriginLatitudeLabel.Name = "TerrainOriginLatitudeLabel";
		this.TerrainOriginLatitudeLabel.SaveToConfig = false;
		this.TerrainOriginLatitudeLabel.Size = new System.Drawing.Size(25, 13);
		this.TerrainOriginLatitudeLabel.TabIndex = 1;
		this.TerrainOriginLatitudeLabel.Text = "Lat:";
		this.TerrainOriginLabel.AutoSize = true;
		this.TerrainOriginLabel.Location = new System.Drawing.Point(10, 12);
		this.TerrainOriginLabel.Name = "TerrainOriginLabel";
		this.TerrainOriginLabel.SaveToConfig = false;
		this.TerrainOriginLabel.Size = new System.Drawing.Size(68, 13);
		this.TerrainOriginLabel.TabIndex = 0;
		this.TerrainOriginLabel.Text = "Terrain origin";
		this.GeoRegionMessagePanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GeoRegionMessagePanel.Location = new System.Drawing.Point(25, 249);
		this.GeoRegionMessagePanel.Margin = new System.Windows.Forms.Padding(5);
		this.GeoRegionMessagePanel.Name = "GeoRegionMessagePanel";
		this.GeoRegionMessagePanel.Size = new System.Drawing.Size(430, 20);
		this.GeoRegionMessagePanel.TabIndex = 4;
		this.GeoRegionMessagePanel.TabStop = false;
		this.ClearRegionBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ClearRegionBtn.Location = new System.Drawing.Point(191, 31);
		this.ClearRegionBtn.Name = "ClearRegionBtn";
		this.ClearRegionBtn.Size = new System.Drawing.Size(79, 23);
		this.ClearRegionBtn.TabIndex = 1;
		this.ClearRegionBtn.Text = "Clear";
		this.ClearRegionBtn.UseVisualStyleBackColor = true;
		this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.RegionWestLongitudeLabel);
		this.panel1.Controls.Add(this.RegionNorthLatitudeLabel);
		this.panel1.Controls.Add(this.RegionEastLongitudeLabel);
		this.panel1.Controls.Add(this.RegionSouthLatitudeInput);
		this.panel1.Controls.Add(this.RegionNorthLatitudeInput);
		this.panel1.Controls.Add(this.RegionEastLongitudeInput);
		this.panel1.Controls.Add(this.RegionSouthLatitudeLabel);
		this.panel1.Controls.Add(this.RegionWestLongitudeInput);
		this.panel1.Location = new System.Drawing.Point(25, 116);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(430, 125);
		this.panel1.TabIndex = 3;
		this.RegionWestLongitudeLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.RegionWestLongitudeLabel.AutoSize = true;
		this.RegionWestLongitudeLabel.Location = new System.Drawing.Point(10, 53);
		this.RegionWestLongitudeLabel.Name = "RegionWestLongitudeLabel";
		this.RegionWestLongitudeLabel.SaveToConfig = false;
		this.RegionWestLongitudeLabel.Size = new System.Drawing.Size(52, 13);
		this.RegionWestLongitudeLabel.TabIndex = 2;
		this.RegionWestLongitudeLabel.Text = "West lon:";
		this.RegionNorthLatitudeLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.RegionNorthLatitudeLabel.AutoSize = true;
		this.RegionNorthLatitudeLabel.Location = new System.Drawing.Point(145, 5);
		this.RegionNorthLatitudeLabel.Name = "RegionNorthLatitudeLabel";
		this.RegionNorthLatitudeLabel.SaveToConfig = false;
		this.RegionNorthLatitudeLabel.Size = new System.Drawing.Size(50, 13);
		this.RegionNorthLatitudeLabel.TabIndex = 0;
		this.RegionNorthLatitudeLabel.Text = "North lat:";
		this.RegionEastLongitudeLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.RegionEastLongitudeLabel.AutoSize = true;
		this.RegionEastLongitudeLabel.Location = new System.Drawing.Point(257, 53);
		this.RegionEastLongitudeLabel.Name = "RegionEastLongitudeLabel";
		this.RegionEastLongitudeLabel.SaveToConfig = false;
		this.RegionEastLongitudeLabel.Size = new System.Drawing.Size(48, 13);
		this.RegionEastLongitudeLabel.TabIndex = 4;
		this.RegionEastLongitudeLabel.Text = "East lon:";
		this.RegionSouthLatitudeInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.RegionSouthLatitudeInput.DecimalPlaces = 6;
		this.RegionSouthLatitudeInput.Location = new System.Drawing.Point(207, 100);
		this.RegionSouthLatitudeInput.Maximum = new decimal(new int[4] { 90, 0, 0, 0 });
		this.RegionSouthLatitudeInput.Minimum = new decimal(new int[4] { 90, 0, 0, -2147483648 });
		this.RegionSouthLatitudeInput.Name = "RegionSouthLatitudeInput";
		this.RegionSouthLatitudeInput.SaveToConfig = true;
		this.RegionSouthLatitudeInput.Size = new System.Drawing.Size(100, 20);
		this.RegionSouthLatitudeInput.TabIndex = 7;
		this.RegionNorthLatitudeInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.RegionNorthLatitudeInput.DecimalPlaces = 6;
		this.RegionNorthLatitudeInput.Location = new System.Drawing.Point(207, 3);
		this.RegionNorthLatitudeInput.Maximum = new decimal(new int[4] { 90, 0, 0, 0 });
		this.RegionNorthLatitudeInput.Minimum = new decimal(new int[4] { 90, 0, 0, -2147483648 });
		this.RegionNorthLatitudeInput.Name = "RegionNorthLatitudeInput";
		this.RegionNorthLatitudeInput.SaveToConfig = true;
		this.RegionNorthLatitudeInput.Size = new System.Drawing.Size(100, 20);
		this.RegionNorthLatitudeInput.TabIndex = 1;
		this.RegionEastLongitudeInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.RegionEastLongitudeInput.DecimalPlaces = 6;
		this.RegionEastLongitudeInput.Location = new System.Drawing.Point(315, 51);
		this.RegionEastLongitudeInput.Maximum = new decimal(new int[4] { 180, 0, 0, 0 });
		this.RegionEastLongitudeInput.Minimum = new decimal(new int[4] { 180, 0, 0, -2147483648 });
		this.RegionEastLongitudeInput.Name = "RegionEastLongitudeInput";
		this.RegionEastLongitudeInput.SaveToConfig = true;
		this.RegionEastLongitudeInput.Size = new System.Drawing.Size(100, 20);
		this.RegionEastLongitudeInput.TabIndex = 5;
		this.RegionSouthLatitudeLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.RegionSouthLatitudeLabel.AutoSize = true;
		this.RegionSouthLatitudeLabel.Location = new System.Drawing.Point(145, 102);
		this.RegionSouthLatitudeLabel.Name = "RegionSouthLatitudeLabel";
		this.RegionSouthLatitudeLabel.SaveToConfig = false;
		this.RegionSouthLatitudeLabel.Size = new System.Drawing.Size(52, 13);
		this.RegionSouthLatitudeLabel.TabIndex = 6;
		this.RegionSouthLatitudeLabel.Text = "South lat:";
		this.RegionWestLongitudeInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.RegionWestLongitudeInput.DecimalPlaces = 6;
		this.RegionWestLongitudeInput.Location = new System.Drawing.Point(72, 51);
		this.RegionWestLongitudeInput.Maximum = new decimal(new int[4] { 180, 0, 0, 0 });
		this.RegionWestLongitudeInput.Minimum = new decimal(new int[4] { 180, 0, 0, -2147483648 });
		this.RegionWestLongitudeInput.Name = "RegionWestLongitudeInput";
		this.RegionWestLongitudeInput.SaveToConfig = true;
		this.RegionWestLongitudeInput.Size = new System.Drawing.Size(100, 20);
		this.RegionWestLongitudeInput.TabIndex = 3;
		this.TileResolutionYLabel.AutoSize = true;
		this.TileResolutionYLabel.Location = new System.Drawing.Point(25, 194);
		this.TileResolutionYLabel.Name = "TileResolutionYLabel";
		this.TileResolutionYLabel.SaveToConfig = false;
		this.TileResolutionYLabel.Size = new System.Drawing.Size(67, 13);
		this.TileResolutionYLabel.TabIndex = 10;
		this.TileResolutionYLabel.Text = "Tile count Y:";
		this.TileResolutionXLabel.AutoSize = true;
		this.TileResolutionXLabel.Location = new System.Drawing.Point(25, 167);
		this.TileResolutionXLabel.Name = "TileResolutionXLabel";
		this.TileResolutionXLabel.SaveToConfig = false;
		this.TileResolutionXLabel.Size = new System.Drawing.Size(67, 13);
		this.TileResolutionXLabel.TabIndex = 8;
		this.TileResolutionXLabel.Text = "Tile count X:";
		this.TileSizeYInput.DecimalPlaces = 2;
		this.TileSizeYInput.Location = new System.Drawing.Point(185, 126);
		this.TileSizeYInput.Maximum = new decimal(new int[4] { 10000, 0, 0, 0 });
		this.TileSizeYInput.Minimum = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TileSizeYInput.Name = "TileSizeYInput";
		this.TileSizeYInput.SaveToConfig = true;
		this.TileSizeYInput.Size = new System.Drawing.Size(100, 20);
		this.TileSizeYInput.TabIndex = 7;
		this.TileSizeYInput.Value = new decimal(new int[4] { 5120, 0, 0, 65536 });
		this.TileSizeYLabel.AutoSize = true;
		this.TileSizeYLabel.Location = new System.Drawing.Point(25, 128);
		this.TileSizeYLabel.Name = "TileSizeYLabel";
		this.TileSizeYLabel.SaveToConfig = false;
		this.TileSizeYLabel.Size = new System.Drawing.Size(98, 13);
		this.TileSizeYLabel.TabIndex = 6;
		this.TileSizeYLabel.Text = "Tile size Y (meters):";
		this.TileSizeXInput.DecimalPlaces = 2;
		this.TileSizeXInput.Location = new System.Drawing.Point(185, 100);
		this.TileSizeXInput.Maximum = new decimal(new int[4] { 10000, 0, 0, 0 });
		this.TileSizeXInput.Minimum = new decimal(new int[4] { 10, 0, 0, 0 });
		this.TileSizeXInput.Name = "TileSizeXInput";
		this.TileSizeXInput.SaveToConfig = true;
		this.TileSizeXInput.Size = new System.Drawing.Size(100, 20);
		this.TileSizeXInput.TabIndex = 5;
		this.TileSizeXInput.Value = new decimal(new int[4] { 5120, 0, 0, 65536 });
		this.TileSizeXLabel.AutoSize = true;
		this.TileSizeXLabel.Location = new System.Drawing.Point(25, 102);
		this.TileSizeXLabel.Name = "TileSizeXLabel";
		this.TileSizeXLabel.SaveToConfig = false;
		this.TileSizeXLabel.Size = new System.Drawing.Size(98, 13);
		this.TileSizeXLabel.TabIndex = 4;
		this.TileSizeXLabel.Text = "Tile size X (meters):";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.TerrainPropertiesGroupBox);
		base.Name = "TerrainSettingsPanel";
		base.Size = new System.Drawing.Size(800, 350);
		this.TerrainPropertiesGroupBox.ResumeLayout(false);
		this.TerrainPropertiesGroupBox.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.SamplingDistanceInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TileCountYInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TileCountXInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TerrainSizeYInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TerrainSizeXInput).EndInit();
		this.GeolocatedTerrainCheckGroupBox.ResumeLayout(false);
		this.GeolocatedTerrainCheckGroupBox.PerformLayout();
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TerrainOriginLongitudeInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TerrainOriginLatitudeInput).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.RegionSouthLatitudeInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.RegionNorthLatitudeInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.RegionEastLongitudeInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.RegionWestLongitudeInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TileSizeYInput).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TileSizeXInput).EndInit();
		base.ResumeLayout(false);
	}

	public TerrainSettingsPanel()
	{
		InitializeComponent();
		ClearMessageInfo();
	}

	public void OnShown()
	{
		ClearMessageInfo();
		TerrainSizeXInput.ValueChanged += TerrainSize_ValueChanged;
		TerrainSizeYInput.ValueChanged += TerrainSize_ValueChanged;
		TileSizeXInput.ValueChanged += TileSize_ValueChanged;
		TileSizeYInput.ValueChanged += TileSize_ValueChanged;
		if (GeolocatedTerrainCheckGroupBox.Checked)
		{
			GeolocatedTerrainCheckGroupBox.Checked = false;
			GeolocatedTerrainCheckGroupBox.Checked = true;
		}
		HandleForceSingleTile();
		ComputeTileCountValues();
	}

	private void TerrainSize_ValueChanged(object sender, EventArgs e)
	{
		HandleForceSingleTile();
		ComputeTileCountValues();
	}

	private void TileSize_ValueChanged(object sender, EventArgs e)
	{
		ComputeTileCountValues();
	}

	private void HandleForceSingleTile()
	{
		if (ForceSingleTile)
		{
			bool enableComputeTileCountValues = EnableComputeTileCountValues;
			SetEnableComputeTileCountValues(bEnabled: false);
			TileSizeXInput.Value = Math.Max(TerrainSizeXInput.Value, TileSizeXInput.Minimum);
			TileSizeYInput.Value = Math.Max(TerrainSizeYInput.Value, TileSizeYInput.Minimum);
			SetEnableComputeTileCountValues(enableComputeTileCountValues);
		}
	}

	public void SetGeolocatedTerrain(bool bGeolocatedTerrain)
	{
		GeolocatedTerrainCheckGroupBox.Checked = bGeolocatedTerrain;
	}

	public bool IsGeolocatedTerrain()
	{
		return GeolocatedTerrainCheckGroupBox.Checked;
	}

	public void SetMaxTerrainSize(int maxSizeX, int maxSizeY)
	{
		TerrainSizeXInput.Maximum = maxSizeX;
		TerrainSizeYInput.Maximum = maxSizeY;
		float num = (float)maxSizeX / 1000f;
		float num2 = (float)maxSizeY / 1000f;
		MaxTerrainSizeInfo.Text = num + " km × " + num2 + " km";
	}

	public float GetTerrainMaxSizeX()
	{
		return (float)TerrainSizeXInput.Maximum;
	}

	public float GetTerrainMaxSizeY()
	{
		return (float)TerrainSizeYInput.Maximum;
	}

	public void SetTerrainSize(float sizeX, float sizeY)
	{
		TerrainSizeXInput.Value = Math.Min((decimal)sizeX, TerrainSizeXInput.Maximum);
		TerrainSizeYInput.Value = Math.Min((decimal)sizeY, TerrainSizeYInput.Maximum);
	}

	public void SetTileSize(float tileSizeX, float tileSizeY)
	{
		TileSizeXInput.Value = (decimal)tileSizeX;
		TileSizeYInput.Value = (decimal)tileSizeY;
	}

	public void SetTessellationSampleDist(float samplingDist)
	{
		SamplingDistanceInput.Value = (decimal)samplingDist;
	}

	public void SetTessellationSampleDistRange(float minSampleDist, float maxSampleDist)
	{
		SamplingDistanceInput.Minimum = (decimal)minSampleDist;
		SamplingDistanceInput.Maximum = (decimal)maxSampleDist;
	}

	public void SetForceSingleTile(bool bForceSingleTile)
	{
		ForceSingleTile = bForceSingleTile;
		if (ForceSingleTile)
		{
			TileSizeXInput.Enabled = false;
			TileSizeYInput.Enabled = false;
		}
	}

	public void SetEnableTessellationSampleDist(bool bEnabled)
	{
		SamplingDistanceInput.Enabled = bEnabled;
	}

	public void SetTerrainOrigin(double latitude, double longitude)
	{
		TerrainOriginLatitudeInput.Value = (decimal)latitude;
		TerrainOriginLongitudeInput.Value = (decimal)longitude;
	}

	public void ClearTerrainOrigin()
	{
		TerrainOriginLatitudeInput.Value = 0m;
		TerrainOriginLongitudeInput.Value = 0m;
	}

	public void ClearRegionValues()
	{
		RegionNorthLatitudeInput.Value = 0m;
		RegionSouthLatitudeInput.Value = 0m;
		RegionWestLongitudeInput.Value = 0m;
		RegionEastLongitudeInput.Value = 0m;
	}

	protected virtual void OnTerrainValueChanged(EventArgs e)
	{
		TerrainValueChanged(this, e);
	}

	public void SetEnableComputeTileCountValues(bool bEnabled)
	{
		EnableComputeTileCountValues = bEnabled;
	}

	public void ComputeTileCountValues()
	{
		if (EnableComputeTileCountValues)
		{
			decimal value = TerrainSizeXInput.Value;
			decimal value2 = TerrainSizeYInput.Value;
			decimal value3 = TileSizeXInput.Value;
			decimal value4 = TileSizeYInput.Value;
			if (value == 0m || value2 == 0m || value3 == 0m || value4 == 0m)
			{
				TileCountXInput.Value = 0m;
				TileCountYInput.Value = 0m;
			}
			else
			{
				TileCountXInput.Value = (int)Math.Ceiling(value / value3);
				TileCountYInput.Value = (int)Math.Ceiling(value2 / value4);
			}
			OnTerrainValueChanged(new EventArgs());
		}
	}

	public bool IsDataValid()
	{
		if (TileCountXInput.Value > 0m && TileCountYInput.Value > 0m)
		{
			return true;
		}
		return false;
	}

	public string GetValidationErrorString()
	{
		decimal value = TerrainSizeXInput.Value;
		decimal value2 = TerrainSizeYInput.Value;
		decimal value3 = TileSizeXInput.Value;
		decimal value4 = TileSizeYInput.Value;
		if (value == 0m && value2 == 0m)
		{
			return "Terrain Size X and Y are invalid.";
		}
		if (value == 0m)
		{
			return "Terrain Size X is invalid.";
		}
		if (value2 == 0m)
		{
			return "Terrain Size Y is invalid.";
		}
		if (value3 == 0m && value4 == 0m)
		{
			return "Tile Size X and Y are invalid.";
		}
		if (value3 == 0m)
		{
			return "Tile Size X is invalid.";
		}
		if (value4 == 0m)
		{
			return "Tile Size Y is invalid.";
		}
		if (value3 > value && value4 > value2)
		{
			return "Tile Size X and Y are larger than Terrain Size X and Y (respectively).";
		}
		if (value3 > value)
		{
			return "Tile Size X is larger than Terrain Size X.";
		}
		if (value4 > value2)
		{
			return "Tile Size Y is larger than Terrain Size Y.";
		}
		return "Terrain settings are invalid.";
	}

	public void ClearMessageInfo()
	{
		GeoRegionMessagePanel.ClearMessage();
	}

	public void ShowInfoMessage(string msg)
	{
		GeoRegionMessagePanel.InfoMessage(msg);
	}

	public void ShowWarningMessage(string msg)
	{
		GeoRegionMessagePanel.WarningMessage(msg);
	}

	public void ShowErrorMessage(string msg)
	{
		GeoRegionMessagePanel.ErrorMessage(msg);
	}

	public void ShowMessagePanel(bool bShow)
	{
		if (bShow)
		{
			GeoRegionMessagePanel.Show();
		}
		else
		{
			GeoRegionMessagePanel.Hide();
		}
	}
}
