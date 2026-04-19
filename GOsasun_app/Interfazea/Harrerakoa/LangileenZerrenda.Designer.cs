namespace GOsasun_app.Interfazea
{
    partial class LangileenZerrenda
    {
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            _lblIzenburua = new Label();
            _pnlBilatzailea = new Panel();
            _txtBilatu = new TextBox();
            _lblBilatu = new Label();
            _dgvLangileak = new DataGridView();
            _pnlPaginazioa = new Panel();
            _btnHurrengoOrria = new Button();
            _lblPaginazioa = new Label();
            _btnAurrekoOrria = new Button();
            _edukiPanela.SuspendLayout();
            _pnlBilatzailea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgvLangileak).BeginInit();
            _pnlPaginazioa.SuspendLayout();
            SuspendLayout();
            // 
            // _edukiPanela
            // 
            _edukiPanela.Controls.Add(_pnlPaginazioa);
            _edukiPanela.Controls.Add(_dgvLangileak);
            _edukiPanela.Controls.Add(_pnlBilatzailea);
            _edukiPanela.Controls.Add(_lblIzenburua);
            _edukiPanela.Padding = new Padding(4);
            // 
            // _atzeraBotoia
            // 
            _atzeraBotoia.FlatAppearance.BorderSize = 0;
            // 
            // _lblIzenburua
            // 
            _lblIzenburua.AutoSize = true;
            _lblIzenburua.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold);
            _lblIzenburua.ForeColor = Color.White;
            _lblIzenburua.Location = new Point(36, 22);
            _lblIzenburua.Name = "_lblIzenburua";
            _lblIzenburua.Size = new Size(572, 54);
            _lblIzenburua.TabIndex = 0;
            _lblIzenburua.Text = "OSASUN LANGILEEN ZERRENDA";
            // 
            // _pnlBilatzailea
            // 
            _pnlBilatzailea.BackColor = Color.White;
            _pnlBilatzailea.BorderStyle = BorderStyle.FixedSingle;
            _pnlBilatzailea.Controls.Add(_txtBilatu);
            _pnlBilatzailea.Controls.Add(_lblBilatu);
            _pnlBilatzailea.Location = new Point(36, 92);
            _pnlBilatzailea.Name = "_pnlBilatzailea";
            _pnlBilatzailea.Size = new Size(520, 78);
            _pnlBilatzailea.TabIndex = 1;
            // 
            // _txtBilatu
            // 
            _txtBilatu.Font = new Font("Segoe UI", 11F);
            _txtBilatu.Location = new Point(110, 18);
            _txtBilatu.Name = "_txtBilatu";
            _txtBilatu.PlaceholderText = "Izena, abizenak edo NAN";
            _txtBilatu.Size = new Size(380, 32);
            _txtBilatu.TabIndex = 1;
            // 
            // _lblBilatu
            // 
            _lblBilatu.AutoSize = true;
            _lblBilatu.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            _lblBilatu.ForeColor = Color.FromArgb(44, 62, 80);
            _lblBilatu.Location = new Point(24, 22);
            _lblBilatu.Name = "_lblBilatu";
            _lblBilatu.Size = new Size(58, 25);
            _lblBilatu.TabIndex = 0;
            _lblBilatu.Text = "Bilatu";
            // 
            // _dgvLangileak
            // 
            _dgvLangileak.AllowUserToAddRows = false;
            _dgvLangileak.AllowUserToDeleteRows = false;
            _dgvLangileak.AllowUserToResizeRows = false;
            _dgvLangileak.BackgroundColor = Color.White;
            _dgvLangileak.Location = new Point(36, 192);
            _dgvLangileak.MultiSelect = false;
            _dgvLangileak.Name = "_dgvLangileak";
            _dgvLangileak.ReadOnly = true;
            _dgvLangileak.RowHeadersVisible = false;
            _dgvLangileak.RowTemplate.Height = 42;
            _dgvLangileak.ScrollBars = ScrollBars.Both;
            _dgvLangileak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgvLangileak.Size = new Size(1300, 620);
            _dgvLangileak.TabIndex = 2;
            // 
            // _pnlPaginazioa
            // 
            _pnlPaginazioa.Controls.Add(_btnHurrengoOrria);
            _pnlPaginazioa.Controls.Add(_lblPaginazioa);
            _pnlPaginazioa.Controls.Add(_btnAurrekoOrria);
            _pnlPaginazioa.Location = new Point(36, 824);
            _pnlPaginazioa.Name = "_pnlPaginazioa";
            _pnlPaginazioa.Size = new Size(1300, 64);
            _pnlPaginazioa.TabIndex = 3;
            // 
            // _btnHurrengoOrria
            // 
            _btnHurrengoOrria.BackColor = Color.FromArgb(41, 128, 185);
            _btnHurrengoOrria.FlatAppearance.BorderSize = 0;
            _btnHurrengoOrria.FlatStyle = FlatStyle.Flat;
            _btnHurrengoOrria.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _btnHurrengoOrria.ForeColor = Color.White;
            _btnHurrengoOrria.Location = new Point(1150, 12);
            _btnHurrengoOrria.Name = "_btnHurrengoOrria";
            _btnHurrengoOrria.Size = new Size(150, 38);
            _btnHurrengoOrria.TabIndex = 2;
            _btnHurrengoOrria.Text = "Hurrengo 10ak";
            _btnHurrengoOrria.UseVisualStyleBackColor = false;
            // 
            // _lblPaginazioa
            // 
            _lblPaginazioa.ForeColor = Color.White;
            _lblPaginazioa.Location = new Point(168, 10);
            _lblPaginazioa.Name = "_lblPaginazioa";
            _lblPaginazioa.Size = new Size(964, 42);
            _lblPaginazioa.TabIndex = 1;
            _lblPaginazioa.Text = "1-10 / 10   |   1. orria / 1";
            _lblPaginazioa.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _btnAurrekoOrria
            // 
            _btnAurrekoOrria.BackColor = Color.FromArgb(52, 73, 94);
            _btnAurrekoOrria.FlatAppearance.BorderSize = 0;
            _btnAurrekoOrria.FlatStyle = FlatStyle.Flat;
            _btnAurrekoOrria.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _btnAurrekoOrria.ForeColor = Color.White;
            _btnAurrekoOrria.Location = new Point(0, 12);
            _btnAurrekoOrria.Name = "_btnAurrekoOrria";
            _btnAurrekoOrria.Size = new Size(150, 38);
            _btnAurrekoOrria.TabIndex = 0;
            _btnAurrekoOrria.Text = "Aurreko 10ak";
            _btnAurrekoOrria.UseVisualStyleBackColor = false;
            // 
            // LangileenZerrenda
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1680, 980);
            Name = "LangileenZerrenda";
            Text = "GOsasun - Langileen zerrenda";
            _edukiPanela.ResumeLayout(false);
            _edukiPanela.PerformLayout();
            _pnlBilatzailea.ResumeLayout(false);
            _pnlBilatzailea.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_dgvLangileak).EndInit();
            _pnlPaginazioa.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Label _lblIzenburua;
        private Panel _pnlBilatzailea;
        private TextBox _txtBilatu;
        private Label _lblBilatu;
        private DataGridView _dgvLangileak;
        private Panel _pnlPaginazioa;
        private Button _btnHurrengoOrria;
        private Label _lblPaginazioa;
        private Button _btnAurrekoOrria;
    }
}