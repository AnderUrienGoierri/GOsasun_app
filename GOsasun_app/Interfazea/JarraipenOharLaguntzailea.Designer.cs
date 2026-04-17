namespace GOsasun_app.Interfazea
{
    partial class JarraipenOharLaguntzailea
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
            lblAzalpena = new Label();
            txtOharra = new TextBox();
            btnOharrikGabe = new Button();
            btnGordeOharra = new Button();
            SuspendLayout();
            // 
            // lblAzalpena
            // 
            lblAzalpena.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblAzalpena.ForeColor = Color.FromArgb(44, 62, 80);
            lblAzalpena.Location = new Point(24, 24);
            lblAzalpena.Name = "lblAzalpena";
            lblAzalpena.Size = new Size(672, 70);
            lblAzalpena.TabIndex = 0;
            lblAzalpena.Text = "Azalpena";
            // 
            // txtOharra
            // 
            txtOharra.Font = new Font("Segoe UI", 11F);
            txtOharra.Location = new Point(24, 108);
            txtOharra.Multiline = true;
            txtOharra.Name = "txtOharra";
            txtOharra.ScrollBars = ScrollBars.Vertical;
            txtOharra.Size = new Size(672, 220);
            txtOharra.TabIndex = 1;
            // 
            // btnOharrikGabe
            // 
            btnOharrikGabe.BackColor = Color.FromArgb(127, 140, 141);
            btnOharrikGabe.FlatAppearance.BorderSize = 0;
            btnOharrikGabe.FlatStyle = FlatStyle.Flat;
            btnOharrikGabe.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnOharrikGabe.ForeColor = Color.White;
            btnOharrikGabe.Location = new Point(358, 346);
            btnOharrikGabe.Name = "btnOharrikGabe";
            btnOharrikGabe.Size = new Size(160, 46);
            btnOharrikGabe.TabIndex = 2;
            btnOharrikGabe.Text = "Oharrik gabe";
            btnOharrikGabe.UseVisualStyleBackColor = false;
            btnOharrikGabe.Click += BtnOharrikGabe_Click;
            // 
            // btnGordeOharra
            // 
            btnGordeOharra.BackColor = Color.FromArgb(52, 152, 219);
            btnGordeOharra.FlatAppearance.BorderSize = 0;
            btnGordeOharra.FlatStyle = FlatStyle.Flat;
            btnGordeOharra.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnGordeOharra.ForeColor = Color.White;
            btnGordeOharra.Location = new Point(536, 346);
            btnGordeOharra.Name = "btnGordeOharra";
            btnGordeOharra.Size = new Size(160, 46);
            btnGordeOharra.TabIndex = 3;
            btnGordeOharra.Text = "Oharra gorde";
            btnGordeOharra.UseVisualStyleBackColor = false;
            btnGordeOharra.Click += BtnGordeOharra_Click;
            // 
            // JarraipenOharLaguntzailea
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(720, 420);
            Controls.Add(btnGordeOharra);
            Controls.Add(btnOharrikGabe);
            Controls.Add(txtOharra);
            Controls.Add(lblAzalpena);
            Name = "JarraipenOharLaguntzailea";
            Text = "Jarraipen oharra";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblAzalpena;
        private TextBox txtOharra;
        private Button btnOharrikGabe;
        private Button btnGordeOharra;
    }
}