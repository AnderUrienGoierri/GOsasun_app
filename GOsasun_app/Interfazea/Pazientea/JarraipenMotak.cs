using GOsasun_app.Modeloa;
using GOsasun_app.Kontrola.Zerbitzuak;
using GOsasun_app.Kontrola;
using System.Collections.Generic;
using System;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOsasun_app.Interfazea
{
    /// <summary>
    /// Medikuak hautatu dezakeen neurketa moten formularioa.
    /// (Tentsioa, Glukosa, Pisua, Altuera)
    /// </summary>
    public partial class JarraipenMotak : OinarriPantaila
    {
        private readonly ErabiltzaileKontrolatzailea _erabiltzaileKontrolatzailea = new ErabiltzaileKontrolatzailea();
        private readonly JarraipenaKontrolatzailea _jarraipenaKontrolatzailea = new JarraipenaKontrolatzailea();
        private readonly int? _pazienteIdAurrehautatu;
        private readonly string? _pazienteIzenburua;

        // -----------------------------------------------------------
        // Eraikitzailea
        // -----------------------------------------------------------
        public JarraipenMotak() : base()
        {
            _pazienteIdAurrehautatu = null;
            InitializeComponent();
            KargatuIkonoak();
        }

        public JarraipenMotak(Erabiltzailea erabiltzailea)
            : base(erabiltzailea)
        {
            _pazienteIdAurrehautatu = null;
            InitializeComponent();
            KargatuIkonoak();
            KonfiguratuGertaerak();
        }

        public JarraipenMotak(Erabiltzailea erabiltzailea, int pazienteId, string? pazienteIzenburua = null)
            : base(erabiltzailea)
        {
            _pazienteIdAurrehautatu = pazienteId;
            _pazienteIzenburua = pazienteIzenburua;
            InitializeComponent();
            KargatuIkonoak();
            KonfiguratuGertaerak();
            EguneratuIzenburua();
        }

        private void EguneratuIzenburua()
        {
            if (_pazienteIdAurrehautatu.HasValue && !string.IsNullOrWhiteSpace(_pazienteIzenburua))
            {
                Text = $"GOsasun - Jarraipen Motak - {_pazienteIzenburua}";
            }
        }

        private void KargatuIkonoak()
        {
            btnTentsiometroa.Ikonoa = KargatuTxartelIrudia("tentsiometroa.png");
            btnPisua.Ikonoa = KargatuTxartelIrudia("pisua.png");
            btnAltuera.Ikonoa = KargatuTxartelIrudia("altuera.png");
            btnOharra.Ikonoa = KargatuTxartelIrudia("abisua.png");
        }

        private void KonfiguratuGertaerak()
        {
            // Tentsiometroa: Pantaila berria ireki
            btnTentsiometroa.Click += (s, e) =>
            {
                Form formularioa = _pazienteIdAurrehautatu.HasValue
                    ? new TentsiometroNeurketak(_erabiltzailea!, _pazienteIdAurrehautatu.Value, _pazienteIzenburua)
                    : new TentsiometroNeurketak(_erabiltzailea!);
                IrekiFormularioa(formularioa);
            };

            // Pisua: eskuzko sarrera
            btnPisua.Click += (s, e) =>
            {
                Form formularioa = _pazienteIdAurrehautatu.HasValue
                    ? new EskuzkoNeurketak(_erabiltzailea!, true, _pazienteIdAurrehautatu.Value, _pazienteIzenburua)
                    : new EskuzkoNeurketak(_erabiltzailea!, true);
                IrekiFormularioa(formularioa);
            };

            // Altuera: eskuzko sarrera
            btnAltuera.Click += (s, e) =>
            {
                Form formularioa = _pazienteIdAurrehautatu.HasValue
                    ? new EskuzkoNeurketak(_erabiltzailea!, false, _pazienteIdAurrehautatu.Value, _pazienteIzenburua)
                    : new EskuzkoNeurketak(_erabiltzailea!, false);
                IrekiFormularioa(formularioa);
            };

            btnOharra.Click += (s, e) => SortuOharJarraipena();
        }

        private Image? KargatuTxartelIrudia(string fitxategiIzena)
        {
            foreach (string root in LortuBilaketaErroakTxartelentzat())
            {
                string[] aukerak =
                {
                    Path.Combine(root, "img", "icons", fitxategiIzena),
                    Path.Combine(root, "GOsasun_app", "img", "icons", fitxategiIzena),
                    Path.Combine(root, "img", "png", fitxategiIzena),
                    Path.Combine(root, "GOsasun_app", "img", "png", fitxategiIzena)
                };

                string? aurkitua = aukerak.FirstOrDefault(File.Exists);
                if (!string.IsNullOrWhiteSpace(aurkitua))
                {
                    using Image jatorrizkoa = Image.FromFile(aurkitua);
                    return new Bitmap(jatorrizkoa);
                }
            }

            return null;
        }

        private static IEnumerable<string> LortuBilaketaErroakTxartelentzat()
        {
            HashSet<string> erroak = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string?[] hasierakoak =
            {
                Application.StartupPath,
                AppContext.BaseDirectory,
                Directory.GetCurrentDirectory(),
                Environment.CurrentDirectory,
                Path.GetDirectoryName(typeof(JarraipenMotak).Assembly.Location)
            };

            foreach (string? hasiera in hasierakoak)
            {
                if (string.IsNullOrWhiteSpace(hasiera) || !Directory.Exists(hasiera)) continue;

                DirectoryInfo? karpeta = new DirectoryInfo(hasiera);
                while (karpeta != null)
                {
                    erroak.Add(karpeta.FullName);
                    karpeta = karpeta.Parent;
                }
            }

            return erroak;
        }

        private void SortuOharJarraipena()
        {
            if (_erabiltzailea == null) return;

            List<Pazientea> pazienteak = new List<Pazientea>();
            int? pazienteId = _erabiltzailea.DaPazientea() ? _erabiltzailea.Id : _pazienteIdAurrehautatu;

            if (!_erabiltzailea.DaPazientea() && !_pazienteIdAurrehautatu.HasValue)
            {
                pazienteak = _erabiltzaileKontrolatzailea.LortuGuztiakPazienteak()
                    .OrderBy(p => p.IzenOsoa)
                    .ToList();

                if (pazienteak.Count == 0)
                {
                    MessageBox.Show("Ez dago pazienterik ohar bidezko jarraipena sortzeko.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            using OharBidezkoJarraipenaLaguntzailea elkarrizketa = new OharBidezkoJarraipenaLaguntzailea();
            elkarrizketa.Hasieratu(
                "Ohar bidezko jarraipena",
                !_erabiltzailea.DaPazientea() && !_pazienteIdAurrehautatu.HasValue,
                pazienteId,
                pazienteak,
                bilaketa => string.IsNullOrWhiteSpace(bilaketa)
                    ? pazienteak.OrderBy(p => p.IzenOsoa).ToList()
                    : _erabiltzaileKontrolatzailea.LortuGuztiakPazienteak(bilaketa.Trim()).OrderBy(p => p.IzenOsoa).ToList());

            if (elkarrizketa.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            if (!elkarrizketa.HautatutakoPazienteId.HasValue)
            {
                MessageBox.Show("Paziente bat aukeratu behar da.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Jarraipena berria = new Jarraipena
            {
                PazienteId = elkarrizketa.HautatutakoPazienteId.Value,
                OsasunLangileId = _erabiltzailea.DaOsasunLangilea() ? _erabiltzailea.Id : null,
                ErregistroData = DateTime.Now,
                Oharrak = elkarrizketa.Oharra
            };

            if (_jarraipenaKontrolatzailea.GordeJarraipena(berria))
            {
                _jarraipenaKontrolatzailea.EsportatuXML(berria);
                MessageBox.Show("Ohar bidezko jarraipena ondo gorde da.", "Arrakasta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show("Errorea gertatu da ohar bidezko jarraipena gordetzean.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void IrekiFormularioa(Form formularioa)
        {
            formularioa.FormClosed += (s, e) => this.Show();
            this.Hide();
            formularioa.Show();
        }

        private void _edukiPanela_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
