using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace ktKitDotNet;

[DialogBoxCreator]
public class MetaDataTemplateEditor : DialogBoxBase
{
	private delegate void Sort();

	public delegate void ElementMovedHandler(int prevIndex, int currIndex);

	public delegate void LoadElementHandler(int index);

	public delegate void TypeChangedHandler(KitCommon.DataType dataType);

	private BooleanType BoolControl;

	private ColorType ColorControl;

	private StringType StringControl;

	private IntegerType IntControl;

	private FloatType FloatControl;

	private Vector2Type Vector2Control;

	private Vector3Type Vector3Control;

	private Vector4Type Vector4Control;

	private EnumerationType EnumControl;

	private bool InitializingType;

	private IContainer components;

	private Button buttonOK;

	private GroupBox ElementGroup;

	private ListView ElementList;

	private Panel ElementPanel;

	private ComboBox ElementType;

	private Label label2;

	private Button DeleteElement;

	private Button NewElement;

	private Button buttonCancel;

	private GroupBox TemplateGroup;

	private ListView TemplateList;

	private Button DeleteTemplate;

	private Button NewTemplate;

	private SplitContainer splitContainer1;

	private ColumnHeader TemplatesColumn;

	private ColumnHeader ElementsColumn;

	public Button NewTemplateButton => NewTemplate;

	public Button DeleteTemplateButton => DeleteTemplate;

	public ListView TemplateListBox => TemplateList;

	public GroupBox ElementGroupBox => ElementGroup;

	public Button NewElementButton => NewElement;

	public Button DeleteElementButton => DeleteElement;

	public ListView ElementListBox => ElementList;

	public ComboBox ElementTypeCombo => ElementType;

	public BooleanType BoolInfo => BoolControl;

	public ColorType ColorInfo => ColorControl;

	public StringType StringInfo => StringControl;

	public IntegerType IntInfo => IntControl;

	public FloatType FloatInfo => FloatControl;

	public Vector2Type Vector2Info => Vector2Control;

	public Vector3Type Vector3Info => Vector3Control;

	public Vector4Type Vector4Info => Vector4Control;

	public EnumerationType EnumInfo => EnumControl;

	public event ElementMovedHandler ElementMoved;

	public event LoadElementHandler LoadElement;

	public event TypeChangedHandler TypeChanged;

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	internal static extern IntPtr GetFocus();

	protected Control GetFocusControl()
	{
		Control result = null;
		IntPtr focus = GetFocus();
		if (focus != IntPtr.Zero)
		{
			result = Control.FromHandle(focus);
		}
		return result;
	}

	public MetaDataTemplateEditor()
	{
		InitializeComponent();
		BoolControl = new BooleanType();
		ColorControl = new ColorType();
		StringControl = new StringType();
		IntControl = new IntegerType();
		FloatControl = new FloatType();
		Vector2Control = new Vector2Type();
		Vector3Control = new Vector3Type();
		Vector4Control = new Vector4Type();
		EnumControl = new EnumerationType();
		EnumControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		InitializingType = false;
		TemplatesColumn.Width = TemplateList.ClientSize.Width;
		ElementsColumn.Width = ElementList.ClientSize.Width;
	}

	private void TemplateList_AfterLabelEdit(object sender, LabelEditEventArgs e)
	{
		TemplateList.BeginInvoke(new Sort(TemplateList.Sort));
	}

	private void TemplateList_SelectedIndexChanged(object sender, EventArgs e)
	{
		int count = TemplateList.SelectedIndices.Count;
		if (count <= 0)
		{
			DeleteTemplate.Enabled = false;
		}
		else
		{
			DeleteTemplate.Enabled = true;
		}
	}

	private void TemplateList_ClientSizeChanged(object sender, EventArgs e)
	{
		TemplatesColumn.Width = TemplateList.ClientSize.Width;
	}

	private void ElementList_ClientSizeChanged(object sender, EventArgs e)
	{
		ElementsColumn.Width = ElementList.ClientSize.Width;
	}

