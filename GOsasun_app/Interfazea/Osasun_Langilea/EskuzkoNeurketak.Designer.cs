using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace GOsasun_app.Interfazea
{
    partial class EskuzkoNeurketak
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
            _lblTitle = new Label();
            _lblBilatu = new Label();
            _txtPazienteBilatu = new TextBox();
            _dgvPazienteak = new DataGridView();
            _lblHistoriala = new Label();
            _dgvHistoriala = new DataGridView();
            _pnlSarrera = new Panel();
            _lblBalioa = new Label();
            _numBalioa = new NumericUpDown();
            _lblUnitatea = new Label();
            _btnGorde = new Button();
            _btnItzuli = new Button();
            _edukiPanela.SuspendLayout();
            _pnlMainCard.SuspendLayout();
            ((ISupportInitialize)_dgvPazienteak).BeginInit();
            ((ISupportInitialize)_dgvHistoriala).BeginInit();
            _pnlSarrera.SuspendLayout();
            ((ISupportInitialize)_numBalioa).BeginInit();
            SuspendLayout();
            //
            // _edukiPanela
            //
            _edukiPanela.Controls.Add(_pnlMainCard);
            _edukiPanela.Size = new Size(1902, 1153);
            //
            // _pnlMainCard
            //
            _pnlMainCard.BackColor = Color.White;
            _pnlMainCard.Controls.Add(_lblTitle);
            _pnlMainCard.Controls.Add(_lblBilatu);
            _pnlMainCard.Controls.Add(_txtPazienteBilatu);
            _pnlMainCard.Controls.Add(_dgvPazienteak);
            _pnlMainCard.Controls.Add(_lblHistoriala);
            _pnlMainCard.Controls.Add(_dgvHistoriala);
            _pnlMainCard.Controls.Add(_pnlSarrera);
            _pnlMainCard.Controls.Add(_btnItzuli);
            _pnlMainCard.Location = new Point(350, 50);
            _pnlMainCard.Name = "_pnlMainCard";
            _pnlMainCard.Size = new Size(1200, 1080);
            _pnlMainCard.TabIndex = 0;
            //
            // _lblTitle
            //
            _lblTitle.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            _lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            _lblTitle.Location = new Point(0, 20);
            _lblTitle.Name = "_lblTitle";
            _lblTitle.Size = new Size(1200, 100);
            _lblTitle.TabIndex = 0;
            _lblTitle.Text = "PISUA / ALTUERA NEURKETA";
            _lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            //
            // _lblBilatu
            //
            _lblBilatu.AutoSize = true;
            _lblBilatu.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            _lblBilatu.Location = new Point(50, 140);
            _lblBilatu.Name = "_lblBilatu";
            _lblBilatu.Size = new Size(403, 51);
            _lblBilatu.TabIndex = 1;
            _lblBilatu.Text = "1. Pazientea aukeratu:";
            //
            // _txtPazienteBilatu
            //
            _txtPazienteBilatu.Font = new Font("Segoe UI", 16F);
            _txtPazienteBilatu.Location = new Point(50, 190);
            _txtPazienteBilatu.Name = "_txtPazienteBilatu";
            _txtPazienteBilatu.PlaceholderText = "Idatzi izena edo abizena...";
            _txtPazienteBilatu.Size = new Size(1100, 65);
            _txtPazienteBilatu.TabIndex = 2;
            //
            // _dgvPazienteak
            //
            _dgvPazienteak.AllowUserToAddRows = false;
            _dgvPazienteak.AllowUserToDeleteRows = false;
            _dgvPazienteak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dgvPazienteak.BackgroundColor = Color.White;
            _dgvPazienteak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dgvPazienteak.Location = new Point(50, 265);
            _dgvPazienteak.MultiSelect = false;
            _dgvPazienteak.Name = "_dgvPazienteak";
            _dgvPazienteak.ReadOnly = true;
            _dgvPazienteak.RowHeadersVisible = false;
            _dgvPazienteak.RowTemplate.Height = 50;
            _dgvPazienteak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgvPazienteak.Size = new Size(1100, 180);
            _dgvPazienteak.TabIndex = 3;
            //
            // _lblHistoriala
            //
            _lblHistoriala.AutoSize = true;
            _lblHistoriala.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            _lblHistoriala.Location = new Point(50, 460);
            _lblHistoriala.Name = "_lblHistoriala";
            _lblHistoriala.Size = new Size(420, 51);
            _lblHistoriala.TabIndex = 4;
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
            _dgvHistoriala.Location = new Point(50, 515);
            _dgvHistoriala.MultiSelect = false;
            _dgvHistoriala.Name = "_dgvHistoriala";
            _dgvHistoriala.ReadOnly = true;
            _dgvHistoriala.RowHeadersVisible = false;
            _dgvHistoriala.RowTemplate.Height = 50;
            _dgvHistoriala.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgvHistoriala.Size = new Size(1100, 250);
            _dgvHistoriala.TabIndex = 5;
            _dgvHistoriala.Visible = false;
            //
            // _pnlSarrera
            //
            _pnlSarrera.BackColor = Color.FromArgb(248, 249, 250);
            _pnlSarrera.Controls.Add(_lblBalioa);
            _pnlSarrera.Controls.Add(_numBalioa);
            _pnlSarrera.Controls.Add(_lblUnitatea);
            _pnlSarrera.Controls.Add(_btnGorde);
            _pnlSarrera.Location = new Point(50, 780);
            _pnlSarrera.Name = "_pnlSarrera";
            _pnlSarrera.Size = new Size(1100, 180);
            _pnlSarrera.TabIndex = 6;
            _pnlSarrera.Visible = false;
            //
            // _lblBalioa
            //
            _lblBalioa.AutoSize = true;
            _lblBalioa.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            _lblBalioa.Location = new Point(30, 30);
            _lblBalioa.Name = "_lblBalioa";
            _lblBalioa.Size = new Size(400, 59);
            _lblBalioa.TabIndex = 0;
            _lblBalioa.Text = "Sartu balioa:";
            //
            // _numBalioa
            //
            _numBalioa.DecimalPlaces = 2;
            _numBalioa.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            _numBalioa.Location = new Point(480, 20);
            _numBalioa.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            _numBalioa.Name = "_numBalioa";
            _numBalioa.Size = new Size(250, 93);
            _numBalioa.TabIndex = 1;
            _numBalioa.TextAlign = HorizontalAlignment.Center;
            //
            // _lblUnitatea
            //
            _lblUnitatea.AutoSize = true;
            _lblUnitatea.Font = new Font("Segoe UI", 16F);
            _lblUnitatea.Location = new Point(740, 45);
            _lblUnitatea.Name = "_lblUnitatea";
            _lblUnitatea.Size = new Size(71, 59);
            _lblUnitatea.TabIndex = 2;
            _lblUnitatea.Text = "kg";
            //
            // _btnGorde
            //
            _btnGorde.BackColor = Color.FromArgb(46, 204, 113);
            _btnGorde.FlatStyle = FlatStyle.Flat;
            _btnGorde.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            _btnGorde.ForeColor = Color.White;
            _btnGorde.Location = new Point(850, 25);
            _btnGorde.Name = "_btnGorde";
            _btnGorde.Size = new Size(220, 85);
            _btnGorde.TabIndex = 3;
            _btnGorde.Text = "Gorde";
            _btnGorde.UseVisualStyleBackColor = false;
            //
            // _btnItzuli
            //
            _btnItzuli.BackColor = Color.FromArgb(231, 76, 60);
            _btnItzuli.FlatStyle = FlatStyle.Flat;
            _btnItzuli.Font = new Font("Segoe UI", 12F);
            _btnItzuli.ForeColor = Color.White;
            _btnItzuli.Location = new Point(525, 980);
            _btnItzuli.Name = "_btnItzuli";
            _btnItzuli.Size = new Size(150, 58);
            _btnItzuli.TabIndex = 7;
            _btnItzuli.Text = "Itzuli";
            _btnItzuli.UseVisualStyleBackColor = false;
            //
            // EskuzkoNeurketak
            //
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1334);
            Margin = new Padding(6, 6, 6, 6);
            Name = "EskuzkoNeurketak";
            Text = "GOsasun - Eskuzko Neurketa";
            _edukiPanela.ResumeLayout(false);
            _pnlMainCard.ResumeLayout(false);
            _pnlMainCard.PerformLayout();
            ((ISupportInitialize)_dgvPazienteak).EndInit();
            ((ISupportInitialize)_dgvHistoriala).EndInit();
            _pnlSarrera.ResumeLayout(false);
            _pnlSarrera.PerformLayout();
            ((ISupportInitialize)_numBalioa).EndInit();
            ResumeLayout(false);
        }

        private Panel _pnlMainCard;
        private Label _lblTitle;
        private Label _lblBilatu;
        private TextBox _txtPazienteBilatu;
        private DataGridView _dgvPazienteak;
        private Label _lblHistoriala;
        private DataGridView _dgvHistoriala;
        private Panel _pnlSarrera;
        private Label _lblBalioa;
        private NumericUpDown _numBalioa;
        private Label _lblUnitatea;
        private Button _btnGorde;
        private Button _btnItzuli;
    }
}
