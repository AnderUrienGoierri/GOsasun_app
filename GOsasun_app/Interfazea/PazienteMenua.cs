// PazienteMenua.cs - Pazientearen Menua (Patient Menu)
// ============================================================

using GOsasun_app.Interfazea.Kontrolak;
using GOsasun_app.Modeloa;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace GOsasun_app.Interfazea
{
    public partial class PazienteMenua : GoiburuPanela
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
            btnNeurketak.Click += (s, e) => IrekiFormularioa(new NireNeurketak(_erabiltzailea!));
            btnErrezetak.Click += (s, e) => IrekiFormularioa(new Errezetak(_erabiltzailea!));
            btnKontaktua.Click += (s, e) => IrekiFormularioa(new Kontaktua(_erabiltzailea!));
            btnGrafikak.Click += (s, e) => IrekiFormularioa(new Grafikak(_erabiltzailea!));
            btnAbisuak.Click += (s, e) => IrekiFormularioa(new Abisuak(_erabiltzailea!));

            // Hitzorduak botoia erantsi dinamikoki
            var btnHitzorduak = new GOsasun_app.Interfazea.Kontrolak.CustomCardButton
            {
                Testua = "NIRE HITZORDUAK",
                Size = new System.Drawing.Size(576, 512),
                Location = new System.Drawing.Point(1263, 597), // Eskuinean behean kokatuta (edo 1150 bada ilara berria behar bada)
                BackColor = System.Drawing.Color.White,
                BorderBiribiltasuna = 24,
                KartaKolorea = System.Drawing.Color.FromArgb(230, 255, 255, 255),
                Padding = new Padding(19, 21, 19, 21)
            };
            
            try { btnHitzorduak.Ikonoa = Image.FromFile(System.IO.Path.Combine(Application.StartupPath, "img", "icons", "hitzorduak.png")); } catch { }

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
