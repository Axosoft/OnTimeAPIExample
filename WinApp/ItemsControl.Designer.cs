namespace WinApp
{
	partial class ItemsControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.Label label1;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemsControl));
			System.Windows.Forms.ToolStrip toolStrip2;
			this.ItemsGridView = new System.Windows.Forms.DataGridView();
			this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.project = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.AddButton = new System.Windows.Forms.ToolStripButton();
			this.DeleteButton = new System.Windows.Forms.ToolStripButton();
			this.OnTimeHostLabel = new System.Windows.Forms.Label();
			this.ProjectComboBox = new System.Windows.Forms.ToolStripComboBox();
			label1 = new System.Windows.Forms.Label();
			toolStrip2 = new System.Windows.Forms.ToolStrip();
			((System.ComponentModel.ISupportInitialize)(this.ItemsGridView)).BeginInit();
			this.toolStrip1.SuspendLayout();
			toolStrip2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(18, 20);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(113, 13);
			label1.TabIndex = 8;
			label1.Text = "Displaying Defects for:";
			// 
			// ItemsGridView
			// 
			this.ItemsGridView.AllowUserToAddRows = false;
			this.ItemsGridView.AllowUserToDeleteRows = false;
			this.ItemsGridView.AllowUserToResizeColumns = false;
			this.ItemsGridView.AllowUserToResizeRows = false;
			this.ItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ItemsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.name,
            this.project});
			this.ItemsGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.ItemsGridView.Location = new System.Drawing.Point(21, 69);
			this.ItemsGridView.Name = "ItemsGridView";
			this.ItemsGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.ItemsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.ItemsGridView.Size = new System.Drawing.Size(744, 240);
			this.ItemsGridView.TabIndex = 9;
			// 
			// id
			// 
			this.id.DataPropertyName = "id";
			this.id.HeaderText = "ID";
			this.id.Name = "id";
			this.id.Width = 50;
			// 
			// name
			// 
			this.name.DataPropertyName = "name";
			this.name.HeaderText = "Name";
			this.name.Name = "name";
			this.name.Width = 500;
			// 
			// project
			// 
			this.project.DataPropertyName = "project";
			this.project.HeaderText = "Project";
			this.project.Name = "project";
			this.project.Width = 200;
			// 
			// toolStrip1
			// 
			this.toolStrip1.AutoSize = false;
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddButton,
            this.DeleteButton});
			this.toolStrip1.Location = new System.Drawing.Point(21, 41);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(128, 25);
			this.toolStrip1.TabIndex = 16;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// AddButton
			// 
			this.AddButton.Image = ((System.Drawing.Image)(resources.GetObject("AddButton.Image")));
			this.AddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AddButton.Name = "AddButton";
			this.AddButton.Size = new System.Drawing.Size(49, 22);
			this.AddButton.Text = "Add";
			this.AddButton.ToolTipText = "Add";
			this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
			// 
			// DeleteButton
			// 
			this.DeleteButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteButton.Image")));
			this.DeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.DeleteButton.Name = "DeleteButton";
			this.DeleteButton.Size = new System.Drawing.Size(60, 22);
			this.DeleteButton.Text = "Delete";
			this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			// 
			// OnTimeHostLabel
			// 
			this.OnTimeHostLabel.AutoSize = true;
			this.OnTimeHostLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.OnTimeHostLabel.Location = new System.Drawing.Point(133, 20);
			this.OnTimeHostLabel.Name = "OnTimeHostLabel";
			this.OnTimeHostLabel.Size = new System.Drawing.Size(0, 13);
			this.OnTimeHostLabel.TabIndex = 17;
			// 
			// toolStrip2
			// 
			toolStrip2.Anchor = System.Windows.Forms.AnchorStyles.None;
			toolStrip2.AutoSize = false;
			toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
			toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProjectComboBox});
			toolStrip2.Location = new System.Drawing.Point(149, 41);
			toolStrip2.Name = "toolStrip2";
			toolStrip2.Size = new System.Drawing.Size(616, 25);
			toolStrip2.TabIndex = 18;
			toolStrip2.Text = "toolStrip2";
			// 
			// ProjectComboBox
			// 
			this.ProjectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ProjectComboBox.Name = "ProjectComboBox";
			this.ProjectComboBox.Size = new System.Drawing.Size(121, 25);
			// 
			// ItemsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(toolStrip2);
			this.Controls.Add(this.OnTimeHostLabel);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.ItemsGridView);
			this.Controls.Add(label1);
			this.Name = "ItemsControl";
			this.Size = new System.Drawing.Size(787, 334);
			((System.ComponentModel.ISupportInitialize)(this.ItemsGridView)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			toolStrip2.ResumeLayout(false);
			toolStrip2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView ItemsGridView;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton AddButton;
		private System.Windows.Forms.ToolStripButton DeleteButton;
		private System.Windows.Forms.DataGridViewTextBoxColumn id;
		private System.Windows.Forms.DataGridViewTextBoxColumn name;
		private System.Windows.Forms.DataGridViewTextBoxColumn project;
		private System.Windows.Forms.Label OnTimeHostLabel;
		private System.Windows.Forms.ToolStripComboBox ProjectComboBox;
	}
}
