// ============================================================
// OinarriPantaila.cs - Formulario oinarria (Base Form)
// ============================================================
// Formulario guztiek heredatzen duten oinarri klasea.
// Portadako atzeko planoa, GoiburuBarra eta atzera-botoia
// kudeatzen ditu.
// ============================================================

using GOsasun_app.Interfazea.Kontrolak;
using GOsasun_app.Modeloa;
using System.ComponentModel;

namespace GOsasun_app.Interfazea
{
    /// <summary>
    /// Formulario guztien oinarri klasea.
    /// Egurrezko atzeko planoa eta nabigazio estandarra eskaintzen ditu.
    /// </summary>
    public partial class OinarriPantaila : Form
    {
        private static readonly Size PortadaIrudiTamaina = new Size(1514, 1394);
        private static readonly Size OinarriPantailaTamaina = new Size(1902, 1394);
        private static readonly Color PortadaAtzekoKolorea = Color.FromArgb(214, 224, 229);

        // Erabiltzaile informazioa (OOP)
        protected Erabiltzailea? _erabiltzailea;

        // -----------------------------------------------------------
        // Eraikitzailea
        // -----------------------------------------------------------
        public OinarriPantaila()
        {
            InitializeComponent();
            KonfiguratuFormularioa();
            KargatuBaliabideak();
            EgokituPortadarenNeurrira();

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
                _goiburuBarra.SaioaItxi += GoiburuBarra_SaioaItxi;
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
            panela.BackColor = Color.Transparent;
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
                this.BackgroundImage = Image.FromFile(atzekoPlanoaBidea);
                this.BackgroundImageLayout = ImageLayout.Center;
                if (_edukiPanela != null)
                {
                    _edukiPanela.BackgroundImage = this.BackgroundImage;
                    _edukiPanela.BackgroundImageLayout = ImageLayout.Center;
                }
                this.ClientSize = new Size(Math.Max(OinarriPantailaTamaina.Width, this.BackgroundImage.Width), Math.Max(OinarriPantailaTamaina.Height, this.BackgroundImage.Height));
            }
            else
            {
                this.BackColor = PortadaAtzekoKolorea;
                if (_edukiPanela != null)
                {
                    _edukiPanela.BackgroundImage = null;
                }
                this.ClientSize = OinarriPantailaTamaina;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EgokituPortadarenNeurrira();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            EgokituPortadarenNeurrira();
        }

        private void EgokituPortadarenNeurrira()
        {
            Size irudiTamaina = this.BackgroundImage?.Size ?? PortadaIrudiTamaina;
            Size helmugaTamaina = new Size(
                Math.Max(OinarriPantailaTamaina.Width, irudiTamaina.Width),
                Math.Max(OinarriPantailaTamaina.Height, irudiTamaina.Height));

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
                _edukiPanela.Location = new Point(0, _goiburuBarra.Bottom);
                _edukiPanela.Size = new Size(helmugaTamaina.Width, helmugaTamaina.Height - _goiburuBarra.Height);
            }

            if (_atzeraBotoia != null)
            {
                _atzeraBotoia.BringToFront();
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

            // Ikonoa kargatu
            string ikonoBidea = Path.Combine(Application.StartupPath, "img", "icons", ikonoFitxategia);

            // Garapen moduko fallback-ak
            if (!File.Exists(ikonoBidea))
            {
                string root = Directory.GetCurrentDirectory();
                string[] saioak = {
                    Path.Combine(root, "img", "icons", ikonoFitxategia),
                    Path.Combine(root, "GOsasun_app", "img", "icons", ikonoFitxategia),
                    Path.Combine(root, "..", "..", "..", "img", "icons", ikonoFitxategia),
                    Path.Combine(root, "..", "..", "..", "GOsasun_app", "img", "icons", ikonoFitxategia)
                };

                foreach (string s in saioak)
                {
                    if (File.Exists(s)) { ikonoBidea = s; break; }
                }
            }

            if (File.Exists(ikonoBidea))
            {
                txartela.Ikonoa = Image.FromFile(ikonoBidea);
            }

            return txartela;
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
