using System;
using System.IO;
using NUnit.Framework;
using ThoughtWorks.TreeSurgeon.Core.Utils;

namespace SQS.Testify.AcceptanceTests
{
	[TestFixture]
	public class ConsoleAcceptanceTest
	{

        private const string TEST_TEMPLATE = "VS2008";
		private string projectName;
		private string outputPath;
		private DirectoryInfo wd;

		[SetUp]
		public void Setup()
		{
			projectName = "AcceptanceTestProject";
			outputPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Testify"), projectName);
			wd = new DirectoryInfo(Directory.GetCurrentDirectory());
		}

		[TearDown]
		public void Teardown()
		{
			try
			{
				// If at first you don't succeed...
				DeleteOutputDir();
			}
			catch (Exception)
			{
				// .. try again. (Stoopid windows file locking...)
				DeleteOutputDir();
			}
		}

		private void DeleteOutputDir()
		{
            if (Directory.Exists(outputPath)) {
                Directory.Delete(outputPath, true);
            }
		}

		// Currently only works in NAnt build - need some Post-Build script that is configuration dependent to set this up for VS use.
		[Test]
		public void ShouldGenerateADevelopmentTreeWithCorrectNameThatBuildsSuccessfullyAndCanBeCleaned()
		{
			RunTreeSurgeonAndCheckOK(TEST_TEMPLATE);

			string nantOutput = RunNantAndCheckPasses("");
            Assert.IsTrue(nantOutput.IndexOf("[msbuild] Build succeeded") > -1);
			Assert.IsTrue(nantOutput.IndexOf("Tests run: ") > -1 && nantOutput.IndexOf(", Failures: 0") > -1) ;
			Assert.IsTrue(Directory.Exists(Path.Combine(outputPath, "build")));
			CheckFileExistsAndIsBiggerThan(Path.Combine("test-reports", projectName + ".UnitTests.NUnitReport.xml"), 500);

			RunNantAndCheckPasses("clean");
			Assert.IsFalse(Directory.Exists(Path.Combine(outputPath, "build")));
		}

		// TREE-5
		[Test]
		public void GeneratedTreeShouldGenerateDistributionFiles()
		{
			RunTreeSurgeonAndCheckOK(TEST_TEMPLATE);
			RunNantAndCheckPasses("full");
			CheckFileExistsAndIsBiggerThan(Path.Combine("dist",projectName + ".zip"), 1000);
		}

		// TREE-4
		[Test]
		public void GeneratedTreeShouldRunNCoverForUnitTests()
		{
			RunTreeSurgeonAndCheckOK(TEST_TEMPLATE);
			RunNantAndCheckPasses("full");
			CheckFileExistsAndIsBiggerThan(Path.Combine("test-reports", projectName + ".UnitTests.CoverageReport.xml"), 500);
		}

		// TREE-4
		[Test]
		public void GeneratedTreeShouldRunFxCop() {
			RunTreeSurgeonAndCheckOK(TEST_TEMPLATE);
			RunNantAndCheckPasses("full");
			CheckFileExistsAndIsBiggerThan(Path.Combine("test-reports", projectName + ".FxCopReport.xml"), 500);
		}


		private string RunNantAndCheckPasses(string nantArgs)
		{
			ProcessResult nantResult = new ProcessExecutor().Execute(new ProcessInfo("go.bat", nantArgs, outputPath));
			string nantOutput = nantResult.StandardOutput;

			if (nantResult.ExitCode != 0)
			{
				Console.WriteLine("Go.bat failed");
				Console.WriteLine("** Standard out from go.bat: **");
				Console.WriteLine(nantOutput);
				Console.WriteLine("** End of Standard out from go.bat: **\n");
				Console.WriteLine("Standard err:");
				Console.WriteLine(nantResult.StandardError);
				Assert.Fail("Go.bat failed");
			}

			Assert.IsTrue(nantOutput.IndexOf("BUILD SUCCEEDED") > -1);
			return nantOutput;
		}

		private void RunTreeSurgeonAndCheckOK(string version)
		{
			// This working dir stuff here for TREE-13
			ProcessInfo pi = new ProcessInfo("TestifyConsole.exe", projectName + " " + version, wd.FullName);
			ProcessResult TestifyResult = new ProcessExecutor().Execute(pi);
			if (TestifyResult.ExitCode != 0)
			{
				Console.WriteLine("Testify Console failed");
				Console.WriteLine("** Standard out from Testify.exe: **");
				Console.WriteLine(TestifyResult.StandardOutput);
				Console.WriteLine("** End of Standard out from Testify.exe: **\n");
				Console.WriteLine("Standard err:");
				Console.WriteLine(TestifyResult.StandardError);
				Assert.Fail("Testify console app failed");
			}

			Assert.IsTrue(Directory.Exists(outputPath));
		}

		private void CheckFileExistsAndIsBiggerThan(string fileName, int fileSize)
		{
			string expectedLocation = Path.Combine(Path.Combine(outputPath, "build"), fileName);
			Assert.IsTrue(File.Exists(expectedLocation));
			Assert.IsTrue(new FileInfo(expectedLocation).Length > fileSize);
		}
	}
}