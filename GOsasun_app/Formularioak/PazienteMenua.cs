using GOsasun_app.Modeloak;

namespace GOsasun_app.Formularioak
{
    public partial class PazienteMenua : OinarriFormularioa
    {
        public PazienteMenua() : base() 
        { 
            InitializeComponent(); 
        }

        public PazienteMenua(Erabiltzailea u) : base(u) 
        { 
            InitializeComponent(); 
            GehituAtzeraBotoia();
            KargatuMenuDinamikoa();
        }

        private void KargatuMenuDinamikoa()
        {
            if (_edukiPanela == null) return;
            _edukiPanela.Controls.Clear();

            // Txartelak sortu
            var btnNeurketak = SortuTxartela("NIRE NEURKETAK", "neurketak.png");
            var btnErrezetak = SortuTxartela("NIRE ERREZETAK", "errezetak.png");
            var btnKontaktua = SortuTxartela("KONTAKTUA", "kontaktua.png");
            var btnGrafikak = SortuTxartela("GRAFIKAK", "grafikak.png");
            var btnAbisuak = SortuTxartela("ABISUAK", "abisua.png");

            // Gertakariak
            btnNeurketak.Click += (s, e) => IrekiFormularioa(new NeurketenFormularioa(_erabiltzailea!));
            btnErrezetak.Click += (s, e) => IrekiFormularioa(new ErrezetenFormularioa(_erabiltzailea!));
            btnKontaktua.Click += (s, e) => IrekiFormularioa(new KontaktuaFormularioa(_erabiltzailea!));
            btnGrafikak.Click += (s, e) => IrekiFormularioa(new GrafikenFormularioa(_erabiltzailea!));
            btnAbisuak.Click += (s, e) => IrekiFormularioa(new AbisuenFormularioa(_erabiltzailea!));

            _edukiPanela.Controls.AddRange(new Control[] { 
                btnNeurketak, btnErrezetak, btnKontaktua, btnGrafikak, btnAbisuak 
            });
        }

        private void IrekiFormularioa(Form formularioa)
        {
            formularioa.FormClosed += (s, e) => this.Show();
            this.Hide();
            formularioa.Show();
        }
    }
}
