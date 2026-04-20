// ============================================================
// OinarriPantaila.cs - Formulario oinarria (Base Form)
// ============================================================
// Formulario guztiek heredatzen duten oinarri klasea.
// Portadako atzeko planoa, GoiburuBarra eta atzera-botoia
// kudeatzen ditu.
// ============================================================

using GOsasun_app.Interfazea.Kontrolak;
using GOsasun_app.Kontrola;
using GOsasun_app.Kontrola.Zerbitzuak;
using GOsasun_app.Modeloa;
using System.Collections.Concurrent;
using System.ComponentModel;
using Svg;

namespace GOsasun_app.Interfazea
{
    /// <summary>
    /// Formulario guztien oinarri klasea.
    /// Egurrezko atzeko planoa eta nabigazio estandarra eskaintzen ditu.
    /// </summary>
    public partial class OinarriPantaila : GOsasunForm
    {
        private static readonly Size PortadaIrudiTamaina = new Size(1514, 1394);
        private static readonly Size OinarriPantailaTamaina = new Size(1902, 1394);
        private static readonly Color PortadaAtzekoKolorea = Color.FromArgb(214, 224, 229);
        private static readonly object PortadaIrudiBlokeoa = new object();
        private static readonly ConcurrentDictionary<string, Bitmap> SvgIkonoCachea = new ConcurrentDictionary<string, Bitmap>(StringComparer.OrdinalIgnoreCase);
        private static Image? _portadaIrudiPartekatua;
        private static string? _portadaIrudiBideaCache;
        private Image? _atzekoPlanoaIrudia;

        // Erabiltzaile informazioa (OOP)
        protected Erabiltzailea? _erabiltzailea;

        // -----------------------------------------------------------
        // Eraikitzailea
        // -----------------------------------------------------------
        public OinarriPantaila()
        {
            InitializeComponent();
            KonfiguratuFormularioa();

            if (!DiseinuModuan())
            {
                KargatuBaliabideak();
                EgokituPortadarenNeurrira();
            }

            if (_goiburuBarra != null)
            {
                _goiburuBarra.SaioaItxi -= GoiburuBarra_SaioaItxi;
                _goiburuBarra.SaioaItxi += GoiburuBarra_SaioaItxi;
                _goiburuBarra.ErabiltzaileaKlik -= GoiburuBarra_ErabiltzaileaKlik;
                _goiburuBarra.ErabiltzaileaKlik += GoiburuBarra_ErabiltzaileaKlik;
            }

            // Atzera botoiaren gertaera lehenetsia
            if (_atzeraBotoia != null)
            {
                _atzeraBotoia.Click += (s, e) => this.Close();
                _atzeraBotoia.BringToFront();
            }
        }

        /// <summary>
        /// Erabiltzaile informazioarekin eraikitzailea.
        /// </summary>
        public OinarriPantaila(Erabiltzailea erabiltzailea)
            : this()
        {
            _erabiltzailea = erabiltzailea;

            // Goiburuko barra informazioarekin eguneratu
            if (_goiburuBarra != null)
            {
                _goiburuBarra.EguneratuInformazioa(_erabiltzailea.IzenOsoa, _erabiltzailea.Rola);
            }
        }

        // -----------------------------------------------------------
        // Formularioaren oinarrizko konfigurazioa
        // -----------------------------------------------------------
        private void KonfiguratuFormularioa()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            this.Text = "GOsasun";
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.ClientSize = OinarriPantailaTamaina;
            this.BackColor = PortadaAtzekoKolorea;

            // Eduki panela konfiguratu
            KonfiguratuEdukiPanela(_edukiPanela);
        }

        private void KonfiguratuEdukiPanela(PortadaPanela panela)
        {
            panela.Dock = DockStyle.Fill;
            panela.AutoScroll = true;
            panela.BackColor = PortadaAtzekoKolorea;
            panela.AtzekoKolorea = PortadaAtzekoKolorea;
            // Padding manualki kudeatuko dugu elementuen X,Y bitartez diseinatzailean
        }

