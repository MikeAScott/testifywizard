using System;
using System.IO;
using NMock;
using NUnit.Framework;
using ThoughtWorks.TreeSurgeon.Core;

namespace ThoughtWorks.TreeSurgeon.UnitTests.Core
{
	[TestFixture]
	public class TreeSurgeonFrontEndTest
	{
		private TreeSurgeonFrontEnd frontEnd;
		private DynamicMock directoryBuilderMock;
		private DynamicMock generatorMock;
		private string treeSurgeonApplicationDirectory;

		[SetUp]
		public void Setup()
		{
			directoryBuilderMock = new DynamicMock(typeof (IDirectoryBuilder));
			generatorMock = new DynamicMock(typeof (IGenerator));
			treeSurgeonApplicationDirectory = "treeSurgeonDirectory";

			frontEnd = new TreeSurgeonFrontEnd((IDirectoryBuilder) directoryBuilderMock.MockInstance
			                                   , (IGenerator) generatorMock.MockInstance, treeSurgeonApplicationDirectory);
		}

		[TearDown]
		public void TearDown()
		{
		}

		private void VerifyAll()
		{
			directoryBuilderMock.Verify();
			generatorMock.Verify();
		}

		[Test]
		public void ShouldCreateDirectoryBuilderAndPassControlToGenerator()
		{
			string expectedOutputPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "TreeSurgeon"), "newProject");
			// SETUP
			directoryBuilderMock.Expect("CreateDirectory", expectedOutputPath);
			generatorMock.Expect("Generate", "newProject", expectedOutputPath, "treeSurgeonDirectory", "2005");

			// EXECUTE
			string outputDirectory = frontEnd.GenerateDevelopmentTree("TreeSurgeon","newProject", "2005");

			// VERIFY
			Assert.AreEqual(expectedOutputPath, outputDirectory);
			VerifyAll();
		}
	}

}