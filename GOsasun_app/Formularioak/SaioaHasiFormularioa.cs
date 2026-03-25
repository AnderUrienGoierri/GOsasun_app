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
    public partial class SaioaHasiFormularioa : Form
    {
        // Eraikitzailea
        public SaioaHasiFormularioa()
        {
            InitializeComponent();
            KonfiguratuFormularioa();
            KargatuBaliabideak();
            KonfiguratuGertakariak();
            KokatuOsagaiak();
        }

        // Formularioaren konfigurazioa
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

        // Irudiak kargatu
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
