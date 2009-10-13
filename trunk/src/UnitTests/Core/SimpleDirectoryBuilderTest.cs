using System;
using System.IO;
using NUnit.Framework;
using ThoughtWorks.TreeSurgeon.Core;

namespace ThoughtWorks.TreeSurgeon.UnitTests.Core
{
	[TestFixture]
	public class SimpleDirectoryBuilderTest
	{
		[SetUp]
		public void Setup()
		{
			Teardown();
		}

		[TearDown]
		public void Teardown()
		{
			if (Directory.Exists("myDir"))
			{
				Directory.Delete("myDir");
			}
		}

		[Test]
		public void ShouldThrowExceptionIfDirectoryAlreadyExists()
		{
			Directory.CreateDirectory("myDir");

			try
			{
				new SimpleDirectoryBuilder().CreateDirectory("myDir");
				Assert.Fail("Should throw exception");
			}
			catch (ApplicationException e)
			{
				Assert.AreNotEqual("", e.Message);
			}
		}

		[Test]
		public void ShouldCreateDirectoryAndReturnFullPath()
		{
			new SimpleDirectoryBuilder().CreateDirectory("myDir");

			Assert.IsTrue(Directory.Exists("myDir"));
		}
	}
}