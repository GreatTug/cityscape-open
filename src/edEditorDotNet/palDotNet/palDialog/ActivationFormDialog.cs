using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using edEditorDotNet.ActivationDialogs;
using edEditorDotNet.Properties;

namespace palDotNet.palDialog;

[DialogBoxCreator]
[CPPCodeGeneration(OutputFile = "..\\edEditor\\edActivation\\ActivationFormDialog", Win32PCOnly = false)]
public class ActivationFormDialog : DialogBoxBase
{
	private enum PostUserInfoResult
	{
		eSuccess,
		eConnectionFailure,
		eRequestFailed,
		eResponseFailed,
		eActivationFailed
	}

	private bool IsNameValid;

	private bool IsEmailValid;

	private bool IsVerifyEmailValid;

	private IContainer components;

	private Button cancelButton;

	private Button okButton;

	private DialogTextBox NameTextBox;

	private Label NameLabel;

	private Label EmailLabel;

	private DialogTextBox EmailTextBox;

	private Panel panel1;

	private Label RequirementsLabel;

	private LinkLabel linkLabelWebPrivacyPolicy;

	private Label EmailRequirementLabel;

	private ActivationTitlePanel TitlePanel;

	private DialogLabel HiddenResult;

	private Label PrivacyNoticeLabel;

	private Label VerifyEmailLabel;

	private TextBox VerifyEmailTextBox;

	private PictureBox ValidatedIcon_VerifyEmail;

	private PictureBox ValidatedIcon_Email;

	private Label CompanyLabel;

	private DialogTextBox PhoneTextBox;

	private Label PhoneLabel;

	private DialogTextBox ZipCodeTextBox;

	private Label ZipCodeLabel;

	private DialogComboBox CountryRegionComboBox;

	private Label CountryRegionLabel;

	private Label CurrentToolLabel;

	private DialogComboBox IndustryComboBox;

	private Label IndustryLabel;

	private DialogTextBox OrganizationTextBox;

	private DialogComboBox CurrentToolComboBox;

	private DialogTextBox PhoneExtensionTextBox;

	private Label PhoneExtensionLabel;

	private DialogLabel InfoLabel;

	private Label RequiredFieldsLabel;

	private PictureBox pictureBox1;

	public ActivationFormDialog()
	{
		InitializeComponent();
		InitCountryRegionComboBox();
		InitIndustryComboBox();
		InitCurrentToolComboBox();
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		ResetCountryRegionComboBox();
		ResetIndustryComboBox();
		ResetCurrentToolComboBox();
		HiddenResult.Text = "";
		IsNameValid = false;
		IsEmailValid = false;
		ValidateName();
		ValidateEmail();
		ValidateVerifyEmail();
		SyncZipCodeTextBox();
		SyncOkButton();
	}

	private void NameTextBox_TextChanged(object sender, EventArgs e)
	{
		ValidateName();
		SyncOkButton();
	}

	private void ValidateName()
	{
		if (NameTextBox.Text.Length > 1)
		{
			IsNameValid = true;
		}
		else
		{
			IsNameValid = false;
		}
	}

	private void EmailTextBox_TextChanged(object sender, EventArgs e)
	{
		ValidateEmail();
		ValidateVerifyEmail();
		SyncOkButton();
	}

	private void ValidateEmail()
	{
		if (string.IsNullOrEmpty(EmailTextBox.Text))
		{
			IsEmailValid = false;
			ClearValidatedIcon_Email();
			return;
		}
		if (EmailTextBox.Text.Length > 0)
		{
			Regex regex = new Regex("^[a-zA-Z][\\w\\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$");
			if (regex.IsMatch(EmailTextBox.Text))
			{
				IsEmailValid = true;
				SetSuccessfulValidatedIcon_Email();
				return;
			}
		}
		IsEmailValid = false;
		SetFailedValidatedIcon_Email();
	}

	private void ClearValidatedIcon_Email()
	{
		ValidatedIcon_Email.Image = null;
	}

	private void SetSuccessfulValidatedIcon_Email()
	{
		ValidatedIcon_Email.Image = Resources.Check16;
	}

	private void SetFailedValidatedIcon_Email()
	{
		ValidatedIcon_Email.Image = Resources.Error16;
	}

	private void VerifyEmailTextBox_TextChanged(object sender, EventArgs e)
	{
		ValidateVerifyEmail();
		SyncOkButton();
	}

