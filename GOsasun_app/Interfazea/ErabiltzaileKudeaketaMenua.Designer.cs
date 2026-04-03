using System.Drawing;
using System.Windows.Forms;

namespace GOsasun_app.Interfazea
{
    partial class ErabiltzaileKudeaketaMenua
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
            this.btnSortu = new GOsasun_app.Interfazea.Kontrolak.CustomCardButton();
            this.btnZerrendatu = new GOsasun_app.Interfazea.Kontrolak.CustomCardButton();
            this._edukiPanela.SuspendLayout();
            this.SuspendLayout();
            // 
            // _edukiPanela
            // 
            this._edukiPanela.Controls.Add(this.btnSortu);
            this._edukiPanela.Controls.Add(this.btnZerrendatu);
            // 
            // _atzeraBotoia
            // 
            this._atzeraBotoia.FlatAppearance.BorderSize = 0;
            // 
            // btnSortu
            // 
            this.btnSortu.BackColor = Color.White;
            this.btnSortu.BorderBiribiltasuna = 24;
            this.btnSortu.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            this.btnSortu.Location = new Point(37, 43);
            this.btnSortu.Name = "btnSortu";
            this.btnSortu.Padding = new Padding(19, 21, 19, 21);
            this.btnSortu.Size = new Size(576, 512);
            this.btnSortu.TabIndex = 0;
            this.btnSortu.Testua = "SORTU";
            this.btnSortu.TestuKolorea = Color.FromArgb(50, 50, 50);
            // 
            // btnZerrendatu
            // 
            this.btnZerrendatu.BackColor = Color.White;
            this.btnZerrendatu.BorderBiribiltasuna = 24;
            this.btnZerrendatu.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            this.btnZerrendatu.Location = new Point(650, 43);
            this.btnZerrendatu.Name = "btnZerrendatu";
            this.btnZerrendatu.Padding = new Padding(19, 21, 19, 21);
            this.btnZerrendatu.Size = new Size(576, 512);
            this.btnZerrendatu.TabIndex = 1;
            this.btnZerrendatu.Testua = "ZERRENDATU";
            this.btnZerrendatu.TestuKolorea = Color.FromArgb(50, 50, 50);
            // 
            // ErabiltzaileKudeaketaMenua
            // 
            this.AutoScaleDimensions = new SizeF(13F, 32F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1902, 1340);
            this.Name = "ErabiltzaileKudeaketaMenua";
            this.Text = "Kudeaketa Menua";
            this._edukiPanela.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private GOsasun_app.Interfazea.Kontrolak.CustomCardButton btnSortu;
        private GOsasun_app.Interfazea.Kontrolak.CustomCardButton btnZerrendatu;
    }
}
