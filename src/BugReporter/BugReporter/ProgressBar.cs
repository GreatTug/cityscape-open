using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BugReporter;

public class ProgressBar : Form
{
	private IContainer components;

	private System.Windows.Forms.ProgressBar progressBar1;

	private Label label1;

	private Button button1;

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
		this.progressBar1 = new System.Windows.Forms.ProgressBar();
		this.label1 = new System.Windows.Forms.Label();
		this.button1 = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.progressBar1.Location = new System.Drawing.Point(13, 38);
		this.progressBar1.Name = "progressBar1";
		this.progressBar1.Size = new System.Drawing.Size(290, 23);
		this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
		this.progressBar1.TabIndex = 0;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(13, 13);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(83, 13);
		this.label1.TabIndex = 1;
		this.label1.Text = "Sending Email...";
		this.button1.Location = new System.Drawing.Point(119, 74);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 2;
		this.button1.Text = "&Ok";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(315, 109);
		base.ControlBox = false;
		base.Controls.Add(this.button1);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.progressBar1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "ProgressBar";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Sending Email";
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public ProgressBar()
	{
		InitializeComponent();
	}

	public void StartProgress(string message)
	{
		label1.Text = "Sending Email (" + message + ") ...";
		button1.Enabled = false;
		progressBar1.Visible = true;
		Show();
	}

	public void StopProgressSuccess()
	{
		progressBar1.Visible = false;
		label1.Text = "Email Successfully Sent!";
		button1.Enabled = true;
	}

	public void StopProgressFail()
	{
		Hide();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		Application.Exit();
	}
}
