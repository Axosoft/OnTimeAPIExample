using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OnTimeApi;
using System.Net;
using Newtonsoft.Json;
using System.Collections;

namespace WinApp
{
	public partial class ApiForm : Form
	{
		public ApiForm()
		{
			InitializeComponent();

			// check to see if the ontime base URL, client id, and client secret have been entered into settings.
			var settings = Program.Settings;
			if(settings.OnTimeUrl == "https://someaccount.ontimenow.com/" ||
				settings.ClientId == "00000000-0000-0000-0000-000000000000" ||
				settings.ClientSecret == "00000000-0000-0000-0000-000000000000")
			{
				// they have not been entered - show the instructions
				updateConfigControl.Visible = true;
			}
			else
			{
				// they have been entered - allow the end user to log in
				loginControl.Visible = true;
				loginControl.LoggedIn += new EventHandler<LoginControl.LoginEventArgs>(loginControl_LoggedIn);
				AcceptButton = loginControl.AcceptButton;
			}
		}

		void loginControl_LoggedIn(object sender, LoginControl.LoginEventArgs e)
		{
			loginControl.Visible = false;
			ItemsControl.SetOnTime(e.OnTime);
			ItemsControl.Visible = true;
		}

		#region Event handlers


		void loginForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			//if(LoginForm.OnTime != null)
			//{
			//    ItemsControl.SetOnTime(LoginForm.OnTime);
			//    ItemsControl.Visible = true;
			//    LoginForm = null;
			//}
			//else
			//    Close();
		}

		#endregion
		

	}
}
