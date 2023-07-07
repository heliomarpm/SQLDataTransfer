using System.Configuration;

namespace SQLDataTransfer.CLI.Configuration
{
    public sealed class TransferSection : ConfigurationSection
    {
        [ConfigurationProperty("transfers", IsRequired = true)]
        [ConfigurationCollection(typeof(TransfersElement), AddItemName = "add")]
        public BaseConfigurationCollection<TransfersElement> Transfers
        {
            get
            {
                return (BaseConfigurationCollection<TransfersElement>)this["transfers"];
            }
        }

        public static void CreateSection()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Create the section entry  
            // in the <configSections> and the 
            // related target section in <configuration>.
            if (config.Sections["transferSection"] == null)
            {
                var customSection = new TransferSection();
                config.Sections.Add("transferSection", customSection);
                customSection.SectionInformation.ForceSave = true;
                config.Save(ConfigurationSaveMode.Full);
            }
        }

        public static TransferSection GetSection()
        {
            CreateSection();
            return ConfigurationManager.GetSection("transferSection") as TransferSection;
        }

    }
}
