// HarreraMenua.cs - Harrerako langilearen Menua (Receptionist Menu)
// ============================================================

using GOsasun_app.Interfazea.Kontrolak;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class HarreraMenua : GoiburuPanela
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
            btnHitzorduak.Click += (s, e) => 
            {
                var h = new HitzorduKudeaketa(_erabiltzailea!);
                h.FormClosed += (sender, args) => this.Show();
                this.Hide();
                h.Show();
            };
        }
    }
}
