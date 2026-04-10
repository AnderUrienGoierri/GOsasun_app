using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace GOsasun_app.Interfazea
{
    partial class NeurketaMotak
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(NeurketaMotak));
            btnTentsiometroa = new GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia();
            btnPisua = new GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia();
            btnAltuera = new GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia();
            _edukiPanela.SuspendLayout();
            SuspendLayout();
            // 
            // _edukiPanela
            // 
            _edukiPanela.Controls.Add(btnTentsiometroa);
            _edukiPanela.Controls.Add(btnPisua);
            _edukiPanela.Controls.Add(btnAltuera);
            _edukiPanela.Size = new Size(1902, 1153);
            _edukiPanela.Paint += _edukiPanela_Paint;
            // 
            // _atzeraBotoia
            // 
            _atzeraBotoia.FlatAppearance.BorderSize = 0;
            // 
            // btnTentsiometroa
            // 
            btnTentsiometroa.BackColor = Color.White;
            btnTentsiometroa.BorderBiribiltasuna = 24;
            btnTentsiometroa.Ikonoa = (Image)resources.GetObject("btnTentsiometroa.Ikonoa");
            btnTentsiometroa.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnTentsiometroa.Location = new Point(80, 300);
            btnTentsiometroa.Margin = new Padding(20);
            btnTentsiometroa.Name = "btnTentsiometroa";
            btnTentsiometroa.Padding = new Padding(10);
            btnTentsiometroa.Size = new Size(550, 450);
            btnTentsiometroa.TabIndex = 0;
            btnTentsiometroa.Testua = "TENTSIOMETROA";
            // 
            // btnPisua
            // 
            btnPisua.BackColor = Color.White;
            btnPisua.BorderBiribiltasuna = 24;
            btnPisua.Ikonoa = (Image)resources.GetObject("btnPisua.Ikonoa");
            btnPisua.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnPisua.Location = new Point(670, 300);
            btnPisua.Margin = new Padding(20);
            btnPisua.Name = "btnPisua";
            btnPisua.Padding = new Padding(10);
            btnPisua.Size = new Size(550, 450);
            btnPisua.TabIndex = 2;
            btnPisua.Testua = "PISUA";
            // 
            // btnAltuera
            // 
            btnAltuera.BackColor = Color.White;
            btnAltuera.BorderBiribiltasuna = 24;
            btnAltuera.Ikonoa = (Image)resources.GetObject("btnAltuera.Ikonoa");
            btnAltuera.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnAltuera.Location = new Point(1260, 300);
            btnAltuera.Margin = new Padding(20);
            btnAltuera.Name = "btnAltuera";
            btnAltuera.Padding = new Padding(10);
            btnAltuera.Size = new Size(550, 450);
            btnAltuera.TabIndex = 3;
            btnAltuera.Testua = "ALTUERA";
            // 
            // NeurketaMotak
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1334);
            Name = "NeurketaMotak";
            Text = "GOsasun - Neurketa Motak";
            _edukiPanela.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia btnTentsiometroa;
        private GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia btnPisua;
        private GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia btnAltuera;
    }
}
