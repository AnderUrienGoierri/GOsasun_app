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
            KargatuMenuDinamikoa();
        }

        private void KargatuMenuDinamikoa()
        {
            if (_edukiPanela == null) return;
            _edukiPanela.Controls.Clear();

            // Txartelak sortu eta gehitu (3 zutabeko grid-ean automatikoki)
            var btnPazienteak = SortuTxartela("NIRE PAZIENTEAK", "pazienteak.png");
            var btnKontaktua = SortuTxartela("KONTAKTUA", "kontaktua.png");
            var btnNeurketak = SortuTxartela("NEURKETAK", "neurketak.png");
            var btnErrezetak = SortuTxartela("ERREZETAK", "errezetak.png");
            var btnGrafikak = SortuTxartela("GRAFIKAK", "grafikak.png");
            var btnAbisuak = SortuTxartela("ABISUAK", "abisua.png");

            // Gertakariak konfiguratu
            btnPazienteak.Click += (s, e) => IrekiFormularioa(new PazienteMenua(_erabiltzailea!));
            btnKontaktua.Click += (s, e) => IrekiFormularioa(new KontaktuaFormularioa(_erabiltzailea!));
            btnNeurketak.Click += (s, e) => IrekiFormularioa(new NeurketenFormularioa(_erabiltzailea!));
            btnErrezetak.Click += (s, e) => IrekiFormularioa(new ErrezetenFormularioa(_erabiltzailea!));
            btnGrafikak.Click += (s, e) => IrekiFormularioa(new GrafikenFormularioa(_erabiltzailea!));
            btnAbisuak.Click += (s, e) => IrekiFormularioa(new AbisuenFormularioa(_erabiltzailea!));

            _edukiPanela.Controls.AddRange(new Control[] { 
                btnPazienteak, btnKontaktua, btnNeurketak, 
                btnErrezetak, btnGrafikak, btnAbisuak 
            });
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
