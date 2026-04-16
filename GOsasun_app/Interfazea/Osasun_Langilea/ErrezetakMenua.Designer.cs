using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace GOsasun_app.Interfazea
{
    partial class ErrezetakMenua
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

        private void InitializeComponent()
        {
            btnErrezetaSortu = new GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia();
            btnErrezetakIkusi = new GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia();
            _edukiPanela.SuspendLayout();
            SuspendLayout();
            //
            // _edukiPanela
            //
            _edukiPanela.Controls.Add(btnErrezetaSortu);
            _edukiPanela.Controls.Add(btnErrezetakIkusi);
            _edukiPanela.Size = new Size(1902, 1153);
            //
            // btnErrezetaSortu
            //
            btnErrezetaSortu.BackColor = Color.White;
            btnErrezetaSortu.BorderBiribiltasuna = 24;
            btnErrezetaSortu.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnErrezetaSortu.Location = new Point(300, 300);
            btnErrezetaSortu.Margin = new Padding(20);
            btnErrezetaSortu.Name = "btnErrezetaSortu";
            btnErrezetaSortu.Padding = new Padding(10);
            btnErrezetaSortu.Size = new Size(550, 450);
            btnErrezetaSortu.TabIndex = 0;
            btnErrezetaSortu.Testua = "ERREZETA SORTU";
            //
            // btnErrezetakIkusi
            //
            btnErrezetakIkusi.BackColor = Color.White;
            btnErrezetakIkusi.BorderBiribiltasuna = 24;
            btnErrezetakIkusi.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnErrezetakIkusi.Location = new Point(1000, 300);
            btnErrezetakIkusi.Margin = new Padding(20);
            btnErrezetakIkusi.Name = "btnErrezetakIkusi";
            btnErrezetakIkusi.Padding = new Padding(10);
            btnErrezetakIkusi.Size = new Size(550, 450);
            btnErrezetakIkusi.TabIndex = 2;
            btnErrezetakIkusi.Testua = "ERREZETAK IKUSI";
            //
            // ErrezetakMenua
            //
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1334);
            Name = "ErrezetakMenua";
            Text = "GOsasun - Errezetak Kudeatu";
            _edukiPanela.ResumeLayout(false);
            ResumeLayout(false);
        }

        private GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia btnErrezetaSortu;
        private GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia btnErrezetakIkusi;
    }
}
