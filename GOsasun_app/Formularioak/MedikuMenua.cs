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
            KonfiguratuGertaerak();
        }

        private void KonfiguratuGertaerak()
        {
<<<<<<< HEAD
            btnPazienteak.Click += (s, e) => IrekiFormularioa(new PazienteenZerrendaFormularioa(_erabiltzailea!));
=======
            // Medikua denez, EZ du Pazienteen menua ireki behar (berriz ere menua delako)
            // Zuzenketa: Mezua erakutsi edo Pazienteen Listua ireki (laster)
            btnPazienteak.Click += (s, e) => MessageBox.Show("Pazienteen zerrenda kudeatzeko formularioa irekiko da laster.");

>>>>>>> ander-branch
            btnKontaktua.Click += (s, e) => IrekiFormularioa(new KontaktuaFormularioa(_erabiltzailea!));
            btnNeurketak.Click += (s, e) => IrekiFormularioa(new NeurketaMotakFormularioa(_erabiltzailea!));
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

        private void MedikuMenua_Load(object sender, EventArgs e)
        {

        }
    }
}
