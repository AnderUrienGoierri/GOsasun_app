// PazienteMenua.cs - Pazientearen Menua (Patient Menu)
// ============================================================

using GOsasun_app.Kontrolak;
using GOsasun_app.Modeloak;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace GOsasun_app.Formularioak
{
    public partial class PazienteMenua : OinarriFormularioa
    {
        public PazienteMenua() : base()
        {
            InitializeComponent();
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
            try
            {
                string iconsPath = Path.Combine(Application.StartupPath, "img", "icons");
                
                btnNeurketak.Ikonoa = Image.FromFile(Path.Combine(iconsPath, "neurketak.png"));
                btnErrezetak.Ikonoa = Image.FromFile(Path.Combine(iconsPath, "errezetak.png"));
                btnKontaktua.Ikonoa = Image.FromFile(Path.Combine(iconsPath, "kontaktua.png"));
                btnGrafikak.Ikonoa = Image.FromFile(Path.Combine(iconsPath, "grafikak.png"));
                btnAbisuak.Ikonoa = Image.FromFile(Path.Combine(iconsPath, "abisua.png"));
            }
            catch (Exception ex)
            {
                // Isilpean kudeatu edo log-ean idatzi (erakustaldian ez errorea bota)
                System.Diagnostics.Debug.WriteLine("Errorea ikonoak kargatzean: " + ex.Message);
            }
        }

        private void KonfiguratuGertaerak()
        {
            btnNeurketak.Click += (s, e) => IrekiFormularioa(new NireNeurketakFormularioa(_erabiltzailea!));
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

        private void btnErrezetak_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
