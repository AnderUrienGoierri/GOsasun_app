// ============================================================
// SaioaHasi.cs - Saioa hasteko formularioa
// ============================================================
// Erabiltzaileak saioa hasteko formularioa. Erabiltzaile-izena
// eta pasahitza eskatzen ditu, MySQL-n egiaztatzen du eta
// rola arabera MenuNagusia irekitzen du.
// ============================================================

using GOsasun_app.Modeloa;
using GOsasun_app.Kontrola;
using GOsasun_app.Kontrola.Zerbitzuak;
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
        private readonly System.Windows.Forms.Timer _blokeoEguneratzeTimerra;

        // Eraikitzailea
        public SaioaHasi()
        {
            InitializeComponent();
            _kontrolatzailea = new ErabiltzaileKontrolatzailea();
            _blokeoEguneratzeTimerra = new System.Windows.Forms.Timer { Interval = 1000 };
            KonfiguratuFormularioa();
            KargatuBaliabideak();
            KonfiguratuGertakariak();
            EguneratuLoginSegurtasuna();
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ZentratuPantailaLanEremuan();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            BeginInvoke(new Action(ZentratuPantailaLanEremuan));
        }

        private void ZentratuPantailaLanEremuan()
        {
            Rectangle lanEremua = Screen.FromControl(this).WorkingArea;
            int x = lanEremua.Left + Math.Max(0, (lanEremua.Width - Width) / 2);
            int y = lanEremua.Top + Math.Max(0, (lanEremua.Height - Height) / 2);
            Location = new Point(x, y);
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

            LoginSegurtasunEgoera segurtasunEgoera = _kontrolatzailea.LortuLoginBlokeoEgoera();

            if (segurtasunEgoera.Blokeatuta)
            {
                EguneratuLoginSegurtasuna(segurtasunEgoera);
                return;
            }

            if (string.IsNullOrEmpty(emaila) || string.IsNullOrEmpty(pasahitza))
            {
                ErakutsiMezua("Mesedez, sartu emaila eta pasahitza.", Color.FromArgb(231, 76, 60));
                return;
            }

            try
            {
                // Kontrolatzaileari deitu (OOP bidez)
                LoginEmaitza loginEmaitza = _kontrolatzailea.Login(emaila, pasahitza);
                Erabiltzailea? erabiltzaileaObj = loginEmaitza.Erabiltzailea;

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

                    _blokeoEguneratzeTimerra.Stop();

                    menuForm.FormClosed += (s, args) =>
                    {
                        _erabiltzaileTextBox.Text = "";
                        _pasahitzaTextBox.Text = "";
                        EguneratuLoginSegurtasuna();
                        this.Show();
                    };
                    this.Hide();
                    menuForm.Show();
                }
                else
                {
                    if (loginEmaitza.Blokeatuta)
                    {
                        EguneratuLoginSegurtasuna(loginEmaitza.Egoera);
                    }
                    else
                    {
                        ErakutsiMezua(SortuHutsegiteMezua(loginEmaitza.Egoera), Color.FromArgb(231, 76, 60));
                    }
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
            _blokeoEguneratzeTimerra.Tick += BlokeoEguneratzeTimerra_Tick;
        }

        private void ErakutsiMezua(string mezua, Color kolorea)
        {
            _mezuLabel.Text = mezua;
            _mezuLabel.ForeColor = kolorea;
        }

        private void BlokeoEguneratzeTimerra_Tick(object? sender, EventArgs e)
        {
            EguneratuLoginSegurtasuna();
        }

        private void EguneratuLoginSegurtasuna(LoginSegurtasunEgoera? egoera = null)
        {
            egoera ??= _kontrolatzailea.LortuLoginBlokeoEgoera();
            bool gaituta = !egoera.Blokeatuta;

            EzarriSaioHasierakoKontrolak(gaituta);

            if (egoera.Blokeatuta)
            {
                _blokeoEguneratzeTimerra.Start();
                ErakutsiMezua(SortuBlokeoMezua(egoera), Color.FromArgb(192, 57, 43));
                return;
            }

            _blokeoEguneratzeTimerra.Stop();

            if (egoera.SaiakeraBakarreraMugatuta)
            {
                ErakutsiMezua(
                    "Blokeoa amaitu da. Saiakera bakarra duzu; huts eginez gero beste 8 orduz blokeatuko da.",
                    Color.FromArgb(243, 156, 18));
                return;
            }

            _mezuLabel.Text = string.Empty;
        }

        private void EzarriSaioHasierakoKontrolak(bool gaituta)
        {
            _erabiltzaileTextBox.Enabled = gaituta;
            _pasahitzaTextBox.Enabled = gaituta;
            _erakutsiPasahitza.Enabled = gaituta;
            _loginBotoia.Enabled = gaituta;
            _loginBotoia.BackColor = gaituta
                ? Color.FromArgb(46, 204, 113)
                : Color.FromArgb(149, 165, 166);
        }

        private static string SortuBlokeoMezua(LoginSegurtasunEgoera egoera)
        {
            string gelditzenDenbora = FormateatuBlokeoDenbora(egoera.GelditzenDenDenbora);

            if (egoera.BlokeoAmaieraLokala.HasValue)
            {
                return $"Programa blokeatuta dago. Saiatu berriro {gelditzenDenbora} barru. Blokeo amaiera: {egoera.BlokeoAmaieraLokala.Value:dd/MM/yyyy HH:mm}.";
            }

            return $"Programa blokeatuta dago. Saiatu berriro {gelditzenDenbora} barru.";
        }

        private static string SortuHutsegiteMezua(LoginSegurtasunEgoera egoera)
        {
            if (egoera.GelditzenDirenSaiakerak == 1)
            {
                return "Erabiltzaile edo pasahitz okerra. Saiakera bakarra geratzen da blokeoa aktibatu aurretik.";
            }

            return $"Erabiltzaile edo pasahitz okerra. {egoera.GelditzenDirenSaiakerak} saiakera geratzen dira blokeoa aktibatu aurretik.";
        }

        private static string FormateatuBlokeoDenbora(TimeSpan denbora)
        {
            if (denbora < TimeSpan.Zero)
            {
                denbora = TimeSpan.Zero;
            }

            int orduak = (int)Math.Floor(denbora.TotalHours);
            return $"{orduak:00}:{denbora.Minutes:00}:{denbora.Seconds:00}";
        }

        private void _loginPanela_Paint(object sender, PaintEventArgs e)
        {

        }

        private void _loginBotoia_Click(object sender, EventArgs e)
        {

        }
    }
}
