using System.Drawing;
using System.Windows.Forms;

namespace GOsasun_app.Formularioak
{
    partial class MedikuMenua
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
            this.btnKontaktua = new GOsasun_app.Kontrolak.CustomCardButton();
            this.btnNeurketak = new GOsasun_app.Kontrolak.CustomCardButton();
            this.btnErrezetak = new GOsasun_app.Kontrolak.CustomCardButton();
            this.btnGrafikak = new GOsasun_app.Kontrolak.CustomCardButton();
            this.btnAbisuak = new GOsasun_app.Kontrolak.CustomCardButton();
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
            this.btnPazienteak.Testua = "NIRE PAZIENTEAK";
            // 
            // btnKontaktua
            // 
            this.btnKontaktua.BackColor = Color.White;
            this.btnKontaktua.BorderBiribiltasuna = 24;
            this.btnKontaktua.Ikonoa = null;
            this.btnKontaktua.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            this.btnKontaktua.Location = new Point(340, 20);
            this.btnKontaktua.Margin = new Padding(20);
            this.btnKontaktua.Name = "btnKontaktua";
            this.btnKontaktua.Size = new Size(300, 200);
            this.btnKontaktua.TabIndex = 1;
            this.btnKontaktua.Testua = "KONTAKTUA";
            // 
            // btnNeurketak
            // 
            this.btnNeurketak.BackColor = Color.White;
            this.btnNeurketak.BorderBiribiltasuna = 24;
            this.btnNeurketak.Ikonoa = null;
            this.btnNeurketak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            this.btnNeurketak.Location = new Point(660, 20);
            this.btnNeurketak.Margin = new Padding(20);
            this.btnNeurketak.Name = "btnNeurketak";
            this.btnNeurketak.Size = new Size(300, 200);
            this.btnNeurketak.TabIndex = 2;
            this.btnNeurketak.Testua = "NEURKETAK";
            // 
            // btnErrezetak
            // 
            this.btnErrezetak.BackColor = Color.White;
            this.btnErrezetak.BorderBiribiltasuna = 24;
            this.btnErrezetak.Ikonoa = null;
            this.btnErrezetak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            this.btnErrezetak.Location = new Point(20, 240);
            this.btnErrezetak.Margin = new Padding(20);
            this.btnErrezetak.Name = "btnErrezetak";
            this.btnErrezetak.Size = new Size(300, 200);
            this.btnErrezetak.TabIndex = 3;
            this.btnErrezetak.Testua = "ERREZETAK";
            // 
            // btnGrafikak
            // 
            this.btnGrafikak.BackColor = Color.White;
            this.btnGrafikak.BorderBiribiltasuna = 24;
            this.btnGrafikak.Ikonoa = null;
            this.btnGrafikak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            this.btnGrafikak.Location = new Point(340, 240);
            this.btnGrafikak.Margin = new Padding(20);
            this.btnGrafikak.Name = "btnGrafikak";
            this.btnGrafikak.Size = new Size(300, 200);
            this.btnGrafikak.TabIndex = 4;
            this.btnGrafikak.Testua = "GRAFIKAK";
            // 
            // btnAbisuak
            // 
            this.btnAbisuak.BackColor = Color.White;
            this.btnAbisuak.BorderBiribiltasuna = 24;
            this.btnAbisuak.Ikonoa = null;
            this.btnAbisuak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            this.btnAbisuak.Location = new Point(660, 240);
            this.btnAbisuak.Margin = new Padding(20);
            this.btnAbisuak.Name = "btnAbisuak";
            this.btnAbisuak.Size = new Size(300, 200);
            this.btnAbisuak.TabIndex = 5;
            this.btnAbisuak.Testua = "ABISUAK";
            // 
            // MedikuMenua
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1024, 600);
            this.Name = "MedikuMenua";
            this.Text = "GOsasun - Mediku Menua";
            
            this._edukiPanela.Controls.Add(this.btnPazienteak);
            this._edukiPanela.Controls.Add(this.btnKontaktua);
            this._edukiPanela.Controls.Add(this.btnNeurketak);
            this._edukiPanela.Controls.Add(this.btnErrezetak);
            this._edukiPanela.Controls.Add(this.btnGrafikak);
            this._edukiPanela.Controls.Add(this.btnAbisuak);
            
            this.ResumeLayout(false);
        }

        #endregion

        private GOsasun_app.Kontrolak.CustomCardButton btnPazienteak;
        private GOsasun_app.Kontrolak.CustomCardButton btnKontaktua;
        private GOsasun_app.Kontrolak.CustomCardButton btnNeurketak;
        private GOsasun_app.Kontrolak.CustomCardButton btnErrezetak;
        private GOsasun_app.Kontrolak.CustomCardButton btnGrafikak;
        private GOsasun_app.Kontrolak.CustomCardButton btnAbisuak;
    }
}
