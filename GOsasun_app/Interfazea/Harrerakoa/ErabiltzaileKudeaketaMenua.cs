using System;
using System.Drawing;
using System.Windows.Forms;
using GOsasun_app.Interfazea.Oinarriak_UI;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class ErabiltzaileKudeaketaMenua : OinarriPantaila
    {
        private string _rolIzena;

        public ErabiltzaileKudeaketaMenua(string rolIzena, Erabiltzailea erabiltzailea) : base(erabiltzailea)
        {
            _rolIzena = rolIzena;
            InitializeComponent();
            KonfiguratuInterfazea();
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

        private void KonfiguratuInterfazea()
        {
            this.Text = $"GOsasun - {_rolIzena} Kudeaketa";
            btnSortu.Testua = $"{_rolIzena.ToUpper()} SORTU";
            btnZerrendatu.Testua = $"{_rolIzena.ToUpper()}AK ZERRENDATU";
            btnSortu.Ikonoa = KargatuIkonoIrudia("plus-circle.svg");
            btnZerrendatu.Ikonoa = KargatuIkonoIrudia("list.svg");
        }

        private void KonfiguratuGertaerak()
        {
            btnSortu.Click += (s, e) => IrekiFormularioa(() => new ErabiltzaileaSortu(_rolIzena, _erabiltzailea!));
            btnZerrendatu.Click += (s, e) =>
            {
                if (_rolIzena == "Pazientea")
                {
                    IrekiFormularioa(() => new PazienteenZerrenda(_erabiltzailea!));
                }
                else if (_rolIzena == "Osasun Langilea" || _rolIzena == "Harrerako Langilea")
                {
                    IrekiFormularioa(() => new LangileenZerrenda(_rolIzena, _erabiltzailea!));
                }
                else
                {
                    MessageBox.Show("Modulu hau garatzen ari da.", "Laster...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };
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
                || btnSortu == null
                || btnZerrendatu == null
                || _edukiPanela.ClientSize.Width <= 0)
            {
                return;
            }

            MenuTxartelBotoia[] txartelak =
            {
                btnSortu,
                btnZerrendatu
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

        private void _edukiPanela_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
