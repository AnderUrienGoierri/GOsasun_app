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
using GOsasun_app.Interfazea.Oinarriak_UI;
using System.ComponentModel;

namespace GOsasun_app.Interfazea
{
    /// <summary>
    /// Saioa hasteko formularioa (Saioa Hasi Formularioa).
    /// Tablet-entzako diseinatua, ukipen-elementu handiekin.
    /// </summary>
    public partial class SaioaHasi : GOsasunForm
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
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            this.ClientSize = PortadaTamaina;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EguneratuLoginDiseinua();
            ZentratuPantailaLanEremuan();
            GordeLeihoTamainaPartekatua();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            BeginInvoke(new Action(EguneratuLoginDiseinua));
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (!DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                EguneratuLoginDiseinua();
            }
        }

        // Irudiak kargatu
        private void KargatuBaliabideak()
        {
            string? atzekoPlanoaBidea = BilatuPortadaBidea();

            if (!string.IsNullOrEmpty(atzekoPlanoaBidea) && File.Exists(atzekoPlanoaBidea))
            {
                this.BackgroundImage = IrudiCachea.LortuBitmapa(atzekoPlanoaBidea);
                this.BackgroundImageLayout = ImageLayout.Zoom;
                if (this.BackgroundImage != null)
                {
                    this.ClientSize = this.BackgroundImage.Size;
                }
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
                    _logoPicture.Image = IrudiCachea.LortuBitmapa(logoBidea);
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

        private void EguneratuLoginDiseinua()
        {
            if (_loginPanela == null
                || _logoPicture == null
                || _tituluLabel == null
                || _erabiltzaileLabel == null
                || _erabiltzaileTextBox == null
                || _pasahitzaLabel == null
                || _pasahitzaTextBox == null
                || _erakutsiPasahitza == null
                || _mezuLabel == null
                || _loginBotoia == null
                || _itzaliBotoia == null
                || ClientSize.Width <= 0
                || ClientSize.Height <= 0)
            {
                return;
            }

            int panelZabalera = Math.Max(430, Math.Min(760, (int)Math.Round(ClientSize.Width * 0.38)));
            int panelAltuera = Math.Max(600, Math.Min(900, (int)Math.Round(ClientSize.Height * 0.78)));
            int panelX = (ClientSize.Width - panelZabalera) / 2;
            int panelY = Math.Max(28, (ClientSize.Height - panelAltuera) / 2);
            _loginPanela.Bounds = new Rectangle(panelX, panelY, panelZabalera, panelAltuera);

            int alboMarjina = Math.Max(28, (int)Math.Round(panelZabalera * 0.16));
            int eremuZabalera = Math.Max(260, panelZabalera - (alboMarjina * 2));
            int y = Math.Max(12, (int)Math.Round(panelAltuera * 0.02));

            int logoZabalera = Math.Max(170, Math.Min(250, (int)Math.Round(panelZabalera * 0.46)));
            int logoAltuera = Math.Max(110, (int)Math.Round(logoZabalera * 0.64));
            _logoPicture.Bounds = new Rectangle((panelZabalera - logoZabalera) / 2, y, logoZabalera, logoAltuera);
            y = _logoPicture.Bottom + Math.Max(6, (int)Math.Round(panelAltuera * 0.01));

            Size tituluNeurria = TextRenderer.MeasureText(_tituluLabel.Text, _tituluLabel.Font);
            _tituluLabel.Bounds = new Rectangle(
                (panelZabalera - eremuZabalera) / 2,
                y,
                eremuZabalera,
                Math.Max(tituluNeurria.Height + 6, 42));
            y = _tituluLabel.Bottom + Math.Max(18, (int)Math.Round(panelAltuera * 0.05));

            _erabiltzaileLabel.Location = new Point(alboMarjina, y);
            y = _erabiltzaileLabel.Bottom + 8;
            _erabiltzaileTextBox.Bounds = new Rectangle(alboMarjina, y, eremuZabalera, _erabiltzaileTextBox.Height);
            y = _erabiltzaileTextBox.Bottom + 16;

            _pasahitzaLabel.Location = new Point(alboMarjina, y);
            y = _pasahitzaLabel.Bottom + 8;
            _pasahitzaTextBox.Bounds = new Rectangle(alboMarjina, y, eremuZabalera, _pasahitzaTextBox.Height);
            y = _pasahitzaTextBox.Bottom + 12;

            _erakutsiPasahitza.Location = new Point(alboMarjina, y);
            y = _erakutsiPasahitza.Bottom + 12;

            int mezuAltuera = Math.Max(56, (int)Math.Round(panelAltuera * 0.12));
            _mezuLabel.Bounds = new Rectangle(alboMarjina, y, eremuZabalera, mezuAltuera);

            int botoiZabalera = Math.Max(220, Math.Min(eremuZabalera, (int)Math.Round(panelZabalera * 0.58)));
            int botoiX = (panelZabalera - botoiZabalera) / 2;
            int botoienTartea = 12;
            int behekoMarjina = Math.Max(26, (int)Math.Round(panelAltuera * 0.05));
            int botoiakBehe = panelAltuera - behekoMarjina;

            _itzaliBotoia.Bounds = new Rectangle(botoiX, botoiakBehe - _itzaliBotoia.Height, botoiZabalera, _itzaliBotoia.Height);
            _loginBotoia.Bounds = new Rectangle(botoiX, _itzaliBotoia.Top - botoienTartea - _loginBotoia.Height, botoiZabalera, _loginBotoia.Height);

            if (_mezuLabel.Bottom > _loginBotoia.Top - 12)
            {
                int gehienezkoMezuAltuera = Math.Max(36, _loginBotoia.Top - 12 - _mezuLabel.Top);
                _mezuLabel.Height = gehienezkoMezuAltuera;
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
                    _blokeoEguneratzeTimerra.Stop();
                    IrekiMenuNagusia(() => SortuMenuNagusia(erabiltzaileaObj));
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

        private Form SortuMenuNagusia(Erabiltzailea erabiltzaileaObj)
        {
            if (erabiltzaileaObj is OsasunLangilea)
            {
                return new MenuaOsasunLangilea(erabiltzaileaObj);
            }

            if (erabiltzaileaObj is Pazientea)
            {
                return new PazienteMenua(erabiltzaileaObj);
            }

            return new HarreraMenua(erabiltzaileaObj);
        }

        private void IrekiMenuNagusia(Func<Form> formularioSortzailea)
        {
            UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;

            BeginInvoke(new Action(() =>
            {
                try
                {
                    Form menuForm = formularioSortzailea();
                    menuForm.Owner = this;
                    menuForm.FormClosed += (s, args) =>
                    {
                        _erabiltzaileTextBox.Text = "";
                        _pasahitzaTextBox.Text = "";
                        EguneratuLoginSegurtasuna();
                        UseWaitCursor = false;
                        Cursor = Cursors.Default;
                        AplikatuLeihoTamainaPartekatuaBerehala();
                        Show();
                    };

                    if (menuForm is GOsasunForm hurrengoPantaila)
                    {
                        GordeLeihoTamainaPartekatua();
                        hurrengoPantaila.AplikatuLeihoTamainaPartekatuaBerehala();

                        EventHandler? prestHandler = null;
                        prestHandler = (s, e) =>
                        {
                            hurrengoPantaila.HasierakoAurkezpenaOsatuta -= prestHandler;
                            if (!IsDisposed)
                            {
                                UseWaitCursor = false;
                                Cursor = Cursors.Default;
                                Hide();
                            }
                        };

                        hurrengoPantaila.HasierakoAurkezpenaOsatuta += prestHandler;
                    }
                    else
                    {
                        UseWaitCursor = false;
                        Cursor = Cursors.Default;
                        Hide();
                    }

                    menuForm.Show();
                }
                catch (Exception ex)
                {
                    UseWaitCursor = false;
                    Cursor = Cursors.Default;
                    ErakutsiMezua($"Errorea menua irekitzean: {ex.Message}", Color.FromArgb(231, 76, 60));
                }
            }));
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
