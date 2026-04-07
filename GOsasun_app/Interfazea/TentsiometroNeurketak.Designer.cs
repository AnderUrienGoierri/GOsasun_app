using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace GOsasun_app.Interfazea
{
    partial class TentsiometroNeurketak
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
            _pnlMainCard = new Panel();
            _lblStatus = new Label();
            _lblBilatu = new Label();
            _txtPazienteBilatu = new TextBox();
            _dgvPazienteak = new DataGridView();
            _lblHistoriala = new Label();
            _dgvHistoriala = new DataGridView();
            _btnInportatu = new Button();
            _btnUtzi = new Button();
            _edukiPanela.SuspendLayout();
            _pnlMainCard.SuspendLayout();
            ((ISupportInitialize)_dgvPazienteak).BeginInit();
            ((ISupportInitialize)_dgvHistoriala).BeginInit();
            SuspendLayout();
            // 
            // _edukiPanela
            // 
            _edukiPanela.Controls.Add(_pnlMainCard);
            _edukiPanela.Size = new Size(1902, 1345);
            // 
            // _atzeraBotoia
            // 
            _atzeraBotoia.FlatAppearance.BorderSize = 0;
            // 
            // _pnlMainCard
            // 
            _pnlMainCard.BackColor = Color.White;
            _pnlMainCard.Controls.Add(_lblStatus);
            _pnlMainCard.Controls.Add(_lblBilatu);
            _pnlMainCard.Controls.Add(_txtPazienteBilatu);
            _pnlMainCard.Controls.Add(_dgvPazienteak);
            _pnlMainCard.Controls.Add(_lblHistoriala);
            _pnlMainCard.Controls.Add(_dgvHistoriala);
            _pnlMainCard.Controls.Add(_btnInportatu);
            _pnlMainCard.Controls.Add(_btnUtzi);
            _pnlMainCard.Location = new Point(177, 50);
            _pnlMainCard.Name = "_pnlMainCard";
            _pnlMainCard.Size = new Size(1591, 1234);
            _pnlMainCard.TabIndex = 0;
            _pnlMainCard.Paint += _pnlMainCard_Paint;
            // 
            // _lblStatus
            // 
            _lblStatus.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            _lblStatus.ForeColor = Color.FromArgb(44, 62, 80);
            _lblStatus.Location = new Point(0, 0);
            _lblStatus.Name = "_lblStatus";
            _lblStatus.Size = new Size(1200, 128);
            _lblStatus.TabIndex = 0;
            _lblStatus.Text = "Konektatu Beurer BM58 USB bidez...";
            _lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _lblBilatu
            // 
            _lblBilatu.AutoSize = true;
            _lblBilatu.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            _lblBilatu.Location = new Point(50, 202);
            _lblBilatu.Name = "_lblBilatu";
            _lblBilatu.Size = new Size(412, 51);
            _lblBilatu.TabIndex = 1;
            _lblBilatu.Text = "1. Pazientea aukeratu:";
            // 
            // _txtPazienteBilatu
            // 
            _txtPazienteBilatu.Font = new Font("Segoe UI", 16F);
            _txtPazienteBilatu.Location = new Point(50, 256);
            _txtPazienteBilatu.Name = "_txtPazienteBilatu";
            _txtPazienteBilatu.PlaceholderText = "Idatzi izena edo abizena...";
            _txtPazienteBilatu.Size = new Size(1100, 64);
            _txtPazienteBilatu.TabIndex = 2;
            // 
            // _dgvPazienteak
            // 
            _dgvPazienteak.AllowUserToAddRows = false;
            _dgvPazienteak.AllowUserToDeleteRows = false;
            _dgvPazienteak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dgvPazienteak.BackgroundColor = Color.White;
            _dgvPazienteak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dgvPazienteak.Location = new Point(50, 325);
            _dgvPazienteak.MultiSelect = false;
            _dgvPazienteak.Name = "_dgvPazienteak";
            _dgvPazienteak.ReadOnly = true;
            _dgvPazienteak.RowHeadersVisible = false;
            _dgvPazienteak.RowHeadersWidth = 82;
            _dgvPazienteak.RowTemplate.Height = 50;
            _dgvPazienteak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgvPazienteak.Size = new Size(1486, 180);
            _dgvPazienteak.TabIndex = 3;
            // 
            // _lblHistoriala
            // 
            _lblHistoriala.AutoSize = true;
            _lblHistoriala.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            _lblHistoriala.Location = new Point(50, 519);
            _lblHistoriala.Name = "_lblHistoriala";
            _lblHistoriala.Size = new Size(439, 51);
            _lblHistoriala.TabIndex = 6;
            _lblHistoriala.Text = "2. Neurketen historiala:";
            _lblHistoriala.Visible = false;
            // 
            // _dgvHistoriala
            // 
            _dgvHistoriala.AllowUserToAddRows = false;
            _dgvHistoriala.AllowUserToDeleteRows = false;
            _dgvHistoriala.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dgvHistoriala.BackgroundColor = Color.White;
            _dgvHistoriala.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dgvHistoriala.Location = new Point(50, 577);
            _dgvHistoriala.MultiSelect = false;
            _dgvHistoriala.Name = "_dgvHistoriala";
            _dgvHistoriala.ReadOnly = true;
            _dgvHistoriala.RowHeadersVisible = false;
            _dgvHistoriala.RowHeadersWidth = 82;
            _dgvHistoriala.RowTemplate.Height = 50;
            _dgvHistoriala.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgvHistoriala.Size = new Size(1486, 320);
            _dgvHistoriala.TabIndex = 7;
            _dgvHistoriala.Visible = false;
            // 
            // _btnInportatu
            // 
            _btnInportatu.BackColor = Color.FromArgb(46, 204, 113);
            _btnInportatu.FlatStyle = FlatStyle.Flat;
            _btnInportatu.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            _btnInportatu.ForeColor = Color.White;
            _btnInportatu.Location = new Point(350, 983);
            _btnInportatu.Name = "_btnInportatu";
            _btnInportatu.Size = new Size(500, 80);
            _btnInportatu.TabIndex = 4;
            _btnInportatu.Text = "Datuak inportatu";
            _btnInportatu.UseVisualStyleBackColor = false;
            // 
            // _btnUtzi
            // 
            _btnUtzi.BackColor = Color.FromArgb(231, 76, 60);
            _btnUtzi.FlatStyle = FlatStyle.Flat;
            _btnUtzi.Font = new Font("Segoe UI", 12F);
            _btnUtzi.ForeColor = Color.White;
            _btnUtzi.Location = new Point(525, 1080);
            _btnUtzi.Name = "_btnUtzi";
            _btnUtzi.Size = new Size(150, 58);
            _btnUtzi.TabIndex = 5;
            _btnUtzi.Text = "Utzi";
            _btnUtzi.UseVisualStyleBackColor = false;
            // 
            // TentsiometroNeurketak
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1526);
            Name = "TentsiometroNeurketak";
            Text = "GOsasun - BM58 Tentsio Inportazioa";
            _edukiPanela.ResumeLayout(false);
            _pnlMainCard.ResumeLayout(false);
            _pnlMainCard.PerformLayout();
            ((ISupportInitialize)_dgvPazienteak).EndInit();
            ((ISupportInitialize)_dgvHistoriala).EndInit();
            ResumeLayout(false);
        }

        private Panel _pnlMainCard;
        private Label _lblStatus;
        private Label _lblBilatu;
        private TextBox _txtPazienteBilatu;
        private DataGridView _dgvPazienteak;
        private Label _lblHistoriala;
        private DataGridView _dgvHistoriala;
        private Button _btnInportatu;
        private Button _btnUtzi;
    }
}
