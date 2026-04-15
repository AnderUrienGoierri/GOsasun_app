namespace GOsasun_app.Interfazea
{
    partial class Jarraipenak
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            _lblIzenburua = new Label();
            _btnJarraipenBerria = new Button();
            _lblBilatu = new Label();
            _txtBilatu = new TextBox();
            _dgvJarraipenak = new DataGridView();
            _edukiPanela.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgvJarraipenak).BeginInit();
            SuspendLayout();
            // 
            // _edukiPanela
            // 
            _edukiPanela.Controls.Add(_dgvJarraipenak);
            _edukiPanela.Controls.Add(_txtBilatu);
            _edukiPanela.Controls.Add(_lblBilatu);
            _edukiPanela.Controls.Add(_btnJarraipenBerria);
            _edukiPanela.Controls.Add(_lblIzenburua);
            _edukiPanela.Location = new Point(0, 181);
            _edukiPanela.Margin = new Padding(6);
            _edukiPanela.Padding = new Padding(4);
            _edukiPanela.Size = new Size(2700, 1213);
            // 
            // _goiburuBarra
            // 
            _goiburuBarra.Margin = new Padding(6);
            _goiburuBarra.Padding = new Padding(37, 21, 37, 21);
            _goiburuBarra.Size = new Size(2700, 181);
            // 
            // _atzeraBotoia
            // 
            _atzeraBotoia.FlatAppearance.BorderSize = 0;
            _atzeraBotoia.Location = new Point(40, 93);
            _atzeraBotoia.Margin = new Padding(6);
            _atzeraBotoia.Size = new Size(250, 59);
            // 
            // _lblIzenburua
            // 
            _lblIzenburua.AutoSize = true;
            _lblIzenburua.BackColor = Color.Transparent;
            _lblIzenburua.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            _lblIzenburua.ForeColor = Color.FromArgb(44, 62, 80);
            _lblIzenburua.Location = new Point(111, 85);
            _lblIzenburua.Margin = new Padding(6, 0, 6, 0);
            _lblIzenburua.Name = "_lblIzenburua";
            _lblIzenburua.Size = new Size(510, 93);
            _lblIzenburua.TabIndex = 0;
            _lblIzenburua.Text = "JARRAIPENAK";
            // 
            // _btnJarraipenBerria
            // 
            _btnJarraipenBerria.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnJarraipenBerria.BackColor = Color.FromArgb(192, 57, 43);
            _btnJarraipenBerria.FlatAppearance.BorderSize = 0;
            _btnJarraipenBerria.FlatStyle = FlatStyle.Flat;
            _btnJarraipenBerria.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            _btnJarraipenBerria.ForeColor = Color.White;
            _btnJarraipenBerria.Location = new Point(2310, 32);
            _btnJarraipenBerria.Margin = new Padding(6);
            _btnJarraipenBerria.Name = "_btnJarraipenBerria";
            _btnJarraipenBerria.Size = new Size(320, 64);
            _btnJarraipenBerria.TabIndex = 1;
            _btnJarraipenBerria.Text = "Jarraipen berria";
            _btnJarraipenBerria.UseVisualStyleBackColor = false;
            // 
            // _lblBilatu
            // 
            _lblBilatu.AutoSize = true;
            _lblBilatu.BackColor = Color.Transparent;
            _lblBilatu.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            _lblBilatu.ForeColor = Color.FromArgb(44, 62, 80);
            _lblBilatu.Location = new Point(70, 115);
            _lblBilatu.Margin = new Padding(6, 0, 6, 0);
            _lblBilatu.Name = "_lblBilatu";
            _lblBilatu.Size = new Size(741, 45);
            _lblBilatu.TabIndex = 2;
            _lblBilatu.Text = "Bilatu pazientea (abizenak, izena edo NAN/DNI)";
            // 
            // _txtBilatu
            // 
            _txtBilatu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _txtBilatu.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _txtBilatu.Location = new Point(70, 160);
            _txtBilatu.Margin = new Padding(6);
            _txtBilatu.MinimumSize = new Size(900, 50);
            _txtBilatu.Name = "_txtBilatu";
            _txtBilatu.PlaceholderText = "Adibidez: Urrutia, Jon edo 000000001";
            _txtBilatu.Size = new Size(2560, 50);
            _txtBilatu.TabIndex = 3;
            // 
            // _dgvJarraipenak
            // 
            _dgvJarraipenak.AllowUserToAddRows = false;
            _dgvJarraipenak.AllowUserToDeleteRows = false;
            _dgvJarraipenak.AllowUserToResizeRows = false;
            _dgvJarraipenak.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _dgvJarraipenak.BackgroundColor = Color.White;
            _dgvJarraipenak.BorderStyle = BorderStyle.None;
            _dgvJarraipenak.ColumnHeadersHeight = 54;
            _dgvJarraipenak.EnableHeadersVisualStyles = false;
            _dgvJarraipenak.GridColor = Color.FromArgb(224, 224, 224);
            _dgvJarraipenak.Location = new Point(70, 285);
            _dgvJarraipenak.Margin = new Padding(6);
            _dgvJarraipenak.MultiSelect = false;
            _dgvJarraipenak.Name = "_dgvJarraipenak";
            _dgvJarraipenak.ReadOnly = true;
            _dgvJarraipenak.RowHeadersVisible = false;
            _dgvJarraipenak.RowHeadersWidth = 82;
            _dgvJarraipenak.RowTemplate.Height = 128;
            _dgvJarraipenak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgvJarraipenak.Size = new Size(2560, 900);
            _dgvJarraipenak.TabIndex = 4;
            // 
            // Jarraipenak
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2700, 1394);
            Margin = new Padding(11, 9, 11, 9);
            Name = "Jarraipenak";
            Text = "GOsasun - Jarraipenak";
            _edukiPanela.ResumeLayout(false);
            _edukiPanela.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_dgvJarraipenak).EndInit();
            ResumeLayout(false);
        }

        private Label _lblIzenburua;
        private Button _btnJarraipenBerria;
        private Label _lblBilatu;
        private TextBox _txtBilatu;
        private DataGridView _dgvJarraipenak;
    }
}