	private void ValidateVerifyEmail()
	{
		if (string.IsNullOrEmpty(VerifyEmailTextBox.Text))
		{
			IsVerifyEmailValid = false;
			ClearValidatedIcon_VerifyEmail();
		}
		else if (!IsEmailValid)
		{
			IsVerifyEmailValid = false;
			SetFailedValidatedIcon_VerifyEmail();
		}
		else if (VerifyEmailTextBox.Text == EmailTextBox.Text)
		{
			IsVerifyEmailValid = true;
			SetSuccessfulValidatedIcon_VerifyEmail();
		}
		else
		{
			IsVerifyEmailValid = false;
			SetFailedValidatedIcon_VerifyEmail();
		}
	}

	private void ClearValidatedIcon_VerifyEmail()
	{
		ValidatedIcon_VerifyEmail.Image = null;
	}

	private void SetSuccessfulValidatedIcon_VerifyEmail()
	{
		ValidatedIcon_VerifyEmail.Image = Resources.Check16;
	}

	private void SetFailedValidatedIcon_VerifyEmail()
	{
		ValidatedIcon_VerifyEmail.Image = Resources.Error16;
	}

	private void InitCountryRegionComboBox()
	{
		string[] items = new string[205]
		{
			"United States", "Canada", "Afghanistan", "Albania", "Algeria", "Andorra", "Angola", "Anguilla", "Antigua and Barbuda", "Argentina",
			"Armenia", "Aruba", "Australia", "Austria", "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus",
			"Belgium", "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia", "Bosnia & Herzegovina", "Botswana", "Brazil", "Brunei Darussalam",
			"Bulgaria", "Burkina Faso", "Burundi", "Cambodia", "Cameroon", "Cape Verde", "Cayman Island", "Central African Republic", "Chad", "Chile",
			"China", "Colombia", "Comoros Island", "Congo, Democratic Republic of the", "Congo, Republic of the", "Costa Rica", "Cote DIvoire", "Croatia", "Cyprus", "Czech Republic",
			"Denmark", "Djibouti", "Dominica", "Dominican Republic", "East Timor", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea",
			"Estonia", "Ethiopia", "Falkland Islands", "Faroe Islands", "Finland", "France", "French Guiana", "French Polynesia", "FYR of Macedonia", "Gabon",
			"Gambia", "Georgia", "Germany", "Ghana", "Gibraltar", "Greece", "Greenland", "Grenada", "Guadeloupe", "Guatemala",
			"Guinea", "Guinea-Bissau", "Guyana", "Haiti", "Honduras", "Hong Kong", "Hungary", "Iceland", "India", "Indonesia",
			"Iraq", "Ireland", "Israel", "Italy", "Jamaica", "Japan", "Jordan", "Kazakhstan", "Kenya", "Korea, South",
			"Kuwait", "Kyrgyzstan", "Lao", "Latvia", "Lebanon", "Lesotho", "Liberia", "Liechtenstein", "Lithuania", "Luxembourg",
			"Macau", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Martinique", "Mauritania", "Mauritius",
			"Mayotte", "Mexico", "Moldova", "Monaco", "Mongolia", "Montenegro", "Montserrat", "Morocco", "Mozambique", "Myanmar",
			"Namibia", "Nepal", "Netherlands", "Netherlands Antilles", "New Caledonia", "New Zealand", "Nicaragua", "Niger", "Nigeria", "Norway",
			"Oman", "Pakistan", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Poland", "Portugal", "Puerto Rico",
			"Qatar", "Reunion", "Romania", "Russian Federation", "Rwanda", "S. Georgia and S. Sandwich Islands", "Saint Kitts and Nevis", "Saint Lucia", "Saint Vincent Islands", "San Marino",
			"Sao Tome & Principe", "Saudi Arabia", "Senegal", "Serbia", "Seychelles", "Sierra Leone", "Singapore", "Slovak Republic", "Slovenia", "Somalia",
			"South Africa", "Spain", "Sri Lanka", "St. Helena", "St. Pierre et Miquelon", "Suriname", "Swaziland", "Sweden", "Switzerland", "Taiwan",
			"Tajikistan", "Tanzania", "Thailand", "Togo ", "Trinidad", "Tunisia", "Turkey", "Turkmenistan", "Turks and Caicos Islands", "Uganda",
			"Ukraine", "United Arab Emirates", "United Kingdom", "Uruguay", "Uzbekistan", "Vatican City", "Venezuela", "Vietnam", "Virgin Island (British)", "Virgin Island (US)",
			"Wallis & Futuna Island", "Western Sahara", "Yemen", "Zambia", "Zimbabwe"
		};
		CountryRegionComboBox.Items.Clear();
		CountryRegionComboBox.Items.AddRange(items);
		CountryRegionComboBox.SelectedIndex = -1;
	}

	private void ResetCountryRegionComboBox()
	{
		CountryRegionComboBox.SelectedIndex = -1;
	}

