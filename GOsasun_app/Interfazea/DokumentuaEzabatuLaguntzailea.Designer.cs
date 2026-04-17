namespace GOsasun_app.Interfazea
{
    partial class DokumentuaEzabatuLaguntzailea
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
            pbAbisua = new PictureBox();
            lblGaldera = new Label();
            pnlBotoiak = new Panel();
            btnBai = new Button();
            btnEz = new Button();
            ((System.ComponentModel.ISupportInitialize)pbAbisua).BeginInit();
            pnlBotoiak.SuspendLayout();
            SuspendLayout();
            // 
            // pbAbisua
            // 
            pbAbisua.Location = new Point(12, 28);
            pbAbisua.Name = "pbAbisua";
            pbAbisua.Size = new Size(140, 145);
            pbAbisua.SizeMode = PictureBoxSizeMode.StretchImage;
            pbAbisua.TabIndex = 0;
            pbAbisua.TabStop = false;
            // 
            // lblGaldera
            // 
            lblGaldera.Font = new Font("Segoe UI", 13F);
            lblGaldera.Location = new Point(158, 28);
            lblGaldera.Name = "lblGaldera";
            lblGaldera.Size = new Size(688, 178);
            lblGaldera.TabIndex = 1;
            lblGaldera.Text = "Galdera";
            // 
            // pnlBotoiak
            // 
            pnlBotoiak.BackColor = Color.FromArgb(245, 245, 245);
            pnlBotoiak.Controls.Add(btnBai);
            pnlBotoiak.Controls.Add(btnEz);
            pnlBotoiak.Dock = DockStyle.Bottom;
            pnlBotoiak.Location = new Point(0, 228);
            pnlBotoiak.Name = "pnlBotoiak";
            pnlBotoiak.Size = new Size(846, 98);
            pnlBotoiak.TabIndex = 2;
            // 
            // btnBai
            // 
            btnBai.Font = new Font("Segoe UI", 12F);
            btnBai.Location = new Point(466, 20);
            btnBai.Name = "btnBai";
            btnBai.Size = new Size(160, 52);
            btnBai.TabIndex = 0;
            btnBai.Text = "BAI";
            btnBai.UseVisualStyleBackColor = true;
            // 
            // btnEz
            // 
            btnEz.Font = new Font("Segoe UI", 12F);
            btnEz.Location = new Point(644, 20);
            btnEz.Name = "btnEz";
            btnEz.Size = new Size(160, 52);
            btnEz.TabIndex = 1;
            btnEz.Text = "EZ";
            btnEz.UseVisualStyleBackColor = true;
            btnEz.Click += btnEz_Click;
            // 
            // DokumentuaEzabatuLaguntzailea
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(846, 326);
            Controls.Add(pnlBotoiak);
            Controls.Add(lblGaldera);
            Controls.Add(pbAbisua);
            Name = "DokumentuaEzabatuLaguntzailea";
            Text = "Dokumentua ezabatu";
            ((System.ComponentModel.ISupportInitialize)pbAbisua).EndInit();
            pnlBotoiak.ResumeLayout(false);
            ResumeLayout(false);
        }

        private PictureBox pbAbisua;
        private Label lblGaldera;
        private Panel pnlBotoiak;
        private Button btnBai;
        private Button btnEz;
    }
}