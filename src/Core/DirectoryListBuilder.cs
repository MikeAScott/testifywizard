using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ThoughtWorks.TreeSurgeon.Core {
	class DirectoryListBuilder {
		readonly DirectoryInfo rootDirectory;
		readonly DirectoryInfo directory;
		public DirectoryListBuilder( DirectoryInfo directory, DirectoryInfo rootDirectory ) {
			this.directory = directory;
			this.rootDirectory = rootDirectory;
		}

		public DirectoryListBuilder(string directory, string rootDirectory) {
			if (!directory.EndsWith(@"\"))
				directory += @"\";
			this.directory = new DirectoryInfo(directory);
			if (!rootDirectory.EndsWith(@"\"))
				rootDirectory += @"\";
			this.rootDirectory = new DirectoryInfo(rootDirectory);
		}


		public FileTemplate[] Build() {
			List<FileTemplate> list = new List<FileTemplate>();
			Build(directory, list);
			return list.ToArray();
		}

		private void Build( DirectoryInfo parentDir, List<FileTemplate> list ) {
			foreach(FileInfo file in parentDir.GetFiles()) {
				string sourceFile = GetRelativePath(file, this.rootDirectory);
				sourceFile = ensureNotVmFile(sourceFile);
				string destFile = GetRelativePath(file, this.directory);
				destFile = ensureNotVmFile(destFile);
				list.Add(new FileTemplate(sourceFile,destFile));
			}
			foreach(DirectoryInfo dir in parentDir.GetDirectories()) {
				if(NotASvnDirectory(dir))
					Build(dir, list);
			}
		}

		private static string ensureNotVmFile(string sourceFile) {
			if (sourceFile.EndsWith(@".vm"))
				sourceFile = sourceFile.Substring(0, sourceFile.Length - 3);
			return sourceFile;
		}

		private bool NotASvnDirectory( DirectoryInfo dir ) {
			return !dir.FullName.EndsWith(".svn");
		}

		private string GetRelativePath(FileSystemInfo file, DirectoryInfo rootPath) {
			string relativePath = file.FullName.Replace(rootPath.FullName, string.Empty);
			return relativePath;
		}
	}

}
