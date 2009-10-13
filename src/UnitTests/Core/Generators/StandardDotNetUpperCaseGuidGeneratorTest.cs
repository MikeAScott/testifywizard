using NUnit.Framework;
using ThoughtWorks.TreeSurgeon.Core.Generators;

namespace ThoughtWorks.TreeSurgeon.UnitTests.Core.Generators
{
	[TestFixture]
	public class StandardDotNetUpperCaseGuidGeneratorTest
	{
		[Test]
		public void ShouldProduceUpperCaseUniqueGUIDs()
		{
			StandardDotNetUpperCaseGuidGenerator generator = new StandardDotNetUpperCaseGuidGenerator();
			string guid1 = generator.GenerateGuid();
			string guid2 = generator.GenerateGuid();

			Assert.AreNotEqual(guid1, guid2);
			Assert.AreEqual(guid1, guid1.ToUpper());
		}

	}
}