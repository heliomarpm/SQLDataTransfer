using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDataTransfer.AppConsole.Configuration
{
    public class TransfersElement : BaseConfigurationElement
    {
        [ConfigurationProperty("disabled", IsRequired = false)]
        public bool Disabled
        {
            get { return (bool)this["disabled"]; }
            set { this["disabled"] = value; }
        }

        [ConfigurationProperty("connectionSource", IsRequired = true)]
        public string ConnectionSource
        {
            get { return (string)this["connectionSource"]; }
        }

        [ConfigurationProperty("connectionTarget", IsRequired = true)]
        public string ConnectionTarget
        {
            get { return (string)this["connectionTarget"]; }
        }

        [ConfigurationProperty("tableLock", IsRequired = false, DefaultValue = true)]
        public bool TableLock
        {
            get { return (bool)this["tableLock"]; }
            set { this["tableLock"] = value; }
        }

        [ConfigurationProperty("checkConstraints", IsRequired = false, DefaultValue = false)]
        public bool CheckConstraints
        {
            get { return (bool)this["checkConstraints"]; }
            set { this["checkConstraints"] = value; }
        }

        [ConfigurationProperty("fireTriggers", IsRequired = false, DefaultValue = false)]
        public bool FireTriggers
        {
            get { return (bool)this["fireTriggers"]; }
            set { this["fireTriggers"] = value; }
        }

        [ConfigurationProperty("bulkCopyTimeout", IsRequired = false, DefaultValue = 0)]
        public int BulkCopyTimeout
        {
            get { return (int)this["bulkCopyTimeout"]; }
            set { this["bulkCopyTimeout"] = value; }
        }

        [ConfigurationProperty("bulkBatchSize", IsRequired = false, DefaultValue = 5000)]
        public int BulkBatchSize
        {
            get { return (int)this["bulkBatchSize"]; }
            set { this["bulkBatchSize"] = value; }
        }

        [ConfigurationProperty("tables", IsRequired = false)]
        [ConfigurationCollection(typeof(TablesElement), AddItemName = "add")]
        public BaseConfigurationCollection<TablesElement> Tables => (BaseConfigurationCollection<TablesElement>)this["tables"];
    }
}
