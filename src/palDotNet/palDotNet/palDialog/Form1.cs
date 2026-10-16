using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace palDotNet.palDialog;

[DialogBoxCreator]
public class Form1 : DialogBoxBase
{
	private IContainer components;

	private DialogNumericUpDownFloat dialogNumericUpDown1;

	private DialogCheckBox dialogCheckBox1;

	private Button buttonOK;

	private Button buttonCancel;

	private GroupBox groupBox1;

	private DialogComboBox dialogComboBox1;

	private DialogPathLoader dialogPathLoader1;

	private DialogTextBox dialogTextBox1;

	private GroupBox groupBox2;

	private DialogRadioButton dialogRadioButton2;

	private DialogRadioButton dialogRadioButton1;

	private DialogNumericUpDownFloat dialogNumericUpDownFloat1;

	private DialogCheckBox dialogCheckBox2;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private GroupBox groupBox3;

	private DialogTextBox dialogTextBox2;

	private GroupBox groupBox4;

	private DialogRadioButton dialogRadioButton3;

	private DialogRadioButton dialogRadioButton4;

	private DialogNumericUpDownFloat dialogNumericUpDownFloat2;

	private DialogPathLoader dialogPathLoader2;

	private DialogComboBox dialogComboBox2;

	private DialogCheckBox dialogCheckBox3;

	private DialogNumericUpDownFloat dialogNumericUpDownFloat3;

	private Label label1;

	private Label label2;

	private GroupBox groupBox5;

	private Label label3;

	private DialogTextBox dialogTextBox3;

	private GroupBox groupBox6;

	private DialogRadioButton dialogRadioButton5;

	private DialogRadioButton dialogRadioButton6;

	private DialogNumericUpDownFloat dialogNumericUpDownFloat4;

	private DialogPathLoader dialogPathLoader3;

	private DialogComboBox dialogComboBox3;

	private DialogCheckBox dialogCheckBox4;

	private DialogNumericUpDownFloat dialogNumericUpDownFloat5;

	private DialogListBox dialogListBox1;

	private TabPage tabPage3;

	private DialogCheckGroupBox dialogCheckGroupBox1;

	private DialogLabel dialogLabel1;

	private DialogListBox dialogListBox2;

	private DialogCheckBox dialogCheckBox5;

	private DialogComboBox dialogComboBox4;

	private DialogNumericUpDown dialogNumericUpDown2;

	private TabPage StyleGridChooserTab;

	private DialogStyleGridChooser StyleGridChooser;

	private TabPage AnchorPanelTab;

	private DialogAnchorPanel AnchorPanel;

	private DialogLabel AnchorYLabel;

	private DialogLabel AnchorXLabel;

	private Panel panel2;

	private Panel panel1;

	private DialogRadioButton AnchorShrinkX;

	private DialogRadioButton AnchorGrowX;

	private DialogRadioButton AnchorShrinkY;

