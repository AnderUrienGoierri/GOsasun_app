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
            KargatuMenuDinamikoa();
        }

        private void KargatuMenuDinamikoa()
        {
            if (_edukiPanela == null) return;
            _edukiPanela.Controls.Clear();

            // Txartelak sortu
            var btnPazienteak = SortuTxartela("PAZIENTEAK KUDEATU", "pazienteak.png");
            var btnMedikuak = SortuTxartela("MEDIKUAK KUDEATU", "medikuak.png");
            var btnLangileak = SortuTxartela("LANGILEAK KUDEATU", "langileak.png");
            var btnHitzorduak = SortuTxartela("HITZORDUAK KUDEATU", "hitzorduak.png");

            // Gertakariak (Simulazioa oraingoz, formularioak ez daudelako oraindik)
            btnPazienteak.Click += (s, e) => MessageBox.Show("Pazienteen kudeaketa eraikitzen...", "Informazioa");
            btnMedikuak.Click += (s, e) => MessageBox.Show("Medikuen kudeaketa eraikitzen...", "Informazioa");
            btnLangileak.Click += (s, e) => MessageBox.Show("Langileen kudeaketa eraikitzen...", "Informazioa");
            btnHitzorduak.Click += (s, e) => MessageBox.Show("Hitzorduen kudeaketa eraikitzen...", "Informazioa");

            _edukiPanela.Controls.AddRange(new Control[] { 
                btnPazienteak, btnMedikuak, btnLangileak, btnHitzorduak 
            });
        }
    }
}
