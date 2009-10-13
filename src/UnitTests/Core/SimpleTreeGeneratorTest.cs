using System.Collections;
using NMock;
using NUnit.Framework;
using ThoughtWorks.TreeSurgeon.Core;
using ThoughtWorks.TreeSurgeon.Core.Generators;
using ThoughtWorks.TreeSurgeon.Core.Generators.Content;
using ThoughtWorks.TreeSurgeon.UnitTests.TestUtils;
using ThoughtWorks.TreeSurgeon.Core.Configuration;

namespace ThoughtWorks.TreeSurgeon.UnitTests.Core
{
	[TestFixture]
	public class SimpleTreeGeneratorTest
	{
		private SimpleTreeGenerator generator;
		private DynamicMock velocityFileGeneratorMock;
		private DynamicMock directoryCopierMock;
		private DynamicMock guidGeneratorMock;

		[SetUp]
		public void Setup()
		{
			directoryCopierMock = new DynamicMock(typeof (IDirectoryCopier));
			velocityFileGeneratorMock = new DynamicMock(typeof (ITransformingFileGenerator));
			guidGeneratorMock = new DynamicMock(typeof (IGuidGenerator));


			generator = new SimpleTreeGenerator((IDirectoryCopier) directoryCopierMock.MockInstance,
			                                    (ITransformingFileGenerator) velocityFileGeneratorMock.MockInstance,
			                                    (IGuidGenerator) guidGeneratorMock.MockInstance,
												MockTemplateSettings.Current);
			//TODO: Change templateTree param to use mock
		}

		[TearDown]
		public void TearDown()
		{
		}

		private void VerifyAll()
		{
			velocityFileGeneratorMock.Verify();
			directoryCopierMock.Verify();
			guidGeneratorMock.Verify();
		}

		[Test]
		public void ShouldCopySkeletonAndCallVelocityFileGeneratorForEveryFile()
		{
			Hashtable expectedVelocityContext = new Hashtable();
			expectedVelocityContext["projectName"] = "NewTestProject";
			expectedVelocityContext["coreGuid"] = "guid1";
			expectedVelocityContext["unitTestGuid"] = "guid2";
			expectedVelocityContext["consoleGuid"] = "guid3";
            expectedVelocityContext["fitFixturesGuid"] = "guid4";
			expectedVelocityContext["solutionItemsGuid"] = "guid5";
			expectedVelocityContext["buildHelpersGuid"] = "guid6";
			expectedVelocityContext["nantDeleteClause"] = @"${directory::exists(build.dir)}";

			// SETUP
			directoryCopierMock.Expect("CopyDirectory", "resourceBase\\resources\\skeleton", "output");

			guidGeneratorMock.ExpectAndReturn("GenerateGuid", "guid1");
			guidGeneratorMock.ExpectAndReturn("GenerateGuid", "guid2");
			guidGeneratorMock.ExpectAndReturn("GenerateGuid", "guid3");
            guidGeneratorMock.ExpectAndReturn("GenerateGuid", "guid4");
            guidGeneratorMock.ExpectAndReturn("GenerateGuid", "guid5");
            guidGeneratorMock.ExpectAndReturn("GenerateGuid", "guid6");

			foreach(TemplateSettings.TemplateFile templ in MockTemplateSettings.Current.Templates["VS2005"].TemplateFiles) {
				velocityFileGeneratorMock.Expect("Generate", @"output\" + string.Format(templ.TargetPath,"NewTestProject"),templ.Path + ".vm", new HashtableConstraint(expectedVelocityContext));
			}
            // EXECUTE
			generator.Generate("NewTestProject", "output", "resourceBase","VS2005");

			// VERIFY
			VerifyAll();
		}
	}
}