using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace GOsasun_app.Formularioak
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(PazienteMenua));
            btnNeurketak = new GOsasun_app.Kontrolak.CustomCardButton();
            btnErrezetak = new GOsasun_app.Kontrolak.CustomCardButton();
            btnKontaktua = new GOsasun_app.Kontrolak.CustomCardButton();
            btnGrafikak = new GOsasun_app.Kontrolak.CustomCardButton();
            btnAbisuak = new GOsasun_app.Kontrolak.CustomCardButton();
            _edukiPanela.SuspendLayout();
            SuspendLayout();
            // 
            // _edukiPanela
            // 
            _edukiPanela.Controls.Add(btnNeurketak);
            _edukiPanela.Controls.Add(btnErrezetak);
            _edukiPanela.Controls.Add(btnKontaktua);
            _edukiPanela.Controls.Add(btnGrafikak);
            _edukiPanela.Controls.Add(btnAbisuak);
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
            btnNeurketak.Ikonoa = (Image)resources.GetObject("btnNeurketak.Ikonoa");
            btnNeurketak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnNeurketak.Location = new Point(37, 43);
            btnNeurketak.Margin = new Padding(37, 43, 37, 43);
            btnNeurketak.Name = "btnNeurketak";
            btnNeurketak.Padding = new Padding(19, 21, 19, 21);
            btnNeurketak.Size = new Size(576, 512);
            btnNeurketak.TabIndex = 0;
            btnNeurketak.Testua = "NIRE NEURKETAK";
            // 
            // btnErrezetak
            // 
            btnErrezetak.BackColor = Color.White;
            btnErrezetak.BorderBiribiltasuna = 24;
            btnErrezetak.Ikonoa = (Image)resources.GetObject("btnErrezetak.Ikonoa");
            btnErrezetak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnErrezetak.Location = new Point(650, 43);
            btnErrezetak.Margin = new Padding(37, 43, 37, 43);
            btnErrezetak.Name = "btnErrezetak";
            btnErrezetak.Padding = new Padding(19, 21, 19, 21);
            btnErrezetak.Size = new Size(576, 512);
            btnErrezetak.TabIndex = 1;
            btnErrezetak.Testua = "NIRE ERREZETAK";
            // 
            // btnKontaktua
            // 
            btnKontaktua.BackColor = Color.White;
            btnKontaktua.BorderBiribiltasuna = 24;
            btnKontaktua.Ikonoa = (Image)resources.GetObject("btnKontaktua.Ikonoa");
            btnKontaktua.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnKontaktua.Location = new Point(1263, 43);
            btnKontaktua.Margin = new Padding(37, 43, 37, 43);
            btnKontaktua.Name = "btnKontaktua";
            btnKontaktua.Padding = new Padding(19, 21, 19, 21);
            btnKontaktua.Size = new Size(576, 512);
            btnKontaktua.TabIndex = 2;
            btnKontaktua.Testua = "KONTAKTUA";
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
            // btnAbisuak
            // 
            btnAbisuak.BackColor = Color.White;
            btnAbisuak.BorderBiribiltasuna = 24;
            btnAbisuak.Ikonoa = (Image)resources.GetObject("btnAbisuak.Ikonoa");
            btnAbisuak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnAbisuak.Location = new Point(650, 597);
            btnAbisuak.Margin = new Padding(37, 43, 37, 43);
            btnAbisuak.Name = "btnAbisuak";
            btnAbisuak.Padding = new Padding(19, 21, 19, 21);
            btnAbisuak.Size = new Size(576, 512);
            btnAbisuak.TabIndex = 4;
            btnAbisuak.Testua = "ABISUAK";
            // 
            // PazienteMenua
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1334);
            Margin = new Padding(11, 9, 11, 9);
            Name = "PazienteMenua";
            Text = "GOsasun - Paziente Menua";
            _edukiPanela.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GOsasun_app.Kontrolak.CustomCardButton btnNeurketak;
        private GOsasun_app.Kontrolak.CustomCardButton btnErrezetak;
        private GOsasun_app.Kontrolak.CustomCardButton btnKontaktua;
        private GOsasun_app.Kontrolak.CustomCardButton btnGrafikak;
        private GOsasun_app.Kontrolak.CustomCardButton btnAbisuak;
    }
}
