namespace ThoughtWorks.TreeSurgeon.Core
{
	public interface IGenerator
	{
		void Generate(string projectName, string outputPath, string resourceBasePath, string templateName);
	}
}