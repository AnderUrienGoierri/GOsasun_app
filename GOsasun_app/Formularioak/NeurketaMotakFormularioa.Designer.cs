using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace GOsasun_app.Formularioak
{
    partial class NeurketaMotakFormularioa
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
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(NeurketaMotakFormularioa));
            btnTentsiometroa = new GOsasun_app.Kontrolak.CustomCardButton();
            btnGlukometroa = new GOsasun_app.Kontrolak.CustomCardButton();
            btnPisua = new GOsasun_app.Kontrolak.CustomCardButton();
            btnAltuera = new GOsasun_app.Kontrolak.CustomCardButton();
            _pnlImport = new Panel();
            _lblStatus = new Label();
            _txtPazienteBilatu = new TextBox();
            _dgvPazienteak = new DataGridView();
            _lblBilatu = new Label();
            _btnInportatu = new Button();
            _btnUtzi = new Button();
            _timerKonexioa = new System.Windows.Forms.Timer(components);
            _edukiPanela.SuspendLayout();
            _pnlImport.SuspendLayout();
            SuspendLayout();
            // 
            // _edukiPanela
            // 
            _edukiPanela.Controls.Add(_pnlImport);
            _edukiPanela.Controls.Add(btnTentsiometroa);
            _edukiPanela.Controls.Add(btnGlukometroa);
            _edukiPanela.Controls.Add(btnPisua);
            _edukiPanela.Controls.Add(btnAltuera);
            _edukiPanela.Size = new Size(1902, 1032);
            // 
            // _atzeraBotoia
            // 
            _atzeraBotoia.FlatAppearance.BorderSize = 0;
            // 
            // btnTentsiometroa
            // 
            btnTentsiometroa.BackColor = Color.White;
            btnTentsiometroa.BorderBiribiltasuna = 24;
            btnTentsiometroa.Ikonoa = (Image)resources.GetObject("btnTentsiometroa.Ikonoa");
            btnTentsiometroa.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnTentsiometroa.Location = new Point(357, 47);
            btnTentsiometroa.Name = "btnTentsiometroa";
            btnTentsiometroa.Size = new Size(557, 427);
            btnTentsiometroa.TabIndex = 0;
            btnTentsiometroa.Testua = "TENTSIOMETROA";
            // 
            // btnGlukometroa
            // 
            btnGlukometroa.BackColor = Color.White;
            btnGlukometroa.BorderBiribiltasuna = 24;
            btnGlukometroa.Ikonoa = (Image)resources.GetObject("btnGlukometroa.Ikonoa");
            btnGlukometroa.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnGlukometroa.Location = new Point(988, 49);
            btnGlukometroa.Name = "btnGlukometroa";
            btnGlukometroa.Size = new Size(557, 427);
            btnGlukometroa.TabIndex = 1;
            btnGlukometroa.Testua = "GLUKOMETROA";
            // 
            // btnPisua
            // 
            btnPisua.BackColor = Color.White;
            btnPisua.BorderBiribiltasuna = 24;
            btnPisua.Ikonoa = (Image)resources.GetObject("btnPisua.Ikonoa");
            btnPisua.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnPisua.Location = new Point(357, 560);
            btnPisua.Name = "btnPisua";
            btnPisua.Size = new Size(557, 427);
            btnPisua.TabIndex = 2;
            btnPisua.Testua = "PISUA";
            // 
            // btnAltuera
            // 
            btnAltuera.BackColor = Color.White;
            btnAltuera.BorderBiribiltasuna = 24;
            btnAltuera.Ikonoa = (Image)resources.GetObject("btnAltuera.Ikonoa");
            btnAltuera.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnAltuera.Location = new Point(988, 560);
            btnAltuera.Name = "btnAltuera";
            btnAltuera.Size = new Size(557, 427);
            btnAltuera.TabIndex = 3;
            btnAltuera.Testua = "ALTUERA";
            // 
            // _pnlImport
            // 
            _pnlImport.BackColor = Color.FromArgb(240, 255, 255, 255);
            _pnlImport.Controls.Add(_lblBilatu);
            _pnlImport.Controls.Add(_txtPazienteBilatu);
            _pnlImport.Controls.Add(_dgvPazienteak);
            _pnlImport.Controls.Add(_btnInportatu);
            _pnlImport.Controls.Add(_btnUtzi);
            _pnlImport.Controls.Add(_lblStatus);
            _pnlImport.Location = new Point(357, 47);
            _pnlImport.Name = "_pnlImport";
            _pnlImport.Padding = new Padding(30);
            _pnlImport.Size = new Size(1188, 940);
            _pnlImport.TabIndex = 4;
            _pnlImport.Visible = false;
            // 
            // _lblStatus
            // 
            _lblStatus.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            _lblStatus.ForeColor = Color.FromArgb(44, 62, 80);
            _lblStatus.Location = new Point(50, 50);
            _lblStatus.Name = "_lblStatus";
            _lblStatus.Size = new Size(1088, 500); // 200tik 500era handitu
            _lblStatus.TabIndex = 0;
            _lblStatus.Text = "Konektatu Beurer BM58 USB bidez...";
            _lblStatus.TextAlign = ContentAlignment.TopCenter; // MiddleCenter-etik TopCenter-era
            // 
            // _lblBilatu
            // 
            _lblBilatu.AutoSize = true;
            _lblBilatu.Font = new Font("Segoe UI", 14F);
            _lblBilatu.Location = new Point(50, 260);
            _lblBilatu.Name = "_lblBilatu";
            _lblBilatu.Size = new Size(295, 51);
            _lblBilatu.TabIndex = 1;
            _lblBilatu.Text = "Pazientea bilatu:";
            _lblBilatu.Visible = false;
            // 
            // _txtPazienteBilatu
            // 
            _txtPazienteBilatu.Font = new Font("Segoe UI", 18F);
            _txtPazienteBilatu.Location = new Point(50, 320);
            _txtPazienteBilatu.Name = "_txtPazienteBilatu";
            _txtPazienteBilatu.PlaceholderText = "Idatzi izena edo abizena...";
            _txtPazienteBilatu.Size = new Size(1088, 71);
            _txtPazienteBilatu.TabIndex = 2;
            _txtPazienteBilatu.Visible = false;
            // 
            // _dgvPazienteak
            // 
            _dgvPazienteak.AllowUserToAddRows = false;
            _dgvPazienteak.AllowUserToDeleteRows = false;
            _dgvPazienteak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dgvPazienteak.BackgroundColor = Color.White;
            _dgvPazienteak.BorderStyle = BorderStyle.None;
            _dgvPazienteak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dgvPazienteak.Location = new Point(50, 410);
            _dgvPazienteak.MultiSelect = false;
            _dgvPazienteak.Name = "_dgvPazienteak";
            _dgvPazienteak.ReadOnly = true;
            _dgvPazienteak.RowHeadersVisible = false;
            _dgvPazienteak.RowTemplate.Height = 60;
            _dgvPazienteak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgvPazienteak.Size = new Size(1088, 350);
            _dgvPazienteak.TabIndex = 3;
            _dgvPazienteak.Visible = false;
            // 
            // _btnInportatu
            // 
            _btnInportatu.BackColor = Color.FromArgb(46, 204, 113);
            _btnInportatu.FlatStyle = FlatStyle.Flat;
            _btnInportatu.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            _btnInportatu.ForeColor = Color.White;
            _btnInportatu.Location = new Point(394, 780);
            _btnInportatu.Name = "_btnInportatu";
            _btnInportatu.Size = new Size(400, 80);
            _btnInportatu.TabIndex = 4;
            _btnInportatu.Text = "Datuak inportatu";
            _btnInportatu.UseVisualStyleBackColor = false;
            _btnInportatu.Visible = false;
            // 
            // _btnUtzi
            // 
            _btnUtzi.BackColor = Color.FromArgb(231, 76, 60);
            _btnUtzi.FlatStyle = FlatStyle.Flat;
            _btnUtzi.Font = new Font("Segoe UI", 12F);
            _btnUtzi.ForeColor = Color.White;
            _btnUtzi.Location = new Point(494, 875);
            _btnUtzi.Name = "_btnUtzi";
            _btnUtzi.Size = new Size(200, 50);
            _btnUtzi.TabIndex = 5;
            _btnUtzi.Text = "Utzi";
            _btnUtzi.UseVisualStyleBackColor = false;
            // 
            // _timerKonexioa
            // 
            _timerKonexioa.Interval = 1000;
            // 
            // NeurketaMotakFormularioa
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1213);
            Name = "NeurketaMotakFormularioa";
            Text = "GOsasun - Neurketa Motak";
            _edukiPanela.ResumeLayout(false);
            _pnlImport.ResumeLayout(false);
            _pnlImport.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GOsasun_app.Kontrolak.CustomCardButton btnTentsiometroa;
        private GOsasun_app.Kontrolak.CustomCardButton btnGlukometroa;
        private GOsasun_app.Kontrolak.CustomCardButton btnPisua;
        private GOsasun_app.Kontrolak.CustomCardButton btnAltuera;
        private Panel _pnlImport;
        private Label _lblStatus;
        private Label _lblBilatu;
        private TextBox _txtPazienteBilatu;
        private DataGridView _dgvPazienteak;
        private Button _btnInportatu;
        private Button _btnUtzi;
        private System.Windows.Forms.Timer _timerKonexioa;
    }
}
