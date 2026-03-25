// ============================================================
// SaioaHasiFormularioa.cs - Saioa hasteko formularioa
// ============================================================
// Erabiltzaileak saioa hasteko formularioa. Erabiltzaile-izena
// eta pasahitza eskatzen ditu, MySQL-n egiaztatzen du eta
// rola arabera MenuNagusia irekitzen du.
// ============================================================

using MySql.Data.MySqlClient;
using GOsasun_app.Modeloak;

namespace GOsasun_app.Formularioak
{
    /// <summary>
    /// Saioa hasteko formularioa (Saioa Hasi Formularioa).
    /// Tablet-entzako diseinatua, ukipen-elementu handiekin.
    /// </summary>
    public class SaioaHasiFormularioa : Form
    {
        // -----------------------------------------------------------
        // Osagaiak
        // -----------------------------------------------------------
        private Panel _loginPanela = null!;
        private PictureBox _logoPicture = null!;
        private Label _tituluLabel = null!;
        private Label _erabiltzaileLabel = null!;
        private TextBox _erabiltzaileTextBox = null!;
        private Label _pasahitzaLabel = null!;
        private TextBox _pasahitzaTextBox = null!;
        private Button _loginBotoia = null!;
        private Label _mezuLabel = null!;
        private CheckBox _erakutsiPasahitza = null!;

        // -----------------------------------------------------------
        // Eraikitzailea
        // -----------------------------------------------------------
        public SaioaHasiFormularioa()
        {
            InitializeComponent();
            KonfiguratuFormularioa();
            KargatuBaliabideak();
            KonfiguratuGertakariak();
            KokatuOsagaiak();
        }

        // -----------------------------------------------------------
        // Formularioaren konfigurazioa
        // -----------------------------------------------------------
        private void KonfiguratuFormularioa()
        {
            this.Text = "GOsasun - Saioa Hasi";
            this.ClientSize = new Size(1600, 1000);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;

            this.Resize += (s, e) => KokatuOsagaiak();
        }

        // -----------------------------------------------------------
        // Irudiak kargatu
        // -----------------------------------------------------------
        private void KargatuBaliabideak()
        {
            if (this.DesignMode) return;

            string bgIzena = "wood_background.png";
            string atzekoPlanoaBidea = Path.Combine(Application.StartupPath, "img", bgIzena);
            
            if (!File.Exists(atzekoPlanoaBidea))
            {
                string root = Directory.GetCurrentDirectory();
                string[] saioak = {
                    Path.Combine(root, "img", bgIzena),
                    Path.Combine(root, "GOsasun_app", "img", bgIzena),
                    Path.Combine(root, "..", "..", "..", "img", bgIzena),
                    Path.Combine(root, "..", "..", "..", "GOsasun_app", "img", bgIzena)
                };
                foreach (string s in saioak)
                {
                    if (File.Exists(s)) { atzekoPlanoaBidea = s; break; }
                }
            }

            if (File.Exists(atzekoPlanoaBidea))
            {
                this.BackgroundImage = Image.FromFile(atzekoPlanoaBidea);
                this.BackgroundImageLayout = ImageLayout.Tile;
            }
            else
            {
                this.BackColor = Color.FromArgb(139, 119, 101);
            }

            string logoIzena = "GOsasun_logo_whatsap.png";
            string logoBidea = Path.Combine(Application.StartupPath, "img", logoIzena);
            
            if (!File.Exists(logoBidea))
            {
                string root = Directory.GetCurrentDirectory();
                string[] saioak = {
                    Path.Combine(root, "img", logoIzena),
                    Path.Combine(root, "GOsasun_app", "img", logoIzena),
                    Path.Combine(root, "..", "..", "..", "img", logoIzena),
                    Path.Combine(root, "..", "..", "..", "GOsasun_app", "img", logoIzena)
                };

                foreach (string s in saioak)
                {
                    if (File.Exists(s)) { logoBidea = s; break; }
                }
            }

            if (File.Exists(logoBidea))
            {
                try {
                    _logoPicture.Image = Image.FromFile(logoBidea);
                    _logoPicture.BackColor = Color.Transparent;
                } catch {
                    _logoPicture.BackColor = Color.Red;
                }
            }
            else
            {
                _logoPicture.BackColor = Color.Blue;
            }
        }

