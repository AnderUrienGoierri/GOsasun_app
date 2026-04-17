namespace GOsasun_app.Interfazea
{
    partial class EsleituOsasunLangileakLaguntzailea
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
            lblAzalpena = new Label();
            lblEspezialitatea = new Label();
            cmbEspezialitatea = new ComboBox();
            lblBilaketa = new Label();
            txtBilaketa = new TextBox();
            dgvLangileak = new DataGridView();
            lblEmaitzak = new Label();
            btnGehitu = new Button();
            lblHautatutakoak = new Label();
            lstHautatutakoak = new ListBox();
            btnKendu = new Button();
            lblJadaEsleituta = new Label();
            btnEsleitu = new Button();
            btnUtzi = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvLangileak).BeginInit();
            SuspendLayout();
            // 
            // lblPazientea
            // 
            lblPazientea.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblPazientea.Location = new Point(24, 16);
            lblPazientea.Name = "lblPazientea";
            lblPazientea.Size = new Size(880, 47);
            lblPazientea.TabIndex = 0;
            lblPazientea.Text = "Pazientea";
            // 
            // lblAzalpena
            // 
            lblAzalpena.Font = new Font("Segoe UI", 9.5F);
            lblAzalpena.ForeColor = Color.FromArgb(95, 95, 95);
            lblAzalpena.Location = new Point(24, 63);
            lblAzalpena.Name = "lblAzalpena";
            lblAzalpena.Size = new Size(1240, 42);
            lblAzalpena.TabIndex = 1;
            lblAzalpena.Text = "Aukeratu lehenik espezialitatea eta ondoren bilatu izen, abizen, DNI, elkargokide zenbakia edo kontsultaren arabera.";
            // 
            // lblEspezialitatea
            // 
            lblEspezialitatea.AutoSize = true;
            lblEspezialitatea.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEspezialitatea.Location = new Point(24, 110);
            lblEspezialitatea.Name = "lblEspezialitatea";
            lblEspezialitatea.Size = new Size(194, 37);
            lblEspezialitatea.TabIndex = 2;
            lblEspezialitatea.Text = "Espezialitatea";
            // 
            // cmbEspezialitatea
            // 
            cmbEspezialitatea.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEspezialitatea.Font = new Font("Segoe UI", 10F);
            cmbEspezialitatea.FormattingEnabled = true;
            cmbEspezialitatea.Location = new Point(24, 150);
            cmbEspezialitatea.Name = "cmbEspezialitatea";
            cmbEspezialitatea.Size = new Size(360, 45);
            cmbEspezialitatea.TabIndex = 3;
            // 
            // lblBilaketa
            // 
            lblBilaketa.AutoSize = true;
            lblBilaketa.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBilaketa.Location = new Point(410, 110);
            lblBilaketa.Name = "lblBilaketa";
            lblBilaketa.Size = new Size(121, 37);
            lblBilaketa.TabIndex = 4;
            lblBilaketa.Text = "Bilaketa";
            // 
            // txtBilaketa
            // 
            txtBilaketa.Font = new Font("Segoe UI", 10F);
            txtBilaketa.Location = new Point(410, 150);
            txtBilaketa.Name = "txtBilaketa";
            txtBilaketa.PlaceholderText = "Izena, abizena, DNI, elkargokide zenbakia edo kontsulta...";
            txtBilaketa.Size = new Size(704, 43);
            txtBilaketa.TabIndex = 5;
            // 
            // dgvLangileak
            // 
            dgvLangileak.AllowUserToAddRows = false;
            dgvLangileak.AllowUserToDeleteRows = false;
            dgvLangileak.AllowUserToResizeRows = false;
            dgvLangileak.BackgroundColor = Color.White;
            dgvLangileak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLangileak.Location = new Point(24, 201);
            dgvLangileak.MultiSelect = false;
            dgvLangileak.Name = "dgvLangileak";
            dgvLangileak.ReadOnly = true;
            dgvLangileak.RowHeadersVisible = false;
            dgvLangileak.RowHeadersWidth = 82;
            dgvLangileak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLangileak.Size = new Size(914, 585);
            dgvLangileak.TabIndex = 6;
            // 
            // lblEmaitzak
            // 
            lblEmaitzak.Font = new Font("Segoe UI", 9F);
            lblEmaitzak.ForeColor = Color.FromArgb(95, 95, 95);
            lblEmaitzak.Location = new Point(24, 744);
            lblEmaitzak.Name = "lblEmaitzak";
            lblEmaitzak.Size = new Size(886, 28);
            lblEmaitzak.TabIndex = 7;
            // 
            // btnGehitu
            // 
            btnGehitu.BackColor = Color.FromArgb(142, 68, 173);
            btnGehitu.Enabled = false;
            btnGehitu.FlatAppearance.BorderSize = 0;
            btnGehitu.FlatStyle = FlatStyle.Flat;
            btnGehitu.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnGehitu.ForeColor = Color.White;
            btnGehitu.Location = new Point(944, 212);
            btnGehitu.Name = "btnGehitu";
            btnGehitu.Size = new Size(170, 52);
            btnGehitu.TabIndex = 8;
            btnGehitu.Text = "+ Gehitu";
            btnGehitu.UseVisualStyleBackColor = false;
            // 
            // lblHautatutakoak
            // 
            lblHautatutakoak.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHautatutakoak.Location = new Point(1140, 110);
            lblHautatutakoak.Name = "lblHautatutakoak";
            lblHautatutakoak.Size = new Size(180, 74);
            lblHautatutakoak.TabIndex = 9;
            lblHautatutakoak.Text = "Esleitzeko hautatutako osasun langileak";
            // 
            // lstHautatutakoak
            // 
            lstHautatutakoak.Font = new Font("Segoe UI", 9.5F);
            lstHautatutakoak.FormattingEnabled = true;
            lstHautatutakoak.HorizontalScrollbar = true;
            lstHautatutakoak.Location = new Point(1140, 190);
            lstHautatutakoak.Name = "lstHautatutakoak";
            lstHautatutakoak.Size = new Size(180, 389);
            lstHautatutakoak.TabIndex = 10;
            // 
            // btnKendu
            // 
            btnKendu.BackColor = Color.FromArgb(127, 140, 141);
            btnKendu.FlatAppearance.BorderSize = 0;
            btnKendu.FlatStyle = FlatStyle.Flat;
            btnKendu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnKendu.ForeColor = Color.White;
            btnKendu.Location = new Point(1140, 620);
            btnKendu.Name = "btnKendu";
            btnKendu.Size = new Size(132, 44);
            btnKendu.TabIndex = 11;
            btnKendu.Text = "Kendu";
            btnKendu.UseVisualStyleBackColor = false;
            // 
            // lblJadaEsleituta
            // 
            lblJadaEsleituta.Font = new Font("Segoe UI", 9F);
            lblJadaEsleituta.ForeColor = Color.FromArgb(95, 95, 95);
            lblJadaEsleituta.Location = new Point(1140, 680);
            lblJadaEsleituta.Name = "lblJadaEsleituta";
            lblJadaEsleituta.Size = new Size(180, 56);
            lblJadaEsleituta.TabIndex = 12;
            // 
            // btnEsleitu
            // 
            btnEsleitu.BackColor = Color.FromArgb(39, 174, 96);
            btnEsleitu.FlatAppearance.BorderSize = 0;
            btnEsleitu.FlatStyle = FlatStyle.Flat;
            btnEsleitu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEsleitu.ForeColor = Color.White;
            btnEsleitu.Location = new Point(1020, 778);
            btnEsleitu.Name = "btnEsleitu";
            btnEsleitu.Size = new Size(152, 44);
            btnEsleitu.TabIndex = 13;
            btnEsleitu.Text = "Esleitu";
            btnEsleitu.UseVisualStyleBackColor = false;
            // 
            // btnUtzi
            // 
            btnUtzi.DialogResult = DialogResult.Cancel;
            btnUtzi.Location = new Point(1190, 778);
            btnUtzi.Name = "btnUtzi";
            btnUtzi.Size = new Size(130, 44);
            btnUtzi.TabIndex = 14;
            btnUtzi.Text = "Utzi";
            btnUtzi.UseVisualStyleBackColor = true;
            // 
            // EsleituOsasunLangileakLaguntzailea
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1348, 840);
            Controls.Add(btnUtzi);
            Controls.Add(btnEsleitu);
            Controls.Add(lblJadaEsleituta);
            Controls.Add(btnKendu);
            Controls.Add(lstHautatutakoak);
            Controls.Add(lblHautatutakoak);
            Controls.Add(btnGehitu);
            Controls.Add(lblEmaitzak);
            Controls.Add(dgvLangileak);
            Controls.Add(txtBilaketa);
            Controls.Add(lblBilaketa);
            Controls.Add(cmbEspezialitatea);
            Controls.Add(lblEspezialitatea);
            Controls.Add(lblAzalpena);
            Controls.Add(lblPazientea);
            Name = "EsleituOsasunLangileakLaguntzailea";
            Text = "Esleitu osasun langileak";
            ((System.ComponentModel.ISupportInitialize)dgvLangileak).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblPazientea;
        private Label lblAzalpena;
        private Label lblEspezialitatea;
        private ComboBox cmbEspezialitatea;
        private Label lblBilaketa;
        private TextBox txtBilaketa;
        private DataGridView dgvLangileak;
        private Label lblEmaitzak;
        private Button btnGehitu;
        private Label lblHautatutakoak;
        private ListBox lstHautatutakoak;
        private Button btnKendu;
        private Label lblJadaEsleituta;
        private Button btnEsleitu;
        private Button btnUtzi;
    }
}
