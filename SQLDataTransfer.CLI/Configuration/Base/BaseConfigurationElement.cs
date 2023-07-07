using System;
using System.Configuration;

namespace SQLDataTransfer.CLI.Configuration
{
    public class BaseConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("name")]
        public String Name
        {
            get { return (String)this["name"]; }
            set { this["name"] = value; }
        }
    }
}
