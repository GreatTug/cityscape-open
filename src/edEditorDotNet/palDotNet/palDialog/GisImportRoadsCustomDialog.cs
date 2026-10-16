using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace palDotNet.palDialog;

[CPPCodeGeneration(OutputFile = "..\\edEditor\\edGISBorkDialogs\\GisImportRoadsCustomDialog", Win32PCOnly = true)]
[DialogBoxCreator]
public class GisImportRoadsCustomDialog : DialogBoxBase
{
	private IContainer components;

	private Button CancelBtn;

	private Button OKBtn;

	private TabControl TabControl;

	private TabPage ConnectivityTab;

	private TabPage FunctionalClassTab;

	private DialogLabel EndNodeFieldNameLabel;

	private DialogLabel StartNodeFieldNameLabel;

	private DialogComboBox EndNodeFieldNameComboBox;

	private DialogComboBox StartNodeFieldNameComboBox;

	private DialogComboBox FunctionalClassFieldNameComboBox;

	private DialogLabel FunctionalClassFieldNameLabel;

	private DialogComboBox StartNodeZLevelFieldNameComboBox;

	private DialogLabel StartNodeZLevelFieldNameLabel;

	private DialogComboBox SingleLaneCountFieldNameComboBox;

	private DialogLabel SingleLaneCountFieldNameLabel;

	private TabPage LaneCountTab;

	private TabPage ZLevelTab;

	private TabPage DirectionalityTab;

	private DialogCheckGroupBox ConnectivityCheckGroupBox;

	private DialogCheckGroupBox SingleFieldLaneCountCheckGroupBox;

	private DialogCheckGroupBox OneWayCheckGroupBox;

	private DialogTextBox OneWayFieldValue_Bidirectional_TextBox;

	private DialogLabel OneWayFieldValuesLabel;

	private DialogLabel OneWayFieldNameLabel;

	private DialogComboBox OneWayFieldNameComboBox;

	private DialogTextBox OneWayFieldValue_EndStart_TextBox;

	private DialogLabel OneWayFieldValue_EndStart_Label;

	private DialogLabel OneWayFieldValue_StartEnd_Label;

	private DialogLabel OneWayFieldValue_Bidirectional_Label;

	private DialogTextBox OneWayFieldValue_StartEnd_TextBox;

	private DialogCheckGroupBox TwoFieldLaneCountCheckGroupBox;

	private DialogComboBox EndStartLaneCountFieldNameComboBox;

	private DialogLabel StartEndLaneCountLabel;

	private DialogComboBox StartEndLaneCountFieldNameComboBox;

	private DialogLabel EndStartLaneCountLabel;

	private DialogCheckGroupBox ZLevelCheckGroupBox;

	private DialogLabel EndNodeZLevelFieldNameLabel;

	private DialogComboBox EndNodeZLevelFieldNameComboBox;

	private DialogStyleGridChooser RoadStyleGridChooser;

	private DialogCheckGroupBox FunctionalClassCheckGroupBox;

