using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace GOsasun_app.Interfazea
{
    partial class NireJarraipenak
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
            btnNeurketaBerria = new GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia();
            dgvHistoriala = new DataGridView();
            dtpHasieraData = new DateTimePicker();
            lblDataFiltroa = new Label();
            btnGarbituFiltroa = new Button();
            dtpAmaieraData = new DateTimePicker();
            lblBilatuOharrak = new Label();
            txtBilatuOharrak = new TextBox();
            _edukiPanela.SuspendLayout();
            ((ISupportInitialize)dgvHistoriala).BeginInit();
            SuspendLayout();
            // 
            // _edukiPanela
            // 
            _edukiPanela.Controls.Add(txtBilatuOharrak);
            _edukiPanela.Controls.Add(lblBilatuOharrak);
            _edukiPanela.Controls.Add(dtpAmaieraData);
            _edukiPanela.Controls.Add(btnGarbituFiltroa);
            _edukiPanela.Controls.Add(lblDataFiltroa);
            _edukiPanela.Controls.Add(dtpHasieraData);
            _edukiPanela.Controls.Add(dgvHistoriala);
            _edukiPanela.Controls.Add(btnNeurketaBerria);
            _edukiPanela.Controls.Add(labelTitulua);
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
            labelTitulua.Location = new Point(80, 3);
            labelTitulua.Name = "labelTitulua";
            labelTitulua.Size = new Size(710, 100);
            labelTitulua.TabIndex = 0;
            labelTitulua.Text = "NIRE JARRAIPENAK";
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
            dgvHistoriala.Location = new Point(80, 330);
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
            dgvHistoriala.RowTemplate.Height = 56;
            dgvHistoriala.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistoriala.Size = new Size(1740, 740);
            dgvHistoriala.TabIndex = 2;
            // 
            // dtpHasieraData
            // 
            dtpHasieraData.Checked = false;
            dtpHasieraData.CustomFormat = "'Hasiera data: 'dd/MM/yyyy";
            dtpHasieraData.Font = new Font("Segoe UI", 11F);
            dtpHasieraData.Format = DateTimePickerFormat.Custom;
            dtpHasieraData.Location = new Point(80, 249);
            dtpHasieraData.Name = "dtpHasieraData";
            dtpHasieraData.ShowCheckBox = true;
            dtpHasieraData.Size = new Size(460, 47);
            dtpHasieraData.TabIndex = 3;
            // 
            // lblDataFiltroa
            // 
            lblDataFiltroa.AutoSize = true;
            lblDataFiltroa.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblDataFiltroa.ForeColor = Color.FromArgb(44, 62, 80);
            lblDataFiltroa.Location = new Point(80, 198);
            lblDataFiltroa.Name = "lblDataFiltroa";
            lblDataFiltroa.Size = new Size(347, 45);
            lblDataFiltroa.TabIndex = 4;
            lblDataFiltroa.Text = "Iragazi daten arabera:";
            // 
            // btnGarbituFiltroa
            // 
            btnGarbituFiltroa.BackColor = Color.FromArgb(149, 165, 166);
            btnGarbituFiltroa.FlatAppearance.BorderSize = 0;
            btnGarbituFiltroa.FlatStyle = FlatStyle.Flat;
            btnGarbituFiltroa.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnGarbituFiltroa.ForeColor = Color.White;
            btnGarbituFiltroa.Location = new Point(1079, 249);
            btnGarbituFiltroa.Name = "btnGarbituFiltroa";
            btnGarbituFiltroa.Size = new Size(170, 50);
            btnGarbituFiltroa.TabIndex = 5;
            btnGarbituFiltroa.Text = "GARBITU";
            btnGarbituFiltroa.UseVisualStyleBackColor = false;
            // 
            // dtpAmaieraData
            // 
            dtpAmaieraData.Checked = false;
            dtpAmaieraData.CustomFormat = "'Amaiera data: 'dd/MM/yyyy";
            dtpAmaieraData.Font = new Font("Segoe UI", 11F);
            dtpAmaieraData.Format = DateTimePickerFormat.Custom;
            dtpAmaieraData.Location = new Point(575, 249);
            dtpAmaieraData.Name = "dtpAmaieraData";
            dtpAmaieraData.ShowCheckBox = true;
            dtpAmaieraData.Size = new Size(460, 47);
            dtpAmaieraData.TabIndex = 6;
            // 
            // lblBilatuOharrak
            // 
            lblBilatuOharrak.AutoSize = true;
            lblBilatuOharrak.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblBilatuOharrak.ForeColor = Color.FromArgb(44, 62, 80);
            lblBilatuOharrak.Location = new Point(80, 125);
            lblBilatuOharrak.Name = "lblBilatuOharrak";
            lblBilatuOharrak.Size = new Size(366, 45);
            lblBilatuOharrak.TabIndex = 7;
            lblBilatuOharrak.Text = "Bilatu oharren arabera:";
            // 
            // txtBilatuOharrak
            // 
            txtBilatuOharrak.Font = new Font("Segoe UI", 12F);
            txtBilatuOharrak.Location = new Point(487, 122);
            txtBilatuOharrak.Name = "txtBilatuOharrak";
            txtBilatuOharrak.PlaceholderText = "Idatzi oharren testua";
            txtBilatuOharrak.Size = new Size(650, 50);
            txtBilatuOharrak.TabIndex = 8;
            // 
            // NireJarraipenak
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1902, 1394);
            Name = "NireJarraipenak";
            Text = "GOsasun - Nire Jarraipenak";
            _edukiPanela.ResumeLayout(false);
            _edukiPanela.PerformLayout();
            ((ISupportInitialize)dgvHistoriala).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label labelTitulua;
        private GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia btnNeurketaBerria;
        private DataGridView dgvHistoriala;
        private DateTimePicker dtpHasieraData;
        private Label lblDataFiltroa;
        private Button btnGarbituFiltroa;
        private DateTimePicker dtpAmaieraData;
        private Label lblBilatuOharrak;
        private TextBox txtBilatuOharrak;
    }
}

