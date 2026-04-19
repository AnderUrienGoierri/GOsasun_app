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
            _edukiPanela.Size = new Size(1902, 762);
            // 
            // _goiburuBarra
            // 
            _goiburuBarra.Margin = new Padding(6);
            _goiburuBarra.Padding = new Padding(37, 21, 37, 21);
            _goiburuBarra.Size = new Size(1902, 293);
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
            lblIzenburua.Location = new Point(386, 10);
            lblIzenburua.Margin = new Padding(6, 0, 6, 0);
            lblIzenburua.Name = "lblIzenburua";
            lblIzenburua.Size = new Size(523, 54);
            lblIzenburua.TabIndex = 2;
            lblIzenburua.Text = "HITZORDUEN KUDEAKETA";
            // 
            // dgvHitzorduak
            // 
            dgvHitzorduak.AllowUserToAddRows = false;
            dgvHitzorduak.AllowUserToDeleteRows = false;
            dgvHitzorduak.BackgroundColor = Color.White;
            dgvHitzorduak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHitzorduak.Location = new Point(386, 70);
            dgvHitzorduak.Margin = new Padding(6);
            dgvHitzorduak.Name = "dgvHitzorduak";
            dgvHitzorduak.ReadOnly = true;
            dgvHitzorduak.RowHeadersVisible = false;
            dgvHitzorduak.RowHeadersWidth = 82;
            dgvHitzorduak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHitzorduak.Size = new Size(1230, 628);
            dgvHitzorduak.TabIndex = 3;
            dgvHitzorduak.CellContentClick += dgvHitzorduak_CellContentClick;
            // 
            // cmbPazienteak
            // 
            cmbPazienteak.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPazienteak.Location = new Point(6, 37);
            cmbPazienteak.Margin = new Padding(6);
            cmbPazienteak.Name = "cmbPazienteak";
            cmbPazienteak.Size = new Size(297, 28);
            cmbPazienteak.TabIndex = 1;
            // 
            // cmbMedikuak
            // 
            cmbMedikuak.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMedikuak.Location = new Point(6, 97);
            cmbMedikuak.Margin = new Padding(6);
            cmbMedikuak.Name = "cmbMedikuak";
            cmbMedikuak.Size = new Size(297, 28);
            cmbMedikuak.TabIndex = 3;
            // 
            // dtpData
            // 
            dtpData.Format = DateTimePickerFormat.Short;
            dtpData.Location = new Point(9, 173);
            dtpData.Margin = new Padding(6);
            dtpData.Name = "dtpData";
            dtpData.Size = new Size(217, 27);
            dtpData.TabIndex = 5;
            // 
            // dtpHasiera
            // 
            dtpHasiera.Format = DateTimePickerFormat.Time;
            dtpHasiera.Location = new Point(9, 305);
            dtpHasiera.Margin = new Padding(6);
            dtpHasiera.Name = "dtpHasiera";
            dtpHasiera.ShowUpDown = true;
            dtpHasiera.Size = new Size(217, 27);
            dtpHasiera.TabIndex = 7;
            // 
            // dtpBukaera
            // 
            dtpBukaera.Format = DateTimePickerFormat.Time;
            dtpBukaera.Location = new Point(9, 248);
            dtpBukaera.Margin = new Padding(6);
            dtpBukaera.Name = "dtpBukaera";
            dtpBukaera.ShowUpDown = true;
            dtpBukaera.Size = new Size(217, 27);
            dtpBukaera.TabIndex = 9;
            dtpBukaera.ValueChanged += dtpBukaera_ValueChanged;
            // 
            // txtArrazoia
            // 
            txtArrazoia.Location = new Point(13, 432);
            txtArrazoia.Margin = new Padding(6);
            txtArrazoia.Multiline = true;
            txtArrazoia.Name = "txtArrazoia";
            txtArrazoia.Size = new Size(321, 88);
            txtArrazoia.TabIndex = 11;
            // 
            // cmbEgoera
            // 
            cmbEgoera.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEgoera.Items.AddRange(new object[] { "Zain", "Bukatuta", "Ezeztatuta" });
            cmbEgoera.Location = new Point(9, 369);
            cmbEgoera.Margin = new Padding(6);
            cmbEgoera.Name = "cmbEgoera";
            cmbEgoera.Size = new Size(294, 28);
            cmbEgoera.TabIndex = 13;
            // 
            // btnGorde
            // 
            btnGorde.BackColor = Color.FromArgb(83, 148, 117);
            btnGorde.ForeColor = Color.White;
            btnGorde.Location = new Point(13, 532);
            btnGorde.Margin = new Padding(6);
            btnGorde.Name = "btnGorde";
            btnGorde.Size = new Size(321, 52);
            btnGorde.TabIndex = 14;
            btnGorde.Text = "Hitzordua sortu";
            btnGorde.UseVisualStyleBackColor = false;
            // 
            // btnEzabatu
            // 
            btnEzabatu.BackColor = Color.IndianRed;
            btnEzabatu.ForeColor = Color.White;
            btnEzabatu.Location = new Point(13, 596);
            btnEzabatu.Margin = new Padding(6);
            btnEzabatu.Name = "btnEzabatu";
            btnEzabatu.Size = new Size(150, 46);
            btnEzabatu.TabIndex = 15;
            btnEzabatu.Text = "Ezabatu";
            btnEzabatu.UseVisualStyleBackColor = false;
            // 
            // btnGarbitu
            // 
            btnGarbitu.Location = new Point(184, 596);
            btnGarbitu.Margin = new Padding(6);
            btnGarbitu.Name = "btnGarbitu";
            btnGarbitu.Size = new Size(150, 46);
            btnGarbitu.TabIndex = 16;
            btnGarbitu.Text = "Garbitu Pantaila";
            // 
            // lblPazientea
            // 
            lblPazientea.Location = new Point(6, 8);
            lblPazientea.Margin = new Padding(6, 0, 6, 0);
            lblPazientea.Name = "lblPazientea";
            lblPazientea.Size = new Size(110, 27);
            lblPazientea.TabIndex = 0;
            lblPazientea.Text = "Pazientea:";
            // 
            // lblMedikua
            // 
            lblMedikua.Location = new Point(6, 69);
            lblMedikua.Margin = new Padding(6, 0, 6, 0);
            lblMedikua.Name = "lblMedikua";
            lblMedikua.Size = new Size(97, 24);
            lblMedikua.TabIndex = 2;
            lblMedikua.Text = "Medikua:";
            // 
            // lblData
            // 
            lblData.Location = new Point(6, 140);
            lblData.Margin = new Padding(6, 0, 6, 0);
            lblData.Name = "lblData";
            lblData.Size = new Size(93, 30);
            lblData.TabIndex = 4;
            lblData.Text = "Data:";
            // 
            // lblHasiera
            // 
            lblHasiera.Location = new Point(9, 218);
            lblHasiera.Margin = new Padding(6, 0, 6, 0);
            lblHasiera.Name = "lblHasiera";
            lblHasiera.Size = new Size(148, 24);
            lblHasiera.TabIndex = 6;
            lblHasiera.Text = "Hasiera:";
            // 
            // lblBukaera
            // 
            lblBukaera.Location = new Point(9, 281);
            lblBukaera.Margin = new Padding(6, 0, 6, 0);
            lblBukaera.Name = "lblBukaera";
            lblBukaera.Size = new Size(181, 30);
            lblBukaera.TabIndex = 8;
            lblBukaera.Text = "Bukaera: (Aukerakoa):";
            lblBukaera.Click += lblBukaera_Click;
            // 
            // lblArrazoia
            // 
            lblArrazoia.Location = new Point(9, 403);
            lblArrazoia.Margin = new Padding(6, 0, 6, 0);
            lblArrazoia.Name = "lblArrazoia";
            lblArrazoia.Size = new Size(135, 23);
            lblArrazoia.TabIndex = 10;
            lblArrazoia.Text = "Arrazoia:";
            // 
            // lblEgoera
            // 
            lblEgoera.Location = new Point(9, 338);
            lblEgoera.Margin = new Padding(6, 0, 6, 0);
            lblEgoera.Name = "lblEgoera";
            lblEgoera.Size = new Size(120, 25);
            lblEgoera.TabIndex = 12;
            lblEgoera.Text = "Egoera:";
            // 
            // panelKudeaketa
            // 
            panelKudeaketa.AutoScroll = true;
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
            panelKudeaketa.Location = new Point(15, 10);
            panelKudeaketa.Margin = new Padding(6);
            panelKudeaketa.Name = "panelKudeaketa";
            panelKudeaketa.Size = new Size(364, 688);
            panelKudeaketa.TabIndex = 4;
            // 
            // HitzorduKudeaketa
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(43, 71, 92);
            ClientSize = new Size(1902, 1055);
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