        // -----------------------------------------------------------
        // Irudiak kargatu (Konstruktorian deitua)
        // -----------------------------------------------------------
        protected void KargatuBaliabideak()
        {
            _atzekoPlanoaIrudia = LortuPortadaIrudiPartekatua();

            if (_atzekoPlanoaIrudia != null)
            {
                this.BackgroundImage = null;
                if (_edukiPanela != null)
                {
                    _edukiPanela.AtzekoPlanoaIrudia = _atzekoPlanoaIrudia;
                    _edukiPanela.Invalidate();
                }

                int goiburuAltuera = _goiburuBarra?.Height ?? 0;
                this.ClientSize = new Size(
                    Math.Max(OinarriPantailaTamaina.Width, _atzekoPlanoaIrudia.Width),
                    Math.Max(OinarriPantailaTamaina.Height, _atzekoPlanoaIrudia.Height + goiburuAltuera));
            }
            else
            {
                _atzekoPlanoaIrudia = null;
                this.BackColor = PortadaAtzekoKolorea;
                if (_edukiPanela != null)
                {
                    _edukiPanela.AtzekoPlanoaIrudia = null;
                    _edukiPanela.Invalidate();
                }
                this.ClientSize = OinarriPantailaTamaina;
            }
        }

        private static Image? LortuPortadaIrudiPartekatua()
        {
            lock (PortadaIrudiBlokeoa)
            {
                if (_portadaIrudiPartekatua != null)
                {
                    return _portadaIrudiPartekatua;
                }

                string? atzekoPlanoaBidea = LortuPortadaBideaCacheatuta();
                if (string.IsNullOrWhiteSpace(atzekoPlanoaBidea) || !File.Exists(atzekoPlanoaBidea))
                {
                    return null;
                }

                using Image jatorrizkoIrudia = Image.FromFile(atzekoPlanoaBidea);
                _portadaIrudiPartekatua = new Bitmap(jatorrizkoIrudia);
                return _portadaIrudiPartekatua;
            }
        }

        private static string? LortuPortadaBideaCacheatuta()
        {
            lock (PortadaIrudiBlokeoa)
            {
                if (!string.IsNullOrWhiteSpace(_portadaIrudiBideaCache) && File.Exists(_portadaIrudiBideaCache))
                {
                    return _portadaIrudiBideaCache;
                }

                _portadaIrudiBideaCache = BilatuPortadaBidea();
                return _portadaIrudiBideaCache;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EgokituPortadarenNeurrira();
            ZentratuPantailaLanEremuan();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            EgokituPortadarenNeurrira();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible && !DiseinuModuan())
            {
                BeginInvoke(new Action(FreskatuPantailaIkusgaiDenean));
            }
        }

        protected override bool PantailaOsoanIreki => false;

        private void EgokituPortadarenNeurrira()
        {
            int goiburuAltuera = _goiburuBarra?.Height ?? 0;
            Size irudiTamaina = _atzekoPlanoaIrudia?.Size ?? PortadaIrudiTamaina;
            Size helmugaTamaina = new Size(
                Math.Max(OinarriPantailaTamaina.Width, irudiTamaina.Width),
                Math.Max(OinarriPantailaTamaina.Height, irudiTamaina.Height + goiburuAltuera));

            if (this.ClientSize != helmugaTamaina)
            {
                this.ClientSize = helmugaTamaina;
            }

            if (_goiburuBarra != null)
            {
                _goiburuBarra.Width = helmugaTamaina.Width;
            }

            if (_edukiPanela != null)
            {
                int goiburuBehea = _goiburuBarra?.Bottom ?? 0;
                _edukiPanela.Location = new Point(0, goiburuBehea);
                _edukiPanela.Size = new Size(helmugaTamaina.Width, helmugaTamaina.Height - goiburuAltuera);
                _edukiPanela.AtzekoKolorea = PortadaAtzekoKolorea;
                _edukiPanela.AtzekoPlanoaIrudia = _atzekoPlanoaIrudia;
                _edukiPanela.Invalidate();
            }

            if (_atzeraBotoia != null)
            {
                _atzeraBotoia.BringToFront();
            }
        }

