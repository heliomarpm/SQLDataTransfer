namespace SQLDataTransfer.GUI
{
    partial class frmMain
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("TableName");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("TableName");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Script");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtProviderDestino = new System.Windows.Forms.TextBox();
            this.txtProviderOrigem = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlNavBottom = new System.Windows.Forms.Panel();
            this.chkTruncateTable = new System.Windows.Forms.CheckBox();
            this.chkTableLock = new System.Windows.Forms.CheckBox();
            this.chkTriggers = new System.Windows.Forms.CheckBox();
            this.chkConstraints = new System.Windows.Forms.CheckBox();
            this.btnCopyData = new System.Windows.Forms.Button();
            this.btnGenInsertScript = new System.Windows.Forms.Button();
            this.pnlNavTop = new System.Windows.Forms.Panel();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lstTables = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlNavBottom.SuspendLayout();
            this.pnlNavTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.pnlTop.Controls.Add(this.btnConnect);
            this.pnlTop.Controls.Add(this.txtProviderDestino);
            this.pnlTop.Controls.Add(this.txtProviderOrigem);
            this.pnlTop.Controls.Add(this.label2);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1115, 142);
            this.pnlTop.TabIndex = 0;
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.btnConnect.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnConnect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnConnect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Location = new System.Drawing.Point(937, 32);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(156, 74);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtProviderDestino
            // 
            this.txtProviderDestino.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProviderDestino.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.txtProviderDestino.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProviderDestino.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.txtProviderDestino.Location = new System.Drawing.Point(281, 82);
            this.txtProviderDestino.Name = "txtProviderDestino";
            this.txtProviderDestino.Size = new System.Drawing.Size(639, 27);
            this.txtProviderDestino.TabIndex = 3;
            // 
            // txtProviderOrigem
            // 
            this.txtProviderOrigem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProviderOrigem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.txtProviderOrigem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProviderOrigem.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.txtProviderOrigem.Location = new System.Drawing.Point(281, 32);
            this.txtProviderOrigem.Name = "txtProviderOrigem";
            this.txtProviderOrigem.Size = new System.Drawing.Size(639, 27);
            this.txtProviderOrigem.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(263, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "ConnectionString of Destination ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "ConnectionString of Source";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 142);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.splitContainer1.Panel1.Controls.Add(this.pnlNavBottom);
            this.splitContainer1.Panel1.Controls.Add(this.pnlNavTop);
            this.splitContainer1.Panel1.Controls.Add(this.lstTables);
            this.splitContainer1.Panel1MinSize = 371;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.splitContainer1.Panel2.Controls.Add(this.txtMemo);
            this.splitContainer1.Panel2MinSize = 300;
            this.splitContainer1.Size = new System.Drawing.Size(1115, 626);
            this.splitContainer1.SplitterDistance = 371;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.SizeChanged += new System.EventHandler(this.splitContainer1_SizeChanged);
            // 
            // pnlNavBottom
            // 
            this.pnlNavBottom.Controls.Add(this.chkTruncateTable);
            this.pnlNavBottom.Controls.Add(this.chkTableLock);
            this.pnlNavBottom.Controls.Add(this.chkTriggers);
            this.pnlNavBottom.Controls.Add(this.chkConstraints);
            this.pnlNavBottom.Controls.Add(this.btnCopyData);
            this.pnlNavBottom.Controls.Add(this.btnGenInsertScript);
            this.pnlNavBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNavBottom.Location = new System.Drawing.Point(0, 460);
            this.pnlNavBottom.Name = "pnlNavBottom";
            this.pnlNavBottom.Size = new System.Drawing.Size(371, 166);
            this.pnlNavBottom.TabIndex = 6;
            // 
            // chkTruncateTable
            // 
            this.chkTruncateTable.AutoSize = true;
            this.chkTruncateTable.ForeColor = System.Drawing.Color.Firebrick;
            this.chkTruncateTable.Location = new System.Drawing.Point(12, 126);
            this.chkTruncateTable.Name = "chkTruncateTable";
            this.chkTruncateTable.Size = new System.Drawing.Size(147, 25);
            this.chkTruncateTable.TabIndex = 10;
            this.chkTruncateTable.Text = "Truncate Table";
            this.chkTruncateTable.UseVisualStyleBackColor = true;
            // 
            // chkTableLock
            // 
            this.chkTableLock.AutoSize = true;
            this.chkTableLock.Checked = true;
            this.chkTableLock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTableLock.Location = new System.Drawing.Point(12, 89);
            this.chkTableLock.Name = "chkTableLock";
            this.chkTableLock.Size = new System.Drawing.Size(111, 25);
            this.chkTableLock.TabIndex = 9;
            this.chkTableLock.Text = "Table Lock";
            this.chkTableLock.UseVisualStyleBackColor = true;
            // 
            // chkTriggers
            // 
            this.chkTriggers.AutoSize = true;
            this.chkTriggers.Location = new System.Drawing.Point(12, 52);
            this.chkTriggers.Name = "chkTriggers";
            this.chkTriggers.Size = new System.Drawing.Size(118, 25);
            this.chkTriggers.TabIndex = 8;
            this.chkTriggers.Text = "Fire Triggers";
            this.chkTriggers.UseVisualStyleBackColor = true;
            // 
            // chkConstraints
            // 
            this.chkConstraints.AutoSize = true;
            this.chkConstraints.Location = new System.Drawing.Point(12, 15);
            this.chkConstraints.Name = "chkConstraints";
            this.chkConstraints.Size = new System.Drawing.Size(177, 25);
            this.chkConstraints.TabIndex = 7;
            this.chkConstraints.Text = "Disable Constraints";
            this.chkConstraints.UseVisualStyleBackColor = true;
            // 
            // btnCopyData
            // 
            this.btnCopyData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.btnCopyData.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnCopyData.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnCopyData.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnCopyData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyData.Location = new System.Drawing.Point(204, 89);
            this.btnCopyData.Name = "btnCopyData";
            this.btnCopyData.Size = new System.Drawing.Size(158, 62);
            this.btnCopyData.TabIndex = 6;
            this.btnCopyData.Text = "Copy Data";
            this.btnCopyData.UseVisualStyleBackColor = false;
            this.btnCopyData.Click += new System.EventHandler(this.btnCopyData_Click);
            // 
            // btnGenInsertScript
            // 
            this.btnGenInsertScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenInsertScript.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.btnGenInsertScript.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnGenInsertScript.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnGenInsertScript.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnGenInsertScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenInsertScript.Location = new System.Drawing.Point(204, 15);
            this.btnGenInsertScript.Name = "btnGenInsertScript";
            this.btnGenInsertScript.Size = new System.Drawing.Size(158, 62);
            this.btnGenInsertScript.TabIndex = 5;
            this.btnGenInsertScript.Text = "Gen. Insert Script";
            this.btnGenInsertScript.UseVisualStyleBackColor = false;
            this.btnGenInsertScript.Click += new System.EventHandler(this.btnGenInsertScript_Click);
            // 
            // pnlNavTop
            // 
            this.pnlNavTop.Controls.Add(this.chkAll);
            this.pnlNavTop.Controls.Add(this.txtSearch);
            this.pnlNavTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNavTop.Location = new System.Drawing.Point(0, 0);
            this.pnlNavTop.Name = "pnlNavTop";
            this.pnlNavTop.Size = new System.Drawing.Size(371, 73);
            this.pnlNavTop.TabIndex = 5;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkAll.Location = new System.Drawing.Point(0, 0);
            this.chkAll.Name = "chkAll";
            this.chkAll.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
            this.chkAll.Size = new System.Drawing.Size(371, 35);
            this.chkAll.TabIndex = 4;
            this.chkAll.Text = "Select all";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.txtSearch.Location = new System.Drawing.Point(10, 37);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(352, 27);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lstTables
            // 
            this.lstTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstTables.BackColor = System.Drawing.Color.Gainsboro;
            this.lstTables.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstTables.CheckBoxes = true;
            this.lstTables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstTables.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstTables.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.StateImageIndex = 0;
            this.lstTables.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.lstTables.Location = new System.Drawing.Point(10, 81);
            this.lstTables.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstTables.Name = "lstTables";
            this.lstTables.Size = new System.Drawing.Size(352, 371);
            this.lstTables.TabIndex = 4;
            this.lstTables.UseCompatibleStateImageBehavior = false;
            this.lstTables.View = System.Windows.Forms.View.Details;
            this.lstTables.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstTables_ItemChecked);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Source Table";
            this.columnHeader1.Width = 280;
            // 
            // txtMemo
            // 
            this.txtMemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.txtMemo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMemo.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMemo.ForeColor = System.Drawing.Color.LightGray;
            this.txtMemo.Location = new System.Drawing.Point(0, 0);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.ReadOnly = true;
            this.txtMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMemo.Size = new System.Drawing.Size(739, 626);
            this.txtMemo.TabIndex = 0;
            this.txtMemo.Text = "Memo";
            this.txtMemo.DoubleClick += new System.EventHandler(this.txtMemo_DoubleClick);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 763);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1115, 5);
            this.progressBar1.TabIndex = 2;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(1115, 768);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.LightGray;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmMain";
            this.Text = "SQLDataTransfer GUI";
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlNavBottom.ResumeLayout(false);
            this.pnlNavBottom.PerformLayout();
            this.pnlNavTop.ResumeLayout(false);
            this.pnlNavTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProviderOrigem;
        private System.Windows.Forms.TextBox txtProviderDestino;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel pnlNavTop;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.ListView lstTables;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel pnlNavBottom;
        private System.Windows.Forms.Button btnCopyData;
        private System.Windows.Forms.Button btnGenInsertScript;
        private System.Windows.Forms.CheckBox chkTruncateTable;
        private System.Windows.Forms.CheckBox chkTableLock;
        private System.Windows.Forms.CheckBox chkTriggers;
        private System.Windows.Forms.CheckBox chkConstraints;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox txtMemo;
    }
}

