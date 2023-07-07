using System;
using System.Configuration;
using System.IO;

namespace SQLDataTransfer.CLI.Configuration
{
    public class TablesElement : BaseConfigurationElement
    {
        [ConfigurationProperty("selectSource", IsRequired = false)]
        private string SelectSource
        {
            get { return (string)this["selectSource"]; }
        }

        public string SelectSourceFileName
        {
            get
            {
                var selectSource = this.SelectSource;
                return selectSource.Substring(selectSource.LastIndexOf(@"\", StringComparison.OrdinalIgnoreCase) + 1);
            }
        }

        /// <summary>
        /// Retornar a query SQL para ser executada durante a copia de dados
        /// </summary>
        public string SelectAll
        {
            get
            {
                string sql = this.SelectSource;
                if (string.IsNullOrEmpty(sql))
                    sql = $"select * from {this.SourceName} with(nolock)";
                else
                    sql = File.ReadAllText(sql);

                return sql;
            }
        }

        /// <summary>
        /// Irá retorno o valor encontrado na chave na seguinte ordem:
        ///     1º: nome do arquivo da chave [selectSource]
        ///     2º: chave [sourceName]
        ///     3º: chave [name]
        /// </summary>
        [ConfigurationProperty("sourceName", IsRequired = false)]
        public string SourceName
        {
            get
            {
                string fileName = this.SelectSourceFileName;
                string sourceName = !string.IsNullOrEmpty(fileName) ? fileName : (string)this["sourceName"];
                return !string.IsNullOrEmpty(sourceName) ? sourceName : base.Name;
            }
        }

        [ConfigurationProperty("truncate", IsRequired = false)]
        public bool Truncate
        {
            get { return (bool)this["truncate"]; }
        }

        [ConfigurationProperty("toCsvFile", IsRequired = false)]
        public string ToCsvFile
        {
            get { return (string)this["toCsvFile"]; }
            set
            {
                this["toCsvFile"] = value.Replace("{DateTime}", DateTime.Now.ToString("yyyyMMdd_HHmmss"))
                                         .Replace("{Date}", DateTime.Now.ToString("yyyyMMdd"))
                                         .Replace("{Time}", DateTime.Now.ToString("HHmmss"));
            }
        }

        [ConfigurationProperty("updateStatistics", IsRequired = false)]
        public bool UpdateStatistics
        {
            get { return (bool)this["updateStatistics"]; }
        }

    }
}

