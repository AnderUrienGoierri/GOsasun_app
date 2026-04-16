using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    /// <summary>
    /// Paziente baten fitxa mediko profesionala erakusten duen formularioa.
    /// </summary>
    public partial class PazienteXehetasunak : OinarriPantaila
    {
        private readonly Pazientea _pazientea;

        public PazienteXehetasunak(Pazientea pazientea) : base()
        {
            _pazientea = pazientea;
            InitializeComponent();
            KonfiguratuPantaila();
            BeteDatuak();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ZentratuPantailaLanEremuan();
        }

        private void KonfiguratuPantaila()
        {
            ClientSize = new Size(1500, 980);
            _goiburuBarra.Width = ClientSize.Width;
            _edukiPanela.Size = new Size(ClientSize.Width, _edukiPanela.Height);
            lblFitxaMota.Text = "PAZIENTEAREN FITXA MEDIKOA";
        }

        private void BeteDatuak()
        {
            lblIzena.Text = _pazientea.IzenOsoa;
            lblAzpiInformazioa.Text = $"NAN: {_pazientea.Nan}   |   Paziente ID: {_pazientea.Id}";
            lblEgoeraBadge.Text = NormalizatuEgoera(_pazientea.EgoeraKlinikoa).ToUpperInvariant();

            bool alta = string.Equals(_pazientea.EgoeraKlinikoa, "Alta", StringComparison.OrdinalIgnoreCase);
            lblEgoeraBadge.BackColor = alta ? Color.FromArgb(223, 245, 232) : Color.FromArgb(252, 231, 230);
            lblEgoeraBadge.ForeColor = alta ? Color.FromArgb(32, 102, 70) : Color.FromArgb(151, 44, 39);

            lblNanBalioa.Text = _pazientea.Nan;
            lblJaiotzeDataBalioa.Text = FormateatuData(_pazientea.JaiotzeData);
            lblAdinaBalioa.Text = KalkulatuAdina(_pazientea.JaiotzeData);
            lblSexuaBalioa.Text = FormateatuTestua(_pazientea.Sexua);
            lblEmailaBalioa.Text = FormateatuTestua(_pazientea.Emaila);
            lblTelefonoaBalioa.Text = FormateatuTestua(_pazientea.Telefonoa);
            lblHelbideaBalioa.Text = FormateatuTestua(_pazientea.Helbidea);
            lblHerriaBalioa.Text = FormateatuKokalekua();
            lblOdolTaldeaBalioa.Text = FormateatuTestua(_pazientea.OdolTaldea);
            lblAltueraBalioa.Text = _pazientea.AzkenAltuera.HasValue ? $"{_pazientea.AzkenAltuera.Value:F2} cm" : "---";
            lblPisuaBalioa.Text = _pazientea.AzkenPisua.HasValue ? $"{_pazientea.AzkenPisua.Value:F2} kg" : "---";
            lblEgoeraBalioa.Text = NormalizatuEgoera(_pazientea.EgoeraKlinikoa);

            KargatuIrudia();
        }

        private string FormateatuKokalekua()
        {
            string herria = string.IsNullOrWhiteSpace(_pazientea.Herria) ? string.Empty : _pazientea.Herria!.Trim();
            string postaKodea = string.IsNullOrWhiteSpace(_pazientea.PostaKodea) ? string.Empty : _pazientea.PostaKodea!.Trim();

            if (string.IsNullOrEmpty(herria) && string.IsNullOrEmpty(postaKodea))
            {
                return "---";
            }

            if (string.IsNullOrEmpty(herria))
            {
                return postaKodea;
            }

            if (string.IsNullOrEmpty(postaKodea))
            {
                return herria;
            }

            return $"{postaKodea} {herria}";
        }

        private static string FormateatuData(DateTime data)
        {
            return data == DateTime.MinValue ? "---" : data.ToString("yyyy/MM/dd");
        }

        private static string KalkulatuAdina(DateTime jaiotzeData)
        {
            if (jaiotzeData == DateTime.MinValue)
            {
                return "---";
            }

            DateTime gaur = DateTime.Today;
            int adina = gaur.Year - jaiotzeData.Year;
            if (jaiotzeData.Date > gaur.AddYears(-adina))
            {
                adina--;
            }

            return $"{adina} urte";
        }

        private static string FormateatuTestua(string? testua)
        {
            return string.IsNullOrWhiteSpace(testua) ? "---" : testua.Trim();
        }

        private static string NormalizatuEgoera(string? egoera)
        {
            return string.IsNullOrWhiteSpace(egoera) ? "Alta" : egoera.Trim();
        }

        private void KargatuIrudia()
        {
            string irudiIzena = $"pazientea_{_pazientea.Id}.png";
            string erlatiboa = Path.Combine("img", "png", "pazienteak", irudiIzena);

            string root = Directory.GetCurrentDirectory();
            string[] bideak =
            {
                Path.Combine(Application.StartupPath, erlatiboa),
                Path.Combine(root, erlatiboa),
                Path.Combine(root, "GOsasun_app", erlatiboa),
                Path.Combine(root, "..", "..", "..", erlatiboa),
                Path.Combine(root, "..", "..", "..", "GOsasun_app", erlatiboa)
            };

            foreach (string bidea in bideak)
            {
                if (!File.Exists(bidea))
                {
                    continue;
                }

                using Image irudia = Image.FromFile(bidea);
                pbIrudia.Image = new Bitmap(irudia);
                return;
            }

            pbIrudia.Image = SortuPlaceholderIrudia();
        }

        private static Bitmap SortuPlaceholderIrudia()
        {
            Bitmap bitmap = new Bitmap(260, 320);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.Clear(Color.FromArgb(241, 246, 250));

            using SolidBrush ringBrush = new SolidBrush(Color.FromArgb(214, 226, 236));
            using SolidBrush iconBrush = new SolidBrush(Color.FromArgb(120, 145, 168));
            using Pen crossPen = new Pen(Color.FromArgb(102, 167, 206), 8f);

            graphics.FillEllipse(ringBrush, 55, 28, 150, 150);
            graphics.FillEllipse(iconBrush, 94, 56, 72, 72);
            graphics.FillPie(iconBrush, 76, 104, 108, 88, 200, 140);
            graphics.DrawLine(crossPen, 192, 224, 228, 224);
            graphics.DrawLine(crossPen, 210, 206, 210, 242);

            return bitmap;
        }
    }
}
