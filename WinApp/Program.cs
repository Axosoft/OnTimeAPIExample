using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OnTimeApi;
using System.Configuration;

namespace WinApp
{
	static class Program
	{
		public static Settings Settings { get; private set; }

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// Initialize Settings from App.config
			Settings = new Settings(
				onTimeUrl: ConfigurationManager.AppSettings.Get("OnTimeUrl"),
				clientId: ConfigurationManager.AppSettings.Get("ClientId"),
				clientSecret: ConfigurationManager.AppSettings.Get("ClientSecret")
			);

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new LoginForm());
		}


	}
}
