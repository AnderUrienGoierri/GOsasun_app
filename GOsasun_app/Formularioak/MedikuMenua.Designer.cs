using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MedikuMenua));
            btnPazienteak = new GOsasun_app.Kontrolak.CustomCardButton();
            btnKontaktua = new GOsasun_app.Kontrolak.CustomCardButton();
            btnNeurketak = new GOsasun_app.Kontrolak.CustomCardButton();
            btnErrezetak = new GOsasun_app.Kontrolak.CustomCardButton();
            btnGrafikak = new GOsasun_app.Kontrolak.CustomCardButton();
            btnAbisuak = new GOsasun_app.Kontrolak.CustomCardButton();
            _edukiPanela.SuspendLayout();
            SuspendLayout();
            // 
            // _edukiPanela
            // 
            _edukiPanela.Controls.Add(btnPazienteak);
            _edukiPanela.Controls.Add(btnKontaktua);
            _edukiPanela.Controls.Add(btnNeurketak);
            _edukiPanela.Controls.Add(btnErrezetak);
            _edukiPanela.Controls.Add(btnGrafikak);
            _edukiPanela.Controls.Add(btnAbisuak);
            _edukiPanela.Size = new Size(1570, 871);
            // 
            // _goiburuBarra
            // 
            _goiburuBarra.Size = new Size(1570, 181);
            // 
            // _atzeraBotoia
            // 
            _atzeraBotoia.FlatAppearance.BorderSize = 0;
            _atzeraBotoia.Visible = false;
            // 
            // btnPazienteak
            // 
            btnPazienteak.BackColor = Color.White;
            btnPazienteak.BorderBiribiltasuna = 24;
            btnPazienteak.Ikonoa = (Image)resources.GetObject("btnPazienteak.Ikonoa");
            btnPazienteak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnPazienteak.Location = new Point(20, 20);
            btnPazienteak.Margin = new Padding(20);
            btnPazienteak.Name = "btnPazienteak";
            btnPazienteak.Padding = new Padding(10);
            btnPazienteak.Size = new Size(529, 330);
            btnPazienteak.TabIndex = 0;
            btnPazienteak.Testua = "NIRE PAZIENTEAK";
            // 
            // btnKontaktua
            // 
            btnKontaktua.BackColor = Color.White;
            btnKontaktua.BorderBiribiltasuna = 24;
            btnKontaktua.Ikonoa = (Image)resources.GetObject("btnKontaktua.Ikonoa");
            btnKontaktua.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnKontaktua.Location = new Point(573, 23);
            btnKontaktua.Margin = new Padding(20);
            btnKontaktua.Name = "btnKontaktua";
            btnKontaktua.Padding = new Padding(10);
            btnKontaktua.Size = new Size(487, 327);
            btnKontaktua.TabIndex = 1;
            btnKontaktua.Testua = "KONTAKTUA";
            // 
            // btnNeurketak
            // 
            btnNeurketak.BackColor = Color.White;
            btnNeurketak.BorderBiribiltasuna = 24;
            btnNeurketak.Ikonoa = (Image)resources.GetObject("btnNeurketak.Ikonoa");
            btnNeurketak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnNeurketak.Location = new Point(1080, 23);
            btnNeurketak.Margin = new Padding(20);
            btnNeurketak.Name = "btnNeurketak";
            btnNeurketak.Padding = new Padding(10);
            btnNeurketak.Size = new Size(468, 335);
            btnNeurketak.TabIndex = 2;
            btnNeurketak.Testua = "NEURKETAK";
            // 
            // btnErrezetak
            // 
            btnErrezetak.BackColor = Color.White;
            btnErrezetak.BorderBiribiltasuna = 24;
            btnErrezetak.Ikonoa = (Image)resources.GetObject("btnErrezetak.Ikonoa");
            btnErrezetak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnErrezetak.Location = new Point(29, 407);
            btnErrezetak.Margin = new Padding(20);
            btnErrezetak.Name = "btnErrezetak";
            btnErrezetak.Padding = new Padding(10);
            btnErrezetak.Size = new Size(520, 322);
            btnErrezetak.TabIndex = 3;
            btnErrezetak.Testua = "ERREZETAK";
            // 
            // btnGrafikak
            // 
            btnGrafikak.BackColor = Color.White;
            btnGrafikak.BorderBiribiltasuna = 24;
            btnGrafikak.Ikonoa = (Image)resources.GetObject("btnGrafikak.Ikonoa");
            btnGrafikak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnGrafikak.Location = new Point(573, 407);
            btnGrafikak.Margin = new Padding(20);
            btnGrafikak.Name = "btnGrafikak";
            btnGrafikak.Padding = new Padding(10);
            btnGrafikak.Size = new Size(492, 322);
            btnGrafikak.TabIndex = 4;
            btnGrafikak.Testua = "GRAFIKAK";
            // 
            // btnAbisuak
            // 
            btnAbisuak.BackColor = Color.White;
            btnAbisuak.BorderBiribiltasuna = 24;
            btnAbisuak.Ikonoa = (Image)resources.GetObject("btnAbisuak.Ikonoa");
            btnAbisuak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnAbisuak.Location = new Point(1080, 407);
            btnAbisuak.Margin = new Padding(20);
            btnAbisuak.Name = "btnAbisuak";
            btnAbisuak.Padding = new Padding(10);
            btnAbisuak.Size = new Size(468, 322);
            btnAbisuak.TabIndex = 5;
            btnAbisuak.Testua = "ABISUAK";
            // 
            // MedikuMenua
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1570, 1052);
            Name = "MedikuMenua";
            Text = "GOsasun - Mediku Menua";
            _edukiPanela.ResumeLayout(false);
            ResumeLayout(false);
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
