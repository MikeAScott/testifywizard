using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ThoughtWorks.TreeSurgeon.Core.Configuration;

namespace ThoughtWorks.TreeSurgeon.UnitTests {
	[TestFixture]
	public class TemplateSettingsTests {

		TemplateSettings settings;

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
		public void FirstTemplateIsVs2005() {
			Assert.AreEqual("2005", settings.Templates[0].Name);			
		}


		[Test]
		public void CanGetTemplateConfigByName() {
			Assert.IsNotNull(settings.Templates["2005"]);
		}

		[Test]
		public void SettingsContainVS2005TemplateDirectory() {
			Assert.AreEqual(@"resources\templates\2005", settings.Templates["2005"].TemplateDirectories[0].Path);
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
