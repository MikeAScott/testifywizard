using System.IO;

namespace ThoughtWorks.TreeSurgeon.Core.Generators.Content
{
	public class DefaultDirectoryCopier : IDirectoryCopier
	{
		public void CopyDirectory(string sourceDirectory, string targetDirectory)
		{
			DirectoryInfo sourceDirectoryInfo = new DirectoryInfo(sourceDirectory);
			DirectoryInfo targetDirectoryInfo = new DirectoryInfo(targetDirectory);

			if (targetDirectoryInfo.Exists)
			{
				targetDirectoryInfo.Create();
			}
			foreach (DirectoryInfo sourceChildDirectory in sourceDirectoryInfo.GetDirectories())
			{
				DirectoryInfo targetChildDirectory = targetDirectoryInfo.CreateSubdirectory(sourceChildDirectory.Name);
				CopyDirectory(sourceChildDirectory.FullName, targetChildDirectory.FullName);
			}
			foreach (FileInfo sourceFile in sourceDirectoryInfo.GetFiles())
			{
				sourceFile.CopyTo(Path.Combine(targetDirectoryInfo.FullName, sourceFile.Name));
			}
		}
	}
}