	private void TemplateList_SizeChanged(object sender, EventArgs e)
	{
		TemplatesColumn.Width = 0;
	}

	private void ElementList_SizeChanged(object sender, EventArgs e)
	{
		ElementsColumn.Width = 0;
	}

	private void buttonOK_Click(object sender, EventArgs e)
	{
		Control focusControl = GetFocusControl();
		if (focusControl is TextBox)
		{
			TextBox textBox = (TextBox)focusControl;
			if (textBox.SelectionLength != textBox.Text.Length)
			{
				textBox.SelectAll();
				return;
			}
		}
		base.DialogResult = DialogResult.OK;
		Close();
	}

	private void ElementType_SelectedIndexChanged(object sender, EventArgs e)
	{
		ElementPanel.Controls.Clear();
		KitCommon.DataType dataType = KitCommon.DataType.eBoolean;
		switch ((string)ElementType.SelectedItem)
		{
		default:
			return;
		case "Boolean":
			ElementPanel.Controls.Add(BoolControl);
			dataType = KitCommon.DataType.eBoolean;
			break;
		case "Color":
			ElementPanel.Controls.Add(ColorControl);
			dataType = KitCommon.DataType.eColor;
			break;
		case "String":
			ElementPanel.Controls.Add(StringControl);
			dataType = KitCommon.DataType.eString;
			break;
		case "Integer":
			ElementPanel.Controls.Add(IntControl);
			dataType = KitCommon.DataType.eInteger;
			break;
		case "Float":
			ElementPanel.Controls.Add(FloatControl);
			dataType = KitCommon.DataType.eFloat;
			break;
		case "2D Vector":
			ElementPanel.Controls.Add(Vector2Control);
			dataType = KitCommon.DataType.eVector2;
			break;
		case "3D Vector":
			ElementPanel.Controls.Add(Vector3Control);
			dataType = KitCommon.DataType.eVector3;
			break;
		case "4D Vector":
			ElementPanel.Controls.Add(Vector4Control);
			dataType = KitCommon.DataType.eVector4;
			break;
		case "Enumeration":
			ElementPanel.Controls.Add(EnumControl);
			dataType = KitCommon.DataType.eEnumeration;
			EnumControl.Size = ElementPanel.Size;
			break;
		}
		if (!InitializingType && TypeChanged != null)
		{
			TypeChanged(dataType);
		}
	}

	public void SetType(KitCommon.DataType dataType)
	{
		InitializingType = true;
		switch (dataType)
		{
		case KitCommon.DataType.eInteger:
			ElementType.SelectedItem = "Integer";
			break;
		case KitCommon.DataType.eFloat:
			ElementType.SelectedItem = "Float";
			break;
		case KitCommon.DataType.eBoolean:
			ElementType.SelectedItem = "Boolean";
			break;
		case KitCommon.DataType.eEnumeration:
			ElementType.SelectedItem = "Enumeration";
			break;
		case KitCommon.DataType.eColor:
			ElementType.SelectedItem = "Color";
			break;
		case KitCommon.DataType.eString:
			ElementType.SelectedItem = "String";
			break;
		case KitCommon.DataType.eVector2:
			ElementType.SelectedItem = "2D Vector";
			break;
		case KitCommon.DataType.eVector3:
			ElementType.SelectedItem = "3D Vector";
			break;
		case KitCommon.DataType.eVector4:
			ElementType.SelectedItem = "4D Vector";
			break;
		}
		InitializingType = false;
	}

	private void ElementList_SelectedIndexChanged(object sender, EventArgs e)
	{
		int count = ElementList.SelectedIndices.Count;
		if (count <= 0)
		{
			DeleteElement.Enabled = false;
		}
		else
		{
			DeleteElement.Enabled = true;
		}
		if (count == 1)
		{
			if (LoadElement != null)
			{
				LoadElement(ElementList.SelectedIndices[0]);
			}
			ElementType.Enabled = true;
		}
		else
		{
			ElementType.SelectedIndex = -1;
			ElementType.Enabled = false;
		}
	}

