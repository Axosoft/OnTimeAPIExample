using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using AxosoftAPI.NET.Models;
using AxosoftAPI.NET;
using AxosoftAPI.NET.Helpers;

namespace WinApp
{
	public partial class ItemsControl : UserControl
	{
		Proxy AxosoftProxy;

		BindingList<Project> Projects = new BindingList<Project>();
		BindingList<Item> Items = new BindingList<Item>();

		public ItemsControl()
		{
			InitializeComponent();

			// configure project combo box in toolbar
			ProjectComboBox.ComboBox.ValueMember = "Id";
			ProjectComboBox.ComboBox.DisplayMember = "Name";
			ProjectComboBox.ComboBox.DataSource = Projects;
			ProjectComboBox.ComboBox.SelectedValueChanged += new EventHandler(ComboBox_SelectedValueChanged);

			// configure the items grid
			ItemsGridView.AutoGenerateColumns = false;
			ItemsGridView.DataSource = Items;
			ItemsGridView.CellEndEdit += new DataGridViewCellEventHandler(ItemsGridView_CellEndEdit);
			ItemsGridView.SelectionChanged += new EventHandler(ItemsGridView_SelectionChanged);

			System.Windows.Forms.Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
		}

		void Application_ApplicationExit(object sender, EventArgs e)
		{
			if (AxosoftProxy != null && string.IsNullOrWhiteSpace(AxosoftProxy.AccessToken))
			{
				AxosoftProxy.Get<object>("oauth2/revoke");
			}
		}

		public void SetAxosoftProxy(Proxy axosoftProxy)
		{
			AxosoftProxy = axosoftProxy;

			// set Axosoft host label
			AxosoftHostLabel.Text = new Uri(Program.Settings.Url).Host;

			GetProjects();
			if (Projects.Count == 0)
			{
				// there are no projects. ask the user if they want to create a new project.
				var dialogResult = MessageBox.Show(
					"Your Axosoft database has no projects. To use this example, you will need to create a project. Would you like to create a new project called \"API Example Project\" now?",
					"Create an Axosoft scrum project",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question);

				if (dialogResult == DialogResult.Yes)
				{
					var project = new Project
					{
						Name = "API Example Project"
					};

					AxosoftProxy.Projects.Create(project);
					GetProjects();
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
			var result = AxosoftProxy.Defects.Get(new Dictionary<string, object> {
				{ "project_id", ((Project)ProjectComboBox.SelectedItem).Id },
				{ "sort_fields", "id desc" },
				{ "page_size", 10 },
				{ "columns", "id,name,reported_date" }
			});

			Items.Clear();

			foreach (var item in result.Data)
			{
				Items.Add(item);
			}
		}

		void GetProjects()
		{
			var result = AxosoftProxy.Projects.Get();

			Projects.Clear();

			foreach (var project in result.Data)
			{
				Projects.Add(project);
			}
		}

		// called on grid selection change, enables / disables buttons as appropriate
		void ItemsGridView_SelectionChanged(object sender, EventArgs e)
		{
			var count = ItemsGridView.SelectedRows.Count;
			var newItemInSelection = ItemsGridView.SelectedRows.Cast<DataGridViewRow>().Any(row => row.Cells["id"].Value == null || (int)row.Cells["id"].Value == 0);
			DeleteButton.Enabled = !newItemInSelection && (count > 0); // enable delete if any items are selected (and none of them is the new item)
			AddAttachmentButton.Enabled = !newItemInSelection && count == 1; // only enable add attachment if one item is selected (and it's not the new item)
		}

		private void AddButton_Click(object sender, EventArgs e)
		{
			AddButton.Enabled = false;
			Items.Insert(0, new Item());
			DataGridViewCell newNameCell = ItemsGridView.Rows[0].Cells["name"];
			ItemsGridView.CurrentCell = newNameCell;
			ItemsGridView.BeginEdit(true);
		}

		private void ItemsGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			var newItemName = (string)ItemsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

			if (string.IsNullOrEmpty(newItemName))
			{
				// no name was entered - cancel add
				Items.RemoveAt(0);
			}
			else
			{
				var item = new Item
				{
					Name = newItemName,
					Project = new Project
					{
						Id = (int)ProjectComboBox.ComboBox.SelectedValue
					}
				};

				AxosoftProxy.Defects.Create(item);

				GetItems();
			}
			AddButton.Enabled = true;
		}

		private void DeleteButton_Click(object sender, EventArgs e)
		{
			var count = ItemsGridView.SelectedRows.Count;
			if (count > 0)
			{
				var dialogResult = MessageBox.Show(
					string.Format("This action is not reversible. Are you sure you want to delete {0} defect{1}?", count, count > 1 ? "s" : ""),
					"Delete confirmation",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Warning);

				if (dialogResult == DialogResult.Yes)
				{
					foreach (DataGridViewRow row in ItemsGridView.SelectedRows)
						AxosoftProxy.Defects.Delete((int)row.Cells["id"].Value);
					GetItems();
				}
			}
		}

		private void AddAttachment_Click(object sender, EventArgs e)
		{
			// have user select a file to attach
			var openFileDialog = new OpenFileDialog();

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				Stream stream;
				if ((stream = openFileDialog.OpenFile()) != null)
				{
					using (stream)
					{
						// get id of selected item
						var id = (int)ItemsGridView.CurrentRow.Cells["id"].Value;

						var attachment = new Attachment
						{
							Data = stream,
							FileName = Path.GetFileName(openFileDialog.FileName)
						};

						var result = AxosoftProxy.Defects.AddAttachment(id, attachment);

						if (!result.IsSuccessful)
						{
							throw new AxosoftAPIException<ErrorResponse>(new ErrorResponse
							{
								Message = result.ErrorMessage,
							});
						}
					}
				}
			}
		}

		private void ComboBox_SelectedValueChanged(object sender, EventArgs e)
		{
			GetItems();
		}
	}

	// This is used to POST an attachment, due to a bug in Axosoft that expects the content to be encoded this way
	class UTF8ByteEncoder : ICryptoTransform
	{
		public bool CanReuseTransform
		{
			get { return true; }
		}

		public bool CanTransformMultipleBlocks
		{
			get { return true; }
		}

		public int InputBlockSize
		{
			get { return 1; }
		}

		public int OutputBlockSize
		{
			get { return 2; }
		}

		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			var originalOutputOffset = outputOffset;
			for (var inputIndex = 0; inputIndex < inputCount; inputIndex++)
			{
				var b = inputBuffer[inputOffset + inputIndex];
				if ((b & 128) > 0)
				{
					outputBuffer[outputOffset++] = (byte)((b >> 6) | 0xc0);
					outputBuffer[outputOffset++] = (byte)((b & 0x3f) | 0x80);
				}
				else
					outputBuffer[outputOffset++] = b;
			}
			return outputOffset - originalOutputOffset;
		}

		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			var outputBuffer = new byte[inputBuffer.Length * 2];
			var outputCount = TransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, 0);
			return outputBuffer.Take(outputCount).ToArray();
		}

		public void Dispose()
		{
		}
	}
}
