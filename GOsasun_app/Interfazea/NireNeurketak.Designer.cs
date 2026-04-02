using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace GOsasun_app.Interfazea
{
    partial class NireNeurketak
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
            labelTitulua = new Label();
            btnNeurketaBerria = new GOsasun_app.Interfazea.Kontrolak.CustomCardButton();
            dgvHistoriala = new DataGridView();
            dtpBilatuData = new DateTimePicker();
            lblFiltroa = new Label();
            btnGarbituFiltroa = new Button();
            _edukiPanela.SuspendLayout();
            ((ISupportInitialize)dgvHistoriala).BeginInit();
            SuspendLayout();
            // 
            // _edukiPanela
            // 
            _edukiPanela.Controls.Add(btnGarbituFiltroa);
            _edukiPanela.Controls.Add(lblFiltroa);
            _edukiPanela.Controls.Add(dtpBilatuData);
            _edukiPanela.Controls.Add(dgvHistoriala);
            _edukiPanela.Controls.Add(btnNeurketaBerria);
            _edukiPanela.Controls.Add(labelTitulua);
            _edukiPanela.Size = new Size(1902, 1153);
            // 
            // _atzeraBotoia
            // 
            _atzeraBotoia.FlatAppearance.BorderSize = 0;
            // 
            // labelTitulua
            // 
            labelTitulua.AutoSize = true;
            labelTitulua.Font = new Font("Segoe UI Semibold", 28F, FontStyle.Bold);
            labelTitulua.ForeColor = Color.FromArgb(44, 62, 80);
            labelTitulua.Location = new Point(80, 60);
            labelTitulua.Name = "labelTitulua";
            labelTitulua.Size = new Size(643, 100);
            labelTitulua.TabIndex = 0;
            labelTitulua.Text = "NIRE NEURKETAK";
            // 
            // btnNeurketaBerria
            // 
            btnNeurketaBerria.BackColor = Color.White;
            btnNeurketaBerria.BorderBiribiltasuna = 20;
            btnNeurketaBerria.Ikonoa = null;
            btnNeurketaBerria.KartaKolorea = Color.FromArgb(46, 204, 113);
            btnNeurketaBerria.Location = new Point(1400, 60);
            btnNeurketaBerria.Margin = new Padding(20);
            btnNeurketaBerria.Name = "btnNeurketaBerria";
            btnNeurketaBerria.Padding = new Padding(10);
            btnNeurketaBerria.Size = new Size(420, 110);
            btnNeurketaBerria.TabIndex = 1;
            btnNeurketaBerria.Testua = "NEURKETA EGIN";
            btnNeurketaBerria.TestuKolorea = Color.White;
            // 
            // dgvHistoriala
            // 
            dgvHistoriala.AllowUserToAddRows = false;
            dgvHistoriala.AllowUserToDeleteRows = false;
            dgvHistoriala.BackgroundColor = Color.White;
            dgvHistoriala.BorderStyle = BorderStyle.None;
            dgvHistoriala.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(52, 152, 219);
            dataGridViewCellStyle1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvHistoriala.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvHistoriala.ColumnHeadersHeight = 60;
            dgvHistoriala.EnableHeadersVisualStyles = false;
            dgvHistoriala.GridColor = Color.FromArgb(236, 240, 241);
            dgvHistoriala.Location = new Point(80, 240);
            dgvHistoriala.Name = "dgvHistoriala";
            dgvHistoriala.ReadOnly = true;
            dgvHistoriala.RowHeadersVisible = false;
            dgvHistoriala.RowHeadersWidth = 82;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 11F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(44, 62, 80);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(232, 244, 255);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(44, 62, 80);
            dgvHistoriala.RowsDefaultCellStyle = dataGridViewCellStyle2;
            dgvHistoriala.RowTemplate.Height = 50;
            dgvHistoriala.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistoriala.Size = new Size(1740, 830);
            dgvHistoriala.TabIndex = 2;
            // 
            // dtpBilatuData
            // 
            dtpBilatuData.CalendarFont = new Font("Segoe UI", 12F);
            dtpBilatuData.Font = new Font("Segoe UI", 12F);
            dtpBilatuData.Format = DateTimePickerFormat.Short;
            dtpBilatuData.Location = new Point(457, 163);
            dtpBilatuData.Name = "dtpBilatuData";
            dtpBilatuData.Size = new Size(250, 50);
            dtpBilatuData.TabIndex = 3;
            // 
            // lblFiltroa
            // 
            lblFiltroa.AutoSize = true;
            lblFiltroa.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblFiltroa.ForeColor = Color.FromArgb(44, 62, 80);
            lblFiltroa.Location = new Point(80, 166);
            lblFiltroa.Name = "lblFiltroa";
            lblFiltroa.Size = new Size(363, 45);
            lblFiltroa.TabIndex = 4;
            lblFiltroa.Text = "Bilatu dataren arabera:";
            // 
            // btnGarbituFiltroa
            // 
            btnGarbituFiltroa.BackColor = Color.FromArgb(149, 165, 166);
            btnGarbituFiltroa.FlatAppearance.BorderSize = 0;
            btnGarbituFiltroa.FlatStyle = FlatStyle.Flat;
            btnGarbituFiltroa.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnGarbituFiltroa.ForeColor = Color.White;
            btnGarbituFiltroa.Location = new Point(741, 163);
            btnGarbituFiltroa.Name = "btnGarbituFiltroa";
            btnGarbituFiltroa.Size = new Size(150, 50);
            btnGarbituFiltroa.TabIndex = 5;
            btnGarbituFiltroa.Text = "GARBITU";
            btnGarbituFiltroa.UseVisualStyleBackColor = false;
            // 
            // NireNeurketak
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1334);
            Name = "NireNeurketak";
            Text = "GOsasun - Nire Neurketak";
            _edukiPanela.ResumeLayout(false);
            _edukiPanela.PerformLayout();
            ((ISupportInitialize)dgvHistoriala).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label labelTitulua;
        private GOsasun_app.Interfazea.Kontrolak.CustomCardButton btnNeurketaBerria;
        private DataGridView dgvHistoriala;
        private DateTimePicker dtpBilatuData;
        private Label lblFiltroa;
        private Button btnGarbituFiltroa;
    }
}