	private void ElementList_ItemDrag(object sender, ItemDragEventArgs e)
	{
		ElementList.DoDragDrop(ElementList.SelectedItems, DragDropEffects.Move);
	}

	private void ElementList_DragEnter(object sender, DragEventArgs e)
	{
		int num = e.Data.GetFormats().Length;
		for (int i = 0; i < num; i++)
		{
			if (e.Data.GetFormats()[i].Equals("System.Windows.Forms.ListView+SelectedListViewItemCollection"))
			{
				e.Effect = DragDropEffects.Move;
			}
		}
	}

	private void ElementList_DragDrop(object sender, DragEventArgs e)
	{
		ListView.SelectedListViewItemCollection selectedItems = ElementList.SelectedItems;
		if (selectedItems.Count == 0)
		{
			return;
		}
		Point point = ElementList.PointToClient(new Point(e.X, e.Y));
		ListViewItem itemAt = ElementList.GetItemAt(point.X, point.Y);
		if (itemAt == null)
		{
			return;
		}
		int index = itemAt.Index;
		ListViewItem[] array = new ListViewItem[selectedItems.Count];
		for (int i = 0; i < selectedItems.Count; i++)
		{
			array[i] = selectedItems[i];
		}
		ListView.ListViewItemCollection items = ElementList.Items;
		for (int j = 0; j < array.GetLength(0); j++)
		{
			ListViewItem listViewItem = array[j];
			int num = index;
			if (num == listViewItem.Index)
			{
				break;
			}
			num = ((listViewItem.Index >= num) ? (index + j) : (num + 1));
			if (ElementMoved != null)
			{
				ElementMoved(listViewItem.Index, num);
			}
			ListViewItem item = (ListViewItem)listViewItem.Clone();
			items.Insert(num, item);
			items.Remove(listViewItem);
		}
	}

	private void MetaDataTemplateEditor_Load(object sender, EventArgs e)
	{
	}

