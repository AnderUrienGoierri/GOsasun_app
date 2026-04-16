// PazienteMenua.cs - Pazientearen Menua (Patient Menu)
// ============================================================

using GOsasun_app.Interfazea.Kontrolak;
using GOsasun_app.Modeloa;
using System.Windows.Forms;

namespace GOsasun_app.Interfazea
{
    public partial class PazienteMenua : OinarriPantaila
    {
        public PazienteMenua() : base()
        {
            InitializeComponent();
            KargatuIkonoak();
        }

        public PazienteMenua(Erabiltzailea erabiltzailea)
            : base(erabiltzailea)
        {
            InitializeComponent();
            KargatuIkonoak();
            KonfiguratuGertaerak();
        }

        private void KargatuIkonoak()
        {
            btnNeurketak.Ikonoa = KargatuIkonoIrudia("stethoscope.svg");
            btnErrezetak.Ikonoa = KargatuIkonoIrudia("pill.svg");
            btnGrafikak.Ikonoa = KargatuIkonoIrudia("line-chart.svg");
        }

        private void KonfiguratuGertaerak()
        {
            btnNeurketak.Click += (s, e) => IrekiFormularioa(new NireJarraipenak(_erabiltzailea!));
            btnErrezetak.Click += (s, e) => IrekiFormularioa(new ErrezetaSortu(_erabiltzailea!));
            btnGrafikak.Click += (s, e) => IrekiFormularioa(new Grafikak(_erabiltzailea!));

            var btnDokumentuak = new GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia
            {
                Testua = "DOKUMENTUAK",
                Size = new System.Drawing.Size(576, 512),
                Location = new System.Drawing.Point(37, 597),
                BackColor = System.Drawing.Color.White,
                BorderBiribiltasuna = 24,
                KartaKolorea = System.Drawing.Color.FromArgb(230, 255, 255, 255),
                Padding = new Padding(19, 21, 19, 21)
            };

            btnDokumentuak.Ikonoa = KargatuIkonoIrudia("dokumentuak.svg");

            btnDokumentuak.Click += (s, e) => IrekiFormularioa(new Dokumentuak(_erabiltzailea!));
            _edukiPanela.Controls.Add(btnDokumentuak);

            // Hitzorduak botoia erantsi dinamikoki
            var btnHitzorduak = new GOsasun_app.Interfazea.Kontrolak.MenuTxartelBotoia
            {
                Testua = "NIRE HITZORDUAK",
                Size = new System.Drawing.Size(576, 512),
                Location = new System.Drawing.Point(650, 597),
                BackColor = System.Drawing.Color.White,
                BorderBiribiltasuna = 24,
                KartaKolorea = System.Drawing.Color.FromArgb(230, 255, 255, 255),
                Padding = new Padding(19, 21, 19, 21)
            };
            
            btnHitzorduak.Ikonoa = KargatuIkonoIrudia("calendar-days.svg");

            btnHitzorduak.Click += (s, e) => IrekiFormularioa(new HitzorduakKontsultatzea(_erabiltzailea!));
            
            _edukiPanela.Controls.Add(btnHitzorduak);
        }

        private void IrekiFormularioa(Form formularioa)
        {
            formularioa.FormClosed += (s, e) => this.Show();
            this.Hide();
            formularioa.Show();
        }

        private void btnErrezetak_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
