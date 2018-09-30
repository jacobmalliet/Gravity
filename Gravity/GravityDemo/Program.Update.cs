using System;
using System.Diagnostics;
using Gravity.Test.Helpers;
using Gravity.Test.TestClasses;
using Relativity.Test.Helpers;

namespace GravityDemo
{
	public partial class Program
	{
		static void Update(int artifactId)
		{
			var gravityLevelOne = rsapiDao.Get<GravityLevelOne>(artifactId, Gravity.Base.ObjectFieldsDepthLevel.FullyRecursive);
			gravityLevelOne.DateTimeField = System.DateTime.Now.AddYears(-2);
			gravityLevelOne.FileField = new Gravity.Base.DiskFileDto(System.IO.Path.Combine(Environment.CurrentDirectory, "Test.txt"));
			rsapiDao.Update( gravityLevelOne , Gravity.Base.ObjectFieldsDepthLevel.FullyRecursive);
			var gravityLevelOneFullyRecursive = rsapiDao.Get<GravityLevelOne>(artifactId, Gravity.Base.ObjectFieldsDepthLevel.FullyRecursive);
			Console.WriteLine(ObjectDumper.Dump(gravityLevelOneFullyRecursive));
		}
	}
}