	private void CountryRegionComboBox_SelectedIndexChanged(object sender, EventArgs e)
	{
		SyncZipCodeTextBox();
	}

	private void SyncZipCodeTextBox()
	{
		if (CountryRegionComboBox.Text == "United States")
		{
			ZipCodeTextBox.Enabled = true;
			return;
		}
		ZipCodeTextBox.Enabled = false;
		ZipCodeTextBox.Text = "";
	}

	private void ZipCodeTextBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.KeyChar.ToString();
		if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '\b')
		{
			e.Handled = true;
		}
	}

	private void PhoneTextBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.KeyChar.ToString();
		if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '.' && e.KeyChar != '(' && e.KeyChar != ')' && e.KeyChar != ' ' && e.KeyChar != '\b')
		{
			e.Handled = true;
		}
	}

	private void PhoneExtensionTextBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.KeyChar.ToString();
		if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
		{
			e.Handled = true;
		}
	}

	private string GetPhoneText()
	{
		string text = "";
		if (PhoneTextBox.Text.Length > 0)
		{
			text += PhoneTextBox.Text;
			if (PhoneExtensionTextBox.Text.Length > 0)
			{
				text += " Ext. ";
				text += PhoneExtensionTextBox.Text;
			}
		}
		return text;
	}

	private void OrganizationTextBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.KeyChar.ToString();
		if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '-' && e.KeyChar != ',' && e.KeyChar != '.' && e.KeyChar != '\b')
		{
			e.Handled = true;
		}
	}

	private void InitIndustryComboBox()
	{
		string[] items = new string[9] { "Game Development", "Simulation", "Film / Media", "Architecture", "Urban Planning", "Navigation / GIS", "Transportation", "Academic", "Other" };
		IndustryComboBox.Items.Clear();
		IndustryComboBox.Items.AddRange(items);
		IndustryComboBox.SelectedIndex = -1;
	}

	private void ResetIndustryComboBox()
	{
		IndustryComboBox.SelectedIndex = -1;
	}

	private void InitCurrentToolComboBox()
	{
		string[] items = new string[6] { "None", "Modeling Package (Maya / 3ds Max / ...)", "Level Editor (UnrealEd / Hammer / ...)", "CAD Tool", "Internally Developed Tool", "Other" };
		CurrentToolComboBox.Items.Clear();
		CurrentToolComboBox.Items.AddRange(items);
		CurrentToolComboBox.SelectedIndex = -1;
	}

	private void ResetCurrentToolComboBox()
	{
		CurrentToolComboBox.SelectedIndex = -1;
	}

	private void linkLabelWebPrivacyPolicy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		try
		{
			Process.Start("http://pixelactive3d.com/Privacy/Registration.php");
		}
		catch (Exception)
		{
		}
	}

	private PostUserInfoResult PostUserInfoToServer()
	{
		string requestUriString = "http://pixelactive3d.com/ActivationCenter/ActivateCityScape.php";
		string text = NameTextBox.Text;
		string text2 = EmailTextBox.Text;
		string text3 = CountryRegionComboBox.Text;
		string text4 = ZipCodeTextBox.Text;
		string phoneText = GetPhoneText();
		string text5 = OrganizationTextBox.Text;
		string text6 = IndustryComboBox.Text;
		string text7 = CurrentToolComboBox.Text;
		string localMacAddress = ActivationHelpers.GetLocalMacAddress();
		if (string.IsNullOrEmpty(localMacAddress))
		{
			return PostUserInfoResult.eConnectionFailure;
		}
		string version = TitlePanel.GetVersion();
		string oSName = ActivationHelpers.GetOSName();
		string text8 = "action=activate";
		text8 = text8 + "&Name=" + text;
		text8 = text8 + "&Email=" + text2;
		text8 = text8 + "&MacAddress=" + localMacAddress;
		text8 = text8 + "&OS=" + oSName;
		text8 = text8 + "&Version=" + version;
		text8 = text8 + "&Country=" + text3;
		text8 = text8 + "&ZipCode=" + text4;
		text8 = text8 + "&Phone=" + phoneText;
		text8 = text8 + "&Organization=" + text5;
		text8 = text8 + "&Industry=" + text6;
		text8 = text8 + "&CurrentTool=" + text7;
		byte[] bytes = Encoding.UTF8.GetBytes(text8);
		WebRequest webRequest;
		try
		{
			webRequest = WebRequest.Create(requestUriString);
			webRequest.Method = "POST";
			webRequest.ContentType = "application/x-www-form-urlencoded";
			webRequest.ContentLength = bytes.Length;
			Stream requestStream = webRequest.GetRequestStream();
			requestStream.Write(bytes, 0, bytes.Length);
			requestStream.Close();
		}
		catch (Exception)
		{
			return PostUserInfoResult.eRequestFailed;
		}
		string text9 = null;
		try
		{
			WebResponse response = webRequest.GetResponse();
			Stream responseStream = response.GetResponseStream();
			StreamReader streamReader = new StreamReader(responseStream);
			text9 = streamReader.ReadToEnd();
			streamReader.Close();
			responseStream.Close();
			response.Close();
		}
		catch (Exception)
		{
			return PostUserInfoResult.eResponseFailed;
		}
		if (text9 == "Activated")
		{
			return PostUserInfoResult.eSuccess;
		}
		return PostUserInfoResult.eActivationFailed;
	}

	private void SyncOkButton()
	{
		if (IsNameValid && IsEmailValid && IsVerifyEmailValid)
		{
			AllowSubmitDialog = true;
			okButton.Enabled = true;
		}
		else
		{
			AllowSubmitDialog = false;
			okButton.Enabled = false;
		}
	}

	private void okButton_Click(object sender, EventArgs e)
	{
		PostUserInfoResult postUserInfoResult = PostUserInfoToServer();
		switch (postUserInfoResult)
		{
		case PostUserInfoResult.eSuccess:
		{
			string text = "An email with your registration key has been sent to:\n" + EmailTextBox.Text + "\n\n";
			text += "You will need this key to complete the registration in the next dialog.\n";
			MessageBox.Show(text, "CityScape Registration", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			break;
		}
		case PostUserInfoResult.eConnectionFailure:
		case PostUserInfoResult.eRequestFailed:
		case PostUserInfoResult.eResponseFailed:
		{
			string text = "Failed to contact registration key server.\nPlease check your internet connection and try again.";
			MessageBox.Show(text, "CityScape Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			break;
		}
		case PostUserInfoResult.eActivationFailed:
		{
			string text = "Failed to retrieve registration key.";
			MessageBox.Show(text, "CityScape Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			break;
		}
		default:
		{
			string text = "An internal error occurred while attempting to retrieve your registration key.";
			MessageBox.Show(text, "CityScape Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			break;
		}
		}
		if (postUserInfoResult == PostUserInfoResult.eSuccess)
		{
			HiddenResult.Text = "Success";
			base.DialogResult = DialogResult.OK;
		}
		else
		{
			HiddenResult.Text = "Error";
			base.DialogResult = DialogResult.No;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(palDotNet.palDialog.ActivationFormDialog));
		this.cancelButton = new System.Windows.Forms.Button();
		this.okButton = new System.Windows.Forms.Button();
		this.NameTextBox = new palDotNet.palDialog.DialogTextBox();
		this.NameLabel = new System.Windows.Forms.Label();
		this.EmailLabel = new System.Windows.Forms.Label();
		this.EmailTextBox = new palDotNet.palDialog.DialogTextBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.RequiredFieldsLabel = new System.Windows.Forms.Label();
		this.PhoneExtensionTextBox = new palDotNet.palDialog.DialogTextBox();
		this.PhoneExtensionLabel = new System.Windows.Forms.Label();
		this.CurrentToolComboBox = new palDotNet.palDialog.DialogComboBox();
		this.CurrentToolLabel = new System.Windows.Forms.Label();
		this.IndustryComboBox = new palDotNet.palDialog.DialogComboBox();
		this.IndustryLabel = new System.Windows.Forms.Label();
		this.OrganizationTextBox = new palDotNet.palDialog.DialogTextBox();
		this.CompanyLabel = new System.Windows.Forms.Label();
		this.PhoneTextBox = new palDotNet.palDialog.DialogTextBox();
		this.PhoneLabel = new System.Windows.Forms.Label();
		this.ZipCodeTextBox = new palDotNet.palDialog.DialogTextBox();
		this.ZipCodeLabel = new System.Windows.Forms.Label();
		this.CountryRegionComboBox = new palDotNet.palDialog.DialogComboBox();
		this.CountryRegionLabel = new System.Windows.Forms.Label();
		this.ValidatedIcon_Email = new System.Windows.Forms.PictureBox();
		this.ValidatedIcon_VerifyEmail = new System.Windows.Forms.PictureBox();
		this.VerifyEmailTextBox = new System.Windows.Forms.TextBox();
		this.VerifyEmailLabel = new System.Windows.Forms.Label();
		this.EmailRequirementLabel = new System.Windows.Forms.Label();
		this.RequirementsLabel = new System.Windows.Forms.Label();
		this.linkLabelWebPrivacyPolicy = new System.Windows.Forms.LinkLabel();
		this.HiddenResult = new palDotNet.palDialog.DialogLabel();
		this.PrivacyNoticeLabel = new System.Windows.Forms.Label();
		this.InfoLabel = new palDotNet.palDialog.DialogLabel();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.TitlePanel = new edEditorDotNet.ActivationDialogs.ActivationTitlePanel();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ValidatedIcon_Email).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ValidatedIcon_VerifyEmail).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cancelButton.Location = new System.Drawing.Point(368, 597);
		this.cancelButton.Name = "cancelButton";
		this.cancelButton.Size = new System.Drawing.Size(96, 23);
		this.cancelButton.TabIndex = 7;
		this.cancelButton.Text = "Not Now";
		this.cancelButton.UseVisualStyleBackColor = true;
		this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.okButton.Location = new System.Drawing.Point(252, 597);
		this.okButton.Name = "okButton";
		this.okButton.Size = new System.Drawing.Size(110, 23);
		this.okButton.TabIndex = 8;
		this.okButton.Text = "Request Key";
		this.okButton.UseVisualStyleBackColor = true;
		this.okButton.Click += new System.EventHandler(okButton_Click);
		this.NameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.NameTextBox.Location = new System.Drawing.Point(134, 15);
		this.NameTextBox.MaxLength = 100;
		this.NameTextBox.Name = "NameTextBox";
		this.NameTextBox.SaveToConfig = true;
		this.NameTextBox.Size = new System.Drawing.Size(255, 20);
		this.NameTextBox.TabIndex = 1;
		this.NameTextBox.TextChanged += new System.EventHandler(NameTextBox_TextChanged);
		this.NameLabel.AutoSize = true;
		this.NameLabel.Location = new System.Drawing.Point(26, 18);
		this.NameLabel.Name = "NameLabel";
		this.NameLabel.Size = new System.Drawing.Size(42, 13);
		this.NameLabel.TabIndex = 0;
		this.NameLabel.Text = "Name*:";
		this.EmailLabel.AutoSize = true;
		this.EmailLabel.Location = new System.Drawing.Point(26, 47);
		this.EmailLabel.Name = "EmailLabel";
		this.EmailLabel.Size = new System.Drawing.Size(39, 13);
		this.EmailLabel.TabIndex = 2;
		this.EmailLabel.Text = "Email*:";
		this.EmailTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.EmailTextBox.Location = new System.Drawing.Point(134, 44);
		this.EmailTextBox.MaxLength = 128;
		this.EmailTextBox.Name = "EmailTextBox";
		this.EmailTextBox.SaveToConfig = true;
		this.EmailTextBox.Size = new System.Drawing.Size(255, 20);
		this.EmailTextBox.TabIndex = 3;
		this.EmailTextBox.TextChanged += new System.EventHandler(EmailTextBox_TextChanged);
		this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.panel1.Controls.Add(this.RequiredFieldsLabel);
		this.panel1.Controls.Add(this.PhoneExtensionTextBox);
		this.panel1.Controls.Add(this.PhoneExtensionLabel);
		this.panel1.Controls.Add(this.CurrentToolComboBox);
		this.panel1.Controls.Add(this.CurrentToolLabel);
		this.panel1.Controls.Add(this.IndustryComboBox);
		this.panel1.Controls.Add(this.IndustryLabel);
		this.panel1.Controls.Add(this.OrganizationTextBox);
		this.panel1.Controls.Add(this.CompanyLabel);
		this.panel1.Controls.Add(this.PhoneTextBox);
		this.panel1.Controls.Add(this.PhoneLabel);
		this.panel1.Controls.Add(this.ZipCodeTextBox);
		this.panel1.Controls.Add(this.ZipCodeLabel);
		this.panel1.Controls.Add(this.CountryRegionComboBox);
		this.panel1.Controls.Add(this.CountryRegionLabel);
		this.panel1.Controls.Add(this.ValidatedIcon_Email);
		this.panel1.Controls.Add(this.ValidatedIcon_VerifyEmail);
		this.panel1.Controls.Add(this.VerifyEmailTextBox);
		this.panel1.Controls.Add(this.VerifyEmailLabel);
		this.panel1.Controls.Add(this.EmailRequirementLabel);
		this.panel1.Controls.Add(this.EmailTextBox);
		this.panel1.Controls.Add(this.NameLabel);
		this.panel1.Controls.Add(this.EmailLabel);
		this.panel1.Controls.Add(this.NameTextBox);
		this.panel1.Location = new System.Drawing.Point(30, 153);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(434, 332);
		this.panel1.TabIndex = 2;
		this.RequiredFieldsLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.RequiredFieldsLabel.AutoSize = true;
		this.RequiredFieldsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.RequiredFieldsLabel.ForeColor = System.Drawing.Color.Red;
		this.RequiredFieldsLabel.Location = new System.Drawing.Point(26, 309);
		this.RequiredFieldsLabel.Name = "RequiredFieldsLabel";
		this.RequiredFieldsLabel.Size = new System.Drawing.Size(86, 13);
		this.RequiredFieldsLabel.TabIndex = 21;
		this.RequiredFieldsLabel.Text = "* Required Fields";
		this.PhoneExtensionTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.PhoneExtensionTextBox.Location = new System.Drawing.Point(338, 188);
		this.PhoneExtensionTextBox.MaxLength = 10;
		this.PhoneExtensionTextBox.Name = "PhoneExtensionTextBox";
		this.PhoneExtensionTextBox.SaveToConfig = true;
		this.PhoneExtensionTextBox.Size = new System.Drawing.Size(51, 20);
		this.PhoneExtensionTextBox.TabIndex = 14;
		this.PhoneExtensionTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(PhoneExtensionTextBox_KeyPress);
		this.PhoneExtensionLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.PhoneExtensionLabel.AutoSize = true;
		this.PhoneExtensionLabel.Location = new System.Drawing.Point(307, 191);
		this.PhoneExtensionLabel.Name = "PhoneExtensionLabel";
		this.PhoneExtensionLabel.Size = new System.Drawing.Size(25, 13);
		this.PhoneExtensionLabel.TabIndex = 13;
		this.PhoneExtensionLabel.Text = "Ext.";
		this.CurrentToolComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.CurrentToolComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.CurrentToolComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.CurrentToolComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.CurrentToolComboBox.FormattingEnabled = true;
		this.CurrentToolComboBox.Location = new System.Drawing.Point(175, 272);
		this.CurrentToolComboBox.Name = "CurrentToolComboBox";
		this.CurrentToolComboBox.SaveToConfig = true;
		this.CurrentToolComboBox.Size = new System.Drawing.Size(214, 21);
		this.CurrentToolComboBox.TabIndex = 20;
		this.CurrentToolLabel.Location = new System.Drawing.Point(26, 270);
		this.CurrentToolLabel.Name = "CurrentToolLabel";
		this.CurrentToolLabel.Size = new System.Drawing.Size(141, 30);
		this.CurrentToolLabel.TabIndex = 19;
		this.CurrentToolLabel.Text = "Current Tool used for Creating Environments:";
		this.IndustryComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.IndustryComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.IndustryComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.IndustryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.IndustryComboBox.FormattingEnabled = true;
		this.IndustryComboBox.Location = new System.Drawing.Point(175, 240);
		this.IndustryComboBox.Name = "IndustryComboBox";
		this.IndustryComboBox.SaveToConfig = true;
		this.IndustryComboBox.Size = new System.Drawing.Size(214, 21);
		this.IndustryComboBox.TabIndex = 18;
		this.IndustryLabel.AutoSize = true;
		this.IndustryLabel.Location = new System.Drawing.Point(26, 243);
		this.IndustryLabel.Name = "IndustryLabel";
		this.IndustryLabel.Size = new System.Drawing.Size(47, 13);
		this.IndustryLabel.TabIndex = 17;
		this.IndustryLabel.Text = "Industry:";
		this.OrganizationTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.OrganizationTextBox.Location = new System.Drawing.Point(175, 214);
		this.OrganizationTextBox.MaxLength = 100;
		this.OrganizationTextBox.Name = "OrganizationTextBox";
		this.OrganizationTextBox.SaveToConfig = true;
		this.OrganizationTextBox.Size = new System.Drawing.Size(214, 20);
		this.OrganizationTextBox.TabIndex = 16;
		this.OrganizationTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(OrganizationTextBox_KeyPress);
		this.CompanyLabel.AutoSize = true;
		this.CompanyLabel.Location = new System.Drawing.Point(26, 217);
		this.CompanyLabel.Name = "CompanyLabel";
		this.CompanyLabel.Size = new System.Drawing.Size(124, 13);
		this.CompanyLabel.TabIndex = 15;
		this.CompanyLabel.Text = "Company / Organization:";
		this.PhoneTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.PhoneTextBox.Location = new System.Drawing.Point(175, 188);
		this.PhoneTextBox.MaxLength = 20;
		this.PhoneTextBox.Name = "PhoneTextBox";
		this.PhoneTextBox.SaveToConfig = true;
		this.PhoneTextBox.Size = new System.Drawing.Size(126, 20);
		this.PhoneTextBox.TabIndex = 12;
		this.PhoneTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(PhoneTextBox_KeyPress);
		this.PhoneLabel.AutoSize = true;
		this.PhoneLabel.Location = new System.Drawing.Point(26, 191);
		this.PhoneLabel.Name = "PhoneLabel";
		this.PhoneLabel.Size = new System.Drawing.Size(41, 13);
		this.PhoneLabel.TabIndex = 11;
		this.PhoneLabel.Text = "Phone:";
		this.ZipCodeTextBox.Location = new System.Drawing.Point(175, 162);
		this.ZipCodeTextBox.MaxLength = 10;
		this.ZipCodeTextBox.Name = "ZipCodeTextBox";
		this.ZipCodeTextBox.SaveToConfig = true;
		this.ZipCodeTextBox.Size = new System.Drawing.Size(90, 20);
		this.ZipCodeTextBox.TabIndex = 10;
		this.ZipCodeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(ZipCodeTextBox_KeyPress);
		this.ZipCodeLabel.AutoSize = true;
		this.ZipCodeLabel.Location = new System.Drawing.Point(26, 165);
		this.ZipCodeLabel.Name = "ZipCodeLabel";
		this.ZipCodeLabel.Size = new System.Drawing.Size(101, 13);
		this.ZipCodeLabel.TabIndex = 9;
		this.ZipCodeLabel.Text = "Zip Code (US Only):";
		this.CountryRegionComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.CountryRegionComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.CountryRegionComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.CountryRegionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.CountryRegionComboBox.FormattingEnabled = true;
		this.CountryRegionComboBox.Location = new System.Drawing.Point(175, 135);
		this.CountryRegionComboBox.Name = "CountryRegionComboBox";
		this.CountryRegionComboBox.SaveToConfig = true;
		this.CountryRegionComboBox.Size = new System.Drawing.Size(214, 21);
		this.CountryRegionComboBox.TabIndex = 8;
		this.CountryRegionComboBox.SelectedIndexChanged += new System.EventHandler(CountryRegionComboBox_SelectedIndexChanged);
		this.CountryRegionLabel.AutoSize = true;
		this.CountryRegionLabel.Location = new System.Drawing.Point(26, 138);
		this.CountryRegionLabel.Name = "CountryRegionLabel";
		this.CountryRegionLabel.Size = new System.Drawing.Size(91, 13);
		this.CountryRegionLabel.TabIndex = 7;
		this.CountryRegionLabel.Text = "Country / Region:";
		this.ValidatedIcon_Email.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.ValidatedIcon_Email.Location = new System.Drawing.Point(395, 47);
		this.ValidatedIcon_Email.Name = "ValidatedIcon_Email";
		this.ValidatedIcon_Email.Size = new System.Drawing.Size(16, 16);
		this.ValidatedIcon_Email.TabIndex = 16;
		this.ValidatedIcon_Email.TabStop = false;
		this.ValidatedIcon_VerifyEmail.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.ValidatedIcon_VerifyEmail.Image = edEditorDotNet.Properties.Resources.Error16;
		this.ValidatedIcon_VerifyEmail.Location = new System.Drawing.Point(395, 76);
		this.ValidatedIcon_VerifyEmail.Name = "ValidatedIcon_VerifyEmail";
		this.ValidatedIcon_VerifyEmail.Size = new System.Drawing.Size(16, 16);
		this.ValidatedIcon_VerifyEmail.TabIndex = 15;
		this.ValidatedIcon_VerifyEmail.TabStop = false;
		this.VerifyEmailTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.VerifyEmailTextBox.Location = new System.Drawing.Point(134, 73);
		this.VerifyEmailTextBox.MaxLength = 128;
		this.VerifyEmailTextBox.Name = "VerifyEmailTextBox";
		this.VerifyEmailTextBox.Size = new System.Drawing.Size(255, 20);
		this.VerifyEmailTextBox.TabIndex = 5;
		this.VerifyEmailTextBox.TextChanged += new System.EventHandler(VerifyEmailTextBox_TextChanged);
		this.VerifyEmailLabel.AutoSize = true;
		this.VerifyEmailLabel.Location = new System.Drawing.Point(26, 76);
		this.VerifyEmailLabel.Name = "VerifyEmailLabel";
		this.VerifyEmailLabel.Size = new System.Drawing.Size(68, 13);
		this.VerifyEmailLabel.TabIndex = 4;
		this.VerifyEmailLabel.Text = "Verify Email*:";
		this.EmailRequirementLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.EmailRequirementLabel.AutoSize = true;
		this.EmailRequirementLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.EmailRequirementLabel.Location = new System.Drawing.Point(67, 105);
		this.EmailRequirementLabel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
		this.EmailRequirementLabel.Name = "EmailRequirementLabel";
		this.EmailRequirementLabel.Size = new System.Drawing.Size(288, 13);
		this.EmailRequirementLabel.TabIndex = 6;
		this.EmailRequirementLabel.Text = "A registration key will be sent to the specified email address.";
		this.EmailRequirementLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.RequirementsLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.RequirementsLabel.AutoSize = true;
		this.RequirementsLabel.Location = new System.Drawing.Point(52, 505);
		this.RequirementsLabel.Name = "RequirementsLabel";
		this.RequirementsLabel.Size = new System.Drawing.Size(273, 13);
		this.RequirementsLabel.TabIndex = 3;
		this.RequirementsLabel.Text = "Registration key request requires an internet connection.";
		this.linkLabelWebPrivacyPolicy.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.linkLabelWebPrivacyPolicy.AutoSize = true;
		this.linkLabelWebPrivacyPolicy.BackColor = System.Drawing.Color.Transparent;
		this.linkLabelWebPrivacyPolicy.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
		this.linkLabelWebPrivacyPolicy.Location = new System.Drawing.Point(27, 602);
		this.linkLabelWebPrivacyPolicy.Name = "linkLabelWebPrivacyPolicy";
		this.linkLabelWebPrivacyPolicy.Size = new System.Drawing.Size(73, 13);
		this.linkLabelWebPrivacyPolicy.TabIndex = 5;
		this.linkLabelWebPrivacyPolicy.TabStop = true;
		this.linkLabelWebPrivacyPolicy.Text = "Privacy Policy";
		this.linkLabelWebPrivacyPolicy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabelWebPrivacyPolicy_LinkClicked);
		this.HiddenResult.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.HiddenResult.AutoSize = true;
		this.HiddenResult.Location = new System.Drawing.Point(150, 602);
		this.HiddenResult.Name = "HiddenResult";
		this.HiddenResult.SaveToConfig = false;
		this.HiddenResult.Size = new System.Drawing.Size(71, 13);
		this.HiddenResult.TabIndex = 6;
		this.HiddenResult.Text = "HiddenResult";
		this.HiddenResult.Visible = false;
		this.PrivacyNoticeLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.PrivacyNoticeLabel.Location = new System.Drawing.Point(27, 532);
		this.PrivacyNoticeLabel.Name = "PrivacyNoticeLabel";
		this.PrivacyNoticeLabel.Size = new System.Drawing.Size(437, 44);
		this.PrivacyNoticeLabel.TabIndex = 4;
		this.PrivacyNoticeLabel.Text = resources.GetString("PrivacyNoticeLabel.Text");
		this.InfoLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.InfoLabel.Location = new System.Drawing.Point(28, 97);
		this.InfoLabel.Name = "InfoLabel";
		this.InfoLabel.SaveToConfig = false;
		this.InfoLabel.Size = new System.Drawing.Size(436, 50);
		this.InfoLabel.TabIndex = 9;
		this.InfoLabel.Text = "In order to export, we ask that you register your copy of CityScape.\r\nRegistration is free.  After registration, this dialog will not appear again.";
		this.pictureBox1.Image = edEditorDotNet.Properties.Resources.Info16;
		this.pictureBox1.Location = new System.Drawing.Point(30, 503);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(16, 16);
		this.pictureBox1.TabIndex = 22;
		this.pictureBox1.TabStop = false;
		this.TitlePanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TitlePanel.BackColor = System.Drawing.SystemColors.Window;
		this.TitlePanel.Location = new System.Drawing.Point(24, 14);
		this.TitlePanel.Margin = new System.Windows.Forms.Padding(15, 5, 15, 3);
		this.TitlePanel.Name = "TitlePanel";
		this.TitlePanel.Size = new System.Drawing.Size(446, 80);
		this.TitlePanel.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Window;
		base.CancelButton = this.cancelButton;
		base.ClientSize = new System.Drawing.Size(494, 632);
		base.Controls.Add(this.pictureBox1);
		base.Controls.Add(this.InfoLabel);
		base.Controls.Add(this.PrivacyNoticeLabel);
		base.Controls.Add(this.HiddenResult);
		base.Controls.Add(this.TitlePanel);
		base.Controls.Add(this.linkLabelWebPrivacyPolicy);
		base.Controls.Add(this.RequirementsLabel);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.okButton);
		base.Controls.Add(this.cancelButton);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "ActivationFormDialog";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "CityScape Activation - Key Request";
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ValidatedIcon_Email).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ValidatedIcon_VerifyEmail).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
