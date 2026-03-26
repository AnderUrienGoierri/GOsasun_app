namespace GOsasun_app.Formularioak
{
    partial class OinarriFormularioa
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
            _edukiPanela = new FlowLayoutPanel();
            _goiburuBarra = new GOsasun_app.Kontrolak.GoiburuBarra("Erabiltzailea", "Rola");
            SuspendLayout();
            // 
            // _edukiPanela
            // 
            _edukiPanela.AutoScroll = true;
            _edukiPanela.BackColor = Color.Transparent;
            _edukiPanela.Dock = DockStyle.Fill;
            _edukiPanela.Location = new Point(0, 0);
            _edukiPanela.Name = "_edukiPanela";
            _edukiPanela.Padding = new Padding(40);
            _edukiPanela.Size = new Size(1024, 600);
            _edukiPanela.TabIndex = 0;
            // 
            // OinarriFormularioa
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1024, 600);
            Controls.Add(_goiburuBarra);
            Controls.Add(_edukiPanela);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "OinarriFormularioa";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GOsasun";
            ResumeLayout(false);
        }

        #endregion

        protected FlowLayoutPanel _edukiPanela;
        protected GOsasun_app.Kontrolak.GoiburuBarra _goiburuBarra;
    }
}
