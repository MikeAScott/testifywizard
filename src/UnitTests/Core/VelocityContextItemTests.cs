using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ThoughtWorks.TreeSurgeon.Core;
using ThoughtWorks.TreeSurgeon.Core.Configuration;

namespace ThoughtWorks.TreeSurgeon.UnitTests.Core.Generators {
	[TestFixture]
	public class VelocityContextItemTests {
		[Test]
		public void CanConstructUsingNameAndValue() {
			TemplateSettings.VelocityContextItem item = new TemplateSettings.VelocityContextItem("projectGuid", "A Project Guid");
			Assert.AreEqual("projectGuid", item.Name);
			Assert.AreEqual("A Project Guid", item.Value);
		}

		[Test]
		public void GenerateGuidisTrueIfPartTwoSaysGUID() {
			TemplateSettings.VelocityContextItem item = new TemplateSettings.VelocityContextItem("projectGuid","{GUID}");
			Assert.IsTrue(item.GenerateGuid);
			Assert.AreEqual("projectGuid", item.Name);
		}
	}
}
