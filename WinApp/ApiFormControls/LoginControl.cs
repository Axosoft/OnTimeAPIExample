using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OnTimeApi;

namespace WinApp
{
	public partial class LoginControl : UserControl
	{
		public class LoginEventArgs : EventArgs
		{
			public OnTime OnTime;

			public LoginEventArgs(OnTime onTime)
			{
				OnTime = onTime;
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
			var OnTime = new OnTime(Program.Settings);
			try
			{
				OnTime.ObtainAccessTokenFromUsernamePassword(
					username: LoginIdText.Text,
					password: PasswordText.Text,
					scope: "read write"
				);
			} catch (OnTimeException ex)
			{
				MessageBox.Show(
					"An error occurred when obtaining access token from OnTime: " + ex.Message,
					"Error obtaining access token",
					MessageBoxButtons.OK, 
					MessageBoxIcon.Error);
			}

			if(OnTime.HasAccessToken())
				LoggedIn(this, new LoginEventArgs(OnTime));
		}
	}
}
