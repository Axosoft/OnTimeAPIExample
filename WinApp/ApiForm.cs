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
		LoginForm LoginForm;

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
				LoginForm = new LoginForm();
				LoginForm.FormClosed += new FormClosedEventHandler(loginForm_FormClosed);

				Shown += new EventHandler(ApiForm_Shown);
			}
		}

		#region Event handlers

		void ApiForm_Shown(object sender, EventArgs e)
		{
			LoginForm.ShowDialog();			
		}

		void loginForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if(LoginForm.OnTime != null)
			{
				ItemsControl.SetOnTime(LoginForm.OnTime);
				ItemsControl.Visible = true;
				LoginForm = null;
			}
			else
				Close();
		}

		#endregion
		

	}
}
