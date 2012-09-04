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

namespace WinApp
{
	public partial class ApiForm : Form
	{
		OnTime OnTime;
		LoginForm LoginForm;

		public ApiForm()
		{
			InitializeComponent();

			LoginForm = new LoginForm();
			LoginForm.FormClosed += new FormClosedEventHandler(loginForm_FormClosed);

			Shown += new EventHandler(ApiForm_Shown);
		}

		void ApiForm_Shown(object sender, EventArgs e)
		{
			LoginForm.ShowDialog();			
		}

		void loginForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if(LoginForm.OnTime != null)
			{
				OnTime = LoginForm.OnTime;
				LoginForm = null;
				GetItems();
			}
			else
				Close();
		}

		void GetItems()
		{
			var webClient = new WebClient();

			var resultString = webClient.DownloadString(OnTime.GetUrl("defects"));

			var result = JsonConvert.DeserializeObject<DataResponse<Item>>(resultString);

			foreach(var item in result.data)
				ItemsGridView.Rows.Add(item.name);
		}

		private void AddButton_Click(object sender, EventArgs e)
		{
			var item = new Item
			{
				name = NewItemName.Text,
				project = new Project
				{
					id = 1
				}
			};

			var webClient = new WebClient();
			var encoding = new System.Text.UTF8Encoding();
			webClient.Encoding = encoding;
			webClient.Headers.Add("Content-Type","application/json");

			webClient.UploadData(OnTime.GetUrl("defects"), encoding.GetBytes(JsonConvert.SerializeObject(new { item = item })));
		}

	}
}
