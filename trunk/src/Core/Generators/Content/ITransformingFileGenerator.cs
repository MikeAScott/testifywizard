using System.Collections;

namespace ThoughtWorks.TreeSurgeon.Core.Generators.Content
{
	public interface ITransformingFileGenerator
	{
		void Generate(string outputDirectory, string transformName, Hashtable transformParameters);
	}
}