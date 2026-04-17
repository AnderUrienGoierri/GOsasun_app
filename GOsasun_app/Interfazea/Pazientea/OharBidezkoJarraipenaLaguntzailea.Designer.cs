namespace GOsasun_app.Interfazea
{
    partial class OharBidezkoJarraipenaLaguntzailea
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
            lblBilatuPazientea = new Label();
            txtPazienteBilatu = new TextBox();
            lblPazientea = new Label();
            cmbPazienteak = new ComboBox();
            lblBilaketaEmaitza = new Label();
            lblOharrak = new Label();
            txtOharrak = new TextBox();
            btnUtzi = new Button();
            btnGorde = new Button();
            SuspendLayout();
            // 
            // lblBilatuPazientea
            // 
            lblBilatuPazientea.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblBilatuPazientea.ForeColor = Color.FromArgb(44, 62, 80);
            lblBilatuPazientea.Location = new Point(24, 17);
            lblBilatuPazientea.Name = "lblBilatuPazientea";
            lblBilatuPazientea.Size = new Size(470, 34);
            lblBilatuPazientea.TabIndex = 0;
            lblBilatuPazientea.Text = "Bilatu pazientea (abizena, izena edo NAN/DNI)";
            // 
            // txtPazienteBilatu
            // 
            txtPazienteBilatu.Font = new Font("Segoe UI", 11F);
            txtPazienteBilatu.Location = new Point(24, 60);
            txtPazienteBilatu.Name = "txtPazienteBilatu";
            txtPazienteBilatu.PlaceholderText = "Idatzi abizena, izena edo NAN/DNI...";
            txtPazienteBilatu.Size = new Size(712, 47);
            txtPazienteBilatu.TabIndex = 1;
            // 
            // lblPazientea
            // 
            lblPazientea.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblPazientea.ForeColor = Color.FromArgb(44, 62, 80);
            lblPazientea.Location = new Point(24, 116);
            lblPazientea.Name = "lblPazientea";
            lblPazientea.Size = new Size(160, 34);
            lblPazientea.TabIndex = 2;
            lblPazientea.Text = "Pazientea";
            // 
            // cmbPazienteak
            // 
            cmbPazienteak.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPazienteak.Font = new Font("Segoe UI", 11F);
            cmbPazienteak.FormattingEnabled = true;
            cmbPazienteak.Location = new Point(24, 156);
            cmbPazienteak.Name = "cmbPazienteak";
            cmbPazienteak.Size = new Size(712, 48);
            cmbPazienteak.TabIndex = 3;
            // 
            // lblBilaketaEmaitza
            // 
            lblBilaketaEmaitza.Font = new Font("Segoe UI", 9F);
            lblBilaketaEmaitza.ForeColor = Color.FromArgb(90, 90, 90);
            lblBilaketaEmaitza.Location = new Point(24, 210);
            lblBilaketaEmaitza.Name = "lblBilaketaEmaitza";
            lblBilaketaEmaitza.Size = new Size(712, 24);
            lblBilaketaEmaitza.TabIndex = 4;
            // 
            // lblOharrak
            // 
            lblOharrak.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblOharrak.ForeColor = Color.FromArgb(44, 62, 80);
            lblOharrak.Location = new Point(24, 248);
            lblOharrak.Name = "lblOharrak";
            lblOharrak.Size = new Size(220, 34);
            lblOharrak.TabIndex = 5;
            lblOharrak.Text = "Idatzi oharra";
            // 
            // txtOharrak
            // 
            txtOharrak.Font = new Font("Segoe UI", 11F);
            txtOharrak.Location = new Point(24, 290);
            txtOharrak.Multiline = true;
            txtOharrak.Name = "txtOharrak";
            txtOharrak.PlaceholderText = "Jarraipen honen oharra idatzi hemen...";
            txtOharrak.ScrollBars = ScrollBars.Vertical;
            txtOharrak.Size = new Size(712, 180);
            txtOharrak.TabIndex = 6;
            // 
            // btnUtzi
            // 
            btnUtzi.BackColor = Color.FromArgb(127, 140, 141);
            btnUtzi.DialogResult = DialogResult.Cancel;
            btnUtzi.FlatAppearance.BorderSize = 0;
            btnUtzi.FlatStyle = FlatStyle.Flat;
            btnUtzi.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnUtzi.ForeColor = Color.White;
            btnUtzi.Location = new Point(240, 473);
            btnUtzi.Name = "btnUtzi";
            btnUtzi.Size = new Size(170, 50);
            btnUtzi.TabIndex = 7;
            btnUtzi.Text = "Utzi";
            btnUtzi.UseVisualStyleBackColor = false;
            // 
            // btnGorde
            // 
            btnGorde.BackColor = Color.FromArgb(52, 152, 219);
            btnGorde.FlatAppearance.BorderSize = 0;
            btnGorde.FlatStyle = FlatStyle.Flat;
            btnGorde.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnGorde.ForeColor = Color.White;
            btnGorde.Location = new Point(426, 473);
            btnGorde.Name = "btnGorde";
            btnGorde.Size = new Size(310, 50);
            btnGorde.TabIndex = 8;
            btnGorde.Text = "Jarraipena sortu";
            btnGorde.UseVisualStyleBackColor = false;
            // 
            // OharBidezkoJarraipenaLaguntzailea
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(760, 540);
            Controls.Add(btnGorde);
            Controls.Add(btnUtzi);
            Controls.Add(txtOharrak);
            Controls.Add(lblOharrak);
            Controls.Add(lblBilaketaEmaitza);
            Controls.Add(cmbPazienteak);
            Controls.Add(lblPazientea);
            Controls.Add(txtPazienteBilatu);
            Controls.Add(lblBilatuPazientea);
            Name = "OharBidezkoJarraipenaLaguntzailea";
            Text = "Ohar bidezko jarraipena";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblBilatuPazientea;
        private TextBox txtPazienteBilatu;
        private Label lblPazientea;
        private ComboBox cmbPazienteak;
        private Label lblBilaketaEmaitza;
        private Label lblOharrak;
        private TextBox txtOharrak;
        private Button btnUtzi;
        private Button btnGorde;
    }
}
