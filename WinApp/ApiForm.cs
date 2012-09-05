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
		OnTime OnTime;
		LoginForm LoginForm;
		BindingList<Project> Projects = new BindingList<Project>();
		BindingList<Item> Items = new BindingList<Item>();

		public ApiForm()
		{
			InitializeComponent();

			LoginForm = new LoginForm();
			LoginForm.FormClosed += new FormClosedEventHandler(loginForm_FormClosed);

			Shown += new EventHandler(ApiForm_Shown);

			ProjectComboBox.ValueMember = "id";
			ProjectComboBox.DisplayMember = "name";
			ProjectComboBox.DataSource = Projects;

			ItemsGridView.AutoGenerateColumns = false;
			ItemsGridView.DataSource = Items;
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
				OnTime = LoginForm.OnTime;
				LoginForm = null;
				GetItems();
				GetProjects();
			}
			else
				Close();
		}

		private void AddButton_Click(object sender, EventArgs e)
		{
			var item = new Item
			{
				name = NewItemName.Text,
				project = new Project
				{
					id = (int)ProjectComboBox.SelectedValue
				}
			};

			OnTime.Post("defects", new { item = item });

			GetItems();
		}

		#endregion
		
		void GetItems()
		{
			var result = OnTime.Get<DataResponse<Item>>("defects");
			var webClient = new WebClient();

			Items.Clear();
			foreach(var item in result.data)
				Items.Add(item);
		}

		void GetProjects()
		{
			var result = OnTime.Get<DataResponse<Project>>("projects");
			
			Projects.Clear();
			foreach(var project in result.data)
				Projects.Add(project);
		}
	}
}
