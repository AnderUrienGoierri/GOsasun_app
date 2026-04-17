namespace GOsasun_app.Interfazea
{
    partial class OsasunTxostenaSortuLaguntzailea
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
            lblPazientea = new Label();
            txtPazienteBilaketa = new TextBox();
            lstPazienteak = new ListBox();
            lblPazienteakEgoera = new Label();
            lblTxostenIzena = new Label();
            txtTxostenIzena = new TextBox();
            lblDeskribapena = new Label();
            txtDeskribapena = new TextBox();
            lblGrafikak = new Label();
            clbGrafikak = new CheckedListBox();
            lblGrafikaAzalpena = new Label();
            chkGrafikaDatuGuztiak = new CheckBox();
            lblGrafikaHasiera = new Label();
            dtpGrafikaHasiera = new DateTimePicker();
            lblGrafikaAmaiera = new Label();
            dtpGrafikaAmaiera = new DateTimePicker();
            lblGrafikaDataTartea = new Label();
            btnSortu = new Button();
            btnUtzi = new Button();
            SuspendLayout();
            // 
            // lblPazientea
            // 
            lblPazientea.AutoSize = true;
            lblPazientea.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPazientea.Location = new Point(24, 24);
            lblPazientea.Name = "lblPazientea";
            lblPazientea.Size = new Size(403, 37);
            lblPazientea.TabIndex = 0;
            lblPazientea.Text = "Bilatu pazientea (abizena, izena edo NAN/DNI)";
            // 
            // txtPazienteBilaketa
            // 
            txtPazienteBilaketa.Font = new Font("Segoe UI", 10F);
            txtPazienteBilaketa.Location = new Point(24, 54);
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
            lstPazienteak.ItemHeight = 37;
            lstPazienteak.Location = new Point(24, 106);
            lstPazienteak.Name = "lstPazienteak";
            lstPazienteak.Size = new Size(632, 170);
            lstPazienteak.TabIndex = 2;
            // 
            // lblPazienteakEgoera
            // 
            lblPazienteakEgoera.Font = new Font("Segoe UI", 9F);
            lblPazienteakEgoera.ForeColor = Color.FromArgb(90, 90, 90);
            lblPazienteakEgoera.Location = new Point(24, 284);
            lblPazienteakEgoera.Name = "lblPazienteakEgoera";
            lblPazienteakEgoera.Size = new Size(632, 44);
            lblPazienteakEgoera.TabIndex = 3;
            // 
            // lblTxostenIzena
            // 
            lblTxostenIzena.AutoSize = true;
            lblTxostenIzena.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTxostenIzena.Location = new Point(24, 330);
            lblTxostenIzena.Name = "lblTxostenIzena";
            lblTxostenIzena.Size = new Size(188, 37);
            lblTxostenIzena.TabIndex = 4;
            lblTxostenIzena.Text = "Txostenaren izena";
            // 
            // txtTxostenIzena
            // 
            txtTxostenIzena.Font = new Font("Segoe UI", 10F);
            txtTxostenIzena.Location = new Point(24, 360);
            txtTxostenIzena.Name = "txtTxostenIzena";
            txtTxostenIzena.Size = new Size(632, 43);
            txtTxostenIzena.TabIndex = 5;
            // 
            // lblDeskribapena
            // 
            lblDeskribapena.AutoSize = true;
            lblDeskribapena.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDeskribapena.Location = new Point(24, 412);
            lblDeskribapena.Name = "lblDeskribapena";
            lblDeskribapena.Size = new Size(144, 37);
            lblDeskribapena.TabIndex = 6;
            lblDeskribapena.Text = "Deskribapena";
            // 
            // txtDeskribapena
            // 
            txtDeskribapena.Font = new Font("Segoe UI", 10F);
            txtDeskribapena.Location = new Point(24, 442);
            txtDeskribapena.Multiline = true;
            txtDeskribapena.Name = "txtDeskribapena";
            txtDeskribapena.Size = new Size(632, 120);
            txtDeskribapena.TabIndex = 7;
            // 
            // lblGrafikak
            // 
            lblGrafikak.AutoSize = true;
            lblGrafikak.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblGrafikak.Location = new Point(24, 580);
            lblGrafikak.Name = "lblGrafikak";
            lblGrafikak.Size = new Size(426, 37);
            lblGrafikak.TabIndex = 8;
            lblGrafikak.Text = "Txostenean txertatu beharreko grafika medikoak";
            // 
            // clbGrafikak
            // 
            clbGrafikak.BorderStyle = BorderStyle.FixedSingle;
            clbGrafikak.CheckOnClick = true;
            clbGrafikak.Font = new Font("Segoe UI", 10F);
            clbGrafikak.FormattingEnabled = true;
            clbGrafikak.Location = new Point(24, 614);
            clbGrafikak.Name = "clbGrafikak";
            clbGrafikak.Size = new Size(632, 126);
            clbGrafikak.TabIndex = 9;
            // 
            // lblGrafikaAzalpena
            // 
            lblGrafikaAzalpena.Font = new Font("Segoe UI", 9F);
            lblGrafikaAzalpena.ForeColor = Color.FromArgb(90, 90, 90);
            lblGrafikaAzalpena.Location = new Point(24, 748);
            lblGrafikaAzalpena.Name = "lblGrafikaAzalpena";
            lblGrafikaAzalpena.Size = new Size(632, 44);
            lblGrafikaAzalpena.TabIndex = 10;
            lblGrafikaAzalpena.Text = "Grafika bat edo gehiago hauta ditzakezu. Lerro etenek joera lineala erakutsiko dute.";
            // 
            // chkGrafikaDatuGuztiak
            // 
            chkGrafikaDatuGuztiak.AutoSize = true;
            chkGrafikaDatuGuztiak.Checked = true;
            chkGrafikaDatuGuztiak.CheckState = CheckState.Checked;
            chkGrafikaDatuGuztiak.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            chkGrafikaDatuGuztiak.Location = new Point(24, 798);
            chkGrafikaDatuGuztiak.Name = "chkGrafikaDatuGuztiak";
            chkGrafikaDatuGuztiak.Size = new Size(292, 39);
            chkGrafikaDatuGuztiak.TabIndex = 11;
            chkGrafikaDatuGuztiak.Text = "Erabili neurketa guztiak (defektuz)";
            chkGrafikaDatuGuztiak.UseVisualStyleBackColor = true;
            // 
            // lblGrafikaHasiera
            // 
            lblGrafikaHasiera.AutoSize = true;
            lblGrafikaHasiera.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblGrafikaHasiera.Location = new Point(24, 834);
            lblGrafikaHasiera.Name = "lblGrafikaHasiera";
            lblGrafikaHasiera.Size = new Size(123, 35);
            lblGrafikaHasiera.TabIndex = 12;
            lblGrafikaHasiera.Text = "Hasiera data";
            // 
            // dtpGrafikaHasiera
            // 
            dtpGrafikaHasiera.Enabled = false;
            dtpGrafikaHasiera.Format = DateTimePickerFormat.Short;
            dtpGrafikaHasiera.Location = new Point(24, 864);
            dtpGrafikaHasiera.Name = "dtpGrafikaHasiera";
            dtpGrafikaHasiera.Size = new Size(250, 39);
            dtpGrafikaHasiera.TabIndex = 13;
            // 
            // lblGrafikaAmaiera
            // 
            lblGrafikaAmaiera.AutoSize = true;
            lblGrafikaAmaiera.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblGrafikaAmaiera.Location = new Point(314, 834);
            lblGrafikaAmaiera.Name = "lblGrafikaAmaiera";
            lblGrafikaAmaiera.Size = new Size(123, 35);
            lblGrafikaAmaiera.TabIndex = 14;
            lblGrafikaAmaiera.Text = "Amaiera data";
            // 
            // dtpGrafikaAmaiera
            // 
            dtpGrafikaAmaiera.Enabled = false;
            dtpGrafikaAmaiera.Format = DateTimePickerFormat.Short;
            dtpGrafikaAmaiera.Location = new Point(314, 864);
            dtpGrafikaAmaiera.Name = "dtpGrafikaAmaiera";
            dtpGrafikaAmaiera.Size = new Size(250, 39);
            dtpGrafikaAmaiera.TabIndex = 15;
            // 
            // lblGrafikaDataTartea
            // 
            lblGrafikaDataTartea.Font = new Font("Segoe UI", 9F);
            lblGrafikaDataTartea.ForeColor = Color.FromArgb(90, 90, 90);
            lblGrafikaDataTartea.Location = new Point(24, 908);
            lblGrafikaDataTartea.Name = "lblGrafikaDataTartea";
            lblGrafikaDataTartea.Size = new Size(632, 44);
            lblGrafikaDataTartea.TabIndex = 16;
            // 
            // btnSortu
            // 
            btnSortu.BackColor = Color.FromArgb(41, 128, 185);
            btnSortu.Cursor = Cursors.Hand;
            btnSortu.FlatAppearance.BorderSize = 0;
            btnSortu.FlatStyle = FlatStyle.Flat;
            btnSortu.ForeColor = Color.White;
            btnSortu.Location = new Point(464, 970);
            btnSortu.Name = "btnSortu";
            btnSortu.Size = new Size(92, 42);
            btnSortu.TabIndex = 17;
            btnSortu.Text = "Sortu";
            btnSortu.UseVisualStyleBackColor = false;
            // 
            // btnUtzi
            // 
            btnUtzi.DialogResult = DialogResult.Cancel;
            btnUtzi.Location = new Point(564, 970);
            btnUtzi.Name = "btnUtzi";
            btnUtzi.Size = new Size(92, 42);
            btnUtzi.TabIndex = 18;
            btnUtzi.Text = "Utzi";
            btnUtzi.UseVisualStyleBackColor = true;
            // 
            // OsasunTxostenaSortuLaguntzailea
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(760, 1040);
            Controls.Add(btnUtzi);
            Controls.Add(btnSortu);
            Controls.Add(lblGrafikaDataTartea);
            Controls.Add(dtpGrafikaAmaiera);
            Controls.Add(lblGrafikaAmaiera);
            Controls.Add(dtpGrafikaHasiera);
            Controls.Add(lblGrafikaHasiera);
            Controls.Add(chkGrafikaDatuGuztiak);
            Controls.Add(lblGrafikaAzalpena);
            Controls.Add(clbGrafikak);
            Controls.Add(lblGrafikak);
            Controls.Add(txtDeskribapena);
            Controls.Add(lblDeskribapena);
            Controls.Add(txtTxostenIzena);
            Controls.Add(lblTxostenIzena);
            Controls.Add(lblPazienteakEgoera);
            Controls.Add(lstPazienteak);
            Controls.Add(txtPazienteBilaketa);
            Controls.Add(lblPazientea);
            Name = "OsasunTxostenaSortuLaguntzailea";
            Text = "Osasun txostena sortu";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblPazientea;
        private TextBox txtPazienteBilaketa;
        private ListBox lstPazienteak;
        private Label lblPazienteakEgoera;
        private Label lblTxostenIzena;
        private TextBox txtTxostenIzena;
        private Label lblDeskribapena;
        private TextBox txtDeskribapena;
        private Label lblGrafikak;
        private CheckedListBox clbGrafikak;
        private Label lblGrafikaAzalpena;
        private CheckBox chkGrafikaDatuGuztiak;
        private Label lblGrafikaHasiera;
        private DateTimePicker dtpGrafikaHasiera;
        private Label lblGrafikaAmaiera;
        private DateTimePicker dtpGrafikaAmaiera;
        private Label lblGrafikaDataTartea;
        private Button btnSortu;
        private Button btnUtzi;
    }
}
