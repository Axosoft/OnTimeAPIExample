using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OnTimeApi;
using System.Web;

namespace WinApp
{
	public partial class LoginForm : Form
	{
		public OnTime OnTime { get; private set; }

		public LoginForm()
		{
			InitializeComponent();

			AcceptButton = LoginButton;
		}

		private void LoginButton_Click(object sender, EventArgs e)
		{
			var OnTime = new OnTime(Program.Settings);
			try
			{
				OnTime.ObtainAccessToken(new Dictionary<string, string> {
					{ "grant_type", "password" },
					{ "username", LoginIdText.Text },
					{ "password", PasswordText.Text },
					{ "scope", "read write" }
				});

				this.OnTime = OnTime;
				Close();
			} catch (OnTimeException ex)
			{
				MessageBox.Show(
					"An error occurred when obtaining access token from OnTime: " + ex.Message,
					"Error obtaining access token",
					MessageBoxButtons.OK, 
					MessageBoxIcon.Error);
			}
		}

		
	}
}
