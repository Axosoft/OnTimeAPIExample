using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using AxosoftAPI.NET.Models;
using AxosoftAPI.NET.Helpers;
using WinApp.Helpers;

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
				url: ConfigurationManager.AppSettings.Get("AxosoftAPI:Url"),
				clientId: ConfigurationManager.AppSettings.Get("AxosoftAPI:ClientId"),
				clientSecret: ConfigurationManager.AppSettings.Get("AxosoftAPI:ClientSecret")
			);

			// Set up exception handling
			Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

			// set up SSL certificate warning callback
			System.Net.ServicePointManager.ServerCertificateValidationCallback = ServerCertificateValidation;

			// set up and start form
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new ApiForm());
		}

		static bool ServerCertificateValidation(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			// accept certificate if there are no errors or if the user ignores the errors
			return
				sslPolicyErrors == SslPolicyErrors.None
				||
				DialogResult.Yes == MessageBox.Show("There is a problem with the server's SSL certificate.  Do you want to ignore the SSL errors?", "SSL certificate error", MessageBoxButtons.YesNo);
		}

		#region Exception Handling

		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			if(e.ExceptionObject is AxosoftAPIException<ErrorResponse>)
				HandleUnhandledException((AxosoftAPIException<ErrorResponse>)e.ExceptionObject);
		}

		static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			if (e.Exception is AxosoftAPIException<ErrorResponse>)
				HandleUnhandledException((AxosoftAPIException<ErrorResponse>)e.Exception);
		}

		static void HandleUnhandledException(AxosoftAPIException<ErrorResponse> e)
		{
			MessageBox.Show(
				"An error occurred when accessing Axosoft: " + e.Message,
				"Error accessing Axosoft API",
				MessageBoxButtons.OK, 
				MessageBoxIcon.Error);
		}

		#endregion
	}
}
