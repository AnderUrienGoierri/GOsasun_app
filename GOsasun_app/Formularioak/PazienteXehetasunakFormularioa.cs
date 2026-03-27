using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using GOsasun_app.Modeloak;

namespace GOsasun_app.Formularioak
{
    /// <summary>
    /// Paziente baten informazio zehatza erakusten duen formularioa.
    /// </summary>
    public partial class PazienteXehetasunakFormularioa : OinarriFormularioa
    {
        private readonly Pazientea _pazientea;

        public PazienteXehetasunakFormularioa(Pazientea pazientea)
            : base() // Ez dugu medikuaren info goiburuan behar, edo agian bai? 
                     // OinarriFormularioa(erabiltzailea) deituko dugu medikuaren izena goiburuan mantentzeko baldin badugu.
        {
            _pazientea = pazientea;
            InitializeComponent();
            BeteDatuak();
        }

        private void BeteDatuak()
        {
            lblIzena.Text = _pazientea.IzenOsoa.ToUpper();
            lblNAN.Text = $"NAN: {_pazientea.Nan}";
            lblJaiotzeData.Text = $"Jaiotze data: {_pazientea.JaiotzeData:yyyy/MM/dd}";
            lblEmaila.Text = $"Emaila: {_pazientea.Emaila}";
            lblTelefonoa.Text = $"Telefonoa: {_pazientea.Telefonoa ?? "---"}";
            lblOdolTaldea.Text = $"Odol taldea: {_pazientea.OdolTaldea ?? "---"}";
            lblAltuera.Text = $"Altuera: {(_pazientea.AzkenAltuera.HasValue ? _pazientea.AzkenAltuera.Value.ToString("F2") + " cm" : "---")}";
            lblPisua.Text = $"Pisua: {(_pazientea.AzkenPisua.HasValue ? _pazientea.AzkenPisua.Value.ToString("F2") + " kg" : "---")}";
            lblEgoera.Text = $"EGOERA KLINIKOA: {_pazientea.EgoeraKlinikoa.ToUpper()}";

            // Kolorea aldatu egoeraren arabera
            lblEgoera.ForeColor = _pazientea.EgoeraKlinikoa.Equals("Alta", StringComparison.OrdinalIgnoreCase) 
                ? Color.FromArgb(39, 174, 96) // Berdea
                : Color.FromArgb(192, 57, 43); // Gorria

            KargatuIrudia();
        }

        private void KargatuIrudia()
        {
            // Erabiltzaileak dioen bezala, irudiak "img/png/pazienteak/pazientea_{id}.png" bidean daude
            string irudiIzena = $"pazientea_{_pazientea.Id}.png";
            string erlatiboa = Path.Combine("img", "png", "pazienteak", irudiIzena);
            
            string root = Directory.GetCurrentDirectory();
            string[] saioak = {
                Path.Combine(Application.StartupPath, erlatiboa),
                Path.Combine(root, erlatiboa),
                Path.Combine(root, "GOsasun_app", erlatiboa),
                Path.Combine(root, "..", "..", "..", erlatiboa),
                Path.Combine(root, "..", "..", "..", "GOsasun_app", erlatiboa)
            };

            string? aurkitutakoBidea = null;
            foreach (string s in saioak)
            {
                if (File.Exists(s)) { aurkitutakoBidea = s; break; }
            }

            if (aurkitutakoBidea != null)
            {
                pbIrudia.Image = Image.FromFile(aurkitutakoBidea);
            }
            else
            {
                // Fallback: irudi lehenetsia edo aurreko logika (DB-koa)
                if (!string.IsNullOrEmpty(_pazientea.Irudia))
                {
                    // ... lehendik zegoen logika ...
                }
            }
        }
    }
}
