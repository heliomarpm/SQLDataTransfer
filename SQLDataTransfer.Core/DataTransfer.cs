
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace SQLDataTransfer.Core
{
    public class DataTransfer : ConnectionBase
    {
        public struct BulkConfig
        {
            public SqlBulkCopyOptions Options;
            public int CopyTimeout;
            public int BatchSize;
        }

        private string _connectionStringSource;
        private string _connectionStringTarget;
        private long _rowsCopied;

        public DataTransfer(string connectionStringSource, string connectionStringTarget)
        {
            this._connectionStringSource = connectionStringSource;
            this._connectionStringTarget = connectionStringTarget;
        }

        private ConnectionHelper _connectSource;
        public ConnectionHelper ConnectSource
        {
            get
            {
                try
                {
                    if (_connectSource == null)
                        _connectSource = base.Connect(_connectionStringSource);

                    return _connectSource;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro na conexão de ORIGEM: {Environment.NewLine}{ex.Message}");
                }
            }
        }

        private ConnectionHelper _connectTarget;
        public ConnectionHelper ConnectTarget
        {
            get
            {
                try
                {
                    if (_connectTarget == null)
                        _connectTarget = base.Connect(_connectionStringTarget);

                    return _connectTarget;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro na conexão de DESTINO: {Environment.NewLine}{ex.Message}");
                }
            }
        }

        public void Disconnect()
        {
            if (_connectSource != null)
                ConnectSource.Disconnect();

            if (_connectTarget != null)
                ConnectTarget.Disconnect();
        }

        public Dictionary<string, string> GetTables()
        {
            DataSet ds = ConnectSource.ExecuteDataset(Properties.Resources.get_tables);
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (ds != null && ds.Tables.Count > 0)
                foreach (DataRow dr in ds.Tables[0].Rows)
                    ret.Add(dr[0].ToString(), dr[1].ToString());

            return ret;
        }

        // Vai permitir informar o valor do campo Identity da Tabela
        public void EnableSetIdentity(string tableName)
        {
            var parametros = new List<IDataParameter>
            {
                new SqlParameter("@NOMETB", tableName)
            };
            ConnectTarget.ExecuteScalar(Properties.Resources.enable_set_identity, CommandType.Text, parametros);
        }

        // Reativa o codigo automatico da tabela
        public void DisableSetIdentity(string tableName)
        {
            var parametros = new List<IDataParameter>
            {
                new SqlParameter("@NOMETB", tableName)
            };
            ConnectTarget.ExecuteScalar(Properties.Resources.disable_set_identity, CommandType.Text, parametros);
        }

        public int EnableTriggers(string tableName)
        {
            var parametros = new List<IDataParameter>
            {
                new SqlParameter("@NOMETB", tableName)
            };
            return (int)ConnectTarget.ExecuteScalar(Properties.Resources.enable_triggers, CommandType.Text, parametros);
        }
        public int DisableTriggers(string tableName)
        {
            var parametros = new List<IDataParameter>
            {
                new SqlParameter("@NOMETB", tableName)
            };
            return (int)ConnectTarget.ExecuteScalar(Properties.Resources.disable_triggers, CommandType.Text, parametros);
        }

        public int EnableConstraints()
        {
            return (int)ConnectTarget.ExecuteScalar(Properties.Resources.enable_constraints, CommandType.Text);
        }

        public int DisableConstraints()
        {
            return (int)ConnectTarget.ExecuteScalar(Properties.Resources.disable_constraints, CommandType.Text);
        }

        public void UpdateStatistics(string nometb)
        {
            var parametros = new List<IDataParameter>
            {
                new SqlParameter("@NOMETB", nometb)
            };
            ConnectTarget.ExecuteScalar(Properties.Resources.update_statistics, CommandType.Text, parametros);
        }

        public void TruncateTable(string nometb)
        {
            var parametros = new List<IDataParameter>
            {
                new SqlParameter("@NOMETB", nometb)
            };
            ConnectTarget.ExecuteScalar(Properties.Resources.truncate, CommandType.Text, parametros, 0);
        }

        public void Reseed(string nometb) //, string nomechave)
        {
            var parametros = new List<IDataParameter>
            {
                new SqlParameter("@NOMETB", nometb)
            };
            //parametros.Add(new SqlParameter("@NOMECHAVE", nomechave));
            ConnectTarget.ExecuteScalar(Properties.Resources.checkident, CommandType.Text, parametros);
        }

        public void Reseed()
        {
            ConnectTarget.ExecuteScalar(Properties.Resources.checkident_all_tables);
        }

        public long BulkCopy(string pNomeTabela)
        {
            return BulkCopy(pNomeTabela, true);
        }

        public long BulkCopy(string pNomeTabela, bool pTableLock)
        {
            return BulkCopy(pNomeTabela, pTableLock, false);
        }

        public long BulkCopy(string pNomeTabela, bool pTableLock, bool pCheckConstraints)
        {
            return BulkCopy(pNomeTabela, pTableLock, pCheckConstraints, false);
        }

        public long BulkCopy(string pNomeTabela, bool pTableLock, bool pCheckConstraints, bool pFireTriggers)
        {
            return BulkCopy(pNomeTabela, pTableLock, pCheckConstraints, pFireTriggers, 0);
        }

        public long BulkCopy(string pNomeTabela, bool pTableLock, bool pCheckConstraints, bool pFireTriggers, int pCopyTimeout)
        {
            return BulkCopy(pNomeTabela, pTableLock, pCheckConstraints, pFireTriggers, pCopyTimeout, 0);
        }

        public long BulkCopy(string pNomeTabela, bool pTableLock, bool pCheckConstraints, bool pFireTriggers, int pCopyTimeout, int pBatchSize)
        {
            var config = new BulkConfig
            {
                CopyTimeout = pCopyTimeout,
                BatchSize = pBatchSize,
                Options = SqlBulkCopyOptions.KeepIdentity
            };
            if (pTableLock) config.Options |= SqlBulkCopyOptions.TableLock;
            if (pFireTriggers) config.Options |= SqlBulkCopyOptions.FireTriggers;
            if (pCheckConstraints) config.Options |= SqlBulkCopyOptions.CheckConstraints;

            return BulkCopy(pNomeTabela, $"SELECT * FROM {pNomeTabela} WITH(NOLOCK)", config);
        }

        public long BulkCopy(string pNomeTabela, string pSqlCommand, BulkConfig pBulkConfig)
        {
            _rowsCopied = 0;
            try
            {
                using (var reader = (SqlDataReader)ConnectSource.ExecuteReader(pSqlCommand))
                {
                    if (reader.HasRows)
                        using (var bulkCopy = new SqlBulkCopy(ConnectTarget.Connection, pBulkConfig.Options, null))
                        {
                            bulkCopy.SqlRowsCopied += BulkCopy_SqlRowsCopied;
                            bulkCopy.NotifyAfter = 1;
                            bulkCopy.BulkCopyTimeout = pBulkConfig.CopyTimeout;
                            bulkCopy.BatchSize = pBulkConfig.BatchSize;
                            bulkCopy.DestinationTableName = pNomeTabela;
                            bulkCopy.WriteToServer(reader);
                        }

                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _rowsCopied;
        }

        private void BulkCopy_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            _rowsCopied = e.RowsCopied;
        }

        public string GenInsertScript(string pNomeTabela)
        {
            using (IDataReader dr = ConnectSource.ExecuteReader(string.Format("SELECT * FROM {0} WITH(NOLOCK)", pNomeTabela)))
            {
                try
                {
                    string result = "";
                    string values;
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Insert");

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    path = Path.Combine(path, pNomeTabela + ".sql");

                    if (File.Exists(path))
                        File.Delete(path);

                    using (var sw = new StreamWriter(path, false, Encoding.ASCII))
                    {
                        while (dr.Read())
                        {
                            values = "";
                            for (int i = 0; i < dr.FieldCount; i++)
                            {
                                if (!object.ReferenceEquals(dr[i], DBNull.Value))
                                {
                                    values += "," + GetFormatDataValue(dr[i].GetType(), dr[i].ToString());
                                }
                                else
                                {
                                    values += ", NULL";
                                }
                            }
                            if (result != "")
                                result += ", (" + values.Substring(1) + ")" + Environment.NewLine;
                            else
                            {
                                result = "INSERT INTO [" + pNomeTabela + "]" + Environment.NewLine;
                                result += " VALUES (" + values.Substring(1) + ")" + Environment.NewLine;
                            }
                            result += "GO\n";
                            sw.WriteLine(result);
                            result = "";
                        }
                        sw.Flush();
                        sw.Close();
                        sw.Dispose();
                    }

                    return path;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (!dr.IsClosed) dr.Close();

                    dr.Dispose();
                }
            }
        }

        private static string GetFormatDataValue(System.Type pTypeValue, String pValue)
        {
            string result;
            pValue = pValue.Trim();
            try
            {
                switch (pTypeValue.Name.ToUpper())
                {
                    case "BIGINT":
                    case "INTEGER":
                    case "SMALLINT":
                    case "TINYINT":
                    case "UNSIGNEDBIGINT":
                    case "UNSIGNEDINT":
                    case "UNSIGNEDSMALLINT":
                    case "UNSIGNEDTINYINT":
                    case "INT16":
                    case "INT32":
                    case "BYTE":
                        result = pValue;
                        break;
                    case "CURRENCY":
                        result = pValue.Replace(",", ".");
                        break;
                    case "DOUBLE":
                    case "FLOAT":
                    case "DECIMAL":
                    case "NUMERIC":
                        result = pValue.Replace(",", ".");
                        break;
                    case "BSTR":
                    case "CHAR":
                    case "VARCHAR":
                    case "STRING":
                        result = string.Concat("'", pValue.Replace("'", "''"), "'");
                        break;
                    case "DATE":
                    case "DBDATE":
                    case "DBTIME":
                    case "DBTIMESTAMP":
                    case "DATETIME":
                        DateTime dt = new DateTime();
                        if (DateTime.TryParse(pValue, out dt))
                        {
                            result = string.Format("'{0}-{1}-{2}'", dt.Year, dt.Month, dt.Day);
                        }
                        else
                            result = "NULL";
                        break;
                    default:
                        result = "NULL";
                        break;
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int ExportCsvFile(string querySelect, string pathFileName, bool includeHeader = true, string separador = ";", int commandTimeout = 1200)
        {
            int rcount = 0;

            try
            {
                using (var dr = ConnectSource.ExecuteReader(querySelect, commandTimeout))
                {
                    List<string> rows = dr.ToList(includeHeader, separador);
                    rcount = rows.Count;

                    if (rcount > 0)
                    {
                        string path = ValidationPathFile(pathFileName, true);

                        if (File.Exists(path))
                            File.Delete(path);

                        using (var sw = new StreamWriter(path, false, Encoding.Default))
                        {
                            rows.ForEach(f => sw.WriteLine(f));
                            sw.Flush();
                            sw.Close();
                            sw.Dispose();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return rcount;
        }

        private static string ValidationPathFile(string pathFileName, bool createDirectory)
        {
            string fileName = pathFileName.Substring(pathFileName.LastIndexOf(@"\", StringComparison.OrdinalIgnoreCase) + 1);
            string path = pathFileName.Replace(fileName, "");
            //Se não for caminho de rede ou caminho absoluto então utiliza o path do app
            if (path.IndexOf(@"\\", 0, 2, StringComparison.OrdinalIgnoreCase) == -1 &&
                path.IndexOf(@":", 1, 1, StringComparison.OrdinalIgnoreCase) == -1)
            {
                //se existir remove a primeira barra invertida antes de combinar o Path
                path = path.Substring(path.IndexOf(@"\", 0, 1, StringComparison.OrdinalIgnoreCase) + 1);
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            }

            if (createDirectory && !Directory.Exists(path))
                Directory.CreateDirectory(path);

            return Path.Combine(path, fileName);
        }

        public string ConnStrSource
        {
            get { return _connectionStringSource; }
            set { _connectionStringSource = value; }
        }
        public string ConnStrTarget
        {
            get { return _connectionStringTarget; }
            set { _connectionStringTarget = value; }
        }
        public string DatabaseSource
        {
            get { return ConnectSource.Connection.Database; }
            //private set { _connOrig = value; }
        }
        public string DatabaseTarget
        {
            get { return ConnectTarget.Connection.Database; }
            //set { _connDest = value; }
        }
        public bool Connected
        {
            get
            {
                bool ret = ConnectSource.Connection.State == System.Data.ConnectionState.Open;
                //Se vou apenas gerar arquivos não preciso de conexao de destino
                if (string.IsNullOrEmpty(_connectionStringTarget))
                    ret = ret && ConnectTarget.Connection.State == System.Data.ConnectionState.Open;
                return ret;
            }
        }
    }
}