        private void InitializeComponent()
        {
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
            _loginPanela.Location = new Point(283, 152);
            _loginPanela.Name = "_loginPanela";
            _loginPanela.Padding = new Padding(30);
            _loginPanela.Size = new Size(672, 774);
            _loginPanela.TabIndex = 0;

            _logoPicture.BackColor = Color.Transparent;
            _logoPicture.Location = new Point(285, 38);
            _logoPicture.Name = "_logoPicture";
            _logoPicture.Size = new Size(120, 120);
            _logoPicture.SizeMode = PictureBoxSizeMode.Zoom;
            _logoPicture.TabIndex = 0;
            _logoPicture.TabStop = false;

            _tituluLabel.AutoSize = true;
            _tituluLabel.BackColor = Color.Transparent;
            _tituluLabel.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            _tituluLabel.ForeColor = Color.FromArgb(44, 62, 80);
            _tituluLabel.Location = new Point(175, 142);
            _tituluLabel.Name = "_tituluLabel";
            _tituluLabel.Size = new Size(348, 100);
            _tituluLabel.TabIndex = 1;
            _tituluLabel.Text = "GOsasun";

            _erabiltzaileLabel.AutoSize = true;
            _erabiltzaileLabel.BackColor = Color.Transparent;
            _erabiltzaileLabel.Font = new Font("Segoe UI", 13F);
            _erabiltzaileLabel.ForeColor = Color.FromArgb(100, 100, 100);
            _erabiltzaileLabel.Location = new Point(165, 238);
            _erabiltzaileLabel.Name = "_erabiltzaileLabel";
            _erabiltzaileLabel.Size = new Size(120, 47);
            _erabiltzaileLabel.TabIndex = 2;
            _erabiltzaileLabel.Text = "Emaila";

            _erabiltzaileTextBox.BorderStyle = BorderStyle.FixedSingle;
            _erabiltzaileTextBox.Font = new Font("Segoe UI", 12F);
            _erabiltzaileTextBox.Location = new Point(165, 287);
            _erabiltzaileTextBox.Name = "_erabiltzaileTextBox";
            _erabiltzaileTextBox.PlaceholderText = "Zure emaila...";
            _erabiltzaileTextBox.Size = new Size(360, 50);
            _erabiltzaileTextBox.TabIndex = 3;

            _pasahitzaLabel.AutoSize = true;
            _pasahitzaLabel.BackColor = Color.Transparent;
            _pasahitzaLabel.Font = new Font("Segoe UI", 13F);
            _pasahitzaLabel.ForeColor = Color.FromArgb(100, 100, 100);
            _pasahitzaLabel.Location = new Point(165, 338);
            _pasahitzaLabel.Name = "_pasahitzaLabel";
            _pasahitzaLabel.Size = new Size(164, 47);
            _pasahitzaLabel.TabIndex = 4;
            _pasahitzaLabel.Text = "Pasahitza";

            _pasahitzaTextBox.BorderStyle = BorderStyle.FixedSingle;
            _pasahitzaTextBox.Font = new Font("Segoe UI", 12F);
            _pasahitzaTextBox.Location = new Point(165, 389);
            _pasahitzaTextBox.Name = "_pasahitzaTextBox";
            _pasahitzaTextBox.PlaceholderText = "Pasahitza...";
            _pasahitzaTextBox.Size = new Size(360, 50);
            _pasahitzaTextBox.TabIndex = 5;
            _pasahitzaTextBox.UseSystemPasswordChar = true;

            _erakutsiPasahitza.AutoSize = true;
            _erakutsiPasahitza.BackColor = Color.Transparent;
            _erakutsiPasahitza.Font = new Font("Segoe UI", 11F);
            _erakutsiPasahitza.ForeColor = Color.FromArgb(100, 100, 100);
            _erakutsiPasahitza.Location = new Point(165, 446);
            _erakutsiPasahitza.Name = "_erakutsiPasahitza";
            _erakutsiPasahitza.Size = new Size(284, 45);
            _erakutsiPasahitza.TabIndex = 6;
            _erakutsiPasahitza.Text = "Erakutsi pasahitza";
            _erakutsiPasahitza.UseVisualStyleBackColor = false;

            _loginBotoia.BackColor = Color.FromArgb(46, 204, 113);
            _loginBotoia.Cursor = Cursors.Hand;
            _loginBotoia.FlatAppearance.BorderSize = 0;
            _loginBotoia.FlatStyle = FlatStyle.Flat;
            _loginBotoia.Location = new Point(171, 507);
            _loginBotoia.Name = "_loginBotoia";
            _loginBotoia.Size = new Size(360, 60);
            _loginBotoia.TabIndex = 7;
            _loginBotoia.Text = "SARTU";
            _loginBotoia.UseVisualStyleBackColor = false;

            _mezuLabel.AutoSize = true;
            _mezuLabel.BackColor = Color.Transparent;
            _mezuLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _mezuLabel.ForeColor = Color.FromArgb(231, 76, 60);
            _mezuLabel.Location = new Point(165, 540);
            _mezuLabel.Name = "_mezuLabel";
            _mezuLabel.Size = new Size(0, 41);
            _mezuLabel.TabIndex = 8;
            _mezuLabel.TextAlign = ContentAlignment.MiddleCenter;

            ClientSize = new Size(1600, 1000);
            Controls.Add(_loginPanela);
            Name = "SaioaHasiFormularioa";
            _loginPanela.ResumeLayout(false);
            _loginPanela.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_logoPicture).EndInit();
            ResumeLayout(false);
        }

