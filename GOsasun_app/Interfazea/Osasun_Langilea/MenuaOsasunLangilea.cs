// MenuaOsasunLangilea.cs - Osasun langilearen menu nagusia
// ============================================================
// Aplikazioaren sarrera nagusia login egin ondoren.
// Erabiltzailearen rolaren arabera (Pazientea/OsasunLangilea)
// txartelak dinamikoki kargatzen ditu.
// ============================================================

using GOsasun_app.Interfazea.Kontrolak;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    /// <summary>
    /// Menu nagusiaren formularioa.
    /// Rolaren arabera txartel desberdinak erakusten ditu.
    /// </summary>
    public partial class MenuaOsasunLangilea : OinarriPantaila
    {
        // -----------------------------------------------------------
        // Eraikitzailea
        // -----------------------------------------------------------
        public MenuaOsasunLangilea() : base()
        {
            InitializeComponent();
        }

        public MenuaOsasunLangilea(Erabiltzailea erabiltzailea)
            : base(erabiltzailea)
        {
            InitializeComponent();
            KonfiguratuGertaerak();
        }

        private void KonfiguratuGertaerak()
        {
            btnPazienteak.Click += (s, e) => IrekiFormularioa(new PazienteenZerrenda(_erabiltzailea!));
            btnNeurketak.Click += (s, e) => IrekiFormularioa(new Jarraipenak(_erabiltzailea!));
            btnErrezetak.Click += (s, e) => IrekiFormularioa(new ErrezetakMenua(_erabiltzailea!));
            btnGrafikak.Click += (s, e) => IrekiFormularioa(new Grafikak(_erabiltzailea!));

            // Hitzorduak botoia dinamikoki gehitu
            var btnHitzorduak = new GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia
            {
                Testua = "HITZORDUAK",
                Size = new System.Drawing.Size(576, 512),
                Location = new System.Drawing.Point(650, 597),
                BackColor = System.Drawing.Color.White,
                BorderBiribiltasuna = 24,
                KartaKolorea = System.Drawing.Color.FromArgb(230, 255, 255, 255),
                Padding = new Padding(19, 21, 19, 21)
            };
            
            try { btnHitzorduak.Ikonoa = System.Drawing.Image.FromFile(System.IO.Path.Combine(Application.StartupPath, "img", "icons", "hitzorduak.png")); } catch { }

            btnHitzorduak.Click += (s, e) => IrekiFormularioa(new HitzorduakKontsultatzea(_erabiltzailea!));
            
            _edukiPanela.Controls.Add(btnHitzorduak);
        }

        // -----------------------------------------------------------
        // Azpi-formularioa ireki eta hau ezkutatu (itxita bueltatu)
        // -----------------------------------------------------------
        private void IrekiFormularioa(Form formularioa)
        {
            formularioa.FormClosed += (s, e) => this.Show();
            this.Hide();
            formularioa.Show();
        }

        private void MedikuMenua_Load(object sender, EventArgs e)
        {

        }
    }
}
