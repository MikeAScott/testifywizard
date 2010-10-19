using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ThoughtWorks.TreeSurgeon.Core.Configuration;
using System.Configuration;
using System.IO;

namespace ThoughtWorks.TreeSurgeon.UnitTests.Core {
    [TestFixture]
    public class ConfigurationMergerTests {

        [SetUp]
        public void SetUp() {
            File.Copy("original.test.config", "test.config", true);
        }

        [Test]
        public void MergerIsCreatedWithExistingTemplateSettings() {
            ConfigurationMerger merger = new ConfigurationMerger("test.config");
            Assert.AreEqual("test.config", merger.ConfigFile);
        }

        [Test]
        public void BeforeMergeConfigHasOneTemplate() {
            TemplateSettings templates = getTemplateSettingsConfig("test.config");
            Assert.AreEqual(1, templates.Templates.Count);
        }

        private static TemplateSettings getTemplateSettingsConfig(String configFile) {
            var map = new ExeConfigurationFileMap { ExeConfigFilename = configFile };
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            TemplateSettings templates = (TemplateSettings)config.GetSection("templateConfiguration/templateSettings");
            return templates;
        }

        [Test]
        public void AfterMergeConfigHasTwoTemplates() {
            ConfigurationMerger merger = new ConfigurationMerger("test.config");
            merger.Merge("sample.config");
            TemplateSettings templates = getTemplateSettingsConfig("test.config");
            Assert.AreEqual(2, templates.Templates.Count);
        }


    }
}
