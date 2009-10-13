using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;


namespace ThoughtWorks.TreeSurgeon.Core.Configuration {

	public class TemplateSettings : ConfigurationSection {

		public static TemplateSettings Current {
			get {
				return (TemplateSettings)ConfigurationManager.GetSection("templateConfiguration/templateSettings");
			}
		}

		[ConfigurationProperty("resourceRoot")]
		public string ResourceRoot {
			get { return (string)this["resourceRoot"]; }
		}


		[ConfigurationProperty("velocityContext", IsDefaultCollection=true)]
		public VelocityContextCollection VelocityContext {
			get { return (VelocityContextCollection)this["velocityContext"]; }
		}

		[ConfigurationProperty("templates", IsDefaultCollection=true)]
		public TemplateCollection Templates {
			get { return (TemplateCollection)this["templates"]; }
		}

		public class VelocityContextCollection : ConfigurationElementCollection<VelocityContextItem> {
			protected override string GetElementKey( VelocityContextItem element ) {
				return element.Name;
			}
			protected override string ElementName {
				get { return "item"; }
			}
		}

		public class VelocityContextItem : ConfigurationElement {

			public VelocityContextItem() {}

			public VelocityContextItem(string name, string value) {
				this["name"] = name;
				this["value"] = value;
			}

			[ConfigurationProperty("name")]
			public string Name {
				get { return (string)this["name"]; }
			}
			[ConfigurationProperty("value")]
			public string Value {
				get { return (string)this["value"]; }
			}

			public bool GenerateGuid {
				get { return Value.ToUpper() == "{GUID}"; }
			}
			
		}

		public class TemplateCollection : ConfigurationElementCollection<Template> {

			protected override string ElementName {
				get { return "template"; }
			}

			protected override string GetElementKey( Template element ) {
				return element.Name;
			}

			public new Template this[string name] {
				get { return (Template)BaseGet(name); }
			}
		}

		public class Template : ConfigurationElement {

			[ConfigurationProperty("name")]
			public string Name {
				get { return (string)this["name"]; }
			}

			[ConfigurationProperty("description", IsRequired=false)]
			public string Description {
				get { return (string)this["description"]; }
			}

			[ConfigurationProperty("skeletonDirectories", IsDefaultCollection=true)]
			public SkeletonDirectories SkeletonDirectories {
				get { return (SkeletonDirectories)this["skeletonDirectories"]; }
			}

			[ConfigurationProperty("templateFiles", IsRequired=false)]
			public TemplateFiles TemplateFiles {
				get { return (TemplateFiles)this["templateFiles"]; }
			}

			[ConfigurationProperty("templateDirectories", IsRequired=false)]
			public TemplateDirectories TemplateDirectories {
				get { return (TemplateDirectories)this["templateDirectories"]; }
			}


		}

		public class SkeletonDirectories : ConfigurationElementCollection<ResourceDirectory> {

			protected override string ElementName {
				get { return "directory"; }
			}

			protected override string GetElementKey( ResourceDirectory element ) {
				return element.Path;
			}

		}

		public class TemplateDirectories : ConfigurationElementCollection<ResourceDirectory> {

			protected override string ElementName {
				get { return "directory"; }
			}

			protected override string GetElementKey(ResourceDirectory element) {
				return element.Path;
			}

		}


		public class ResourceDirectory : ConfigurationElement {

			public ResourceDirectory() {}

			public ResourceDirectory( string path ) {
				this["path"] = path;
			}

			[ConfigurationProperty("path")]
			public string Path {
				get { return (string)this["path"]; }
			}

		}

		public class TemplateFiles : ConfigurationElementCollection<TemplateFile> {

			[ConfigurationProperty("directory")]
			public string Directory {
			    get { return (string)this["directory"]; }
			}

			protected override string ElementName {
				get { return "file"; }
			}

			protected override string GetElementKey( TemplateFile element ) {
				return element.Path;
			}
		}

		public class TemplateFile : ConfigurationElement {

			public TemplateFile():base() { }

			public TemplateFile( string path ):this() {
				this["path"] = path;
			}

			public TemplateFile( string path, string targetPath ):this(path) {
				this["targetPath"] = targetPath;
			}

			[ConfigurationProperty("path")]
			public string Path {
				get { return (string)this["path"]; }
			}

			[ConfigurationProperty("targetPath")]
			public string TargetPath {
				get {
					if(this.Properties.Contains("targetPath") && (string)this["targetPath"] != string.Empty)
						return (string)this["targetPath"];
					else
						return (string)this["path"];
				}
			}
		}

	
	}
}
