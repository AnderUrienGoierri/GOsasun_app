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
            this.btnPazienteak = new GOsasun_app.Kontrolak.CustomCardButton();
            this.btnMedikuak = new GOsasun_app.Kontrolak.CustomCardButton();
            this.btnLangileak = new GOsasun_app.Kontrolak.CustomCardButton();
            this.btnHitzorduak = new GOsasun_app.Kontrolak.CustomCardButton();
            this.SuspendLayout();
            // 
            // btnPazienteak
            // 
            this.btnPazienteak.BackColor = Color.White;
            this.btnPazienteak.BorderBiribiltasuna = 24;
            this.btnPazienteak.Ikonoa = null;
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
            this.btnMedikuak.Ikonoa = null;
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
            this.btnLangileak.Ikonoa = null;
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
            this.btnHitzorduak.Ikonoa = null;
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
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1024, 600);
            this.Name = "HarreraMenua";
            this.Text = "GOsasun - Harrera Menua";
            
            this._edukiPanela.Controls.Add(this.btnPazienteak);
            this._edukiPanela.Controls.Add(this.btnMedikuak);
            this._edukiPanela.Controls.Add(this.btnLangileak);
            this._edukiPanela.Controls.Add(this.btnHitzorduak);
            
            this.ResumeLayout(false);
        }

        #endregion

        private GOsasun_app.Kontrolak.CustomCardButton btnPazienteak;
        private GOsasun_app.Kontrolak.CustomCardButton btnMedikuak;
        private GOsasun_app.Kontrolak.CustomCardButton btnLangileak;
        private GOsasun_app.Kontrolak.CustomCardButton btnHitzorduak;
    }
}
