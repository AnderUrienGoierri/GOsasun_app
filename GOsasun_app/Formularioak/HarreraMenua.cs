using GOsasun_app.Modeloak;

namespace GOsasun_app.Formularioak
{
    /// <summary>
    /// Harrerako langileentzako menu nagusia.
    /// </summary>
    public partial class HarreraMenua : OinarriFormularioa
    {
        public HarreraMenua() : base()
        {
            InitializeComponent();
        }

        public HarreraMenua(Erabiltzailea u) : base(u)
        {
            InitializeComponent();
            KargatuIkonoak();
            KonfiguratuGertaerak();
        }

        private void KargatuIkonoak()
        {
            btnPazienteak.Ikonoa = KargatuIrudia("pazienteak.png");
            btnMedikuak.Ikonoa = KargatuIrudia("medikuak.png");
            btnLangileak.Ikonoa = KargatuIrudia("langileak.png");
            btnHitzorduak.Ikonoa = KargatuIrudia("hitzorduak.png");
        }

        private Image? KargatuIrudia(string fitxategia)
        {
            string path = Path.Combine(Application.StartupPath, "img", "icons", fitxategia);
            if (!File.Exists(path))
            {
                // Bilatu beste karpetetan
                string root = Directory.GetCurrentDirectory();
                string[] aukerak = {
                    Path.Combine(root, "img", "icons", fitxategia),
                    Path.Combine(root, "GOsasun_app", "img", "icons", fitxategia),
                    Path.Combine(root, "..", "..", "..", "img", "icons", fitxategia)
                };
                foreach (var a in aukerak) { if (File.Exists(a)) { path = a; break; } }
            }
            return File.Exists(path) ? Image.FromFile(path) : null;
        }

        private void KonfiguratuGertaerak()
        {
            // Gertakariak (Simulazioa oraingoz, formularioak ez daudelako oraindik)
            btnPazienteak.Click += (s, e) => MessageBox.Show("Pazienteen kudeaketa eraikitzen...", "Informazioa");
            btnMedikuak.Click += (s, e) => MessageBox.Show("Medikuen kudeaketa eraikitzen...", "Informazioa");
            btnLangileak.Click += (s, e) => MessageBox.Show("Langileen kudeaketa eraikitzen...", "Informazioa");
            btnHitzorduak.Click += (s, e) => MessageBox.Show("Hitzorduen kudeaketa eraikitzen...", "Informazioa");
        }
    }
}
