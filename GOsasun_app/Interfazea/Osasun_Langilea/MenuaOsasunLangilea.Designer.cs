using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace GOsasun_app.Interfazea
{
    partial class MenuaOsasunLangilea
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MenuaOsasunLangilea));
            btnPazienteak = new GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia();
            btnNeurketak = new GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia();
            btnErrezetak = new GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia();
            btnGrafikak = new GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia();
            _edukiPanela.SuspendLayout();
            SuspendLayout();
            // 
            // _edukiPanela
            // 
            _edukiPanela.Controls.Add(btnPazienteak);
            _edukiPanela.Controls.Add(btnNeurketak);
            _edukiPanela.Controls.Add(btnErrezetak);
            _edukiPanela.Controls.Add(btnGrafikak);
            _edukiPanela.Size = new Size(1902, 1153);
            // 
            // btnPazienteak
            // 
            btnPazienteak.BackColor = Color.White;
            btnPazienteak.BorderBiribiltasuna = 24;
            btnPazienteak.Ikonoa = (Image)resources.GetObject("btnPazienteak.Ikonoa");
            btnPazienteak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnPazienteak.Location = new Point(37, 43);
            btnPazienteak.Margin = new Padding(37, 43, 37, 43);
            btnPazienteak.Name = "btnPazienteak";
            btnPazienteak.Padding = new Padding(19, 21, 19, 21);
            btnPazienteak.Size = new Size(576, 512);
            btnPazienteak.TabIndex = 0;
            btnPazienteak.Testua = "NIRE PAZIENTEAK";
            // 
            // btnNeurketak
            // 
            btnNeurketak.BackColor = Color.White;
            btnNeurketak.BorderBiribiltasuna = 24;
            btnNeurketak.Ikonoa = (Image)resources.GetObject("btnNeurketak.Ikonoa");
            btnNeurketak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnNeurketak.Location = new Point(650, 43);
            btnNeurketak.Margin = new Padding(37, 43, 37, 43);
            btnNeurketak.Name = "btnNeurketak";
            btnNeurketak.Padding = new Padding(19, 21, 19, 21);
            btnNeurketak.Size = new Size(576, 512);
            btnNeurketak.TabIndex = 1;
            btnNeurketak.Testua = "JARRAIPENAK";
            // 
            // btnErrezetak
            // 
            btnErrezetak.BackColor = Color.White;
            btnErrezetak.BorderBiribiltasuna = 24;
            btnErrezetak.Ikonoa = (Image)resources.GetObject("btnErrezetak.Ikonoa");
            btnErrezetak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnErrezetak.Location = new Point(1263, 43);
            btnErrezetak.Margin = new Padding(37, 43, 37, 43);
            btnErrezetak.Name = "btnErrezetak";
            btnErrezetak.Padding = new Padding(19, 21, 19, 21);
            btnErrezetak.Size = new Size(576, 512);
            btnErrezetak.TabIndex = 2;
            btnErrezetak.Testua = "ERREZETAK";
            // 
            // btnGrafikak
            // 
            btnGrafikak.BackColor = Color.White;
            btnGrafikak.BorderBiribiltasuna = 24;
            btnGrafikak.Ikonoa = (Image)resources.GetObject("btnGrafikak.Ikonoa");
            btnGrafikak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnGrafikak.Location = new Point(37, 597);
            btnGrafikak.Margin = new Padding(37, 43, 37, 43);
            btnGrafikak.Name = "btnGrafikak";
            btnGrafikak.Padding = new Padding(19, 21, 19, 21);
            btnGrafikak.Size = new Size(576, 512);
            btnGrafikak.TabIndex = 3;
            btnGrafikak.Testua = "GRAFIKAK";
            // 
            // MenuaOsasunLangilea
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1334);
            Margin = new Padding(11, 9, 11, 9);
            Name = "MenuaOsasunLangilea";
            Text = "GOsasun - Osasun Langilearen Menua";
            Load += MedikuMenua_Load;
            _edukiPanela.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia btnPazienteak;
        private GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia btnNeurketak;
        private GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia btnErrezetak;
        private GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia btnGrafikak;
    }
}
