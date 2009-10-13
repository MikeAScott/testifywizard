using System;
using System.Collections;
using System.IO;
using NMock;
using NUnit.Framework;
using ThoughtWorks.TreeSurgeon.Core.Generators.Content;
using ThoughtWorks.TreeSurgeon.UnitTests.TestUtils;

namespace ThoughtWorks.TreeSurgeon.UnitTests.Core.Generators.Content
{
	[TestFixture]
	public class FileGeneratorWithTransformerTest
	{
		private DynamicMock transformerMock;
		private FileGeneratorWithTransformer generator;
		private string outputPath = @"testOutput\output.txt";

		[SetUp]
		public void Setup()
		{
			transformerMock = new DynamicMock(typeof (ITransformer));
			transformerMock.Strict = true;
			generator = new FileGeneratorWithTransformer((ITransformer) transformerMock.MockInstance);
		}

		[TearDown]
		public void Teardown()
		{
			if (File.Exists(outputPath))
			{
				File.Delete(outputPath);
			}
			if (Directory.Exists("testOutput"))
			{
				Directory.Delete("testOutput");
			}
		}

		private void VerifyAll()
		{
			transformerMock.Verify();
		}

		[Test]
		public void ShouldThrowAnExceptionIfOutputFileAlreadyExists()
		{
			Directory.CreateDirectory("testOutput");
			using (StreamWriter writer = new StreamWriter(outputPath))
			{
				writer.Write("blah");
				writer.Flush();
				writer.Close();
			}

			try
			{
				generator.Generate(outputPath, "foo", new Hashtable());
				Assert.Fail("Should throw an exception since output file already exists");
			}
			catch (ApplicationException e)
			{
				Assert.IsTrue(e.Message.IndexOf("to already exist") > -1);
			}

			VerifyAll();
		}

		[Test]
		public void ShouldWriteFileWithResponseFromTransformer()
		{
			Hashtable myParams = new Hashtable();
			myParams["myParam1"] = "myVal1";

			string fileContents = @"Some
contents";

			transformerMock.ExpectAndReturn("Transform", fileContents, "myTransform", new HashtableConstraint(myParams));
			generator.Generate(outputPath, "myTransform", myParams);

			using (StreamReader reader = new StreamReader(outputPath))
			{
				Assert.AreEqual(fileContents.Trim(), reader.ReadToEnd().Trim());
			}

			VerifyAll();
		}
	}
}