	public DialogStyleGridChooser RoadStyleChooser => RoadStyleGridChooser;

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
		this.TabControl = new System.Windows.Forms.TabControl();
		this.FunctionalClassTab = new System.Windows.Forms.TabPage();
		this.FunctionalClassCheckGroupBox = new palDotNet.palDialog.DialogCheckGroupBox();
		this.FunctionalClassFieldNameComboBox = new palDotNet.palDialog.DialogComboBox();
		this.FunctionalClassFieldNameLabel = new palDotNet.palDialog.DialogLabel();
		this.RoadStyleGridChooser = new palDotNet.palDialog.DialogStyleGridChooser();
		this.ConnectivityTab = new System.Windows.Forms.TabPage();
		this.ConnectivityCheckGroupBox = new palDotNet.palDialog.DialogCheckGroupBox();
		this.EndNodeFieldNameComboBox = new palDotNet.palDialog.DialogComboBox();
		this.StartNodeFieldNameComboBox = new palDotNet.palDialog.DialogComboBox();
		this.StartNodeFieldNameLabel = new palDotNet.palDialog.DialogLabel();
		this.EndNodeFieldNameLabel = new palDotNet.palDialog.DialogLabel();
		this.DirectionalityTab = new System.Windows.Forms.TabPage();
		this.OneWayCheckGroupBox = new palDotNet.palDialog.DialogCheckGroupBox();
		this.OneWayFieldValue_EndStart_TextBox = new palDotNet.palDialog.DialogTextBox();
		this.OneWayFieldValue_StartEnd_TextBox = new palDotNet.palDialog.DialogTextBox();
		this.OneWayFieldValue_Bidirectional_TextBox = new palDotNet.palDialog.DialogTextBox();
		this.OneWayFieldValue_EndStart_Label = new palDotNet.palDialog.DialogLabel();
		this.OneWayFieldValue_StartEnd_Label = new palDotNet.palDialog.DialogLabel();
		this.OneWayFieldValue_Bidirectional_Label = new palDotNet.palDialog.DialogLabel();
		this.OneWayFieldValuesLabel = new palDotNet.palDialog.DialogLabel();
		this.OneWayFieldNameLabel = new palDotNet.palDialog.DialogLabel();
		this.OneWayFieldNameComboBox = new palDotNet.palDialog.DialogComboBox();
		this.LaneCountTab = new System.Windows.Forms.TabPage();
		this.TwoFieldLaneCountCheckGroupBox = new palDotNet.palDialog.DialogCheckGroupBox();
		this.StartEndLaneCountLabel = new palDotNet.palDialog.DialogLabel();
		this.EndStartLaneCountFieldNameComboBox = new palDotNet.palDialog.DialogComboBox();
		this.StartEndLaneCountFieldNameComboBox = new palDotNet.palDialog.DialogComboBox();
		this.EndStartLaneCountLabel = new palDotNet.palDialog.DialogLabel();
		this.SingleFieldLaneCountCheckGroupBox = new palDotNet.palDialog.DialogCheckGroupBox();
		this.SingleLaneCountFieldNameLabel = new palDotNet.palDialog.DialogLabel();
		this.SingleLaneCountFieldNameComboBox = new palDotNet.palDialog.DialogComboBox();
		this.ZLevelTab = new System.Windows.Forms.TabPage();
		this.ZLevelCheckGroupBox = new palDotNet.palDialog.DialogCheckGroupBox();
		this.StartNodeZLevelFieldNameLabel = new palDotNet.palDialog.DialogLabel();
		this.EndNodeZLevelFieldNameComboBox = new palDotNet.palDialog.DialogComboBox();
		this.StartNodeZLevelFieldNameComboBox = new palDotNet.palDialog.DialogComboBox();
		this.EndNodeZLevelFieldNameLabel = new palDotNet.palDialog.DialogLabel();
		this.TabControl.SuspendLayout();
		this.FunctionalClassTab.SuspendLayout();
		this.FunctionalClassCheckGroupBox.SuspendLayout();
		this.ConnectivityTab.SuspendLayout();
		this.ConnectivityCheckGroupBox.SuspendLayout();
		this.DirectionalityTab.SuspendLayout();
		this.OneWayCheckGroupBox.SuspendLayout();
		this.LaneCountTab.SuspendLayout();
		this.TwoFieldLaneCountCheckGroupBox.SuspendLayout();
		this.SingleFieldLaneCountCheckGroupBox.SuspendLayout();
		this.ZLevelTab.SuspendLayout();
		this.ZLevelCheckGroupBox.SuspendLayout();
		base.SuspendLayout();
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
		this.TabControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TabControl.Controls.Add(this.FunctionalClassTab);
		this.TabControl.Controls.Add(this.ConnectivityTab);
		this.TabControl.Controls.Add(this.DirectionalityTab);
		this.TabControl.Controls.Add(this.LaneCountTab);
		this.TabControl.Controls.Add(this.ZLevelTab);
		this.TabControl.Location = new System.Drawing.Point(13, 13);
		this.TabControl.Name = "TabControl";
		this.TabControl.SelectedIndex = 0;
		this.TabControl.Size = new System.Drawing.Size(469, 355);
		this.TabControl.TabIndex = 0;
		this.FunctionalClassTab.Controls.Add(this.FunctionalClassCheckGroupBox);
		this.FunctionalClassTab.Location = new System.Drawing.Point(4, 22);
		this.FunctionalClassTab.Name = "FunctionalClassTab";
		this.FunctionalClassTab.Padding = new System.Windows.Forms.Padding(3);
		this.FunctionalClassTab.Size = new System.Drawing.Size(461, 329);
		this.FunctionalClassTab.TabIndex = 2;
		this.FunctionalClassTab.Text = "Functional Class";
		this.FunctionalClassTab.UseVisualStyleBackColor = true;
		this.FunctionalClassCheckGroupBox.Controls.Add(this.FunctionalClassFieldNameComboBox);
		this.FunctionalClassCheckGroupBox.Controls.Add(this.FunctionalClassFieldNameLabel);
		this.FunctionalClassCheckGroupBox.Controls.Add(this.RoadStyleGridChooser);
		this.FunctionalClassCheckGroupBox.Location = new System.Drawing.Point(6, 6);
		this.FunctionalClassCheckGroupBox.Name = "FunctionalClassCheckGroupBox";
		this.FunctionalClassCheckGroupBox.SaveToConfig = true;
		this.FunctionalClassCheckGroupBox.Size = new System.Drawing.Size(449, 317);
		this.FunctionalClassCheckGroupBox.TabIndex = 3;
		this.FunctionalClassCheckGroupBox.TabStop = false;
		this.FunctionalClassCheckGroupBox.Text = "Functional class";
		this.FunctionalClassCheckGroupBox.CheckStateChanged += new System.EventHandler(FunctionalClassCheckGroupBox_CheckStateChanged);
		this.FunctionalClassFieldNameComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.FunctionalClassFieldNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.FunctionalClassFieldNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.FunctionalClassFieldNameComboBox.FormattingEnabled = true;
		this.FunctionalClassFieldNameComboBox.Location = new System.Drawing.Point(224, 30);
		this.FunctionalClassFieldNameComboBox.Name = "FunctionalClassFieldNameComboBox";
		this.FunctionalClassFieldNameComboBox.SaveToConfig = true;
		this.FunctionalClassFieldNameComboBox.Size = new System.Drawing.Size(160, 21);
		this.FunctionalClassFieldNameComboBox.Sorted = true;
		this.FunctionalClassFieldNameComboBox.TabIndex = 1;
		this.FunctionalClassFieldNameComboBox.SelectedIndexChanged += new System.EventHandler(FunctionalClassFieldNameComboBox_SelectedIndexChanged);
		this.FunctionalClassFieldNameLabel.AutoSize = true;
		this.FunctionalClassFieldNameLabel.ForeColor = System.Drawing.SystemColors.ControlText;
		this.FunctionalClassFieldNameLabel.Location = new System.Drawing.Point(62, 33);
		this.FunctionalClassFieldNameLabel.Name = "FunctionalClassFieldNameLabel";
		this.FunctionalClassFieldNameLabel.SaveToConfig = false;
		this.FunctionalClassFieldNameLabel.Size = new System.Drawing.Size(108, 13);
		this.FunctionalClassFieldNameLabel.TabIndex = 0;
		this.FunctionalClassFieldNameLabel.Text = "Functional class field:";
		this.RoadStyleGridChooser.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.RoadStyleGridChooser.Location = new System.Drawing.Point(13, 74);
		this.RoadStyleGridChooser.Margin = new System.Windows.Forms.Padding(10);
		this.RoadStyleGridChooser.MinimumSize = new System.Drawing.Size(0, 140);
		this.RoadStyleGridChooser.Name = "RoadStyleGridChooser";
		this.RoadStyleGridChooser.Size = new System.Drawing.Size(423, 230);
		this.RoadStyleGridChooser.TabIndex = 2;
		this.ConnectivityTab.Controls.Add(this.ConnectivityCheckGroupBox);
		this.ConnectivityTab.Location = new System.Drawing.Point(4, 22);
		this.ConnectivityTab.Name = "ConnectivityTab";
		this.ConnectivityTab.Padding = new System.Windows.Forms.Padding(3);
		this.ConnectivityTab.Size = new System.Drawing.Size(461, 329);
		this.ConnectivityTab.TabIndex = 1;
		this.ConnectivityTab.Text = "Connectivity";
		this.ConnectivityTab.UseVisualStyleBackColor = true;
		this.ConnectivityCheckGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ConnectivityCheckGroupBox.Controls.Add(this.EndNodeFieldNameComboBox);
		this.ConnectivityCheckGroupBox.Controls.Add(this.StartNodeFieldNameComboBox);
		this.ConnectivityCheckGroupBox.Controls.Add(this.StartNodeFieldNameLabel);
		this.ConnectivityCheckGroupBox.Controls.Add(this.EndNodeFieldNameLabel);
		this.ConnectivityCheckGroupBox.ForeColor = System.Drawing.Color.Transparent;
		this.ConnectivityCheckGroupBox.Location = new System.Drawing.Point(7, 6);
		this.ConnectivityCheckGroupBox.Name = "ConnectivityCheckGroupBox";
		this.ConnectivityCheckGroupBox.SaveToConfig = true;
		this.ConnectivityCheckGroupBox.Size = new System.Drawing.Size(441, 99);
		this.ConnectivityCheckGroupBox.TabIndex = 0;
		this.ConnectivityCheckGroupBox.TabStop = false;
		this.ConnectivityCheckGroupBox.Text = "Connectivity";
		this.EndNodeFieldNameComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.EndNodeFieldNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.EndNodeFieldNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.EndNodeFieldNameComboBox.FormattingEnabled = true;
		this.EndNodeFieldNameComboBox.Location = new System.Drawing.Point(192, 58);
		this.EndNodeFieldNameComboBox.Name = "EndNodeFieldNameComboBox";
		this.EndNodeFieldNameComboBox.SaveToConfig = true;
		this.EndNodeFieldNameComboBox.Size = new System.Drawing.Size(164, 21);
		this.EndNodeFieldNameComboBox.Sorted = true;
		this.EndNodeFieldNameComboBox.TabIndex = 3;
		this.StartNodeFieldNameComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.StartNodeFieldNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.StartNodeFieldNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.StartNodeFieldNameComboBox.FormattingEnabled = true;
		this.StartNodeFieldNameComboBox.Location = new System.Drawing.Point(192, 29);
		this.StartNodeFieldNameComboBox.Name = "StartNodeFieldNameComboBox";
		this.StartNodeFieldNameComboBox.SaveToConfig = true;
		this.StartNodeFieldNameComboBox.Size = new System.Drawing.Size(164, 21);
		this.StartNodeFieldNameComboBox.Sorted = true;
		this.StartNodeFieldNameComboBox.TabIndex = 1;
		this.StartNodeFieldNameLabel.AutoSize = true;
		this.StartNodeFieldNameLabel.ForeColor = System.Drawing.SystemColors.ControlText;
		this.StartNodeFieldNameLabel.Location = new System.Drawing.Point(40, 32);
		this.StartNodeFieldNameLabel.Name = "StartNodeFieldNameLabel";
		this.StartNodeFieldNameLabel.SaveToConfig = false;
		this.StartNodeFieldNameLabel.Size = new System.Drawing.Size(81, 13);
		this.StartNodeFieldNameLabel.TabIndex = 0;
		this.StartNodeFieldNameLabel.Text = "Start node field:";
		this.EndNodeFieldNameLabel.AutoSize = true;
		this.EndNodeFieldNameLabel.ForeColor = System.Drawing.SystemColors.ControlText;
		this.EndNodeFieldNameLabel.Location = new System.Drawing.Point(40, 61);
		this.EndNodeFieldNameLabel.Name = "EndNodeFieldNameLabel";
		this.EndNodeFieldNameLabel.SaveToConfig = false;
		this.EndNodeFieldNameLabel.Size = new System.Drawing.Size(78, 13);
		this.EndNodeFieldNameLabel.TabIndex = 2;
		this.EndNodeFieldNameLabel.Text = "End node field:";
		this.DirectionalityTab.Controls.Add(this.OneWayCheckGroupBox);
		this.DirectionalityTab.Location = new System.Drawing.Point(4, 22);
		this.DirectionalityTab.Name = "DirectionalityTab";
		this.DirectionalityTab.Padding = new System.Windows.Forms.Padding(3);
		this.DirectionalityTab.Size = new System.Drawing.Size(461, 329);
		this.DirectionalityTab.TabIndex = 5;
		this.DirectionalityTab.Text = "Directionality";
		this.DirectionalityTab.UseVisualStyleBackColor = true;
		this.OneWayCheckGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.OneWayCheckGroupBox.Controls.Add(this.OneWayFieldValue_EndStart_TextBox);
		this.OneWayCheckGroupBox.Controls.Add(this.OneWayFieldValue_StartEnd_TextBox);
		this.OneWayCheckGroupBox.Controls.Add(this.OneWayFieldValue_Bidirectional_TextBox);
		this.OneWayCheckGroupBox.Controls.Add(this.OneWayFieldValue_EndStart_Label);
		this.OneWayCheckGroupBox.Controls.Add(this.OneWayFieldValue_StartEnd_Label);
		this.OneWayCheckGroupBox.Controls.Add(this.OneWayFieldValue_Bidirectional_Label);
		this.OneWayCheckGroupBox.Controls.Add(this.OneWayFieldValuesLabel);
		this.OneWayCheckGroupBox.Controls.Add(this.OneWayFieldNameLabel);
		this.OneWayCheckGroupBox.Controls.Add(this.OneWayFieldNameComboBox);
		this.OneWayCheckGroupBox.Location = new System.Drawing.Point(7, 7);
		this.OneWayCheckGroupBox.Name = "OneWayCheckGroupBox";
		this.OneWayCheckGroupBox.SaveToConfig = true;
		this.OneWayCheckGroupBox.Size = new System.Drawing.Size(441, 199);
		this.OneWayCheckGroupBox.TabIndex = 0;
		this.OneWayCheckGroupBox.TabStop = false;
		this.OneWayCheckGroupBox.Text = "One-way";
		this.OneWayFieldValue_EndStart_TextBox.Location = new System.Drawing.Point(257, 154);
		this.OneWayFieldValue_EndStart_TextBox.Name = "OneWayFieldValue_EndStart_TextBox";
		this.OneWayFieldValue_EndStart_TextBox.SaveToConfig = true;
		this.OneWayFieldValue_EndStart_TextBox.Size = new System.Drawing.Size(120, 20);
		this.OneWayFieldValue_EndStart_TextBox.TabIndex = 8;
		this.OneWayFieldValue_StartEnd_TextBox.Location = new System.Drawing.Point(257, 127);
		this.OneWayFieldValue_StartEnd_TextBox.Name = "OneWayFieldValue_StartEnd_TextBox";
		this.OneWayFieldValue_StartEnd_TextBox.SaveToConfig = true;
		this.OneWayFieldValue_StartEnd_TextBox.Size = new System.Drawing.Size(120, 20);
		this.OneWayFieldValue_StartEnd_TextBox.TabIndex = 6;
		this.OneWayFieldValue_Bidirectional_TextBox.Location = new System.Drawing.Point(257, 100);
		this.OneWayFieldValue_Bidirectional_TextBox.Name = "OneWayFieldValue_Bidirectional_TextBox";
		this.OneWayFieldValue_Bidirectional_TextBox.SaveToConfig = true;
		this.OneWayFieldValue_Bidirectional_TextBox.Size = new System.Drawing.Size(120, 20);
		this.OneWayFieldValue_Bidirectional_TextBox.TabIndex = 4;
		this.OneWayFieldValue_EndStart_Label.AutoSize = true;
		this.OneWayFieldValue_EndStart_Label.Location = new System.Drawing.Point(69, 157);
		this.OneWayFieldValue_EndStart_Label.Name = "OneWayFieldValue_EndStart_Label";
		this.OneWayFieldValue_EndStart_Label.SaveToConfig = false;
		this.OneWayFieldValue_EndStart_Label.Size = new System.Drawing.Size(118, 13);
		this.OneWayFieldValue_EndStart_Label.TabIndex = 7;
		this.OneWayFieldValue_EndStart_Label.Text = "End node to start node:";
		this.OneWayFieldValue_StartEnd_Label.AutoSize = true;
		this.OneWayFieldValue_StartEnd_Label.Location = new System.Drawing.Point(69, 130);
		this.OneWayFieldValue_StartEnd_Label.Name = "OneWayFieldValue_StartEnd_Label";
		this.OneWayFieldValue_StartEnd_Label.SaveToConfig = false;
		this.OneWayFieldValue_StartEnd_Label.Size = new System.Drawing.Size(119, 13);
		this.OneWayFieldValue_StartEnd_Label.TabIndex = 5;
		this.OneWayFieldValue_StartEnd_Label.Text = "Start node to end node:";
		this.OneWayFieldValue_Bidirectional_Label.AutoSize = true;
		this.OneWayFieldValue_Bidirectional_Label.Location = new System.Drawing.Point(69, 103);
		this.OneWayFieldValue_Bidirectional_Label.Name = "OneWayFieldValue_Bidirectional_Label";
		this.OneWayFieldValue_Bidirectional_Label.SaveToConfig = false;
		this.OneWayFieldValue_Bidirectional_Label.Size = new System.Drawing.Size(67, 13);
		this.OneWayFieldValue_Bidirectional_Label.TabIndex = 3;
		this.OneWayFieldValue_Bidirectional_Label.Text = "Bidirectional:";
		this.OneWayFieldValuesLabel.AutoSize = true;
		this.OneWayFieldValuesLabel.Location = new System.Drawing.Point(40, 71);
		this.OneWayFieldValuesLabel.Name = "OneWayFieldValuesLabel";
		this.OneWayFieldValuesLabel.SaveToConfig = false;
		this.OneWayFieldValuesLabel.Size = new System.Drawing.Size(243, 13);
		this.OneWayFieldValuesLabel.TabIndex = 2;
		this.OneWayFieldValuesLabel.Text = "One-way field values (leave blank if not specified):";
		this.OneWayFieldNameLabel.AutoSize = true;
		this.OneWayFieldNameLabel.Location = new System.Drawing.Point(40, 32);
		this.OneWayFieldNameLabel.Name = "OneWayFieldNameLabel";
		this.OneWayFieldNameLabel.SaveToConfig = false;
		this.OneWayFieldNameLabel.Size = new System.Drawing.Size(74, 13);
		this.OneWayFieldNameLabel.TabIndex = 0;
		this.OneWayFieldNameLabel.Text = "One-way field:";
		this.OneWayFieldNameComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.OneWayFieldNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.OneWayFieldNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.OneWayFieldNameComboBox.FormattingEnabled = true;
		this.OneWayFieldNameComboBox.Location = new System.Drawing.Point(213, 29);
		this.OneWayFieldNameComboBox.Name = "OneWayFieldNameComboBox";
		this.OneWayFieldNameComboBox.SaveToConfig = true;
		this.OneWayFieldNameComboBox.Size = new System.Drawing.Size(164, 21);
		this.OneWayFieldNameComboBox.Sorted = true;
		this.OneWayFieldNameComboBox.TabIndex = 1;
		this.LaneCountTab.Controls.Add(this.TwoFieldLaneCountCheckGroupBox);
		this.LaneCountTab.Controls.Add(this.SingleFieldLaneCountCheckGroupBox);
		this.LaneCountTab.Location = new System.Drawing.Point(4, 22);
		this.LaneCountTab.Name = "LaneCountTab";
		this.LaneCountTab.Padding = new System.Windows.Forms.Padding(3);
		this.LaneCountTab.Size = new System.Drawing.Size(461, 329);
		this.LaneCountTab.TabIndex = 3;
		this.LaneCountTab.Text = "Lane Count";
		this.LaneCountTab.UseVisualStyleBackColor = true;
		this.TwoFieldLaneCountCheckGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TwoFieldLaneCountCheckGroupBox.Controls.Add(this.StartEndLaneCountLabel);
		this.TwoFieldLaneCountCheckGroupBox.Controls.Add(this.EndStartLaneCountFieldNameComboBox);
		this.TwoFieldLaneCountCheckGroupBox.Controls.Add(this.StartEndLaneCountFieldNameComboBox);
		this.TwoFieldLaneCountCheckGroupBox.Controls.Add(this.EndStartLaneCountLabel);
		this.TwoFieldLaneCountCheckGroupBox.Location = new System.Drawing.Point(7, 88);
		this.TwoFieldLaneCountCheckGroupBox.Name = "TwoFieldLaneCountCheckGroupBox";
		this.TwoFieldLaneCountCheckGroupBox.SaveToConfig = true;
		this.TwoFieldLaneCountCheckGroupBox.Size = new System.Drawing.Size(448, 107);
		this.TwoFieldLaneCountCheckGroupBox.TabIndex = 1;
		this.TwoFieldLaneCountCheckGroupBox.TabStop = false;
		this.TwoFieldLaneCountCheckGroupBox.Text = "Two-field lane count";
		this.TwoFieldLaneCountCheckGroupBox.CheckedChanged += new System.EventHandler(TwoFieldLaneCountCheckGroupBox_CheckedChanged);
		this.StartEndLaneCountLabel.AutoSize = true;
		this.StartEndLaneCountLabel.Location = new System.Drawing.Point(39, 34);
		this.StartEndLaneCountLabel.Name = "StartEndLaneCountLabel";
		this.StartEndLaneCountLabel.SaveToConfig = false;
		this.StartEndLaneCountLabel.Size = new System.Drawing.Size(128, 13);
		this.StartEndLaneCountLabel.TabIndex = 0;
		this.StartEndLaneCountLabel.Text = "Start-end lane count field:";
		this.EndStartLaneCountFieldNameComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.EndStartLaneCountFieldNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.EndStartLaneCountFieldNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.EndStartLaneCountFieldNameComboBox.FormattingEnabled = true;
		this.EndStartLaneCountFieldNameComboBox.Location = new System.Drawing.Point(213, 65);
		this.EndStartLaneCountFieldNameComboBox.Name = "EndStartLaneCountFieldNameComboBox";
		this.EndStartLaneCountFieldNameComboBox.SaveToConfig = true;
		this.EndStartLaneCountFieldNameComboBox.Size = new System.Drawing.Size(164, 21);
		this.EndStartLaneCountFieldNameComboBox.Sorted = true;
		this.EndStartLaneCountFieldNameComboBox.TabIndex = 3;
		this.StartEndLaneCountFieldNameComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.StartEndLaneCountFieldNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.StartEndLaneCountFieldNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.StartEndLaneCountFieldNameComboBox.FormattingEnabled = true;
		this.StartEndLaneCountFieldNameComboBox.Location = new System.Drawing.Point(213, 31);
		this.StartEndLaneCountFieldNameComboBox.Name = "StartEndLaneCountFieldNameComboBox";
		this.StartEndLaneCountFieldNameComboBox.SaveToConfig = true;
		this.StartEndLaneCountFieldNameComboBox.Size = new System.Drawing.Size(164, 21);
		this.StartEndLaneCountFieldNameComboBox.Sorted = true;
		this.StartEndLaneCountFieldNameComboBox.TabIndex = 1;
		this.EndStartLaneCountLabel.AutoSize = true;
		this.EndStartLaneCountLabel.Location = new System.Drawing.Point(39, 68);
		this.EndStartLaneCountLabel.Name = "EndStartLaneCountLabel";
		this.EndStartLaneCountLabel.SaveToConfig = false;
		this.EndStartLaneCountLabel.Size = new System.Drawing.Size(127, 13);
		this.EndStartLaneCountLabel.TabIndex = 2;
		this.EndStartLaneCountLabel.Text = "End-start lane count field:";
		this.SingleFieldLaneCountCheckGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.SingleFieldLaneCountCheckGroupBox.Controls.Add(this.SingleLaneCountFieldNameLabel);
		this.SingleFieldLaneCountCheckGroupBox.Controls.Add(this.SingleLaneCountFieldNameComboBox);
		this.SingleFieldLaneCountCheckGroupBox.ForeColor = System.Drawing.Color.Transparent;
		this.SingleFieldLaneCountCheckGroupBox.Location = new System.Drawing.Point(6, 6);
		this.SingleFieldLaneCountCheckGroupBox.Name = "SingleFieldLaneCountCheckGroupBox";
		this.SingleFieldLaneCountCheckGroupBox.SaveToConfig = true;
		this.SingleFieldLaneCountCheckGroupBox.Size = new System.Drawing.Size(449, 75);
		this.SingleFieldLaneCountCheckGroupBox.TabIndex = 0;
		this.SingleFieldLaneCountCheckGroupBox.TabStop = false;
		this.SingleFieldLaneCountCheckGroupBox.Text = "Single-field lane count";
		this.SingleFieldLaneCountCheckGroupBox.CheckedChanged += new System.EventHandler(SingleFieldLaneCountCheckGroupBox_CheckedChanged);
		this.SingleLaneCountFieldNameLabel.AutoSize = true;
		this.SingleLaneCountFieldNameLabel.ForeColor = System.Drawing.SystemColors.ControlText;
		this.SingleLaneCountFieldNameLabel.Location = new System.Drawing.Point(40, 32);
		this.SingleLaneCountFieldNameLabel.Name = "SingleLaneCountFieldNameLabel";
		this.SingleLaneCountFieldNameLabel.SaveToConfig = false;
		this.SingleLaneCountFieldNameLabel.Size = new System.Drawing.Size(86, 13);
		this.SingleLaneCountFieldNameLabel.TabIndex = 0;
		this.SingleLaneCountFieldNameLabel.Text = "Lane count field:";
		this.SingleLaneCountFieldNameComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.SingleLaneCountFieldNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.SingleLaneCountFieldNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.SingleLaneCountFieldNameComboBox.FormattingEnabled = true;
		this.SingleLaneCountFieldNameComboBox.Location = new System.Drawing.Point(214, 29);
		this.SingleLaneCountFieldNameComboBox.Name = "SingleLaneCountFieldNameComboBox";
		this.SingleLaneCountFieldNameComboBox.SaveToConfig = true;
		this.SingleLaneCountFieldNameComboBox.Size = new System.Drawing.Size(164, 21);
		this.SingleLaneCountFieldNameComboBox.Sorted = true;
		this.SingleLaneCountFieldNameComboBox.TabIndex = 1;
		this.ZLevelTab.Controls.Add(this.ZLevelCheckGroupBox);
		this.ZLevelTab.Location = new System.Drawing.Point(4, 22);
		this.ZLevelTab.Name = "ZLevelTab";
		this.ZLevelTab.Padding = new System.Windows.Forms.Padding(3);
		this.ZLevelTab.Size = new System.Drawing.Size(461, 329);
		this.ZLevelTab.TabIndex = 4;
		this.ZLevelTab.Text = "Z-Levels";
		this.ZLevelTab.UseVisualStyleBackColor = true;
		this.ZLevelCheckGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ZLevelCheckGroupBox.Controls.Add(this.StartNodeZLevelFieldNameLabel);
		this.ZLevelCheckGroupBox.Controls.Add(this.EndNodeZLevelFieldNameComboBox);
		this.ZLevelCheckGroupBox.Controls.Add(this.StartNodeZLevelFieldNameComboBox);
		this.ZLevelCheckGroupBox.Controls.Add(this.EndNodeZLevelFieldNameLabel);
		this.ZLevelCheckGroupBox.Location = new System.Drawing.Point(7, 7);
		this.ZLevelCheckGroupBox.Name = "ZLevelCheckGroupBox";
		this.ZLevelCheckGroupBox.SaveToConfig = true;
		this.ZLevelCheckGroupBox.Size = new System.Drawing.Size(478, 107);
		this.ZLevelCheckGroupBox.TabIndex = 0;
		this.ZLevelCheckGroupBox.TabStop = false;
		this.ZLevelCheckGroupBox.Text = "Z-levels";
		this.StartNodeZLevelFieldNameLabel.AutoSize = true;
		this.StartNodeZLevelFieldNameLabel.ForeColor = System.Drawing.SystemColors.ControlText;
		this.StartNodeZLevelFieldNameLabel.Location = new System.Drawing.Point(40, 32);
		this.StartNodeZLevelFieldNameLabel.Name = "StartNodeZLevelFieldNameLabel";
		this.StartNodeZLevelFieldNameLabel.SaveToConfig = false;
		this.StartNodeZLevelFieldNameLabel.Size = new System.Drawing.Size(116, 13);
		this.StartNodeZLevelFieldNameLabel.TabIndex = 0;
		this.StartNodeZLevelFieldNameLabel.Text = "Start node Z-level field:";
		this.EndNodeZLevelFieldNameComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.EndNodeZLevelFieldNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.EndNodeZLevelFieldNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.EndNodeZLevelFieldNameComboBox.FormattingEnabled = true;
		this.EndNodeZLevelFieldNameComboBox.Location = new System.Drawing.Point(214, 65);
		this.EndNodeZLevelFieldNameComboBox.Name = "EndNodeZLevelFieldNameComboBox";
		this.EndNodeZLevelFieldNameComboBox.SaveToConfig = true;
		this.EndNodeZLevelFieldNameComboBox.Size = new System.Drawing.Size(164, 21);
		this.EndNodeZLevelFieldNameComboBox.Sorted = true;
		this.EndNodeZLevelFieldNameComboBox.TabIndex = 3;
		this.StartNodeZLevelFieldNameComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.StartNodeZLevelFieldNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.StartNodeZLevelFieldNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.StartNodeZLevelFieldNameComboBox.FormattingEnabled = true;
		this.StartNodeZLevelFieldNameComboBox.Location = new System.Drawing.Point(214, 29);
		this.StartNodeZLevelFieldNameComboBox.Name = "StartNodeZLevelFieldNameComboBox";
		this.StartNodeZLevelFieldNameComboBox.SaveToConfig = true;
		this.StartNodeZLevelFieldNameComboBox.Size = new System.Drawing.Size(164, 21);
		this.StartNodeZLevelFieldNameComboBox.Sorted = true;
		this.StartNodeZLevelFieldNameComboBox.TabIndex = 1;
		this.EndNodeZLevelFieldNameLabel.AutoSize = true;
		this.EndNodeZLevelFieldNameLabel.Location = new System.Drawing.Point(40, 68);
		this.EndNodeZLevelFieldNameLabel.Name = "EndNodeZLevelFieldNameLabel";
		this.EndNodeZLevelFieldNameLabel.SaveToConfig = false;
		this.EndNodeZLevelFieldNameLabel.Size = new System.Drawing.Size(117, 13);
		this.EndNodeZLevelFieldNameLabel.TabIndex = 2;
		this.EndNodeZLevelFieldNameLabel.Text = "End node Z-Level field:";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.CancelBtn;
		base.ClientSize = new System.Drawing.Size(494, 424);
		base.Controls.Add(this.TabControl);
		base.Controls.Add(this.OKBtn);
		base.Controls.Add(this.CancelBtn);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "GisImportRoadsCustomDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "GIS Mode - Import Roads - Custom";
		this.TabControl.ResumeLayout(false);
		this.FunctionalClassTab.ResumeLayout(false);
		this.FunctionalClassCheckGroupBox.ResumeLayout(false);
		this.FunctionalClassCheckGroupBox.PerformLayout();
		this.ConnectivityTab.ResumeLayout(false);
		this.ConnectivityCheckGroupBox.ResumeLayout(false);
		this.ConnectivityCheckGroupBox.PerformLayout();
		this.DirectionalityTab.ResumeLayout(false);
		this.OneWayCheckGroupBox.ResumeLayout(false);
		this.OneWayCheckGroupBox.PerformLayout();
		this.LaneCountTab.ResumeLayout(false);
		this.TwoFieldLaneCountCheckGroupBox.ResumeLayout(false);
		this.TwoFieldLaneCountCheckGroupBox.PerformLayout();
		this.SingleFieldLaneCountCheckGroupBox.ResumeLayout(false);
		this.SingleFieldLaneCountCheckGroupBox.PerformLayout();
		this.ZLevelTab.ResumeLayout(false);
		this.ZLevelCheckGroupBox.ResumeLayout(false);
		this.ZLevelCheckGroupBox.PerformLayout();
		base.ResumeLayout(false);
	}

	public GisImportRoadsCustomDialog()
	{
		InitializeComponent();
		ResetAttributeTabs();
		FunctionalClassCheckGroupBox.DisableChildrenIfUnchecked = false;
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		InitAttributeTabs();
		SyncOkButton();
	}

	public void InitAttributeTabs()
	{
		FunctionalClassCheckGroupBox.Checked = false;
		ConnectivityCheckGroupBox.Checked = false;
		OneWayCheckGroupBox.Checked = false;
		SingleFieldLaneCountCheckGroupBox.Checked = false;
		TwoFieldLaneCountCheckGroupBox.Checked = false;
		ZLevelCheckGroupBox.Checked = false;
	}

	public void ResetAttributeTabs()
	{
		ResetAttrMap_FunctionalClassFieldNameItems();
		ResetAttrMap_StartNodeFieldNameItems();
		ResetAttrMap_EndNodeFieldNameItems();
		ResetAttrMap_OneWayFieldNameItems();
		ResetAttrMap_OneWayFieldValues();
		ResetAttrMap_LaneCountFieldNameItems();
		ResetAttrMap_ZLevelFieldNameItems();
	}

	public void SetAttrMap_StartNodeFieldNameItems(string[] itemNames)
	{
		StartNodeFieldNameComboBox.Items.Clear();
		StartNodeFieldNameComboBox.Items.AddRange(itemNames);
		StartNodeFieldNameComboBox.SelectedIndex = -1;
	}

	public void SetAttrMap_EndNodeFieldNameItems(string[] itemNames)
	{
		EndNodeFieldNameComboBox.Items.Clear();
		EndNodeFieldNameComboBox.Items.AddRange(itemNames);
		EndNodeFieldNameComboBox.SelectedIndex = -1;
	}

	public void ResetAttrMap_StartNodeFieldNameItems()
	{
		StartNodeFieldNameComboBox.Items.Clear();
		StartNodeFieldNameComboBox.SelectedIndex = -1;
	}

	public void ResetAttrMap_EndNodeFieldNameItems()
	{
		EndNodeFieldNameComboBox.Items.Clear();
		EndNodeFieldNameComboBox.SelectedIndex = -1;
	}

	public void SetAttrMap_OneWayFieldNameItems(string[] itemNames)
	{
		OneWayFieldNameComboBox.Items.Clear();
		OneWayFieldNameComboBox.Items.AddRange(itemNames);
		OneWayFieldNameComboBox.SelectedIndex = -1;
	}

	public void ResetAttrMap_OneWayFieldNameItems()
	{
		OneWayFieldNameComboBox.Items.Clear();
		OneWayFieldNameComboBox.SelectedIndex = -1;
	}

	public void ResetAttrMap_OneWayFieldValues()
	{
		OneWayFieldValue_Bidirectional_TextBox.Text = "";
		OneWayFieldValue_StartEnd_TextBox.Text = "";
		OneWayFieldValue_EndStart_TextBox.Text = "";
	}

	public void SetAttrMap_FunctionalClassFieldNameItems(string[] itemNames)
	{
		FunctionalClassFieldNameComboBox.Items.Clear();
		FunctionalClassFieldNameComboBox.Items.AddRange(itemNames);
		FunctionalClassFieldNameComboBox.SelectedIndex = -1;
		SyncFunctionalClassStyleChooser();
	}

	public void ResetAttrMap_FunctionalClassFieldNameItems()
	{
		FunctionalClassFieldNameComboBox.Items.Clear();
		FunctionalClassFieldNameComboBox.SelectedIndex = -1;
		SyncFunctionalClassStyleChooser();
	}

	public void ClearRoadStyles()
	{
		RoadStyleGridChooser.ClearStyles();
	}

	public string GetFunctionalClassFieldName()
	{
		return FunctionalClassFieldNameComboBox.Text;
	}

	private void FunctionalClassFieldNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		SyncFunctionalClassStyleChooser();
	}

	private void FunctionalClassCheckGroupBox_CheckStateChanged(object sender, EventArgs e)
	{
		if (FunctionalClassCheckGroupBox.Checked)
		{
			FunctionalClassFieldNameComboBox.Enabled = true;
			SyncFunctionalClassStyleChooser();
		}
		else
		{
			FunctionalClassFieldNameComboBox.Enabled = false;
			RoadStyleGridChooser.Enabled = false;
		}
	}

	private void SyncFunctionalClassStyleChooser()
	{
		if (FunctionalClassCheckGroupBox.Checked && FunctionalClassFieldNameComboBox.SelectedIndex != -1)
		{
			RoadStyleGridChooser.Enabled = true;
		}
		else
		{
			RoadStyleGridChooser.Enabled = false;
		}
	}

	public void SetAttrMap_SingleLaneCountFieldNameItems(string[] itemNames)
	{
		SingleLaneCountFieldNameComboBox.Items.Clear();
		SingleLaneCountFieldNameComboBox.Items.AddRange(itemNames);
		SingleLaneCountFieldNameComboBox.SelectedIndex = -1;
	}

	public void SetAttrMap_StartEndLaneCountFieldNameItems(string[] itemNames)
	{
		StartEndLaneCountFieldNameComboBox.Items.Clear();
		StartEndLaneCountFieldNameComboBox.Items.AddRange(itemNames);
		StartEndLaneCountFieldNameComboBox.SelectedIndex = -1;
	}

	public void SetAttrMap_EndStartLaneCountFieldNameItems(string[] itemNames)
	{
		EndStartLaneCountFieldNameComboBox.Items.Clear();
		EndStartLaneCountFieldNameComboBox.Items.AddRange(itemNames);
		EndStartLaneCountFieldNameComboBox.SelectedIndex = -1;
	}

	public void ResetAttrMap_LaneCountFieldNameItems()
	{
		SingleLaneCountFieldNameComboBox.Items.Clear();
		SingleLaneCountFieldNameComboBox.SelectedIndex = -1;
		StartEndLaneCountFieldNameComboBox.Items.Clear();
		StartEndLaneCountFieldNameComboBox.SelectedIndex = -1;
		EndStartLaneCountFieldNameComboBox.Items.Clear();
		EndStartLaneCountFieldNameComboBox.SelectedIndex = -1;
	}

	private void SingleFieldLaneCountCheckGroupBox_CheckedChanged(object sender, EventArgs e)
	{
		if (SingleFieldLaneCountCheckGroupBox.Checked)
		{
			TwoFieldLaneCountCheckGroupBox.Checked = false;
		}
	}

	private void TwoFieldLaneCountCheckGroupBox_CheckedChanged(object sender, EventArgs e)
	{
		if (TwoFieldLaneCountCheckGroupBox.Checked)
		{
			SingleFieldLaneCountCheckGroupBox.Checked = false;
		}
	}

	public void SetAttrMap_StartNodeZLevelFieldNameItems(string[] itemNames)
	{
		StartNodeZLevelFieldNameComboBox.Items.Clear();
		StartNodeZLevelFieldNameComboBox.Items.AddRange(itemNames);
		StartNodeZLevelFieldNameComboBox.SelectedIndex = -1;
	}

	public void SetAttrMap_EndNodeZLevelFieldNameItems(string[] itemNames)
	{
		EndNodeZLevelFieldNameComboBox.Items.Clear();
		EndNodeZLevelFieldNameComboBox.Items.AddRange(itemNames);
		EndNodeZLevelFieldNameComboBox.SelectedIndex = -1;
	}

	public void ResetAttrMap_ZLevelFieldNameItems()
	{
		StartNodeZLevelFieldNameComboBox.Items.Clear();
		StartNodeZLevelFieldNameComboBox.SelectedIndex = -1;
		EndNodeZLevelFieldNameComboBox.Items.Clear();
		EndNodeZLevelFieldNameComboBox.SelectedIndex = -1;
	}

	private void SyncOkButton()
	{
		AllowSubmitDialog = true;
		OKBtn.Enabled = AllowSubmitDialog;
	}

	private void OKBtn_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
	}
}