        private void KokatuOsagaiak()
        {
            _loginPanela.Location = new Point(
                (this.ClientSize.Width - _loginPanela.Width) / 2,
                (this.ClientSize.Height - _loginPanela.Height) / 2);

            int ezkerTartea = (_loginPanela.Width - 360) / 2;
            int y = 20;

            _logoPicture.Location = new Point((_loginPanela.Width - _logoPicture.Width) / 2, y);
            y += _logoPicture.Height + 5;

            _tituluLabel.Location = new Point((_loginPanela.Width - _tituluLabel.Width) / 2, y);
            y += _tituluLabel.Height + 25;

            _erabiltzaileLabel.Location = new Point(ezkerTartea, y);
            y += _erabiltzaileLabel.Height + 5;
            _erabiltzaileTextBox.Location = new Point(ezkerTartea, y);
            y += _erabiltzaileTextBox.Height + 15;

            _pasahitzaLabel.Location = new Point(ezkerTartea, y);
            y += _pasahitzaLabel.Height + 5;
            _pasahitzaTextBox.Location = new Point(ezkerTartea, y);
            y += _pasahitzaTextBox.Height + 8;

            _erakutsiPasahitza.Location = new Point(ezkerTartea, y);
            y += _erakutsiPasahitza.Height + 20;

            _loginBotoia.Location = new Point(ezkerTartea, y);
            y += _loginBotoia.Height + 15;

            _mezuLabel.Location = new Point(ezkerTartea, y);
        }

        private void LoginPanela_Paint(object? sender, PaintEventArgs e)
        {
            using (var bidea = new System.Drawing.Drawing2D.GraphicsPath())
            {
                int r = 30;
                var rect = _loginPanela.ClientRectangle;
                bidea.AddArc(rect.X, rect.Y, r * 2, r * 2, 180, 90);
                bidea.AddArc(rect.Right - r * 2, rect.Y, r * 2, r * 2, 270, 90);
                bidea.AddArc(rect.Right - r * 2, rect.Bottom - r * 2, r * 2, r * 2, 0, 90);
                bidea.AddArc(rect.X, rect.Bottom - r * 2, r * 2, r * 2, 90, 90);
                bidea.CloseFigure();
                _loginPanela.Region = new Region(bidea);
            }
        }

        private void LoginBotoia_Click(object? sender, EventArgs e)
        {
            string emaila = _erabiltzaileTextBox.Text.Trim();
            string pasahitza = _pasahitzaTextBox.Text;

            if (string.IsNullOrEmpty(emaila) || string.IsNullOrEmpty(pasahitza))
            {
                ErakutsiMezua("Mesedez, sartu emaila eta pasahitza.", Color.FromArgb(231, 76, 60));
                return;
            }

            try
            {
                var (arrakasta, erabiltzaileaObj) = EgiaztatuErabiltzailea(emaila, pasahitza);

                if (arrakasta && erabiltzaileaObj != null)
                {
                    ErakutsiMezua("Saioa ongi hasi da! Itxaron...", Color.FromArgb(46, 204, 113));
                    var menuNagusia = new MenuNagusia(erabiltzaileaObj);
                    menuNagusia.FormClosed += (s, args) =>
                    {
                        _erabiltzaileTextBox.Text = "";
                        _pasahitzaTextBox.Text = "";
                        _mezuLabel.Text = "";
                        this.Show();
                    };
                    this.Hide();
                    menuNagusia.Show();
                }
                else
                {
                    ErakutsiMezua("Erabiltzaile edo pasahitz okerra.", Color.FromArgb(231, 76, 60));
                }
            }
            catch (MySqlException)
            {
                ErakutsiMezua($"DB errorea. Garapen modua erabiltzen...", Color.FromArgb(243, 156, 18));
                GarapenModuLogin(emaila);
            }
            catch (Exception ex)
            {
                ErakutsiMezua($"Errorea: {ex.Message}", Color.FromArgb(231, 76, 60));
            }
        }

