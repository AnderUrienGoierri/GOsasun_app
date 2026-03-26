namespace GOsasun_app.Formularioak
{
    partial class MedikuMenua
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnPazienteak = new GOsasun_app.Kontrolak.CustomCardButton();
            btnKontaktua = new GOsasun_app.Kontrolak.CustomCardButton();
            btnNeurketak = new GOsasun_app.Kontrolak.CustomCardButton();
            btnErrezetak = new GOsasun_app.Kontrolak.CustomCardButton();
            btnGrafikak = new GOsasun_app.Kontrolak.CustomCardButton();
            btnAbisuak = new GOsasun_app.Kontrolak.CustomCardButton();
            this.SuspendLayout();
            // 
            // btnPazienteak
            // 
            btnPazienteak.Testua = "NIRE PAZIENTEAK";
            btnPazienteak.Size = new System.Drawing.Size(280, 180);
            btnPazienteak.Margin = new System.Windows.Forms.Padding(15);
            btnPazienteak.Name = "btnPazienteak";
            // 
            // btnKontaktua
            // 
            btnKontaktua.Testua = "KONTAKTUA";
            btnKontaktua.Size = new System.Drawing.Size(280, 180);
            btnKontaktua.Margin = new System.Windows.Forms.Padding(15);
            btnKontaktua.Name = "btnKontaktua";
            // 
            // btnNeurketak
            // 
            btnNeurketak.Testua = "NEURKETAK";
            btnNeurketak.Size = new System.Drawing.Size(280, 180);
            btnNeurketak.Margin = new System.Windows.Forms.Padding(15);
            btnNeurketak.Name = "btnNeurketak";
            // 
            // btnErrezetak
            // 
            btnErrezetak.Testua = "ERREZETAK";
            btnErrezetak.Size = new System.Drawing.Size(280, 180);
            btnErrezetak.Margin = new System.Windows.Forms.Padding(15);
            btnErrezetak.Name = "btnErrezetak";
            // 
            // btnGrafikak
            // 
            btnGrafikak.Testua = "GRAFIKAK";
            btnGrafikak.Size = new System.Drawing.Size(280, 180);
            btnGrafikak.Margin = new System.Windows.Forms.Padding(15);
            btnGrafikak.Name = "btnGrafikak";
            // 
            // btnAbisuak
            // 
            btnAbisuak.Testua = "ABISUAK";
            btnAbisuak.Size = new System.Drawing.Size(280, 180);
            btnAbisuak.Margin = new System.Windows.Forms.Padding(15);
            btnAbisuak.Name = "btnAbisuak";
            // 
            // MedikuMenua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 600);
            this._edukiPanela.Controls.Add(btnPazienteak);
            this._edukiPanela.Controls.Add(btnKontaktua);
            this._edukiPanela.Controls.Add(btnNeurketak);
            this._edukiPanela.Controls.Add(btnErrezetak);
            this._edukiPanela.Controls.Add(btnGrafikak);
            this._edukiPanela.Controls.Add(btnAbisuak);
            this.Name = "MedikuMenua";
            this.Text = "GOsasun - Mediku Menua";
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
