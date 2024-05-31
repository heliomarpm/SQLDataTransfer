using SQLDataTransfer.CLI.Configuration;
using SQLDataTransfer.Core;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SQLDataTransfer.CLI
{
    internal class Program
    {
        private static Logger _logger = new Logger();
        private static DataTransfer _transfer;

        static void Main(string[] args)
        {
            try
            {
                var transferSection = TransferSection.GetSection();
                //Se grupo de transferencia foi passado como parametro, executa apenas determinado
                if (args.Length > 0)
                {
                    foreach (TransfersElement item in transferSection.Transfers)
                    {
                        item.Disabled = !args.Contains(item.Name);
                    }

                    var confirmarTransferencia = (args.Length > 1 && !(args[1].ToUpper() == "S" || args[1].ToUpper() == "Y"));

                    if (args.Length == 1 || confirmarTransferencia)
                    {
                        if (!ConfirmarTransferencia(transferSection))
                            return;
                    }
                }
                else if (args.Length == 0)
                {
                    //Se não foi passado nenhuma configuração como parametro então deve esperar ok do usuário
                    if (!ConfirmarTransferencia(transferSection))
                        return;
                }

                var tempoProcesso = new Stopwatch();
                var tempoEtapa = new Stopwatch();

                foreach (TransfersElement item in transferSection.Transfers)
                {
                    //local de armazenamento do log de execução
                    _logger = new Logger();
                    if (!item.Disabled)
                    {
                        tempoProcesso.Restart();
                        EscreverLogInfo(item);
                        _transfer = new DataTransfer(item.ConnectionSource, item.ConnectionTarget);
                        if (_transfer.Connected)
                        {
                            _logger.WriteLine('=');
                            _logger.WriteLog(string.Format("{0} BEGIN Transfer: {1} => {2}", DateTime.Now.ToString("dd.MM.yyyy"), _transfer.DatabaseSource, _transfer.DatabaseTarget), ConsoleColor.Blue, false);
                            _logger.WriteLine();

                            //setando parametros para execucao do bulk
                            var bulkConfig = new DataTransfer.BulkConfig
                            {
                                Options = SqlBulkCopyOptions.KeepIdentity
                            };

                            if (item.TableLock) bulkConfig.Options |= SqlBulkCopyOptions.TableLock;
                            if (item.CheckConstraints) bulkConfig.Options |= SqlBulkCopyOptions.CheckConstraints;
                            if (item.FireTriggers) bulkConfig.Options |= SqlBulkCopyOptions.FireTriggers;

                            bulkConfig.CopyTimeout = item.BulkCopyTimeout;
                            bulkConfig.BatchSize = item.BulkBatchSize;

                            IniciarTransferencia(item.Tables, bulkConfig);

                            /*
                            var tasks = new Task[item.Tables.Count];
                            for (int i = 0; i < tasks.Count(); i++)
                            {
                                tasks[i] = new Task((object element) =>
                                {
                                    IniciarTransferencia((TablesConfigurationElement)element);
                                },
                                item.Tables[i]);
                            }
                            Parallel.ForEach(tasks, (t) =>
                            {
                                t.Start();
                            });
                            Task.WaitAll(tasks);
                            */

                            //_transfer.Reseed();
                            //EscreverMemo("Reseed Identitys: " + tempo.Elapsed.ToString());
                            //EscreverMemo(new String('-', 80) + Environment.NewLine);
                        }
                        else
                        {
                            _logger.WriteError("FALHA NA CONEXÃO, VERIFIQUE AS CONFIGURAÇÕES");
                        }
                        tempoProcesso.Stop();

                        _logger.WriteLine();
                        _logger.WriteLog($"{DateTime.Now:dd.MM.yyyy} END Transfer. {tempoProcesso.Elapsed}", ConsoleColor.Blue, false);
                        _logger.WriteLine('=');
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.WriteError(ex.Message);
            }
            finally
            {
                if (_transfer != null)
                {
                    _transfer.Disconnect();
                    _transfer = null;
                }
            }
            Console.Read();
        }

        private static Boolean ConfirmarTransferencia(TransferSection envConfig)
        {
            Console.WriteLine("<<< TRANSFERÊNCIAS CONFIGURADAS >>>");
            foreach (TransfersElement item in envConfig.Transfers)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Transfer: {0}", item.Name);

                if (item.Disabled)
                {
                    WriteLineRG($"\t Enabled: ", !item.Disabled);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("\t Bulk Copy Timeout: {0}", item.BulkCopyTimeout);
                    Console.WriteLine("\t Bulk Batch Size: {0}", item.BulkBatchSize);
                    WriteLineRG("\t Bulk Table Lock: ", item.TableLock);
                    WriteLineRG("\t Bulk Check Constraints: ", item.CheckConstraints);
                    WriteLineRG("\t Bulk Fire Triggers: ", item.FireTriggers);
                    Console.WriteLine();
                    Console.WriteLine("\t Tables Count: {0}", item.Tables.Count);
                }

                Console.WriteLine(new String('-', 80) + Environment.NewLine);
            }

            Console.WriteLine("Pressione [ENTER] para CONTINUAR");

            return Console.ReadKey().Key == ConsoleKey.Enter;
        }

        private static void IniciarTransferencia(BaseConfigurationCollection<TablesElement> tables, DataTransfer.BulkConfig bulkConfig)
        {
            var tempo = new System.Diagnostics.Stopwatch();
            int rcount;

            bool desativarFKs = (!bulkConfig.Options.HasFlag(SqlBulkCopyOptions.CheckConstraints));
            bool desativarTriggers = (!bulkConfig.Options.HasFlag(SqlBulkCopyOptions.FireTriggers));

            if (desativarFKs)
            {
                tempo.Start();
                _logger.WriteLog("DesativarFKs iniciado...");
                rcount = _transfer.DisableConstraints();
                tempo.Stop();
                if (rcount == 0)
                {
                    desativarFKs = false;
                    _logger.WriteLog($"\t Nenhuma ForeingKey existente para desativação! {tempo.Elapsed}");
                }
                else
                    _logger.WriteLog($"\t {rcount.ToString("n0")} ForeingKey's desativadas! {tempo.Elapsed}");
                
                _logger.WriteLine();
            }

            #region [ TRANSFERENCIA DOS DADOS ]           
            foreach (TablesElement table in tables)
            {
                string msg = "IniciarTransferencia: ";
                try
                {
                    if (!string.IsNullOrEmpty(table.ToCsvFile))
                        _logger.WriteLog($"Exportação da fonte de dados \"{table.SourceName}\"", ConsoleColor.DarkGreen);
                    else if (string.Compare(table.Name, table.SourceName, StringComparison.OrdinalIgnoreCase) == 0)
                        _logger.WriteLog($"Transferindo dados para a tabela \"{table.Name}\"", ConsoleColor.DarkGreen);
                    else
                        _logger.WriteLog($"Transferindo dados de \"{table.SourceName}\" para \"{table.Name}\"", ConsoleColor.DarkGreen);

                    if (!string.IsNullOrEmpty(table.ToCsvFile))
                    {
                        msg = $"Exportação da fonte de dados \"{table.SourceName}\"";
                        tempo.Restart();
                        rcount = _transfer.ExportCsvFile(table.SelectAll, table.ToCsvFile, true, ";", bulkConfig.CopyTimeout);
                        tempo.Stop();
                        _logger.WriteLog($"\t Arquivo \"{table.ToCsvFile}\" criado com {rcount:n0} linhas! {tempo.Elapsed}");
                    }
                    else
                    {
                        //if (desativarTriggers)
                        //{
                        //    msg = "Desativação das triggers";
                        //    tempo.Restart();
                        //    rcount = _transfer.DesativarTriggers(table.Name);
                        //    tempo.Stop();
                        //    if (rcount == 0)
                        //    {
                        //        desativarTriggers = false;
                        //        EscreverLog($"\t Nenhuma Trigger existente para desativação! {tempo.Elapsed}");
                        //    }
                        //    else
                        //        EscreverLog($"\t {rcount.ToString("n0")} Trigger's desativadas! {tempo.Elapsed}");
                        //}

                        if (table.Truncate)
                        {
                            msg = "Exclusão de dados";
                            tempo.Restart();
                            _transfer.TruncateTable(table.Name);
                            tempo.Stop();
                            _logger.WriteLog($"\t {msg} finalizado! {tempo.Elapsed}");
                        }

                        msg = "Copia de dados";
                        tempo.Restart();
                        _logger.WriteLog($"\t Transferência de {_transfer.BulkCopy(table.Name, table.SelectAll, bulkConfig):n0} registros finalizado! {tempo.Elapsed}");
                        tempo.Stop();

                        msg = "Re-Sequênciamento de campo Identity";
                        tempo.Restart();
                        _transfer.Reseed(table.Name);
                        tempo.Stop();

                        _logger.WriteLog($"\t {msg} finalizado! {tempo.Elapsed}");
                        if (table.UpdateStatistics)
                        {
                            msg = "Atualização de estatisticas";
                            tempo.Restart();
                            _transfer.UpdateStatistics(table.Name);
                            tempo.Stop();
                            _logger.WriteLog($"\t {msg} finalizado! {tempo.Elapsed}");
                        }

                        //if (desativarTriggers)
                        //{
                        //    msg = "Ativação das Triggers";
                        //    tempo.Restart();
                        //    _logger.WriteLog($"{_transfer.enableTriggers(table.Name).ToString("n0")} Trigger's ativadas! {tempo.Elapsed}");
                        //    tempo.Stop();
                        //}
                    }
                }
                catch (Exception ex)
                {
                    _logger.WriteError(msg + " " + ex.Message);
                }
                finally
                {
                    _logger.WriteLine();
                }
            }
            #endregion
            if (desativarFKs)
            {
                _logger.WriteLog("AtivarFKs iniciado...");
                tempo.Start();
                _logger.WriteLog($"{_transfer.EnableConstraints():n0} ForeingKey's ativadas! {tempo.Elapsed}");
                tempo.Stop();
                _logger.WriteLine();
            }
        }

        private static void EscreverLogInfo(TransfersElement item)
        {
            var sb = new StringBuilder($"\nConfig: {item.Name}");

            sb.AppendLine()
              .AppendLine($"\t Bulk Copy Timeout: {item.BulkCopyTimeout}")
              .AppendLine($"\t Bulk Batch Size: {item.BulkBatchSize}")
              .AppendLine($"\t Bulk Table Lock: {item.TableLock}")
              .AppendLine($"\t Bulk Chec kConstraints: {item.CheckConstraints}")
              .AppendLine($"\t Bulk Fire Triggers: {item.FireTriggers}");

            _logger.WriteLog(sb.ToString(), ConsoleColor.White, false);
            _logger.WriteLine(' ');

            sb.Clear();
            sb.AppendLine($"\t Tables Count: {item.Tables.Count}{Environment.NewLine}");
            _logger.WriteLog(sb.ToString(), ConsoleColor.White, false);
        }

        private static void WriteLineRG(string texto, bool textoVerde)
        {
            var fcAtual = Console.ForegroundColor;
            Console.Write(texto);
            Console.ForegroundColor = (textoVerde ? ConsoleColor.Green : ConsoleColor.DarkRed);
            Console.WriteLine(textoVerde);
            Console.ForegroundColor = fcAtual;
        }
    }
}

