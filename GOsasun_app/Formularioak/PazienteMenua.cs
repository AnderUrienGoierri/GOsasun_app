using GOsasun_app.Modeloak;

namespace GOsasun_app.Formularioak
{
    public partial class PazienteMenua : OinarriFormularioa
    {
        public PazienteMenua() : base() 
        { 
            InitializeComponent(); 
        }

        public PazienteMenua(Erabiltzailea u) : base(u) 
        { 
            InitializeComponent(); 
            GehituAtzeraBotoia();
            KargatuIkonoak();
            KonfiguratuGertaerak();
        }

        private void KargatuIkonoak()
        {
            btnNeurketak.Ikonoa = KargatuIrudia("neurketak.png");
            btnErrezetak.Ikonoa = KargatuIrudia("errezetak.png");
            btnKontaktua.Ikonoa = KargatuIrudia("kontaktua.png");
            btnGrafikak.Ikonoa = KargatuIrudia("grafikak.png");
            btnAbisuak.Ikonoa = KargatuIrudia("abisua.png");
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
            btnNeurketak.Click += (s, e) => IrekiFormularioa(new NeurketenFormularioa(_erabiltzailea!));
            btnErrezetak.Click += (s, e) => IrekiFormularioa(new ErrezetenFormularioa(_erabiltzailea!));
            btnKontaktua.Click += (s, e) => IrekiFormularioa(new KontaktuaFormularioa(_erabiltzailea!));
            btnGrafikak.Click += (s, e) => IrekiFormularioa(new GrafikenFormularioa(_erabiltzailea!));
            btnAbisuak.Click += (s, e) => IrekiFormularioa(new AbisuenFormularioa(_erabiltzailea!));
        }

        private void IrekiFormularioa(Form formularioa)
        {
            formularioa.FormClosed += (s, e) => this.Show();
            this.Hide();
            formularioa.Show();
        }
    }
}
