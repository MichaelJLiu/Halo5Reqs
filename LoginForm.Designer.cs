﻿namespace Halo5Reqs
{
	partial class LoginForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.webView = new Microsoft.Web.WebView2.WinForms.WebView2();
			((System.ComponentModel.ISupportInitialize)(this.webView)).BeginInit();
			this.SuspendLayout();
			// 
			// webView
			// 
			this.webView.AllowExternalDrop = true;
			this.webView.CreationProperties = null;
			this.webView.DefaultBackgroundColor = System.Drawing.Color.White;
			this.webView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webView.Location = new System.Drawing.Point(0, 0);
			this.webView.Name = "webView";
			this.webView.Size = new System.Drawing.Size(800, 450);
			this.webView.TabIndex = 0;
			this.webView.ZoomFactor = 1D;
			this.webView.NavigationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs>(this.webView_NavigationCompleted);
			// 
			// LoginForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.webView);
			this.Name = "LoginForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Sign In to Halo Waypoint";
			this.Load += new System.EventHandler(this.LoginForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.webView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Microsoft.Web.WebView2.WinForms.WebView2 webView;
	}
}