using System;
using System.Collections.Generic;
using System.Text;
using ThoughtWorks.TreeSurgeon.Core.Configuration;

namespace ThoughtWorks.TreeSurgeon.UnitTests.TestUtils {
	class MockTemplateSettings: TemplateSettings {

		public static new TemplateSettings Current {
			get {return new MockTemplateSettings();}
		}

		public MockTemplateSettings () {
			this["resourceRoot"] = ".";
			MockVelocityContextCollection velocityContext = new MockVelocityContextCollection();
			this["velocityContext"] = velocityContext;
			velocityContext.Add("coreGuid", "{GUID}");
			velocityContext.Add("unitTestGuid", "{GUID}");
			velocityContext.Add("consoleGuid", "{GUID}");
			velocityContext.Add("fitFixturesGuid", "{GUID}");
			velocityContext.Add("solutionItemsGuid", "{GUID}");
			velocityContext.Add("buildHelpersGuid", "{GUID}");
			velocityContext.Add("nantDeleteClause", "${directory::exists(build.dir)}");


			MockTemplateCollection templates = new MockTemplateCollection();
			this["templates"] = templates;
			MockTemplate template = templates.AddNew("VS2005", "Visual Studio Project");
			template.AddSkeletonDirectory("resources\\skeleton");
			template.AddTemplateFile(@"go.bat");
			template.AddTemplateFile(@"{0}.build");
			template.AddTemplateFile(@"StartNUnitGUI.bat");
			template.AddTemplateFile(@"RunAcceptanceTests.bat");
			template.AddTemplateFile(@"src\{0}.sln");
			template.AddTemplateFile(@"src\Console\Console.csproj");
			template.AddTemplateFile(@"src\Console\AssemblyInfo.cs");
			template.AddTemplateFile(@"src\Console\HelloWorld.cs");
			template.AddTemplateFile(@"src\Core\Core.csproj");
			template.AddTemplateFile(@"src\Core\AssemblyInfo.cs");
			template.AddTemplateFile(@"src\Core\ApplicationInfo.cs");
		}

		class MockVelocityContextCollection : VelocityContextCollection {
			public void Add( string name, string value) {
				BaseAdd(new VelocityContextItem(name, value));
			}
		}
		
		class MockTemplateCollection : TemplateCollection {
			public MockTemplate AddNew( string name, string description ) {
				MockTemplate template = new MockTemplate(name, description);
				BaseAdd(template);
				return template;
			}
		}

		class MockTemplate : Template {

			MockSkeletonDirectories skeletons;
			MockTemplateFiles templateFiles;


			public MockTemplate(string name, string description):base() {
				this["name"] = name;
				this["description"] = description;
				skeletons = new MockSkeletonDirectories();
				this["skeletonDirectories"] = skeletons;
				templateFiles = new MockTemplateFiles("testtemplates\\2005");
				this["templateFiles"] = templateFiles;
			}

			public void AddSkeletonDirectory( string path ) {
				skeletons.Add(path);
			}

			public void AddTemplateFile( string path ) {
				templateFiles.Add(path);
			}
		}

		class MockSkeletonDirectories : SkeletonDirectories {
			public void Add( string path) {
				BaseAdd(new ResourceDirectory(path));
			}
		}

		class MockTemplateFiles : TemplateFiles {
			public MockTemplateFiles(string directory) {
				this["directory"] = directory;
			}

			public void Add( string path ) {
				BaseAdd(new TemplateFile(path));
			}
		}



	}
}