	private DialogRadioButton AnchorGrowY;

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
		this.dialogNumericUpDown1 = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.dialogCheckBox1 = new palDotNet.palDialog.DialogCheckBox();
		this.buttonOK = new System.Windows.Forms.Button();
		this.buttonCancel = new System.Windows.Forms.Button();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label2 = new System.Windows.Forms.Label();
		this.dialogTextBox1 = new palDotNet.palDialog.DialogTextBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dialogRadioButton2 = new palDotNet.palDialog.DialogRadioButton();
		this.dialogRadioButton1 = new palDotNet.palDialog.DialogRadioButton();
		this.dialogNumericUpDownFloat1 = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.dialogPathLoader1 = new palDotNet.palDialog.DialogPathLoader();
		this.dialogComboBox1 = new palDotNet.palDialog.DialogComboBox();
		this.dialogCheckBox2 = new palDotNet.palDialog.DialogCheckBox();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.label1 = new System.Windows.Forms.Label();
		this.dialogTextBox2 = new palDotNet.palDialog.DialogTextBox();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.dialogRadioButton3 = new palDotNet.palDialog.DialogRadioButton();
		this.dialogRadioButton4 = new palDotNet.palDialog.DialogRadioButton();
		this.dialogNumericUpDownFloat2 = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.dialogPathLoader2 = new palDotNet.palDialog.DialogPathLoader();
		this.dialogComboBox2 = new palDotNet.palDialog.DialogComboBox();
		this.dialogCheckBox3 = new palDotNet.palDialog.DialogCheckBox();
		this.dialogNumericUpDownFloat3 = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.tabPage3 = new System.Windows.Forms.TabPage();
		this.dialogCheckGroupBox1 = new palDotNet.palDialog.DialogCheckGroupBox();
		this.dialogLabel1 = new palDotNet.palDialog.DialogLabel();
		this.dialogListBox2 = new palDotNet.palDialog.DialogListBox();
		this.dialogCheckBox5 = new palDotNet.palDialog.DialogCheckBox();
		this.dialogComboBox4 = new palDotNet.palDialog.DialogComboBox();
		this.dialogNumericUpDown2 = new palDotNet.palDialog.DialogNumericUpDown();
		this.StyleGridChooserTab = new System.Windows.Forms.TabPage();
		this.StyleGridChooser = new palDotNet.palDialog.DialogStyleGridChooser();
		this.AnchorPanelTab = new System.Windows.Forms.TabPage();
		this.panel2 = new System.Windows.Forms.Panel();
		this.AnchorShrinkY = new palDotNet.palDialog.DialogRadioButton();
		this.AnchorGrowY = new palDotNet.palDialog.DialogRadioButton();
		this.AnchorYLabel = new palDotNet.palDialog.DialogLabel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.AnchorShrinkX = new palDotNet.palDialog.DialogRadioButton();
		this.AnchorGrowX = new palDotNet.palDialog.DialogRadioButton();
		this.AnchorXLabel = new palDotNet.palDialog.DialogLabel();
		this.AnchorPanel = new palDotNet.palDialog.DialogAnchorPanel();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.dialogListBox1 = new palDotNet.palDialog.DialogListBox();
		this.label3 = new System.Windows.Forms.Label();
		this.dialogTextBox3 = new palDotNet.palDialog.DialogTextBox();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.dialogRadioButton5 = new palDotNet.palDialog.DialogRadioButton();
		this.dialogRadioButton6 = new palDotNet.palDialog.DialogRadioButton();
		this.dialogNumericUpDownFloat4 = new palDotNet.palDialog.DialogNumericUpDownFloat();
		this.dialogPathLoader3 = new palDotNet.palDialog.DialogPathLoader();
		this.dialogComboBox3 = new palDotNet.palDialog.DialogComboBox();
		this.dialogCheckBox4 = new palDotNet.palDialog.DialogCheckBox();
		this.dialogNumericUpDownFloat5 = new palDotNet.palDialog.DialogNumericUpDownFloat();
		((System.ComponentModel.ISupportInitialize)this.dialogNumericUpDown1).BeginInit();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dialogNumericUpDownFloat1).BeginInit();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dialogNumericUpDownFloat2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dialogNumericUpDownFloat3).BeginInit();
		this.tabPage3.SuspendLayout();
		this.dialogCheckGroupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dialogNumericUpDown2).BeginInit();
		this.StyleGridChooserTab.SuspendLayout();
		this.AnchorPanelTab.SuspendLayout();
		this.panel2.SuspendLayout();
		this.panel1.SuspendLayout();
		this.groupBox5.SuspendLayout();
		this.groupBox6.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dialogNumericUpDownFloat4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dialogNumericUpDownFloat5).BeginInit();
		base.SuspendLayout();
		this.dialogNumericUpDown1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.dialogNumericUpDown1.Location = new System.Drawing.Point(380, 15);
		this.dialogNumericUpDown1.Name = "dialogNumericUpDown1";
		this.dialogNumericUpDown1.Size = new System.Drawing.Size(120, 20);
		this.dialogNumericUpDown1.TabIndex = 1;
		this.dialogNumericUpDown1.Value = new decimal(new int[4] { 33, 0, 0, 0 });
		this.dialogCheckBox1.AutoSize = true;
		this.dialogCheckBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.dialogCheckBox1.Location = new System.Drawing.Point(15, 19);
		this.dialogCheckBox1.Name = "dialogCheckBox1";
		this.dialogCheckBox1.Size = new System.Drawing.Size(106, 17);
		this.dialogCheckBox1.TabIndex = 2;
		this.dialogCheckBox1.Text = "dialogCheckBox1";
		this.dialogCheckBox1.UseVisualStyleBackColor = true;
		this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.buttonOK.Location = new System.Drawing.Point(401, 455);
		this.buttonOK.Name = "buttonOK";
		this.buttonOK.Size = new System.Drawing.Size(70, 23);
		this.buttonOK.TabIndex = 3;
		this.buttonOK.Text = "OK";
		this.buttonOK.UseVisualStyleBackColor = true;
		this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.buttonCancel.Location = new System.Drawing.Point(477, 455);
		this.buttonCancel.Name = "buttonCancel";
		this.buttonCancel.Size = new System.Drawing.Size(70, 23);
		this.buttonCancel.TabIndex = 3;
		this.buttonCancel.Text = "Cancel";
		this.buttonCancel.UseVisualStyleBackColor = true;
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.dialogTextBox1);
		this.groupBox1.Controls.Add(this.groupBox2);
		this.groupBox1.Controls.Add(this.dialogNumericUpDownFloat1);
		this.groupBox1.Controls.Add(this.dialogPathLoader1);
		this.groupBox1.Controls.Add(this.dialogComboBox1);
		this.groupBox1.Controls.Add(this.dialogCheckBox1);
		this.groupBox1.Controls.Add(this.dialogNumericUpDown1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.groupBox1.Location = new System.Drawing.Point(3, 3);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(521, 205);
		this.groupBox1.TabIndex = 4;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "groupBox1";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(12, 185);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(35, 13);
		this.label2.TabIndex = 10;
		this.label2.Text = "label2";
		this.dialogTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.dialogTextBox1.Location = new System.Drawing.Point(15, 143);
		this.dialogTextBox1.Name = "dialogTextBox1";
		this.dialogTextBox1.Size = new System.Drawing.Size(485, 20);
		this.dialogTextBox1.TabIndex = 8;
		this.groupBox2.Controls.Add(this.dialogRadioButton2);
		this.groupBox2.Controls.Add(this.dialogRadioButton1);
		this.groupBox2.Location = new System.Drawing.Point(15, 73);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(485, 45);
		this.groupBox2.TabIndex = 7;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "groupBox2";
		this.dialogRadioButton2.AutoSize = true;
		this.dialogRadioButton2.Location = new System.Drawing.Point(307, 19);
		this.dialogRadioButton2.Name = "dialogRadioButton2";
		this.dialogRadioButton2.Size = new System.Drawing.Size(118, 17);
		this.dialogRadioButton2.TabIndex = 6;
		this.dialogRadioButton2.TabStop = true;
		this.dialogRadioButton2.Text = "dialogRadioButton1";
		this.dialogRadioButton2.UseVisualStyleBackColor = true;
		this.dialogRadioButton1.AutoSize = true;
		this.dialogRadioButton1.Location = new System.Drawing.Point(6, 19);
		this.dialogRadioButton1.Name = "dialogRadioButton1";
		this.dialogRadioButton1.Size = new System.Drawing.Size(118, 17);
		this.dialogRadioButton1.TabIndex = 6;
		this.dialogRadioButton1.TabStop = true;
		this.dialogRadioButton1.Text = "dialogRadioButton1";
		this.dialogRadioButton1.UseVisualStyleBackColor = true;
		this.dialogNumericUpDownFloat1.Location = new System.Drawing.Point(254, 15);
		this.dialogNumericUpDownFloat1.Name = "dialogNumericUpDownFloat1";
		this.dialogNumericUpDownFloat1.Size = new System.Drawing.Size(120, 20);
		this.dialogNumericUpDownFloat1.TabIndex = 5;
		this.dialogPathLoader1.BackColor = System.Drawing.Color.Transparent;
		this.dialogPathLoader1.BrowseType = palDotNet.palDialog.DialogPathLoader.EnumBrowseType.eSave;
		this.dialogPathLoader1.CheckFileExists = false;
		this.dialogPathLoader1.Description = null;
		this.dialogPathLoader1.Filter = null;
		this.dialogPathLoader1.Location = new System.Drawing.Point(15, 42);
		this.dialogPathLoader1.Name = "dialogPathLoader1";
		this.dialogPathLoader1.Path = "";
		this.dialogPathLoader1.Size = new System.Drawing.Size(485, 24);
		this.dialogPathLoader1.TabIndex = 4;
		this.dialogComboBox1.FormattingEnabled = true;
		this.dialogComboBox1.Items.AddRange(new object[3] { "Item1", "Item2", "Item3" });
		this.dialogComboBox1.Location = new System.Drawing.Point(127, 15);
		this.dialogComboBox1.Name = "dialogComboBox1";
		this.dialogComboBox1.Size = new System.Drawing.Size(121, 21);
		this.dialogComboBox1.TabIndex = 3;
		this.dialogCheckBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.dialogCheckBox2.AutoSize = true;
		this.dialogCheckBox2.Location = new System.Drawing.Point(12, 459);
		this.dialogCheckBox2.Name = "dialogCheckBox2";
		this.dialogCheckBox2.Size = new System.Drawing.Size(116, 17);
		this.dialogCheckBox2.TabIndex = 5;
		this.dialogCheckBox2.Text = "Disable All Controls";
		this.dialogCheckBox2.UseVisualStyleBackColor = true;
		this.dialogCheckBox2.Click += new System.EventHandler(dialogCheckBox2_Click);
		this.tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Controls.Add(this.tabPage3);
		this.tabControl1.Controls.Add(this.StyleGridChooserTab);
		this.tabControl1.Controls.Add(this.AnchorPanelTab);
		this.tabControl1.Location = new System.Drawing.Point(12, 12);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(535, 237);
		this.tabControl1.TabIndex = 6;
		this.tabPage1.Controls.Add(this.groupBox1);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(527, 211);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "tabPage1";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.tabPage2.Controls.Add(this.groupBox3);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(527, 211);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "tabPage2";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.groupBox3.Controls.Add(this.label1);
		this.groupBox3.Controls.Add(this.dialogTextBox2);
		this.groupBox3.Controls.Add(this.groupBox4);
		this.groupBox3.Controls.Add(this.dialogNumericUpDownFloat2);
		this.groupBox3.Controls.Add(this.dialogPathLoader2);
		this.groupBox3.Controls.Add(this.dialogComboBox2);
		this.groupBox3.Controls.Add(this.dialogCheckBox3);
		this.groupBox3.Controls.Add(this.dialogNumericUpDownFloat3);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.groupBox3.Location = new System.Drawing.Point(3, 3);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(521, 205);
		this.groupBox3.TabIndex = 5;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "groupBox3";
		this.label1.AutoSize = true;
		this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.label1.Location = new System.Drawing.Point(12, 181);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(35, 13);
		this.label1.TabIndex = 9;
		this.label1.Text = "label1";
		this.dialogTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.dialogTextBox2.Location = new System.Drawing.Point(15, 143);
		this.dialogTextBox2.Name = "dialogTextBox2";
		this.dialogTextBox2.Size = new System.Drawing.Size(485, 20);
		this.dialogTextBox2.TabIndex = 8;
		this.groupBox4.Controls.Add(this.dialogRadioButton3);
		this.groupBox4.Controls.Add(this.dialogRadioButton4);
		this.groupBox4.Location = new System.Drawing.Point(15, 73);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(485, 45);
		this.groupBox4.TabIndex = 7;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "groupBox4";
		this.dialogRadioButton3.AutoSize = true;
		this.dialogRadioButton3.Location = new System.Drawing.Point(307, 19);
		this.dialogRadioButton3.Name = "dialogRadioButton3";
		this.dialogRadioButton3.Size = new System.Drawing.Size(118, 17);
		this.dialogRadioButton3.TabIndex = 6;
		this.dialogRadioButton3.TabStop = true;
		this.dialogRadioButton3.Text = "dialogRadioButton1";
		this.dialogRadioButton3.UseVisualStyleBackColor = true;
		this.dialogRadioButton4.AutoSize = true;
		this.dialogRadioButton4.Location = new System.Drawing.Point(6, 19);
		this.dialogRadioButton4.Name = "dialogRadioButton4";
		this.dialogRadioButton4.Size = new System.Drawing.Size(118, 17);
		this.dialogRadioButton4.TabIndex = 6;
		this.dialogRadioButton4.TabStop = true;
		this.dialogRadioButton4.Text = "dialogRadioButton4";
		this.dialogRadioButton4.UseVisualStyleBackColor = true;
		this.dialogNumericUpDownFloat2.Location = new System.Drawing.Point(254, 15);
		this.dialogNumericUpDownFloat2.Name = "dialogNumericUpDownFloat2";
		this.dialogNumericUpDownFloat2.Size = new System.Drawing.Size(120, 20);
		this.dialogNumericUpDownFloat2.TabIndex = 5;
		this.dialogPathLoader2.BackColor = System.Drawing.Color.Transparent;
		this.dialogPathLoader2.BrowseType = palDotNet.palDialog.DialogPathLoader.EnumBrowseType.eSave;
		this.dialogPathLoader2.CheckFileExists = false;
		this.dialogPathLoader2.Description = null;
		this.dialogPathLoader2.Filter = null;
		this.dialogPathLoader2.Location = new System.Drawing.Point(15, 42);
		this.dialogPathLoader2.Name = "dialogPathLoader2";
		this.dialogPathLoader2.Path = "";
		this.dialogPathLoader2.Size = new System.Drawing.Size(485, 24);
		this.dialogPathLoader2.TabIndex = 4;
		this.dialogComboBox2.FormattingEnabled = true;
		this.dialogComboBox2.Items.AddRange(new object[3] { "Item1", "Item2", "Item3" });
		this.dialogComboBox2.Location = new System.Drawing.Point(127, 15);
		this.dialogComboBox2.Name = "dialogComboBox2";
		this.dialogComboBox2.Size = new System.Drawing.Size(121, 21);
		this.dialogComboBox2.TabIndex = 3;
		this.dialogCheckBox3.AutoSize = true;
		this.dialogCheckBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.dialogCheckBox3.Location = new System.Drawing.Point(15, 19);
		this.dialogCheckBox3.Name = "dialogCheckBox3";
		this.dialogCheckBox3.Size = new System.Drawing.Size(106, 17);
		this.dialogCheckBox3.TabIndex = 2;
		this.dialogCheckBox3.Text = "dialogCheckBox3";
		this.dialogCheckBox3.UseVisualStyleBackColor = true;
		this.dialogNumericUpDownFloat3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.dialogNumericUpDownFloat3.Location = new System.Drawing.Point(380, 15);
		this.dialogNumericUpDownFloat3.Name = "dialogNumericUpDownFloat3";
		this.dialogNumericUpDownFloat3.Size = new System.Drawing.Size(120, 20);
		this.dialogNumericUpDownFloat3.TabIndex = 1;
		this.dialogNumericUpDownFloat3.Value = new decimal(new int[4] { 33, 0, 0, 0 });
		this.tabPage3.Controls.Add(this.dialogCheckGroupBox1);
		this.tabPage3.Location = new System.Drawing.Point(4, 22);
		this.tabPage3.Name = "tabPage3";
		this.tabPage3.Size = new System.Drawing.Size(527, 211);
		this.tabPage3.TabIndex = 2;
		this.tabPage3.Text = "tabPage3";
		this.tabPage3.UseVisualStyleBackColor = true;
		this.dialogCheckGroupBox1.Controls.Add(this.dialogLabel1);
		this.dialogCheckGroupBox1.Controls.Add(this.dialogListBox2);
		this.dialogCheckGroupBox1.Controls.Add(this.dialogCheckBox5);
		this.dialogCheckGroupBox1.Controls.Add(this.dialogComboBox4);
		this.dialogCheckGroupBox1.Controls.Add(this.dialogNumericUpDown2);
		this.dialogCheckGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dialogCheckGroupBox1.ForeColor = System.Drawing.Color.Transparent;
		this.dialogCheckGroupBox1.Location = new System.Drawing.Point(0, 0);
		this.dialogCheckGroupBox1.Name = "dialogCheckGroupBox1";
		this.dialogCheckGroupBox1.Size = new System.Drawing.Size(527, 211);
		this.dialogCheckGroupBox1.TabIndex = 0;
		this.dialogCheckGroupBox1.TabStop = false;
		this.dialogCheckGroupBox1.Text = "This is the GroupCheckBox Text";
		this.dialogLabel1.AutoSize = true;
		this.dialogLabel1.Location = new System.Drawing.Point(39, 107);
		this.dialogLabel1.Name = "dialogLabel1";
		this.dialogLabel1.Size = new System.Drawing.Size(67, 13);
		this.dialogLabel1.TabIndex = 5;
		this.dialogLabel1.Text = "dialogLabel1";
		this.dialogListBox2.FormattingEnabled = true;
		this.dialogListBox2.Location = new System.Drawing.Point(196, 78);
		this.dialogListBox2.Name = "dialogListBox2";
		this.dialogListBox2.Size = new System.Drawing.Size(120, 43);
		this.dialogListBox2.TabIndex = 4;
		this.dialogCheckBox5.AutoSize = true;
		this.dialogCheckBox5.ForeColor = System.Drawing.SystemColors.ControlText;
		this.dialogCheckBox5.Location = new System.Drawing.Point(39, 78);
		this.dialogCheckBox5.Name = "dialogCheckBox5";
		this.dialogCheckBox5.Size = new System.Drawing.Size(109, 17);
		this.dialogCheckBox5.TabIndex = 2;
		this.dialogCheckBox5.Text = "dialogCheckBox5";
		this.dialogCheckBox5.UseVisualStyleBackColor = true;
		this.dialogComboBox4.FormattingEnabled = true;
		this.dialogComboBox4.Location = new System.Drawing.Point(196, 34);
		this.dialogComboBox4.Name = "dialogComboBox4";
		this.dialogComboBox4.Size = new System.Drawing.Size(121, 21);
		this.dialogComboBox4.TabIndex = 3;
		this.dialogNumericUpDown2.Location = new System.Drawing.Point(39, 35);
		this.dialogNumericUpDown2.Name = "dialogNumericUpDown2";
		this.dialogNumericUpDown2.Size = new System.Drawing.Size(120, 20);
		this.dialogNumericUpDown2.TabIndex = 1;
		this.StyleGridChooserTab.Controls.Add(this.StyleGridChooser);
		this.StyleGridChooserTab.Location = new System.Drawing.Point(4, 22);
		this.StyleGridChooserTab.Name = "StyleGridChooserTab";
		this.StyleGridChooserTab.Padding = new System.Windows.Forms.Padding(3);
		this.StyleGridChooserTab.Size = new System.Drawing.Size(527, 211);
		this.StyleGridChooserTab.TabIndex = 3;
		this.StyleGridChooserTab.Text = "StyleGridChooser";
		this.StyleGridChooserTab.UseVisualStyleBackColor = true;
		this.StyleGridChooser.Dock = System.Windows.Forms.DockStyle.Fill;
		this.StyleGridChooser.Location = new System.Drawing.Point(3, 3);
		this.StyleGridChooser.MinimumSize = new System.Drawing.Size(0, 140);
		this.StyleGridChooser.Name = "StyleGridChooser";
		this.StyleGridChooser.Size = new System.Drawing.Size(521, 205);
		this.StyleGridChooser.TabIndex = 0;
		this.AnchorPanelTab.Controls.Add(this.panel2);
		this.AnchorPanelTab.Controls.Add(this.panel1);
		this.AnchorPanelTab.Controls.Add(this.AnchorPanel);
		this.AnchorPanelTab.Location = new System.Drawing.Point(4, 22);
		this.AnchorPanelTab.Name = "AnchorPanelTab";
		this.AnchorPanelTab.Size = new System.Drawing.Size(527, 211);
		this.AnchorPanelTab.TabIndex = 4;
		this.AnchorPanelTab.Text = "AnchorPanel";
		this.AnchorPanelTab.UseVisualStyleBackColor = true;
		this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel2.Controls.Add(this.AnchorShrinkY);
		this.panel2.Controls.Add(this.AnchorGrowY);
		this.panel2.Controls.Add(this.AnchorYLabel);
		this.panel2.Location = new System.Drawing.Point(24, 165);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(252, 29);
		this.panel2.TabIndex = 4;
		this.AnchorShrinkY.AutoSize = true;
		this.AnchorShrinkY.Location = new System.Drawing.Point(158, 5);
		this.AnchorShrinkY.Name = "AnchorShrinkY";
		this.AnchorShrinkY.Size = new System.Drawing.Size(55, 17);
		this.AnchorShrinkY.TabIndex = 4;
		this.AnchorShrinkY.TabStop = true;
		this.AnchorShrinkY.Text = "Shrink";
		this.AnchorShrinkY.UseVisualStyleBackColor = true;
		this.AnchorShrinkY.CheckedChanged += new System.EventHandler(AnchorShrinkY_CheckedChanged);
		this.AnchorGrowY.AutoSize = true;
		this.AnchorGrowY.Location = new System.Drawing.Point(89, 5);
		this.AnchorGrowY.Name = "AnchorGrowY";
		this.AnchorGrowY.Size = new System.Drawing.Size(50, 17);
		this.AnchorGrowY.TabIndex = 3;
		this.AnchorGrowY.TabStop = true;
		this.AnchorGrowY.Text = "Grow";
		this.AnchorGrowY.UseVisualStyleBackColor = true;
		this.AnchorGrowY.CheckedChanged += new System.EventHandler(AnchorGrowY_CheckedChanged);
		this.AnchorYLabel.AutoSize = true;
		this.AnchorYLabel.Location = new System.Drawing.Point(3, 7);
		this.AnchorYLabel.Name = "AnchorYLabel";
		this.AnchorYLabel.Size = new System.Drawing.Size(54, 13);
		this.AnchorYLabel.TabIndex = 2;
		this.AnchorYLabel.Text = "Anchor Y:";
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.AnchorShrinkX);
		this.panel1.Controls.Add(this.AnchorGrowX);
		this.panel1.Controls.Add(this.AnchorXLabel);
		this.panel1.Location = new System.Drawing.Point(24, 130);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(252, 29);
		this.panel1.TabIndex = 3;
		this.AnchorShrinkX.AutoSize = true;
		this.AnchorShrinkX.Location = new System.Drawing.Point(158, 5);
		this.AnchorShrinkX.Name = "AnchorShrinkX";
		this.AnchorShrinkX.Size = new System.Drawing.Size(55, 17);
		this.AnchorShrinkX.TabIndex = 3;
		this.AnchorShrinkX.TabStop = true;
		this.AnchorShrinkX.Text = "Shrink";
		this.AnchorShrinkX.UseVisualStyleBackColor = true;
		this.AnchorShrinkX.CheckedChanged += new System.EventHandler(AnchorShrinkX_CheckedChanged);
		this.AnchorGrowX.AutoSize = true;
		this.AnchorGrowX.Location = new System.Drawing.Point(89, 5);
		this.AnchorGrowX.Name = "AnchorGrowX";
		this.AnchorGrowX.Size = new System.Drawing.Size(50, 17);
		this.AnchorGrowX.TabIndex = 2;
		this.AnchorGrowX.TabStop = true;
		this.AnchorGrowX.Text = "Grow";
		this.AnchorGrowX.UseVisualStyleBackColor = true;
		this.AnchorGrowX.CheckedChanged += new System.EventHandler(AnchorGrowX_CheckedChanged);
		this.AnchorXLabel.AutoSize = true;
		this.AnchorXLabel.Location = new System.Drawing.Point(3, 7);
		this.AnchorXLabel.Name = "AnchorXLabel";
		this.AnchorXLabel.Size = new System.Drawing.Size(54, 13);
		this.AnchorXLabel.TabIndex = 1;
		this.AnchorXLabel.Text = "Anchor X:";
		this.AnchorPanel.BackColor = System.Drawing.SystemColors.Control;
		this.AnchorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.AnchorPanel.Location = new System.Drawing.Point(24, 22);
		this.AnchorPanel.Name = "AnchorPanel";
		this.AnchorPanel.Size = new System.Drawing.Size(252, 102);
		this.AnchorPanel.TabIndex = 0;
		this.groupBox5.Controls.Add(this.dialogListBox1);
		this.groupBox5.Controls.Add(this.label3);
		this.groupBox5.Controls.Add(this.dialogTextBox3);
		this.groupBox5.Controls.Add(this.groupBox6);
		this.groupBox5.Controls.Add(this.dialogNumericUpDownFloat4);
		this.groupBox5.Controls.Add(this.dialogPathLoader3);
		this.groupBox5.Controls.Add(this.dialogComboBox3);
		this.groupBox5.Controls.Add(this.dialogCheckBox4);
		this.groupBox5.Controls.Add(this.dialogNumericUpDownFloat5);
		this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.groupBox5.Location = new System.Drawing.Point(19, 255);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(521, 194);
		this.groupBox5.TabIndex = 7;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "groupBox5";
		this.dialogListBox1.FormattingEnabled = true;
		this.dialogListBox1.Items.AddRange(new object[6] { "ListBoxItem 1", "ListBoxItem 2", "ListBoxItem 3", "ListBoxItem 4", "ListBoxItem 5", "ListBoxItem 6" });
		this.dialogListBox1.Location = new System.Drawing.Point(225, 124);
		this.dialogListBox1.Name = "dialogListBox1";
		this.dialogListBox1.Size = new System.Drawing.Size(275, 56);
		this.dialogListBox1.TabIndex = 11;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(12, 173);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(35, 13);
		this.label3.TabIndex = 10;
		this.label3.Text = "label3";
		this.dialogTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.dialogTextBox3.Location = new System.Drawing.Point(15, 143);
		this.dialogTextBox3.Name = "dialogTextBox3";
		this.dialogTextBox3.Size = new System.Drawing.Size(150, 20);
		this.dialogTextBox3.TabIndex = 8;
		this.groupBox6.Controls.Add(this.dialogRadioButton5);
		this.groupBox6.Controls.Add(this.dialogRadioButton6);
		this.groupBox6.Location = new System.Drawing.Point(15, 73);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(485, 45);
		this.groupBox6.TabIndex = 7;
		this.groupBox6.TabStop = false;
		this.groupBox6.Text = "groupBox6";
		this.dialogRadioButton5.AutoSize = true;
		this.dialogRadioButton5.Location = new System.Drawing.Point(307, 19);
		this.dialogRadioButton5.Name = "dialogRadioButton5";
		this.dialogRadioButton5.Size = new System.Drawing.Size(118, 17);
		this.dialogRadioButton5.TabIndex = 6;
		this.dialogRadioButton5.TabStop = true;
		this.dialogRadioButton5.Text = "dialogRadioButton1";
		this.dialogRadioButton5.UseVisualStyleBackColor = true;
		this.dialogRadioButton6.AutoSize = true;
		this.dialogRadioButton6.Location = new System.Drawing.Point(6, 19);
		this.dialogRadioButton6.Name = "dialogRadioButton6";
		this.dialogRadioButton6.Size = new System.Drawing.Size(118, 17);
		this.dialogRadioButton6.TabIndex = 6;
		this.dialogRadioButton6.TabStop = true;
		this.dialogRadioButton6.Text = "dialogRadioButton6";
		this.dialogRadioButton6.UseVisualStyleBackColor = true;
		this.dialogNumericUpDownFloat4.Location = new System.Drawing.Point(254, 15);
		this.dialogNumericUpDownFloat4.Name = "dialogNumericUpDownFloat4";
		this.dialogNumericUpDownFloat4.Size = new System.Drawing.Size(120, 20);
		this.dialogNumericUpDownFloat4.TabIndex = 5;
		this.dialogPathLoader3.BackColor = System.Drawing.Color.Transparent;
		this.dialogPathLoader3.BrowseType = palDotNet.palDialog.DialogPathLoader.EnumBrowseType.eSave;
		this.dialogPathLoader3.CheckFileExists = false;
		this.dialogPathLoader3.Description = null;
		this.dialogPathLoader3.Filter = null;
		this.dialogPathLoader3.Location = new System.Drawing.Point(15, 42);
		this.dialogPathLoader3.Name = "dialogPathLoader3";
		this.dialogPathLoader3.Path = "";
		this.dialogPathLoader3.Size = new System.Drawing.Size(485, 24);
		this.dialogPathLoader3.TabIndex = 4;
		this.dialogComboBox3.FormattingEnabled = true;
		this.dialogComboBox3.Items.AddRange(new object[3] { "Item1", "Item2", "Item3" });
		this.dialogComboBox3.Location = new System.Drawing.Point(127, 15);
		this.dialogComboBox3.Name = "dialogComboBox3";
		this.dialogComboBox3.Size = new System.Drawing.Size(121, 21);
		this.dialogComboBox3.TabIndex = 3;
		this.dialogCheckBox4.AutoSize = true;
		this.dialogCheckBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.dialogCheckBox4.Location = new System.Drawing.Point(15, 19);
		this.dialogCheckBox4.Name = "dialogCheckBox4";
		this.dialogCheckBox4.Size = new System.Drawing.Size(106, 17);
		this.dialogCheckBox4.TabIndex = 2;
		this.dialogCheckBox4.Text = "dialogCheckBox4";
		this.dialogCheckBox4.UseVisualStyleBackColor = true;
		this.dialogNumericUpDownFloat5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.dialogNumericUpDownFloat5.Location = new System.Drawing.Point(380, 15);
		this.dialogNumericUpDownFloat5.Name = "dialogNumericUpDownFloat5";
		this.dialogNumericUpDownFloat5.Size = new System.Drawing.Size(120, 20);
		this.dialogNumericUpDownFloat5.TabIndex = 1;
		this.dialogNumericUpDownFloat5.Value = new decimal(new int[4] { 33, 0, 0, 0 });
		base.AcceptButton = this.buttonOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.CancelButton = this.buttonCancel;
		base.ClientSize = new System.Drawing.Size(559, 488);
		base.Controls.Add(this.groupBox5);
		base.Controls.Add(this.tabControl1);
		base.Controls.Add(this.dialogCheckBox2);
		base.Controls.Add(this.buttonOK);
		base.Controls.Add(this.buttonCancel);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.Name = "Form1";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		((System.ComponentModel.ISupportInitialize)this.dialogNumericUpDown1).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dialogNumericUpDownFloat1).EndInit();
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage2.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dialogNumericUpDownFloat2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dialogNumericUpDownFloat3).EndInit();
		this.tabPage3.ResumeLayout(false);
		this.dialogCheckGroupBox1.ResumeLayout(false);
		this.dialogCheckGroupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dialogNumericUpDown2).EndInit();
		this.StyleGridChooserTab.ResumeLayout(false);
		this.AnchorPanelTab.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		this.groupBox6.ResumeLayout(false);
		this.groupBox6.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dialogNumericUpDownFloat4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dialogNumericUpDownFloat5).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public Form1()
	{
		InitializeComponent();
		InitAnchorPanelTab();
	}

	private void dialogCheckBox2_Click(object sender, EventArgs e)
	{
		DialogCheckBox dialogCheckBox = sender as DialogCheckBox;
		foreach (Control control in base.Controls)
		{
			if (control != dialogCheckBox)
			{
				control.Enabled = !dialogCheckBox.Checked;
			}
		}
	}

	private void InitAnchorPanelTab()
	{
		AnchorPanel.SetDefaultConfig();
		if (AnchorPanel.AnchorModeX == DialogAnchorPanel.AnchorModes.eGrow)
		{
			AnchorGrowX.Checked = true;
		}
		else
		{
			AnchorShrinkX.Checked = true;
		}
		if (AnchorPanel.AnchorModeY == DialogAnchorPanel.AnchorModes.eGrow)
		{
			AnchorGrowY.Checked = true;
		}
		else
		{
			AnchorShrinkY.Checked = true;
		}
	}

	private void AnchorGrowX_CheckedChanged(object sender, EventArgs e)
	{
		AnchorPanel.AnchorModeX = DialogAnchorPanel.AnchorModes.eGrow;
	}

	private void AnchorShrinkX_CheckedChanged(object sender, EventArgs e)
	{
		AnchorPanel.AnchorModeX = DialogAnchorPanel.AnchorModes.eShrink;
	}

	private void AnchorGrowY_CheckedChanged(object sender, EventArgs e)
	{
		AnchorPanel.AnchorModeY = DialogAnchorPanel.AnchorModes.eGrow;
	}

	private void AnchorShrinkY_CheckedChanged(object sender, EventArgs e)
	{
		AnchorPanel.AnchorModeY = DialogAnchorPanel.AnchorModes.eShrink;
	}
}
