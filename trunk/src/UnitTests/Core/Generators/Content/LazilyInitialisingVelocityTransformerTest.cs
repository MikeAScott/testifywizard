using System.Collections;
using System.IO;
using NMock;
using NUnit.Framework;
using ThoughtWorks.TreeSurgeon.Core.Generators.Content;
using ThoughtWorks.TreeSurgeon.UnitTests.TestUtils;

namespace ThoughtWorks.TreeSurgeon.UnitTests.Core.Generators.Content
{
	[TestFixture]
	public class LazilyInitialisingVelocityTransformerTest
	{
		private LazilyInitialisingVelocityTransformer viewTransformer;
		private DynamicMock configurationMock;

		[SetUp]
		public void Setup()
		{
			Teardown();
			Directory.CreateDirectory(@"testtemplates");
			Directory.CreateDirectory(@"testtemplates\2005");
			using(StreamWriter writer = new StreamWriter(@"testtemplates\2005\testtemplate.vm"))
			{
				writer.Write("foo is $foo");
				writer.Flush();
				writer.Close();
			}

			configurationMock = new DynamicMock(typeof (IVelocityTransformerConfig));
			configurationMock.SetupResult("TemplateDirectory", @"testtemplates\2005");
			viewTransformer = new LazilyInitialisingVelocityTransformer(MockTemplateSettings.Current,"VS2005");
		}

		[TearDown]
		public void Teardown()
		{
			if (File.Exists(@"testtemplates\2005\testtemplate.vm"))
			{
				File.Delete(@"testtemplates\2005\testtemplate.vm");
			}
			if (Directory.Exists(@"testtemplates\2005"))
			{
				Directory.Delete(@"testtemplates\2005");
			}
			if(Directory.Exists("testtemplates")) {
				Directory.Delete("testtemplates");
			}

		}

		private void VerifyAll()
		{
			configurationMock.Verify();
		}

		[Test]
		public void ShouldUseVelocityToMergeContextContentsWithTemplate()
		{
			Hashtable contextContents = new Hashtable();
			contextContents["foo"] = "bar";

			Assert.AreEqual("foo is bar", viewTransformer.Transform(@"testtemplates\2005\testtemplate.vm", contextContents));
			VerifyAll();
		}
	}
}