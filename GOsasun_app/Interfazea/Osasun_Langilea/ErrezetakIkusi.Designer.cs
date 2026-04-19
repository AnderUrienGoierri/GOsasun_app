using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace GOsasun_app.Interfazea
{
    partial class ErrezetakIkusi
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            pnlEzkerra = new Panel();
            lblFiltroa = new Label();
            txtBilatuPaz = new TextBox();
            chkPazienteGuztiak = new CheckBox();
            lblEgutegia = new Label();
            mcDataFiltroa = new MonthCalendar();
            chkErrezetaAktiboak = new CheckBox();
            btnGarbituFiltroak = new Button();
            btnEditatu = new Button();
            btnEzabatu = new Button();
            pnlEskuina = new Panel();
            dgvErrezetak = new DataGridView();
            dgvBotikak = new DataGridView();
            lblIzenburua = new Label();
            _edukiPanela.SuspendLayout();
            pnlEzkerra.SuspendLayout();
            pnlEskuina.SuspendLayout();
            ((ISupportInitialize)dgvErrezetak).BeginInit();
            ((ISupportInitialize)dgvBotikak).BeginInit();
            SuspendLayout();
            // 
            // _edukiPanela
            // 
            _edukiPanela.Controls.Add(lblIzenburua);
            _edukiPanela.Controls.Add(pnlEzkerra);
            _edukiPanela.Controls.Add(pnlEskuina);
            // 
            // _atzeraBotoia
            // 
            _atzeraBotoia.FlatAppearance.BorderSize = 0;
            // 
            // pnlEzkerra
            // 
            pnlEzkerra.BackColor = Color.White;
            pnlEzkerra.Controls.Add(lblFiltroa);
            pnlEzkerra.Controls.Add(txtBilatuPaz);
            pnlEzkerra.Controls.Add(chkPazienteGuztiak);
            pnlEzkerra.Controls.Add(lblEgutegia);
            pnlEzkerra.Controls.Add(mcDataFiltroa);
            pnlEzkerra.Controls.Add(chkErrezetaAktiboak);
            pnlEzkerra.Controls.Add(btnGarbituFiltroak);
            pnlEzkerra.Controls.Add(btnEditatu);
            pnlEzkerra.Controls.Add(btnEzabatu);
            pnlEzkerra.Location = new Point(50, 150);
            pnlEzkerra.Name = "pnlEzkerra";
            pnlEzkerra.Size = new Size(400, 1094);
            pnlEzkerra.TabIndex = 1;
            // 
            // lblFiltroa
            // 
            lblFiltroa.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblFiltroa.Location = new Point(20, 0);
            lblFiltroa.Name = "lblFiltroa";
            lblFiltroa.Size = new Size(360, 37);
            lblFiltroa.TabIndex = 0;
            lblFiltroa.Text = "Pazientea Bilatu (NAN, Izena):";
            // 
            // txtBilatuPaz
            // 
            txtBilatuPaz.Font = new Font("Segoe UI", 12F);
            txtBilatuPaz.Location = new Point(20, 40);
            txtBilatuPaz.Name = "txtBilatuPaz";
            txtBilatuPaz.Size = new Size(360, 50);
            txtBilatuPaz.TabIndex = 1;
            txtBilatuPaz.TextChanged += txtBilatuPaz_TextChanged_1;
            // 
            // chkPazienteGuztiak
            // 
            chkPazienteGuztiak.AutoSize = true;
            chkPazienteGuztiak.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            chkPazienteGuztiak.ForeColor = Color.FromArgb(44, 62, 80);
            chkPazienteGuztiak.Location = new Point(20, 96);
            chkPazienteGuztiak.Name = "chkPazienteGuztiak";
            chkPazienteGuztiak.Size = new Size(264, 42);
            chkPazienteGuztiak.TabIndex = 2;
            chkPazienteGuztiak.Text = "Paziente guztiak";
            chkPazienteGuztiak.UseVisualStyleBackColor = true;
            // 
            // lblEgutegia
            // 
            lblEgutegia.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblEgutegia.Location = new Point(30, 198);
            lblEgutegia.Name = "lblEgutegia";
            lblEgutegia.Size = new Size(350, 47);
            lblEgutegia.TabIndex = 3;
            lblEgutegia.Text = "Data Bidezko Filtroa:";
            // 
            // mcDataFiltroa
            // 
            mcDataFiltroa.Location = new Point(30, 254);
            mcDataFiltroa.Name = "mcDataFiltroa";
            mcDataFiltroa.TabIndex = 4;
            // 
            // chkErrezetaAktiboak
            // 
            chkErrezetaAktiboak.AutoSize = true;
            chkErrezetaAktiboak.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            chkErrezetaAktiboak.ForeColor = Color.FromArgb(44, 62, 80);
            chkErrezetaAktiboak.Location = new Point(20, 490);
            chkErrezetaAktiboak.Name = "chkErrezetaAktiboak";
            chkErrezetaAktiboak.Size = new Size(255, 42);
            chkErrezetaAktiboak.TabIndex = 5;
            chkErrezetaAktiboak.Text = "Errezeta aktiboak";
            chkErrezetaAktiboak.UseVisualStyleBackColor = true;
            // 
            // btnGarbituFiltroak
            // 
            btnGarbituFiltroak.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnGarbituFiltroak.Location = new Point(20, 540);
            btnGarbituFiltroak.Name = "btnGarbituFiltroak";
            btnGarbituFiltroak.Size = new Size(360, 50);
            btnGarbituFiltroak.TabIndex = 6;
            btnGarbituFiltroak.Text = "FILTROAK GARBITU";
            // 
            // btnEditatu
            // 
            btnEditatu.BackColor = Color.SteelBlue;
            btnEditatu.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnEditatu.ForeColor = Color.White;
            btnEditatu.Location = new Point(20, 610);
            btnEditatu.Name = "btnEditatu";
            btnEditatu.Size = new Size(360, 151);
            btnEditatu.TabIndex = 7;
            btnEditatu.Text = "ERREZETA EDITATU";
            btnEditatu.UseVisualStyleBackColor = false;
            // 
            // btnEzabatu
            // 
            btnEzabatu.BackColor = Color.IndianRed;
            btnEzabatu.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnEzabatu.ForeColor = Color.White;
            btnEzabatu.Location = new Point(20, 777);
            btnEzabatu.Name = "btnEzabatu";
            btnEzabatu.Size = new Size(360, 136);
            btnEzabatu.TabIndex = 8;
            btnEzabatu.Text = "ERREZETA EZABATU";
            btnEzabatu.UseVisualStyleBackColor = false;
            // 
            // pnlEskuina
            // 
            pnlEskuina.BackColor = Color.White;
            pnlEskuina.Controls.Add(dgvErrezetak);
            pnlEskuina.Controls.Add(dgvBotikak);
            pnlEskuina.Location = new Point(480, 150);
            pnlEskuina.Name = "pnlEskuina";
            pnlEskuina.Size = new Size(1410, 1094);
            pnlEskuina.TabIndex = 2;
            // 
            // dgvErrezetak
            // 
            dgvErrezetak.AllowUserToAddRows = false;
            dgvErrezetak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvErrezetak.ColumnHeadersHeight = 46;
            dgvErrezetak.Location = new Point(20, 22);
            dgvErrezetak.MultiSelect = false;
            dgvErrezetak.Name = "dgvErrezetak";
            dgvErrezetak.ReadOnly = true;
            dgvErrezetak.RowHeadersWidth = 82;
            dgvErrezetak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvErrezetak.Size = new Size(1368, 500);
            dgvErrezetak.TabIndex = 0;
            // 
            // dgvBotikak
            // 
            dgvBotikak.AllowUserToAddRows = false;
            dgvBotikak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBotikak.ColumnHeadersHeight = 46;
            dgvBotikak.Location = new Point(20, 540);
            dgvBotikak.MultiSelect = false;
            dgvBotikak.Name = "dgvBotikak";
            dgvBotikak.ReadOnly = true;
            dgvBotikak.RowHeadersWidth = 82;
            dgvBotikak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBotikak.Size = new Size(1368, 330);
            dgvBotikak.TabIndex = 4;
            // 
            // lblIzenburua
            // 
            lblIzenburua.BackColor = Color.Transparent;
            lblIzenburua.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblIzenburua.ForeColor = Color.White;
            lblIzenburua.Location = new Point(0, 0);
            lblIzenburua.Name = "lblIzenburua";
            lblIzenburua.Padding = new Padding(48, 0, 0, 0);
            lblIzenburua.Size = new Size(1902, 120);
            lblIzenburua.TabIndex = 0;
            lblIzenburua.Text = "ERREZETEN ZERRENDA";
            lblIzenburua.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ErrezetakIkusi
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1902, 1575);
            Name = "ErrezetakIkusi";
            Text = "GOsasun - Errezetak Ikusi";
            _edukiPanela.ResumeLayout(false);
            pnlEzkerra.ResumeLayout(false);
            pnlEzkerra.PerformLayout();
            pnlEskuina.ResumeLayout(false);
            ((ISupportInitialize)dgvErrezetak).EndInit();
            ((ISupportInitialize)dgvBotikak).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlEzkerra;
        private System.Windows.Forms.Label lblFiltroa;
        private System.Windows.Forms.TextBox txtBilatuPaz;
        private System.Windows.Forms.CheckBox chkPazienteGuztiak;
        private System.Windows.Forms.Label lblEgutegia;
        private System.Windows.Forms.MonthCalendar mcDataFiltroa;
        private System.Windows.Forms.CheckBox chkErrezetaAktiboak;
        private System.Windows.Forms.Button btnGarbituFiltroak;
        private System.Windows.Forms.Panel pnlEskuina;
        private System.Windows.Forms.DataGridView dgvErrezetak;
        private System.Windows.Forms.DataGridView dgvBotikak;
        private System.Windows.Forms.Button btnEditatu;
        private System.Windows.Forms.Button btnEzabatu;
        private System.Windows.Forms.Label lblIzenburua;
    }
}

