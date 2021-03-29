using SQLDataTransfer.AppConsole.Configuration;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SQLDataTransfer.AppConsole
{
    class Program
    {
        private static DataTransfer _transfer;
        private static string _fileName;

        static void Main(string[] args)
        {
            //local de armazenamento do log de execução
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Log");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            _fileName = Path.Combine(path, string.Format("ErroConsole_{0}.txt", DateTime.Now.ToString("yyyyMMdd")));
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
                    _fileName = Path.Combine(path, string.Format("{0}_{1}.txt", item.Name, DateTime.Now.ToString("yyyyMMdd")));
                    if (!item.Disabled)
                    {
                        tempoProcesso.Restart();
                        EscreverLogInfo(item);
                        _transfer = new DataTransfer(item.ConnectionSource, item.ConnectionTarget);
                        if (_transfer.Conectado)
                        {
                            EscreverLogLinha('=');
                            EscreverLog(string.Format("{0} BEGIN Transfer: {1} => {2}", DateTime.Now.ToString("dd.MM.yyyy"), _transfer.DatabaseOrigem, _transfer.DatabaseDestino), ConsoleColor.Blue, false);
                            EscreverLogLinha();

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
                            EscreverLogErro("FALHA NA CONEXÃO, VERIFIQUE AS CONFIGURAÇÕES");
                        }
                        tempoProcesso.Stop();

                        EscreverLogLinha();
                        EscreverLog($"{DateTime.Now:dd.MM.yyyy} END Transfer. {tempoProcesso.Elapsed}", ConsoleColor.Blue, false);
                        EscreverLogLinha('=');
                    }
                }
            }
            catch (Exception ex)
            {
                EscreverLogErro(ex.Message);
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
                Console.WriteLine("Config: {0}", item.Name);
                WriteLineRG("\t Executar Transferência: ", !item.Disabled);
                Console.WriteLine();
                Console.WriteLine("\t Bulk Copy Timeout: {0}", item.BulkCopyTimeout);
                Console.WriteLine("\t Bulk Batch Size: {0}", item.BulkBatchSize);
                WriteLineRG("\t Bulk Table Lock: ", item.TableLock);
                WriteLineRG("\t Bulk Check Constraints: ", item.CheckConstraints);
                WriteLineRG("\t Bulk Fire Triggers: ", item.FireTriggers);

                Console.WriteLine();
                Console.WriteLine("\t Tables Count: {0}", item.Tables.Count);
                Console.WriteLine(new String('-', 80) + Environment.NewLine);
            }
            
            Console.WriteLine("Pressione [ENTER] para CONTINUAR");
                        
            return Console.ReadKey().Key == ConsoleKey.Enter;
        }

        private static void IniciarTransferencia(BaseConfigurationCollection<TablesElement> tables, DataTransfer.BulkConfig bulkConfig)
        {
            var tempo = new System.Diagnostics.Stopwatch();
            int rcount;

            #region [ TRANSFERENCIA DOS DADOS ]           
            foreach (TablesElement table in tables)
            {
                string msg = "IniciarTransferencia: ";
                try
                {
                    if (!string.IsNullOrEmpty(table.ToCsvFile))
                        EscreverLog($"Exportação da fonte de dados \"{table.SourceName}\"", ConsoleColor.DarkGreen);
                    else if (string.Compare(table.Name, table.SourceName, StringComparison.OrdinalIgnoreCase) == 0)
                        EscreverLog($"Transferindo dados para a tabela \"{table.Name}\"", ConsoleColor.DarkGreen);
                    else
                        EscreverLog($"Transferindo dados de \"{table.SourceName}\" para \"{table.Name}\"", ConsoleColor.DarkGreen);

                    if (!string.IsNullOrEmpty(table.ToCsvFile))
                    {
                        msg = $"Exportação da fonte de dados \"{table.SourceName}\"";
                        tempo.Restart();
                        rcount = _transfer.ExportarArquivoCsv(table.SelectAll, table.ToCsvFile, true, ";", bulkConfig.CopyTimeout);
                        tempo.Stop();
                        EscreverLog($"\t Arquivo \"{table.ToCsvFile}\" criado com {rcount:n0} linhas! {tempo.Elapsed}");
                    }
                    else
                    {
                        if (table.Truncate)
                        {
                            msg = "Exclusão de dados";
                            tempo.Restart();
                            _transfer.LimparTabelaDestino(table.Name);
                            tempo.Stop();
                            EscreverLog($"\t {msg} finalizado! {tempo.Elapsed}");
                        }

                        msg = "Copia de dados";
                        tempo.Restart();
                        EscreverLog($"\t Transferência de {_transfer.BulkCopy(table.Name, table.SelectAll, bulkConfig):n0} registros finalizado! {tempo.Elapsed}");
                        tempo.Stop();

                        msg = "Re-Sequênciamento de campo Identity";
                        tempo.Restart();
                        _transfer.Reseed(table.Name);
                        tempo.Stop();

                        EscreverLog($"\t {msg} finalizado! {tempo.Elapsed}");
                        if (table.UpdateStatistics)
                        {
                            msg = "Atualização de estatisticas";
                            tempo.Restart();
                            _transfer.UpdateStatistics(table.Name);
                            tempo.Stop();
                            EscreverLog($"\t {msg} finalizado! {tempo.Elapsed}");
                        }
                    }

                }
                catch (Exception ex)
                {
                    EscreverLogErro(msg + " " + ex.Message);
                }
                finally
                {
                    EscreverLogLinha();
                }
            }
            #endregion

        }

        private static void EscreverLogInfo(TransfersElement item)
        {
            var sb = new System.Text.StringBuilder($"\nConfig: {item.Name}");

            sb.AppendLine()
              .AppendLine($"\t Bulk CopyTimeout: {item.BulkCopyTimeout}")
              .AppendLine($"\t Bulk BatchSize: {item.BulkBatchSize}")
              .AppendLine($"\t Bulk TableLock: {item.TableLock}")
              .AppendLine($"\t Bulk CheckConstraints: {item.CheckConstraints}")
              .AppendLine($"\t Bulk FireTriggers: {item.FireTriggers}");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(sb.ToString());
            File.AppendAllText(_fileName, sb.ToString());

            sb.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(sb.ToString());
            File.AppendAllText(_fileName, sb.ToString() + Environment.NewLine);

            sb.Clear();
            sb.AppendLine($"\t Tables Count: {item.Tables.Count}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(sb.ToString());
            File.AppendAllText(_fileName, sb.ToString() + Environment.NewLine);
        }

        private static void EscreverLog(string texto, ConsoleColor fColor = ConsoleColor.White, bool includeTime = true)
        {
            Console.ForegroundColor = fColor;
            if (includeTime)
                texto = string.Format("{0}: {1}", DateTime.Now.ToString("HH:mm:ss"), texto);

            Console.WriteLine(texto);

            File.AppendAllText(_fileName, texto + Environment.NewLine);
        }

        private static void EscreverLogLinha(char divisor = '-')
        {
            string texto = new String(divisor, 100) + Environment.NewLine;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(texto);
            File.AppendAllText(_fileName, texto);
        }

        private static void EscreverLogErro(string texto)
        {
            EscreverLog("ERRO! " + texto, ConsoleColor.DarkRed);
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

