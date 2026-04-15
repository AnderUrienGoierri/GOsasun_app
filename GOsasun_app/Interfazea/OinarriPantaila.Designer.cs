namespace GOsasun_app.Interfazea
{
    partial class OinarriPantaila
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
            _edukiPanela = new Panel();
            _goiburuBarra = new GOsasun_app.Interfazea.Kontrolak.GoiburuBarra();
            _atzeraBotoia = new Button();
            SuspendLayout();
            //
            // _edukiPanela
            //
            _edukiPanela.AutoScroll = true;
            _edukiPanela.BackColor = Color.Transparent;
            _edukiPanela.Dock = DockStyle.Fill;
            _edukiPanela.Location = new Point(0, 181);
            _edukiPanela.Name = "_edukiPanela";
            _edukiPanela.Padding = new Padding(2);
            _edukiPanela.Size = new Size(1902, 1213);
            _edukiPanela.TabIndex = 0;
            //
            // _goiburuBarra
            //
            _goiburuBarra.BackColor = Color.FromArgb(44, 62, 80);
            _goiburuBarra.Dock = DockStyle.Top;
            _goiburuBarra.Location = new Point(0, 0);
            _goiburuBarra.Name = "_goiburuBarra";
            _goiburuBarra.Padding = new Padding(20, 10, 20, 10);
            _goiburuBarra.Size = new Size(1902, 181);
            _goiburuBarra.TabIndex = 1;
            //
            // _atzeraBotoia
            //
            _atzeraBotoia.BackColor = Color.FromArgb(180, 52, 73, 94);
            _atzeraBotoia.Cursor = Cursors.Hand;
            _atzeraBotoia.FlatAppearance.BorderSize = 0;
            _atzeraBotoia.FlatStyle = FlatStyle.Flat;
            _atzeraBotoia.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            _atzeraBotoia.ForeColor = Color.White;
            _atzeraBotoia.Location = new Point(40, 93);
            _atzeraBotoia.Name = "_atzeraBotoia";
            _atzeraBotoia.Size = new Size(250, 59);
            _atzeraBotoia.TabIndex = 2;
            _atzeraBotoia.Text = " ⬅  Atzera";
            _atzeraBotoia.UseVisualStyleBackColor = false;
            //
            // OinarriPantaila
            //
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(214, 224, 229);
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(1902, 1394);
            Controls.Add(_atzeraBotoia);
            Controls.Add(_edukiPanela);
            Controls.Add(_goiburuBarra);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(6, 4, 6, 4);
            MaximizeBox = false;
            Name = "OinarriPantaila";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GOsasun";
            ResumeLayout(false);
        }

        #endregion

        protected Panel _edukiPanela;
        protected GOsasun_app.Interfazea.Kontrolak.GoiburuBarra _goiburuBarra;
        protected Button _atzeraBotoia;
    }
}
