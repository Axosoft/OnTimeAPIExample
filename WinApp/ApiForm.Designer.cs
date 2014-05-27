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
			this.label5 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.loginControl = new WinApp.LoginControl();
			this.updateConfigControl = new WinApp.UpdateConfigControl();
			this.ItemsControl = new WinApp.ItemsControl();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.label5.Dock = System.Windows.Forms.DockStyle.Top;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
			this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.label5.Location = new System.Drawing.Point(0, 0);
			this.label5.Name = "label5";
			this.label5.Padding = new System.Windows.Forms.Padding(2, 10, 0, 0);
			this.label5.Size = new System.Drawing.Size(784, 55);
			this.label5.TabIndex = 8;
			this.label5.Text = "Axosoft Defects Explorer";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.loginControl);
			this.panel1.Controls.Add(this.updateConfigControl);
			this.panel1.Controls.Add(this.ItemsControl);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 55);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(784, 331);
			this.panel1.TabIndex = 10;
			// 
			// loginControl
			// 
			this.loginControl.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.loginControl.Location = new System.Drawing.Point(221, 91);
			this.loginControl.Name = "loginControl";
			this.loginControl.Size = new System.Drawing.Size(338, 150);
			this.loginControl.TabIndex = 11;
			this.loginControl.Visible = false;
			// 
			// updateConfigControl
			// 
			this.updateConfigControl.BackColor = System.Drawing.Color.White;
			this.updateConfigControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.updateConfigControl.Location = new System.Drawing.Point(0, 0);
			this.updateConfigControl.Name = "updateConfigControl";
			this.updateConfigControl.Size = new System.Drawing.Size(784, 331);
			this.updateConfigControl.TabIndex = 10;
			this.updateConfigControl.Visible = false;
			// 
			// ItemsControl
			// 
			this.ItemsControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ItemsControl.Location = new System.Drawing.Point(0, 0);
			this.ItemsControl.Name = "ItemsControl";
			this.ItemsControl.Size = new System.Drawing.Size(784, 331);
			this.ItemsControl.TabIndex = 9;
			this.ItemsControl.Visible = false;
			// 
			// ApiForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(784, 386);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label5);
			this.Name = "ApiForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Axosoft API Example";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label5;
		private ItemsControl ItemsControl;
		private System.Windows.Forms.Panel panel1;
		private UpdateConfigControl updateConfigControl;
		private LoginControl loginControl;
	}
}