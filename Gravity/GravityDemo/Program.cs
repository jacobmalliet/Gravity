using System;
using System.Configuration;
using System.Net;
using Gravity.Test.Helpers;
using Relativity.API;
using Relativity.Test.Helpers;

namespace GravityDemo
{
	public partial class Program
	{
		private static Gravity.DAL.RSAPI.RsapiDao rsapiDao; 
		static void Main()
		{
			Init();

			Console.WriteLine("Create new instance of GravityLevelOne");
			var newArtifactId = Create();
			Console.WriteLine("Read GravityLevelOne");
			Read(newArtifactId);
			Console.WriteLine("Update GravityLevelOne");
			Update(newArtifactId);
			Console.WriteLine("Query GravityLevelOne");
			Query(newArtifactId);
			Console.WriteLine("Delete GravityLevelOne");
			Delete(newArtifactId);


			Console.WriteLine("Press any key to exit.");
			Console.ReadKey();
		}

		private static void Init()
		{
			ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
			var helper = new TestHelper();
			var workspaceId = Convert.ToInt32(ConfigurationManager.AppSettings["DebugWorkspaceId"]);
			var settings = new Gravity.Utils.InvokeWithRetrySettings(1, 1000);
			var invokeWithRetryService = new Gravity.Utils.InvokeWithRetryService(settings);
			var rsapiProvider = new Gravity.DAL.RSAPI.RsapiProvider(helper.GetServicesManager(), ExecutionIdentity.System, invokeWithRetryService, workspaceId ,1000);
			rsapiDao = new Gravity.DAL.RSAPI.RsapiDao(rsapiProvider, invokeWithRetryService);
		}
	}
}
