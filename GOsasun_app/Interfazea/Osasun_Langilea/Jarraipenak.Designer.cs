namespace GOsasun_app.Interfazea
{
    partial class Jarraipenak
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            _colPazienteNan = new DataGridViewTextBoxColumn();
            _colPazienteIzena = new DataGridViewTextBoxColumn();
            _colPazienteAbizenak = new DataGridViewTextBoxColumn();
            _colErregistroData = new DataGridViewTextBoxColumn();
            _colTentsioSistolikoa = new DataGridViewTextBoxColumn();
            _colTentsioDiastolikoa = new DataGridViewTextBoxColumn();
            _colPultsuaPpm = new DataGridViewTextBoxColumn();
            _colPisuaKg = new DataGridViewTextBoxColumn();
            _colAltuera = new DataGridViewTextBoxColumn();
            _colDokumentuKopurua = new DataGridViewTextBoxColumn();
            _colOharrak = new DataGridViewTextBoxColumn();
            _colEkintzak = new DataGridViewTextBoxColumn();
            _lblIzenburua = new Label();
            _btnJarraipenBerria = new Button();
            _lblBilatu = new Label();
            _txtBilatu = new TextBox();
            _lblDataFiltroa = new Label();
            _dtpHasieraData = new DateTimePicker();
            _dtpAmaieraData = new DateTimePicker();
            _btnFiltroakGarbitu = new Button();
            _chkJarraipenGuztiakIkusi = new CheckBox();
            _dgvJarraipenak = new DataGridView();
            _edukiPanela.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgvJarraipenak).BeginInit();
            SuspendLayout();
            // 
            // _edukiPanela
            // 
            _edukiPanela.Controls.Add(_dgvJarraipenak);
            _edukiPanela.Controls.Add(_chkJarraipenGuztiakIkusi);
            _edukiPanela.Controls.Add(_btnFiltroakGarbitu);
            _edukiPanela.Controls.Add(_dtpAmaieraData);
            _edukiPanela.Controls.Add(_dtpHasieraData);
            _edukiPanela.Controls.Add(_lblDataFiltroa);
            _edukiPanela.Controls.Add(_txtBilatu);
            _edukiPanela.Controls.Add(_lblBilatu);
            _edukiPanela.Controls.Add(_btnJarraipenBerria);
            _edukiPanela.Controls.Add(_lblIzenburua);
            _edukiPanela.Margin = new Padding(6);
            _edukiPanela.Padding = new Padding(4);
            // 
            // _goiburuBarra
            // 
            _goiburuBarra.Margin = new Padding(6);
            _goiburuBarra.Padding = new Padding(37, 21, 37, 21);
            // 
            // _atzeraBotoia
            // 
            _atzeraBotoia.FlatAppearance.BorderSize = 0;
            _atzeraBotoia.Margin = new Padding(6);
            // 
            // _colPazienteNan
            // 
            _colPazienteNan.DataPropertyName = "PazienteNan";
            _colPazienteNan.HeaderText = "NAN/DNI";
            _colPazienteNan.MinimumWidth = 140;
            _colPazienteNan.Name = "_colPazienteNan";
            _colPazienteNan.ReadOnly = true;
            _colPazienteNan.Width = 140;
            // 
            // _colPazienteIzena
            // 
            _colPazienteIzena.DataPropertyName = "PazienteIzena";
            _colPazienteIzena.HeaderText = "Izena";
            _colPazienteIzena.MinimumWidth = 125;
            _colPazienteIzena.Name = "_colPazienteIzena";
            _colPazienteIzena.ReadOnly = true;
            _colPazienteIzena.Width = 125;
            // 
            // _colPazienteAbizenak
            // 
            _colPazienteAbizenak.DataPropertyName = "PazienteAbizenak";
            _colPazienteAbizenak.HeaderText = "Abizenak";
            _colPazienteAbizenak.MinimumWidth = 170;
            _colPazienteAbizenak.Name = "_colPazienteAbizenak";
            _colPazienteAbizenak.ReadOnly = true;
            _colPazienteAbizenak.Width = 170;
            // 
            // _colErregistroData
            // 
            _colErregistroData.DataPropertyName = "ErregistroData";
            _colErregistroData.HeaderText = "Data";
            _colErregistroData.MinimumWidth = 205;
            _colErregistroData.Name = "_colErregistroData";
            _colErregistroData.ReadOnly = true;
            _colErregistroData.Width = 205;
            // 
            // _colTentsioSistolikoa
            // 
            _colTentsioSistolikoa.DataPropertyName = "TentsioSistolikoa";
            _colTentsioSistolikoa.HeaderText = "Sist.";
            _colTentsioSistolikoa.MinimumWidth = 78;
            _colTentsioSistolikoa.Name = "_colTentsioSistolikoa";
            _colTentsioSistolikoa.ReadOnly = true;
            _colTentsioSistolikoa.Width = 78;
            // 
            // _colTentsioDiastolikoa
            // 
            _colTentsioDiastolikoa.DataPropertyName = "TentsioDiastolikoa";
            _colTentsioDiastolikoa.HeaderText = "Diast.";
            _colTentsioDiastolikoa.MinimumWidth = 78;
            _colTentsioDiastolikoa.Name = "_colTentsioDiastolikoa";
            _colTentsioDiastolikoa.ReadOnly = true;
            _colTentsioDiastolikoa.Width = 78;
            // 
            // _colPultsuaPpm
            // 
            _colPultsuaPpm.DataPropertyName = "PultsuaPpm";
            _colPultsuaPpm.HeaderText = "Pultsua";
            _colPultsuaPpm.MinimumWidth = 95;
            _colPultsuaPpm.Name = "_colPultsuaPpm";
            _colPultsuaPpm.ReadOnly = true;
            _colPultsuaPpm.Width = 95;
            // 
            // _colPisuaKg
            // 
            _colPisuaKg.DataPropertyName = "PisuaKg";
            _colPisuaKg.HeaderText = "Pisua (kg)";
            _colPisuaKg.MinimumWidth = 110;
            _colPisuaKg.Name = "_colPisuaKg";
            _colPisuaKg.ReadOnly = true;
            _colPisuaKg.Width = 110;
            // 
            // _colAltuera
            // 
            _colAltuera.DataPropertyName = "Altuera";
            _colAltuera.HeaderText = "Altuera (m)";
            _colAltuera.MinimumWidth = 110;
            _colAltuera.Name = "_colAltuera";
            _colAltuera.ReadOnly = true;
            _colAltuera.Width = 110;
            // 
            // _colDokumentuKopurua
            // 
            _colDokumentuKopurua.DataPropertyName = "DokumentuKopurua";
            _colDokumentuKopurua.HeaderText = "Dok.";
            _colDokumentuKopurua.MinimumWidth = 70;
            _colDokumentuKopurua.Name = "_colDokumentuKopurua";
            _colDokumentuKopurua.ReadOnly = true;
            _colDokumentuKopurua.Width = 70;
            // 
            // _colOharrak
            // 
            _colOharrak.DataPropertyName = "Oharrak";
            _colOharrak.HeaderText = "Oharrak";
            _colOharrak.MinimumWidth = 430;
            _colOharrak.Name = "_colOharrak";
            _colOharrak.ReadOnly = true;
            _colOharrak.Width = 430;
            // 
            // _colEkintzak
            // 
            _colEkintzak.DataPropertyName = "EkintzakTestua";
            _colEkintzak.HeaderText = "EKINTZAK";
            _colEkintzak.MinimumWidth = 280;
            _colEkintzak.Name = "_colEkintzak";
            _colEkintzak.ReadOnly = true;
            _colEkintzak.Width = 280;
            // 
            // _lblIzenburua
            // 
            _lblIzenburua.AutoSize = true;
            _lblIzenburua.BackColor = Color.Transparent;
            _lblIzenburua.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            _lblIzenburua.ForeColor = Color.FromArgb(44, 62, 80);
            _lblIzenburua.Location = new Point(18, -18);
            _lblIzenburua.Margin = new Padding(6, 0, 6, 0);
            _lblIzenburua.Name = "_lblIzenburua";
            _lblIzenburua.Size = new Size(510, 93);
            _lblIzenburua.TabIndex = 0;
            _lblIzenburua.Text = "JARRAIPENAK";
            // 
            // _btnJarraipenBerria
            // 
            _btnJarraipenBerria.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnJarraipenBerria.BackColor = Color.FromArgb(41, 128, 185);
            _btnJarraipenBerria.FlatAppearance.BorderSize = 0;
            _btnJarraipenBerria.FlatStyle = FlatStyle.Flat;
            _btnJarraipenBerria.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            _btnJarraipenBerria.ForeColor = Color.White;
            _btnJarraipenBerria.Location = new Point(1512, 35);
            _btnJarraipenBerria.Margin = new Padding(6);
            _btnJarraipenBerria.Name = "_btnJarraipenBerria";
            _btnJarraipenBerria.Size = new Size(320, 64);
            _btnJarraipenBerria.TabIndex = 1;
            _btnJarraipenBerria.Text = "Jarraipen berria";
            _btnJarraipenBerria.UseVisualStyleBackColor = false;
            // 
            // _lblBilatu
            // 
            _lblBilatu.AutoSize = true;
            _lblBilatu.BackColor = Color.Transparent;
            _lblBilatu.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            _lblBilatu.ForeColor = Color.FromArgb(44, 62, 80);
            _lblBilatu.Location = new Point(18, 63);
            _lblBilatu.Margin = new Padding(6, 0, 6, 0);
            _lblBilatu.Name = "_lblBilatu";
            _lblBilatu.Size = new Size(741, 45);
            _lblBilatu.TabIndex = 2;
            _lblBilatu.Text = "Bilatu pazientea (abizenak, izena edo NAN/DNI)";
            // 
            // _txtBilatu
            // 
            _txtBilatu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _txtBilatu.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _txtBilatu.Location = new Point(18, 109);
            _txtBilatu.Margin = new Padding(6);
            _txtBilatu.MinimumSize = new Size(900, 52);
            _txtBilatu.Name = "_txtBilatu";
            _txtBilatu.PlaceholderText = "Adibidez: Urrutia, Jon edo 000000001";
            _txtBilatu.Size = new Size(900, 52);
            _txtBilatu.TabIndex = 3;
            // 
            // _lblDataFiltroa
            // 
            _lblDataFiltroa.AutoSize = true;
            _lblDataFiltroa.BackColor = Color.Transparent;
            _lblDataFiltroa.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _lblDataFiltroa.ForeColor = Color.FromArgb(44, 62, 80);
            _lblDataFiltroa.Location = new Point(344, 167);
            _lblDataFiltroa.Margin = new Padding(6, 0, 6, 0);
            _lblDataFiltroa.Name = "_lblDataFiltroa";
            _lblDataFiltroa.Size = new Size(434, 41);
            _lblDataFiltroa.TabIndex = 4;
            _lblDataFiltroa.Text = "Data filtroa (hasiera-amaiera)";
            // 
            // _dtpHasieraData
            // 
            _dtpHasieraData.Checked = false;
            _dtpHasieraData.CustomFormat = "'Hasiera data: 'dd/MM/yyyy";
            _dtpHasieraData.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _dtpHasieraData.Format = DateTimePickerFormat.Custom;
            _dtpHasieraData.Location = new Point(635, 165);
            _dtpHasieraData.Margin = new Padding(6);
            _dtpHasieraData.Name = "_dtpHasieraData";
            _dtpHasieraData.ShowCheckBox = true;
            _dtpHasieraData.Size = new Size(291, 47);
            _dtpHasieraData.TabIndex = 5;
            _dtpHasieraData.ValueChanged += _dtpHasieraData_ValueChanged;
            // 
            // _dtpAmaieraData
            // 
            _dtpAmaieraData.Checked = false;
            _dtpAmaieraData.CustomFormat = "'Amaiera data: 'dd/MM/yyyy";
            _dtpAmaieraData.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _dtpAmaieraData.Format = DateTimePickerFormat.Custom;
            _dtpAmaieraData.Location = new Point(635, 211);
            _dtpAmaieraData.Margin = new Padding(6);
            _dtpAmaieraData.Name = "_dtpAmaieraData";
            _dtpAmaieraData.ShowCheckBox = true;
            _dtpAmaieraData.Size = new Size(291, 47);
            _dtpAmaieraData.TabIndex = 6;
            // 
            // _btnFiltroakGarbitu
            // 
            _btnFiltroakGarbitu.BackColor = Color.FromArgb(44, 62, 80);
            _btnFiltroakGarbitu.FlatAppearance.BorderSize = 0;
            _btnFiltroakGarbitu.FlatStyle = FlatStyle.Flat;
            _btnFiltroakGarbitu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _btnFiltroakGarbitu.ForeColor = Color.White;
            _btnFiltroakGarbitu.Location = new Point(393, 207);
            _btnFiltroakGarbitu.Margin = new Padding(6);
            _btnFiltroakGarbitu.Name = "_btnFiltroakGarbitu";
            _btnFiltroakGarbitu.Size = new Size(230, 47);
            _btnFiltroakGarbitu.TabIndex = 7;
            _btnFiltroakGarbitu.Text = "Filtroak garbitu";
            _btnFiltroakGarbitu.UseVisualStyleBackColor = false;
            // 
            // _chkJarraipenGuztiakIkusi
            // 
            _chkJarraipenGuztiakIkusi.AutoSize = true;
            _chkJarraipenGuztiakIkusi.BackColor = Color.Transparent;
            _chkJarraipenGuztiakIkusi.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            _chkJarraipenGuztiakIkusi.ForeColor = Color.FromArgb(44, 62, 80);
            _chkJarraipenGuztiakIkusi.Location = new Point(22, 163);
            _chkJarraipenGuztiakIkusi.Margin = new Padding(6);
            _chkJarraipenGuztiakIkusi.Name = "_chkJarraipenGuztiakIkusi";
            _chkJarraipenGuztiakIkusi.Size = new Size(344, 42);
            _chkJarraipenGuztiakIkusi.TabIndex = 8;
            _chkJarraipenGuztiakIkusi.Text = "Jarraipen guztiak ikusi";
            _chkJarraipenGuztiakIkusi.UseVisualStyleBackColor = false;
            // 
            // _dgvJarraipenak
            // 
            _dgvJarraipenak.AllowUserToAddRows = false;
            _dgvJarraipenak.AllowUserToDeleteRows = false;
            _dgvJarraipenak.AllowUserToResizeRows = false;
            _dgvJarraipenak.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _dgvJarraipenak.BackgroundColor = Color.White;
            _dgvJarraipenak.BorderStyle = BorderStyle.None;
            _dgvJarraipenak.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            _dgvJarraipenak.ColumnHeadersHeight = 96;
            _dgvJarraipenak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            _dgvJarraipenak.Columns.AddRange(new DataGridViewColumn[] { _colPazienteNan, _colPazienteIzena, _colPazienteAbizenak, _colErregistroData, _colTentsioSistolikoa, _colTentsioDiastolikoa, _colPultsuaPpm, _colPisuaKg, _colAltuera, _colDokumentuKopurua, _colOharrak, _colEkintzak });
            _dgvJarraipenak.EnableHeadersVisualStyles = false;
            _dgvJarraipenak.GridColor = Color.FromArgb(205, 211, 217);
            _dgvJarraipenak.Location = new Point(0, 255);
            _dgvJarraipenak.Margin = new Padding(6);
            _dgvJarraipenak.MultiSelect = false;
            _dgvJarraipenak.Name = "_dgvJarraipenak";
            _dgvJarraipenak.ReadOnly = true;
            _dgvJarraipenak.RowHeadersVisible = false;
            _dgvJarraipenak.RowHeadersWidth = 82;
            _dgvJarraipenak.RowTemplate.Height = 96;
            _dgvJarraipenak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgvJarraipenak.Size = new Size(1762, 991);
            _dgvJarraipenak.TabIndex = 9;
            // 
            // Jarraipenak
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1902, 1575);
            Margin = new Padding(11, 9, 11, 9);
            Name = "Jarraipenak";
            Text = "GOsasun - Jarraipenak";
            _edukiPanela.ResumeLayout(false);
            _edukiPanela.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_dgvJarraipenak).EndInit();
            ResumeLayout(false);
        }

        private Label _lblIzenburua;
        private Button _btnJarraipenBerria;
        private Label _lblBilatu;
        private TextBox _txtBilatu;
        private Label _lblDataFiltroa;
        private DateTimePicker _dtpHasieraData;
        private DateTimePicker _dtpAmaieraData;
        private Button _btnFiltroakGarbitu;
        private CheckBox _chkJarraipenGuztiakIkusi;
        private DataGridView _dgvJarraipenak;
        private DataGridViewTextBoxColumn _colPazienteNan;
        private DataGridViewTextBoxColumn _colPazienteIzena;
        private DataGridViewTextBoxColumn _colPazienteAbizenak;
        private DataGridViewTextBoxColumn _colErregistroData;
        private DataGridViewTextBoxColumn _colTentsioSistolikoa;
        private DataGridViewTextBoxColumn _colTentsioDiastolikoa;
        private DataGridViewTextBoxColumn _colPultsuaPpm;
        private DataGridViewTextBoxColumn _colPisuaKg;
        private DataGridViewTextBoxColumn _colAltuera;
        private DataGridViewTextBoxColumn _colDokumentuKopurua;
        private DataGridViewTextBoxColumn _colOharrak;
        private DataGridViewTextBoxColumn _colEkintzak;
    }
}

