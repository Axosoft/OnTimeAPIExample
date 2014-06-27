using System;
using System.Collections.Generic;
using System.Linq;

using AxosoftAPI.NET;
using AxosoftAPI.NET.Models;

namespace ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			// TODO: Update the Url, Client Id, Client Secret, Username, & Password values below

			#region Create/configure Proxy
			
			// Create a new Axosoft client object
			var axosoftClient = new Proxy
			{
				// Axosoft instance specific values
				Url = "https://someaccount.axosoft.com/",
				ClientId = "00000000-0000-0000-0000-000000000000",
				ClientSecret = "00000000-0000-0000-0000-000000000000",
			};

			#endregion

			#region Authentication [using username/password in this example]

			// We must authenticate against Axosoft
			axosoftClient.ObtainAccessTokenFromUsernamePassword("admin", "admin", ScopeEnum.ReadWrite);

			// Once authenticated we can query Axosoft
			if (string.IsNullOrWhiteSpace(axosoftClient.AccessToken))
			{
				Console.WriteLine("Unable to authenticate against Axosoft.");

				// Wait for input before closing the console
				Console.WriteLine("Press any key to close the console.");
				Console.ReadKey(true);

				return;
			}
			
			#endregion

			#region Example 1

			// Example 1: we can get all projects
			var projectsResult = axosoftClient.Projects.Get();

			if (!projectsResult.IsSuccessful)
			{
				// Wait for input before closing the console
				Console.WriteLine("Unable to get projects. We're done here!");
				Console.ReadKey(true);

				return;
			}

			Console.WriteLine("Example 1 -> Projects:");

			foreach (var project in projectsResult.Data)
			{
				Console.WriteLine(string.Format("Project Id: {0} - Name: {1}", project.Id, project.Name));
			}

			Console.WriteLine();

			#endregion

			#region Example 2

			// Example 2: we can get a single project by id (this can also be done for items, worklogs, etc.)
			var project1 = projectsResult.Data.FirstOrDefault(x => x.Id.HasValue);

			if (project1 == null)
			{
				// Wait for input before closing the console
				Console.WriteLine("Unable to get one project. We're done here!");
				Console.ReadKey(true);

				return;
			}

			project1 = axosoftClient.Projects.Get(project1.Id.Value).Data;

			Console.WriteLine("Example 2 -> Project by Id:");

			Console.WriteLine(string.Format("Project Id: {0} - Description: {1}", project1.Id, project1.Description));

			Console.WriteLine();

			#endregion

			#region Example 3

			// Example 3: we can get items using filters (all items created today)
			// Additional pre-defined date filter values are: yesterday,last_week,this_week,last10_days,last30_days
			var featuresResult = axosoftClient.Features.Get(new Dictionary<string, object>
			{
				{ "filters", "created_date_time=today" }
			});

			if (!featuresResult.IsSuccessful)
			{
				// Wait for input before closing the console
				Console.WriteLine("Unable to get feature items. We're done here!");
				Console.ReadKey(true);

				return;
			}

			Console.WriteLine("Example 3 -> Query items using filters (all features created today):");

			foreach (var feature in featuresResult.Data)
			{
				Console.WriteLine(string.Format("Feature Id: {0} - Name: {1}", feature.Id, feature.Name));
			}

			Console.WriteLine();

			#endregion

			#region Example 4

			// Example 4: we can create a new project
			Console.WriteLine("Example 4 -> Create project:");

			var project2Result = axosoftClient.Projects.Create(new Project
			{
				Name = string.Format("Test Project API"),
				Description = string.Format("Created on: {0}", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")),
			});

			if (!project2Result.IsSuccessful)
			{
				// Wait for input before closing the console
				Console.WriteLine("Unable to create a new project. We're done here!");
				Console.ReadKey(true);

				return;
			}

			Console.WriteLine(string.Format("Project created; Id: {0}", project2Result.Data.Id));

			Console.WriteLine();

			#endregion

			#region Example 5

			// Example 5: we can create new items
			Console.WriteLine("Example 5 -> Create items:");

			var feature1Result = axosoftClient.Features.Create(new Item
			{
				Name = "Item 1",
				Description = "This is item 1",
				DueDate = DateTime.Today,
				PubliclyViewable = true,
				Project = project2Result.Data,
			});

			if (!feature1Result.IsSuccessful)
			{
				// Wait for input before closing the console
				Console.WriteLine("Unable to create a new feature. We're done here!");
				Console.ReadKey(true);

				return;
			}

			Console.WriteLine(string.Format("Feature created; Id {0}", feature1Result.Data.Id));

			var defect1Result = axosoftClient.Defects.Create(new Item
			{
				Name = "Item 2",
				Description = "This is item 2",
				Project = project2Result.Data,
			});

			if (!defect1Result.IsSuccessful)
			{
				// Wait for input before closing the console
				Console.WriteLine("Unable to create a new defect. We're done here!");
				Console.ReadKey(true);

				return;
			}

			Console.WriteLine(string.Format("Defect created; Id {0}", defect1Result.Data.Id));

			Console.WriteLine();

			#endregion

			#region Example 6

			Console.WriteLine("Example 6 -> Update an item:");

			// Create new object to test reseting  all values and updating only specific properties
			var feature1UpdateResult = axosoftClient.Features.Update(new Item
			{
				Id = feature1Result.Data.Id,
				Description = "Changed item description!!!",
			});

			Console.WriteLine(string.Format("Feature update; Id {0}", feature1Result.Data.Id));

			Console.WriteLine();

			#endregion

			#region Example 7

			// Example 7: we can delete an item
			Console.WriteLine("Example 7 -> Delete item:");

			var deleteDefect1Result = axosoftClient.Defects.Delete(defect1Result.Data.Id.Value, new Dictionary<string, object>
			{
				{ "delete_items", true }
			});

			if (!deleteDefect1Result.IsSuccessful)
			{
				// Wait for input before closing the console
				Console.WriteLine("Unable to delete a defect. We're done here!");
				Console.ReadKey(true);

				return;
			}

			Console.WriteLine(string.Format("Defect deleted; Id {0}", defect1Result.Data.Id));

			Console.WriteLine();

			#endregion

			#region Example 8

			// Example 8: we can delete a project (and all of it's items)
			Console.WriteLine("Example 8 -> Delete project:");

			var deleteProject2Result = axosoftClient.Projects.Delete(project2Result.Data.Id.Value, new Dictionary<string, object>
			{
				{ "delete_items", true }
			});

			if (!deleteProject2Result.IsSuccessful)
			{
				// Wait for input before closing the console
				Console.WriteLine("Unable to delete a project. We're done here!");
				Console.ReadKey(true);

				return;
			}

			Console.WriteLine(string.Format("Project deleted; Id {0}", project2Result.Data.Id));

			Console.WriteLine();

			#endregion

			#region Example 9

			// Example 9: we can get all releases (this can also be done for items, worklogs, etc.)
			var releasesResult = axosoftClient.Releases.Get();

			if (!releasesResult.IsSuccessful)
			{
				// Wait for input before closing the console
				Console.WriteLine("Unable to get releases. We're done here!");
				Console.ReadKey(true);

				return;
			}

			Console.WriteLine("Example 9 -> Releases:");

			foreach (var release in releasesResult.Data)
			{
				Console.WriteLine(string.Format("Release Id: {0} - Name: {1}", release.Id, release.Name));
			}

			Console.WriteLine();

			#endregion

			#region Example 10

			// Example 10: we can get current user
			var meResult = axosoftClient.Me.Get();

			if (!meResult.IsSuccessful)
			{
				// Wait for input before closing the console
				Console.WriteLine("Unable to get current user. We're done here!");
				Console.ReadKey(true);

				return;
			}

			Console.WriteLine("Example 10 -> Current user:");

			Console.WriteLine(string.Format("Id: {0} - Login Id: {1}", meResult.Data.Id, meResult.Data.LoginId));

			Console.WriteLine();

			#endregion

			#region Example 11

			// Example 11: we can get all customers
			var customersRequest = axosoftClient.Customers.Get();

			if (!customersRequest.IsSuccessful)
			{
				// Wait for input before closing the console
				Console.WriteLine("Unable to get customers. We're done here!");
				Console.ReadKey(true);

				return;
			}

			Console.WriteLine("Example 11 -> All customers:");

			foreach (var customer in customersRequest.Data)
			{
				Console.WriteLine(string.Format("Customer Id: {0} - Name: {1}. Custom Fields: {2}", customer.Id, customer.CompanyName, customer.CustomFields == null ? 0 : customer.CustomFields.Count()));
			}

			Console.WriteLine();

			#endregion

			#region Example 12

			// Example 12: we can get first customer
			var customer1 = customersRequest.Data.FirstOrDefault(x => x.Id.HasValue);

			if (customer1 == null)
			{
				// Wait for input before closing the console
				Console.WriteLine("Unable to get one customer. We're done here!");
				Console.ReadKey(true);

				return;
			}

			customer1 = axosoftClient.Customers.Get(customer1.Id.Value, new Dictionary<string, object>
			{
				{ "extend", "custom_fields" }
			}).Data;

			Console.WriteLine("Example 12 -> Customer by Id:");

			Console.WriteLine(string.Format("Customer Id: {0} - Cutom Fields count: {1}", customer1.Id, customer1.CustomFields == null ? 0 : customer1.CustomFields.Count()));

			Console.WriteLine();

			#endregion

			#region Example 13

			// Example 13: we can create a new customer
			Console.WriteLine("Example 13 -> Create customer:");

			var customer2Result = axosoftClient.Customers.Create(new Customer
			{
				CompanyName = "Test Company",
				CompanyUrl = "www.axosoft.com",
				CustomFields = new Dictionary<string, object>
				{
					{ "custom_165", "test" },
					{ "custom_166", 666 }
				}
			});

			if (!customer2Result.IsSuccessful)
			{
				// Wait for input before closing the console
				Console.WriteLine("Unable to create a new customer. We're done here!");
				Console.ReadKey(true);

				return;
			}

			Console.WriteLine(string.Format("Customer created; Id: {0}", customer2Result.Data.Id));

			Console.WriteLine();

			#endregion

			#region Example 14

			// Example 14: we can delete a customer
			Console.WriteLine("Example 14 -> Delete customer:");

			var deleteCustomer1Result = axosoftClient.Customers.Delete(customer2Result.Data.Id.Value);

			if (!deleteCustomer1Result.IsSuccessful)
			{
				// Wait for input before closing the console
				Console.WriteLine("Unable to delete a customer. We're done here!");
				Console.ReadKey(true);

				return;
			}

			Console.WriteLine(string.Format("Customer deleted; Id {0}", customer2Result.Data.Id));

			Console.WriteLine();

			#endregion

			#region Example 15

			Console.WriteLine("Example 15 -> Get System Options Settings:");

			var systemOptions = axosoftClient.Get<Response<SystemOptions>>("settings/system_options");

			Console.WriteLine(string.Format("Did we get anything: {0}", systemOptions.Data != null));

			Console.WriteLine();

			#endregion

			#region Example 16

			Console.WriteLine("Example 16 -> Get all defects:");

			var allDefects = axosoftClient.Defects.Get(new Dictionary<string, object> { { "columns", "id" } });

			if (!allDefects.IsSuccessful)
			{
				// Wait for input before closing the console
				Console.WriteLine("Unable to get all defects. We're done here!");
				Console.ReadKey(true);

				return;
			}

			Console.WriteLine(string.Format("Total number of defects {0}", allDefects.Data.Count()));

			Console.WriteLine();

			#endregion

			#region Example 17

			Console.WriteLine("Example 17 -> Get all features:");

			var allFeatures = axosoftClient.Features.Get(new Dictionary<string, object> { { "columns", "id" } });

			if (!allFeatures.IsSuccessful)
			{
				// Wait for input before closing the console
				Console.WriteLine("Unable to get all defects. We're done here!");
				Console.ReadKey(true);

				return;
			}

			Console.WriteLine(string.Format("Total number of features {0}", allFeatures.Data.Count()));

			Console.WriteLine();

			#endregion

			#region Example 18

			Console.WriteLine("Example 18 -> Get all Incidents:");

			var allIncidents = axosoftClient.Incidents.Get(new Dictionary<string, object> { { "columns", "id" } });

			if (!allIncidents.IsSuccessful)
			{
				// Wait for input before closing the console
				Console.WriteLine("Unable to get all defects. We're done here!");
				Console.ReadKey(true);

				return;
			}

			Console.WriteLine(string.Format("Total number of incidents {0}", allIncidents.Data.Count()));

			Console.WriteLine();

			#endregion

			#region Example 19

			Console.WriteLine("Example 18 -> Get all Tasks:");

			var allTasks = axosoftClient.Tasks.Get(new Dictionary<string, object> { { "columns", "id" } });

			if (!allTasks.IsSuccessful)
			{
				// Wait for input before closing the console
				Console.WriteLine("Unable to get all defects. We're done here!");
				Console.ReadKey(true);

				return;
			}

			Console.WriteLine(string.Format("Total number of tasks {0}", allTasks.Data.Count()));

			Console.WriteLine();

			#endregion

			// We're done!!!
			Console.WriteLine("We're done!!!");

			// Wait for input before closing the console
			Console.WriteLine("Press any key to close the console.");
			Console.ReadKey(true);
		}
	}
}
