using SQLDataTransfer.Core;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLDataTransfer.GUI
{
    public partial class frmMain : Form
    {
        DataTransfer _transfer = null;
        bool _transferIsBusy;
        bool _BloquearEventoItemCheck;
        Stopwatch _tempoProcesso;
        readonly BackgroundWorker _worker;
        readonly Color bgMemoAtivo = Color.LightGray; // Color.FromArgb(23, 23, 23);
        readonly Color bgMemoInativo = Color.Gray;

        public frmMain()
        {
            InitializeComponent();

            _worker = new BackgroundWorker();
            _worker.DoWork += _worker_DoWork;
            _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;
            _worker.WorkerSupportsCancellation = true;

            txtProviderOrigem.Text = string.Empty;
            txtProviderDestino.Text = string.Empty;

#if DEBUG
            txtProviderOrigem.Text = @"Data Source=NB-TOPONE\SQLEXPRESS;Initial Catalog=DB_IDE;Integrated Security=SSPI;Persist Security Info=False;";
            txtProviderDestino.Text = @"Data Source=NB-TOPONE\SQLEXPRESS;Initial Catalog=DB_IDE_DEV;Integrated Security=SSPI;Persist Security Info=False;";
            //txtProviderDestino.Text = @"Data Source=CP-ABXXDNK3-M;Initial Catalog=DB_FRTB;Persist Security Info=True;User ID=sa;PWD=CD0$;Timeout=7200;";
            //txtProviderOrigem.Text = @"Server=BBASQLATS4-64-S;Trusted_Connection=True;Database=IBBA_RISCO;Integrated Security=true;";
            //txtProviderDestino.Text = @"Server=bbags2sqld2;Trusted_Connection=True;Database=IBBA_RISCO;Integrated Security=true;";
            //txtProviderOrigem.Text = @"Server=SQACC007;Database=DBMM04;User ID=MWSERVL;Password=WISDOM;Persist Security Info=True;";
            //txtProviderDestino.Text = @"Server=SQDSC004\A;Database=DBMM00;User ID=DBOMM00;Password=DBOMM00;Persist Security Info=True;";
            //txtProviderOrigem.Text = @"Server=SQACC016\a;Database=DBM600;User ID=m6gendb;Password=A6P7DE;Persist Security Info=True;";
            //txtProviderDestino.Text = @"Server=SQDSC020\a;Database=DBM600;User ID=m6gendb;Password=A6P7DE;Persist Security Info=True;";
#endif
            BlockConnection(false);
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (_worker.CancellationPending == false && _transferIsBusy)
            {
                System.Threading.Thread.Sleep(250);
            }
            if (_worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            e.Result = 42;
        }

        private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _tempoProcesso.Stop();
            btnCopyData.Text = "Executar";
            btnCopyData.Enabled = true;
            Application.DoEvents();
            if (!e.Cancelled)
            {
                EscreverMemo("Fim de execução: " + _tempoProcesso.Elapsed);
                MessageBox.Show("Transferência de Dados finalizada! " + _tempoProcesso.Elapsed);
            }
            else
            {
                EscreverMemo("Processamento Cancelado! " + DateTime.Now.ToString("HH:mm:ss"));
            }
            _tempoProcesso = null;
        }

        private void EscreverMemo(string texto)
        {
            txtMemo.Text += texto + Environment.NewLine;
            txtMemo.SelectionStart = txtMemo.TextLength;
            txtMemo.ScrollToCaret();
            Application.DoEvents();
        }

        private void BlockConnection(bool IsConnected)
        {
            if (!IsConnected) { lstTables.Items.Clear(); }
            txtProviderOrigem.Enabled = !IsConnected;
            txtProviderDestino.Enabled = !IsConnected;
            btnConnect.Text = IsConnected ? "Disconnect" : "Connect";
        }
        private void GetAllTables()
        {
            _BloquearEventoItemCheck = true;
            lstTables.Items.Clear();
            foreach (KeyValuePair<string, string> tb in _transfer.GetTables())
            {
                var item = new ListViewItem(tb.Key)
                {
                    Tag = tb.Value
                };
                lstTables.Items.Add(item);
            }
            _BloquearEventoItemCheck = false;
        }

        private void MostrarSelecionados()
        {
            int qtd = 0;
            string msg = string.Empty;
            foreach (ListViewItem item in lstTables.Items)
            {
                if (item.Checked)
                {
                    qtd += 1;
                    msg += item.Text + Environment.NewLine;
                }
            }
            txtMemo.Clear();
            EscreverMemo("Tabelas selecionadas: " + qtd.ToString());
            EscreverMemo(new String('-', 80) + Environment.NewLine);
            EscreverMemo(msg);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                txtMemo.Clear();
                txtMemo.ReadOnly = true;
                txtMemo.ForeColor = bgMemoInativo;

                if (_transfer != null)// && _transfer.Conectado)
                {
                    _transfer.Disconnect();
                    _transfer = null;
                    BlockConnection(false);
                    lstTables.Items.Clear();
                }
                else
                {
                    _transfer = new DataTransfer(txtProviderOrigem.Text, txtProviderDestino.Text);
                    //if (_transfer.Conectado)
                    //{                       
                    this.Text = string.Format("DataTransfer: {0} => {1}", _transfer.DatabaseSource, _transfer.DatabaseTarget);
                    BlockConnection(true);
                    GetAllTables();
                    //}
                }
            }
            catch (Exception ex)
            {
                txtMemo.Text = "";
                EscreverMemo("ERRO frmMain.Conectar: " + ex.Message);
                if (_transfer != null)
                {
                    _transfer.Disconnect();
                    _transfer = null;
                }
                BlockConnection(false);
            }
        }

        private void splitContainer1_SizeChanged(object sender, EventArgs e)
        {
            return;
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                if (this.Width < 800) this.Width = 800;
                if (this.Height < 600) this.Height = 600;
            }
        }

        private void txtMemo_DoubleClick(object sender, EventArgs e)
        {
            if (_transferIsBusy)
                return;
            bool readOnly = !txtMemo.ReadOnly;
            txtMemo.Clear();
            txtMemo.ForeColor = (!readOnly ? bgMemoAtivo : bgMemoInativo);
            txtMemo.ReadOnly = readOnly;
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            _BloquearEventoItemCheck = true;
            foreach (ListViewItem item in lstTables.Items)
            {
                item.Checked = chkAll.Checked;
            }
            _BloquearEventoItemCheck = false;
            MostrarSelecionados();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                _BloquearEventoItemCheck = true;
                lstTables.SelectedIndices.Clear();
                string value = txtSearch.Text;
                if (value.Contains(';'))
                {
                    string[] tables = value.Split(';');
                    foreach (var tbl in tables)
                    {
                        if (value.Contains(tbl.Trim() + ';'))
                            if (tbl.Trim() != "")
                            {
                                ListViewItem item = lstTables.FindItemWithText(tbl.Trim());
                                if (item != null && item.Text.Trim().ToUpper().Equals(tbl.Trim().ToUpper()))
                                {
                                    item.Checked = true;
                                }
                            }
                    }
                }
                else
                {
                    ListViewItem item = lstTables.FindItemWithText(value);
                    if (item != null)
                    {
                        item.EnsureVisible();
                        //item.Focused = true;
                        //item.Selected = true;
                        //item.BackColor = SystemColors.Highlight;
                        //item.ForeColor = SystemColors.HighlightText;
                    }
                }
                _BloquearEventoItemCheck = false;
                MostrarSelecionados();
            }
        }

        private void lstTables_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!_BloquearEventoItemCheck)
            {
                MostrarSelecionados();
            }
        }

        private void btnGenInsertScript_Click(object sender, EventArgs e)
        {
            txtMemo.Clear();
            foreach (ListViewItem item in lstTables.Items)
            {
                if (item.Checked)
                {
                    EscreverMemo(_transfer.GenInsertScript(item.Text));
                    EscreverMemo(new string('-', 84) + Environment.NewLine);
                }
            }
        }

        private void btnCopyData_Click(object sender, EventArgs e)
        {
            if (!txtMemo.ReadOnly)
            {
                _tempoProcesso = new Stopwatch();

                EscreverMemo(Environment.NewLine + new string('-', 84) + Environment.NewLine);
                _tempoProcesso = new Stopwatch();
                _tempoProcesso.Restart();
                EscreverMemo(CreateCsvFile(txtMemo.Text));
                _tempoProcesso.Stop();
                EscreverMemo(new string('-', 84) + Environment.NewLine);
                EscreverMemo("Tempo de escrita:" + _tempoProcesso.Elapsed);
            }
            else
            {
                if (_worker.IsBusy)
                {
                    btnCopyData.Text = "CANCELANDO";
                    btnCopyData.Enabled = false;
                    Application.DoEvents();
                    _worker.CancelAsync();
                    return;
                }
                else
                {
                    btnCopyData.Text = "Cancelar";
                    Application.DoEvents();
                    _tempoProcesso = new Stopwatch();
                    _tempoProcesso.Restart();
                    _transferIsBusy = true;
                    _worker.RunWorkerAsync();
                    PrepareTransfer();
                    _transferIsBusy = false;
                }
            }
        }

        private void PrepareTransfer()
        {
            var tempo = new System.Diagnostics.Stopwatch();
            bool desativarFKs = chkConstraints.Checked;
            
            try
            {
                int rcount = 0;

                txtMemo.Clear();
                EscreverMemo(new String('=', 80) + Environment.NewLine);
                EscreverMemo("Inicio de execução: " + DateTime.Now.ToString());
                EscreverMemo(new String('-', 80) + Environment.NewLine);

                if (desativarFKs)
                {
                    if (_worker.CancellationPending == false)
                    {
                        tempo.Restart();
                        rcount = _transfer.DisableConstraints();
                        tempo.Stop();
                        if (rcount == 0)
                        {
                            desativarFKs = false;
                            EscreverMemo($"Nenhuma ForeingKey foi encontrado para desativação! {tempo.Elapsed}");
                        }
                        else
                            EscreverMemo($"{rcount.ToString("n0")} ForeingKey's desativadas! {tempo.Elapsed}");

                        EscreverMemo(new String('-', 80) + Environment.NewLine);
                    }
                }

                //if (chkZerarBase.Checked)
                //{
                //    if (_worker.CancellationPending == false)
                //    {
                //        tempo.Restart();
                //        _transfer.TruncateDataBase();
                //        tempo.Stop();
                //        EscreverMemo("Zerar Base finalizado! " + tempo.Elapsed);
                //        EscreverMemo(new String('-', 80) + Environment.NewLine);
                //    }
                //}
                foreach (ListViewItem item in lstTables.Items)
                {
                    if (item.Checked)
                    {
                        if (_worker.CancellationPending == false)
                            StartTransfer(item.Text, item.Tag.ToString());
                        else
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                EscreverMemo("ERRO frmMain.btnExecutar: " + ex.Message);
                _worker.CancelAsync();
            }
            finally
            {
                if (desativarFKs)
                {
                    tempo.Restart();
                    EscreverMemo($"{_transfer.EnableConstraints().ToString("n0")} ForeingKey's ativadas! {tempo.Elapsed}");
                    tempo.Stop();
                }

                EscreverMemo(new String('-', 80) + Environment.NewLine);
                EscreverMemo("Fim de execução: " + DateTime.Now.ToString());
                EscreverMemo(new String('=', 80) + Environment.NewLine);
            }
        }

        private void StartTransfer(string pTableName, string pIdentityField)
        {
            var tempo = new System.Diagnostics.Stopwatch();
            string msg = "IniciarTransferencia: ";

            try
            {
                bool desativarTriggers = !chkTriggers.Checked;
                long rcount = 0;

                EscreverMemo("Transferindo dados [" + pTableName + "] ");

                //if (desativarTriggers)
                //{
                //    msg = "Desativação das Triggers";
                //    tempo.Restart();
                //    rcount = _transfer.DisableTriggers(pTableName);
                //    tempo.Stop();
                //    if (rcount == 0)
                //    {
                //        desativarTriggers = false;
                //        EscreverMemo($"Nenhuma Trigger foi encontrado para desativação! {tempo.Elapsed}");
                //    }
                //    else
                //        EscreverMemo($"{rcount.ToString("n0")} Trigger's desativadas! {tempo.Elapsed}");
                //}

                if (chkTruncateTable.Checked)
                {
                    msg = "Exclusão de dados";
                    tempo.Restart();
                    _transfer.TruncateTable(pTableName);
                    tempo.Stop();
                    EscreverMemo($"\t {msg} finalizado! {tempo.Elapsed}");
                }

                msg = "Copia de dados";
                tempo.Restart();
                rcount = _transfer.BulkCopy(pTableName, chkTableLock.Checked, chkConstraints.Checked, chkTriggers.Checked, 0, 10000);
                EscreverMemo($"\t Transferência de {rcount:n0} registros finalizado! {tempo.Elapsed}");
                tempo.Stop();

                //no carreamento da lista de tabelas já foi verificado a existencia de identity
                if (!string.IsNullOrEmpty(pIdentityField))
                {
                    msg = "Re-Sequênciamento campo Identity";
                    tempo.Restart();
                    _transfer.Reseed(pTableName);//, pIdentityField);
                    tempo.Stop();
                    EscreverMemo($"\t {msg} finalizado! {tempo.Elapsed}");
                }

                //if (desativarTriggers)
                //{
                //    msg = "Ativação das Triggers";
                //    tempo.Restart();
                //    EscreverMemo($"{_transfer.EnableTriggers(pTableName).ToString("n0")} Trigger's ativadas! {tempo.Elapsed}");
                //    tempo.Stop();
                //}
            }
            catch (Exception ex)
            {
                EscreverMemo("ERRO frmMain.IniciarTransferencia: " + msg + Environment.NewLine + ex.Message);
            }
            finally
            {
                EscreverMemo(new String('-', 80) + Environment.NewLine);
            }
        }

        public string CreateCsvFile(string pSelect)
        {
            string path = "Nenhuma arquivo foi gerado!";
            try
            {
                using (var dr = _transfer.ConnectSource.ExecuteReader(pSelect))
                {
                    List<string> rows = dr.ToList(true);
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Extracao");

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    path = Path.Combine(path, "extracao_" + DateTime.Now.ToString("yyyyMMdd") + ".csv");

                    using (StreamWriter sw = new StreamWriter(path, false, Encoding.Default))
                    {
                        rows.ForEach(f => sw.WriteLine(f));
                        sw.Flush();
                        sw.Close();
                        sw.Dispose();
                    }
                }
                return path;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
