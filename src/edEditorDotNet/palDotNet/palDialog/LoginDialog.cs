using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace palDotNet.palDialog;

[CPPCodeGeneration(OutputFile = "..\\muMultiUser\\muClient\\LoginDialog")]
[DialogBoxCreator]
public class LoginDialog : DialogBoxBase
{
	private IContainer components;

	private Button okBtn;

	private Button cancelBtn;

	private DialogTextBox Username;

	private DialogTextBox Password;

	private Label label1;

	private Label label2;

	private DialogLabel EncryptedPassword;

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
		this.okBtn = new System.Windows.Forms.Button();
		this.cancelBtn = new System.Windows.Forms.Button();
		this.Username = new palDotNet.palDialog.DialogTextBox();
		this.Password = new palDotNet.palDialog.DialogTextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.EncryptedPassword = new palDotNet.palDialog.DialogLabel();
		base.SuspendLayout();
		this.okBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.okBtn.Location = new System.Drawing.Point(79, 83);
		this.okBtn.Name = "okBtn";
		this.okBtn.Size = new System.Drawing.Size(75, 23);
		this.okBtn.TabIndex = 5;
		this.okBtn.Text = "OK";
		this.okBtn.UseVisualStyleBackColor = true;
		this.cancelBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cancelBtn.Location = new System.Drawing.Point(160, 83);
		this.cancelBtn.Name = "cancelBtn";
		this.cancelBtn.Size = new System.Drawing.Size(75, 23);
		this.cancelBtn.TabIndex = 6;
		this.cancelBtn.Text = "Cancel";
		this.cancelBtn.UseVisualStyleBackColor = true;
		this.Username.Location = new System.Drawing.Point(89, 23);
		this.Username.Name = "Username";
		this.Username.Size = new System.Drawing.Size(146, 20);
		this.Username.TabIndex = 3;
		this.Username.KeyUp += new System.Windows.Forms.KeyEventHandler(Username_KeyUp);
		this.Password.Location = new System.Drawing.Point(89, 49);
		this.Password.Name = "Password";
		this.Password.Size = new System.Drawing.Size(146, 20);
		this.Password.TabIndex = 4;
		this.Password.UseSystemPasswordChar = true;
		this.Password.Validated += new System.EventHandler(Password_Validated);
		this.Password.KeyUp += new System.Windows.Forms.KeyEventHandler(Password_KeyUp);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(25, 23);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(58, 13);
		this.label1.TabIndex = 8;
		this.label1.Text = "Username:";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(25, 49);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(56, 13);
		this.label2.TabIndex = 8;
		this.label2.Text = "Password:";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.EncryptedPassword.AutoSize = true;
		this.EncryptedPassword.Location = new System.Drawing.Point(13, 92);
		this.EncryptedPassword.Name = "EncryptedPassword";
		this.EncryptedPassword.Size = new System.Drawing.Size(0, 13);
		this.EncryptedPassword.TabIndex = 9;
		this.EncryptedPassword.Visible = false;
		base.AcceptButton = this.okBtn;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.CancelButton = this.cancelBtn;
		base.ClientSize = new System.Drawing.Size(247, 116);
		base.Controls.Add(this.EncryptedPassword);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.Password);
		base.Controls.Add(this.Username);
		base.Controls.Add(this.cancelBtn);
		base.Controls.Add(this.okBtn);
		this.ForeColor = System.Drawing.SystemColors.ControlText;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "LoginDialog";
		this.Text = "Login";
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public LoginDialog()
	{
		InitializeComponent();
		Password.Text = "";
		Password_Validated(null, EventArgs.Empty);
	}

	private void Password_Validated(object sender, EventArgs e)
	{
		MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
		byte[] bytes = Encoding.ASCII.GetBytes(Password.Text);
		byte[] array = mD5CryptoServiceProvider.ComputeHash(bytes);
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < array.Length; i++)
		{
			stringBuilder.AppendFormat("{0:x2}", array[i]);
		}
		EncryptedPassword.Text = stringBuilder.ToString();
	}

	private void Password_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			base.AcceptButton.PerformClick();
		}
	}

	private void Username_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			base.AcceptButton.PerformClick();
		}
	}
}
