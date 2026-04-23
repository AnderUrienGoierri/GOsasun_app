using System;
using System.Drawing;
using System.Windows.Forms;

namespace GOsasun_app.Interfazea
{
    partial class HitzorduakKontsultatzea
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
            calEgutegia = new MonthCalendar();
            dgvHitzorduak = new DataGridView();
            lblBilatuPazientea = new Label();
            txtPazienteBilatu = new TextBox();
            chkPazienteGuztiak = new CheckBox();
            _edukiPanela.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHitzorduak).BeginInit();
            SuspendLayout();
            // 
            // _edukiPanela
            // 
            _edukiPanela.Controls.Add(dgvHitzorduak);
            _edukiPanela.Controls.Add(chkPazienteGuztiak);
            _edukiPanela.Controls.Add(txtPazienteBilatu);
            _edukiPanela.Controls.Add(lblBilatuPazientea);
            _edukiPanela.Controls.Add(calEgutegia);
            _edukiPanela.Controls.Add(lblIzenburua);
            // 
            // _goiburuBarra
            // 
            _goiburuBarra.Paint += _goiburuBarra_Paint;
            // 
            // _atzeraBotoia
            // 
            _atzeraBotoia.FlatAppearance.BorderSize = 0;
            // 
            // lblIzenburua
            // 
            lblIzenburua.AutoSize = true;
            lblIzenburua.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            lblIzenburua.ForeColor = Color.White;
            lblIzenburua.Location = new Point(41, 8);
            lblIzenburua.Name = "lblIzenburua";
            lblIzenburua.Size = new Size(1162, 128);
            lblIzenburua.TabIndex = 2;
            lblIzenburua.Text = "HITZORDUEN EGUTEGIA";
            lblIzenburua.Click += lblIzenburua_Click;
            // 
            // calEgutegia
            // 
            calEgutegia.Location = new Point(41, 300);
            calEgutegia.Name = "calEgutegia";
            calEgutegia.TabIndex = 3;
            // 
            // dgvHitzorduak
            // 
            dgvHitzorduak.AllowUserToAddRows = false;
            dgvHitzorduak.AllowUserToDeleteRows = false;
            dgvHitzorduak.BackgroundColor = Color.White;
            dgvHitzorduak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHitzorduak.Location = new Point(385, 202);
            dgvHitzorduak.Name = "dgvHitzorduak";
            dgvHitzorduak.ReadOnly = true;
            dgvHitzorduak.RowHeadersVisible = false;
            dgvHitzorduak.RowHeadersWidth = 82;
            dgvHitzorduak.Size = new Size(1460, 800);
            dgvHitzorduak.TabIndex = 5;
            // 
            // 
            // lblBilatuPazientea
            // 
            lblBilatuPazientea.AutoSize = true;
            lblBilatuPazientea.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBilatuPazientea.ForeColor = Color.FromArgb(44, 62, 80);
            lblBilatuPazientea.Location = new Point(40, 164);
            lblBilatuPazientea.Name = "lblBilatuPazientea";
            lblBilatuPazientea.Size = new Size(337, 37);
            lblBilatuPazientea.TabIndex = 8;
            lblBilatuPazientea.Text = "Bilatu pazientea edo DNI";
            lblBilatuPazientea.Click += lblBilatuPazientea_Click;
            // 
            // txtPazienteBilatu
            // 
            txtPazienteBilatu.Font = new Font("Segoe UI", 10F);
            txtPazienteBilatu.Location = new Point(40, 199);
            txtPazienteBilatu.Name = "txtPazienteBilatu";
            txtPazienteBilatu.PlaceholderText = "Abizena, izena edo NAN/DNI";
            txtPazienteBilatu.Size = new Size(325, 43);
            txtPazienteBilatu.TabIndex = 9;
            // 
            // chkPazienteGuztiak
            // 
            chkPazienteGuztiak.AutoSize = true;
            chkPazienteGuztiak.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            chkPazienteGuztiak.ForeColor = Color.FromArgb(44, 62, 80);
            chkPazienteGuztiak.Location = new Point(41, 242);
            chkPazienteGuztiak.Name = "chkPazienteGuztiak";
            chkPazienteGuztiak.Size = new Size(246, 40);
            chkPazienteGuztiak.TabIndex = 10;
            chkPazienteGuztiak.Text = "Paziente guztiak";
            chkPazienteGuztiak.UseVisualStyleBackColor = true;
            // 
            // HitzorduakKontsultatzea
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(142, 121, 102);
            ClientSize = new Size(1902, 1575);
            Margin = new Padding(11, 9, 11, 9);
            Name = "HitzorduakKontsultatzea";
            Text = "GOsasun - Hitzorduak Kontsultatzea";
            _edukiPanela.ResumeLayout(false);
            _edukiPanela.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHitzorduak).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblIzenburua;
        private System.Windows.Forms.MonthCalendar calEgutegia;
        private System.Windows.Forms.DataGridView dgvHitzorduak;
        private Label lblBilatuPazientea;
        private TextBox txtPazienteBilatu;
        private CheckBox chkPazienteGuztiak;
    }
}

