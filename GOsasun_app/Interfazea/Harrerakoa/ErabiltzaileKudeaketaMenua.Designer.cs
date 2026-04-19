using System.Drawing;
using System.Windows.Forms;

namespace GOsasun_app.Interfazea
{
    partial class ErabiltzaileKudeaketaMenua
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
            btnSortu = new GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia();
            btnZerrendatu = new GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia();
            _edukiPanela.SuspendLayout();
            SuspendLayout();
            // 
            // _edukiPanela
            // 
            _edukiPanela.Controls.Add(btnSortu);
            _edukiPanela.Controls.Add(btnZerrendatu);
            _edukiPanela.Size = new Size(1902, 874);
            _edukiPanela.Paint += _edukiPanela_Paint;
            // 
            // _atzeraBotoia
            // 
            _atzeraBotoia.FlatAppearance.BorderSize = 0;
            // 
            // btnSortu
            // 
            btnSortu.BackColor = Color.White;
            btnSortu.BorderBiribiltasuna = 24;
            btnSortu.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnSortu.Ikonoa = null;
            btnSortu.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnSortu.Location = new Point(261, 183);
            btnSortu.Margin = new Padding(20);
            btnSortu.Name = "btnSortu";
            btnSortu.Padding = new Padding(19, 21, 19, 21);
            btnSortu.Size = new Size(319, 237);
            btnSortu.TabIndex = 0;
            btnSortu.Testua = "SORTU";
            btnSortu.TestuKolorea = Color.FromArgb(50, 50, 50);
            // 
            // btnZerrendatu
            // 
            btnZerrendatu.BackColor = Color.White;
            btnZerrendatu.BorderBiribiltasuna = 24;
            btnZerrendatu.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnZerrendatu.Ikonoa = null;
            btnZerrendatu.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnZerrendatu.Location = new Point(632, 183);
            btnZerrendatu.Margin = new Padding(20);
            btnZerrendatu.Name = "btnZerrendatu";
            btnZerrendatu.Padding = new Padding(19, 21, 19, 21);
            btnZerrendatu.Size = new Size(319, 237);
            btnZerrendatu.TabIndex = 1;
            btnZerrendatu.Testua = "ZERRENDATU";
            btnZerrendatu.TestuKolorea = Color.FromArgb(50, 50, 50);
            // 
            // ErabiltzaileKudeaketaMenua
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1902, 1055);
            Name = "ErabiltzaileKudeaketaMenua";
            Text = "Kudeaketa Menua";
            _edukiPanela.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia btnSortu;
        private GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia btnZerrendatu;
    }
}

