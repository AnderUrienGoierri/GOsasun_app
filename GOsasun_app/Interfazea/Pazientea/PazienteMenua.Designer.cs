using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace GOsasun_app.Interfazea
{
    partial class PazienteMenua
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
            btnNeurketak = new GOsasun_app.Interfazea.Oinarriak_UI.MenuTxartelBotoia();
            btnErrezetak = new GOsasun_app.Interfazea.Oinarriak_UI.MenuTxartelBotoia();
            btnGrafikak = new GOsasun_app.Interfazea.Oinarriak_UI.MenuTxartelBotoia();
            btnDokumentuak = new GOsasun_app.Interfazea.Oinarriak_UI.MenuTxartelBotoia();
            btnHitzorduak = new GOsasun_app.Interfazea.Oinarriak_UI.MenuTxartelBotoia();
            _edukiPanela.SuspendLayout();
            SuspendLayout();
            // 
            // _edukiPanela
            // 
            _edukiPanela.Controls.Add(btnHitzorduak);
            _edukiPanela.Controls.Add(btnDokumentuak);
            _edukiPanela.Controls.Add(btnNeurketak);
            _edukiPanela.Controls.Add(btnErrezetak);
            _edukiPanela.Controls.Add(btnGrafikak);
            _edukiPanela.Size = new Size(1902, 1177);
            // 
            // _goiburuBarra
            // 
            _goiburuBarra.Size = new Size(1902, 181);
            // 
            // btnNeurketak
            // 
            btnNeurketak.BackColor = Color.White;
            btnNeurketak.BorderBiribiltasuna = 24;
            btnNeurketak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnNeurketak.Location = new Point(37, 43);
            btnNeurketak.Margin = new Padding(37, 43, 37, 43);
            btnNeurketak.Name = "btnNeurketak";
            btnNeurketak.Padding = new Padding(19, 21, 19, 21);
            btnNeurketak.Size = new Size(576, 512);
            btnNeurketak.TabIndex = 0;
            btnNeurketak.Testua = "NIRE JARRAIPENAK";
            // 
            // btnErrezetak
            // 
            btnErrezetak.BackColor = Color.White;
            btnErrezetak.BorderBiribiltasuna = 24;
            btnErrezetak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnErrezetak.Location = new Point(650, 43);
            btnErrezetak.Margin = new Padding(37, 43, 37, 43);
            btnErrezetak.Name = "btnErrezetak";
            btnErrezetak.Padding = new Padding(19, 21, 19, 21);
            btnErrezetak.Size = new Size(576, 512);
            btnErrezetak.TabIndex = 1;
            btnErrezetak.Testua = "NIRE ERREZETAK";
            // 
            // btnGrafikak
            // 
            btnGrafikak.BackColor = Color.White;
            btnGrafikak.BorderBiribiltasuna = 24;
            btnGrafikak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnGrafikak.Location = new Point(1263, 43);
            btnGrafikak.Margin = new Padding(37, 43, 37, 43);
            btnGrafikak.Name = "btnGrafikak";
            btnGrafikak.Padding = new Padding(19, 21, 19, 21);
            btnGrafikak.Size = new Size(576, 512);
            btnGrafikak.TabIndex = 2;
            btnGrafikak.Testua = "GRAFIKAK";
            // 
            // btnDokumentuak
            // 
            btnDokumentuak.BackColor = Color.White;
            btnDokumentuak.BorderBiribiltasuna = 24;
            btnDokumentuak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnDokumentuak.Location = new Point(37, 597);
            btnDokumentuak.Margin = new Padding(37, 43, 37, 43);
            btnDokumentuak.Name = "btnDokumentuak";
            btnDokumentuak.Padding = new Padding(19, 21, 19, 21);
            btnDokumentuak.Size = new Size(576, 512);
            btnDokumentuak.TabIndex = 3;
            btnDokumentuak.Testua = "DOKUMENTUAK";
            // 
            // btnHitzorduak
            // 
            btnHitzorduak.BackColor = Color.White;
            btnHitzorduak.BorderBiribiltasuna = 24;
            btnHitzorduak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnHitzorduak.Location = new Point(650, 597);
            btnHitzorduak.Margin = new Padding(37, 43, 37, 43);
            btnHitzorduak.Name = "btnHitzorduak";
            btnHitzorduak.Padding = new Padding(19, 21, 19, 21);
            btnHitzorduak.Size = new Size(576, 512);
            btnHitzorduak.TabIndex = 4;
            btnHitzorduak.Testua = "NIRE HITZORDUAK";
            // 
            // PazienteMenua
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1902, 1334);
            Margin = new Padding(11, 9, 11, 9);
            Name = "PazienteMenua";
            Text = "GOsasun - Paziente Menua";
            _edukiPanela.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GOsasun_app.Interfazea.Oinarriak_UI.MenuTxartelBotoia btnNeurketak;
        private GOsasun_app.Interfazea.Oinarriak_UI.MenuTxartelBotoia btnErrezetak;
        private GOsasun_app.Interfazea.Oinarriak_UI.MenuTxartelBotoia btnGrafikak;
        private GOsasun_app.Interfazea.Oinarriak_UI.MenuTxartelBotoia btnDokumentuak;
        private GOsasun_app.Interfazea.Oinarriak_UI.MenuTxartelBotoia btnHitzorduak;
    }
}

