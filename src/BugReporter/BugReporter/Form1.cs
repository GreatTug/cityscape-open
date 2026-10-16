using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using BugReporter.Properties;

namespace BugReporter;

public class Form1 : Form
{
	private string ApplicationName;

	private string[] Attachments;

	private ProgressBar ProgressBar = new ProgressBar();

	private MailMessage MailMessage = new MailMessage();

	private IContainer components;

	private RichTextBox richTextBoxAdditionalComments;

	private Label label1;

	private Label labelApplication;

	private Button buttonSendBugReport;

	private Button buttonCancel;

	private Label label2;

	private Label labelAttachments;

	private Label label3;

	private LinkLabel linkLabelData;

	private Label label4;

	private Label label5;

	private Label label6;

	private Label label7;

	private TextBox textBoxEmailAddress;

	private RichTextBox richTextBoxComputerInformation;

	private TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private PictureBox pictureBox1;

	private bool HasEmailAddress => textBoxEmailAddress.Text != "<optional>";

	public Form1()
	{
		InitializeComponent();
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		string text = string.Empty;
		if (commandLineArgs.Length == 1)
		{
			ApplicationName = "PixelActive Terrain Editor";
			Attachments = new string[0];
			for (int i = 0; i < Attachments.Length; i++)
			{
				text += Attachments[i];
				if (i < Attachments.Length - 1)
				{
					text += ", ";
				}
			}
		}
		else
		{
			ApplicationName = commandLineArgs[0];
			List<string> list = new List<string>();
			for (int j = 1; j < commandLineArgs.Length; j++)
			{
				if (File.Exists(commandLineArgs[j]))
				{
					list.Add(commandLineArgs[j]);
				}
			}
			Attachments = list.ToArray();
			for (int k = 0; k < Attachments.Length; k++)
			{
				if (k > 0)
				{
					text += ", ";
				}
				text += Attachments[k];
			}
		}
		if (Attachments.Length > 0)
		{
			linkLabelData.Text = Path.GetDirectoryName(Attachments[0]);
		}
		labelApplication.Text = ApplicationName;
		labelAttachments.Text = text;
		richTextBoxComputerInformation.Text += ComputerInfo.OperatingSystem;
		richTextBoxComputerInformation.Text += ComputerInfo.GraphicsCard;
		richTextBoxComputerInformation.Text += ComputerInfo.Processor;
	}

	private void buttonCancel_Click(object sender, EventArgs e)
	{
		Application.Exit();
	}

	private void TrySend(string server, MailMessage mail, string logIn, string password, int? port)
	{
		SmtpClient smtpClient = new SmtpClient(server);
		if (!string.IsNullOrEmpty(logIn) && !string.IsNullOrEmpty(password))
		{
			smtpClient.Credentials = new NetworkCredential(logIn, password);
		}
		smtpClient.SendCompleted += smtp_SendCompleted;
		if (port.HasValue)
		{
			smtpClient.Port = port.Value;
		}
		port = smtpClient.Port;
		ProgressBar.StartProgress(server + ", port " + port);
		smtpClient.SendAsync(mail, null);
	}

	private void buttonSendBugReport_Click(object sender, EventArgs e)
	{
		if (!HasEmailAddress)
		{
			DialogResult dialogResult = MessageBox.Show("You have not inputted an email address.\nAre you sure you want to send this message without your email address in case we need to contact you?", "No Email Address", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.No)
			{
				return;
			}
		}
		bool flag = true;
		do
		{
			try
			{
				string address = "bugsender@pixelactive3d.com";
				MailMessage.From = new MailAddress(address);
				MailMessage.To.Add("defects@pixelactive3d.com");
				if (HasEmailAddress)
				{
					MailMessage.Subject = "Bug from " + textBoxEmailAddress.Text + " at " + DateTime.Now.ToString();
				}
				else
				{
					MailMessage.Subject = "Bug from " + Environment.UserName + " at " + DateTime.Now.ToString();
				}
				MailMessage.Body = "See attachments for minidump.  Additional User Comments:" + Environment.NewLine + Environment.NewLine + richTextBoxAdditionalComments.Text;
				MailMessage mailMessage = MailMessage;
				mailMessage.Body = mailMessage.Body + "-----------------------------------------------------------------------------" + Environment.NewLine;
				MailMessage mailMessage2 = MailMessage;
				mailMessage2.Body = mailMessage2.Body + richTextBoxComputerInformation.Text + Environment.NewLine;
				string[] attachments = Attachments;
				foreach (string fileName in attachments)
				{
					MailMessage.Attachments.Add(new Attachment(fileName));
				}
				TrySend("mail.pixelactive3d.com", MailMessage, "bugsender@pixelactive3d.com", "bu6600y", 26);
				flag = false;
			}
			catch (Exception exception)
			{
				flag = SendEmailException(exception);
			}
		}
		while (flag);
	}

