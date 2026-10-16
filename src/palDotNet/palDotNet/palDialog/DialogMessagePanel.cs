using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using palDotNet.Properties;

namespace palDotNet.palDialog;

public class DialogMessagePanel : UserControl
{
	private IContainer components;

	private PictureBox MessageIcon;

	private DialogLabel MessageLabel;

	public string MessageText => MessageLabel.Text;

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
		this.MessageLabel = new palDotNet.palDialog.DialogLabel();
		this.MessageIcon = new System.Windows.Forms.PictureBox();
		((System.ComponentModel.ISupportInitialize)this.MessageIcon).BeginInit();
		base.SuspendLayout();
		this.MessageLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.MessageLabel.Location = new System.Drawing.Point(24, 2);
		this.MessageLabel.Name = "MessageLabel";
		this.MessageLabel.Size = new System.Drawing.Size(93, 16);
		this.MessageLabel.TabIndex = 0;
		this.MessageLabel.Text = "Message Text";
		this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.MessageIcon.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.MessageIcon.Image = palDotNet.Properties.Resources.Info16;
		this.MessageIcon.Location = new System.Drawing.Point(2, 2);
		this.MessageIcon.Name = "MessageIcon";
		this.MessageIcon.Size = new System.Drawing.Size(16, 16);
		this.MessageIcon.TabIndex = 0;
		this.MessageIcon.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.MessageLabel);
		base.Controls.Add(this.MessageIcon);
		base.Name = "DialogMessagePanel";
		base.Size = new System.Drawing.Size(120, 20);
		((System.ComponentModel.ISupportInitialize)this.MessageIcon).EndInit();
		base.ResumeLayout(false);
	}

	public DialogMessagePanel()
	{
		InitializeComponent();
		base.TabStop = false;
	}

	public void ClearMessage()
	{
		MessageLabel.Text = "";
		MessageIcon.Hide();
		ForceRepaint();
	}

	private void ForceRepaint()
	{
		base.Parent.Refresh();
		Refresh();
	}

	public void InfoMessage(string msg)
	{
		MessageLabel.Text = msg;
		MessageIcon.Image = Resources.Info16;
		MessageIcon.Show();
	}

	public void WarningMessage(string msg)
	{
		MessageLabel.Text = msg;
		MessageIcon.Image = Resources.Warning16;
		MessageIcon.Show();
	}

	public void ErrorMessage(string msg)
	{
		MessageLabel.Text = msg;
		MessageIcon.Image = Resources.Error16;
		MessageIcon.Show();
	}
}
