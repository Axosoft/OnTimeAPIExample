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
		public LoginForm()
		{
			InitializeComponent();
		}

		private void LoginButton_Click(object sender, EventArgs e)
		{
			var OnTime = new OnTime(Program.Settings);
			try
			{
				OnTime.ObtainAccessToken(string.Format("grant_type=password&username={0}&password={1}",
					HttpUtility.UrlEncode(LoginIdText.Text),
					HttpUtility.UrlEncode(PasswordText.Text)));
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
