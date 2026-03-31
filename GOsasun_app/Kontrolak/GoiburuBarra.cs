// ============================================================
// GoiburuBarra.cs - Goiburuko barra (Header Bar)
// ============================================================
// Aplikazioaren goialdean agertzen den barra iluna.
// Erabiltzailearen izena, data, ordua eta saioa ixteko
// botoia erakusten ditu.
// ============================================================

namespace GOsasun_app.Kontrolak
{
    /// <summary>
    /// Goiburuko barra: erabiltzaile izena, data, ordua eta logout botoia.
    /// Pantailaren goialdean akoplatuta dago (Dock.Top).
    /// </summary>
    public class GoiburuBarra : Panel
    {
        // -----------------------------------------------------------
        // Osagaiak
        // -----------------------------------------------------------
        private Label _erabiltzaileLabel;
        private Label _dataLabel;
        private Label _orduaLabel;
        private Button _logoutBotoia;
        private PictureBox _logoPictureBox;
        private System.Windows.Forms.Timer _orduaTimer;

        /// <summary>
        /// Saioa itxitakoan jaurtitzen den gertaera.
        /// </summary>
        public event EventHandler? SaioaItxi;

        // -----------------------------------------------------------
        // Eraikitzaileak
        // -----------------------------------------------------------
        public GoiburuBarra() : this("Erabiltzailea", "Rola") { }

        public GoiburuBarra(string erabiltzaileIzena, string rola)
        {
            // Panel konfigurazioa
            this.Dock = DockStyle.Top;
            this.Height = 85;
            this.BackColor = Color.FromArgb(44, 62, 80);  // #2C3E50
            this.Padding = new Padding(20, 10, 20, 10);
            this.DoubleBuffered = true;

            // Logo
            _logoPictureBox = new PictureBox
            {
                Size = new Size(55, 55),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(20, 15),
                BackColor = Color.Transparent
            };

            // Logo irudia kargatu
            string logoIzena = "GOsasun_logoa-removebg-preview-white.png";
            string logoPath = Path.Combine(Application.StartupPath, "img", logoIzena);
            
            if (!File.Exists(logoPath))
            {
                string root = Directory.GetCurrentDirectory();
                string[] saioak = {
                    Path.Combine(root, "img", logoIzena),
                    Path.Combine(root, "GOsasun_app", "img", logoIzena),
                    Path.Combine(root, "..", "..", "..", "img", logoIzena),
                    Path.Combine(root, "..", "..", "..", "GOsasun_app", "img", logoIzena)
                };
                foreach (string s in saioak) { if (File.Exists(s)) { logoPath = s; break; } }
            }

            if (File.Exists(logoPath))
            {
                _logoPictureBox.Image = Image.FromFile(logoPath);
            }

            // Erabiltzaile izena
            _erabiltzaileLabel = new Label
            {
                Text = $"  {erabiltzaileIzena}  ({rola})",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(70, 18),
                BackColor = Color.Transparent
            };

            // Data
            _dataLabel = new Label
            {
                Text = DateTime.Now.ToString("yyyy/MM/dd, dddd", new System.Globalization.CultureInfo("eu-ES")),
                Font = new Font("Segoe UI", 12f, FontStyle.Regular),
                ForeColor = Color.FromArgb(189, 195, 199), // #BDC3C7
                AutoSize = true,
                BackColor = Color.Transparent
            };

            // Ordua
            _orduaLabel = new Label
            {
                Text = DateTime.Now.ToString("HH:mm:ss"),
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                ForeColor = Color.FromArgb(46, 204, 113), // #2ECC71 berdea
                AutoSize = true,
                BackColor = Color.Transparent
            };

            // Saioa itxi botoia
            _logoutBotoia = new Button
            {
                Text = "⏻  Irten",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(231, 76, 60), // #E74C3C gorria
                FlatStyle = FlatStyle.Flat,
                Size = new Size(150, 55),
                Cursor = Cursors.Hand
            };
            _logoutBotoia.FlatAppearance.BorderSize = 0;
            _logoutBotoia.Click += (s, e) => SaioaItxi?.Invoke(this, EventArgs.Empty);

            // Osagaiak gehitu
            this.Controls.Add(_logoPictureBox);
            this.Controls.Add(_erabiltzaileLabel);
            this.Controls.Add(_dataLabel);
            this.Controls.Add(_orduaLabel);
            this.Controls.Add(_logoutBotoia);

            // Ordua eguneratzeko temporitzadorea
            _orduaTimer = new System.Windows.Forms.Timer();
            _orduaTimer.Interval = 1000;
            _orduaTimer.Tick += (s, e) =>
            {
                _orduaLabel.Text = DateTime.Now.ToString("HH:mm:ss");
                _dataLabel.Text = DateTime.Now.ToString("yyyy/MM/dd, dddd", new System.Globalization.CultureInfo("eu-ES"));
            };
            _orduaTimer.Start();
            
            // Hasierako kokapena behartu
            GoiburuBarra_Resize(null, EventArgs.Empty);

            // Resize kudeatzailea
            this.Resize += GoiburuBarra_Resize;
        }

        public void EguneratuInformazioa(string erabiltzaileIzena, string rola)
        {
            _erabiltzaileLabel.Text = $"  {erabiltzaileIzena}  ({rola})";
            GoiburuBarra_Resize(null, EventArgs.Empty);
        }

        // -----------------------------------------------------------
        // Tamaina aldatzean osagaiak berrezarri
        // -----------------------------------------------------------
        private void GoiburuBarra_Resize(object? sender, EventArgs e)
        {
            int eskuinTartea = 15;

            // Logout botoia eskuinean
            _logoutBotoia.Location = new Point(
                this.Width - _logoutBotoia.Width - eskuinTartea,
                (this.Height - _logoutBotoia.Height) / 2);

            // Ordua logoutaren ezkerrean
            _orduaLabel.Location = new Point(
                _logoutBotoia.Left - _orduaLabel.Width - 25,
                (this.Height - _orduaLabel.Height) / 2);

            // Data orduaren ezkerrean
            _dataLabel.Location = new Point(
                _orduaLabel.Left - _dataLabel.Width - 25,
                (this.Height - _dataLabel.Height) / 2);
        }

        // -----------------------------------------------------------
        // Baliabideak askatu
        // -----------------------------------------------------------
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _orduaTimer?.Stop();
                _orduaTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
