using System;
using System.Collections.Generic;
using Gravity.Test.Helpers;
using Gravity.Test.TestClasses;
using GravityDemo.Utils;
using Relativity.Test.Helpers;

namespace GravityDemo
{
	public partial class Program
	{
		static int Create()
		{
			var randomNumber = RandomNumberUtil.RandomNumber();

			var gravityLevel3 = new Gravity.Test.TestClasses.GravityLevel3 {
				Name = $"GravityLevel3-{randomNumber}",
			};

			var gravityLevel2 = new Gravity.Test.TestClasses.GravityLevel2 {
				Name = $"GravityLevel2-{randomNumber}",
				GravityLevel3SingleObj = gravityLevel3
			};

			var gravityLevelOne = new Gravity.Test.TestClasses.GravityLevelOne {
				Name = $"GravityLevelOne-{randomNumber}",
				GravityLevel2Obj = gravityLevel2,
				MultipleChoiceFieldChoices = new[] { MultipleChoiceFieldChoices.MultipleChoice2, MultipleChoiceFieldChoices.MultipleChoice3 }
			};

			return rsapiDao.Insert(gravityLevelOne, Gravity.Base.ObjectFieldsDepthLevel.FullyRecursive);
		}
	}
}
