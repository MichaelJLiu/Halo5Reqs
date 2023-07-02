using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Halo5Reqs.Api;

using Microsoft.Web.WebView2.Core;

namespace Halo5Reqs
{
	public partial class LoginForm : Form
	{
		public LoginForm()
		{
			this.InitializeComponent();
		}

		public MainForm MainForm { get; set; }

		private async void LoginForm_Load(Object sender, EventArgs e)
		{
			CoreWebView2EnvironmentOptions options =
				new CoreWebView2EnvironmentOptions
				{
					AllowSingleSignOnUsingOSPrimaryAccount = true,
				};
			CoreWebView2Environment environment = await CoreWebView2Environment.CreateAsync(null, Application.LocalUserAppDataPath, options);
			await this.webView.EnsureCoreWebView2Async(environment);
			this.webView.Source = new Uri("https://www.halowaypoint.com/halo-5-guardians/requisitions");
		}

		private async void webView_NavigationCompleted(Object sender, CoreWebView2NavigationCompletedEventArgs e)
		{
			if (this.webView.Source.AbsolutePath.StartsWith("/halo-5-guardians", StringComparison.Ordinal))
			{
				List<CoreWebView2Cookie> cookies = await this.webView.CoreWebView2.CookieManager.GetCookiesAsync("https://www.halowaypoint.com");
				CoreWebView2Cookie tokenCookie = cookies.Find(cookie => cookie.Name == "343-spartan-token");

				if (tokenCookie != null)
				{
					String token = Uri.UnescapeDataString(tokenCookie.Value);
					String gamertag = await new ProfileApiClient(token).GetGamertag();
					this.MainForm = new MainForm(token, gamertag);
					this.MainForm.Show();
					this.MainForm.Focus();
					this.DialogResult = DialogResult.OK;
				}
			}
		}
	}
}
