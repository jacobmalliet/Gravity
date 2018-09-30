using System;
using Gravity.Test.Helpers;
using Gravity.Test.TestClasses;
using Relativity.Test.Helpers;
using System.Diagnostics;

namespace GravityDemo
{
	public partial class Program
	{
		static void Read(int artifactId)
		{
			var gravityLevelOneParentOnly = rsapiDao.Get<GravityLevelOne>(artifactId, Gravity.Base.ObjectFieldsDepthLevel.OnlyParentObject);
			Console.WriteLine(ObjectDumper.Dump(gravityLevelOneParentOnly));
			var gravityLevelOneFullyRecursive = rsapiDao.Get<GravityLevelOne>(artifactId, Gravity.Base.ObjectFieldsDepthLevel.FullyRecursive);
			Console.WriteLine(ObjectDumper.Dump(gravityLevelOneFullyRecursive));
		}
	}
}
