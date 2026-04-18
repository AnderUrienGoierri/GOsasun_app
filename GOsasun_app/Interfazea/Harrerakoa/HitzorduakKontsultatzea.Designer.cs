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
            btnGuztiak = new Button();
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
            _edukiPanela.Controls.Add(btnGuztiak);
            _edukiPanela.Controls.Add(chkPazienteGuztiak);
            _edukiPanela.Controls.Add(txtPazienteBilatu);
            _edukiPanela.Controls.Add(lblBilatuPazientea);
            _edukiPanela.Controls.Add(calEgutegia);
            _edukiPanela.Controls.Add(lblIzenburua);
            _edukiPanela.Location = new Point(0, 310);
            _edukiPanela.Size = new Size(2036, 1120);
            //
            // _goiburuBarra
            //
            _goiburuBarra.Size = new Size(2036, 310);
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
            lblIzenburua.Location = new Point(385, 19);
            lblIzenburua.Name = "lblIzenburua";
            lblIzenburua.Size = new Size(1162, 128);
            lblIzenburua.TabIndex = 2;
            lblIzenburua.Text = "HITZORDUEN EGUTEGIA";
            //
            // calEgutegia
            //
            calEgutegia.Location = new Point(34, 150);
            calEgutegia.Name = "calEgutegia";
            calEgutegia.TabIndex = 3;
            //
            // dgvHitzorduak
            //
            dgvHitzorduak.AllowUserToAddRows = false;
            dgvHitzorduak.AllowUserToDeleteRows = false;
            dgvHitzorduak.BackgroundColor = Color.White;
            dgvHitzorduak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHitzorduak.Location = new Point(385, 150);
            dgvHitzorduak.Name = "dgvHitzorduak";
            dgvHitzorduak.ReadOnly = true;
            dgvHitzorduak.RowHeadersVisible = false;
            dgvHitzorduak.RowHeadersWidth = 82;
            dgvHitzorduak.Size = new Size(1592, 800);
            dgvHitzorduak.TabIndex = 5;
            //
            // btnGuztiak
            //
            btnGuztiak.BackColor = Color.White;
            btnGuztiak.FlatStyle = FlatStyle.Flat;
            btnGuztiak.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btnGuztiak.ForeColor = Color.FromArgb(43, 71, 92);
            btnGuztiak.Location = new Point(34, 810);
            btnGuztiak.Name = "btnGuztiak";
            btnGuztiak.Size = new Size(325, 86);
            btnGuztiak.TabIndex = 7;
            btnGuztiak.Text = "Erakutsi Guztiak";
            btnGuztiak.UseVisualStyleBackColor = false;
            btnGuztiak.Click += btnGuztiak_Click_1;
            // 
            // lblBilatuPazientea
            // 
            lblBilatuPazientea.AutoSize = true;
            lblBilatuPazientea.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBilatuPazientea.ForeColor = Color.FromArgb(44, 62, 80);
            lblBilatuPazientea.Location = new Point(34, 501);
            lblBilatuPazientea.Name = "lblBilatuPazientea";
            lblBilatuPazientea.Size = new Size(322, 37);
            lblBilatuPazientea.TabIndex = 8;
            lblBilatuPazientea.Text = "Bilatu pazientea edo DNI";
            // 
            // txtPazienteBilatu
            // 
            txtPazienteBilatu.Font = new Font("Segoe UI", 10F);
            txtPazienteBilatu.Location = new Point(34, 548);
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
            chkPazienteGuztiak.Location = new Point(40, 617);
            chkPazienteGuztiak.Name = "chkPazienteGuztiak";
            chkPazienteGuztiak.Size = new Size(228, 39);
            chkPazienteGuztiak.TabIndex = 10;
            chkPazienteGuztiak.Text = "Paziente guztiak";
            chkPazienteGuztiak.UseVisualStyleBackColor = true;
            //
            // HitzorduakKontsultatzea
            //
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(142, 121, 102);
            ClientSize = new Size(2036, 1430);
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
        private System.Windows.Forms.Button btnGuztiak;
        private Label lblBilatuPazientea;
        private TextBox txtPazienteBilatu;
        private CheckBox chkPazienteGuztiak;
    }
}

