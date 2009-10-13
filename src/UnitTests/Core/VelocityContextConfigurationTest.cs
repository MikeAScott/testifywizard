using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ThoughtWorks.TreeSurgeon.Core;
using ThoughtWorks.TreeSurgeon.Core.Configuration;

namespace ThoughtWorks.TreeSurgeon.UnitTests.Core.Generators {
	[TestFixture]
	public class VelocityContextConfigurationTest {

		TemplateSettings.VelocityContextCollection config;
		[SetUp]
		public void SetUp() {
			config = TemplateSettings.Current.VelocityContext;
		}

		[Test]
		public void CanGetVelocityContextConfiguration() {
			Assert.IsNotNull(config);
		}

		[Test]
		public void ItemsReturnsArrayOfVelocityContextItems() {
			CollectionAssert.AllItemsAreInstancesOfType(config,typeof(TemplateSettings.VelocityContextItem));
		}

		[Test]
		public void FirstItemIsCoreGuid() {
			Assert.AreEqual("coreGuid", config[0].Name);
			Assert.IsTrue(config[0].GenerateGuid);
		}

		[Test]
		public void CheckDefaultSettings() {
			Assert.AreEqual(9, config.Count, "Should be 9 items");
			Assert.AreEqual("guiGuid", config[1].Name);
			Assert.IsTrue(config[1].GenerateGuid);
			Assert.AreEqual("unitTestGuid", config[2].Name);
			Assert.IsTrue(config[2].GenerateGuid);
			Assert.AreEqual("consoleGuid", config[3].Name);
			Assert.IsTrue(config[3].GenerateGuid);
			Assert.AreEqual("fitFixturesGuid", config[4].Name);
			Assert.IsTrue(config[4].GenerateGuid);
			Assert.AreEqual("solutionItemsGuid", config[5].Name);
			Assert.IsTrue(config[5].GenerateGuid);
			Assert.AreEqual("buildHelpersGuid", config[6].Name);
			Assert.IsTrue(config[6].GenerateGuid);
			Assert.AreEqual("nantDeleteClause", config[7].Name);
			Assert.AreEqual(@"${directory::exists(build.dir)}",config[7].Value);
		}
	}
}
