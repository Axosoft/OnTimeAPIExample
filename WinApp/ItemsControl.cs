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

			ProjectComboBox.ComboBox.ValueMember = "id";
			ProjectComboBox.ComboBox.DisplayMember = "name";
			ProjectComboBox.ComboBox.DataSource = Projects;

			ItemsGridView.AutoGenerateColumns = false;
			ItemsGridView.DataSource = Items;
			ItemsGridView.CellEndEdit += new DataGridViewCellEventHandler(ItemsGridView_CellEndEdit);
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
			Items.Insert(0, new Item());
			DataGridViewCell newNameCell = ItemsGridView.Rows[0].Cells[0];
			ItemsGridView.CurrentCell = newNameCell;
			ItemsGridView.BeginEdit(true);
		}

		void ItemsGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			var newItemName = (string)ItemsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

			if(string.IsNullOrEmpty(newItemName))
			{
				// no name was entered - cancel add
				Items.RemoveAt(0);
			}
			else
			{
				var item = new Item
				{
					name = newItemName,
					project = new Project
					{
						id = (int)ProjectComboBox.ComboBox.SelectedValue
					}
				};

				OnTime.Post("defects", new { item = item });

				GetItems();
			}
		}

	}
}
