namespace GOsasun_app.Interfazea
{
    partial class JarraipenOharrakLaguntzailea
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
            txtOharrak = new TextBox();
            btnUtzi = new Button();
            btnGorde = new Button();
            SuspendLayout();
            // 
            // txtOharrak
            // 
            txtOharrak.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtOharrak.Font = new Font("Segoe UI", 11F);
            txtOharrak.Location = new Point(14, 14);
            txtOharrak.Multiline = true;
            txtOharrak.Name = "txtOharrak";
            txtOharrak.ScrollBars = ScrollBars.Vertical;
            txtOharrak.Size = new Size(852, 466);
            txtOharrak.TabIndex = 0;
            // 
            // btnUtzi
            // 
            btnUtzi.DialogResult = DialogResult.Cancel;
            btnUtzi.Font = new Font("Segoe UI", 10.5F);
            btnUtzi.Location = new Point(554, 510);
            btnUtzi.Name = "btnUtzi";
            btnUtzi.Size = new Size(120, 46);
            btnUtzi.TabIndex = 1;
            btnUtzi.Text = "Utzi";
            btnUtzi.UseVisualStyleBackColor = true;
            // 
            // btnGorde
            // 
            btnGorde.BackColor = Color.FromArgb(41, 128, 185);
            btnGorde.FlatAppearance.BorderSize = 0;
            btnGorde.FlatStyle = FlatStyle.Flat;
            btnGorde.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnGorde.ForeColor = Color.White;
            btnGorde.Location = new Point(680, 510);
            btnGorde.Name = "btnGorde";
            btnGorde.Size = new Size(186, 46);
            btnGorde.TabIndex = 2;
            btnGorde.Text = "Gorde aldaketak";
            btnGorde.UseVisualStyleBackColor = false;
            // 
            // JarraipenOharrakLaguntzailea
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 600);
            Controls.Add(btnGorde);
            Controls.Add(btnUtzi);
            Controls.Add(txtOharrak);
            Name = "JarraipenOharrakLaguntzailea";
            Text = "Oharrak";
            ResumeLayout(false);
            PerformLayout();
        }

        private TextBox txtOharrak;
        private Button btnUtzi;
        private Button btnGorde;
    }
}