using System;
using System.Diagnostics;
using System.Reflection;
using Gravity.Test.TestClasses;

namespace GravityDemo
{
	public partial class Program
	{
		static void Query(int artifactId)
		{
			var gravityLevelOneParentOnly = rsapiDao.Get<GravityLevelOne>(artifactId, Gravity.Base.ObjectFieldsDepthLevel.OnlyParentObject);
			var nameFieldGuid = FieldGuid<GravityLevelOne>(nameof(gravityLevelOneParentOnly.Name));				
			var textCondition = new kCura.Relativity.Client.TextCondition(nameFieldGuid, kCura.Relativity.Client.TextConditionEnum.EqualTo, gravityLevelOneParentOnly.Name);
			var gravityLevelOnes = rsapiDao.Query<GravityLevelOne>(textCondition, Gravity.Base.ObjectFieldsDepthLevel.OnlyParentObject);
			Console.WriteLine(ObjectDumper.Dump(gravityLevelOnes));
		}

		public static Guid FieldGuid<T>(string fieldName)
			=> typeof(T).GetProperty(fieldName).GetCustomAttribute<RelativityObjectFieldAttribute>().FieldGuid;

	}
}
