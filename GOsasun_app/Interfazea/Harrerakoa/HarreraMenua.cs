// HarreraMenua.cs - Harrerako langilearen Menua (Receptionist Menu)
// ============================================================

using GOsasun_app.Interfazea.Kontrolak;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class HarreraMenua : OinarriPantaila
    {
        public HarreraMenua() : base()
        {
            InitializeComponent();
        }

        public HarreraMenua(Erabiltzailea erabiltzailea)
            : base(erabiltzailea)
        {
            InitializeComponent();
            KonfiguratuGertaerak();
        }

        private void KonfiguratuGertaerak()
        {
            btnPazienteak.Click += (s, e) => IrekiKudeaketa("Pazientea");
            btnMedikuak.Click += (s, e) => IrekiKudeaketa("Osasun Langilea");
            btnLangileak.Click += (s, e) => IrekiKudeaketa("Harrerako Langilea");
            btnHitzorduak.Click += (s, e) =>
            {
                var h = new HitzorduKudeaketa(_erabiltzailea!);
                h.FormClosed += (sender, args) => this.Show();
                this.Hide();
                h.Show();
            };
        }

        private void IrekiKudeaketa(string rolIzena)
        {
            var m = new ErabiltzaileKudeaketaMenua(rolIzena, _erabiltzailea!);
            m.FormClosed += (sender, args) => this.Show();
            this.Hide();
            m.Show();
        }

        private void btnLangileak_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
