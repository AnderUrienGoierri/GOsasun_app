using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using GOsasun_app.Interfazea.Kontrolak;
using GOsasun_app.Kontrola;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    /// <summary>
    /// Paziente baten fitxa mediko profesionala erakusten duen formularioa.
    /// </summary>
    public partial class PazienteXehetasunak : OinarriPantaila
    {
        private readonly Pazientea _pazientea;
        private readonly PazienteKontrolatzailea _pazienteKontrolatzailea = new PazienteKontrolatzailea();

        [EditorBrowsable(EditorBrowsableState.Never)]
        public PazienteXehetasunak() : this(SortuDiseinukoPazientea())
        {
        }

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
            KonfiguratuGoiburukoEkintzak();
            KonfiguratuTxartelenEdukia();
        }

        private void KonfiguratuGoiburukoEkintzak()
        {
            btnEgoeraMedikoaAldatu.Click -= BtnEgoeraMedikoaAldatu_Click;
            btnEgoeraMedikoaAldatu.Click += BtnEgoeraMedikoaAldatu_Click;
        }

        private void KonfiguratuTxartelenEdukia()
        {
            KonfiguratuArgazkiaAtala();
            KonfiguratuIdentifikazioAtala();
            KonfiguratuHarremanAtala();
            KonfiguratuKlinikoAtala();
        }

        private void KonfiguratuArgazkiaAtala()
        {
            lblArgazkiAzalpena.Text = "Argazkia edo identifikazio bisuala. Daturik ez badago, irudi lehenetsi klinikoa erakusten da.";
            lblArgazkiAzalpena.AutoSize = false;
            lblArgazkiAzalpena.Size = new Size(242, 104);
        }

        private void KonfiguratuIdentifikazioAtala()
        {
            KonfiguratuSectionTitle(lblIdentifikazioa, "IDENTIFIKAZIOA", 28);

            KonfiguratuField(lblNanTitulua, "NAN / DNI", 28, 86);
            KonfiguratuValue(lblNanBalioa, "---", 28, 118, 230);

            KonfiguratuField(lblJaiotzeDataTitulua, "JAIOTZE DATA", 318, 86);
            KonfiguratuValue(lblJaiotzeDataBalioa, "---", 318, 118, 230);

            KonfiguratuField(lblAdinaTitulua, "ADINA", 28, 202);
            KonfiguratuValue(lblAdinaBalioa, "---", 28, 234, 230);

            KonfiguratuField(lblSexuaTitulua, "SEXUA", 318, 202);
            KonfiguratuValue(lblSexuaBalioa, "---", 318, 234, 230);
        }

        private void KonfiguratuHarremanAtala()
        {
            KonfiguratuSectionTitle(lblHarremana, "HARREMANETARAKO DATUAK", 28);

            KonfiguratuField(lblEmailaTitulua, "EMAILA", 28, 86);
            lblEmailaTitulua.Size = new Size(360, 28);
            KonfiguratuValue(lblEmailaBalioa, "---", 28, 118, 500);
            lblEmailaBalioa.Height = 52;

            KonfiguratuField(lblTelefonoaTitulua, "TELEFONOA", 548, 86);
            lblTelefonoaTitulua.Size = new Size(220, 28);
            KonfiguratuValue(lblTelefonoaBalioa, "---", 548, 118, 240);
            lblTelefonoaBalioa.Height = 52;

            KonfiguratuField(lblHelbideaTitulua, "HELBIDEA", 28, 206);
            lblHelbideaTitulua.Size = new Size(360, 28);
            KonfiguratuValue(lblHelbideaBalioa, "---", 28, 238, 500);
            lblHelbideaBalioa.Height = 70;

            KonfiguratuField(lblHerriaTitulua, "P.K. / UDALERRIA", 548, 206);
            lblHerriaTitulua.Size = new Size(300, 28);
            KonfiguratuValue(lblHerriaBalioa, "---", 548, 238, 320);
            lblHerriaBalioa.Height = 108;
        }

        private void KonfiguratuKlinikoAtala()
        {
            KonfiguratuSectionTitle(lblKlinikoa, "LABURPEN KLINIKOA", 32);

            KonfiguratuField(lblOdolTaldeaTitulua, "ODOL TALDEA", 32, 104);
            KonfiguratuValue(lblOdolTaldeaBalioa, "---", 32, 138, 340);
            lblOdolTaldeaBalioa.Height = 52;

            KonfiguratuField(lblAltueraTitulua, "AZKEN ALTUERA", 430, 104);
            KonfiguratuValue(lblAltueraBalioa, "---", 430, 138, 340);
            lblAltueraBalioa.Height = 52;

            KonfiguratuField(lblPisuaTitulua, "AZKEN PISUA", 32, 286);
            KonfiguratuValue(lblPisuaBalioa, "---", 32, 320, 360);
            lblPisuaBalioa.Height = 52;

            KonfiguratuField(lblEgoeraTitulua, "EGOERA KLINIKOA", 430, 286);
            KonfiguratuValue(lblEgoeraBalioa, "---", 430, 320, 500);
            lblEgoeraBalioa.Height = 70;
        }

        private void BeteDatuak()
        {
            lblIzena.Text = _pazientea.IzenOsoa;
            lblAzpiInformazioa.Text = $"NAN: {_pazientea.Nan}   |   Paziente ID: {_pazientea.Id}";
            EguneratuEgoeraIkuspegia();

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

            KargatuIrudia();
        }

        private static Pazientea SortuDiseinukoPazientea()
        {
            return new Pazientea
            {
                Id = 1,
                Izena = "Ane",
                Abizenak = "Etxeberria",
                Nan = "12345678A",
                JaiotzeData = new DateTime(1989, 4, 12),
                Sexua = "Emakumea",
                Emaila = "ane.etxeberria@paziente.eus",
                Telefonoa = "688112233",
                Helbidea = "Kale Nagusia 12",
                Herria = "Donostia",
                PostaKodea = "20004",
                OdolTaldea = "A+",
                AzkenAltuera = 168.4m,
                AzkenPisua = 63.2m,
                EgoeraKlinikoa = "Alta"
            };
        }

        private void EguneratuEgoeraIkuspegia()
        {
            string egoera = NormalizatuEgoera(_pazientea.EgoeraKlinikoa);
            bool alta = string.Equals(egoera, "Alta", StringComparison.OrdinalIgnoreCase);

            lblEgoeraBadge.Text = egoera.ToUpperInvariant();
            lblEgoeraBadge.BackColor = alta ? Color.FromArgb(223, 245, 232) : Color.FromArgb(252, 231, 230);
            lblEgoeraBadge.ForeColor = alta ? Color.FromArgb(32, 102, 70) : Color.FromArgb(151, 44, 39);
            lblEgoeraBalioa.Text = egoera;
        }

        private void BtnEgoeraMedikoaAldatu_Click(object? sender, EventArgs e)
        {
            string unekoEgoera = NormalizatuEgoera(_pazientea.EgoeraKlinikoa);
            string egoeraBerria = string.Equals(unekoEgoera, "Alta", StringComparison.OrdinalIgnoreCase) ? "Baja" : "Alta";
            string baieztapenTestua = egoeraBerria == "Alta" ? "altan" : "bajan";

            DialogResult erantzuna = MessageBox.Show(
                $"Pazientea {baieztapenTestua} jarri nahi duzu?",
                "Egoera medikoa aldatu",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (erantzuna != DialogResult.Yes)
            {
                return;
            }

            bool ondoGordeDa = _pazienteKontrolatzailea.AldatuPazientearenEgoera(_pazientea.Id, egoeraBerria);
            if (!ondoGordeDa)
            {
                MessageBox.Show(
                    "Ez da posible izan pazientearen egoera medikoa eguneratzea.",
                    "Errorea",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            _pazientea.EgoeraKlinikoa = egoeraBerria;
            EguneratuEgoeraIkuspegia();

            MessageBox.Show(
                $"Pazientea {baieztapenTestua} geratu da.",
                "Egoera eguneratuta",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
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

            return $"{postaKodea}{Environment.NewLine}{herria}";
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

                Image? aurrekoa = pbIrudia.Image;
                pbIrudia.Image = IrudiCachea.LortuBitmapa(bidea);
                aurrekoa?.Dispose();
                return;
            }

            pbIrudia.Image?.Dispose();
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

        private void pnlKlinikoa_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
