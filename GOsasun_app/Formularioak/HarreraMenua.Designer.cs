using System.Drawing;
using System.Windows.Forms;

namespace GOsasun_app.Formularioak
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
            btnPazienteak = new GOsasun_app.Kontrolak.CustomCardButton();
            btnMedikuak = new GOsasun_app.Kontrolak.CustomCardButton();
            btnLangileak = new GOsasun_app.Kontrolak.CustomCardButton();
            btnHitzorduak = new GOsasun_app.Kontrolak.CustomCardButton();
            SuspendLayout();
            // 
            // _goiburuBarra
            // 
            _goiburuBarra.Size = new Size(1902, 181);
            // 
            // btnPazienteak
            // 
            this.btnPazienteak.BackColor = Color.White;
            this.btnPazienteak.BorderBiribiltasuna = 24;
            this.btnPazienteak.Ikonoa = Image.FromFile(@"img\icons\pazienteak.png");
            this.btnPazienteak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            this.btnPazienteak.Location = new Point(20, 20);
            this.btnPazienteak.Margin = new Padding(20);
            this.btnPazienteak.Name = "btnPazienteak";
            this.btnPazienteak.Size = new Size(300, 200);
            this.btnPazienteak.TabIndex = 0;
            this.btnPazienteak.Testua = "PAZIENTEAK KUDEATU";
            // 
            // btnMedikuak
            // 
            this.btnMedikuak.BackColor = Color.White;
            this.btnMedikuak.BorderBiribiltasuna = 24;
            this.btnMedikuak.Ikonoa = Image.FromFile(@"img\icons\medikuak.png");
            this.btnMedikuak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            this.btnMedikuak.Location = new Point(340, 20);
            this.btnMedikuak.Margin = new Padding(20);
            this.btnMedikuak.Name = "btnMedikuak";
            this.btnMedikuak.Size = new Size(300, 200);
            this.btnMedikuak.TabIndex = 1;
            this.btnMedikuak.Testua = "MEDIKUAK KUDEATU";
            // 
            // btnLangileak
            // 
            this.btnLangileak.BackColor = Color.White;
            this.btnLangileak.BorderBiribiltasuna = 24;
            this.btnLangileak.Ikonoa = Image.FromFile(@"img\icons\langileak.png");
            this.btnLangileak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            this.btnLangileak.Location = new Point(660, 20);
            this.btnLangileak.Margin = new Padding(20);
            this.btnLangileak.Name = "btnLangileak";
            this.btnLangileak.Size = new Size(300, 200);
            this.btnLangileak.TabIndex = 2;
            this.btnLangileak.Testua = "LANGILEAK KUDEATU";
            // 
            // btnHitzorduak
            // 
            this.btnHitzorduak.BackColor = Color.White;
            this.btnHitzorduak.BorderBiribiltasuna = 24;
            this.btnHitzorduak.Ikonoa = Image.FromFile(@"img\icons\hitzorduak.png");
            this.btnHitzorduak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            this.btnHitzorduak.Location = new Point(20, 240);
            this.btnHitzorduak.Margin = new Padding(20);
            this.btnHitzorduak.Name = "btnHitzorduak";
            this.btnHitzorduak.Size = new Size(300, 200);
            this.btnHitzorduak.TabIndex = 3;
            this.btnHitzorduak.Testua = "HITZORDUAK KUDEATU";
            // 
            // HarreraMenua
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1280);
            Margin = new Padding(11, 9, 11, 9);
            Name = "HarreraMenua";
            Text = "GOsasun - Harrera Menua";
            ResumeLayout(false);
        }

        #endregion

        private GOsasun_app.Kontrolak.CustomCardButton btnPazienteak;
        private GOsasun_app.Kontrolak.CustomCardButton btnMedikuak;
        private GOsasun_app.Kontrolak.CustomCardButton btnLangileak;
        private GOsasun_app.Kontrolak.CustomCardButton btnHitzorduak;
    }
}
