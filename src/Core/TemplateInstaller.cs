using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using ThoughtWorks.TreeSurgeon.Core.Configuration;
using System.IO;

namespace SQS.Testify.Core {
    public class TemplateInstaller {
        private string workingDirectory;
        private string targetDirectory;

        public TemplateInstaller(string workingDirectory, string targetDirectory) {
            // TODO: Complete member initialization
            this.workingDirectory = workingDirectory;
            this.targetDirectory = targetDirectory;
        }

        public void Install(string fileName) {
            FastZip fastZip = new FastZip();
            fastZip.ExtractZip(fileName, targetDirectory, "");
   
            String configFile = Path.Combine(targetDirectory, Path.GetFileNameWithoutExtension(fileName)+ ".config");
            if (File.Exists(configFile)) {
                ConfigurationMerger merger = new ConfigurationMerger(System.AppDomain.CurrentDomain.FriendlyName + ".config");
                merger.Merge(configFile);
            }
        }
    }
}
