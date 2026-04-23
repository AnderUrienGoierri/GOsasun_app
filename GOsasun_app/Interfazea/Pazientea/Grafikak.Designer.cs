namespace GOsasun_app.Interfazea
{
    partial class Grafikak
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
            lblIzenburua = new Label();
            pnlIragazkiak = new Panel();
            lblPazienteBilatu = new Label();
            txtPazienteBilatu = new TextBox();
            lblPazientea = new Label();
            cmbPazienteak = new ComboBox();
            chkPazienteGuztiak = new CheckBox();
            lblGrafikaMota = new Label();
            cmbGrafikaMota = new ComboBox();
            lblHasiera = new Label();
            dtpHasiera = new DateTimePicker();
            lblAmaiera = new Label();
            dtpAmaiera = new DateTimePicker();
            btnGrafikoaErakutsi = new Button();
            lblEgoera = new Label();
            lblPazienteDatuak = new Label();
            lblAzalpena = new Label();
            pnlGrafikoa = new Panel();
            _edukiPanela.SuspendLayout();
            pnlIragazkiak.SuspendLayout();
            SuspendLayout();
            // 
            // _edukiPanela
            // 
            _edukiPanela.Controls.Add(lblPazienteDatuak);
            _edukiPanela.Controls.Add(lblEgoera);
            _edukiPanela.Controls.Add(pnlGrafikoa);
            _edukiPanela.Controls.Add(lblAzalpena);
            _edukiPanela.Controls.Add(pnlIragazkiak);
            _edukiPanela.Controls.Add(lblIzenburua);
            _edukiPanela.Location = new Point(0, 186);
            // 
            // _goiburuBarra
            // 
            _goiburuBarra.Size = new Size(1902, 186);
            // 
            // _atzeraBotoia
            // 
            _atzeraBotoia.FlatAppearance.BorderSize = 0;
            // 
            // lblIzenburua
            // 
            lblIzenburua.Dock = DockStyle.Top;
            lblIzenburua.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIzenburua.ForeColor = Color.White;
            lblIzenburua.Location = new Point(2, 2);
            lblIzenburua.Name = "lblIzenburua";
            lblIzenburua.Size = new Size(1898, 93);
            lblIzenburua.TabIndex = 0;
            lblIzenburua.Text = "OSASUN DATUEN GRAFIKAK";
            lblIzenburua.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlIragazkiak
            // 
            pnlIragazkiak.BackColor = Color.FromArgb(245, 247, 250);
            pnlIragazkiak.BorderStyle = BorderStyle.FixedSingle;
            pnlIragazkiak.Controls.Add(lblPazienteBilatu);
            pnlIragazkiak.Controls.Add(txtPazienteBilatu);
            pnlIragazkiak.Controls.Add(lblPazientea);
            pnlIragazkiak.Controls.Add(cmbPazienteak);
            pnlIragazkiak.Controls.Add(chkPazienteGuztiak);
            pnlIragazkiak.Controls.Add(lblGrafikaMota);
            pnlIragazkiak.Controls.Add(cmbGrafikaMota);
            pnlIragazkiak.Controls.Add(lblHasiera);
            pnlIragazkiak.Controls.Add(dtpHasiera);
            pnlIragazkiak.Controls.Add(lblAmaiera);
            pnlIragazkiak.Controls.Add(dtpAmaiera);
            pnlIragazkiak.Controls.Add(btnGrafikoaErakutsi);
            pnlIragazkiak.Location = new Point(0, 94);
            pnlIragazkiak.Name = "pnlIragazkiak";
            pnlIragazkiak.Size = new Size(1901, 134);
            pnlIragazkiak.TabIndex = 1;
            // 
            // lblPazienteBilatu
            // 
            lblPazienteBilatu.AutoSize = true;
            lblPazienteBilatu.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblPazienteBilatu.ForeColor = Color.FromArgb(44, 62, 80);
            lblPazienteBilatu.Location = new Point(10, -3);
            lblPazienteBilatu.Name = "lblPazienteBilatu";
            lblPazienteBilatu.Size = new Size(245, 41);
            lblPazienteBilatu.TabIndex = 0;
            lblPazienteBilatu.Text = "Bilatu pazientea";
            // 
            // txtPazienteBilatu
            // 
            txtPazienteBilatu.Font = new Font("Segoe UI", 11F);
            txtPazienteBilatu.Location = new Point(10, 40);
            txtPazienteBilatu.Name = "txtPazienteBilatu";
            txtPazienteBilatu.PlaceholderText = "Abizena, izena edo NAN";
            txtPazienteBilatu.Size = new Size(346, 47);
            txtPazienteBilatu.TabIndex = 1;
            // 
            // lblPazientea
            // 
            lblPazientea.AutoSize = true;
            lblPazientea.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblPazientea.ForeColor = Color.FromArgb(44, 62, 80);
            lblPazientea.Location = new Point(366, -1);
            lblPazientea.Name = "lblPazientea";
            lblPazientea.Size = new Size(152, 41);
            lblPazientea.TabIndex = 2;
            lblPazientea.Text = "Pazientea";
            // 
            // cmbPazienteak
            // 
            cmbPazienteak.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPazienteak.Font = new Font("Segoe UI", 11F);
            cmbPazienteak.FormattingEnabled = true;
            cmbPazienteak.Location = new Point(362, 40);
            cmbPazienteak.Name = "cmbPazienteak";
            cmbPazienteak.Size = new Size(292, 48);
            cmbPazienteak.TabIndex = 3;
            // 
            // chkPazienteGuztiak
            // 
            chkPazienteGuztiak.AutoSize = true;
            chkPazienteGuztiak.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            chkPazienteGuztiak.ForeColor = Color.FromArgb(44, 62, 80);
            chkPazienteGuztiak.Location = new Point(10, 87);
            chkPazienteGuztiak.Name = "chkPazienteGuztiak";
            chkPazienteGuztiak.Size = new Size(309, 40);
            chkPazienteGuztiak.TabIndex = 4;
            chkPazienteGuztiak.Text = "Paziente guztiak ikusi";
            chkPazienteGuztiak.UseVisualStyleBackColor = true;
            chkPazienteGuztiak.CheckedChanged += chkPazienteGuztiak_CheckedChanged;
            // 
            // lblGrafikaMota
            // 
            lblGrafikaMota.AutoSize = true;
            lblGrafikaMota.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblGrafikaMota.ForeColor = Color.FromArgb(44, 62, 80);
            lblGrafikaMota.Location = new Point(664, -1);
            lblGrafikaMota.Name = "lblGrafikaMota";
            lblGrafikaMota.Size = new Size(202, 41);
            lblGrafikaMota.TabIndex = 4;
            lblGrafikaMota.Text = "Grafika mota";
            // 
            // cmbGrafikaMota
            // 
            cmbGrafikaMota.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGrafikaMota.Font = new Font("Segoe UI", 11F);
            cmbGrafikaMota.FormattingEnabled = true;
            cmbGrafikaMota.Location = new Point(660, 40);
            cmbGrafikaMota.Name = "cmbGrafikaMota";
            cmbGrafikaMota.Size = new Size(341, 48);
            cmbGrafikaMota.TabIndex = 5;
            // 
            // lblHasiera
            // 
            lblHasiera.AutoSize = true;
            lblHasiera.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblHasiera.ForeColor = Color.FromArgb(44, 62, 80);
            lblHasiera.Location = new Point(1007, -1);
            lblHasiera.Name = "lblHasiera";
            lblHasiera.Size = new Size(194, 41);
            lblHasiera.TabIndex = 6;
            lblHasiera.Text = "Hasiera data";
            // 
            // dtpHasiera
            // 
            dtpHasiera.Font = new Font("Segoe UI", 11F);
            dtpHasiera.Format = DateTimePickerFormat.Short;
            dtpHasiera.Location = new Point(1006, 40);
            dtpHasiera.Name = "dtpHasiera";
            dtpHasiera.Size = new Size(231, 47);
            dtpHasiera.TabIndex = 7;
            // 
            // lblAmaiera
            // 
            lblAmaiera.AutoSize = true;
            lblAmaiera.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblAmaiera.ForeColor = Color.FromArgb(44, 62, 80);
            lblAmaiera.Location = new Point(1255, -1);
            lblAmaiera.Name = "lblAmaiera";
            lblAmaiera.Size = new Size(206, 41);
            lblAmaiera.TabIndex = 8;
            lblAmaiera.Text = "Amaiera data";
            // 
            // dtpAmaiera
            // 
            dtpAmaiera.Font = new Font("Segoe UI", 11F);
            dtpAmaiera.Format = DateTimePickerFormat.Short;
            dtpAmaiera.Location = new Point(1255, 40);
            dtpAmaiera.Name = "dtpAmaiera";
            dtpAmaiera.Size = new Size(225, 47);
            dtpAmaiera.TabIndex = 9;
            // 
            // btnGrafikoaErakutsi
            // 
            btnGrafikoaErakutsi.BackColor = Color.FromArgb(52, 152, 219);
            btnGrafikoaErakutsi.FlatAppearance.BorderSize = 0;
            btnGrafikoaErakutsi.FlatStyle = FlatStyle.Flat;
            btnGrafikoaErakutsi.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnGrafikoaErakutsi.ForeColor = Color.White;
            btnGrafikoaErakutsi.Location = new Point(1594, 10);
            btnGrafikoaErakutsi.Name = "btnGrafikoaErakutsi";
            btnGrafikoaErakutsi.Size = new Size(295, 78);
            btnGrafikoaErakutsi.TabIndex = 10;
            btnGrafikoaErakutsi.Text = "Grafikoa erakutsi";
            btnGrafikoaErakutsi.UseVisualStyleBackColor = false;
            // 
            // lblEgoera
            // 
            lblEgoera.Font = new Font("Segoe UI", 12F, FontStyle.Italic);
            lblEgoera.ForeColor = Color.FromArgb(127, 140, 141);
            lblEgoera.Location = new Point(984, 225);
            lblEgoera.Name = "lblEgoera";
            lblEgoera.Size = new Size(825, 50);
            lblEgoera.TabIndex = 2;
            lblEgoera.Text = "Pazienteak kargatzen...";
            lblEgoera.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblPazienteDatuak
            // 
            lblPazienteDatuak.AutoSize = true;
            lblPazienteDatuak.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblPazienteDatuak.ForeColor = Color.DimGray;
            lblPazienteDatuak.Location = new Point(94, 304);
            lblPazienteDatuak.Name = "lblPazienteDatuak";
            lblPazienteDatuak.Size = new Size(33, 45);
            lblPazienteDatuak.TabIndex = 3;
            lblPazienteDatuak.Text = "-";
            // 
            // lblAzalpena
            // 
            lblAzalpena.Font = new Font("Segoe UI", 9.5F);
            lblAzalpena.ForeColor = Color.DimGray;
            lblAzalpena.Location = new Point(832, 318);
            lblAzalpena.Name = "lblAzalpena";
            lblAzalpena.Size = new Size(975, 32);
            lblAzalpena.TabIndex = 4;
            lblAzalpena.Text = "-";
            lblAzalpena.Click += lblAzalpena_Click;
            // 
            // pnlGrafikoa
            // 
            pnlGrafikoa.BackColor = Color.White;
            pnlGrafikoa.BorderStyle = BorderStyle.FixedSingle;
            pnlGrafikoa.Location = new Point(94, 349);
            pnlGrafikoa.Name = "pnlGrafikoa";
            pnlGrafikoa.Size = new Size(1714, 756);
            pnlGrafikoa.TabIndex = 5;
            // 
            // Grafikak
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1902, 1580);
            Name = "Grafikak";
            Text = "GOsasun - Grafikak";
            _edukiPanela.ResumeLayout(false);
            _edukiPanela.PerformLayout();
            pnlIragazkiak.ResumeLayout(false);
            pnlIragazkiak.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblIzenburua;
        private System.Windows.Forms.Panel pnlIragazkiak;
        private System.Windows.Forms.Label lblPazienteBilatu;
        private System.Windows.Forms.TextBox txtPazienteBilatu;
        private System.Windows.Forms.Label lblPazientea;
        private System.Windows.Forms.ComboBox cmbPazienteak;
        private System.Windows.Forms.CheckBox chkPazienteGuztiak;
        private System.Windows.Forms.Label lblGrafikaMota;
        private System.Windows.Forms.ComboBox cmbGrafikaMota;
        private System.Windows.Forms.Label lblHasiera;
        private System.Windows.Forms.DateTimePicker dtpHasiera;
        private System.Windows.Forms.Label lblAmaiera;
        private System.Windows.Forms.DateTimePicker dtpAmaiera;
        private System.Windows.Forms.Button btnGrafikoaErakutsi;
        private System.Windows.Forms.Label lblPazienteDatuak;
        private System.Windows.Forms.Label lblEgoera;
        private System.Windows.Forms.Label lblAzalpena;
        private System.Windows.Forms.Panel pnlGrafikoa;
    }
}

