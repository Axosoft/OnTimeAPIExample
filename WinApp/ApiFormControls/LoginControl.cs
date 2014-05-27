using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AxosoftAPI.NET;
using AxosoftAPI.NET.Helpers;
using AxosoftAPI.NET.Models;

namespace WinApp
{
	public partial class LoginControl : UserControl
	{
		public class LoginEventArgs : EventArgs
		{
			public Proxy AxosoftProxy;

			public LoginEventArgs(Proxy axosoftProxy)
			{
				AxosoftProxy = axosoftProxy;
			}
		}

		public Button AcceptButton { get { return LoginButton; } }

		public LoginControl()
		{
			InitializeComponent();
		}


		public event EventHandler<LoginEventArgs> LoggedIn;

		private void LoginButton_Click(object sender, EventArgs e)
		{
			var axosoftProxy = new Proxy
			{
				Url = Program.Settings.Url,
				ClientId = Program.Settings.ClientId,
				ClientSecret = Program.Settings.ClientSecret
			};

			try
			{
				axosoftProxy.ObtainAccessTokenFromUsernamePassword
				(
					username: LoginIdText.Text,
					password: PasswordText.Text,
					scope: ScopeEnum.ReadWrite
				);
			}
			catch (AxosoftAPIException<ErrorResponse> ex)
			{
				MessageBox.Show(
					"An error occurred when obtaining access token from Axosoft: " + ex.Message,
					"Error obtaining access token",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}

			if (!string.IsNullOrWhiteSpace(axosoftProxy.AccessToken))
			{
				LoggedIn(this, new LoginEventArgs(axosoftProxy));
			}
		}
	}
}
