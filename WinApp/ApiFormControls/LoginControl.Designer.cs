namespace WinApp
{
	partial class LoginControl
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
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.LoginButton = new System.Windows.Forms.Button();
			this.PasswordText = new System.Windows.Forms.TextBox();
			this.LoginIdText = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 11);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(322, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "Please log in with your OnTime credentials to retrieve your Defects.";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 73);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "Password";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 47);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Login ID";
			// 
			// LoginButton
			// 
			this.LoginButton.Location = new System.Drawing.Point(248, 114);
			this.LoginButton.Name = "LoginButton";
			this.LoginButton.Size = new System.Drawing.Size(75, 23);
			this.LoginButton.TabIndex = 8;
			this.LoginButton.Text = "Log in";
			this.LoginButton.UseVisualStyleBackColor = true;
			this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
			// 
			// PasswordText
			// 
			this.PasswordText.Location = new System.Drawing.Point(96, 70);
			this.PasswordText.Name = "PasswordText";
			this.PasswordText.PasswordChar = '*';
			this.PasswordText.Size = new System.Drawing.Size(227, 20);
			this.PasswordText.TabIndex = 7;
			// 
			// LoginIdText
			// 
			this.LoginIdText.Location = new System.Drawing.Point(96, 44);
			this.LoginIdText.Name = "LoginIdText";
			this.LoginIdText.Size = new System.Drawing.Size(227, 20);
			this.LoginIdText.TabIndex = 6;
			// 
			// LoginControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.LoginButton);
			this.Controls.Add(this.PasswordText);
			this.Controls.Add(this.LoginIdText);
			this.Name = "LoginControl";
			this.Size = new System.Drawing.Size(333, 150);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button LoginButton;
		private System.Windows.Forms.TextBox PasswordText;
		private System.Windows.Forms.TextBox LoginIdText;
	}
}
