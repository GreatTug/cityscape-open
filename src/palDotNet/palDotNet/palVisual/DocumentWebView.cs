using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using palDotNet.palWidgets;

namespace palDotNet.palVisual;

public class DocumentWebView : DocumentView, IWebBrowserVisual
{
	private IContainer components;

	private System.Windows.Forms.WebBrowser webBrowser1;

	private palDotNet.palWidgets.WebBrowser WebBrowser;

	[Browsable(false)]
	[ReadOnly(true)]
	public override Guid Guid => WebBrowser.Guid;

	private WebViewControlSettings ConfigSettings { get; set; }

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			SaveComponentSettings();
			if (components != null)
			{
				components.Dispose();
			}
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.webBrowser1 = new System.Windows.Forms.WebBrowser();
		base.SuspendLayout();
		this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.webBrowser1.Location = new System.Drawing.Point(0, 0);
		this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
		this.webBrowser1.Name = "webBrowser1";
		this.webBrowser1.Size = new System.Drawing.Size(292, 273);
		this.webBrowser1.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(292, 273);
		base.Controls.Add(this.webBrowser1);
		base.Name = "DocumentWebView";
		base.ResumeLayout(false);
	}

	public DocumentWebView()
	{
		InitializeComponent();
		base.ShowHint = DockState.Document;
	}

	public static void Register(DockPanel dockPanel)
	{
		WidgetBase.EventCreate += delegate(object s, EventArgs e)
		{
			if (s is palDotNet.palWidgets.WebBrowser webBrowser)
			{
				DocumentWebView documentWebView = new DocumentWebView();
				documentWebView.WebBrowser = webBrowser;
				documentWebView.Init(dockPanel, webBrowser, documentWebView);
				DocumentView.TheWindows.Add(webBrowser, documentWebView);
				webBrowser.Visual = documentWebView;
			}
		};
	}

	public void Navigate(string urlString, bool showIfHidden)
	{
		if (showIfHidden && base.IsHidden)
		{
			Show();
		}
		webBrowser1.Navigate(urlString);
	}

	public override void SaveComponentSettings()
	{
		if (ConfigSettings == null)
		{
			ConfigSettings = new WebViewControlSettings(SettingsKey);
		}
		ConfigSettings.Url = webBrowser1.Url.ToString();
		ConfigSettings.Save();
	}

	public override void ResetComponentSettings()
	{
		if (ConfigSettings == null)
		{
			ConfigSettings = new WebViewControlSettings(SettingsKey);
		}
		ConfigSettings.Reload();
	}

	public override void LoadComponentSettings()
	{
		if (ConfigSettings == null)
		{
			ConfigSettings = new WebViewControlSettings(SettingsKey);
		}
		try
		{
			Navigate(ConfigSettings.Url, showIfHidden: false);
		}
		catch (Exception)
		{
		}
	}
}
