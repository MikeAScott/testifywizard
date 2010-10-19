using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Data;

namespace ThoughtWorks.TreeSurgeon.Core.Configuration {
    public class ConfigurationMerger {
        private string configFile;

        public ConfigurationMerger(String configFile) {
            this.configFile = configFile;
        }

        public string ConfigFile {
            get {
                return configFile;
            }
        }

        public void Merge(string mergeFile) {
            XmlDocument config = new XmlDocument();
            config.Load(configFile);
            XmlDocument merge = new XmlDocument();
            merge.Load(mergeFile);

            mergeConfig(config, merge, "//templates");
            mergeConfig(config, merge, "//velocityContext");
            config.Save(configFile);        
        }

        private static void mergeConfig(XmlDocument config, XmlDocument merge, string nodesXpath) {
            XmlNode configNodes = config.SelectSingleNode(nodesXpath);
            foreach (XmlNode mergeItem in merge.SelectNodes(nodesXpath+"/*")) {
                XmlNode newItem = config.ImportNode(mergeItem, true);
                configNodes.AppendChild(newItem);
            }
        }
    }
}
