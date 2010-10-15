using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ThoughtWorks.TreeSurgeon.Core.Configuration;

namespace ThoughtWorks.TreeSurgeon.UnitTests {
	[TestFixture]
	public class TemplateSettingsTests {

		TemplateSettings settings;
        private const string TEST_TEMPLATE = "VS2008";

		[SetUp]
		public void SetUp() {
			settings = TemplateSettings.Current;
		}

		[Test]
		public void CanGetCurrentSettings() {
			Assert.IsNotNull(TemplateSettings.Current);
		}

		[Test]
		public void TemplateSettingsContainsListOfTemplates() {
			CollectionAssert.AllItemsAreInstancesOfType(settings.Templates, typeof(TemplateSettings.Template));
		}
		[Test]
		public void FirstTemplateIsTestTemplate() {
			Assert.AreEqual(TEST_TEMPLATE, settings.Templates[0].Name);			
		}


		[Test]
		public void CanGetTemplateConfigByName() {
			Assert.IsNotNull(settings.Templates[TEST_TEMPLATE]);
		}

		[Test]
		public void SettingsContainVS2008TemplateDirectory() {
			Assert.AreEqual(@"resources\templates\2008", settings.Templates[TEST_TEMPLATE].TemplateDirectories[0].Path);
		}

		[Test]
        public void SettingsContainListofSkeletonDirectories(){
			CollectionAssert.AllItemsAreInstancesOfType(settings.Templates[0].SkeletonDirectories,typeof(TemplateSettings.ResourceDirectory));
        }

		[Test]
		public void ContainsVelocityContextItems() {
			CollectionAssert.AllItemsAreInstancesOfType(settings.VelocityContext, typeof(TemplateSettings.VelocityContextItem));
		}

		[Test]
		public void ContainsCoreGuidContextItem() {
			Assert.AreEqual("coreGuid", settings.VelocityContext[0].Name);
		}
	}
}
