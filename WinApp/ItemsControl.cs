using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OnTimeApi;
using System.Net;

namespace WinApp
{
	public partial class ItemsControl : UserControl
	{
		OnTime OnTime;

		BindingList<Project> Projects = new BindingList<Project>();
		BindingList<Item> Items = new BindingList<Item>();

		public ItemsControl()
		{
			InitializeComponent();

			ProjectComboBox.ValueMember = "id";
			ProjectComboBox.DisplayMember = "name";
			ProjectComboBox.DataSource = Projects;

			ItemsGridView.AutoGenerateColumns = false;
			ItemsGridView.DataSource = Items;
		}

		public void SetOnTime(OnTime onTime)
		{
			OnTime = onTime;

			GetItems();
			GetProjects();
		}

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
	}
}
