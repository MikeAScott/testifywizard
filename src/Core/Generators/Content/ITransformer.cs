using System.Collections;

namespace ThoughtWorks.TreeSurgeon.Core.Generators.Content
{
	public interface ITransformer
	{
		string Transform(string transformName, Hashtable transformParameters);
	}
}