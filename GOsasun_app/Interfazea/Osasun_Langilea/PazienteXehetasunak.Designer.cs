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
            _edukiPanela.Size = new System.Drawing.Size(1500, 799);
            // 
            // _goiburuBarra
            // 
            _goiburuBarra.Size = new System.Drawing.Size(1500, 181);
            // 
            // pnlGoiburua
            // 
            pnlGoiburua.BackColor = System.Drawing.Color.FromArgb(246, 250, 252);
            pnlGoiburua.BorderStyle = BorderStyle.FixedSingle;
            pnlGoiburua.Controls.Add(lblFitxaMota);
            pnlGoiburua.Controls.Add(lblIzena);
            pnlGoiburua.Controls.Add(lblAzpiInformazioa);
            pnlGoiburua.Controls.Add(lblEgoeraBadge);
            pnlGoiburua.Location = new System.Drawing.Point(72, 44);
            pnlGoiburua.Name = "pnlGoiburua";
            pnlGoiburua.Size = new System.Drawing.Size(1356, 128);
            pnlGoiburua.TabIndex = 0;
            // 
            // lblFitxaMota
            // 
            lblFitxaMota.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFitxaMota.ForeColor = System.Drawing.Color.FromArgb(78, 105, 130);
            lblFitxaMota.Location = new System.Drawing.Point(28, 18);
            lblFitxaMota.Name = "lblFitxaMota";
            lblFitxaMota.Size = new System.Drawing.Size(364, 24);
            lblFitxaMota.TabIndex = 0;
            lblFitxaMota.Text = "PAZIENTEAREN FITXA MEDIKOA";
            // 
            // lblIzena
            // 
            lblIzena.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblIzena.ForeColor = System.Drawing.Color.FromArgb(30, 49, 69);
            lblIzena.Location = new System.Drawing.Point(24, 42);
            lblIzena.Name = "lblIzena";
            lblIzena.Size = new System.Drawing.Size(900, 48);
            lblIzena.TabIndex = 1;
            lblIzena.Text = "Pazientearen izen-abizenak";
            // 
            // lblAzpiInformazioa
            // 
            lblAzpiInformazioa.Font = new Font("Segoe UI", 10.5F);
            lblAzpiInformazioa.ForeColor = System.Drawing.Color.FromArgb(86, 103, 121);
            lblAzpiInformazioa.Location = new System.Drawing.Point(29, 90);
            lblAzpiInformazioa.Name = "lblAzpiInformazioa";
            lblAzpiInformazioa.Size = new System.Drawing.Size(740, 24);
            lblAzpiInformazioa.TabIndex = 2;
            lblAzpiInformazioa.Text = "NAN: --- | Paziente ID: ---";
            // 
            // lblEgoeraBadge
            // 
            lblEgoeraBadge.BackColor = System.Drawing.Color.FromArgb(223, 245, 232);
            lblEgoeraBadge.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblEgoeraBadge.ForeColor = System.Drawing.Color.FromArgb(32, 102, 70);
            lblEgoeraBadge.Location = new System.Drawing.Point(1114, 42);
            lblEgoeraBadge.Name = "lblEgoeraBadge";
            lblEgoeraBadge.Size = new System.Drawing.Size(194, 42);
            lblEgoeraBadge.TabIndex = 3;
            lblEgoeraBadge.Text = "ALTA";
            lblEgoeraBadge.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlArgazkia
            // 
            pnlArgazkia.BackColor = System.Drawing.Color.White;
            pnlArgazkia.BorderStyle = BorderStyle.FixedSingle;
            pnlArgazkia.Controls.Add(pbIrudia);
            pnlArgazkia.Controls.Add(lblArgazkiAzalpena);
            pnlArgazkia.Location = new System.Drawing.Point(72, 204);
            pnlArgazkia.Name = "pnlArgazkia";
            pnlArgazkia.Size = new System.Drawing.Size(292, 510);
            pnlArgazkia.TabIndex = 1;
            // 
            // pbIrudia
            // 
            pbIrudia.BackColor = System.Drawing.Color.FromArgb(241, 246, 250);
            pbIrudia.Location = new System.Drawing.Point(24, 26);
            pbIrudia.Name = "pbIrudia";
            pbIrudia.Size = new System.Drawing.Size(242, 320);
            pbIrudia.SizeMode = PictureBoxSizeMode.Zoom;
            pbIrudia.TabIndex = 0;
            pbIrudia.TabStop = false;
            // 
            // lblArgazkiAzalpena
            // 
            lblArgazkiAzalpena.Font = new Font("Segoe UI", 10F);
            lblArgazkiAzalpena.ForeColor = System.Drawing.Color.FromArgb(97, 113, 130);
            lblArgazkiAzalpena.Location = new System.Drawing.Point(24, 366);
            lblArgazkiAzalpena.Name = "lblArgazkiAzalpena";
            lblArgazkiAzalpena.Size = new System.Drawing.Size(242, 104);
            lblArgazkiAzalpena.TabIndex = 1;
            lblArgazkiAzalpena.Text = "Argazkia edo identifikazio bisuala. Daturik ez badago, irudi lehenetsi klinikoa erakusten da.";
            // 
            // pnlIdentifikazioa
            // 
            pnlIdentifikazioa.BackColor = System.Drawing.Color.White;
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
            pnlIdentifikazioa.Location = new System.Drawing.Point(392, 204);
            pnlIdentifikazioa.Name = "pnlIdentifikazioa";
            pnlIdentifikazioa.Size = new System.Drawing.Size(496, 246);
            pnlIdentifikazioa.TabIndex = 2;
            // 
            // pnlHarremana
            // 
            pnlHarremana.BackColor = System.Drawing.Color.White;
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
            pnlHarremana.Location = new System.Drawing.Point(932, 204);
            pnlHarremana.Name = "pnlHarremana";
            pnlHarremana.Size = new System.Drawing.Size(496, 246);
            pnlHarremana.TabIndex = 3;
            // 
            // pnlKlinikoa
            // 
            pnlKlinikoa.BackColor = System.Drawing.Color.White;
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
            pnlKlinikoa.Location = new System.Drawing.Point(392, 468);
            pnlKlinikoa.Name = "pnlKlinikoa";
            pnlKlinikoa.Size = new System.Drawing.Size(1036, 246);
            pnlKlinikoa.TabIndex = 4;
            // 
            // section labels and values
            // 
            KonfiguratuSectionTitle(lblIdentifikazioa, "IDENTIFIKAZIOA", 22);
            KonfiguratuField(lblNanTitulua, "NAN", 30, 72);
            KonfiguratuValue(lblNanBalioa, "---", 30, 100, 190);
            KonfiguratuField(lblJaiotzeDataTitulua, "Jaiotze data", 260, 72);
            KonfiguratuValue(lblJaiotzeDataBalioa, "---", 260, 100, 190);
            KonfiguratuField(lblAdinaTitulua, "Adina", 30, 148);
            KonfiguratuValue(lblAdinaBalioa, "---", 30, 176, 190);
            KonfiguratuField(lblSexuaTitulua, "Sexua", 260, 148);
            KonfiguratuValue(lblSexuaBalioa, "---", 260, 176, 190);
            KonfiguratuSectionTitle(lblHarremana, "HARREMAN ETA KOKAPENA", 22);
            KonfiguratuField(lblEmailaTitulua, "Emaila", 30, 72);
            KonfiguratuValue(lblEmailaBalioa, "---", 30, 100, 420);
            KonfiguratuField(lblTelefonoaTitulua, "Telefonoa", 30, 148);
            KonfiguratuValue(lblTelefonoaBalioa, "---", 30, 176, 190);
            KonfiguratuField(lblHelbideaTitulua, "Helbidea", 260, 148);
            KonfiguratuValue(lblHelbideaBalioa, "---", 260, 176, 190);
            KonfiguratuField(lblHerriaTitulua, "Herria / PK", 30, 204);
            KonfiguratuValue(lblHerriaBalioa, "---", 30, 228, 420);
            KonfiguratuSectionTitle(lblKlinikoa, "LABURPEN KLINIKOA", 22);
            KonfiguratuField(lblOdolTaldeaTitulua, "Odol taldea", 30, 76);
            KonfiguratuValue(lblOdolTaldeaBalioa, "---", 30, 108, 190);
            KonfiguratuField(lblAltueraTitulua, "Azken altuera", 292, 76);
            KonfiguratuValue(lblAltueraBalioa, "---", 292, 108, 190);
            KonfiguratuField(lblPisuaTitulua, "Azken pisua", 554, 76);
            KonfiguratuValue(lblPisuaBalioa, "---", 554, 108, 190);
            KonfiguratuField(lblEgoeraTitulua, "Egoera klinikoa", 816, 76);
            KonfiguratuValue(lblEgoeraBalioa, "---", 816, 108, 190);
            // 
            // PazienteXehetasunak
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1500, 980);
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
            label.Location = new System.Drawing.Point(x, 20);
            label.Name = label.Name;
            label.Size = new System.Drawing.Size(340, 24);
            label.Text = testua;
        }

        private static void KonfiguratuField(Label label, string testua, int x, int y)
        {
            label.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label.ForeColor = System.Drawing.Color.FromArgb(112, 127, 143);
            label.Location = new System.Drawing.Point(x, y);
            label.Size = new System.Drawing.Size(170, 22);
            label.Text = testua;
        }

        private static void KonfiguratuValue(Label label, string testua, int x, int y, int zabalera)
        {
            label.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label.ForeColor = System.Drawing.Color.FromArgb(30, 49, 69);
            label.Location = new System.Drawing.Point(x, y);
            label.Size = new System.Drawing.Size(zabalera, 32);
            label.Text = testua;
        }

        private Panel pnlGoiburua;
        private Label lblFitxaMota;
        private Label lblIzena;
        private Label lblAzpiInformazioa;
        private Label lblEgoeraBadge;
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
