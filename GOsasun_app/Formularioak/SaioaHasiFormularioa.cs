// ============================================================
// SaioaHasiFormularioa.cs - Saioa hasteko formularioa
// ============================================================
// Erabiltzaileak saioa hasteko formularioa. Erabiltzaile-izena
// eta pasahitza eskatzen ditu, MySQL-n egiaztatzen du eta
// rola arabera MenuNagusia irekitzen du.
// ============================================================

using GOsasun_app.Modeloak;
using GOsasun_app.Kontrolatzaileak;

namespace GOsasun_app.Formularioak
{
    /// <summary>
    /// Saioa hasteko formularioa (Saioa Hasi Formularioa).
    /// Tablet-entzako diseinatua, ukipen-elementu handiekin.
    /// </summary>
    public partial class SaioaHasiFormularioa : Form
    {
        private readonly ErabiltzaileKontrolatzailea _kontrolatzailea;

        // Eraikitzailea
        public SaioaHasiFormularioa()
        {
            InitializeComponent();
            _kontrolatzailea = new ErabiltzaileKontrolatzailea();
            KonfiguratuFormularioa();
            KargatuBaliabideak();
            KonfiguratuGertakariak();
        }

        // Formularioaren konfigurazioa
        private void KonfiguratuFormularioa()
        {
            this.Text = "GOsasun - Saioa Hasi";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
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
                    Path.Combine(root, "img", "png", "logoak", logoIzena),
                    Path.Combine(root, "GOsasun_app", "img", "png", "logoak", logoIzena),
                    Path.Combine(root, "Assets", logoIzena),
                    Path.Combine(root, "GOsasun_app", "Assets", logoIzena),
                    Path.Combine(root, "..", "..", "..", "img", "png", "logoak", logoIzena),
                    Path.Combine(root, "..", "..", "..", "GOsasun_app", "img", "png", "logoak", logoIzena),
                    Path.Combine(root, "..", "..", "..", "Assets", logoIzena)
                };

                foreach (string s in saioak)
                {
                    if (File.Exists(s)) { logoBidea = s; break; }
                }
            }

            if (File.Exists(logoBidea))
            {
                try
                {
                    _logoPicture.Image = Image.FromFile(logoBidea);
                    _logoPicture.BackColor = Color.Transparent;
                }
                catch
                {
                    _logoPicture.BackColor = Color.Red;
                }
            }
            else
            {
                _logoPicture.BackColor = Color.Blue;
            }
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
                // Kontrolatzaileari deitu (OOP bidez)
                var erabiltzaileaObj = _kontrolatzailea.Login(emaila, pasahitza);

                if (erabiltzaileaObj != null)
                {
                    ErakutsiMezua("Saioa ongi hasi da! Itxaron...", Color.FromArgb(46, 204, 113));

                    Form menuForm;
                    if (erabiltzaileaObj is Medikua)
                    {
                        menuForm = new MedikuMenua(erabiltzaileaObj);
                    }
                    else if (erabiltzaileaObj is Pazientea)
                    {
                        menuForm = new PazienteMenua(erabiltzaileaObj);
                    }
                    else
                    {
                        menuForm = new HarreraMenua(erabiltzaileaObj);
                    }

                    menuForm.FormClosed += (s, args) =>
                    {
                        _erabiltzaileTextBox.Text = "";
                        _pasahitzaTextBox.Text = "";
                        _mezuLabel.Text = "";
                        this.Show();
                    };
                    this.Hide();
                    menuForm.Show();
                }
                else
                {
                    ErakutsiMezua("Erabiltzaile edo pasahitz okerra.", Color.FromArgb(231, 76, 60));
                }
            }
            catch (Exception ex)
            {
                ErakutsiMezua($"Errorea saioa hastean: {ex.Message}", Color.FromArgb(231, 76, 60));
            }
        }

        private void KonfiguratuGertakariak()
        {
            _loginPanela.Paint += LoginPanela_Paint;
            _erakutsiPasahitza.CheckedChanged += (s, e) => { _pasahitzaTextBox.UseSystemPasswordChar = !_erakutsiPasahitza.Checked; };
            _loginBotoia.Click += LoginBotoia_Click;
        }

        private void ErakutsiMezua(string mezua, Color kolorea)
        {
            _mezuLabel.Text = mezua;
            _mezuLabel.ForeColor = kolorea;
        }

        private void _loginPanela_Paint(object sender, PaintEventArgs e)
        {

        }

        private void _loginBotoia_Click(object sender, EventArgs e)
        {

        }
    }
}
