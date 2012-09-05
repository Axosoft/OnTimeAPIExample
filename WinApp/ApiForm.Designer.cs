namespace WinApp
{
	partial class ApiForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.ItemsGridView = new System.Windows.Forms.DataGridView();
			this.AddButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.NewItemName = new System.Windows.Forms.TextBox();
			this.ProjectComboBox = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this._Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Project = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.ItemsGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Most recent defects:";
			// 
			// ItemsGridView
			// 
			this.ItemsGridView.AllowUserToAddRows = false;
			this.ItemsGridView.AllowUserToDeleteRows = false;
			this.ItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ItemsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._Name,
            this.Project});
			this.ItemsGridView.Location = new System.Drawing.Point(12, 40);
			this.ItemsGridView.Name = "ItemsGridView";
			this.ItemsGridView.ReadOnly = true;
			this.ItemsGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.ItemsGridView.Size = new System.Drawing.Size(744, 150);
			this.ItemsGridView.TabIndex = 1;
			// 
			// AddButton
			// 
			this.AddButton.Location = new System.Drawing.Point(681, 296);
			this.AddButton.Name = "AddButton";
			this.AddButton.Size = new System.Drawing.Size(75, 23);
			this.AddButton.TabIndex = 2;
			this.AddButton.Text = "Add";
			this.AddButton.UseVisualStyleBackColor = true;
			this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 226);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(94, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Add a new defect:";
			// 
			// NewItemName
			// 
			this.NewItemName.Location = new System.Drawing.Point(138, 243);
			this.NewItemName.Name = "NewItemName";
			this.NewItemName.Size = new System.Drawing.Size(618, 20);
			this.NewItemName.TabIndex = 4;
			// 
			// ProjectComboBox
			// 
			this.ProjectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ProjectComboBox.FormattingEnabled = true;
			this.ProjectComboBox.Location = new System.Drawing.Point(138, 269);
			this.ProjectComboBox.Name = "ProjectComboBox";
			this.ProjectComboBox.Size = new System.Drawing.Size(618, 21);
			this.ProjectComboBox.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 246);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Name";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(13, 272);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Project";
			// 
			// _Name
			// 
			this._Name.DataPropertyName = "name";
			this._Name.HeaderText = "Name";
			this._Name.Name = "_Name";
			this._Name.ReadOnly = true;
			this._Name.Width = 500;
			// 
			// Project
			// 
			this.Project.DataPropertyName = "project";
			this.Project.HeaderText = "Project";
			this.Project.Name = "Project";
			this.Project.ReadOnly = true;
			this.Project.Width = 200;
			// 
			// ApiForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(767, 331);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.ProjectComboBox);
			this.Controls.Add(this.NewItemName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.AddButton);
			this.Controls.Add(this.ItemsGridView);
			this.Controls.Add(this.label1);
			this.Name = "ApiForm";
			this.Text = "OnTime API Example";
			((System.ComponentModel.ISupportInitialize)(this.ItemsGridView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridView ItemsGridView;
		private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
		private System.Windows.Forms.Button AddButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox NewItemName;
		private System.Windows.Forms.ComboBox ProjectComboBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DataGridViewTextBoxColumn _Name;
		private System.Windows.Forms.DataGridViewTextBoxColumn Project;
	}
}