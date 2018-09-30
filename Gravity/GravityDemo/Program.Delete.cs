using System;
using System.Collections.Generic;
using System.Diagnostics;
using Gravity.Test.Helpers;
using Gravity.Test.TestClasses;
using GravityDemo.Utils;
using Relativity.Test.Helpers;

namespace GravityDemo
{
	public partial class Program
	{

		#region RecursiveDelete
		static void Delete()
		{
			var randomNumber = RandomNumberUtil.RandomNumber();
			var gravityLevel1 = new Gravity.Test.TestClasses.GravityLevelOne {
				Name = $"GravityLevel1-{randomNumber}",
			    GravityLevel2Childs = new List<GravityLevel2Child>()
			};

			for(var i = 1; i < 4; i++)
			{
				var gravityLevel2Child = new Gravity.Test.TestClasses.GravityLevel2Child {
					Name = $"GravityLevel2Child-{randomNumber}-{i}"
				};
				gravityLevel1.GravityLevel2Childs.Add(gravityLevel2Child);
			}
			var artifactId = rsapiDao.Insert<GravityLevelOne>(gravityLevel1, Gravity.Base.ObjectFieldsDepthLevel.FullyRecursive);

			rsapiDao.Delete<GravityLevelOne>(artifactId, Gravity.Base.ObjectFieldsDepthLevel.FullyRecursive);
		}

		#endregion

		static void Delete(int artifactId)
		{
			rsapiDao.Delete<GravityLevelOne>(artifactId, Gravity.Base.ObjectFieldsDepthLevel.FullyRecursive);
		}

	}
}