        private void FreskatuPantailaIkusgaiDenean()
        {
            if (IsDisposed || DiseinuModuan())
            {
                return;
            }

            EgokituPortadarenNeurrira();
            AplikatuLeihoTamainaPartekatuaBerehala();
            _edukiPanela?.PerformLayout();
            _edukiPanela?.Invalidate(true);
            PerformLayout();
            Invalidate(true);
            Update();
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

        protected static string? BilatuFitxategiErlatiboa(string erlatiboa)
        {
            if (string.IsNullOrWhiteSpace(erlatiboa))
            {
                return null;
            }

            string normalizatua = erlatiboa.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);

            if (Path.IsPathRooted(normalizatua) && File.Exists(normalizatua))
            {
                return normalizatua;
            }

            try
            {
                string helmugaIrudia = AplikazioBideak.LortuIrudiHelmugaBidea(erlatiboa);
                if (File.Exists(helmugaIrudia))
                {
                    return helmugaIrudia;
                }
            }
            catch
            {
                // Fallback-arekin jarraitu, konfigurazioa ez badago prest.
            }

            foreach (string irudiErroa in AplikazioBideak.LortuIrudiErroak())
            {
                string[] irudiAukerak =
                {
                    Path.Combine(irudiErroa, Path.GetFileName(normalizatua)),
                    Path.Combine(irudiErroa, normalizatua),
                    Path.Combine(irudiErroa, normalizatua.StartsWith($"img{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase)
                        ? normalizatua.Substring($"img{Path.DirectorySeparatorChar}".Length)
                        : normalizatua),
                    Path.Combine(irudiErroa, normalizatua.StartsWith($"img{Path.DirectorySeparatorChar}png{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase)
                        ? normalizatua.Substring($"img{Path.DirectorySeparatorChar}png{Path.DirectorySeparatorChar}".Length)
                        : normalizatua)
                };

                string? irudiAurkitua = irudiAukerak.FirstOrDefault(File.Exists);
                if (!string.IsNullOrWhiteSpace(irudiAurkitua))
                {
                    return irudiAurkitua;
                }
            }

            foreach (string root in LortuBilaketaErroak())
            {
                string[] aukerak =
                {
                    Path.Combine(root, normalizatua),
                    Path.Combine(root, "GOsasun_app", normalizatua)
                };

                string? aurkitua = aukerak.FirstOrDefault(File.Exists);
                if (!string.IsNullOrWhiteSpace(aurkitua))
                {
                    return aurkitua;
                }
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
                Path.GetDirectoryName(typeof(OinarriPantaila).Assembly.Location)
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

        // -----------------------------------------------------------
        // Eduki panela sortu (FlowLayoutPanel txarteletarako)
        // -----------------------------------------------------------


        // Atzera botoia kudeatzeko metodo zaharra ezabatuta (Designerrak kudeatzen du orain)

        // -----------------------------------------------------------
        // Txartel bat sortu (laguntza-metodoa)
        // -----------------------------------------------------------
        protected MenuTxartelBotoia SortuTxartela(string testua, string ikonoFitxategia)
        {
            var txartela = new MenuTxartelBotoia
            {
                Testua = testua,
                Size = new Size(300, 200),
                Margin = new Padding(20) // Tartea txartelen artean
            };

            txartela.Ikonoa = KargatuIkonoIrudia(ikonoFitxategia);

            return txartela;
        }

        protected Image? KargatuIkonoIrudia(string fitxategiIzena, Color? svgKolorea = null, int svgTamaina = 256)
        {
            string? svgBidea = BilatuBaliabidea("img", "svg", fitxategiIzena);
            return KargatuSvgIkonoCachetik(svgBidea, svgKolorea ?? Color.FromArgb(44, 62, 80), svgTamaina);
        }

        protected Bitmap? KargatuIkonoBitmapa(string fitxategiIzena, Color? svgKolorea = null, int svgTamaina = 256)
        {
            return KargatuIkonoIrudia(fitxategiIzena, svgKolorea, svgTamaina) as Bitmap;
        }

        protected static Bitmap? KargatuSvgIkonoCachetik(string? svgBidea, Color kolorea, int svgTamaina)
        {
            if (string.IsNullOrWhiteSpace(svgBidea) || !File.Exists(svgBidea))
            {
                return null;
            }

            string cacheGakoa = $"{svgBidea}|{kolorea.ToArgb()}|{svgTamaina}";
            if (SvgIkonoCachea.TryGetValue(cacheGakoa, out Bitmap? cachekoIkonoa))
            {
                return new Bitmap(cachekoIkonoa);
            }

            try
            {
                string svgEdukia = File.ReadAllText(svgBidea);
                string ordezkoKolorea = ColorTranslator.ToHtml(kolorea);
                svgEdukia = svgEdukia.Replace("currentColor", ordezkoKolorea, StringComparison.OrdinalIgnoreCase);

                using MemoryStream memoria = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(svgEdukia));
                SvgDocument svg = SvgDocument.Open<SvgDocument>(memoria);
                Bitmap marraztua = svg.Draw(svgTamaina, svgTamaina);
                SvgIkonoCachea.TryAdd(cacheGakoa, new Bitmap(marraztua));
                return marraztua;
            }
            catch
            {
                return null;
            }
        }

        private static string? BilatuBaliabidea(string karpetaNagusia, string azpiKarpeta, string fitxategiIzena)
        {
            foreach (string root in LortuBilaketaErroak())
            {
                string[] aukerak = {
                    Path.Combine(root, karpetaNagusia, azpiKarpeta, fitxategiIzena),
                    Path.Combine(root, "GOsasun_app", karpetaNagusia, azpiKarpeta, fitxategiIzena)
                };

                string? aurkitua = aukerak.FirstOrDefault(File.Exists);
                if (!string.IsNullOrWhiteSpace(aurkitua)) return aurkitua;
            }

            return null;
        }

        protected void IrekiAzpiPantaila(Func<Form> formularioSortzailea, Action? itzultzean = null)
        {
            ArgumentNullException.ThrowIfNull(formularioSortzailea);

            ErakutsiNabigazioKarga();

            BeginInvoke(new Action(() =>
            {
                try
                {
                    Form formularioa = formularioSortzailea();
                    IrekiAzpiPantaila(formularioa, itzultzean);
                }
                catch (Exception ex)
                {
                    GarbituNabigazioKarga();
                    MessageBox.Show(
                        "Ezin izan da pantaila berria ireki: " + ex.Message,
                        "Errorea",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }));
        }

        protected void IrekiAzpiPantaila(Form formularioa, Action? itzultzean = null)
        {
            ErakutsiNabigazioKarga();
            formularioa.Owner = this;
            formularioa.FormClosed += (s, e) =>
            {
                if (!IsDisposed)
                {
                    GarbituNabigazioKarga();
                    itzultzean?.Invoke();
                    AplikatuLeihoTamainaPartekatuaBerehala();
                    Show();
                    ZentratuPantailaLanEremuan();
                    FreskatuPantailaIkusgaiDenean();
                }
            };

            if (formularioa is GOsasunForm hurrengoPantaila)
            {
                GordeLeihoTamainaPartekatua();
                hurrengoPantaila.AplikatuLeihoTamainaPartekatuaBerehala();

                EventHandler? prestHandler = null;
                prestHandler = (s, e) =>
                {
                    hurrengoPantaila.HasierakoAurkezpenaOsatuta -= prestHandler;
                    if (!IsDisposed)
                    {
                        Hide();
                        GarbituNabigazioKarga();
                    }
                };

                hurrengoPantaila.HasierakoAurkezpenaOsatuta += prestHandler;
            }
            else
            {
                Hide();
                GarbituNabigazioKarga();
            }

            formularioa.Show();
        }

        private void ErakutsiNabigazioKarga()
        {
            UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;
        }

        private void GarbituNabigazioKarga()
        {
            UseWaitCursor = false;
            Cursor = Cursors.Default;
        }

        private void GoiburuBarra_ErabiltzaileaKlik(object? sender, EventArgs e)
        {
            if (_erabiltzailea == null || this is NireErabiltzaileFitxa)
            {
                return;
            }

            Erabiltzailea? erabiltzaileOsoa = LortuErabiltzaileOsoa(_erabiltzailea);
            if (erabiltzaileOsoa == null)
            {
                MessageBox.Show(
                    "Ez da posible izan uneko erabiltzailearen fitxa kargatzea.",
                    "Errorea",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            _erabiltzailea = erabiltzaileOsoa;
            _goiburuBarra?.EguneratuInformazioa(_erabiltzailea.IzenOsoa, _erabiltzailea.Rola);
            IrekiAzpiPantaila(() => new NireErabiltzaileFitxa(erabiltzaileOsoa));
        }

        private static Erabiltzailea? LortuErabiltzaileOsoa(Erabiltzailea erabiltzailea)
        {
            PazienteKontrolatzailea pazienteKontrolatzailea = new PazienteKontrolatzailea();
            OsasunLangileKontrolatzailea osasunLangileKontrolatzailea = new OsasunLangileKontrolatzailea();
            HarrerakoLangileKontrolatzailea harrerakoLangileKontrolatzailea = new HarrerakoLangileKontrolatzailea();

            if (erabiltzailea is Pazientea)
            {
                return pazienteKontrolatzailea.LortuPazientea(erabiltzailea.Id);
            }

            if (erabiltzailea is OsasunLangilea)
            {
                return osasunLangileKontrolatzailea.LortuOsasunLangilea(erabiltzailea.Id);
            }

            return harrerakoLangileKontrolatzailea.LortuHarrerakoa(erabiltzailea.Id);
        }

        // -----------------------------------------------------------
        // Saioa itxi gertaera
        // -----------------------------------------------------------
        private void GoiburuBarra_SaioaItxi(object? sender, EventArgs e)
        {
            DialogResult emaitza = MessageBox.Show(
                "Ziur zaude saioa itxi nahi duzula?",
                "Saioa Itxi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (emaitza == DialogResult.Yes)
            {
                try
                {
                    // Saioa hasteko formularioa bilatu (SaioaHasi)
                    var loginForm = Application.OpenForms.OfType<SaioaHasi>().FirstOrDefault();

                    if (loginForm != null)
                    {
                        // Guztiak ezkutatu berehala feedback bisuala emateko (flicker prebenitzeko)
                        var irekiak = Application.OpenForms.Cast<Form>().ToList();
                        foreach (Form f in irekiak)
                        {
                            if (f != loginForm) f.Hide();
                        }

                        // Login pantaila erakutsi
                        loginForm.Show();

                        // Beste guztiak itxi (Dispose erabili gertaera kateak mozteko eta memoria garbitzeko)
                        foreach (Form f in irekiak)
                        {
                            if (f != loginForm) f.Dispose();
                        }
                    }
                    else
                    {
                        // Ez bada aurkitu (kasu arraroa), berria sortu
                        SaioaHasi berria = new SaioaHasi();
                        berria.Show();
                        this.Hide();
                        this.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    // Errore larria bada, aplikazioa restart egin
                    System.Diagnostics.Debug.WriteLine($"Logout errorea: {ex.Message}");
                    Application.Restart();
                }
            }
        }
    }
}
