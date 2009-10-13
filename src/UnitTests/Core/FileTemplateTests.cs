using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ThoughtWorks.TreeSurgeon.Core;

namespace ThoughtWorks.TreeSurgeon.UnitTests.Core {
	[TestFixture]
	public class FileTemplateTests {
		[Test]
		public void CanConstructUsingNameAndChildPathTemplate() {
			FileTemplate template = new FileTemplate("Sapling.bat", "{0}.bat");
			Assert.AreEqual("Sapling.bat", template.Name);
			Assert.AreEqual("{0}.bat", template.ChildPathTemplate);
		}

		[Test]
		public void CanParseTwoPartCsvString() {
			FileTemplate template = FileTemplate.Parse("Tests.cs, Tests\\Test.cs");
			Assert.IsNotNull(template);
			Assert.AreEqual("Tests.cs", template.Name);
			Assert.AreEqual("Tests\\Test.cs", template.ChildPathTemplate);
		}

		[Test]
		public void ParseOfOnePartStringSetsChildPathSameAsName() {
			FileTemplate template = FileTemplate.Parse("OnlyOne.cs");
			Assert.AreEqual(template.Name, template.ChildPathTemplate);
		}
	}
}
