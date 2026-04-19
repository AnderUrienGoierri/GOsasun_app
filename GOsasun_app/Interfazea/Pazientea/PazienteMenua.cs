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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EguneratuTxartelenDiseinua();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            BeginInvoke(new Action(EguneratuTxartelenDiseinua));
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (!DiseinuModuan())
            {
                EguneratuTxartelenDiseinua();
            }
        }

        private void KargatuIkonoak()
        {
            btnNeurketak.Ikonoa = KargatuIkonoIrudia("stethoscope.svg");
            btnErrezetak.Ikonoa = KargatuIkonoIrudia("pill.svg");
            btnGrafikak.Ikonoa = KargatuIkonoIrudia("line-chart.svg");
            btnDokumentuak.Ikonoa = KargatuIkonoIrudia("dokumentuak.svg");
            btnHitzorduak.Ikonoa = KargatuIkonoIrudia("calendar-days.svg");
        }

        private void KonfiguratuGertaerak()
        {
            btnNeurketak.Click += (s, e) => IrekiFormularioa(() => new Jarraipenak(_erabiltzailea!, _erabiltzailea!.Id, _erabiltzailea.IzenOsoa));
            btnErrezetak.Click += (s, e) => IrekiFormularioa(() => new ErrezetakIkusi(_erabiltzailea!));
            btnGrafikak.Click += (s, e) => IrekiFormularioa(() => new Grafikak(_erabiltzailea!));
            btnDokumentuak.Click += (s, e) => IrekiFormularioa(() => new Dokumentuak(_erabiltzailea!));
            btnHitzorduak.Click += (s, e) => IrekiFormularioa(() => new HitzorduakKontsultatzea(_erabiltzailea!));
        }

        private void IrekiFormularioa(Func<Form> formularioSortzailea)
        {
            IrekiAzpiPantaila(formularioSortzailea);
        }

        private void IrekiFormularioa(Form formularioa)
        {
            IrekiAzpiPantaila(formularioa);
        }

        private void EguneratuTxartelenDiseinua()
        {
            if (_edukiPanela == null
                || btnNeurketak == null
                || btnErrezetak == null
                || btnGrafikak == null
                || btnDokumentuak == null
                || btnHitzorduak == null
                || _edukiPanela.ClientSize.Width <= 0)
            {
                return;
            }

            MenuTxartelBotoia[] txartelak =
            {
                btnNeurketak,
                btnErrezetak,
                btnGrafikak,
                btnDokumentuak,
                btnHitzorduak
            };

            int zabalera = _edukiPanela.ClientSize.Width;
            int altuera = _edukiPanela.ClientSize.Height;
            int kanpoMarjina = zabalera < 1100 ? 28 : 36;
            int tartea = zabalera < 1100 ? 20 : 26;
            int zutabeKopurua = zabalera < 1100 ? 2 : 3;
            int errenkadaKopurua = (int)Math.Ceiling(txartelak.Length / (double)zutabeKopurua);
            int txartelZabalera = (zabalera - (kanpoMarjina * 2) - (tartea * (zutabeKopurua - 1))) / zutabeKopurua;
            txartelZabalera = Math.Max(250, txartelZabalera);
            int goikoMarjina = 20;
            int behekoMarjina = 20;
            int txartelAltueraMax = (altuera - goikoMarjina - behekoMarjina - (tartea * (errenkadaKopurua - 1))) / errenkadaKopurua;
            int txartelAltuera = Math.Max(180, Math.Min(txartelAltueraMax, (int)Math.Round(txartelZabalera * 0.66)));

            for (int i = 0; i < txartelak.Length; i++)
            {
                int errenkada = i / zutabeKopurua;
                int zutabea = i % zutabeKopurua;
                int x = kanpoMarjina + (zutabea * (txartelZabalera + tartea));
                int y = goikoMarjina + (errenkada * (txartelAltuera + tartea));
                txartelak[i].Bounds = new Rectangle(x, y, txartelZabalera, txartelAltuera);
            }

            int beharrezkoAltuera = goikoMarjina + (errenkadaKopurua * txartelAltuera) + ((errenkadaKopurua - 1) * tartea) + behekoMarjina;
            _edukiPanela.AutoScrollMinSize = new Size(0, beharrezkoAltuera);
        }

        private void btnErrezetak_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
