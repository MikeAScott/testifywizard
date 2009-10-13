using System;
using System.Collections;
using System.IO;

namespace ThoughtWorks.TreeSurgeon.Core.Generators.Content
{
	public class FileGeneratorWithTransformer : ITransformingFileGenerator
	{
		private readonly ITransformer Transformer;

		public FileGeneratorWithTransformer(ITransformer Transformer)
		{
			this.Transformer = Transformer;
		}

		public void Generate(string outputFileName, string transformName, Hashtable transformParameters)
		{

			if (File.Exists(outputFileName))
			{
				throw new ApplicationException(string.Format("Did not expect file [{0}] to already exist", outputFileName));
			}

			string outputDir = Path.GetDirectoryName(outputFileName);
			if (! Directory.Exists(outputDir))
			{
				Directory.CreateDirectory(outputDir);
			}

			using (StreamWriter writer = File.CreateText(outputFileName))
			{
				writer.WriteLine(Transformer.Transform(transformName, transformParameters));
				writer.Close();
			}
		}
	}
}