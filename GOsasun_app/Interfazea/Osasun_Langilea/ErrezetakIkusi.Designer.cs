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
            lblEgutegia = new Label();
            mcDataFiltroa = new MonthCalendar();
            btnGarbituFiltroak = new Button();
            pnlEskuina = new Panel();
            dgvErrezetak = new DataGridView();
            btnEditatu = new Button();
            btnEzabatu = new Button();
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
            _edukiPanela.Size = new Size(1902, 1263);
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
            pnlEzkerra.Controls.Add(lblEgutegia);
            pnlEzkerra.Controls.Add(mcDataFiltroa);
            pnlEzkerra.Controls.Add(btnGarbituFiltroak);
            pnlEzkerra.Location = new Point(50, 150);
            pnlEzkerra.Name = "pnlEzkerra";
            pnlEzkerra.Size = new Size(400, 1094);
            pnlEzkerra.TabIndex = 1;
            //
            // lblFiltroa
            //
            lblFiltroa.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblFiltroa.Location = new Point(20, 30);
            lblFiltroa.Name = "lblFiltroa";
            lblFiltroa.Size = new Size(360, 64);
            lblFiltroa.TabIndex = 0;
            lblFiltroa.Text = "Pazientea Bilatu (NAN, Izena):";
            //
            // txtBilatuPaz
            //
            txtBilatuPaz.Font = new Font("Segoe UI", 12F);
            txtBilatuPaz.Location = new Point(20, 118);
            txtBilatuPaz.Name = "txtBilatuPaz";
            txtBilatuPaz.Size = new Size(360, 50);
            txtBilatuPaz.TabIndex = 1;
            //
            // lblEgutegia
            //
            lblEgutegia.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblEgutegia.Location = new Point(20, 190);
            lblEgutegia.Name = "lblEgutegia";
            lblEgutegia.Size = new Size(360, 64);
            lblEgutegia.TabIndex = 2;
            lblEgutegia.Text = "Data Bidezko Filtroa:";
            //
            // mcDataFiltroa
            //
            mcDataFiltroa.Location = new Point(32, 278);
            mcDataFiltroa.Name = "mcDataFiltroa";
            mcDataFiltroa.TabIndex = 3;
            //
            // btnGarbituFiltroak
            //
            btnGarbituFiltroak.Location = new Point(20, 480);
            btnGarbituFiltroak.Name = "btnGarbituFiltroak";
            btnGarbituFiltroak.Size = new Size(360, 50);
            btnGarbituFiltroak.TabIndex = 4;
            btnGarbituFiltroak.Text = "FILTROAK GARBITU";
            //
            // pnlEskuina
            //
            pnlEskuina.BackColor = Color.White;
            pnlEskuina.Controls.Add(dgvErrezetak);
            pnlEskuina.Controls.Add(dgvBotikak);
            pnlEskuina.Controls.Add(btnEditatu);
            pnlEskuina.Controls.Add(btnEzabatu);
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
            // btnEditatu
            //
            btnEditatu.BackColor = Color.SteelBlue;
            btnEditatu.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnEditatu.ForeColor = Color.White;
            btnEditatu.Location = new Point(20, 900);
            btnEditatu.Name = "btnEditatu";
            btnEditatu.Size = new Size(300, 118);
            btnEditatu.TabIndex = 1;
            btnEditatu.Text = "ERREZETA EDITATU";
            btnEditatu.UseVisualStyleBackColor = false;
            //
            // btnEzabatu
            //
            btnEzabatu.BackColor = Color.IndianRed;
            btnEzabatu.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnEzabatu.ForeColor = Color.White;
            btnEzabatu.Location = new Point(340, 900);
            btnEzabatu.Name = "btnEzabatu";
            btnEzabatu.Size = new Size(300, 118);
            btnEzabatu.TabIndex = 2;
            btnEzabatu.Text = "ERREZETA EZABATU";
            btnEzabatu.UseVisualStyleBackColor = false;
            //
            // lblIzenburua
            //
            lblIzenburua.BackColor = Color.Transparent;
            lblIzenburua.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblIzenburua.ForeColor = Color.White;
            lblIzenburua.Location = new Point(0, 0);
            lblIzenburua.Name = "lblIzenburua";
            lblIzenburua.Size = new Size(1902, 120);
            lblIzenburua.TabIndex = 0;
            lblIzenburua.Text = "ERREZETEN ZERRENDA ETA KUDEAKETA";
            lblIzenburua.TextAlign = ContentAlignment.MiddleCenter;
            //
            // ErrezetakIkusi
            //
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1902, 1444);
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
        private System.Windows.Forms.Label lblEgutegia;
        private System.Windows.Forms.MonthCalendar mcDataFiltroa;
        private System.Windows.Forms.Button btnGarbituFiltroak;
        private System.Windows.Forms.Panel pnlEskuina;
        private System.Windows.Forms.DataGridView dgvErrezetak;
        private System.Windows.Forms.DataGridView dgvBotikak;
        private System.Windows.Forms.Button btnEditatu;
        private System.Windows.Forms.Button btnEzabatu;
        private System.Windows.Forms.Label lblIzenburua;
    }
}

