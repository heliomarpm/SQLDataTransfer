using System;
using System.Configuration;

namespace SQLDataTransfer.AppConsole.Configuration
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
