namespace GOsasun_app.Formularioak
{
    partial class SaioaHasiFormularioa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaioaHasiFormularioa));
            _loginPanela = new Panel();
            _logoPicture = new PictureBox();
            _tituluLabel = new Label();
            _erabiltzaileLabel = new Label();
            _erabiltzaileTextBox = new TextBox();
            _pasahitzaLabel = new Label();
            _pasahitzaTextBox = new TextBox();
            _erakutsiPasahitza = new CheckBox();
            _loginBotoia = new Button();
            _mezuLabel = new Label();
            _loginPanela.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_logoPicture).BeginInit();
            SuspendLayout();
            // 
            // _loginPanela
            // 
            _loginPanela.BackColor = Color.FromArgb(240, 255, 255, 255);
            _loginPanela.Controls.Add(_logoPicture);
            _loginPanela.Controls.Add(_tituluLabel);
            _loginPanela.Controls.Add(_erabiltzaileLabel);
            _loginPanela.Controls.Add(_erabiltzaileTextBox);
            _loginPanela.Controls.Add(_pasahitzaLabel);
            _loginPanela.Controls.Add(_pasahitzaTextBox);
            _loginPanela.Controls.Add(_erakutsiPasahitza);
            _loginPanela.Controls.Add(_loginBotoia);
            _loginPanela.Controls.Add(_mezuLabel);
            _loginPanela.Location = new Point(455, 122);
            _loginPanela.Name = "_loginPanela";
            _loginPanela.Padding = new Padding(30);
            _loginPanela.Size = new Size(672, 774);
            _loginPanela.TabIndex = 0;
            _loginPanela.Paint += _loginPanela_Paint;
            // 
            // _logoPicture
            // 
            _logoPicture.BackColor = Color.Transparent;
            _logoPicture.Image = (Image)resources.GetObject("_logoPicture.Image");
            _logoPicture.Location = new Point(145, 0);
            _logoPicture.Name = "_logoPicture";
            _logoPicture.Size = new Size(360, 229);
            _logoPicture.SizeMode = PictureBoxSizeMode.Zoom;
            _logoPicture.TabIndex = 0;
            _logoPicture.TabStop = false;
            // 
            // _tituluLabel
            // 
            _tituluLabel.AutoSize = true;
            _tituluLabel.BackColor = Color.Transparent;
            _tituluLabel.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            _tituluLabel.ForeColor = Color.FromArgb(44, 62, 80);
            _tituluLabel.Location = new Point(147, 219);
            _tituluLabel.Name = "_tituluLabel";
            _tituluLabel.Size = new Size(348, 100);
            _tituluLabel.TabIndex = 1;
            _tituluLabel.Text = "GOsasun";
            // 
            // _erabiltzaileLabel
            // 
            _erabiltzaileLabel.AutoSize = true;
            _erabiltzaileLabel.BackColor = Color.Transparent;
            _erabiltzaileLabel.Font = new Font("Segoe UI", 13F);
            _erabiltzaileLabel.ForeColor = Color.FromArgb(100, 100, 100);
            _erabiltzaileLabel.Location = new Point(145, 315);
            _erabiltzaileLabel.Name = "_erabiltzaileLabel";
            _erabiltzaileLabel.Size = new Size(120, 47);
            _erabiltzaileLabel.TabIndex = 2;
            _erabiltzaileLabel.Text = "Emaila";
            // 
            // _erabiltzaileTextBox
            // 
            _erabiltzaileTextBox.BorderStyle = BorderStyle.FixedSingle;
            _erabiltzaileTextBox.Font = new Font("Segoe UI", 12F);
            _erabiltzaileTextBox.Location = new Point(147, 365);
            _erabiltzaileTextBox.Name = "_erabiltzaileTextBox";
            _erabiltzaileTextBox.PlaceholderText = "Zure emaila...";
            _erabiltzaileTextBox.Size = new Size(360, 50);
            _erabiltzaileTextBox.TabIndex = 3;
            // 
            // _pasahitzaLabel
            // 
            _pasahitzaLabel.AutoSize = true;
            _pasahitzaLabel.BackColor = Color.Transparent;
            _pasahitzaLabel.Font = new Font("Segoe UI", 13F);
            _pasahitzaLabel.ForeColor = Color.FromArgb(100, 100, 100);
            _pasahitzaLabel.Location = new Point(147, 435);
            _pasahitzaLabel.Name = "_pasahitzaLabel";
            _pasahitzaLabel.Size = new Size(164, 47);
            _pasahitzaLabel.TabIndex = 4;
            _pasahitzaLabel.Text = "Pasahitza";
            // 
            // _pasahitzaTextBox
            // 
            _pasahitzaTextBox.BorderStyle = BorderStyle.FixedSingle;
            _pasahitzaTextBox.Font = new Font("Segoe UI", 12F);
            _pasahitzaTextBox.Location = new Point(147, 485);
            _pasahitzaTextBox.Name = "_pasahitzaTextBox";
            _pasahitzaTextBox.PlaceholderText = "Pasahitza...";
            _pasahitzaTextBox.Size = new Size(360, 50);
            _pasahitzaTextBox.TabIndex = 5;
            _pasahitzaTextBox.UseSystemPasswordChar = true;
            // 
            // _erakutsiPasahitza
            // 
            _erakutsiPasahitza.AutoSize = true;
            _erakutsiPasahitza.BackColor = Color.Transparent;
            _erakutsiPasahitza.Font = new Font("Segoe UI", 11F);
            _erakutsiPasahitza.ForeColor = Color.FromArgb(100, 100, 100);
            _erakutsiPasahitza.Location = new Point(151, 541);
            _erakutsiPasahitza.Name = "_erakutsiPasahitza";
            _erakutsiPasahitza.Size = new Size(284, 45);
            _erakutsiPasahitza.TabIndex = 6;
            _erakutsiPasahitza.Text = "Erakutsi pasahitza";
            _erakutsiPasahitza.UseVisualStyleBackColor = false;
            // 
            // _loginBotoia
            // 
            _loginBotoia.BackColor = Color.FromArgb(46, 204, 113);
            _loginBotoia.Cursor = Cursors.Hand;
            _loginBotoia.FlatAppearance.BorderSize = 0;
            _loginBotoia.FlatStyle = FlatStyle.Flat;
            _loginBotoia.Location = new Point(145, 629);
            _loginBotoia.Name = "_loginBotoia";
            _loginBotoia.Size = new Size(360, 60);
            _loginBotoia.TabIndex = 7;
            _loginBotoia.Text = "SARTU";
            _loginBotoia.UseVisualStyleBackColor = false;
            _loginBotoia.Click += _loginBotoia_Click;
            // 
            // _mezuLabel
            // 
            _mezuLabel.AutoSize = true;
            _mezuLabel.BackColor = Color.Transparent;
            _mezuLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _mezuLabel.ForeColor = Color.FromArgb(231, 76, 60);
            _mezuLabel.Location = new Point(145, 602);
            _mezuLabel.Name = "_mezuLabel";
            _mezuLabel.Size = new Size(0, 41);
            _mezuLabel.TabIndex = 8;
            _mezuLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SaioaHasiFormularioa
            // 
            ClientSize = new Size(1600, 1000);
            Controls.Add(_loginPanela);
            Name = "SaioaHasiFormularioa";
            _loginPanela.ResumeLayout(false);
            _loginPanela.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_logoPicture).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel _loginPanela;
        private PictureBox _logoPicture;
        private Label _tituluLabel;
        private Label _erabiltzaileLabel;
        private TextBox _erabiltzaileTextBox;
        private Label _pasahitzaLabel;
        private TextBox _pasahitzaTextBox;
        private Button _loginBotoia;
        private Label _mezuLabel;
        private CheckBox _erakutsiPasahitza;
    }
}
