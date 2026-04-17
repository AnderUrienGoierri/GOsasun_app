// ============================================================
// OinarriPantaila.cs - Formulario oinarria (Base Form)
// ============================================================
// Formulario guztiek heredatzen duten oinarri klasea.
// Portadako atzeko planoa, GoiburuBarra eta atzera-botoia
// kudeatzen ditu.
// ============================================================

using GOsasun_app.Interfazea.Kontrolak;
using GOsasun_app.Kontrola;
using GOsasun_app.Modeloa;
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
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.ClientSize = OinarriPantailaTamaina;
            this.BackColor = PortadaAtzekoKolorea;

            // Eduki panela konfiguratu
            KonfiguratuEdukiPanela(_edukiPanela);
        }

        private void KonfiguratuEdukiPanela(Panel panela)
        {
            panela.Dock = DockStyle.Fill;
            panela.AutoScroll = true;
            panela.BackColor = PortadaAtzekoKolorea;
            panela.Paint -= EdukiPanela_Paint; 
            panela.Paint += EdukiPanela_Paint;
            panela.Resize -= EdukiPanela_Resize;
            panela.Resize += EdukiPanela_Resize;
            // Padding manualki kudeatuko dugu elementuen X,Y bitartez diseinatzailean
        }

        // -----------------------------------------------------------
        // Irudiak kargatu (Konstruktorian deitua)
        // -----------------------------------------------------------
        protected void KargatuBaliabideak()
        {
            string? atzekoPlanoaBidea = BilatuPortadaBidea();
            if (!string.IsNullOrEmpty(atzekoPlanoaBidea) && File.Exists(atzekoPlanoaBidea))
            {
                _atzekoPlanoaIrudia?.Dispose();
                using Image jatorrizkoIrudia = Image.FromFile(atzekoPlanoaBidea);
                _atzekoPlanoaIrudia = new Bitmap(jatorrizkoIrudia);

                this.BackgroundImage = null;
                if (_edukiPanela != null)
                {
                    _edukiPanela.BackgroundImage = null;
                    _edukiPanela.Invalidate();
                }

                int goiburuAltuera = _goiburuBarra?.Height ?? 0;
                this.ClientSize = new Size(
                    Math.Max(OinarriPantailaTamaina.Width, _atzekoPlanoaIrudia.Width),
                    Math.Max(OinarriPantailaTamaina.Height, _atzekoPlanoaIrudia.Height + goiburuAltuera));
            }
            else
            {
                _atzekoPlanoaIrudia?.Dispose();
                _atzekoPlanoaIrudia = null;
                this.BackColor = PortadaAtzekoKolorea;
                if (_edukiPanela != null)
                {
                    _edukiPanela.BackgroundImage = null;
                    _edukiPanela.Invalidate();
                }
                this.ClientSize = OinarriPantailaTamaina;
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
            BeginInvoke(new Action(ZentratuPantailaLanEremuan));
        }

        protected void ZentratuPantailaLanEremuan()
        {
            if (DiseinuModuan())
            {
                return;
            }

            Rectangle lanEremua = Owner != null
                ? Screen.FromControl(Owner).WorkingArea
                : Screen.FromControl(this).WorkingArea;

            int x = lanEremua.Left + Math.Max(0, (lanEremua.Width - Width) / 2);
            int y = lanEremua.Top + Math.Max(0, (lanEremua.Height - Height) / 2);
            Location = new Point(x, y);
        }

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
                _edukiPanela.Invalidate();
            }

            if (_atzeraBotoia != null)
            {
                _atzeraBotoia.BringToFront();
            }
        }

        private void EdukiPanela_Resize(object? sender, EventArgs e)
        {
            _edukiPanela?.Invalidate();
        }

        private void EdukiPanela_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Panel panela)
            {
                return;
            }

            e.Graphics.Clear(PortadaAtzekoKolorea);

            if (_atzekoPlanoaIrudia == null)
            {
                return;
            }

            int x = Math.Max(0, (panela.ClientSize.Width - _atzekoPlanoaIrudia.Width) / 2);
            e.Graphics.DrawImage(_atzekoPlanoaIrudia, x, 0, _atzekoPlanoaIrudia.Width, _atzekoPlanoaIrudia.Height);
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

        protected bool DiseinuModuan()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode || (Site?.DesignMode ?? false);
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
            if (string.IsNullOrWhiteSpace(svgBidea) || !File.Exists(svgBidea))
            {
                return null;
            }

            try
            {
                string svgEdukia = File.ReadAllText(svgBidea);
                string ordezkoKolorea = ColorTranslator.ToHtml(svgKolorea ?? Color.FromArgb(44, 62, 80));
                svgEdukia = svgEdukia.Replace("currentColor", ordezkoKolorea, StringComparison.OrdinalIgnoreCase);

                using MemoryStream memoria = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(svgEdukia));
                SvgDocument svg = SvgDocument.Open<SvgDocument>(memoria);
                return svg.Draw(svgTamaina, svgTamaina);
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

        protected void IrekiAzpiPantaila(Form formularioa)
        {
            formularioa.Owner = this;
            formularioa.FormClosed += (s, e) =>
            {
                if (!IsDisposed)
                {
                    Show();
                    ZentratuPantailaLanEremuan();
                }
            };

            Hide();
            formularioa.Show();
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
            IrekiAzpiPantaila(new NireErabiltzaileFitxa(erabiltzaileOsoa));
        }

        private static Erabiltzailea? LortuErabiltzaileOsoa(Erabiltzailea erabiltzailea)
        {
            ErabiltzaileKontrolatzailea kontrolatzailea = new ErabiltzaileKontrolatzailea();

            if (erabiltzailea is Pazientea)
            {
                return kontrolatzailea.LortuPazientea(erabiltzailea.Id);
            }

            if (erabiltzailea is OsasunLangilea)
            {
                return kontrolatzailea.LortuOsasunLangilea(erabiltzailea.Id);
            }

            return kontrolatzailea.LortuHarrerakoa(erabiltzailea.Id);
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
