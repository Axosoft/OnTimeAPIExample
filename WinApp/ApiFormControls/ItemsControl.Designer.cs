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
			System.Windows.Forms.ToolStrip toolStrip2;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemsControl));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.ProjectComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.ItemsGridView = new System.Windows.Forms.DataGridView();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.AddButton = new System.Windows.Forms.ToolStripButton();
			this.DeleteButton = new System.Windows.Forms.ToolStripButton();
			this.AddAttachmentButton = new System.Windows.Forms.ToolStripButton();
			this.AxosoftHostLabel = new System.Windows.Forms.Label();
			this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.reportedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			label1 = new System.Windows.Forms.Label();
			toolStrip2 = new System.Windows.Forms.ToolStrip();
			toolStrip2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ItemsGridView)).BeginInit();
			this.toolStrip1.SuspendLayout();
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
			// toolStrip2
			// 
			toolStrip2.AutoSize = false;
			toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
			toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProjectComboBox});
			toolStrip2.Location = new System.Drawing.Point(295, 41);
			toolStrip2.Name = "toolStrip2";
			toolStrip2.Size = new System.Drawing.Size(470, 25);
			toolStrip2.TabIndex = 18;
			toolStrip2.Text = "toolStrip2";
			// 
			// ProjectComboBox
			// 
			this.ProjectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ProjectComboBox.Name = "ProjectComboBox";
			this.ProjectComboBox.Size = new System.Drawing.Size(121, 25);
			// 
			// ItemsGridView
			// 
			this.ItemsGridView.AllowUserToAddRows = false;
			this.ItemsGridView.AllowUserToDeleteRows = false;
			this.ItemsGridView.AllowUserToResizeColumns = false;
			this.ItemsGridView.AllowUserToResizeRows = false;
			this.ItemsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ItemsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.name,
            this.reportedDate});
			this.ItemsGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.ItemsGridView.Location = new System.Drawing.Point(21, 69);
			this.ItemsGridView.Name = "ItemsGridView";
			this.ItemsGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.ItemsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.ItemsGridView.Size = new System.Drawing.Size(744, 240);
			this.ItemsGridView.TabIndex = 9;
			// 
			// toolStrip1
			// 
			this.toolStrip1.AutoSize = false;
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddButton,
            this.DeleteButton,
            this.AddAttachmentButton});
			this.toolStrip1.Location = new System.Drawing.Point(21, 41);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(274, 25);
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
			this.DeleteButton.Enabled = false;
			this.DeleteButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteButton.Image")));
			this.DeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.DeleteButton.Name = "DeleteButton";
			this.DeleteButton.Size = new System.Drawing.Size(60, 22);
			this.DeleteButton.Text = "Delete";
			this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			// 
			// AddAttachmentButton
			// 
			this.AddAttachmentButton.Enabled = false;
			this.AddAttachmentButton.Image = ((System.Drawing.Image)(resources.GetObject("AddAttachmentButton.Image")));
			this.AddAttachmentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AddAttachmentButton.Name = "AddAttachmentButton";
			this.AddAttachmentButton.Size = new System.Drawing.Size(115, 22);
			this.AddAttachmentButton.Text = "Add Attachment";
			this.AddAttachmentButton.Click += new System.EventHandler(this.AddAttachment_Click);
			// 
			// AxosoftHostLabel
			// 
			this.AxosoftHostLabel.AutoSize = true;
			this.AxosoftHostLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.AxosoftHostLabel.Location = new System.Drawing.Point(133, 20);
			this.AxosoftHostLabel.Name = "AxosoftHostLabel";
			this.AxosoftHostLabel.Size = new System.Drawing.Size(0, 13);
			this.AxosoftHostLabel.TabIndex = 17;
			// 
			// id
			// 
			this.id.DataPropertyName = "Id";
			this.id.HeaderText = "ID";
			this.id.Name = "id";
			this.id.Width = 50;
			// 
			// name
			// 
			this.name.DataPropertyName = "Name";
			this.name.HeaderText = "Name";
			this.name.Name = "name";
			this.name.Width = 450;
			// 
			// reportedDate
			// 
			this.reportedDate.DataPropertyName = "ReportedDate";
			dataGridViewCellStyle1.Format = "MM/dd/yyyy";
			this.reportedDate.DefaultCellStyle = dataGridViewCellStyle1;
			this.reportedDate.HeaderText = "Reported Date";
			this.reportedDate.Name = "reportedDate";
			this.reportedDate.Width = 200;
			// 
			// ItemsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(toolStrip2);
			this.Controls.Add(this.AxosoftHostLabel);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.ItemsGridView);
			this.Controls.Add(label1);
			this.Name = "ItemsControl";
			this.Size = new System.Drawing.Size(787, 334);
			toolStrip2.ResumeLayout(false);
			toolStrip2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ItemsGridView)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView ItemsGridView;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton AddButton;
		private System.Windows.Forms.ToolStripButton DeleteButton;
		private System.Windows.Forms.Label AxosoftHostLabel;
		private System.Windows.Forms.ToolStripComboBox ProjectComboBox;
		private System.Windows.Forms.ToolStripButton AddAttachmentButton;
		private System.Windows.Forms.DataGridViewTextBoxColumn id;
		private System.Windows.Forms.DataGridViewTextBoxColumn name;
		private System.Windows.Forms.DataGridViewTextBoxColumn reportedDate;
	}
}
