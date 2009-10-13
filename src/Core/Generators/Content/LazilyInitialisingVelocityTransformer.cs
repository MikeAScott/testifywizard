using System.Collections;
using System.IO;
using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;
using ThoughtWorks.TreeSurgeon.Core.Configuration;

namespace ThoughtWorks.TreeSurgeon.Core.Generators.Content
{
	public class LazilyInitialisingVelocityTransformer : ITransformer
	{
		private readonly TemplateSettings config;
		private readonly string templateName;
		private VelocityEngine lazilyInitialisedEngine = null;

		public LazilyInitialisingVelocityTransformer(TemplateSettings config, string templateName)
		{
			this.config = config;
			this.templateName = templateName;
		}

		public string Transform(string transformName, Hashtable transformParameters)
		{
			string output = "";
			using (TextWriter writer = new StringWriter())
			{
				VelocityEngine.MergeTemplate(transformName, new VelocityContext(transformParameters), writer);
				output = writer.ToString();
			}
			return output;
		}

		public TemplateSettings.Template Template {
			get { return config.Templates[templateName]; }
		}

		private VelocityEngine VelocityEngine
		{
			get
			{
				lock (this)
				{
					if (lazilyInitialisedEngine == null)
					{
						lazilyInitialisedEngine = new VelocityEngine();
						lazilyInitialisedEngine.SetProperty(RuntimeConstants_Fields.RUNTIME_LOG_LOGSYSTEM_CLASS, "NVelocity.Runtime.Log.NullLogSystem");
//						lazilyInitialisedEngine.SetProperty(RuntimeConstants_Fields.FILE_RESOURCE_LOADER_PATH, Template.Directory);
						lazilyInitialisedEngine.SetProperty(RuntimeConstants_Fields.FILE_RESOURCE_LOADER_PATH, this.config.ResourceRoot);
						lazilyInitialisedEngine.SetProperty(RuntimeConstants_Fields.RESOURCE_MANAGER_CLASS, "NVelocity.Runtime.Resource.ResourceManagerImpl");
						lazilyInitialisedEngine.Init();
					}
				}
				return lazilyInitialisedEngine;
			}
		}
	}
}