        private void KonfiguratuGertakariak()
        {
            _loginPanela.Paint += LoginPanela_Paint;
            _pasahitzaTextBox.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) LoginBotoia_Click(s, e); };
            _erakutsiPasahitza.CheckedChanged += (s, e) => { _pasahitzaTextBox.UseSystemPasswordChar = !_erakutsiPasahitza.Checked; };
            _loginBotoia.Click += LoginBotoia_Click;
        }

        private (bool arrakasta, Erabiltzailea? erabiltzailea) EgiaztatuErabiltzailea(string emaila, string pasahitza)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    SELECT e.erabiltzaile_id, e.izena, e.abizena, e.emaila, r.rol_izena 
                    FROM Erabiltzaileak e 
                    JOIN Rolak r ON e.rol_id = r.rol_id 
                    WHERE e.emaila = @emaila 
                    AND e.pasahitza = SHA2(@pasahitza, 256)
                    AND e.aktibo = 1";

                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    komandoa.Parameters.AddWithValue("@emaila", emaila);
                    komandoa.Parameters.AddWithValue("@pasahitza", pasahitza);

                    using (var irakurlea = komandoa.ExecuteReader())
                    {
                        if (irakurlea.Read())
                        {
                            int id = irakurlea.GetInt32("erabiltzaile_id");
                            string izena = irakurlea.GetString("izena");
                            string abizena = irakurlea.GetString("abizena");
                            string email = irakurlea.GetString("emaila");
                            string rolIzena = irakurlea.GetString("rol_izena");

                            Erabiltzailea? u = null;
                            if (rolIzena.Equals("Pazientea", StringComparison.OrdinalIgnoreCase))
                                u = new Pazientea { Id = id, Izena = izena, Abizenak = abizena, Emaila = email, RolId = 1 };
                            else if (rolIzena.Equals("Medikua", StringComparison.OrdinalIgnoreCase))
                                u = new Medikua { Id = id, Izena = izena, Abizenak = abizena, Emaila = email, RolId = 2 };
                            else
                                u = new HarrerakoLangilea { Id = id, Izena = izena, Abizenak = abizena, Emaila = email, RolId = 3 };
                            
                            return (true, u);
                        }
                    }
                }
            }
            return (false, null);
        }

        private void GarapenModuLogin(string emaila)
        {
            if (emaila.Contains("paziente") || emaila.Contains("@gmail.com"))
            {
                var u = new Pazientea { Id = 99, Izena = "Joseba", Abizenak = "Zabala", Emaila = emaila, RolId = 1 };
                var menu = new MenuNagusia(u);
                menu.FormClosed += (s, args) => { this.Show(); _mezuLabel.Text = ""; };
                this.Hide();
                menu.Show();
            }
            else if (emaila.Contains("mediku") || emaila.Contains("@gosasun.eus"))
            {
                var u = new Medikua { Id = 98, Izena = "Ane", Abizenak = "Etxeberria", Emaila = emaila, RolId = 2 };
                var menu = new MenuNagusia(u);
                menu.FormClosed += (s, args) => { this.Show(); _mezuLabel.Text = ""; };
                this.Hide();
                menu.Show();
            }
            else
            {
                ErakutsiMezua("Garapen modua: erabili email bat", Color.FromArgb(243, 156, 18));
            }
        }

        private void ErakutsiMezua(string mezua, Color kolorea)
        {
            _mezuLabel.Text = mezua;
            _mezuLabel.ForeColor = kolorea;
        }
    }
}
