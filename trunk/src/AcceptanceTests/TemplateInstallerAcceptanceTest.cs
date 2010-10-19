using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.IO;
using ThoughtWorks.TreeSurgeon.Core.Utils;
using ThoughtWorks.TreeSurgeon.Core.Configuration;
using System.Configuration;

namespace SQS.Testify.AcceptanceTests {
    [TestFixture]
    public class TemplateInstallerAcceptanceTest   {

        private DirectoryInfo wd;
        private string resourcesPath;

        [SetUp]
        public void Setup() {
            wd = new DirectoryInfo(Directory.GetCurrentDirectory());
            resourcesPath = Path.Combine(wd.FullName, "resources");
        }

        [Test]
        public void CanExtractTemplateFromZip()  {
            RunTemplateInstaller("sample.zip");
            CheckFileExistsAndIsBiggerThan("sample.config", 100);
            CheckFileExistsAndIsBiggerThan("skeleton/Sample/Sample.txt", 100);
            CheckConfigContainsTemplate("Sample");
        }

        private void CheckConfigContainsTemplate(string templateName) {
            TemplateSettings settings = getTemplateSettingsConfig("TestifyConsole.exe.config");
            Assert.IsNotNull(settings.Templates[templateName]);
        }


        [TearDown]
        public void TearDown() {
            string configPath = Path.Combine(resourcesPath,"sample.config");
            string skeletonSample = Path.Combine(resourcesPath, Path.Combine("skeleton", "Sample"));
            string templateSample = Path.Combine(resourcesPath, Path.Combine("templates", "Sample"));
            if (Directory.Exists(skeletonSample))
                Directory.Delete(skeletonSample, true);
            if (Directory.Exists(templateSample))
                Directory.Delete(templateSample, true);
            if (File.Exists(configPath))
                File.Delete(configPath);
        }


        private void RunTemplateInstaller(string fileName) {
            // This working dir stuff here for TREE-13
            ProcessInfo pi = new ProcessInfo("TestifyConsole.exe", "-i " + fileName);
            ProcessResult installerResult = new ProcessExecutor().Execute(pi);
            if (installerResult.ExitCode != 0) {
                Console.WriteLine("TestifyConsole failed");
                Console.WriteLine("** Standard out from TestifyConsole.exe: **");
                Console.WriteLine(installerResult.StandardOutput);
                Console.WriteLine("** End of Standard out from TestifyConsole.exe: **\n");
                Console.WriteLine("Standard err:");
                Console.WriteLine(installerResult.StandardError);
                Assert.Fail("TestifyConsole console app failed");
            }
        }

        private void CheckFileExistsAndIsBiggerThan(string fileName, int fileSize) {
            string expectedLocation = Path.Combine(resourcesPath, fileName);
            Assert.IsTrue(File.Exists(expectedLocation));
            Assert.IsTrue(new FileInfo(expectedLocation).Length > fileSize);
        }


        private static TemplateSettings getTemplateSettingsConfig(String configFile) {
            var map = new ExeConfigurationFileMap { ExeConfigFilename = configFile };
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            TemplateSettings templates = (TemplateSettings)config.GetSection("templateConfiguration/templateSettings");
            return templates;
        }


    }
}
