// MedikuMenua.cs - Medikuaren Menua (Doctor Menu)
// ============================================================
// Aplikazioaren sarrera nagusia login egin ondoren.
// Erabiltzailearen rolaren arabera (Pazientea/Medikua)
// txartelak dinamikoki kargatzen ditu.
// ============================================================

using GOsasun_app.Kontrolak;
using GOsasun_app.Modeloak;

namespace GOsasun_app.Formularioak
{
    /// <summary>
    /// Menu nagusiaren formularioa.
    /// Rolaren arabera txartel desberdinak erakusten ditu.
    /// </summary>
    public partial class MedikuMenua : OinarriFormularioa
    {
        // -----------------------------------------------------------
        // Eraikitzailea
        // -----------------------------------------------------------
        public MedikuMenua() : base()
        {
            InitializeComponent();
        }

        public MedikuMenua(Erabiltzailea erabiltzailea)
            : base(erabiltzailea)
        {
            InitializeComponent();
            KargatuIkonoak();
            KonfiguratuGertaerak();
        }

        private void KargatuIkonoak()
        {
            btnPazienteak.Ikonoa = KargatuIrudia("pazienteak.png");
            btnKontaktua.Ikonoa = KargatuIrudia("kontaktua.png");
            btnNeurketak.Ikonoa = KargatuIrudia("neurketak.png");
            btnErrezetak.Ikonoa = KargatuIrudia("errezetak.png");
            btnGrafikak.Ikonoa = KargatuIrudia("grafikak.png");
            btnAbisuak.Ikonoa = KargatuIrudia("abisua.png");
        }

        private Image? KargatuIrudia(string fitxategia)
        {
            string path = Path.Combine(Application.StartupPath, "img", fitxategia);
            if (!File.Exists(path))
            {
                // Bilatu beste karpetetan
                string root = Directory.GetCurrentDirectory();
                string[] aukerak = {
                    Path.Combine(root, "img", fitxategia),
                    Path.Combine(root, "GOsasun_app", "img", fitxategia),
                    Path.Combine(root, "..", "..", "..", "img", fitxategia)
                };
                foreach (var a in aukerak) { if (File.Exists(a)) { path = a; break; } }
            }
            return File.Exists(path) ? Image.FromFile(path) : null;
        }

        private void KonfiguratuGertaerak()
        {
            if (this.DesignMode) return;
            if (_erabiltzailea == null) return;

            btnPazienteak.Click += (s, e) => IrekiFormularioa(new PazienteMenua(_erabiltzailea!));
            btnKontaktua.Click += (s, e) => IrekiFormularioa(new KontaktuaFormularioa(_erabiltzailea!));
            btnNeurketak.Click += (s, e) => IrekiFormularioa(new NeurketenFormularioa(_erabiltzailea!));
            btnErrezetak.Click += (s, e) => IrekiFormularioa(new ErrezetenFormularioa(_erabiltzailea!));
            btnGrafikak.Click += (s, e) => IrekiFormularioa(new GrafikenFormularioa(_erabiltzailea!));
            btnAbisuak.Click += (s, e) => IrekiFormularioa(new AbisuenFormularioa(_erabiltzailea!));
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

        private void btnGrafikak_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
