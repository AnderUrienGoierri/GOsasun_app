// ============================================================
// SaioaHasi.cs - Saioa hasteko formularioa
// ============================================================
// Erabiltzaileak saioa hasteko formularioa. Erabiltzaile-izena
// eta pasahitza eskatzen ditu, MySQL-n egiaztatzen du eta
// rola arabera MenuNagusia irekitzen du.
// ============================================================

using GOsasun_app.Modeloa;
using GOsasun_app.Kontrola;
using System.ComponentModel;

namespace GOsasun_app.Interfazea
{
    /// <summary>
    /// Saioa hasteko formularioa (Saioa Hasi Formularioa).
    /// Tablet-entzako diseinatua, ukipen-elementu handiekin.
    /// </summary>
    public partial class SaioaHasi : Form
    {
        private static readonly Size PortadaTamaina = new Size(1514, 1394);
        private readonly ErabiltzaileKontrolatzailea _kontrolatzailea;

        // Eraikitzailea
        public SaioaHasi()
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
            this.ClientSize = PortadaTamaina;
        }

        // Irudiak kargatu
        private void KargatuBaliabideak()
        {
            string? atzekoPlanoaBidea = BilatuPortadaBidea();

            if (!string.IsNullOrEmpty(atzekoPlanoaBidea) && File.Exists(atzekoPlanoaBidea))
            {
                this.BackgroundImage = Image.FromFile(atzekoPlanoaBidea);
                this.BackgroundImageLayout = ImageLayout.None;
                this.ClientSize = this.BackgroundImage.Size;
            }
            else
            {
                this.BackColor = Color.FromArgb(235, 242, 247);
                this.ClientSize = PortadaTamaina;
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

        private static string? BilatuPortadaBidea()
        {
            foreach (string root in LortuBilaketaErroak())
            {
                string[] saioak = {
                    Path.Combine(root, "img", "png", "app_portada.png"),
                    Path.Combine(root, "GOsasun_app", "img", "png", "app_portada.png")
                };

                string? aurkitua = saioak.FirstOrDefault(File.Exists);
                if (!string.IsNullOrEmpty(aurkitua)) return aurkitua;
            }

            return null;
        }

        private static IEnumerable<string> LortuBilaketaErroak()
        {
            HashSet<string> erroak = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string?[] hasierakoak = {
                Application.StartupPath,
                AppContext.BaseDirectory,
                Directory.GetCurrentDirectory(),
                Environment.CurrentDirectory,
                Path.GetDirectoryName(typeof(SaioaHasi).Assembly.Location)
            };

            foreach (string? hasiera in hasierakoak)
            {
                if (string.IsNullOrWhiteSpace(hasiera) || !Directory.Exists(hasiera)) continue;

                DirectoryInfo? karpeta = new DirectoryInfo(hasiera);
                while (karpeta != null)
                {
                    erroak.Add(karpeta.FullName);
                    karpeta = karpeta.Parent;
                }
            }

            return erroak;
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
                    Form menuForm;
                    if (erabiltzaileaObj is OsasunLangilea)
                    {
                        menuForm = new MenuaOsasunLangilea(erabiltzaileaObj);
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
            _itzaliBotoia.Click += (s, e) => Application.Exit();
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
