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
            _edukiPanela.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHitzorduak).BeginInit();
            SuspendLayout();
            //
            // _edukiPanela
            //
            _edukiPanela.Controls.Add(dgvHitzorduak);
            _edukiPanela.Controls.Add(btnGuztiak);
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
            dgvHitzorduak.Size = new Size(1611, 800);
            dgvHitzorduak.TabIndex = 5;
            //
            // btnGuztiak
            //
            btnGuztiak.BackColor = Color.White;
            btnGuztiak.FlatStyle = FlatStyle.Flat;
            btnGuztiak.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btnGuztiak.ForeColor = Color.FromArgb(43, 71, 92);
            btnGuztiak.Location = new Point(6, 479);
            btnGuztiak.Name = "btnGuztiak";
            btnGuztiak.Size = new Size(374, 165);
            btnGuztiak.TabIndex = 4;
            btnGuztiak.Text = "Erakutsi Guztiak";
            btnGuztiak.UseVisualStyleBackColor = false;
            btnGuztiak.Click += btnGuztiak_Click_1;
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
    }
}

