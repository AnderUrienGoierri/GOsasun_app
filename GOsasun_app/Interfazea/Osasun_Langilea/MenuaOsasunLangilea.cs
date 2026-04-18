// MenuaOsasunLangilea.cs - Osasun langilearen menu nagusia
// ============================================================
// Aplikazioaren sarrera nagusia login egin ondoren.
// Erabiltzailearen rolaren arabera (Pazientea/OsasunLangilea)
// txartelak dinamikoki kargatzen ditu.
// ============================================================

using GOsasun_app.Interfazea.Kontrolak;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    /// <summary>
    /// Menu nagusiaren formularioa.
    /// Rolaren arabera txartel desberdinak erakusten ditu.
    /// </summary>
    public partial class MenuaOsasunLangilea : OinarriPantaila
    {
        // -----------------------------------------------------------
        // Eraikitzailea
        // -----------------------------------------------------------
        public MenuaOsasunLangilea() : base()
        {
            InitializeComponent();
            KargatuIkonoak();
        }

        public MenuaOsasunLangilea(Erabiltzailea erabiltzailea)
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
            btnPazienteak.Ikonoa = KargatuIkonoIrudia("users.svg");
            btnNeurketak.Ikonoa = KargatuIkonoIrudia("stethoscope.svg");
            btnErrezetak.Ikonoa = KargatuIkonoIrudia("pill.svg");
            btnGrafikak.Ikonoa = KargatuIkonoIrudia("line-chart.svg");
            btnDokumentuak.Ikonoa = KargatuIkonoIrudia("dokumentuak.svg");
            btnHitzorduak.Ikonoa = KargatuIkonoIrudia("calendar-days.svg");
        }

        private void KonfiguratuGertaerak()
        {
            btnPazienteak.Click += (s, e) => IrekiFormularioa(new PazienteenZerrenda(_erabiltzailea!));
            btnNeurketak.Click += (s, e) => IrekiFormularioa(new Jarraipenak(_erabiltzailea!));
            btnErrezetak.Click += (s, e) => IrekiFormularioa(new ErrezetakMenua(_erabiltzailea!));
            btnGrafikak.Click += (s, e) => IrekiFormularioa(new Grafikak(_erabiltzailea!));
            btnDokumentuak.Click += (s, e) => IrekiFormularioa(new Dokumentuak(_erabiltzailea!));
            btnHitzorduak.Click += (s, e) => IrekiFormularioa(new HitzorduakKontsultatzea(_erabiltzailea!));
        }

        // -----------------------------------------------------------
        // Azpi-formularioa ireki eta hau ezkutatu (itxita bueltatu)
        // -----------------------------------------------------------
        private void IrekiFormularioa(Form formularioa)
        {
            IrekiAzpiPantaila(formularioa);
        }

        private void EguneratuTxartelenDiseinua()
        {
            if (_edukiPanela == null
                || btnPazienteak == null
                || btnNeurketak == null
                || btnErrezetak == null
                || btnGrafikak == null
                || btnHitzorduak == null
                || btnDokumentuak == null
                || _edukiPanela.ClientSize.Width <= 0)
            {
                return;
            }

            MenuTxartelBotoia[] txartelak =
            {
                btnPazienteak,
                btnNeurketak,
                btnErrezetak,
                btnGrafikak,
                btnHitzorduak,
                btnDokumentuak
            };

            int zabalera = _edukiPanela.ClientSize.Width;
            int altuera = _edukiPanela.ClientSize.Height;
            int kanpoMarjina = zabalera < 1100 ? 28 : 36;
            int tartea = zabalera < 1100 ? 20 : 26;
            int zutabeKopurua = zabalera < 1100 ? 2 : 3;
            int txartelZabalera = (zabalera - (kanpoMarjina * 2) - (tartea * (zutabeKopurua - 1))) / zutabeKopurua;
            txartelZabalera = Math.Max(250, txartelZabalera);
            int errenkadaKopurua = (int)Math.Ceiling(txartelak.Length / (double)zutabeKopurua);
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

        private void MedikuMenua_Load(object sender, EventArgs e)
        {

        }
    }
}
