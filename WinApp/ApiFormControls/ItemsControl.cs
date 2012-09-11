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

			// configure project combo box in toolbar
			ProjectComboBox.ComboBox.ValueMember = "id";
			ProjectComboBox.ComboBox.DisplayMember = "name";
			ProjectComboBox.ComboBox.DataSource = Projects;
			ProjectComboBox.ComboBox.SelectedValueChanged += new EventHandler(ComboBox_SelectedValueChanged);

			// configure the items grid
			ItemsGridView.AutoGenerateColumns = false;
			ItemsGridView.DataSource = Items;
			ItemsGridView.CellEndEdit += new DataGridViewCellEventHandler(ItemsGridView_CellEndEdit);
		}

		public void SetOnTime(OnTime onTime)
		{
			OnTime = onTime;

			// set OnTime host label
			OnTimeHostLabel.Text = new Uri(OnTime.Settings.OnTimeUrl).Host;

			GetProjects();
			if(Projects.Count == 0)
			{
				// there are no projects. ask the user if they want to create a new project.
				var dialogResult = MessageBox.Show(
					"Your OnTime database has no projects. To use this example, you will need to create a project. Would you like to create a new project called \"API Example Project\" now?",
					"Create an OnTime project",
					MessageBoxButtons.YesNo, 
					MessageBoxIcon.Question);

				if(dialogResult == DialogResult.Yes)
				{
					var project = new Project
					{
						name = "API Example Project"
					};

					OnTime.Post("projects", project);
				}
				else
				{
					Application.Exit();
					return;
				}
			}
			GetItems();
		}

		void GetItems()
		{
			var result = OnTime.Get<DataResponse<Item>>("defects", new Dictionary<string, object> {
				{ "project_id", ((Project)ProjectComboBox.SelectedItem).id },
				{ "sort_fields", "id desc" },
				{ "page_size", 10 },
				{ "columns", "id,name,project" }
			});

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
			DataGridViewCell newNameCell = ItemsGridView.Rows[0].Cells["name"];
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

		private void DeleteButton_Click(object sender, EventArgs e)
		{
			if(ItemsGridView.SelectedRows.Count > 0)
			{
				foreach(DataGridViewRow row in ItemsGridView.SelectedRows)
				{
					OnTime.Delete("defects", (int)row.Cells["id"].Value);
				}
				GetItems();
			}
		}

		void ComboBox_SelectedValueChanged(object sender, EventArgs e)
		{
			GetItems();
		}


	}
}
