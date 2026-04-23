using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace GOsasun_app.Interfazea
{
    partial class HarreraMenua
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
            btnPazienteak = new GOsasun_app.Interfazea.Oinarriak_UI.MenuTxartelBotoia();
            btnMedikuak = new GOsasun_app.Interfazea.Oinarriak_UI.MenuTxartelBotoia();
            btnLangileak = new GOsasun_app.Interfazea.Oinarriak_UI.MenuTxartelBotoia();
            btnHitzorduak = new GOsasun_app.Interfazea.Oinarriak_UI.MenuTxartelBotoia();
            btnDokumentuak = new GOsasun_app.Interfazea.Oinarriak_UI.MenuTxartelBotoia();
            _edukiPanela.SuspendLayout();
            SuspendLayout();
            //
            // _edukiPanela
            //
            _edukiPanela.Controls.Add(btnDokumentuak);
            _edukiPanela.Controls.Add(btnPazienteak);
            _edukiPanela.Controls.Add(btnMedikuak);
            _edukiPanela.Controls.Add(btnLangileak);
            _edukiPanela.Controls.Add(btnHitzorduak);
            _edukiPanela.Size = new Size(1902, 1159);
            //
            // _atzeraBotoia
            //
            _atzeraBotoia.FlatAppearance.BorderSize = 0;
            //
            // btnPazienteak
            //
            btnPazienteak.BackColor = Color.White;
            btnPazienteak.BorderBiribiltasuna = 24;
            btnPazienteak.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPazienteak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnPazienteak.Location = new Point(37, 43);
            btnPazienteak.Margin = new Padding(37, 43, 37, 43);
            btnPazienteak.Name = "btnPazienteak";
            btnPazienteak.Padding = new Padding(19, 21, 19, 21);
            btnPazienteak.Size = new Size(576, 512);
            btnPazienteak.TabIndex = 0;
            btnPazienteak.Testua = "PAZIENTEAK KUDEATU";
            btnPazienteak.TestuKolorea = Color.FromArgb(50, 50, 50);
            //
            // btnMedikuak
            //
            btnMedikuak.BackColor = Color.White;
            btnMedikuak.BorderBiribiltasuna = 24;
            btnMedikuak.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMedikuak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnMedikuak.Location = new Point(650, 43);
            btnMedikuak.Margin = new Padding(37, 43, 37, 43);
            btnMedikuak.Name = "btnMedikuak";
            btnMedikuak.Padding = new Padding(19, 21, 19, 21);
            btnMedikuak.Size = new Size(576, 512);
            btnMedikuak.TabIndex = 1;
            btnMedikuak.Testua = "OSASUN LANGILEAK KUDEATU";
            btnMedikuak.TestuKolorea = Color.FromArgb(50, 50, 50);
            //
            // btnLangileak
            //
            btnLangileak.BackColor = Color.White;
            btnLangileak.BorderBiribiltasuna = 24;
            btnLangileak.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLangileak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnLangileak.Location = new Point(1263, 43);
            btnLangileak.Margin = new Padding(37, 43, 37, 43);
            btnLangileak.Name = "btnLangileak";
            btnLangileak.Padding = new Padding(19, 21, 19, 21);
            btnLangileak.Size = new Size(576, 512);
            btnLangileak.TabIndex = 2;
            btnLangileak.Testua = "HARRERAKO LANGILEAK KUDEATU";
            btnLangileak.TestuKolorea = Color.FromArgb(50, 50, 50);
            btnLangileak.Paint += btnLangileak_Paint;
            //
            // btnHitzorduak
            //
            btnHitzorduak.BackColor = Color.White;
            btnHitzorduak.BorderBiribiltasuna = 24;
            btnHitzorduak.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnHitzorduak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnHitzorduak.Location = new Point(37, 597);
            btnHitzorduak.Margin = new Padding(37, 43, 37, 43);
            btnHitzorduak.Name = "btnHitzorduak";
            btnHitzorduak.Padding = new Padding(19, 21, 19, 21);
            btnHitzorduak.Size = new Size(576, 512);
            btnHitzorduak.TabIndex = 3;
            btnHitzorduak.Testua = "HITZORDUAK KUDEATU";
            btnHitzorduak.TestuKolorea = Color.FromArgb(50, 50, 50);
            //
            // btnDokumentuak
            //
            btnDokumentuak.BackColor = Color.White;
            btnDokumentuak.BorderBiribiltasuna = 24;
            btnDokumentuak.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDokumentuak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnDokumentuak.Location = new Point(650, 597);
            btnDokumentuak.Margin = new Padding(37, 43, 37, 43);
            btnDokumentuak.Name = "btnDokumentuak";
            btnDokumentuak.Padding = new Padding(19, 21, 19, 21);
            btnDokumentuak.Size = new Size(576, 512);
            btnDokumentuak.TabIndex = 4;
            btnDokumentuak.Testua = "DOKUMENTUAK";
            btnDokumentuak.TestuKolorea = Color.FromArgb(50, 50, 50);
            //
            // HarreraMenua
            //
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1902, 1340);
            Margin = new Padding(11, 9, 11, 9);
            Name = "HarreraMenua";
            Text = "GOsasun - Harrera Menua";
            _edukiPanela.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GOsasun_app.Interfazea.Oinarriak_UI.MenuTxartelBotoia btnPazienteak;
        private GOsasun_app.Interfazea.Oinarriak_UI.MenuTxartelBotoia btnMedikuak;
        private GOsasun_app.Interfazea.Oinarriak_UI.MenuTxartelBotoia btnLangileak;
        private GOsasun_app.Interfazea.Oinarriak_UI.MenuTxartelBotoia btnHitzorduak;
        private GOsasun_app.Interfazea.Oinarriak_UI.MenuTxartelBotoia btnDokumentuak;
    }
}

