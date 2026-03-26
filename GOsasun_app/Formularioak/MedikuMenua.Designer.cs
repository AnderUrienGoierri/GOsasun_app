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
            btnNeurketak.SuspendLayout();
            btnErrezetak.SuspendLayout();
            SuspendLayout();
            // 
            // _goiburuBarra
            // 
            _goiburuBarra.Size = new Size(1902, 181);
            // 
            // btnPazienteak
            // 
            btnPazienteak.BackColor = Color.White;
            btnPazienteak.BorderBiribiltasuna = 24;
            btnPazienteak.Ikonoa = null;
            btnPazienteak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnPazienteak.Location = new Point(492, 220);
            btnPazienteak.Margin = new Padding(28, 32, 28, 32);
            btnPazienteak.Name = "btnPazienteak";
            btnPazienteak.Padding = new Padding(19, 21, 19, 21);
            btnPazienteak.Size = new Size(520, 384);
            btnPazienteak.TabIndex = 0;
            btnPazienteak.Testua = "NIRE PAZIENTEAK";
            // 
            // btnKontaktua
            // 
            btnKontaktua.BackColor = Color.White;
            btnKontaktua.BorderBiribiltasuna = 24;
            btnKontaktua.Ikonoa = null;
            btnKontaktua.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnKontaktua.Location = new Point(0, 220);
            btnKontaktua.Margin = new Padding(28, 32, 28, 32);
            btnKontaktua.Name = "btnKontaktua";
            btnKontaktua.Padding = new Padding(19, 21, 19, 21);
            btnKontaktua.Size = new Size(520, 384);
            btnKontaktua.TabIndex = 1;
            btnKontaktua.Testua = "KONTAKTUA";
            // 
            // btnNeurketak
            // 
            btnNeurketak.BackColor = Color.White;
            btnNeurketak.BorderBiribiltasuna = 24;
            btnNeurketak.Controls.Add(btnPazienteak);
            btnNeurketak.Ikonoa = null;
            btnNeurketak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnNeurketak.Location = new Point(469, 1028);
            btnNeurketak.Margin = new Padding(28, 32, 28, 32);
            btnNeurketak.Name = "btnNeurketak";
            btnNeurketak.Padding = new Padding(19, 21, 19, 21);
            btnNeurketak.Size = new Size(520, 384);
            btnNeurketak.TabIndex = 2;
            btnNeurketak.Testua = "NEURKETAK";
            // 
            // btnErrezetak
            // 
            btnErrezetak.BackColor = Color.White;
            btnErrezetak.BorderBiribiltasuna = 24;
            btnErrezetak.Controls.Add(btnKontaktua);
            btnErrezetak.Ikonoa = null;
            btnErrezetak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnErrezetak.Location = new Point(469, 580);
            btnErrezetak.Margin = new Padding(28, 32, 28, 32);
            btnErrezetak.Name = "btnErrezetak";
            btnErrezetak.Padding = new Padding(19, 21, 19, 21);
            btnErrezetak.Size = new Size(520, 384);
            btnErrezetak.TabIndex = 3;
            btnErrezetak.Testua = "ERREZETAK";
            // 
            // btnGrafikak
            // 
            btnGrafikak.BackColor = Color.White;
            btnGrafikak.BorderBiribiltasuna = 24;
            btnGrafikak.Ikonoa = null;
            btnGrafikak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnGrafikak.Location = new Point(469, 1476);
            btnGrafikak.Margin = new Padding(28, 32, 28, 32);
            btnGrafikak.Name = "btnGrafikak";
            btnGrafikak.Padding = new Padding(19, 21, 19, 21);
            btnGrafikak.Size = new Size(520, 384);
            btnGrafikak.TabIndex = 4;
            btnGrafikak.Testua = "GRAFIKAK";
            btnGrafikak.Paint += btnGrafikak_Paint;
            // 
            // btnAbisuak
            // 
            btnAbisuak.BackColor = Color.White;
            btnAbisuak.BorderBiribiltasuna = 24;
            btnAbisuak.Ikonoa = null;
            btnAbisuak.KartaKolorea = Color.FromArgb(230, 255, 255, 255);
            btnAbisuak.Location = new Point(469, 132);
            btnAbisuak.Margin = new Padding(28, 32, 28, 32);
            btnAbisuak.Name = "btnAbisuak";
            btnAbisuak.Padding = new Padding(19, 21, 19, 21);
            btnAbisuak.Size = new Size(520, 384);
            btnAbisuak.TabIndex = 5;
            btnAbisuak.Testua = "ABISUAK";
            // 
            // MedikuMenua
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1280);
            Margin = new Padding(11, 9, 11, 9);
            Name = "MedikuMenua";
            Text = "GOsasun - Mediku Menua";
            btnNeurketak.ResumeLayout(false);
            btnErrezetak.ResumeLayout(false);
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
