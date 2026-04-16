using System;
using System.Drawing;
using System.Windows.Forms;

namespace GOsasun_app.Interfazea
{
    partial class HitzorduKudeaketa
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
            lblIzenburua = new Label();
            dgvHitzorduak = new DataGridView();
            cmbPazienteak = new ComboBox();
            cmbMedikuak = new ComboBox();
            dtpData = new DateTimePicker();
            dtpHasiera = new DateTimePicker();
            dtpBukaera = new DateTimePicker();
            txtArrazoia = new TextBox();
            cmbEgoera = new ComboBox();
            btnGorde = new Button();
            btnEzabatu = new Button();
            btnGarbitu = new Button();
            lblPazientea = new Label();
            lblMedikua = new Label();
            lblData = new Label();
            lblHasiera = new Label();
            lblBukaera = new Label();
            lblArrazoia = new Label();
            lblEgoera = new Label();
            panelKudeaketa = new Panel();
            _edukiPanela.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHitzorduak).BeginInit();
            panelKudeaketa.SuspendLayout();
            SuspendLayout();
            //
            // _edukiPanela
            //
            _edukiPanela.Controls.Add(panelKudeaketa);
            _edukiPanela.Controls.Add(dgvHitzorduak);
            _edukiPanela.Controls.Add(lblIzenburua);
            _edukiPanela.Location = new Point(0, 293);
            _edukiPanela.Margin = new Padding(6);
            _edukiPanela.Padding = new Padding(4);
            _edukiPanela.Size = new Size(2199, 1299);
            _edukiPanela.Paint += _edukiPanela_Paint;
            //
            // _goiburuBarra
            //
            _goiburuBarra.Margin = new Padding(6);
            _goiburuBarra.Padding = new Padding(37, 21, 37, 21);
            _goiburuBarra.Size = new Size(2199, 293);
            //
            // _atzeraBotoia
            //
            _atzeraBotoia.FlatAppearance.BorderSize = 0;
            _atzeraBotoia.Location = new Point(74, 198);
            _atzeraBotoia.Margin = new Padding(6);
            _atzeraBotoia.Size = new Size(385, 73);
            //
            // lblIzenburua
            //
            lblIzenburua.AutoSize = true;
            lblIzenburua.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblIzenburua.ForeColor = Color.White;
            lblIzenburua.Location = new Point(631, 107);
            lblIzenburua.Margin = new Padding(6, 0, 6, 0);
            lblIzenburua.Name = "lblIzenburua";
            lblIzenburua.Size = new Size(841, 86);
            lblIzenburua.TabIndex = 2;
            lblIzenburua.Text = "HITZORDUEN KUDEAKETA";
            //
            // dgvHitzorduak
            //
            dgvHitzorduak.AllowUserToAddRows = false;
            dgvHitzorduak.AllowUserToDeleteRows = false;
            dgvHitzorduak.BackgroundColor = Color.White;
            dgvHitzorduak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHitzorduak.Location = new Point(81, 256);
            dgvHitzorduak.Margin = new Padding(6);
            dgvHitzorduak.Name = "dgvHitzorduak";
            dgvHitzorduak.ReadOnly = true;
            dgvHitzorduak.RowHeadersVisible = false;
            dgvHitzorduak.RowHeadersWidth = 82;
            dgvHitzorduak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHitzorduak.Size = new Size(1300, 1024);
            dgvHitzorduak.TabIndex = 3;
            //
            // cmbPazienteak
            //
            cmbPazienteak.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPazienteak.Location = new Point(37, 85);
            cmbPazienteak.Margin = new Padding(6);
            cmbPazienteak.Name = "cmbPazienteak";
            cmbPazienteak.Size = new Size(628, 40);
            cmbPazienteak.TabIndex = 1;
            //
            // cmbMedikuak
            //
            cmbMedikuak.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMedikuak.Location = new Point(37, 203);
            cmbMedikuak.Margin = new Padding(6);
            cmbMedikuak.Name = "cmbMedikuak";
            cmbMedikuak.Size = new Size(628, 40);
            cmbMedikuak.TabIndex = 3;
            //
            // dtpData
            //
            dtpData.Format = DateTimePickerFormat.Short;
            dtpData.Location = new Point(37, 320);
            dtpData.Margin = new Padding(6);
            dtpData.Name = "dtpData";
            dtpData.Size = new Size(294, 39);
            dtpData.TabIndex = 5;
            //
            // dtpHasiera
            //
            dtpHasiera.Format = DateTimePickerFormat.Time;
            dtpHasiera.Location = new Point(40, 493);
            dtpHasiera.Margin = new Padding(6);
            dtpHasiera.Name = "dtpHasiera";
            dtpHasiera.ShowUpDown = true;
            dtpHasiera.Size = new Size(294, 39);
            dtpHasiera.TabIndex = 7;
            //
            // dtpBukaera
            //
            dtpBukaera.Format = DateTimePickerFormat.Time;
            dtpBukaera.Location = new Point(40, 409);
            dtpBukaera.Margin = new Padding(6);
            dtpBukaera.Name = "dtpBukaera";
            dtpBukaera.ShowUpDown = true;
            dtpBukaera.Size = new Size(294, 39);
            dtpBukaera.TabIndex = 9;
            //
            // txtArrazoia
            //
            txtArrazoia.Location = new Point(28, 697);
            txtArrazoia.Margin = new Padding(6);
            txtArrazoia.Multiline = true;
            txtArrazoia.Name = "txtArrazoia";
            txtArrazoia.Size = new Size(628, 123);
            txtArrazoia.TabIndex = 11;
            //
            // cmbEgoera
            //
            cmbEgoera.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEgoera.Items.AddRange(new object[] { "Zain", "Bukatuta", "Ezeztatuta" });
            cmbEgoera.Location = new Point(37, 587);
            cmbEgoera.Margin = new Padding(6);
            cmbEgoera.Name = "cmbEgoera";
            cmbEgoera.Size = new Size(294, 40);
            cmbEgoera.TabIndex = 13;
            //
            // btnGorde
            //
            btnGorde.BackColor = Color.FromArgb(83, 148, 117);
            btnGorde.ForeColor = Color.White;
            btnGorde.Location = new Point(37, 832);
            btnGorde.Margin = new Padding(6);
            btnGorde.Name = "btnGorde";
            btnGorde.Size = new Size(631, 75);
            btnGorde.TabIndex = 14;
            btnGorde.Text = "Gorde / Sortu";
            btnGorde.UseVisualStyleBackColor = false;
            //
            // btnEzabatu
            //
            btnEzabatu.BackColor = Color.IndianRed;
            btnEzabatu.ForeColor = Color.White;
            btnEzabatu.Location = new Point(37, 928);
            btnEzabatu.Margin = new Padding(6);
            btnEzabatu.Name = "btnEzabatu";
            btnEzabatu.Size = new Size(297, 64);
            btnEzabatu.TabIndex = 15;
            btnEzabatu.Text = "Ezabatu";
            btnEzabatu.UseVisualStyleBackColor = false;
            //
            // btnGarbitu
            //
            btnGarbitu.Location = new Point(371, 928);
            btnGarbitu.Margin = new Padding(6);
            btnGarbitu.Name = "btnGarbitu";
            btnGarbitu.Size = new Size(297, 64);
            btnGarbitu.TabIndex = 16;
            btnGarbitu.Text = "Garbitu Pantaila";
            //
            // lblPazientea
            //
            lblPazientea.Location = new Point(37, 35);
            lblPazientea.Margin = new Padding(6, 0, 6, 0);
            lblPazientea.Name = "lblPazientea";
            lblPazientea.Size = new Size(186, 49);
            lblPazientea.TabIndex = 0;
            lblPazientea.Text = "Pazientea:";
            //
            // lblMedikua
            //
            lblMedikua.Location = new Point(37, 153);
            lblMedikua.Margin = new Padding(6, 0, 6, 0);
            lblMedikua.Name = "lblMedikua";
            lblMedikua.Size = new Size(186, 49);
            lblMedikua.TabIndex = 2;
            lblMedikua.Text = "Medikua:";
            //
            // lblData
            //
            lblData.Location = new Point(40, 275);
            lblData.Margin = new Padding(6, 0, 6, 0);
            lblData.Name = "lblData";
            lblData.Size = new Size(291, 39);
            lblData.TabIndex = 4;
            lblData.Text = "Data:";
            //
            // lblHasiera
            //
            lblHasiera.Location = new Point(37, 366);
            lblHasiera.Margin = new Padding(6, 0, 6, 0);
            lblHasiera.Name = "lblHasiera";
            lblHasiera.Size = new Size(294, 37);
            lblHasiera.TabIndex = 6;
            lblHasiera.Text = "Hasiera:";
            //
            // lblBukaera
            //
            lblBukaera.Location = new Point(37, 454);
            lblBukaera.Margin = new Padding(6, 0, 6, 0);
            lblBukaera.Name = "lblBukaera";
            lblBukaera.Size = new Size(297, 33);
            lblBukaera.TabIndex = 8;
            lblBukaera.Text = "Bukaera: (Aukerakoa):";
            lblBukaera.Click += lblBukaera_Click;
            //
            // lblArrazoia
            //
            lblArrazoia.Location = new Point(37, 648);
            lblArrazoia.Margin = new Padding(6, 0, 6, 0);
            lblArrazoia.Name = "lblArrazoia";
            lblArrazoia.Size = new Size(297, 43);
            lblArrazoia.TabIndex = 10;
            lblArrazoia.Text = "Arrazoia:";
            //
            // lblEgoera
            //
            lblEgoera.Location = new Point(37, 540);
            lblEgoera.Margin = new Padding(6, 0, 6, 0);
            lblEgoera.Name = "lblEgoera";
            lblEgoera.Size = new Size(297, 41);
            lblEgoera.TabIndex = 12;
            lblEgoera.Text = "Egoera:";
            //
            // panelKudeaketa
            //
            panelKudeaketa.BackColor = Color.White;
            panelKudeaketa.Controls.Add(lblPazientea);
            panelKudeaketa.Controls.Add(cmbPazienteak);
            panelKudeaketa.Controls.Add(lblMedikua);
            panelKudeaketa.Controls.Add(cmbMedikuak);
            panelKudeaketa.Controls.Add(lblData);
            panelKudeaketa.Controls.Add(dtpData);
            panelKudeaketa.Controls.Add(lblHasiera);
            panelKudeaketa.Controls.Add(dtpHasiera);
            panelKudeaketa.Controls.Add(lblBukaera);
            panelKudeaketa.Controls.Add(dtpBukaera);
            panelKudeaketa.Controls.Add(lblArrazoia);
            panelKudeaketa.Controls.Add(txtArrazoia);
            panelKudeaketa.Controls.Add(lblEgoera);
            panelKudeaketa.Controls.Add(cmbEgoera);
            panelKudeaketa.Controls.Add(btnGorde);
            panelKudeaketa.Controls.Add(btnEzabatu);
            panelKudeaketa.Controls.Add(btnGarbitu);
            panelKudeaketa.Location = new Point(1393, 256);
            panelKudeaketa.Margin = new Padding(6);
            panelKudeaketa.Name = "panelKudeaketa";
            panelKudeaketa.Size = new Size(706, 1024);
            panelKudeaketa.TabIndex = 4;
            // 
            // HitzorduKudeaketa
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 71, 92);
            ClientSize = new Size(2199, 1592);
            Margin = new Padding(11, 9, 11, 9);
            Name = "HitzorduKudeaketa";
            Text = "GOsasun - Hitzorduen Kudeaketa";
            _edukiPanela.ResumeLayout(false);
            _edukiPanela.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHitzorduak).EndInit();
            panelKudeaketa.ResumeLayout(false);
            panelKudeaketa.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblIzenburua;
        private System.Windows.Forms.DataGridView dgvHitzorduak;
        private System.Windows.Forms.ComboBox cmbPazienteak;
        private System.Windows.Forms.ComboBox cmbMedikuak;
        private System.Windows.Forms.DateTimePicker dtpData;
        private System.Windows.Forms.DateTimePicker dtpHasiera;
        private System.Windows.Forms.DateTimePicker dtpBukaera;
        private System.Windows.Forms.TextBox txtArrazoia;
        private System.Windows.Forms.ComboBox cmbEgoera;
        private System.Windows.Forms.Button btnGorde;
        private System.Windows.Forms.Button btnEzabatu;
        private System.Windows.Forms.Button btnGarbitu;
        private System.Windows.Forms.Label lblPazientea;
        private System.Windows.Forms.Label lblMedikua;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.Label lblHasiera;
        private System.Windows.Forms.Label lblBukaera;
        private System.Windows.Forms.Label lblArrazoia;
        private System.Windows.Forms.Label lblEgoera;
        private System.Windows.Forms.Panel panelKudeaketa;
    }
}
