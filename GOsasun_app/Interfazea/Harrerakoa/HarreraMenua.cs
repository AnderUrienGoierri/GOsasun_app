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

            var btnDokumentuak = new MenuTxartelBotoia
            {
                Testua = "DOKUMENTUAK",
                Size = new Size(576, 512),
                Location = new Point(650, 597),
                BackColor = Color.White,
                BorderBiribiltasuna = 24,
                KartaKolorea = Color.FromArgb(230, 255, 255, 255),
                Padding = new Padding(19, 21, 19, 21)
            };

            btnDokumentuak.Ikonoa = KargatuIkonoIrudia("dokumentuak.svg");

            btnDokumentuak.Click += (s, e) => IrekiFormularioa(new Dokumentuak(_erabiltzailea!));
            _edukiPanela.Controls.Add(btnDokumentuak);
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
