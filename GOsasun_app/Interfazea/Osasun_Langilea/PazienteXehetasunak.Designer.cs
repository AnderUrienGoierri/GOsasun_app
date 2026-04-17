namespace GOsasun_app.Interfazea
{
    partial class PazienteXehetasunak
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
            pnlGoiburua = new Panel();
            lblFitxaMota = new Label();
            lblIzena = new Label();
            lblAzpiInformazioa = new Label();
            lblEgoeraBadge = new Label();
            btnEgoeraMedikoaAldatu = new Button();
            pnlArgazkia = new Panel();
            pbIrudia = new PictureBox();
            lblArgazkiAzalpena = new Label();
            pnlIdentifikazioa = new Panel();
            lblIdentifikazioa = new Label();
            lblNanTitulua = new Label();
            lblNanBalioa = new Label();
            lblJaiotzeDataTitulua = new Label();
            lblJaiotzeDataBalioa = new Label();
            lblAdinaTitulua = new Label();
            lblAdinaBalioa = new Label();
            lblSexuaTitulua = new Label();
            lblSexuaBalioa = new Label();
            pnlHarremana = new Panel();
            lblHarremana = new Label();
            lblEmailaTitulua = new Label();
            lblEmailaBalioa = new Label();
            lblTelefonoaTitulua = new Label();
            lblTelefonoaBalioa = new Label();
            lblHelbideaTitulua = new Label();
            lblHelbideaBalioa = new Label();
            lblHerriaTitulua = new Label();
            lblHerriaBalioa = new Label();
            pnlKlinikoa = new Panel();
            lblKlinikoa = new Label();
            lblOdolTaldeaTitulua = new Label();
            lblOdolTaldeaBalioa = new Label();
            lblAltueraTitulua = new Label();
            lblAltueraBalioa = new Label();
            lblPisuaTitulua = new Label();
            lblPisuaBalioa = new Label();
            lblEgoeraTitulua = new Label();
            lblEgoeraBalioa = new Label();
            _edukiPanela.SuspendLayout();
            pnlGoiburua.SuspendLayout();
            pnlArgazkia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbIrudia).BeginInit();
            pnlIdentifikazioa.SuspendLayout();
            pnlHarremana.SuspendLayout();
            pnlKlinikoa.SuspendLayout();
            SuspendLayout();
            // 
            // _edukiPanela
            // 
            _edukiPanela.Controls.Add(pnlKlinikoa);
            _edukiPanela.Controls.Add(pnlHarremana);
            _edukiPanela.Controls.Add(pnlIdentifikazioa);
            _edukiPanela.Controls.Add(pnlArgazkia);
            _edukiPanela.Controls.Add(pnlGoiburua);
            // 
            // _atzeraBotoia
            // 
            _atzeraBotoia.FlatAppearance.BorderSize = 0;
            // 
            // pnlGoiburua
            // 
            pnlGoiburua.BackColor = Color.FromArgb(246, 250, 252);
            pnlGoiburua.BorderStyle = BorderStyle.FixedSingle;
            pnlGoiburua.Controls.Add(lblFitxaMota);
            pnlGoiburua.Controls.Add(lblIzena);
            pnlGoiburua.Controls.Add(lblAzpiInformazioa);
            pnlGoiburua.Controls.Add(lblEgoeraBadge);
            pnlGoiburua.Controls.Add(btnEgoeraMedikoaAldatu);
            pnlGoiburua.Location = new Point(12, 44);
            pnlGoiburua.Name = "pnlGoiburua";
            pnlGoiburua.Size = new Size(1878, 236);
            pnlGoiburua.TabIndex = 0;
            // 
            // lblFitxaMota
            // 
            lblFitxaMota.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFitxaMota.ForeColor = Color.FromArgb(78, 105, 130);
            lblFitxaMota.Location = new Point(28, 18);
            lblFitxaMota.Name = "lblFitxaMota";
            lblFitxaMota.Size = new Size(364, 42);
            lblFitxaMota.TabIndex = 0;
            lblFitxaMota.Text = "PAZIENTEAREN FITXA MEDIKOA";
            // 
            // lblIzena
            // 
            lblIzena.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblIzena.ForeColor = Color.FromArgb(30, 49, 69);
            lblIzena.Location = new Point(15, 67);
            lblIzena.Name = "lblIzena";
            lblIzena.Size = new Size(900, 82);
            lblIzena.TabIndex = 1;
            lblIzena.Text = "Pazientearen izen-abizenak";
            // 
            // lblAzpiInformazioa
            // 
            lblAzpiInformazioa.Font = new Font("Segoe UI", 10.5F);
            lblAzpiInformazioa.ForeColor = Color.FromArgb(86, 103, 121);
            lblAzpiInformazioa.Location = new Point(28, 159);
            lblAzpiInformazioa.Name = "lblAzpiInformazioa";
            lblAzpiInformazioa.Size = new Size(832, 64);
            lblAzpiInformazioa.TabIndex = 2;
            lblAzpiInformazioa.Text = "NAN: --- | Paziente ID: ---";
            // 
            // lblEgoeraBadge
            // 
            lblEgoeraBadge.BackColor = Color.FromArgb(223, 245, 232);
            lblEgoeraBadge.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblEgoeraBadge.ForeColor = Color.FromArgb(32, 102, 70);
            lblEgoeraBadge.Location = new Point(1522, 15);
            lblEgoeraBadge.Name = "lblEgoeraBadge";
            lblEgoeraBadge.Size = new Size(194, 42);
            lblEgoeraBadge.TabIndex = 3;
            lblEgoeraBadge.Text = "ALTA";
            lblEgoeraBadge.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnEgoeraMedikoaAldatu
            // 
            btnEgoeraMedikoaAldatu.BackColor = Color.FromArgb(41, 128, 185);
            btnEgoeraMedikoaAldatu.Cursor = Cursors.Hand;
            btnEgoeraMedikoaAldatu.FlatAppearance.BorderSize = 0;
            btnEgoeraMedikoaAldatu.FlatStyle = FlatStyle.Flat;
            btnEgoeraMedikoaAldatu.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnEgoeraMedikoaAldatu.ForeColor = Color.White;
            btnEgoeraMedikoaAldatu.Location = new Point(1490, 82);
            btnEgoeraMedikoaAldatu.Name = "btnEgoeraMedikoaAldatu";
            btnEgoeraMedikoaAldatu.Size = new Size(358, 52);
            btnEgoeraMedikoaAldatu.TabIndex = 4;
            btnEgoeraMedikoaAldatu.Text = "EGOERA MEDIKOA ALDATU";
            btnEgoeraMedikoaAldatu.UseVisualStyleBackColor = false;
            // 
            // pnlArgazkia
            // 
            pnlArgazkia.BackColor = Color.White;
            pnlArgazkia.BorderStyle = BorderStyle.FixedSingle;
            pnlArgazkia.Controls.Add(pbIrudia);
            pnlArgazkia.Controls.Add(lblArgazkiAzalpena);
            pnlArgazkia.Location = new Point(12, 288);
            pnlArgazkia.Name = "pnlArgazkia";
            pnlArgazkia.Size = new Size(389, 532);
            pnlArgazkia.TabIndex = 1;
            // 
            // pbIrudia
            // 
            pbIrudia.BackColor = Color.FromArgb(241, 246, 250);
            pbIrudia.Location = new Point(24, 26);
            pbIrudia.Name = "pbIrudia";
            pbIrudia.Size = new Size(242, 320);
            pbIrudia.SizeMode = PictureBoxSizeMode.Zoom;
            pbIrudia.TabIndex = 0;
            pbIrudia.TabStop = false;
            // 
            // lblArgazkiAzalpena
            // 
            lblArgazkiAzalpena.Font = new Font("Segoe UI", 10F);
            lblArgazkiAzalpena.ForeColor = Color.FromArgb(97, 113, 130);
            lblArgazkiAzalpena.Location = new Point(24, 366);
            lblArgazkiAzalpena.Name = "lblArgazkiAzalpena";
            lblArgazkiAzalpena.Size = new Size(242, 104);
            lblArgazkiAzalpena.TabIndex = 1;
            lblArgazkiAzalpena.Text = "Argazkia edo identifikazio bisuala. Daturik ez badago, irudi lehenetsi klinikoa erakusten da.";
            // 
            // pnlIdentifikazioa
            // 
            pnlIdentifikazioa.BackColor = Color.White;
            pnlIdentifikazioa.BorderStyle = BorderStyle.FixedSingle;
            pnlIdentifikazioa.Controls.Add(lblIdentifikazioa);
            pnlIdentifikazioa.Controls.Add(lblNanTitulua);
            pnlIdentifikazioa.Controls.Add(lblNanBalioa);
            pnlIdentifikazioa.Controls.Add(lblJaiotzeDataTitulua);
            pnlIdentifikazioa.Controls.Add(lblJaiotzeDataBalioa);
            pnlIdentifikazioa.Controls.Add(lblAdinaTitulua);
            pnlIdentifikazioa.Controls.Add(lblAdinaBalioa);
            pnlIdentifikazioa.Controls.Add(lblSexuaTitulua);
            pnlIdentifikazioa.Controls.Add(lblSexuaBalioa);
            pnlIdentifikazioa.Location = new Point(407, 286);
            pnlIdentifikazioa.Name = "pnlIdentifikazioa";
            pnlIdentifikazioa.Size = new Size(601, 453);
            pnlIdentifikazioa.TabIndex = 2;
            // 
            // lblIdentifikazioa
            // 
            lblIdentifikazioa.Location = new Point(0, 0);
            lblIdentifikazioa.Name = "lblIdentifikazioa";
            lblIdentifikazioa.Size = new Size(100, 23);
            lblIdentifikazioa.TabIndex = 0;
            // 
            // lblNanTitulua
            // 
            lblNanTitulua.Location = new Point(0, 0);
            lblNanTitulua.Name = "lblNanTitulua";
            lblNanTitulua.Size = new Size(100, 23);
            lblNanTitulua.TabIndex = 1;
            // 
            // lblNanBalioa
            // 
            lblNanBalioa.Location = new Point(0, 0);
            lblNanBalioa.Name = "lblNanBalioa";
            lblNanBalioa.Size = new Size(100, 23);
            lblNanBalioa.TabIndex = 2;
            // 
            // lblJaiotzeDataTitulua
            // 
            lblJaiotzeDataTitulua.Location = new Point(0, 0);
            lblJaiotzeDataTitulua.Name = "lblJaiotzeDataTitulua";
            lblJaiotzeDataTitulua.Size = new Size(100, 23);
            lblJaiotzeDataTitulua.TabIndex = 3;
            // 
            // lblJaiotzeDataBalioa
            // 
            lblJaiotzeDataBalioa.Location = new Point(0, 0);
            lblJaiotzeDataBalioa.Name = "lblJaiotzeDataBalioa";
            lblJaiotzeDataBalioa.Size = new Size(100, 23);
            lblJaiotzeDataBalioa.TabIndex = 4;
            // 
            // lblAdinaTitulua
            // 
            lblAdinaTitulua.Location = new Point(0, 0);
            lblAdinaTitulua.Name = "lblAdinaTitulua";
            lblAdinaTitulua.Size = new Size(100, 23);
            lblAdinaTitulua.TabIndex = 5;
            // 
            // lblAdinaBalioa
            // 
            lblAdinaBalioa.Location = new Point(0, 0);
            lblAdinaBalioa.Name = "lblAdinaBalioa";
            lblAdinaBalioa.Size = new Size(100, 23);
            lblAdinaBalioa.TabIndex = 6;
            // 
            // lblSexuaTitulua
            // 
            lblSexuaTitulua.Location = new Point(0, 0);
            lblSexuaTitulua.Name = "lblSexuaTitulua";
            lblSexuaTitulua.Size = new Size(100, 23);
            lblSexuaTitulua.TabIndex = 7;
            // 
            // lblSexuaBalioa
            // 
            lblSexuaBalioa.Location = new Point(0, 0);
            lblSexuaBalioa.Name = "lblSexuaBalioa";
            lblSexuaBalioa.Size = new Size(100, 23);
            lblSexuaBalioa.TabIndex = 8;
            // 
            // pnlHarremana
            // 
            pnlHarremana.BackColor = Color.White;
            pnlHarremana.BorderStyle = BorderStyle.FixedSingle;
            pnlHarremana.Controls.Add(lblHarremana);
            pnlHarremana.Controls.Add(lblEmailaTitulua);
            pnlHarremana.Controls.Add(lblEmailaBalioa);
            pnlHarremana.Controls.Add(lblTelefonoaTitulua);
            pnlHarremana.Controls.Add(lblTelefonoaBalioa);
            pnlHarremana.Controls.Add(lblHelbideaTitulua);
            pnlHarremana.Controls.Add(lblHelbideaBalioa);
            pnlHarremana.Controls.Add(lblHerriaTitulua);
            pnlHarremana.Controls.Add(lblHerriaBalioa);
            pnlHarremana.Location = new Point(1014, 287);
            pnlHarremana.Name = "pnlHarremana";
            pnlHarremana.Size = new Size(876, 452);
            pnlHarremana.TabIndex = 3;
            // 
            // lblHarremana
            // 
            lblHarremana.Location = new Point(0, 0);
            lblHarremana.Name = "lblHarremana";
            lblHarremana.Size = new Size(100, 23);
            lblHarremana.TabIndex = 0;
            // 
            // lblEmailaTitulua
            // 
            lblEmailaTitulua.Location = new Point(0, 0);
            lblEmailaTitulua.Name = "lblEmailaTitulua";
            lblEmailaTitulua.Size = new Size(100, 23);
            lblEmailaTitulua.TabIndex = 1;
            // 
            // lblEmailaBalioa
            // 
            lblEmailaBalioa.Location = new Point(0, 0);
            lblEmailaBalioa.Name = "lblEmailaBalioa";
            lblEmailaBalioa.Size = new Size(100, 23);
            lblEmailaBalioa.TabIndex = 2;
            // 
            // lblTelefonoaTitulua
            // 
            lblTelefonoaTitulua.Location = new Point(0, 0);
            lblTelefonoaTitulua.Name = "lblTelefonoaTitulua";
            lblTelefonoaTitulua.Size = new Size(100, 23);
            lblTelefonoaTitulua.TabIndex = 3;
            // 
            // lblTelefonoaBalioa
            // 
            lblTelefonoaBalioa.Location = new Point(0, 0);
            lblTelefonoaBalioa.Name = "lblTelefonoaBalioa";
            lblTelefonoaBalioa.Size = new Size(100, 23);
            lblTelefonoaBalioa.TabIndex = 4;
            // 
            // lblHelbideaTitulua
            // 
            lblHelbideaTitulua.Location = new Point(0, 0);
            lblHelbideaTitulua.Name = "lblHelbideaTitulua";
            lblHelbideaTitulua.Size = new Size(100, 23);
            lblHelbideaTitulua.TabIndex = 5;
            // 
            // lblHelbideaBalioa
            // 
            lblHelbideaBalioa.Location = new Point(0, 0);
            lblHelbideaBalioa.Name = "lblHelbideaBalioa";
            lblHelbideaBalioa.Size = new Size(100, 23);
            lblHelbideaBalioa.TabIndex = 6;
            // 
            // lblHerriaTitulua
            // 
            lblHerriaTitulua.Location = new Point(0, 0);
            lblHerriaTitulua.Name = "lblHerriaTitulua";
            lblHerriaTitulua.Size = new Size(100, 23);
            lblHerriaTitulua.TabIndex = 7;
            // 
            // lblHerriaBalioa
            // 
            lblHerriaBalioa.Location = new Point(0, 0);
            lblHerriaBalioa.Name = "lblHerriaBalioa";
            lblHerriaBalioa.Size = new Size(100, 23);
            lblHerriaBalioa.TabIndex = 8;
            // 
            // pnlKlinikoa
            // 
            pnlKlinikoa.BackColor = Color.White;
            pnlKlinikoa.BorderStyle = BorderStyle.FixedSingle;
            pnlKlinikoa.Controls.Add(lblKlinikoa);
            pnlKlinikoa.Controls.Add(lblOdolTaldeaTitulua);
            pnlKlinikoa.Controls.Add(lblOdolTaldeaBalioa);
            pnlKlinikoa.Controls.Add(lblAltueraTitulua);
            pnlKlinikoa.Controls.Add(lblAltueraBalioa);
            pnlKlinikoa.Controls.Add(lblPisuaTitulua);
            pnlKlinikoa.Controls.Add(lblPisuaBalioa);
            pnlKlinikoa.Controls.Add(lblEgoeraTitulua);
            pnlKlinikoa.Controls.Add(lblEgoeraBalioa);
            pnlKlinikoa.Location = new Point(406, 745);
            pnlKlinikoa.Name = "pnlKlinikoa";
            pnlKlinikoa.Size = new Size(1484, 600);
            pnlKlinikoa.TabIndex = 4;
            pnlKlinikoa.Paint += pnlKlinikoa_Paint;
            // 
            // lblKlinikoa
            // 
            lblKlinikoa.Location = new Point(0, 0);
            lblKlinikoa.Name = "lblKlinikoa";
            lblKlinikoa.Size = new Size(100, 23);
            lblKlinikoa.TabIndex = 0;
            // 
            // lblOdolTaldeaTitulua
            // 
            lblOdolTaldeaTitulua.Location = new Point(0, 0);
            lblOdolTaldeaTitulua.Name = "lblOdolTaldeaTitulua";
            lblOdolTaldeaTitulua.Size = new Size(100, 23);
            lblOdolTaldeaTitulua.TabIndex = 1;
            // 
            // lblOdolTaldeaBalioa
            // 
            lblOdolTaldeaBalioa.Location = new Point(0, 0);
            lblOdolTaldeaBalioa.Name = "lblOdolTaldeaBalioa";
            lblOdolTaldeaBalioa.Size = new Size(100, 23);
            lblOdolTaldeaBalioa.TabIndex = 2;
            // 
            // lblAltueraTitulua
            // 
            lblAltueraTitulua.Location = new Point(0, 0);
            lblAltueraTitulua.Name = "lblAltueraTitulua";
            lblAltueraTitulua.Size = new Size(100, 23);
            lblAltueraTitulua.TabIndex = 3;
            // 
            // lblAltueraBalioa
            // 
            lblAltueraBalioa.Location = new Point(0, 0);
            lblAltueraBalioa.Name = "lblAltueraBalioa";
            lblAltueraBalioa.Size = new Size(100, 23);
            lblAltueraBalioa.TabIndex = 4;
            // 
            // lblPisuaTitulua
            // 
            lblPisuaTitulua.Location = new Point(0, 0);
            lblPisuaTitulua.Name = "lblPisuaTitulua";
            lblPisuaTitulua.Size = new Size(100, 23);
            lblPisuaTitulua.TabIndex = 5;
            // 
            // lblPisuaBalioa
            // 
            lblPisuaBalioa.Location = new Point(0, 0);
            lblPisuaBalioa.Name = "lblPisuaBalioa";
            lblPisuaBalioa.Size = new Size(100, 23);
            lblPisuaBalioa.TabIndex = 6;
            // 
            // lblEgoeraTitulua
            // 
            lblEgoeraTitulua.Location = new Point(0, 0);
            lblEgoeraTitulua.Name = "lblEgoeraTitulua";
            lblEgoeraTitulua.Size = new Size(100, 23);
            lblEgoeraTitulua.TabIndex = 7;
            // 
            // lblEgoeraBalioa
            // 
            lblEgoeraBalioa.Location = new Point(0, 0);
            lblEgoeraBalioa.Name = "lblEgoeraBalioa";
            lblEgoeraBalioa.Size = new Size(100, 23);
            lblEgoeraBalioa.TabIndex = 8;
            // 
            // PazienteXehetasunak
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1902, 1575);
            Name = "PazienteXehetasunak";
            Text = "GOsasun - Pazientearen Xehetasunak";
            _edukiPanela.ResumeLayout(false);
            pnlGoiburua.ResumeLayout(false);
            pnlArgazkia.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbIrudia).EndInit();
            pnlIdentifikazioa.ResumeLayout(false);
            pnlHarremana.ResumeLayout(false);
            pnlKlinikoa.ResumeLayout(false);
            ResumeLayout(false);
        }

        private static void KonfiguratuSectionTitle(Label label, string testua, int x)
        {
            label.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label.ForeColor = System.Drawing.Color.FromArgb(77, 102, 126);
            label.Location = new System.Drawing.Point(x, 22);
            label.Name = label.Name;
            label.Size = new System.Drawing.Size(360, 30);
            label.Text = testua;
        }

        private static void KonfiguratuField(Label label, string testua, int x, int y)
        {
            label.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            label.ForeColor = System.Drawing.Color.FromArgb(112, 127, 143);
            label.Location = new System.Drawing.Point(x, y);
            label.Size = new System.Drawing.Size(260, 28);
            label.Text = testua;
        }

        private static void KonfiguratuValue(Label label, string testua, int x, int y, int zabalera)
        {
            label.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label.ForeColor = System.Drawing.Color.FromArgb(30, 49, 69);
            label.Location = new System.Drawing.Point(x, y);
            label.Size = new System.Drawing.Size(zabalera, 52);
            label.Text = testua;
        }

        private Panel pnlGoiburua;
        private Label lblFitxaMota;
        private Label lblIzena;
        private Label lblAzpiInformazioa;
        private Label lblEgoeraBadge;
        private Button btnEgoeraMedikoaAldatu;
        private Panel pnlArgazkia;
        private PictureBox pbIrudia;
        private Label lblArgazkiAzalpena;
        private Panel pnlIdentifikazioa;
        private Label lblIdentifikazioa;
        private Label lblNanTitulua;
        private Label lblNanBalioa;
        private Label lblJaiotzeDataTitulua;
        private Label lblJaiotzeDataBalioa;
        private Label lblAdinaTitulua;
        private Label lblAdinaBalioa;
        private Label lblSexuaTitulua;
        private Label lblSexuaBalioa;
        private Panel pnlHarremana;
        private Label lblHarremana;
        private Label lblEmailaTitulua;
        private Label lblEmailaBalioa;
        private Label lblTelefonoaTitulua;
        private Label lblTelefonoaBalioa;
        private Label lblHelbideaTitulua;
        private Label lblHelbideaBalioa;
        private Label lblHerriaTitulua;
        private Label lblHerriaBalioa;
        private Panel pnlKlinikoa;
        private Label lblKlinikoa;
        private Label lblOdolTaldeaTitulua;
        private Label lblOdolTaldeaBalioa;
        private Label lblAltueraTitulua;
        private Label lblAltueraBalioa;
        private Label lblPisuaTitulua;
        private Label lblPisuaBalioa;
        private Label lblEgoeraTitulua;
        private Label lblEgoeraBalioa;
    }
}