	private void linkLabelData_Click(object sender, EventArgs e)
	{
		ProcessStartInfo processStartInfo = new ProcessStartInfo();
		processStartInfo.FileName = "explorer.exe";
		processStartInfo.Arguments = "\"" + linkLabelData.Text + "\"";
		Process.Start(processStartInfo);
	}

	private bool SendEmailException(Exception exception)
	{
		MessageBox.Show("Auto email failed.  Attempting to use your default email program.", "Auto Email Failed!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		try
		{
			MapiMailMessage mapiMailMessage = new MapiMailMessage(MailMessage.Subject, MailMessage.Body);
			mapiMailMessage.Recipients.Add(MailMessage.To[0].ToString());
			string[] attachments = Attachments;
			foreach (string value in attachments)
			{
				mapiMailMessage.Files.Add(value);
			}
			mapiMailMessage.ShowDialog(wait: true);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Failed to start default mail program: " + Environment.NewLine + Environment.NewLine + ex.Message, "Mail Failure", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			throw;
		}
		Application.Exit();
		return false;
	}

	private void smtp_SendCompleted(object sender, AsyncCompletedEventArgs e)
	{
		if (e.Error != null)
		{
			ProgressBar.StopProgressFail();
			if (SendEmailException(e.Error))
			{
				buttonSendBugReport_Click(this, null);
			}
			else
			{
				Application.Exit();
			}
		}
		else
		{
			ProgressBar.StopProgressSuccess();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BugReporter.Form1));
		this.richTextBoxAdditionalComments = new System.Windows.Forms.RichTextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.labelApplication = new System.Windows.Forms.Label();
		this.buttonSendBugReport = new System.Windows.Forms.Button();
		this.buttonCancel = new System.Windows.Forms.Button();
		this.label2 = new System.Windows.Forms.Label();
		this.labelAttachments = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.linkLabelData = new System.Windows.Forms.LinkLabel();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.textBoxEmailAddress = new System.Windows.Forms.TextBox();
		this.richTextBoxComputerInformation = new System.Windows.Forms.RichTextBox();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.richTextBoxAdditionalComments.Dock = System.Windows.Forms.DockStyle.Fill;
		this.richTextBoxAdditionalComments.Location = new System.Drawing.Point(3, 3);
		this.richTextBoxAdditionalComments.Name = "richTextBoxAdditionalComments";
		this.richTextBoxAdditionalComments.Size = new System.Drawing.Size(579, 161);
		this.richTextBoxAdditionalComments.TabIndex = 0;
		this.richTextBoxAdditionalComments.Text = "";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(12, 126);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(62, 13);
		this.label1.TabIndex = 3;
		this.label1.Text = "Application:";
		this.labelApplication.AutoSize = true;
		this.labelApplication.Location = new System.Drawing.Point(85, 127);
		this.labelApplication.Name = "labelApplication";
		this.labelApplication.Size = new System.Drawing.Size(0, 13);
		this.labelApplication.TabIndex = 4;
		this.buttonSendBugReport.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.buttonSendBugReport.Location = new System.Drawing.Point(333, 416);
		this.buttonSendBugReport.Name = "buttonSendBugReport";
		this.buttonSendBugReport.Size = new System.Drawing.Size(131, 23);
		this.buttonSendBugReport.TabIndex = 5;
		this.buttonSendBugReport.Text = "&Send Bug Report";
		this.buttonSendBugReport.UseVisualStyleBackColor = true;
		this.buttonSendBugReport.Click += new System.EventHandler(buttonSendBugReport_Click);
		this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.buttonCancel.Location = new System.Drawing.Point(470, 416);
		this.buttonCancel.Name = "buttonCancel";
		this.buttonCancel.Size = new System.Drawing.Size(131, 23);
		this.buttonCancel.TabIndex = 5;
		this.buttonCancel.Text = "&Cancel";
		this.buttonCancel.UseVisualStyleBackColor = true;
		this.buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(5, 143);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(69, 13);
		this.label2.TabIndex = 6;
		this.label2.Text = "Attachments:";
		this.labelAttachments.Location = new System.Drawing.Point(85, 143);
		this.labelAttachments.Name = "labelAttachments";
		this.labelAttachments.Size = new System.Drawing.Size(427, 42);
		this.labelAttachments.TabIndex = 4;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(144, 9);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(329, 19);
		this.label3.TabIndex = 7;
		this.label3.Text = "This application is closing due to a bug.";
		this.linkLabelData.AutoSize = true;
		this.linkLabelData.Location = new System.Drawing.Point(85, 89);
		this.linkLabelData.Name = "linkLabelData";
		this.linkLabelData.Size = new System.Drawing.Size(0, 13);
		this.linkLabelData.TabIndex = 8;
		this.linkLabelData.Click += new System.EventHandler(linkLabelData_Click);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(7, 89);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(67, 13);
		this.label4.TabIndex = 9;
		this.label4.Text = "Saved Data:";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(38, 104);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(560, 13);
		this.label5.TabIndex = 10;
		this.label5.Text = "(Note that saved data will appear in this directory after the bug report has been sent or canceled)";
		this.label6.Location = new System.Drawing.Point(147, 38);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(365, 40);
		this.label6.TabIndex = 11;
		this.label6.Text = resources.GetString("label6.Text");
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(14, 196);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(101, 13);
		this.label7.TabIndex = 12;
		this.label7.Text = "Your Email Address:";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.textBoxEmailAddress.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.textBoxEmailAddress.Location = new System.Drawing.Point(122, 193);
		this.textBoxEmailAddress.Name = "textBoxEmailAddress";
		this.textBoxEmailAddress.Size = new System.Drawing.Size(485, 20);
		this.textBoxEmailAddress.TabIndex = 13;
		this.textBoxEmailAddress.Text = "<optional>";
		this.richTextBoxComputerInformation.Dock = System.Windows.Forms.DockStyle.Fill;
		this.richTextBoxComputerInformation.Location = new System.Drawing.Point(3, 3);
		this.richTextBoxComputerInformation.Name = "richTextBoxComputerInformation";
		this.richTextBoxComputerInformation.Size = new System.Drawing.Size(579, 161);
		this.richTextBoxComputerInformation.TabIndex = 0;
		this.richTextBoxComputerInformation.Text = "";
		this.tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Location = new System.Drawing.Point(12, 219);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(593, 193);
		this.tabControl1.TabIndex = 15;
		this.tabPage1.Controls.Add(this.richTextBoxAdditionalComments);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(585, 167);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "Additional Comments";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.tabPage2.Controls.Add(this.richTextBoxComputerInformation);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(585, 167);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "Computer Information";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.pictureBox1.Image = BugReporter.Properties.Resources.warning;
		this.pictureBox1.Location = new System.Drawing.Point(104, 38);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(32, 32);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
		this.pictureBox1.TabIndex = 16;
		this.pictureBox1.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.buttonCancel;
		base.ClientSize = new System.Drawing.Size(616, 451);
		base.Controls.Add(this.pictureBox1);
		base.Controls.Add(this.tabControl1);
		base.Controls.Add(this.textBoxEmailAddress);
		base.Controls.Add(this.label7);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.linkLabelData);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.buttonCancel);
		base.Controls.Add(this.buttonSendBugReport);
		base.Controls.Add(this.labelAttachments);
		base.Controls.Add(this.labelApplication);
		base.Controls.Add(this.label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "Form1";
		base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "PixelActive Bug Reporter";
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
