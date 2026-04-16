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
            chkBajan = new CheckBox();
            chkAltan = new CheckBox();
            chkPazienteGuztiak = new CheckBox();
            btnPazienteBerria = new Button();
            btnOsasunLangileaSortu = new Button();
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
            // 
            // _atzeraBotoia
            // 
            _atzeraBotoia.FlatAppearance.BorderSize = 0;
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
            dgvPazienteak.Location = new Point(2, 495);
            dgvPazienteak.Margin = new Padding(6);
            dgvPazienteak.MultiSelect = false;
            dgvPazienteak.Name = "dgvPazienteak";
            dgvPazienteak.ReadOnly = true;
            dgvPazienteak.RowHeadersVisible = false;
            dgvPazienteak.RowHeadersWidth = 82;
            dgvPazienteak.RowTemplate.Height = 64;
            dgvPazienteak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPazienteak.Size = new Size(1898, 897);
            dgvPazienteak.TabIndex = 1;
            dgvPazienteak.CellContentClick += dgvPazienteak_CellContentClick;
            // 
            // lblIzenburua
            // 
            lblIzenburua.BackColor = Color.Transparent;
            lblIzenburua.Dock = DockStyle.Top;
            lblIzenburua.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblIzenburua.ForeColor = Color.White;
            lblIzenburua.Location = new Point(2, 2);
            lblIzenburua.Margin = new Padding(6, 0, 6, 0);
            lblIzenburua.Name = "lblIzenburua";
            lblIzenburua.Size = new Size(1898, 94);
            lblIzenburua.TabIndex = 0;
            lblIzenburua.Text = "NIRE PAZIENTEAK";
            lblIzenburua.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlBilatzailea
            // 
            pnlBilatzailea.BackColor = Color.Transparent;
            pnlBilatzailea.Controls.Add(chkBajan);
            pnlBilatzailea.Controls.Add(chkAltan);
            pnlBilatzailea.Controls.Add(chkPazienteGuztiak);
            pnlBilatzailea.Controls.Add(btnPazienteBerria);
            pnlBilatzailea.Controls.Add(btnOsasunLangileaSortu);
            pnlBilatzailea.Controls.Add(txtBilatu);
            pnlBilatzailea.Controls.Add(lblBilatu);
            pnlBilatzailea.Dock = DockStyle.Top;
            pnlBilatzailea.Location = new Point(2, 96);
            pnlBilatzailea.Margin = new Padding(6);
            pnlBilatzailea.Name = "pnlBilatzailea";
            pnlBilatzailea.Size = new Size(1898, 399);
            pnlBilatzailea.TabIndex = 2;
            //
            // chkBajan
            //
            chkBajan.AutoSize = true;
            chkBajan.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            chkBajan.ForeColor = Color.White;
            chkBajan.Location = new Point(1570, 38);
            chkBajan.Name = "chkBajan";
            chkBajan.Size = new Size(122, 42);
            chkBajan.TabIndex = 4;
            chkBajan.Text = "Bajan";
            chkBajan.UseVisualStyleBackColor = true;
            //
            // chkAltan
            //
            chkAltan.AutoSize = true;
            chkAltan.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            chkAltan.ForeColor = Color.White;
            chkAltan.Location = new Point(1444, 37);
            chkAltan.Name = "chkAltan";
            chkAltan.Size = new Size(120, 42);
            chkAltan.TabIndex = 3;
            chkAltan.Text = "Altan";
            chkAltan.UseVisualStyleBackColor = true;
            //
            // chkPazienteGuztiak
            //
            chkPazienteGuztiak.AutoSize = true;
            chkPazienteGuztiak.Checked = true;
            chkPazienteGuztiak.CheckState = CheckState.Checked;
            chkPazienteGuztiak.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            chkPazienteGuztiak.ForeColor = Color.White;
            chkPazienteGuztiak.Location = new Point(1105, 37);
            chkPazienteGuztiak.Name = "chkPazienteGuztiak";
            chkPazienteGuztiak.Size = new Size(333, 42);
            chkPazienteGuztiak.TabIndex = 2;
            chkPazienteGuztiak.Text = "Paziente guztiak ikusi";
            chkPazienteGuztiak.UseVisualStyleBackColor = true;
            //
            // btnPazienteBerria
            //
            btnPazienteBerria.BackColor = Color.FromArgb(52, 152, 219);
            btnPazienteBerria.FlatAppearance.BorderSize = 0;
            btnPazienteBerria.FlatStyle = FlatStyle.Flat;
            btnPazienteBerria.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnPazienteBerria.ForeColor = Color.White;
            btnPazienteBerria.Location = new Point(136, 102);
            btnPazienteBerria.Margin = new Padding(6);
            btnPazienteBerria.Name = "btnPazienteBerria";
            btnPazienteBerria.Size = new Size(394, 56);
            btnPazienteBerria.TabIndex = 5;
            btnPazienteBerria.Text = "Paziente berria gehitu";
            btnPazienteBerria.UseVisualStyleBackColor = false;
            //
            // btnOsasunLangileaSortu
            //
            btnOsasunLangileaSortu.BackColor = Color.FromArgb(22, 160, 133);
            btnOsasunLangileaSortu.FlatAppearance.BorderSize = 0;
            btnOsasunLangileaSortu.FlatStyle = FlatStyle.Flat;
            btnOsasunLangileaSortu.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnOsasunLangileaSortu.ForeColor = Color.White;
            btnOsasunLangileaSortu.Location = new Point(548, 102);
            btnOsasunLangileaSortu.Margin = new Padding(6);
            btnOsasunLangileaSortu.Name = "btnOsasunLangileaSortu";
            btnOsasunLangileaSortu.Size = new Size(403, 56);
            btnOsasunLangileaSortu.TabIndex = 6;
            btnOsasunLangileaSortu.Text = "Osasun-langilea sortu";
            btnOsasunLangileaSortu.UseVisualStyleBackColor = false;
            btnOsasunLangileaSortu.Visible = true;
            //
            // txtBilatu
            //
            txtBilatu.Font = new Font("Segoe UI", 12F);
            txtBilatu.Location = new Point(136, 30);
            txtBilatu.Margin = new Padding(6);
            txtBilatu.Name = "txtBilatu";
            txtBilatu.PlaceholderText = "Bilatu izena, abizena edo NAN...";
            txtBilatu.Size = new Size(923, 50);
            txtBilatu.TabIndex = 1;
            //
            // lblBilatu
            //
            lblBilatu.AutoSize = true;
            lblBilatu.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblBilatu.ForeColor = Color.White;
            lblBilatu.Location = new Point(21, 30);
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
            ClientSize = new Size(1902, 1575);
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
        private System.Windows.Forms.CheckBox chkBajan;
        private System.Windows.Forms.CheckBox chkAltan;
        private System.Windows.Forms.CheckBox chkPazienteGuztiak;
        private System.Windows.Forms.Button btnPazienteBerria;
        private System.Windows.Forms.Button btnOsasunLangileaSortu;
    }
}
