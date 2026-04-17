namespace GOsasun_app.Interfazea
{
    partial class DokumentuBerriaLaguntzailea
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
            lblPazienteBilaketa = new Label();
            txtPazienteBilaketa = new TextBox();
            lstPazienteak = new ListBox();
            lblPazienteakEgoera = new Label();
            lblDokumentuIzena = new Label();
            txtDokumentuIzena = new TextBox();
            lblPdfFitxategia = new Label();
            txtPdfFitxategia = new TextBox();
            btnPdfHautatu = new Button();
            lblDeskribapena = new Label();
            txtDeskribapena = new TextBox();
            btnGorde = new Button();
            btnUtzi = new Button();
            SuspendLayout();
            // 
            // lblPazienteBilaketa
            // 
            lblPazienteBilaketa.AutoSize = true;
            lblPazienteBilaketa.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPazienteBilaketa.Location = new Point(24, 19);
            lblPazienteBilaketa.Name = "lblPazienteBilaketa";
            lblPazienteBilaketa.Size = new Size(617, 37);
            lblPazienteBilaketa.TabIndex = 0;
            lblPazienteBilaketa.Text = "Bilatu pazientea (abizena, izena edo NAN/DNI)";
            // 
            // txtPazienteBilaketa
            // 
            txtPazienteBilaketa.Font = new Font("Segoe UI", 10F);
            txtPazienteBilaketa.Location = new Point(24, 70);
            txtPazienteBilaketa.Name = "txtPazienteBilaketa";
            txtPazienteBilaketa.PlaceholderText = "Idatzi abizena, izena edo NAN/DNI...";
            txtPazienteBilaketa.Size = new Size(632, 43);
            txtPazienteBilaketa.TabIndex = 1;
            // 
            // lstPazienteak
            // 
            lstPazienteak.Font = new Font("Segoe UI", 10F);
            lstPazienteak.FormattingEnabled = true;
            lstPazienteak.IntegralHeight = false;
            lstPazienteak.Location = new Point(24, 122);
            lstPazienteak.Name = "lstPazienteak";
            lstPazienteak.Size = new Size(632, 150);
            lstPazienteak.TabIndex = 2;
            // 
            // lblPazienteakEgoera
            // 
            lblPazienteakEgoera.Font = new Font("Segoe UI", 9F);
            lblPazienteakEgoera.ForeColor = Color.FromArgb(90, 90, 90);
            lblPazienteakEgoera.Location = new Point(24, 280);
            lblPazienteakEgoera.Name = "lblPazienteakEgoera";
            lblPazienteakEgoera.Size = new Size(632, 32);
            lblPazienteakEgoera.TabIndex = 3;
            // 
            // lblDokumentuIzena
            // 
            lblDokumentuIzena.AutoSize = true;
            lblDokumentuIzena.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDokumentuIzena.Location = new Point(24, 305);
            lblDokumentuIzena.Name = "lblDokumentuIzena";
            lblDokumentuIzena.Size = new Size(242, 37);
            lblDokumentuIzena.TabIndex = 4;
            lblDokumentuIzena.Text = "Dokumentu izena";
            // 
            // txtDokumentuIzena
            // 
            txtDokumentuIzena.Font = new Font("Segoe UI", 10F);
            txtDokumentuIzena.Location = new Point(24, 348);
            txtDokumentuIzena.Name = "txtDokumentuIzena";
            txtDokumentuIzena.Size = new Size(632, 43);
            txtDokumentuIzena.TabIndex = 5;
            // 
            // lblPdfFitxategia
            // 
            lblPdfFitxategia.AutoSize = true;
            lblPdfFitxategia.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPdfFitxategia.Location = new Point(24, 397);
            lblPdfFitxategia.Name = "lblPdfFitxategia";
            lblPdfFitxategia.Size = new Size(200, 37);
            lblPdfFitxategia.TabIndex = 6;
            lblPdfFitxategia.Text = "PDF fitxategia";
            // 
            // txtPdfFitxategia
            // 
            txtPdfFitxategia.Font = new Font("Segoe UI", 10F);
            txtPdfFitxategia.Location = new Point(24, 430);
            txtPdfFitxategia.Name = "txtPdfFitxategia";
            txtPdfFitxategia.ReadOnly = true;
            txtPdfFitxategia.Size = new Size(486, 43);
            txtPdfFitxategia.TabIndex = 7;
            // 
            // btnPdfHautatu
            // 
            btnPdfHautatu.BackColor = Color.FromArgb(44, 62, 80);
            btnPdfHautatu.Cursor = Cursors.Hand;
            btnPdfHautatu.FlatAppearance.BorderSize = 0;
            btnPdfHautatu.FlatStyle = FlatStyle.Flat;
            btnPdfHautatu.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnPdfHautatu.ForeColor = Color.White;
            btnPdfHautatu.Location = new Point(522, 428);
            btnPdfHautatu.Name = "btnPdfHautatu";
            btnPdfHautatu.Size = new Size(134, 42);
            btnPdfHautatu.TabIndex = 8;
            btnPdfHautatu.Text = "PDF";
            btnPdfHautatu.UseVisualStyleBackColor = false;
            // 
            // lblDeskribapena
            // 
            lblDeskribapena.AutoSize = true;
            lblDeskribapena.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDeskribapena.Location = new Point(24, 483);
            lblDeskribapena.Name = "lblDeskribapena";
            lblDeskribapena.Size = new Size(193, 37);
            lblDeskribapena.TabIndex = 9;
            lblDeskribapena.Text = "Deskribapena";
            // 
            // txtDeskribapena
            // 
            txtDeskribapena.Font = new Font("Segoe UI", 10F);
            txtDeskribapena.Location = new Point(24, 528);
            txtDeskribapena.Multiline = true;
            txtDeskribapena.Name = "txtDeskribapena";
            txtDeskribapena.ScrollBars = ScrollBars.Vertical;
            txtDeskribapena.Size = new Size(632, 110);
            txtDeskribapena.TabIndex = 10;
            // 
            // btnGorde
            // 
            btnGorde.BackColor = Color.FromArgb(83, 148, 117);
            btnGorde.Cursor = Cursors.Hand;
            btnGorde.FlatAppearance.BorderSize = 0;
            btnGorde.FlatStyle = FlatStyle.Flat;
            btnGorde.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnGorde.ForeColor = Color.White;
            btnGorde.Location = new Point(233, 676);
            btnGorde.Name = "btnGorde";
            btnGorde.Size = new Size(117, 54);
            btnGorde.TabIndex = 11;
            btnGorde.Text = "Gorde";
            btnGorde.UseVisualStyleBackColor = false;
            // 
            // btnUtzi
            // 
            btnUtzi.DialogResult = DialogResult.Cancel;
            btnUtzi.Font = new Font("Segoe UI", 10.5F);
            btnUtzi.Location = new Point(368, 676);
            btnUtzi.Name = "btnUtzi";
            btnUtzi.Size = new Size(92, 54);
            btnUtzi.TabIndex = 12;
            btnUtzi.Text = "Utzi";
            btnUtzi.UseVisualStyleBackColor = true;
            // 
            // DokumentuBerriaLaguntzailea
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(720, 742);
            Controls.Add(btnUtzi);
            Controls.Add(btnGorde);
            Controls.Add(txtDeskribapena);
            Controls.Add(lblDeskribapena);
            Controls.Add(btnPdfHautatu);
            Controls.Add(txtPdfFitxategia);
            Controls.Add(lblPdfFitxategia);
            Controls.Add(txtDokumentuIzena);
            Controls.Add(lblDokumentuIzena);
            Controls.Add(lblPazienteakEgoera);
            Controls.Add(lstPazienteak);
            Controls.Add(txtPazienteBilaketa);
            Controls.Add(lblPazienteBilaketa);
            Name = "DokumentuBerriaLaguntzailea";
            Text = "Dokumentu berria";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblPazienteBilaketa;
        private TextBox txtPazienteBilaketa;
        private ListBox lstPazienteak;
        private Label lblPazienteakEgoera;
        private Label lblDokumentuIzena;
        private TextBox txtDokumentuIzena;
        private Label lblPdfFitxategia;
        private TextBox txtPdfFitxategia;
        private Button btnPdfHautatu;
        private Label lblDeskribapena;
        private TextBox txtDeskribapena;
        private Button btnGorde;
        private Button btnUtzi;
    }
}