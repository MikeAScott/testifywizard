using System.IO;
using NUnit.Framework;
using ThoughtWorks.TreeSurgeon.Core.Generators.Content;

namespace ThoughtWorks.TreeSurgeon.UnitTests.Core.Generators.Content
{
	[TestFixture]
	public class DefaultDirectoryCopierTest
	{
		private DefaultDirectoryCopier directoryCopier;

		[SetUp]
		public void Setup()
		{
			Teardown();
			directoryCopier = new DefaultDirectoryCopier();

			Directory.CreateDirectory("source");
			Directory.CreateDirectory(@"source\sub");
			CreateFile(@"source\file1");
			CreateFile(@"source\file2");
			CreateFile(@"source\sub\file3");
		}

		private static void CreateFile(string path)
		{
			using (StreamWriter writer = new StreamWriter(path))
			{
				writer.Write(path);
				writer.Flush();
				writer.Close();
			}
		}

		[TearDown]
		public void Teardown()
		{
			DeleteFileIfExists(@"source\file1");
			DeleteFileIfExists(@"source\file2");
			DeleteFileIfExists(@"source\sub\file3");
			DeleteDirectoryIfExists(@"source\sub");
			DeleteDirectoryIfExists(@"source");
			DeleteFileIfExists(@"myTarget\file1");
			DeleteFileIfExists(@"myTarget\file2");
			DeleteFileIfExists(@"myTarget\sub\file3");
			DeleteDirectoryIfExists(@"myTarget\sub");
			DeleteDirectoryIfExists(@"myTarget");
		}

		[Test]
		public void ShouldCreateOutputDirectoryIfItDoesntExist()
		{
			directoryCopier.CopyDirectory("source", "myTarget");
			Assert.IsTrue(Directory.Exists("myTarget"));
		}

		[Test]
		public void ShouldCopyFilesFromSourceToTargetRecursively()
		{
			directoryCopier.CopyDirectory("source", "myTarget");
			AssertFileExistsAndContains(@"myTarget\file1", @"source\file1");
			AssertFileExistsAndContains(@"myTarget\file2", @"source\file2");
			AssertFileExistsAndContains(@"myTarget\sub\file3", @"source\sub\file3");
		}

		private void AssertFileExistsAndContains(string path, string expectedContents)
		{
			Assert.IsTrue(File.Exists(path));
			using (StreamReader reader = new StreamReader(path))
			{
				Assert.AreEqual(expectedContents, reader.ReadToEnd());
			}
		}

		private void DeleteFileIfExists(string file)
		{
			if (File.Exists(file))
			{
				File.Delete(file);
			}
		}

		private void DeleteDirectoryIfExists(string directory)
		{
			if (Directory.Exists(directory))
			{
				Directory.Delete(directory);
			}
		}
	}
}
