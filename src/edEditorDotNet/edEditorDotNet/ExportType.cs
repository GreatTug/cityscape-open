using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.palDialog;

namespace edEditorDotNet;

public class ExportType : UserControl, IDialogContainer
{
	private IContainer components;

	private DialogLabel ExporterTypeLabel;

	private DialogComboBox ExporterName;

	private DialogComboBox ExportFilters;

	private DialogComboBox ExportSupportedOptions;

	public ExportType()
	{
		InitializeComponent();
	}

	public DialogComboBox GetExporterTypeComboBox()
	{
		return ExporterName;
	}

	public string GetExporterType()
	{
		return ExporterName.SelectedItem.ToString();
	}

	public string GetFilter()
	{
		string[] extensions = GetExtensions();
		string text = "";
		string[] array = extensions;
		foreach (string text2 in array)
		{
			string text3 = text;
			text = text3 + ExporterName.SelectedItem.ToString() + " (*." + text2 + ")|*." + text2 + "|";
		}
		return text.TrimEnd('|');
	}

	public string GetSupportedOptions()
	{
		if (ExportSupportedOptions.Items.Count > ExporterName.SelectedIndex)
		{
			ExportSupportedOptions.SelectedIndex = ExporterName.SelectedIndex;
			return ExportSupportedOptions.SelectedItem.ToString();
		}
		return "";
	}

	public string GetPrimaryExtension()
	{
		string[] extensions = GetExtensions();
		if (extensions.Length == 0)
		{
			return "";
		}
		if (extensions[0] == "*")
		{
			return "";
		}
		return extensions[0];
	}

	private string[] GetExtensions()
	{
		if (ExportFilters.Items.Count <= ExporterName.SelectedIndex)
		{
			return new string[1] { "*" };
		}
		ExportFilters.SelectedIndex = ExporterName.SelectedIndex;
		string text = ExportFilters.SelectedItem.ToString();
		if (text == "")
		{
			return new string[1] { "*" };
		}
		return text.Split(';');
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
		this.ExporterName = new palDotNet.palDialog.DialogComboBox();
		this.ExporterTypeLabel = new palDotNet.palDialog.DialogLabel();
		this.ExportFilters = new palDotNet.palDialog.DialogComboBox();
		this.ExportSupportedOptions = new palDotNet.palDialog.DialogComboBox();
		base.SuspendLayout();
		this.ExporterName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ExporterName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.ExporterName.FormattingEnabled = true;
		this.ExporterName.Location = new System.Drawing.Point(55, 0);
		this.ExporterName.Name = "ExporterName";
		this.ExporterName.SaveToConfig = true;
		this.ExporterName.Size = new System.Drawing.Size(316, 21);
		this.ExporterName.TabIndex = 1;
		this.ExporterTypeLabel.AutoSize = true;
		this.ExporterTypeLabel.Location = new System.Drawing.Point(3, 3);
		this.ExporterTypeLabel.Name = "ExporterTypeLabel";
		this.ExporterTypeLabel.SaveToConfig = true;
		this.ExporterTypeLabel.Size = new System.Drawing.Size(49, 13);
		this.ExporterTypeLabel.TabIndex = 0;
		this.ExporterTypeLabel.Text = "Exporter:";
		this.ExportFilters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.ExportFilters.FormattingEnabled = true;
		this.ExportFilters.Location = new System.Drawing.Point(250, 26);
		this.ExportFilters.Name = "ExportFilters";
		this.ExportFilters.SaveToConfig = true;
		this.ExportFilters.Size = new System.Drawing.Size(121, 21);
		this.ExportFilters.TabIndex = 2;
		this.ExportFilters.Visible = false;
		this.ExportSupportedOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.ExportSupportedOptions.FormattingEnabled = true;
		this.ExportSupportedOptions.Location = new System.Drawing.Point(123, 26);
		this.ExportSupportedOptions.Name = "ExportSupportedOptions";
		this.ExportSupportedOptions.SaveToConfig = false;
		this.ExportSupportedOptions.Size = new System.Drawing.Size(121, 21);
		this.ExportSupportedOptions.TabIndex = 3;
		this.ExportSupportedOptions.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.ExportSupportedOptions);
		base.Controls.Add(this.ExportFilters);
		base.Controls.Add(this.ExporterName);
		base.Controls.Add(this.ExporterTypeLabel);
		base.Name = "ExportType";
		base.Size = new System.Drawing.Size(374, 50);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
