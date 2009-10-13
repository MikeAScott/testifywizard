using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtWorks.TreeSurgeon.Core {
	public class FileTemplate {

		private string childPathTemplate;
		public string ChildPathTemplate {
			get { return childPathTemplate; }
		}

		private string name;
		public string Name {
			get { return name; }
		}

		public static FileTemplate Parse(string csvPair) {
			try {
				string[] pair = csvPair.Split(',');
				return new FileTemplate(pair[0], pair[1]);
			} catch (IndexOutOfRangeException) {
				return new FileTemplate(csvPair, csvPair);
			}
		}
		
		public FileTemplate(string fileTemplateName, string childPathTemplate) {
			this.childPathTemplate = childPathTemplate.Trim();
			this.name = fileTemplateName.Trim();
		}

	}
}
