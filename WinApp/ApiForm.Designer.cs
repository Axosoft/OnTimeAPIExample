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
			this._Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.ItemsGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(93, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Most recent items:";
			// 
			// ItemsGridView
			// 
			this.ItemsGridView.AllowUserToAddRows = false;
			this.ItemsGridView.AllowUserToDeleteRows = false;
			this.ItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ItemsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._Name});
			this.ItemsGridView.Location = new System.Drawing.Point(12, 40);
			this.ItemsGridView.Name = "ItemsGridView";
			this.ItemsGridView.ReadOnly = true;
			this.ItemsGridView.Size = new System.Drawing.Size(844, 150);
			this.ItemsGridView.TabIndex = 1;
			// 
			// _Name
			// 
			this._Name.HeaderText = "Name";
			this._Name.Name = "_Name";
			this._Name.ReadOnly = true;
			this._Name.Width = 700;
			// 
			// ApiForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(873, 517);
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
		private System.Windows.Forms.DataGridViewTextBoxColumn _Name;
	}
}