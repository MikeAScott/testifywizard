using System;
using System.IO;
using ThoughtWorks.TreeSurgeon.Core.Generators;
using ThoughtWorks.TreeSurgeon.Core.Generators.Content;
using ThoughtWorks.TreeSurgeon.Core.Configuration;

namespace ThoughtWorks.TreeSurgeon.Core
{
	public class TreeSurgeonFrontEnd
	{
		private readonly IDirectoryBuilder directoryBuilder;
		private readonly IGenerator generator;
		private readonly string treeSurgeonApplicationDirectory;

		public TreeSurgeonFrontEnd(IDirectoryBuilder directoryBuilder, IGenerator generator, string treeSurgeonApplicationDirectory)
		{
			this.directoryBuilder = directoryBuilder;
			this.generator = generator;
			this.treeSurgeonApplicationDirectory = treeSurgeonApplicationDirectory;
		}

		public string GenerateDevelopmentTree(string folderName, string projectName, string templateName)
		{
			string outputPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), folderName), projectName);
			directoryBuilder.CreateDirectory(outputPath);
			generator.Generate(projectName, outputPath, treeSurgeonApplicationDirectory, templateName);
			return outputPath;
		}

		// We don't have ObjectWizard plugged in yet, so here's our instance strategy
		public TreeSurgeonFrontEnd(string treeSurgeonApplicationDirectory, string version) : this(
			new SimpleDirectoryBuilder(),
			new SimpleTreeGenerator(
				new DefaultDirectoryCopier(),
				new FileGeneratorWithTransformer(new LazilyInitialisingVelocityTransformer(TemplateSettings.Current,version)),
				new StandardDotNetUpperCaseGuidGenerator(),
				TemplateSettings.Current),
			treeSurgeonApplicationDirectory
			){}
	}
}