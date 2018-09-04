﻿using Gravity.Base;
using Gravity.DAL.RSAPI;
using Gravity.Test.Helpers;
using Gravity.Test.TestClasses;
using kCura.Relativity.Client;
using kCura.Relativity.Client.DTOs;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gravity.Test.Unit
{
	public class RsapiDaoDeleteTests
	{
		//NUT: not unit testable

		const int rootId = 1;
		const int level2Id_1 = 10;
		const int level2Id_2 = 20;

		Mock<IRsapiProvider> mockProvider;

		[SetUp]
		public void Init()
		{
			mockProvider = new Mock<IRsapiProvider>(MockBehavior.Strict);
		}

		[SetUp]
		public void End()
		{
			mockProvider.VerifyAll();
		}

		[Test]
		public void Delete_NoRecursion_NoChildObjects()
		{
			SetupQuery<GravityLevel2Child>(rootId, new int[0]); // no child objects
			SetupDelete(new[] { rootId }); //delete will occur
			ExecuteDelete(ObjectFieldsDepthLevel.OnlyParentObject);

			//can have linked single object; will get unlinked - NUT
			//can have linked multiple object; will get unlined - NUT
			//can have file field - NUT
		}

		[Test]
		public void Delete_NoRecursion_ChildObjects()
		{
			SetupQuery<GravityLevel2Child>(rootId, new[] { level2Id_1, level2Id_2 }); //child objects
			//delete will not occur, throws instead
			Assert.Throws<ArgumentOutOfRangeException>(() => ExecuteDelete(ObjectFieldsDepthLevel.OnlyParentObject));
			//fails if has child objects
		}

		[Test]
		public void Delete_LevelOneRecursion_ChildObjects()
		{
			SetupQuery<GravityLevel2Child>(rootId, new[] { level2Id_1, level2Id_2 });
			//delete will occur on both levels
			SetupDelete(new[] { level2Id_1, level2Id_2 });
			SetupDelete(new[] { rootId });
			ExecuteDelete(ObjectFieldsDepthLevel.FirstLevelOnly);
			//succeeds if has child objects; deletes child objects
		}

		[Test, Ignore("No children of child objects defined")]
		public void Delete_LevelOneRecustion_LevelTwoObjects()
		{
			//fails if has nested child objects
			//in such case does not delete other child objects either
		}

		[Test, Ignore("No children of child objects defined")]
		public void Delete_Recursive_LevelTwoObjects()
		{
			//deletes deeply nested child objects
		}

		private void SetupDelete(int[] artifactIds)
		{
			mockProvider.Setup(x => x.Delete(It.Is<List<int>>(
				y => new HashSet<int>(y).SetEquals(artifactIds))))
			.Returns(new WriteResultSet<RDO> { Success = true });
		}

		private void SetupQuery<T>(int parentArtifactId, int[] resultArtifactIds) where T: BaseDto
		{
			mockProvider.Setup(x =>
				x.Query(It.Is<Query<RDO>>(
					y => y.ArtifactTypeGuid == BaseDto.GetObjectTypeGuid<T>()
						&& ((WholeNumberCondition)y.Condition).Value.Single() == parentArtifactId)))
				.Returns(new[] { resultArtifactIds.Select(y => new RDO(y)).ToSuccessResultSet<QueryResultSet<RDO>>() });
		}

		private void ExecuteDelete(ObjectFieldsDepthLevel depthLevel)
			=> new RsapiDao(mockProvider.Object).Delete<GravityLevelOne>(rootId, depthLevel);
	}
}