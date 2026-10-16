using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace palDotNet.palDialog;

public class DialogPathLoader : UserControl, IDialogValue
{
	public enum EnumBrowseType
	{
		eSave,
		eOpen,
		eDirectories
	}

	private IContainer components;

	private TextBox mTextBox;

	private Button mBrowseButton;

	private bool mAlreadyFocused;

	[DialogValueType(EnumDialogValueTypes.eString)]
	[ReadOnly(true)]
	public object DialogValue
	{
		get
		{
			return mTextBox.Text;
		}
		set
		{
			mTextBox.Text = (string)value;
			PathChangedHandler();
		}
	}

	public TextBox TextBox => mTextBox;

	[ReadOnly(true)]
	public bool LoadedFromSettings { get; set; }

	public bool SaveToConfig { get; set; }

	public string Description { get; set; }

	public EnumBrowseType BrowseType { get; set; }

	public string Filter { get; set; }

	public bool CheckFileExists { get; set; }

	public string Path
	{
		get
		{
			return mTextBox.Text;
		}
		set
		{
			mTextBox.Text = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(true)]
	public event EventHandler PathChanged;

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
		this.mTextBox = new System.Windows.Forms.TextBox();
		this.mBrowseButton = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.mTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.mTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.mTextBox.HideSelection = false;
		this.mTextBox.Location = new System.Drawing.Point(0, 2);
		this.mTextBox.Name = "mTextBox";
		this.mTextBox.Size = new System.Drawing.Size(290, 20);
		this.mTextBox.TabIndex = 0;
		this.mTextBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(mTextBox_PreviewKeyDown);
		this.mTextBox.Leave += new System.EventHandler(mTextBox_Leave);
		this.mTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(mTextBox_KeyPress);
		this.mTextBox.Enter += new System.EventHandler(mTextBox_Enter);
		this.mTextBox.MouseUp += new System.Windows.Forms.MouseEventHandler(mTextBox_MouseUp);
		this.mBrowseButton.Dock = System.Windows.Forms.DockStyle.Right;
		this.mBrowseButton.Location = new System.Drawing.Point(295, 0);
		this.mBrowseButton.Name = "mBrowseButton";
		this.mBrowseButton.Size = new System.Drawing.Size(35, 24);
		this.mBrowseButton.TabIndex = 1;
		this.mBrowseButton.Text = "...";
		this.mBrowseButton.UseVisualStyleBackColor = true;
		this.mBrowseButton.Click += new System.EventHandler(mBrowseButton_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Transparent;
		base.Controls.Add(this.mBrowseButton);
		base.Controls.Add(this.mTextBox);
		base.Name = "DialogPathLoader";
		base.Size = new System.Drawing.Size(330, 24);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public DialogPathLoader()
	{
		SaveToConfig = true;
		BrowseType = EnumBrowseType.eSave;
		InitializeComponent();
	}

	private void mBrowseButton_Click(object sender, EventArgs e)
	{
		ShowDialog();
	}

	public DialogResult ShowDialog()
	{
		FileDialog fileDialog = null;
		if (BrowseType == EnumBrowseType.eSave)
		{
			fileDialog = new SaveFileDialog();
		}
		else if (BrowseType == EnumBrowseType.eOpen)
		{
			fileDialog = new OpenFileDialog();
		}
		DialogResult dialogResult;
		if (fileDialog != null)
		{
			fileDialog.CheckFileExists = CheckFileExists;
			try
			{
				if (!string.IsNullOrEmpty(mTextBox.Text))
				{
					if (Directory.Exists(mTextBox.Text))
					{
						fileDialog.InitialDirectory = mTextBox.Text;
					}
					else
					{
						string directoryName = System.IO.Path.GetDirectoryName(mTextBox.Text);
						if (Directory.Exists(directoryName))
						{
							fileDialog.InitialDirectory = directoryName;
						}
						fileDialog.FileName = mTextBox.Text;
					}
				}
				if (!string.IsNullOrEmpty(Description))
				{
					fileDialog.Title = Description;
				}
				if (!string.IsNullOrEmpty(Filter))
				{
					fileDialog.Filter = Filter;
				}
				else
				{
					fileDialog.Filter = "All files (*.*)|*.*";
				}
			}
			catch
			{
			}
			try
			{
				dialogResult = fileDialog.ShowDialog(this);
			}
			catch (InvalidOperationException)
			{
				fileDialog.FileName = string.Empty;
				dialogResult = fileDialog.ShowDialog(this);
			}
			if (dialogResult == DialogResult.OK)
			{
				mTextBox.Text = fileDialog.FileName;
				PathChangedHandler();
			}
		}
		else
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.Description = Description;
			try
			{
				if (!string.IsNullOrEmpty(mTextBox.Text) && Directory.Exists(mTextBox.Text))
				{
					folderBrowserDialog.SelectedPath = mTextBox.Text;
				}
			}
			catch
			{
			}
			dialogResult = folderBrowserDialog.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				mTextBox.Text = folderBrowserDialog.SelectedPath;
				PathChangedHandler();
			}
		}
		return dialogResult;
	}

	public void AddToolTip(ToolTip toolTip, string dialogName, string varName)
	{
		string caption = "script command: " + Environment.NewLine + "'" + dialogName + " -" + varName + " <path>'";
		toolTip.SetToolTip(mTextBox, caption);
	}

	private void mTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			e.IsInputKey = true;
		}
	}

	private void mTextBox_Enter(object sender, EventArgs e)
	{
		if (Control.MouseButtons == MouseButtons.None)
		{
			mTextBox.SelectAll();
			mAlreadyFocused = true;
		}
	}

	private void mTextBox_Leave(object sender, EventArgs e)
	{
		PathChangedHandler();
		mTextBox.SelectionLength = 0;
		mAlreadyFocused = false;
	}

	private void mTextBox_MouseUp(object sender, MouseEventArgs e)
	{
		if (!mAlreadyFocused && mTextBox.SelectionLength == 0)
		{
			mAlreadyFocused = true;
			mTextBox.SelectAll();
		}
	}

	private void mTextBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			PathChangedHandler();
			e.Handled = true;
		}
	}

	private void PathChangedHandler()
	{
		if (PathChanged != null)
		{
			PathChanged(this, EventArgs.Empty);
		}
	}
}
