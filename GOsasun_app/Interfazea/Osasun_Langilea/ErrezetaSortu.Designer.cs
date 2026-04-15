namespace GOsasun_app.Interfazea
{
    partial class ErrezetaSortu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblIzenburua = new Label();
            pnlEzkerra = new Panel();
            dgvPazienteak = new DataGridView();
            txtBilatuPaz = new TextBox();
            lblPazientea = new Label();
            pnlEskuina = new Panel();
            btnSortuErrezeta = new Button();
            grpBotikak = new GroupBox();
            lblBotika = new Label();
            cmbBotikak = new ComboBox();
            lblDosia = new Label();
            txtDosia = new TextBox();
            lblMaiztasuna = new Label();
            txtMaiztasuna = new TextBox();
            btnGehituBotika = new Button();
            dgvBotikak = new DataGridView();
            btnKenduBotika = new Button();
            dtpIraungitzeData = new DateTimePicker();
            lblIraungitzeData = new Label();
            txtDiagnostikoa = new TextBox();
            lblDiagnostikoa = new Label();
            lblErrezetaDatuak = new Label();
            _edukiPanela.SuspendLayout();
            pnlEzkerra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPazienteak).BeginInit();
            pnlEskuina.SuspendLayout();
            grpBotikak.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBotikak).BeginInit();
            SuspendLayout();
            // 
            // _edukiPanela
            // 
            _edukiPanela.Controls.Add(pnlEskuina);
            _edukiPanela.Controls.Add(pnlEzkerra);
            _edukiPanela.Controls.Add(lblIzenburua);
            _edukiPanela.Size = new Size(1902, 1099);
            // 
            // _atzeraBotoia
            // 
            _atzeraBotoia.FlatAppearance.BorderSize = 0;
            // 
            // lblIzenburua
            // 
            lblIzenburua.BackColor = Color.Transparent;
            lblIzenburua.Dock = DockStyle.Top;
            lblIzenburua.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblIzenburua.ForeColor = Color.White;
            lblIzenburua.Location = new Point(2, 2);
            lblIzenburua.Name = "lblIzenburua";
            lblIzenburua.Size = new Size(1898, 128);
            lblIzenburua.TabIndex = 0;
            lblIzenburua.Text = "ERREZETA BERRIA SORTU";
            lblIzenburua.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlEzkerra
            // 
            pnlEzkerra.BackColor = Color.Transparent;
            pnlEzkerra.Controls.Add(dgvPazienteak);
            pnlEzkerra.Controls.Add(txtBilatuPaz);
            pnlEzkerra.Controls.Add(lblPazientea);
            pnlEzkerra.Dock = DockStyle.Left;
            pnlEzkerra.Location = new Point(2, 130);
            pnlEzkerra.Name = "pnlEzkerra";
            pnlEzkerra.Padding = new Padding(10);
            pnlEzkerra.Size = new Size(446, 967);
            pnlEzkerra.TabIndex = 1;
            // 
            // dgvPazienteak
            // 
            dgvPazienteak.AllowUserToAddRows = false;
            dgvPazienteak.AllowUserToDeleteRows = false;
            dgvPazienteak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPazienteak.BackgroundColor = Color.White;
            dgvPazienteak.ColumnHeadersHeight = 46;
            dgvPazienteak.Dock = DockStyle.Fill;
            dgvPazienteak.Location = new Point(10, 126);
            dgvPazienteak.MultiSelect = false;
            dgvPazienteak.Name = "dgvPazienteak";
            dgvPazienteak.ReadOnly = true;
            dgvPazienteak.RowHeadersVisible = false;
            dgvPazienteak.RowHeadersWidth = 82;
            dgvPazienteak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPazienteak.Size = new Size(426, 831);
            dgvPazienteak.TabIndex = 0;
            // 
            // txtBilatuPaz
            // 
            txtBilatuPaz.Dock = DockStyle.Top;
            txtBilatuPaz.Font = new Font("Segoe UI", 12F);
            txtBilatuPaz.Location = new Point(10, 76);
            txtBilatuPaz.Margin = new Padding(0, 5, 0, 10);
            txtBilatuPaz.Name = "txtBilatuPaz";
            txtBilatuPaz.PlaceholderText = "Bilatu...";
            txtBilatuPaz.Size = new Size(426, 50);
            txtBilatuPaz.TabIndex = 1;
            // 
            // lblPazientea
            // 
            lblPazientea.Dock = DockStyle.Top;
            lblPazientea.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblPazientea.ForeColor = Color.White;
            lblPazientea.Location = new Point(10, 10);
            lblPazientea.Name = "lblPazientea";
            lblPazientea.Size = new Size(426, 66);
            lblPazientea.TabIndex = 2;
            lblPazientea.Text = "1. Hautatu Pazientea";
            lblPazientea.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlEskuina
            // 
            pnlEskuina.BackColor = Color.Transparent;
            pnlEskuina.Controls.Add(btnSortuErrezeta);
            pnlEskuina.Controls.Add(grpBotikak);
            pnlEskuina.Controls.Add(dtpIraungitzeData);
            pnlEskuina.Controls.Add(lblIraungitzeData);
            pnlEskuina.Controls.Add(txtDiagnostikoa);
            pnlEskuina.Controls.Add(lblDiagnostikoa);
            pnlEskuina.Controls.Add(lblErrezetaDatuak);
            pnlEskuina.Dock = DockStyle.Fill;
            pnlEskuina.Location = new Point(448, 130);
            pnlEskuina.Name = "pnlEskuina";
            pnlEskuina.Padding = new Padding(20);
            pnlEskuina.Size = new Size(1452, 967);
            pnlEskuina.TabIndex = 0;
            pnlEskuina.Paint += pnlEskuina_Paint;
            // 
            // btnSortuErrezeta
            // 
            btnSortuErrezeta.BackColor = Color.FromArgb(52, 152, 219);
            btnSortuErrezeta.FlatStyle = FlatStyle.Flat;
            btnSortuErrezeta.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnSortuErrezeta.ForeColor = Color.White;
            btnSortuErrezeta.Location = new Point(36, 867);
            btnSortuErrezeta.Name = "btnSortuErrezeta";
            btnSortuErrezeta.Size = new Size(680, 97);
            btnSortuErrezeta.TabIndex = 0;
            btnSortuErrezeta.Text = "ERREZETA SORTU ETA GORDE";
            btnSortuErrezeta.UseVisualStyleBackColor = false;
            // 
            // grpBotikak
            // 
            grpBotikak.BackColor = Color.FromArgb(100, 0, 0, 0);
            grpBotikak.Controls.Add(lblBotika);
            grpBotikak.Controls.Add(cmbBotikak);
            grpBotikak.Controls.Add(lblDosia);
            grpBotikak.Controls.Add(txtDosia);
            grpBotikak.Controls.Add(lblMaiztasuna);
            grpBotikak.Controls.Add(txtMaiztasuna);
            grpBotikak.Controls.Add(btnGehituBotika);
            grpBotikak.Controls.Add(dgvBotikak);
            grpBotikak.Controls.Add(btnKenduBotika);
            grpBotikak.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            grpBotikak.ForeColor = Color.White;
            grpBotikak.Location = new Point(36, 182);
            grpBotikak.Name = "grpBotikak";
            grpBotikak.Size = new Size(1340, 670);
            grpBotikak.TabIndex = 1;
            grpBotikak.TabStop = false;
            grpBotikak.Text = "3. Botikak Gehitu";
            // 
            // lblBotika
            // 
            lblBotika.Location = new Point(20, 46);
            lblBotika.Name = "lblBotika";
            lblBotika.Size = new Size(135, 50);
            lblBotika.TabIndex = 0;
            lblBotika.Text = "Botika:";
            // 
            // cmbBotikak
            // 
            cmbBotikak.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBotikak.Location = new Point(20, 99);
            cmbBotikak.Name = "cmbBotikak";
            cmbBotikak.Size = new Size(300, 53);
            cmbBotikak.TabIndex = 1;
            // 
            // lblDosia
            // 
            lblDosia.Location = new Point(351, 46);
            lblDosia.Name = "lblDosia";
            lblDosia.Size = new Size(136, 50);
            lblDosia.TabIndex = 2;
            lblDosia.Text = "Dosia:";
            // 
            // txtDosia
            // 
            txtDosia.Location = new Point(351, 99);
            txtDosia.Name = "txtDosia";
            txtDosia.PlaceholderText = "(adib. 1 pilula)";
            txtDosia.Size = new Size(272, 50);
            txtDosia.TabIndex = 3;
            // 
            // lblMaiztasuna
            // 
            lblMaiztasuna.Location = new Point(656, 46);
            lblMaiztasuna.Name = "lblMaiztasuna";
            lblMaiztasuna.Size = new Size(214, 50);
            lblMaiztasuna.TabIndex = 4;
            lblMaiztasuna.Text = "Maiztasuna:";
            // 
            // txtMaiztasuna
            // 
            txtMaiztasuna.Location = new Point(656, 99);
            txtMaiztasuna.Name = "txtMaiztasuna";
            txtMaiztasuna.PlaceholderText = "(adib. 8 ordu)";
            txtMaiztasuna.Size = new Size(244, 50);
            txtMaiztasuna.TabIndex = 5;
            // 
            // btnGehituBotika
            // 
            btnGehituBotika.BackColor = Color.FromArgb(46, 204, 113);
            btnGehituBotika.FlatStyle = FlatStyle.Flat;
            btnGehituBotika.ForeColor = Color.White;
            btnGehituBotika.Location = new Point(1109, 124);
            btnGehituBotika.Name = "btnGehituBotika";
            btnGehituBotika.Size = new Size(207, 50);
            btnGehituBotika.TabIndex = 6;
            btnGehituBotika.Text = "Gehitu";
            btnGehituBotika.UseVisualStyleBackColor = false;
            // 
            // dgvBotikak
            // 
            dgvBotikak.AllowUserToAddRows = false;
            dgvBotikak.AllowUserToDeleteRows = false;
            dgvBotikak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBotikak.BackgroundColor = Color.White;
            dgvBotikak.ColumnHeadersHeight = 46;
            dgvBotikak.Location = new Point(6, 180);
            dgvBotikak.MultiSelect = false;
            dgvBotikak.Name = "dgvBotikak";
            dgvBotikak.ReadOnly = true;
            dgvBotikak.RowHeadersVisible = false;
            dgvBotikak.RowHeadersWidth = 82;
            dgvBotikak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBotikak.Size = new Size(1310, 354);
            dgvBotikak.TabIndex = 7;
            // 
            // btnKenduBotika
            // 
            btnKenduBotika.BackColor = Color.FromArgb(231, 76, 60);
            btnKenduBotika.FlatStyle = FlatStyle.Flat;
            btnKenduBotika.ForeColor = Color.White;
            btnKenduBotika.Location = new Point(1002, 540);
            btnKenduBotika.Name = "btnKenduBotika";
            btnKenduBotika.Size = new Size(314, 59);
            btnKenduBotika.TabIndex = 8;
            btnKenduBotika.Text = "Aukeratua Kendu";
            btnKenduBotika.UseVisualStyleBackColor = false;
            // 
            // dtpIraungitzeData
            // 
            dtpIraungitzeData.Font = new Font("Segoe UI", 12F);
            dtpIraungitzeData.Format = DateTimePickerFormat.Short;
            dtpIraungitzeData.Location = new Point(860, 124);
            dtpIraungitzeData.Name = "dtpIraungitzeData";
            dtpIraungitzeData.Size = new Size(224, 50);
            dtpIraungitzeData.TabIndex = 2;
            // 
            // lblIraungitzeData
            // 
            lblIraungitzeData.Font = new Font("Segoe UI", 12F);
            lblIraungitzeData.ForeColor = Color.White;
            lblIraungitzeData.Location = new Point(860, 71);
            lblIraungitzeData.Name = "lblIraungitzeData";
            lblIraungitzeData.Size = new Size(210, 50);
            lblIraungitzeData.TabIndex = 3;
            lblIraungitzeData.Text = "Iraungitze Data:";
            // 
            // txtDiagnostikoa
            // 
            txtDiagnostikoa.Font = new Font("Segoe UI", 12F);
            txtDiagnostikoa.Location = new Point(166, 126);
            txtDiagnostikoa.Name = "txtDiagnostikoa";
            txtDiagnostikoa.Size = new Size(530, 50);
            txtDiagnostikoa.TabIndex = 4;
            // 
            // lblDiagnostikoa
            // 
            lblDiagnostikoa.Font = new Font("Segoe UI", 12F);
            lblDiagnostikoa.ForeColor = Color.White;
            lblDiagnostikoa.Location = new Point(160, 76);
            lblDiagnostikoa.Name = "lblDiagnostikoa";
            lblDiagnostikoa.Size = new Size(250, 64);
            lblDiagnostikoa.TabIndex = 5;
            lblDiagnostikoa.Text = "Diagnostikoa:";
            // 
            // lblErrezetaDatuak
            // 
            lblErrezetaDatuak.Dock = DockStyle.Top;
            lblErrezetaDatuak.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblErrezetaDatuak.ForeColor = Color.White;
            lblErrezetaDatuak.Location = new Point(20, 20);
            lblErrezetaDatuak.Name = "lblErrezetaDatuak";
            lblErrezetaDatuak.Size = new Size(1412, 56);
            lblErrezetaDatuak.TabIndex = 6;
            lblErrezetaDatuak.Text = "2. Errezetaren Xehetasunak";
            // 
            // ErrezetaSortu
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1280);
            Name = "ErrezetaSortu";
            Text = "GOsasun - Errezetak";
            _edukiPanela.ResumeLayout(false);
            pnlEzkerra.ResumeLayout(false);
            pnlEzkerra.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPazienteak).EndInit();
            pnlEskuina.ResumeLayout(false);
            pnlEskuina.PerformLayout();
            grpBotikak.ResumeLayout(false);
            grpBotikak.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBotikak).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblIzenburua;
        private System.Windows.Forms.Panel pnlEzkerra;
        private System.Windows.Forms.Label lblPazientea;
        private System.Windows.Forms.TextBox txtBilatuPaz;
        private System.Windows.Forms.DataGridView dgvPazienteak;
        
        private System.Windows.Forms.Panel pnlEskuina;
        private System.Windows.Forms.Label lblErrezetaDatuak;
        private System.Windows.Forms.Label lblDiagnostikoa;
        private System.Windows.Forms.TextBox txtDiagnostikoa;
        private System.Windows.Forms.Label lblIraungitzeData;
        private System.Windows.Forms.DateTimePicker dtpIraungitzeData;
        
        private System.Windows.Forms.GroupBox grpBotikak;
        private System.Windows.Forms.Label lblBotika;
        private System.Windows.Forms.ComboBox cmbBotikak;
        private System.Windows.Forms.Label lblDosia;
        private System.Windows.Forms.TextBox txtDosia;
        private System.Windows.Forms.Label lblMaiztasuna;
        private System.Windows.Forms.TextBox txtMaiztasuna;
        private System.Windows.Forms.Button btnGehituBotika;
        private System.Windows.Forms.DataGridView dgvBotikak;
        private System.Windows.Forms.Button btnKenduBotika;
        
        private System.Windows.Forms.Button btnSortuErrezeta;
    }
}
