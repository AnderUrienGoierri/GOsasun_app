namespace GOsasun_app.Interfazea
{
    partial class JarraipenaEditatuLaguntzailea
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

        private void InitializeComponent()
        {
            lblGoiburua = new Label();
            tlpBalioak = new TableLayoutPanel();
            lblSistolikoa = new Label();
            txtSistolikoa = new TextBox();
            lblDiastolikoa = new Label();
            txtDiastolikoa = new TextBox();
            lblPultsua = new Label();
            txtPultsua = new TextBox();
            lblPisua = new Label();
            txtPisua = new TextBox();
            lblAltuera = new Label();
            txtAltuera = new TextBox();
            lblOharrak = new Label();
            txtOharrak = new TextBox();
            lblDokumentuak = new Label();
            dgvDokumentuak = new DataGridView();
            colDokumentua = new DataGridViewTextBoxColumn();
            colFitxategia = new DataGridViewTextBoxColumn();
            colIgotzeData = new DataGridViewTextBoxColumn();
            colIreki = new DataGridViewButtonColumn();
            colEzabatu = new DataGridViewButtonColumn();
            btnUtzi = new Button();
            btnGorde = new Button();
            tlpBalioak.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDokumentuak).BeginInit();
            SuspendLayout();
            // 
            // lblGoiburua
            // 
            lblGoiburua.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblGoiburua.ForeColor = Color.FromArgb(44, 62, 80);
            lblGoiburua.Location = new Point(24, 24);
            lblGoiburua.Name = "lblGoiburua";
            lblGoiburua.Size = new Size(1220, 72);
            lblGoiburua.TabIndex = 0;
            lblGoiburua.Text = "Goiburua";
            lblGoiburua.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tlpBalioak
            // 
            tlpBalioak.ColumnCount = 4;
            tlpBalioak.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tlpBalioak.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 350F));
            tlpBalioak.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tlpBalioak.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 350F));
            tlpBalioak.Controls.Add(lblSistolikoa, 0, 0);
            tlpBalioak.Controls.Add(txtSistolikoa, 1, 0);
            tlpBalioak.Controls.Add(lblDiastolikoa, 2, 0);
            tlpBalioak.Controls.Add(txtDiastolikoa, 3, 0);
            tlpBalioak.Controls.Add(lblPultsua, 0, 1);
            tlpBalioak.Controls.Add(txtPultsua, 1, 1);
            tlpBalioak.Controls.Add(lblPisua, 2, 1);
            tlpBalioak.Controls.Add(txtPisua, 3, 1);
            tlpBalioak.Controls.Add(lblAltuera, 0, 2);
            tlpBalioak.Controls.Add(txtAltuera, 1, 2);
            tlpBalioak.Location = new Point(24, 116);
            tlpBalioak.Name = "tlpBalioak";
            tlpBalioak.RowCount = 3;
            tlpBalioak.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
            tlpBalioak.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
            tlpBalioak.RowStyles.Add(new RowStyle(SizeType.Absolute, 58F));
            tlpBalioak.Size = new Size(1000, 174);
            tlpBalioak.TabIndex = 1;
            // 
            // lblSistolikoa
            // 
            lblSistolikoa.Dock = DockStyle.Fill;
            lblSistolikoa.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblSistolikoa.ForeColor = Color.FromArgb(44, 62, 80);
            lblSistolikoa.Location = new Point(3, 0);
            lblSistolikoa.Name = "lblSistolikoa";
            lblSistolikoa.Size = new Size(144, 58);
            lblSistolikoa.TabIndex = 0;
            lblSistolikoa.Text = "Sistolikoa";
            lblSistolikoa.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtSistolikoa
            // 
            txtSistolikoa.Dock = DockStyle.Fill;
            txtSistolikoa.Font = new Font("Segoe UI", 10.5F);
            txtSistolikoa.Location = new Point(153, 3);
            txtSistolikoa.Name = "txtSistolikoa";
            txtSistolikoa.Size = new Size(344, 45);
            txtSistolikoa.TabIndex = 1;
            // 
            // lblDiastolikoa
            // 
            lblDiastolikoa.Dock = DockStyle.Fill;
            lblDiastolikoa.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblDiastolikoa.ForeColor = Color.FromArgb(44, 62, 80);
            lblDiastolikoa.Location = new Point(503, 0);
            lblDiastolikoa.Name = "lblDiastolikoa";
            lblDiastolikoa.Size = new Size(144, 58);
            lblDiastolikoa.TabIndex = 2;
            lblDiastolikoa.Text = "Diastolik";
            lblDiastolikoa.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtDiastolikoa
            // 
            txtDiastolikoa.Dock = DockStyle.Fill;
            txtDiastolikoa.Font = new Font("Segoe UI", 10.5F);
            txtDiastolikoa.Location = new Point(653, 3);
            txtDiastolikoa.Name = "txtDiastolikoa";
            txtDiastolikoa.Size = new Size(344, 45);
            txtDiastolikoa.TabIndex = 3;
            // 
            // lblPultsua
            // 
            lblPultsua.Dock = DockStyle.Fill;
            lblPultsua.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblPultsua.ForeColor = Color.FromArgb(44, 62, 80);
            lblPultsua.Location = new Point(3, 58);
            lblPultsua.Name = "lblPultsua";
            lblPultsua.Size = new Size(144, 58);
            lblPultsua.TabIndex = 4;
            lblPultsua.Text = "Pultsua";
            lblPultsua.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtPultsua
            // 
            txtPultsua.Dock = DockStyle.Fill;
            txtPultsua.Font = new Font("Segoe UI", 10.5F);
            txtPultsua.Location = new Point(153, 61);
            txtPultsua.Name = "txtPultsua";
            txtPultsua.Size = new Size(344, 45);
            txtPultsua.TabIndex = 5;
            // 
            // lblPisua
            // 
            lblPisua.Dock = DockStyle.Fill;
            lblPisua.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblPisua.ForeColor = Color.FromArgb(44, 62, 80);
            lblPisua.Location = new Point(503, 58);
            lblPisua.Name = "lblPisua";
            lblPisua.Size = new Size(144, 58);
            lblPisua.TabIndex = 6;
            lblPisua.Text = "Pisua";
            lblPisua.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtPisua
            // 
            txtPisua.Dock = DockStyle.Fill;
            txtPisua.Font = new Font("Segoe UI", 10.5F);
            txtPisua.Location = new Point(653, 61);
            txtPisua.Name = "txtPisua";
            txtPisua.Size = new Size(344, 45);
            txtPisua.TabIndex = 7;
            // 
            // lblAltuera
            // 
            lblAltuera.Dock = DockStyle.Fill;
            lblAltuera.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblAltuera.ForeColor = Color.FromArgb(44, 62, 80);
            lblAltuera.Location = new Point(3, 116);
            lblAltuera.Name = "lblAltuera";
            lblAltuera.Size = new Size(144, 58);
            lblAltuera.TabIndex = 8;
            lblAltuera.Text = "Altuera";
            lblAltuera.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtAltuera
            // 
            txtAltuera.Dock = DockStyle.Fill;
            txtAltuera.Font = new Font("Segoe UI", 10.5F);
            txtAltuera.Location = new Point(153, 119);
            txtAltuera.Name = "txtAltuera";
            txtAltuera.Size = new Size(344, 45);
            txtAltuera.TabIndex = 9;
            // 
            // lblOharrak
            // 
            lblOharrak.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblOharrak.ForeColor = Color.FromArgb(44, 62, 80);
            lblOharrak.Location = new Point(24, 322);
            lblOharrak.Name = "lblOharrak";
            lblOharrak.Size = new Size(160, 42);
            lblOharrak.TabIndex = 2;
            lblOharrak.Text = "Oharrak";
            // 
            // txtOharrak
            // 
            txtOharrak.Font = new Font("Segoe UI", 10.5F);
            txtOharrak.Location = new Point(24, 370);
            txtOharrak.Multiline = true;
            txtOharrak.Name = "txtOharrak";
            txtOharrak.ScrollBars = ScrollBars.Vertical;
            txtOharrak.Size = new Size(1080, 150);
            txtOharrak.TabIndex = 3;
            // 
            // lblDokumentuak
            // 
            lblDokumentuak.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblDokumentuak.ForeColor = Color.FromArgb(44, 62, 80);
            lblDokumentuak.Location = new Point(24, 552);
            lblDokumentuak.Name = "lblDokumentuak";
            lblDokumentuak.Size = new Size(360, 42);
            lblDokumentuak.TabIndex = 4;
            lblDokumentuak.Text = "Esleitutako dokumentuak";
            // 
            // dgvDokumentuak
            // 
            dgvDokumentuak.AllowUserToAddRows = false;
            dgvDokumentuak.AllowUserToDeleteRows = false;
            dgvDokumentuak.AllowUserToResizeRows = false;
            dgvDokumentuak.BackgroundColor = Color.White;
            dgvDokumentuak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDokumentuak.Columns.AddRange(new DataGridViewColumn[] { colDokumentua, colFitxategia, colIgotzeData, colIreki, colEzabatu });
            dgvDokumentuak.Location = new Point(24, 608);
            dgvDokumentuak.MultiSelect = false;
            dgvDokumentuak.Name = "dgvDokumentuak";
            dgvDokumentuak.ReadOnly = true;
            dgvDokumentuak.RowHeadersVisible = false;
            dgvDokumentuak.RowHeadersWidth = 82;
            dgvDokumentuak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDokumentuak.Size = new Size(1080, 280);
            dgvDokumentuak.TabIndex = 5;
            // 
            // colDokumentua
            // 
            colDokumentua.DataPropertyName = "DokumentuIzena";
            colDokumentua.HeaderText = "Dokumentua";
            colDokumentua.MinimumWidth = 10;
            colDokumentua.Name = "colDokumentua";
            colDokumentua.ReadOnly = true;
            colDokumentua.Width = 260;
            // 
            // colFitxategia
            // 
            colFitxategia.DataPropertyName = "FitxategiIzena";
            colFitxategia.HeaderText = "Fitxategia";
            colFitxategia.MinimumWidth = 10;
            colFitxategia.Name = "colFitxategia";
            colFitxategia.ReadOnly = true;
            colFitxategia.Width = 260;
            // 
            // colIgotzeData
            // 
            colIgotzeData.DataPropertyName = "IgotzeData";
            colIgotzeData.DefaultCellStyle = new DataGridViewCellStyle { Format = "g" };
            colIgotzeData.HeaderText = "Igotze data";
            colIgotzeData.MinimumWidth = 10;
            colIgotzeData.Name = "colIgotzeData";
            colIgotzeData.ReadOnly = true;
            colIgotzeData.Width = 160;
            // 
            // colIreki
            // 
            colIreki.HeaderText = "";
            colIreki.MinimumWidth = 10;
            colIreki.Name = "colIreki";
            colIreki.ReadOnly = true;
            colIreki.Text = "Ireki";
            colIreki.UseColumnTextForButtonValue = true;
            colIreki.Width = 90;
            // 
            // colEzabatu
            // 
            colEzabatu.HeaderText = "";
            colEzabatu.MinimumWidth = 10;
            colEzabatu.Name = "colEzabatu";
            colEzabatu.ReadOnly = true;
            colEzabatu.Text = "Ezabatu";
            colEzabatu.UseColumnTextForButtonValue = true;
            colEzabatu.Width = 90;
            // 
            // btnUtzi
            // 
            btnUtzi.DialogResult = DialogResult.Cancel;
            btnUtzi.Location = new Point(874, 914);
            btnUtzi.Name = "btnUtzi";
            btnUtzi.Size = new Size(120, 46);
            btnUtzi.TabIndex = 6;
            btnUtzi.Text = "Utzi";
            btnUtzi.UseVisualStyleBackColor = true;
            // 
            // btnGorde
            // 
            btnGorde.BackColor = Color.FromArgb(41, 128, 185);
            btnGorde.FlatAppearance.BorderSize = 0;
            btnGorde.FlatStyle = FlatStyle.Flat;
            btnGorde.ForeColor = Color.White;
            btnGorde.Location = new Point(1014, 914);
            btnGorde.Name = "btnGorde";
            btnGorde.Size = new Size(230, 46);
            btnGorde.TabIndex = 7;
            btnGorde.Text = "Gorde aldaketak";
            btnGorde.UseVisualStyleBackColor = false;
            // 
            // JarraipenaEditatuLaguntzailea
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1320, 1000);
            Controls.Add(btnGorde);
            Controls.Add(btnUtzi);
            Controls.Add(dgvDokumentuak);
            Controls.Add(lblDokumentuak);
            Controls.Add(txtOharrak);
            Controls.Add(lblOharrak);
            Controls.Add(tlpBalioak);
            Controls.Add(lblGoiburua);
            Name = "JarraipenaEditatuLaguntzailea";
            Text = "Jarraipena editatu";
            tlpBalioak.ResumeLayout(false);
            tlpBalioak.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDokumentuak).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblGoiburua;
        private TableLayoutPanel tlpBalioak;
        private Label lblSistolikoa;
        private TextBox txtSistolikoa;
        private Label lblDiastolikoa;
        private TextBox txtDiastolikoa;
        private Label lblPultsua;
        private TextBox txtPultsua;
        private Label lblPisua;
        private TextBox txtPisua;
        private Label lblAltuera;
        private TextBox txtAltuera;
        private Label lblOharrak;
        private TextBox txtOharrak;
        private Label lblDokumentuak;
        private DataGridView dgvDokumentuak;
        private DataGridViewTextBoxColumn colDokumentua;
        private DataGridViewTextBoxColumn colFitxategia;
        private DataGridViewTextBoxColumn colIgotzeData;
        private DataGridViewButtonColumn colIreki;
        private DataGridViewButtonColumn colEzabatu;
        private Button btnUtzi;
        private Button btnGorde;
    }
}