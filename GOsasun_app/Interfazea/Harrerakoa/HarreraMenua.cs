// HarreraMenua.cs - Harrerako langilearen Menua (Receptionist Menu)
// ============================================================

using GOsasun_app.Interfazea.Kontrolak;
using GOsasun_app.Modeloa;
using System.Drawing;

namespace GOsasun_app.Interfazea
{
    public partial class HarreraMenua : OinarriPantaila
    {
        public HarreraMenua() : base()
        {
            InitializeComponent();
            KargatuIkonoak();
        }

        public HarreraMenua(Erabiltzailea erabiltzailea)
            : base(erabiltzailea)
        {
            InitializeComponent();
            KargatuIkonoak();
            KonfiguratuGertaerak();
        }

        private void KargatuIkonoak()
        {
            btnPazienteak.Ikonoa = KargatuIkonoIrudia("users.svg");
            btnMedikuak.Ikonoa = KargatuIkonoIrudia("stethoscope.svg");
            btnLangileak.Ikonoa = KargatuIkonoIrudia("user-cog.svg");
            btnHitzorduak.Ikonoa = KargatuIkonoIrudia("calendar-days.svg");
            btnDokumentuak.Ikonoa = KargatuIkonoIrudia("dokumentuak.svg");
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

            btnDokumentuak.Click += (s, e) => IrekiFormularioa(new Dokumentuak(_erabiltzailea!));
        }

        private void IrekiKudeaketa(string rolIzena)
        {
            var m = new ErabiltzaileKudeaketaMenua(rolIzena, _erabiltzailea!);
            m.FormClosed += (sender, args) => this.Show();
            this.Hide();
            m.Show();
        }

        private void IrekiFormularioa(Form formularioa)
        {
            formularioa.FormClosed += (sender, args) => this.Show();
            this.Hide();
            formularioa.Show();
        }

        private void btnLangileak_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
