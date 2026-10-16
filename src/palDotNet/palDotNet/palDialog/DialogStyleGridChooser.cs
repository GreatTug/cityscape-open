using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace palDotNet.palDialog;

public class DialogStyleGridChooser : UserControl
{
	private IContainer components;

	private Panel panel2;

	private DataGridView StyleDataGridView;

	private DataGridViewTextBoxColumn ValueColumn;

	private DataGridViewTextBoxColumn StyleNameColumn;

	private DataGridViewButtonColumn ChooseStyleColumn;

	private Button RemoveEntryButton;

	private Button AddEntryButton;

	private Button ExtractFromFileButton;

	private Button ClearAllButton;

	private int RowCounter;

	private int StyleDataGridView_CurrentRowIndex;

	public Button ExtractFromFileBtn => ExtractFromFileButton;

	public DataGridView StyleDataGrid => StyleDataGridView;

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
		this.panel2 = new System.Windows.Forms.Panel();
		this.ClearAllButton = new System.Windows.Forms.Button();
		this.ExtractFromFileButton = new System.Windows.Forms.Button();
		this.RemoveEntryButton = new System.Windows.Forms.Button();
		this.AddEntryButton = new System.Windows.Forms.Button();
		this.StyleDataGridView = new System.Windows.Forms.DataGridView();
		this.ValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.StyleNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ChooseStyleColumn = new System.Windows.Forms.DataGridViewButtonColumn();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.StyleDataGridView).BeginInit();
		base.SuspendLayout();
		this.panel2.Controls.Add(this.ClearAllButton);
		this.panel2.Controls.Add(this.ExtractFromFileButton);
		this.panel2.Controls.Add(this.RemoveEntryButton);
		this.panel2.Controls.Add(this.AddEntryButton);
		this.panel2.Controls.Add(this.StyleDataGridView);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(400, 200);
		this.panel2.TabIndex = 1;
		this.ClearAllButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.ClearAllButton.Location = new System.Drawing.Point(175, 174);
		this.ClearAllButton.Name = "ClearAllButton";
		this.ClearAllButton.Size = new System.Drawing.Size(85, 23);
		this.ClearAllButton.TabIndex = 3;
		this.ClearAllButton.Text = "Clear All";
		this.ClearAllButton.UseVisualStyleBackColor = true;
		this.ClearAllButton.Click += new System.EventHandler(ClearAllButton_Click);
		this.ExtractFromFileButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.ExtractFromFileButton.Location = new System.Drawing.Point(266, 174);
		this.ExtractFromFileButton.Name = "ExtractFromFileButton";
		this.ExtractFromFileButton.Size = new System.Drawing.Size(131, 23);
		this.ExtractFromFileButton.TabIndex = 4;
		this.ExtractFromFileButton.Text = "Extract From File";
		this.ExtractFromFileButton.UseVisualStyleBackColor = true;
		this.RemoveEntryButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.RemoveEntryButton.Location = new System.Drawing.Point(89, 174);
		this.RemoveEntryButton.Name = "RemoveEntryButton";
		this.RemoveEntryButton.Size = new System.Drawing.Size(80, 23);
		this.RemoveEntryButton.TabIndex = 2;
		this.RemoveEntryButton.Text = "Remove";
		this.RemoveEntryButton.UseVisualStyleBackColor = true;
		this.RemoveEntryButton.Click += new System.EventHandler(RemoveEntryButton_Click);
		this.AddEntryButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.AddEntryButton.Location = new System.Drawing.Point(3, 174);
		this.AddEntryButton.Name = "AddEntryButton";
		this.AddEntryButton.Size = new System.Drawing.Size(80, 23);
		this.AddEntryButton.TabIndex = 1;
		this.AddEntryButton.Text = "Add";
		this.AddEntryButton.UseVisualStyleBackColor = true;
		this.AddEntryButton.Click += new System.EventHandler(AddEntryButton_Click);
		this.StyleDataGridView.AllowUserToAddRows = false;
		this.StyleDataGridView.AllowUserToDeleteRows = false;
		this.StyleDataGridView.AllowUserToResizeRows = false;
		this.StyleDataGridView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.StyleDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.StyleDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.StyleDataGridView.Columns.AddRange(this.ValueColumn, this.StyleNameColumn, this.ChooseStyleColumn);
		this.StyleDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.StyleDataGridView.Location = new System.Drawing.Point(0, 0);
		this.StyleDataGridView.MultiSelect = false;
		this.StyleDataGridView.Name = "StyleDataGridView";
		this.StyleDataGridView.RowHeadersWidth = 24;
		this.StyleDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.StyleDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.StyleDataGridView.Size = new System.Drawing.Size(400, 168);
		this.StyleDataGridView.TabIndex = 0;
		this.StyleDataGridView.CurrentCellChanged += new System.EventHandler(StyleDataGridView_CurrentCellChanged);
		this.ValueColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.ValueColumn.FillWeight = 127.1574f;
		this.ValueColumn.HeaderText = "Value";
		this.ValueColumn.Name = "ValueColumn";
		this.ValueColumn.ReadOnly = true;
		this.StyleNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.StyleNameColumn.FillWeight = 127.1574f;
		this.StyleNameColumn.HeaderText = "Style";
		this.StyleNameColumn.Name = "StyleNameColumn";
		this.StyleNameColumn.ReadOnly = true;
		this.ChooseStyleColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.ChooseStyleColumn.FillWeight = 45.68528f;
		this.ChooseStyleColumn.HeaderText = "";
		this.ChooseStyleColumn.Name = "ChooseStyleColumn";
		this.ChooseStyleColumn.ReadOnly = true;
		this.ChooseStyleColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ChooseStyleColumn.Width = 30;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.panel2);
		this.MinimumSize = new System.Drawing.Size(0, 140);
		base.Name = "DialogStyleGridChooser";
		base.Size = new System.Drawing.Size(400, 200);
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.StyleDataGridView).EndInit();
		base.ResumeLayout(false);
	}

	public DialogStyleGridChooser()
	{
		InitializeComponent();
		StyleDataGridView.Columns[0].ReadOnly = false;
		StyleDataGridView.Columns[1].ReadOnly = true;
		StyleDataGridView.Columns[2].ReadOnly = true;
		RowCounter = 0;
		StyleDataGridView_CurrentRowIndex = -1;
		ClearStyles();
		AddDefaultStyle();
		SyncButtons();
	}

	public void AddDefaultStyle()
	{
		int index = StyleDataGridView.Rows.Add();
		DataGridViewRow dataGridViewRow = StyleDataGridView.Rows[index];
		dataGridViewRow.ReadOnly = false;
		dataGridViewRow.Cells[0].Value = "[default]";
		dataGridViewRow.Cells[0].ReadOnly = true;
		dataGridViewRow.Cells[1].Value = "";
		dataGridViewRow.Cells[1].ReadOnly = true;
		dataGridViewRow.Cells[2].Value = "...";
		dataGridViewRow.Cells[2].ReadOnly = true;
		SyncButtons();
	}

	public void AddStyle(string value, string styleName)
	{
		int index = StyleDataGridView.Rows.Add();
		DataGridViewRow dataGridViewRow = StyleDataGridView.Rows[index];
		dataGridViewRow.ReadOnly = false;
		dataGridViewRow.Cells[0].Value = value;
		dataGridViewRow.Cells[0].ReadOnly = false;
		dataGridViewRow.Cells[1].Value = styleName;
		dataGridViewRow.Cells[1].ReadOnly = true;
		dataGridViewRow.Cells[2].Value = "...";
		dataGridViewRow.Cells[2].ReadOnly = true;
		SyncButtons();
	}

	public void ClearStyles()
	{
		ArrayList arrayList = new ArrayList();
		foreach (DataGridViewRow item in (IEnumerable)StyleDataGridView.Rows)
		{
			string text = item.Cells[0].Value as string;
			if (text != "[default]")
			{
				arrayList.Add(item);
			}
		}
		foreach (DataGridViewRow item2 in arrayList)
		{
			StyleDataGridView.Rows.Remove(item2);
		}
		StyleDataGridView_CurrentRowIndex = -1;
		SyncButtons();
	}

	private void StyleDataGridView_CurrentCellChanged(object sender, EventArgs e)
	{
		DataGridViewRow currentRow = StyleDataGridView.CurrentRow;
		if (currentRow != null)
		{
			int index = currentRow.Index;
			if (StyleDataGridView_CurrentRowIndex != index)
			{
				StyleDataGridView_CurrentRowIndex = index;
			}
			SyncButtons();
		}
	}

	public void SyncButtons()
	{
		SyncRemoveButton();
		SyncClearAllButton();
	}

	private void SyncRemoveButton()
	{
		if (StyleDataGridView.RowCount > 0)
		{
			DataGridViewRow currentRow = StyleDataGridView.CurrentRow;
			if (currentRow != null)
			{
				string text = currentRow.Cells[0].Value as string;
				if (text == "[default]")
				{
					RemoveEntryButton.Enabled = false;
				}
				else
				{
					RemoveEntryButton.Enabled = true;
				}
			}
			else
			{
				RemoveEntryButton.Enabled = true;
			}
		}
		else
		{
			RemoveEntryButton.Enabled = false;
		}
	}

	private void SyncClearAllButton()
	{
		if (StyleDataGridView.RowCount > 1)
		{
			ClearAllButton.Enabled = true;
		}
		else
		{
			ClearAllButton.Enabled = false;
		}
	}

	private void AddEntryButton_Click(object sender, EventArgs e)
	{
		RowCounter++;
		string value = "Value " + RowCounter;
		AddStyle(value, "");
		int num = StyleDataGridView.RowCount - 1;
		DataGridViewRow dataGridViewRow = StyleDataGridView.Rows[num];
		dataGridViewRow.Selected = true;
		StyleDataGridView.CurrentCell = dataGridViewRow.Cells[0];
		StyleDataGridView_CurrentRowIndex = num;
		SyncButtons();
	}

	private void RemoveEntryButton_Click(object sender, EventArgs e)
	{
		if (StyleDataGridView_CurrentRowIndex != -1)
		{
			StyleDataGridView.Rows.Remove(StyleDataGridView.CurrentRow);
			if (StyleDataGridView.CurrentRow != null)
			{
				StyleDataGridView_CurrentRowIndex = StyleDataGridView.CurrentRow.Index;
			}
			else
			{
				StyleDataGridView_CurrentRowIndex = -1;
			}
			SyncButtons();
		}
	}

	private void ClearAllButton_Click(object sender, EventArgs e)
	{
		ClearStyles();
		SyncButtons();
	}
}
