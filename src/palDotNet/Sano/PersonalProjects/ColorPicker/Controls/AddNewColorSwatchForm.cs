using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sano.PersonalProjects.ColorPicker.Controls;

internal class AddNewColorSwatchForm : Form
{
	private TextBox txtColorDescription;

	private Button btnOk;

	private PictureBox picColor;

	private Button btnCancel;

	private Label lblDescription;

	internal string ColorDescription => txtColorDescription.Text;

	internal AddNewColorSwatchForm(Color colorToAdd)
	{
		InitializeComponent();
		picColor.BackColor = colorToAdd;
	}

	private void InitializeComponent()
	{
		this.picColor = new System.Windows.Forms.PictureBox();
		this.lblDescription = new System.Windows.Forms.Label();
		this.txtColorDescription = new System.Windows.Forms.TextBox();
		this.btnCancel = new System.Windows.Forms.Button();
		this.btnOk = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.picColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.picColor.Location = new System.Drawing.Point(12, 8);
		this.picColor.Name = "picColor";
		this.picColor.Size = new System.Drawing.Size(50, 50);
		this.picColor.TabIndex = 0;
		this.picColor.TabStop = false;
		this.lblDescription.Location = new System.Drawing.Point(79, 14);
		this.lblDescription.Name = "lblDescription";
		this.lblDescription.Size = new System.Drawing.Size(184, 23);
		this.lblDescription.TabIndex = 3;
		this.lblDescription.Text = "Enter color description:";
		this.txtColorDescription.Location = new System.Drawing.Point(79, 33);
		this.txtColorDescription.MaxLength = 100;
		this.txtColorDescription.Name = "txtColorDescription";
		this.txtColorDescription.Size = new System.Drawing.Size(208, 20);
		this.txtColorDescription.TabIndex = 0;
		this.txtColorDescription.Text = "";
		this.txtColorDescription.KeyUp += new System.Windows.Forms.KeyEventHandler(txtColorDescription_KeyUp);
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Location = new System.Drawing.Point(304, 35);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.TabIndex = 2;
		this.btnCancel.Text = "Cancel";
		this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnOk.Location = new System.Drawing.Point(304, 8);
		this.btnOk.Name = "btnOk";
		this.btnOk.TabIndex = 1;
		this.btnOk.Text = "OK";
		this.btnOk.Click += new System.EventHandler(btnOk_Click);
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
		base.ClientSize = new System.Drawing.Size(394, 66);
		base.Controls.Add(this.btnOk);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.txtColorDescription);
		base.Controls.Add(this.lblDescription);
		base.Controls.Add(this.picColor);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "AddNewColorSwatchForm";
		this.Text = "Add Color";
		base.ResumeLayout(false);
	}

	private void btnOk_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void txtColorDescription_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyData == Keys.Return)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}
		else if (e.KeyData == Keys.Escape)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
