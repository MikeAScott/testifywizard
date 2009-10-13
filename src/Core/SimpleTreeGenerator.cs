using System;
using System.Collections;
using System.IO;
using ThoughtWorks.TreeSurgeon.Core.Generators;
using ThoughtWorks.TreeSurgeon.Core.Generators.Content;
using ThoughtWorks.TreeSurgeon.Core.Configuration;

namespace ThoughtWorks.TreeSurgeon.Core
{
	public class SimpleTreeGenerator : IGenerator
	{
		protected readonly ITransformingFileGenerator generator;
		private readonly IGuidGenerator guidGenerator;
		private readonly IDirectoryCopier directoryCopier;
		private readonly TemplateSettings settings;

		public SimpleTreeGenerator( IDirectoryCopier directoryCopier, ITransformingFileGenerator generator, IGuidGenerator guidGenerator, TemplateSettings settings )
		{
			this.generator = generator;
			this.guidGenerator = guidGenerator;
			this.directoryCopier = directoryCopier;
			this.settings = settings;
		}

		public void Generate(string projectName, string outputPath, string resourceBasePath, string templateName)
		{
			TemplateSettings.Template template = settings.Templates[templateName];
			foreach(TemplateSettings.ResourceDirectory skeletonPath in template.SkeletonDirectories) {
				copySkeletonDirectory(outputPath, resourceBasePath, skeletonPath);
			}

			Hashtable velocityContext = buildVelocityContext(projectName);

			FileGeneratorContext genContext = new FileGeneratorContext(generator, outputPath, projectName, velocityContext);

			if(template.TemplateDirectories != null) {
				foreach (TemplateSettings.ResourceDirectory templateDirectory in template.TemplateDirectories) {
					copyTemplateDirectory(resourceBasePath, genContext, templateDirectory.Path);
				}
			}
			
			if (template.TemplateFiles != null) {
				foreach(TemplateSettings.TemplateFile tmpl in template.TemplateFiles) {
					genContext.Generate(tmpl.TargetPath, tmpl.Path);
				}
			}
		}

		private static void copyTemplateDirectory(string resourceBasePath, FileGeneratorContext genContext, string templateDirectory) {
			string templateDirectoryPath = Path.Combine(resourceBasePath, templateDirectory);
			DirectoryListBuilder builder = new DirectoryListBuilder(templateDirectoryPath,resourceBasePath);
			FileTemplate[] templates = builder.Build();
			foreach (FileTemplate tmpl in templates) {
				genContext.Generate(tmpl.ChildPathTemplate, tmpl.Name);
			}
		}

		private void copySkeletonDirectory(string outputPath, string resourceBasePath, TemplateSettings.ResourceDirectory skeletonPath) {
			directoryCopier.CopyDirectory(Path.Combine(resourceBasePath, skeletonPath.Path), outputPath);
		}

		private Hashtable buildVelocityContext(string projectName) {
			Hashtable velocityContext = new Hashtable();
			velocityContext["projectName"] = projectName;
			TemplateSettings.VelocityContextCollection velocityConfig = settings.VelocityContext;
			foreach (TemplateSettings.VelocityContextItem item in velocityConfig) {
				if (item.GenerateGuid)
					velocityContext[item.Name] = guidGenerator.GenerateGuid();
				else
					velocityContext[item.Name] = item.Value;
			}
			return velocityContext;
		}

		private class FileGeneratorContext {
			ITransformingFileGenerator generator;
			string topPath;
			string projectName;
			Hashtable velocityContext;

			public FileGeneratorContext(ITransformingFileGenerator generator,string topPath, string projectName, Hashtable velocityContext) {
				this.generator = generator;
				this.topPath = topPath;
				this.projectName = projectName;
				this.velocityContext = velocityContext;
			}

			public void Generate(string childPathTemplate, string templateName) {
				string outputDir = Path.Combine(topPath, string.Format(childPathTemplate, projectName));
				generator.Generate(outputDir, templateName + ".vm", velocityContext);
			}
		}
	}
}