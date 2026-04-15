namespace GOsasun_app.Interfazea
{
    partial class PazienteenZerrenda
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            dgvPazienteak = new DataGridView();
            lblIzenburua = new Label();
            pnlBilatzailea = new Panel();
            txtBilatu = new TextBox();
            lblBilatu = new Label();
            _edukiPanela.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPazienteak).BeginInit();
            pnlBilatzailea.SuspendLayout();
            SuspendLayout();
            // 
            // _edukiPanela
            // 
            _edukiPanela.Controls.Add(dgvPazienteak);
            _edukiPanela.Controls.Add(pnlBilatzailea);
            _edukiPanela.Controls.Add(lblIzenburua);
            _edukiPanela.Size = new Size(1902, 1099);
            // 
            // _goiburuBarra
            // 
            _goiburuBarra.Size = new Size(1902, 181);
            // 
            // dgvPazienteak
            // 
            dgvPazienteak.AllowUserToAddRows = false;
            dgvPazienteak.AllowUserToDeleteRows = false;
            dgvPazienteak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPazienteak.BackgroundColor = Color.White;
            dgvPazienteak.BorderStyle = BorderStyle.None;
            dgvPazienteak.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvPazienteak.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(52, 73, 94);
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvPazienteak.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvPazienteak.ColumnHeadersHeight = 50;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 11F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(50, 50, 50);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(236, 240, 241);
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvPazienteak.DefaultCellStyle = dataGridViewCellStyle2;
            dgvPazienteak.Dock = DockStyle.Fill;
            dgvPazienteak.EnableHeadersVisualStyles = false;
            dgvPazienteak.Location = new Point(4, 239);
            dgvPazienteak.Margin = new Padding(6, 6, 6, 6);
            dgvPazienteak.MultiSelect = false;
            dgvPazienteak.Name = "dgvPazienteak";
            dgvPazienteak.ReadOnly = true;
            dgvPazienteak.RowHeadersVisible = false;
            dgvPazienteak.RowHeadersWidth = 82;
            dgvPazienteak.RowTemplate.Height = 45;
            dgvPazienteak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPazienteak.Size = new Size(1894, 856);
            dgvPazienteak.TabIndex = 1;
            dgvPazienteak.CellContentClick += dgvPazienteak_CellContentClick;
            // 
            // lblIzenburua
            // 
            lblIzenburua.BackColor = Color.Transparent;
            lblIzenburua.Dock = DockStyle.Top;
            lblIzenburua.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblIzenburua.ForeColor = Color.White;
            lblIzenburua.Location = new Point(4, 4);
            lblIzenburua.Margin = new Padding(6, 0, 6, 0);
            lblIzenburua.Name = "lblIzenburua";
            lblIzenburua.Size = new Size(1894, 128);
            lblIzenburua.TabIndex = 0;
            lblIzenburua.Text = "NIRE PAZIENTEAK";
            lblIzenburua.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlBilatzailea
            // 
            pnlBilatzailea.BackColor = Color.Transparent;
            pnlBilatzailea.Controls.Add(txtBilatu);
            pnlBilatzailea.Controls.Add(lblBilatu);
            pnlBilatzailea.Dock = DockStyle.Top;
            pnlBilatzailea.Location = new Point(4, 132);
            pnlBilatzailea.Margin = new Padding(6, 6, 6, 6);
            pnlBilatzailea.Name = "pnlBilatzailea";
            pnlBilatzailea.Size = new Size(1894, 107);
            pnlBilatzailea.TabIndex = 2;
            // 
            // txtBilatu
            // 
            txtBilatu.Font = new Font("Segoe UI", 12F);
            txtBilatu.Location = new Point(279, 21);
            txtBilatu.Margin = new Padding(6, 6, 6, 6);
            txtBilatu.Name = "txtBilatu";
            txtBilatu.PlaceholderText = "Bilatu izena, abizena edo NAN...";
            txtBilatu.Size = new Size(739, 50);
            txtBilatu.TabIndex = 1;
            // 
            // lblBilatu
            // 
            lblBilatu.AutoSize = true;
            lblBilatu.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblBilatu.ForeColor = Color.White;
            lblBilatu.Location = new Point(111, 30);
            lblBilatu.Margin = new Padding(6, 0, 6, 0);
            lblBilatu.Name = "lblBilatu";
            lblBilatu.Size = new Size(116, 45);
            lblBilatu.TabIndex = 0;
            lblBilatu.Text = "Bilatu:";
            // 
            // PazienteenZerrenda
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1280);
            Margin = new Padding(11, 9, 11, 9);
            Name = "PazienteenZerrenda";
            Text = "GOsasun - Pazienteen Zerrenda";
            _edukiPanela.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPazienteak).EndInit();
            pnlBilatzailea.ResumeLayout(false);
            pnlBilatzailea.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPazienteak;
        private System.Windows.Forms.Label lblIzenburua;
        private System.Windows.Forms.Panel pnlBilatzailea;
        private System.Windows.Forms.TextBox txtBilatu;
        private System.Windows.Forms.Label lblBilatu;
    }
}