	private void ListView_KeyDown(object sender, KeyEventArgs e)
	{
		if (sender is ListView listView && e.KeyCode == Keys.F2 && listView.SelectedItems.Count == 1)
		{
			listView.SelectedItems[0].BeginEdit();
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
		this.buttonOK = new System.Windows.Forms.Button();
		this.ElementGroup = new System.Windows.Forms.GroupBox();
		this.ElementList = new System.Windows.Forms.ListView();
		this.ElementsColumn = new System.Windows.Forms.ColumnHeader();
		this.ElementPanel = new System.Windows.Forms.Panel();
		this.ElementType = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.DeleteElement = new System.Windows.Forms.Button();
		this.NewElement = new System.Windows.Forms.Button();
		this.buttonCancel = new System.Windows.Forms.Button();
		this.TemplateGroup = new System.Windows.Forms.GroupBox();
		this.TemplateList = new System.Windows.Forms.ListView();
		this.TemplatesColumn = new System.Windows.Forms.ColumnHeader();
		this.DeleteTemplate = new System.Windows.Forms.Button();
		this.NewTemplate = new System.Windows.Forms.Button();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.ElementGroup.SuspendLayout();
		this.TemplateGroup.SuspendLayout();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		base.SuspendLayout();
		this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.buttonOK.Location = new System.Drawing.Point(607, 343);
		this.buttonOK.Name = "buttonOK";
		this.buttonOK.Size = new System.Drawing.Size(75, 23);
		this.buttonOK.TabIndex = 0;
		this.buttonOK.Text = "OK";
		this.buttonOK.UseVisualStyleBackColor = true;
		this.buttonOK.Click += new System.EventHandler(buttonOK_Click);
		this.ElementGroup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ElementGroup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.ElementGroup.Controls.Add(this.ElementList);
		this.ElementGroup.Controls.Add(this.ElementPanel);
		this.ElementGroup.Controls.Add(this.ElementType);
		this.ElementGroup.Controls.Add(this.label2);
		this.ElementGroup.Controls.Add(this.DeleteElement);
		this.ElementGroup.Controls.Add(this.NewElement);
		this.ElementGroup.Enabled = false;
		this.ElementGroup.Location = new System.Drawing.Point(3, 3);
		this.ElementGroup.Name = "ElementGroup";
		this.ElementGroup.Size = new System.Drawing.Size(510, 319);
		this.ElementGroup.TabIndex = 3;
		this.ElementGroup.TabStop = false;
		this.ElementGroup.Text = "Elements";
		this.ElementList.AllowDrop = true;
		this.ElementList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ElementList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { this.ElementsColumn });
		this.ElementList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
		this.ElementList.HideSelection = false;
		this.ElementList.LabelEdit = true;
		this.ElementList.LabelWrap = false;
		this.ElementList.Location = new System.Drawing.Point(6, 20);
		this.ElementList.Name = "ElementList";
		this.ElementList.ShowGroups = false;
		this.ElementList.Size = new System.Drawing.Size(221, 264);
		this.ElementList.TabIndex = 11;
		this.ElementList.UseCompatibleStateImageBehavior = false;
		this.ElementList.View = System.Windows.Forms.View.Details;
		this.ElementList.ClientSizeChanged += new System.EventHandler(ElementList_ClientSizeChanged);
		this.ElementList.SelectedIndexChanged += new System.EventHandler(ElementList_SelectedIndexChanged);
		this.ElementList.SizeChanged += new System.EventHandler(ElementList_SizeChanged);
		this.ElementList.DragDrop += new System.Windows.Forms.DragEventHandler(ElementList_DragDrop);
		this.ElementList.DragEnter += new System.Windows.Forms.DragEventHandler(ElementList_DragEnter);
		this.ElementList.KeyDown += new System.Windows.Forms.KeyEventHandler(ListView_KeyDown);
		this.ElementList.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(ElementList_ItemDrag);
		this.ElementsColumn.Text = "Elements";
		this.ElementPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.ElementPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.ElementPanel.Location = new System.Drawing.Point(233, 46);
		this.ElementPanel.Name = "ElementPanel";
		this.ElementPanel.Size = new System.Drawing.Size(271, 267);
		this.ElementPanel.TabIndex = 10;
		this.ElementType.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.ElementType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.ElementType.Enabled = false;
		this.ElementType.FormattingEnabled = true;
		this.ElementType.Items.AddRange(new object[9] { "Boolean", "Color", "String", "Integer", "Float", "2D Vector", "3D Vector", "4D Vector", "Enumeration" });
		this.ElementType.Location = new System.Drawing.Point(271, 19);
		this.ElementType.Name = "ElementType";
		this.ElementType.Size = new System.Drawing.Size(121, 21);
		this.ElementType.TabIndex = 9;
		this.ElementType.SelectedValueChanged += new System.EventHandler(ElementType_SelectedIndexChanged);
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(233, 22);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(31, 13);
		this.label2.TabIndex = 8;
		this.label2.Text = "Type";
		this.DeleteElement.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.DeleteElement.Enabled = false;
		this.DeleteElement.Location = new System.Drawing.Point(87, 290);
		this.DeleteElement.Name = "DeleteElement";
		this.DeleteElement.Size = new System.Drawing.Size(75, 23);
		this.DeleteElement.TabIndex = 4;
		this.DeleteElement.Text = "Delete";
		this.DeleteElement.UseVisualStyleBackColor = true;
		this.NewElement.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.NewElement.Location = new System.Drawing.Point(6, 290);
		this.NewElement.Name = "NewElement";
		this.NewElement.Size = new System.Drawing.Size(75, 23);
		this.NewElement.TabIndex = 3;
		this.NewElement.Text = "New";
		this.NewElement.UseVisualStyleBackColor = true;
		this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.buttonCancel.Location = new System.Drawing.Point(688, 343);
		this.buttonCancel.Name = "buttonCancel";
		this.buttonCancel.Size = new System.Drawing.Size(75, 23);
		this.buttonCancel.TabIndex = 4;
		this.buttonCancel.Text = "Cancel";
		this.buttonCancel.UseVisualStyleBackColor = true;
		this.TemplateGroup.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TemplateGroup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.TemplateGroup.Controls.Add(this.TemplateList);
		this.TemplateGroup.Controls.Add(this.DeleteTemplate);
		this.TemplateGroup.Controls.Add(this.NewTemplate);
		this.TemplateGroup.Location = new System.Drawing.Point(3, 3);
		this.TemplateGroup.Name = "TemplateGroup";
		this.TemplateGroup.Size = new System.Drawing.Size(228, 319);
		this.TemplateGroup.TabIndex = 5;
		this.TemplateGroup.TabStop = false;
		this.TemplateGroup.Text = "Templates";
		this.TemplateList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TemplateList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[1] { this.TemplatesColumn });
		this.TemplateList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
		this.TemplateList.HideSelection = false;
		this.TemplateList.LabelEdit = true;
		this.TemplateList.LabelWrap = false;
		this.TemplateList.Location = new System.Drawing.Point(7, 20);
		this.TemplateList.Name = "TemplateList";
		this.TemplateList.ShowGroups = false;
		this.TemplateList.Size = new System.Drawing.Size(215, 264);
		this.TemplateList.Sorting = System.Windows.Forms.SortOrder.Ascending;
		this.TemplateList.TabIndex = 3;
		this.TemplateList.UseCompatibleStateImageBehavior = false;
		this.TemplateList.View = System.Windows.Forms.View.Details;
		this.TemplateList.ClientSizeChanged += new System.EventHandler(TemplateList_ClientSizeChanged);
		this.TemplateList.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(TemplateList_AfterLabelEdit);
		this.TemplateList.SelectedIndexChanged += new System.EventHandler(TemplateList_SelectedIndexChanged);
		this.TemplateList.SizeChanged += new System.EventHandler(TemplateList_SizeChanged);
		this.TemplateList.KeyDown += new System.Windows.Forms.KeyEventHandler(ListView_KeyDown);
		this.TemplatesColumn.Text = "Templates";
		this.DeleteTemplate.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.DeleteTemplate.Enabled = false;
		this.DeleteTemplate.Location = new System.Drawing.Point(88, 290);
		this.DeleteTemplate.Name = "DeleteTemplate";
		this.DeleteTemplate.Size = new System.Drawing.Size(75, 23);
		this.DeleteTemplate.TabIndex = 2;
		this.DeleteTemplate.Text = "Delete";
		this.DeleteTemplate.UseVisualStyleBackColor = true;
		this.NewTemplate.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.NewTemplate.Location = new System.Drawing.Point(7, 290);
		this.NewTemplate.Name = "NewTemplate";
		this.NewTemplate.Size = new System.Drawing.Size(75, 23);
		this.NewTemplate.TabIndex = 1;
		this.NewTemplate.Text = "New";
		this.NewTemplate.UseVisualStyleBackColor = true;
		this.splitContainer1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.splitContainer1.Location = new System.Drawing.Point(12, 12);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.TemplateGroup);
		this.splitContainer1.Panel2.Controls.Add(this.ElementGroup);
		this.splitContainer1.Size = new System.Drawing.Size(754, 325);
		this.splitContainer1.SplitterDistance = 234;
		this.splitContainer1.TabIndex = 6;
		base.AcceptButton = this.buttonOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.buttonCancel;
		base.ClientSize = new System.Drawing.Size(768, 378);
		base.Controls.Add(this.splitContainer1);
		base.Controls.Add(this.buttonCancel);
		base.Controls.Add(this.buttonOK);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		this.MinimumSize = new System.Drawing.Size(688, 416);
		base.Name = "MetaDataTemplateEditor";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Meta Data Template Editor";
		base.Load += new System.EventHandler(MetaDataTemplateEditor_Load);
		this.ElementGroup.ResumeLayout(false);
		this.ElementGroup.PerformLayout();
		this.TemplateGroup.ResumeLayout(false);
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
