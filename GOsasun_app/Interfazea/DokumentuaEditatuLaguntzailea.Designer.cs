namespace GOsasun_app.Interfazea
{
    partial class DokumentuaEditatuLaguntzailea
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
            lblDokumentuIzena = new Label();
            txtDokumentuIzena = new TextBox();
            lblDeskribapena = new Label();
            txtDeskribapena = new TextBox();
            btnGorde = new Button();
            btnUtzi = new Button();
            SuspendLayout();
            // 
            // lblDokumentuIzena
            // 
            lblDokumentuIzena.AutoSize = true;
            lblDokumentuIzena.Font = new Font("Segoe UI", 11F);
            lblDokumentuIzena.Location = new Point(24, 24);
            lblDokumentuIzena.Name = "lblDokumentuIzena";
            lblDokumentuIzena.Size = new Size(252, 41);
            lblDokumentuIzena.TabIndex = 0;
            lblDokumentuIzena.Text = "Dokumentu izena";
            // 
            // txtDokumentuIzena
            // 
            txtDokumentuIzena.Font = new Font("Segoe UI", 11F);
            txtDokumentuIzena.Location = new Point(24, 70);
            txtDokumentuIzena.Name = "txtDokumentuIzena";
            txtDokumentuIzena.Size = new Size(560, 47);
            txtDokumentuIzena.TabIndex = 1;
            // 
            // lblDeskribapena
            // 
            lblDeskribapena.AutoSize = true;
            lblDeskribapena.Font = new Font("Segoe UI", 11F);
            lblDeskribapena.Location = new Point(24, 132);
            lblDeskribapena.Name = "lblDeskribapena";
            lblDeskribapena.Size = new Size(199, 41);
            lblDeskribapena.TabIndex = 2;
            lblDeskribapena.Text = "Deskribapena";
            // 
            // txtDeskribapena
            // 
            txtDeskribapena.Font = new Font("Segoe UI", 11F);
            txtDeskribapena.Location = new Point(24, 178);
            txtDeskribapena.Multiline = true;
            txtDeskribapena.Name = "txtDeskribapena";
            txtDeskribapena.ScrollBars = ScrollBars.Vertical;
            txtDeskribapena.Size = new Size(560, 88);
            txtDeskribapena.TabIndex = 3;
            // 
            // btnGorde
            // 
            btnGorde.BackColor = Color.FromArgb(83, 148, 117);
            btnGorde.FlatAppearance.BorderSize = 0;
            btnGorde.FlatStyle = FlatStyle.Flat;
            btnGorde.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnGorde.ForeColor = Color.White;
            btnGorde.Location = new Point(148, 276);
            btnGorde.Name = "btnGorde";
            btnGorde.Size = new Size(142, 42);
            btnGorde.TabIndex = 4;
            btnGorde.Text = "Gorde";
            btnGorde.UseVisualStyleBackColor = false;
            // 
            // btnUtzi
            // 
            btnUtzi.DialogResult = DialogResult.Cancel;
            btnUtzi.Font = new Font("Segoe UI", 10.5F);
            btnUtzi.Location = new Point(306, 276);
            btnUtzi.Name = "btnUtzi";
            btnUtzi.Size = new Size(92, 42);
            btnUtzi.TabIndex = 5;
            btnUtzi.Text = "Utzi";
            btnUtzi.UseVisualStyleBackColor = true;
            // 
            // DokumentuaEditatuLaguntzailea
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(620, 330);
            Controls.Add(btnUtzi);
            Controls.Add(btnGorde);
            Controls.Add(txtDeskribapena);
            Controls.Add(lblDeskribapena);
            Controls.Add(txtDokumentuIzena);
            Controls.Add(lblDokumentuIzena);
            Name = "DokumentuaEditatuLaguntzailea";
            Text = "Dokumentua editatu";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblDokumentuIzena;
        private TextBox txtDokumentuIzena;
        private Label lblDeskribapena;
        private TextBox txtDeskribapena;
        private Button btnGorde;
        private Button btnUtzi;
    }
}