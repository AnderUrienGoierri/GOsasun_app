// HarreraMenua.cs - Harrerako langilearen Menua (Receptionist Menu)
// ============================================================

using GOsasun_app.Kontrolak;
using GOsasun_app.Modeloak;

namespace GOsasun_app.Formularioak
{
    public partial class HarreraMenua : OinarriFormularioa
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
            btnPazienteak.Click += (s, e) => MessageBox.Show("Pazienteak kudeatzeko formularioa irekiko da laster.");
            btnMedikuak.Click += (s, e) => MessageBox.Show("Medikuak kudeatzeko formularioa irekiko da laster.");
            btnLangileak.Click += (s, e) => MessageBox.Show("Harrerako langileak kudeatzeko formularioa irekiko da laster.");
            btnHitzorduak.Click += (s, e) => MessageBox.Show("Hitzorduak kudeatzeko formularioa irekiko da laster.");
        }
    }
}
