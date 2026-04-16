using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using GOsasun_app.Kontrola;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class Grafikak : OinarriPantaila
    {
        private readonly JarraipenaKontrolatzailea _jarraipenaKontrolatzailea;
        private readonly ErabiltzaileKontrolatzailea _erabiltzaileKontrolatzailea;
        private readonly List<Pazientea> _pazienteGuztiak = new List<Pazientea>();
        private readonly List<GrafikoSeriea> _unekoSerieak = new List<GrafikoSeriea>();
        private bool _dataTarteaEguneratzen;
        private string _grafikoIzenburua = "Osasun datuen bilakaera";
        private string _ardatzYIzenburua = "Balioa";

        private enum GrafikaMota
        {
            Pisua,
            Altuera,
            Pultsua,
            Presioa
        }

        private sealed class GrafikaMotaItem
        {
            public GrafikaMota Balioa { get; init; }
            public string Testua { get; init; } = string.Empty;
            public override string ToString() => Testua;
        }

        private sealed class GrafikoSeriea
        {
            public string Izena { get; init; } = string.Empty;
            public Color Kolorea { get; init; }
            public bool Etena { get; init; }
            public List<(DateTime Data, double Balioa)> Puntuak { get; init; } = new List<(DateTime Data, double Balioa)>();
        }

        public Grafikak() : base()
        {
            _jarraipenaKontrolatzailea = new JarraipenaKontrolatzailea();
            _erabiltzaileKontrolatzailea = new ErabiltzaileKontrolatzailea();
            InitializeComponent();
            KonfiguratuPantaila();
        }

        public Grafikak(Erabiltzailea u) : base(u)
        {
            _jarraipenaKontrolatzailea = new JarraipenaKontrolatzailea();
            _erabiltzaileKontrolatzailea = new ErabiltzaileKontrolatzailea();
            InitializeComponent();
            KonfiguratuPantaila();
        }

        private void KonfiguratuPantaila()
        {
            ClientSize = new Size(1902, 1394);
            _goiburuBarra.Width = ClientSize.Width;
            _edukiPanela.Size = new Size(ClientSize.Width, _edukiPanela.Height);

            KonfiguratuGrafikoa();
            KonfiguratuGrafikaMotenZerrenda();
            KonfiguratuGertaerak();
            KargatuPazienteak();
            KargatuHasierakoBalioak();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ClientSize = new Size(1902, 1394);
            _goiburuBarra.Width = ClientSize.Width;
            _edukiPanela.Size = new Size(ClientSize.Width, _edukiPanela.Height);
            ZentratuPantailaLanEremuan();
        }

        private void KonfiguratuGrafikoa()
        {
            pnlGrafikoa.Paint += PnlGrafikoa_Paint;
            pnlGrafikoa.Resize += (_, _) => pnlGrafikoa.Invalidate();
        }

        private void KonfiguratuGrafikaMotenZerrenda()
        {
            cmbGrafikaMota.DisplayMember = nameof(GrafikaMotaItem.Testua);
            cmbGrafikaMota.ValueMember = nameof(GrafikaMotaItem.Balioa);
            cmbGrafikaMota.DataSource = new List<GrafikaMotaItem>
            {
                new GrafikaMotaItem { Balioa = GrafikaMota.Pisua, Testua = "Pisua (kg)" },
                new GrafikaMotaItem { Balioa = GrafikaMota.Altuera, Testua = "Altuera (m)" },
                new GrafikaMotaItem { Balioa = GrafikaMota.Pultsua, Testua = "Pultsua (ppm)" },
                new GrafikaMotaItem { Balioa = GrafikaMota.Presioa, Testua = "Presio Sist./Diast. (mmHg)" }
            };
            cmbGrafikaMota.SelectedIndex = 0;
        }

        private void KonfiguratuGertaerak()
        {
            txtPazienteBilatu.TextChanged += (_, _) => IragaziPazienteak();
            cmbPazienteak.SelectedIndexChanged += (_, _) => PazienteAukeraAldatuDa();
            btnGrafikoaErakutsi.Click += (_, _) => MarraztuAukeratutakoGrafikoa();
            dtpHasiera.ValueChanged += (_, _) => BalidatuDataTartea();
            dtpAmaiera.ValueChanged += (_, _) => BalidatuDataTartea();
            chkPazienteGuztiak.CheckedChanged += (_, _) => KargatuPazienteak();
        }

        private void KargatuPazienteak()
        {
            int? hautatutakoPazienteId = cmbPazienteak.SelectedItem is Pazientea hautatutakoPazientea
                ? hautatutakoPazientea.Id
                : null;

            _pazienteGuztiak.Clear();

            if (_erabiltzailea?.DaPazientea() == true)
            {
                Pazientea? pazientea = _erabiltzaileKontrolatzailea.LortuPazientea(_erabiltzailea.Id);
                if (pazientea != null)
                {
                    _pazienteGuztiak.Add(pazientea);
                }
            }
            else if (_erabiltzailea?.DaOsasunLangilea() == true)
            {
                IEnumerable<Pazientea> pazienteak = chkPazienteGuztiak.Checked
                    ? _erabiltzaileKontrolatzailea.LortuGuztiakPazienteak()
                    : _erabiltzaileKontrolatzailea.LortuLangilearenPazienteak(_erabiltzailea.Id);

                _pazienteGuztiak.AddRange(pazienteak
                    .OrderBy(p => p.Abizenak)
                    .ThenBy(p => p.Izena));
            }
            else
            {
                _pazienteGuztiak.AddRange(_erabiltzaileKontrolatzailea
                    .LortuGuztiakPazienteak()
                    .OrderBy(p => p.Abizenak)
                    .ThenBy(p => p.Izena));
            }

            bool pazienteBakarrik = _erabiltzailea?.DaPazientea() == true;
            txtPazienteBilatu.Visible = !pazienteBakarrik;
            lblPazienteBilatu.Visible = !pazienteBakarrik;
            cmbPazienteak.Enabled = !pazienteBakarrik;
            chkPazienteGuztiak.Visible = _erabiltzailea?.DaOsasunLangilea() == true;
            chkPazienteGuztiak.Enabled = _erabiltzailea?.DaOsasunLangilea() == true;

            IragaziPazienteak(hautatutakoPazienteId);
        }

        private void KargatuHasierakoBalioak()
        {
            _dataTarteaEguneratzen = true;
            try
            {
                DateTime gaur = DateTime.Today;
                DateTime amaiera = MugatuDataTartean(dtpAmaiera, gaur);
                DateTime hasiera = MugatuDataTartean(dtpHasiera, amaiera.AddMonths(-6));

                if (hasiera > amaiera)
                {
                    hasiera = dtpHasiera.MinDate <= amaiera ? amaiera : dtpHasiera.MinDate;
                }

                dtpAmaiera.Value = amaiera;
                dtpHasiera.Value = hasiera;
            }
            finally
            {
                _dataTarteaEguneratzen = false;
            }

            lblEgoera.Text = "Paziente bat eta grafika mota aukeratu, gero sakatu 'Grafikoa erakutsi'.";
            lblAzalpena.Text = "Grafikoak aukeratutako datu-tarteko neurketak eta joera lineala erakusten ditu.";
        }

        private static DateTime MugatuDataTartean(DateTimePicker kontrola, DateTime data)
        {
            DateTime balioa = data.Date;

            if (balioa < kontrola.MinDate.Date)
            {
                return kontrola.MinDate.Date;
            }

            if (balioa > kontrola.MaxDate.Date)
            {
                return kontrola.MaxDate.Date;
            }

            return balioa;
        }

        private void IragaziPazienteak(int? hautatutakoPazienteId = null)
        {
            string bilaketa = txtPazienteBilatu.Text.Trim();
            List<Pazientea> filtratuak = _pazienteGuztiak
                .Where(pazientea => string.IsNullOrWhiteSpace(bilaketa)
                    || pazientea.Izena.Contains(bilaketa, StringComparison.OrdinalIgnoreCase)
                    || pazientea.Abizenak.Contains(bilaketa, StringComparison.OrdinalIgnoreCase)
                    || pazientea.Nan.Contains(bilaketa, StringComparison.OrdinalIgnoreCase))
                .OrderBy(pazientea => pazientea.Abizenak)
                .ThenBy(pazientea => pazientea.Izena)
                .ToList();

            cmbPazienteak.DataSource = null;
            cmbPazienteak.DisplayMember = nameof(Pazientea.IzenOsoa);
            cmbPazienteak.ValueMember = nameof(Pazientea.Id);
            cmbPazienteak.DataSource = filtratuak;

            if (filtratuak.Count == 0)
            {
                lblEgoera.Text = "Ez da pazienterik aurkitu emandako bilaketarekin.";
                lblPazienteDatuak.Text = "Pazientea: -";
                GarbituGrafikoa();
                return;
            }

            int indizea = hautatutakoPazienteId.HasValue
                ? filtratuak.FindIndex(pazientea => pazientea.Id == hautatutakoPazienteId.Value)
                : -1;

            cmbPazienteak.SelectedIndex = indizea >= 0 ? indizea : 0;
        }

        private void PazienteAukeraAldatuDa()
        {
            if (cmbPazienteak.SelectedItem is not Pazientea pazientea)
            {
                return;
            }

            EguneratuPazienteInfoa(pazientea);
            EguneratuDataTartea(pazientea.Id);
        }

        private void EguneratuPazienteInfoa(Pazientea pazientea)
        {
            lblPazienteDatuak.Text = $"Pazientea: {pazientea.IzenOsoa} | NAN: {pazientea.Nan}";
        }

        private void EguneratuDataTartea(int pazienteId)
        {
            List<Jarraipena> jarraipenak = _jarraipenaKontrolatzailea
                .LortuJarraipenGuztiak(pazienteId: pazienteId)
                .OrderBy(j => j.ErregistroData)
                .ToList();

            if (jarraipenak.Count == 0)
            {
                return;
            }

            _dataTarteaEguneratzen = true;
            try
            {
                DateTime lehenengoa = jarraipenak.First().ErregistroData.Date;
                DateTime azkena = jarraipenak.Last().ErregistroData.Date;
                dtpHasiera.MinDate = lehenengoa;
                dtpAmaiera.MinDate = lehenengoa;
                dtpHasiera.MaxDate = azkena;
                dtpAmaiera.MaxDate = azkena;
                dtpHasiera.Value = lehenengoa;
                dtpAmaiera.Value = azkena;
            }
            finally
            {
                _dataTarteaEguneratzen = false;
            }
        }

        private void BalidatuDataTartea()
        {
            if (_dataTarteaEguneratzen)
            {
                return;
            }

            if (dtpHasiera.Value.Date > dtpAmaiera.Value.Date)
            {
                dtpAmaiera.Value = dtpHasiera.Value.Date;
            }
        }

        private void MarraztuAukeratutakoGrafikoa()
        {
            if (cmbPazienteak.SelectedItem is not Pazientea pazientea)
            {
                lblEgoera.Text = "Aukeratu paziente bat grafikoa erakutsi aurretik.";
                return;
            }

            if (cmbGrafikaMota.SelectedItem is not GrafikaMotaItem grafikaMotaItem)
            {
                lblEgoera.Text = "Aukeratu grafika mota bat.";
                return;
            }

            List<Jarraipena> jarraipenak = _jarraipenaKontrolatzailea
                .LortuJarraipenGuztiak(
                    hasieraData: dtpHasiera.Value.Date,
                    amaieraData: dtpAmaiera.Value.Date,
                    pazienteId: pazientea.Id)
                .OrderBy(j => j.ErregistroData)
                .ToList();

            GarbituGrafikoa();

            if (jarraipenak.Count == 0)
            {
                lblEgoera.Text = "Ez dago neurketarik aukeratutako data tartean.";
                lblAzalpena.Text = "Saiatu data tartea zabaltzen edo beste paziente bat aukeratzen.";
                return;
            }

            switch (grafikaMotaItem.Balioa)
            {
                case GrafikaMota.Pisua:
                    MarraztuSerieBakarra(jarraipenak, "Pisua (kg)", "Pisua (kg)", Color.FromArgb(52, 152, 219), j => j.PisuaKg.HasValue ? (double?)j.PisuaKg.Value : null);
                    break;
                case GrafikaMota.Altuera:
                    MarraztuSerieBakarra(jarraipenak, "Altuera (m)", "Altuera (m)", Color.FromArgb(39, 174, 96), j => j.Altuera.HasValue ? (double?)j.Altuera.Value : null);
                    break;
                case GrafikaMota.Pultsua:
                    MarraztuSerieBakarra(jarraipenak, "Pultsua (ppm)", "Pultsua (ppm)", Color.FromArgb(231, 76, 60), j => j.PultsuaPpm.HasValue ? (double?)j.PultsuaPpm.Value : null);
                    break;
                case GrafikaMota.Presioa:
                    MarraztuPresioGrafikoa(jarraipenak);
                    break;
            }

            _grafikoIzenburua = $"{grafikaMotaItem.Testua} - {pazientea.IzenOsoa}";
            pnlGrafikoa.Invalidate();
        }

        private void MarraztuSerieBakarra(IReadOnlyList<Jarraipena> jarraipenak, string serieIzena, string ardatzIzena, Color kolorea, Func<Jarraipena, double?> balioHautatzailea)
        {
            List<(DateTime Data, double Balioa)> puntuak = jarraipenak
                .Select(jarraipena => (jarraipena.ErregistroData, balioHautatzailea(jarraipena)))
                .Where(item => item.Item2.HasValue)
                .Select(item => (item.ErregistroData, item.Item2!.Value))
                .ToList();

            if (puntuak.Count == 0)
            {
                lblEgoera.Text = "Ez dago aukeratutako grafika motarako baliorik data tarte honetan.";
                lblAzalpena.Text = "Beste grafika mota bat edo data tarte zabalagoa aukeratu.";
                return;
            }

            _unekoSerieak.Add(SortuDatuSeriea(serieIzena, kolorea, puntuak));
            GehituErregresioSeriea($"{serieIzena} joera", puntuak, kolorea);

            _ardatzYIzenburua = ardatzIzena;
            lblEgoera.Text = $"{puntuak.Count} neurketa erakusten ari dira aukeratutako tartean.";
            lblAzalpena.Text = "Marra etenak erregresio linealaren joera adierazten du.";
        }

        private void MarraztuPresioGrafikoa(IReadOnlyList<Jarraipena> jarraipenak)
        {
            List<(DateTime Data, double Balioa)> sistolikoa = jarraipenak
                .Where(j => j.TentsioSistolikoa.HasValue)
                .Select(j => (j.ErregistroData, (double)j.TentsioSistolikoa!.Value))
                .ToList();

            List<(DateTime Data, double Balioa)> diastolikoa = jarraipenak
                .Where(j => j.TentsioDiastolikoa.HasValue)
                .Select(j => (j.ErregistroData, (double)j.TentsioDiastolikoa!.Value))
                .ToList();

            if (sistolikoa.Count == 0 && diastolikoa.Count == 0)
            {
                lblEgoera.Text = "Ez dago presio neurketarik aukeratutako data tartean.";
                lblAzalpena.Text = "Saiatu beste data tarte edo beste paziente batekin.";
                return;
            }

            if (sistolikoa.Count > 0)
            {
                _unekoSerieak.Add(SortuDatuSeriea("Sistolikoa", Color.FromArgb(52, 152, 219), sistolikoa));
                GehituErregresioSeriea("Sistoliko joera", sistolikoa, Color.FromArgb(52, 152, 219));
            }

            if (diastolikoa.Count > 0)
            {
                _unekoSerieak.Add(SortuDatuSeriea("Diastolikoa", Color.FromArgb(231, 76, 60), diastolikoa));
                GehituErregresioSeriea("Diastoliko joera", diastolikoa, Color.FromArgb(231, 76, 60));
            }

            _ardatzYIzenburua = "Presioa (mmHg)";
            lblEgoera.Text = $"{Math.Max(sistolikoa.Count, diastolikoa.Count)} neurketa erakusten ari dira aukeratutako tartean.";
            lblAzalpena.Text = "Lerro sendoek sistolikoa eta diastolikoa erakusten dituzte; lerro etenek joera lineala.";
        }

        private static GrafikoSeriea SortuDatuSeriea(string izena, Color kolorea, IReadOnlyList<(DateTime Data, double Balioa)> puntuak, bool etena = false)
        {
            return new GrafikoSeriea
            {
                Izena = izena,
                Kolorea = kolorea,
                Etena = etena,
                Puntuak = puntuak.ToList()
            };
        }

        private void GehituErregresioSeriea(string izena, IReadOnlyList<(DateTime Data, double Balioa)> puntuak, Color kolorea)
        {
            if (puntuak.Count < 2)
            {
                return;
            }

            (double malda, double ebakidura) = KalkulatuErregresioLineala(puntuak);
            double lehenX = puntuak.First().Data.ToOADate();
            double azkenX = puntuak.Last().Data.ToOADate();

            _unekoSerieak.Add(SortuDatuSeriea(
                izena,
                Color.FromArgb(180, kolorea),
                new[]
                {
                    (DateTime.FromOADate(lehenX), malda * lehenX + ebakidura),
                    (DateTime.FromOADate(azkenX), malda * azkenX + ebakidura)
                },
                true));
        }

        private static (double Malda, double Ebakidura) KalkulatuErregresioLineala(IReadOnlyList<(DateTime Data, double Balioa)> puntuak)
        {
            double batezbestekoX = puntuak.Average(p => p.Data.ToOADate());
            double batezbestekoY = puntuak.Average(p => p.Balioa);

            double izendatzailea = puntuak.Sum(p => Math.Pow(p.Data.ToOADate() - batezbestekoX, 2));
            if (Math.Abs(izendatzailea) < double.Epsilon)
            {
                return (0d, batezbestekoY);
            }

            double zenbakitzailea = puntuak.Sum(p => (p.Data.ToOADate() - batezbestekoX) * (p.Balioa - batezbestekoY));
            double malda = zenbakitzailea / izendatzailea;
            double ebakidura = batezbestekoY - (malda * batezbestekoX);
            return (malda, ebakidura);
        }

        private void GarbituGrafikoa()
        {
            _unekoSerieak.Clear();
            _grafikoIzenburua = "Osasun datuen bilakaera";
            _ardatzYIzenburua = "Balioa";
            pnlGrafikoa.Invalidate();
        }

        private void PnlGrafikoa_Paint(object? sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.Clear(Color.FromArgb(248, 251, 253));

            Rectangle area = pnlGrafikoa.ClientRectangle;
            if (area.Width <= 0 || area.Height <= 0)
            {
                return;
            }

            Color cardBorder = Color.FromArgb(214, 223, 232);
            Color panelFill = Color.White;
            Color plotFill = Color.FromArgb(251, 253, 255);
            Color textColor = Color.FromArgb(33, 52, 72);
            Color mutedTextColor = Color.FromArgb(94, 109, 126);

            Rectangle cardArea = new Rectangle(12, 12, area.Width - 24, area.Height - 24);
            using SolidBrush cardBrush = new SolidBrush(panelFill);
            using Pen cardPen = new Pen(cardBorder, 1.2f);
            graphics.FillRectangle(cardBrush, cardArea);
            graphics.DrawRectangle(cardPen, cardArea);

            using Font titleFont = new Font("Segoe UI", 15F, FontStyle.Bold);
            using Font subtitleFont = new Font("Segoe UI", 10F, FontStyle.Regular);
            using Font axisFont = new Font("Segoe UI", 9.3F, FontStyle.Bold);
            using Font labelFont = new Font("Segoe UI", 8.4F, FontStyle.Regular);
            using Font legendTitleFont = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            using Brush textBrush = new SolidBrush(textColor);
            using Brush mutedTextBrush = new SolidBrush(mutedTextColor);
            using Pen axisPen = new Pen(Color.FromArgb(132, 146, 166), 1.2f);
            using Pen gridPen = new Pen(Color.FromArgb(230, 236, 242), 1f);
            using Pen dividerPen = new Pen(Color.FromArgb(226, 232, 238), 1f);

            graphics.DrawString(_grafikoIzenburua, titleFont, textBrush, new PointF(cardArea.Left + 22, cardArea.Top + 18));
            graphics.DrawString("Eboluzio klinikoa eta joera lineala", subtitleFont, mutedTextBrush, new PointF(cardArea.Left + 22, cardArea.Top + 58));
            graphics.DrawLine(dividerPen, cardArea.Left + 18, cardArea.Top + 98, cardArea.Right - 18, cardArea.Top + 98);

            if (_unekoSerieak.Count == 0 || _unekoSerieak.All(seriea => seriea.Puntuak.Count == 0))
            {
                using Font emptyFont = new Font("Segoe UI", 12F, FontStyle.Bold);
                SizeF emptySize = graphics.MeasureString("Ez dago erakusteko daturik", emptyFont);
                graphics.DrawString("Ez dago erakusteko daturik", emptyFont, Brushes.Gray, (area.Width - emptySize.Width) / 2f, (area.Height - emptySize.Height) / 2f);
                return;
            }

            Rectangle legendArea = new Rectangle(cardArea.Right - 430, cardArea.Top + 118, 388, cardArea.Height - 158);
            Rectangle plotArea = new Rectangle(cardArea.Left + 86, cardArea.Top + 136, Math.Max(150, legendArea.Left - (cardArea.Left + 118)), Math.Max(200, cardArea.Height - 262));

            using SolidBrush plotBrush = new SolidBrush(plotFill);
            graphics.FillRectangle(plotBrush, plotArea);
            graphics.DrawRectangle(cardPen, plotArea);

            using SolidBrush legendBrush = new SolidBrush(Color.FromArgb(249, 251, 252));
            graphics.FillRectangle(legendBrush, legendArea);
            graphics.DrawRectangle(cardPen, legendArea);
            TextRenderer.DrawText(
                graphics,
                "Legenda klinikoa",
                legendTitleFont,
                new Rectangle(legendArea.Left + 18, legendArea.Top + 16, legendArea.Width - 36, 38),
                textColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.PreserveGraphicsClipping);
            graphics.DrawLine(dividerPen, legendArea.Left + 14, legendArea.Top + 58, legendArea.Right - 14, legendArea.Top + 58);

            List<(DateTime Data, double Balioa)> puntuGuztiak = _unekoSerieak.SelectMany(seriea => seriea.Puntuak).ToList();
            double minX = puntuGuztiak.Min(p => p.Data.ToOADate());
            double maxX = puntuGuztiak.Max(p => p.Data.ToOADate());
            if (Math.Abs(maxX - minX) < double.Epsilon)
            {
                maxX = minX + 1d;
            }

            double minY = puntuGuztiak.Min(p => p.Balioa);
            double maxY = puntuGuztiak.Max(p => p.Balioa);
            if (Math.Abs(maxY - minY) < double.Epsilon)
            {
                maxY = minY + 1d;
            }

            double yPadding = (maxY - minY) * 0.1d;
            minY -= yPadding;
            maxY += yPadding;

            int yTickCount = 5;
            for (int i = 0; i <= yTickCount; i++)
            {
                float y = plotArea.Top + (plotArea.Height * i / (float)yTickCount);
                graphics.DrawLine(gridPen, plotArea.Left, y, plotArea.Right, y);
                double balioa = maxY - ((maxY - minY) * i / yTickCount);
                string etiketa = balioa.ToString("N1");
                SizeF labelSize = graphics.MeasureString(etiketa, labelFont);
                graphics.DrawString(etiketa, labelFont, mutedTextBrush, plotArea.Left - labelSize.Width - 12, y - (labelSize.Height / 2f));
            }

            int xTicks = Math.Min(5, Math.Max(2, puntuGuztiak.Count));
            for (int i = 0; i < xTicks; i++)
            {
                double tickRatio = xTicks == 1 ? 0d : i / (double)(xTicks - 1);
                float x = plotArea.Left + (float)(plotArea.Width * tickRatio);
                graphics.DrawLine(gridPen, x, plotArea.Top, x, plotArea.Bottom);
                DateTime tickDate = DateTime.FromOADate(minX + ((maxX - minX) * tickRatio));
                string etiketa = tickDate.ToString("dd/MM/yy");
                SizeF labelSize = graphics.MeasureString(etiketa, labelFont);

                GraphicsState state = graphics.Save();
                graphics.TranslateTransform(x - (labelSize.Width / 2f), plotArea.Bottom + 34);
                graphics.RotateTransform(-28f);
                graphics.DrawString(etiketa, labelFont, mutedTextBrush, 0, 0);
                graphics.Restore(state);
            }

            graphics.DrawLine(axisPen, plotArea.Left, plotArea.Bottom, plotArea.Right, plotArea.Bottom);
            graphics.DrawLine(axisPen, plotArea.Left, plotArea.Top, plotArea.Left, plotArea.Bottom);

            foreach (GrafikoSeriea seriea in _unekoSerieak.Where(s => s.Puntuak.Count > 0))
            {
                using Pen seriePen = new Pen(seriea.Kolorea, seriea.Etena ? 2f : 3f)
                {
                    DashStyle = seriea.Etena ? DashStyle.Dash : DashStyle.Solid
                };
                using Brush markerBrush = new SolidBrush(seriea.Kolorea);

                PointF[] points = seriea.Puntuak
                    .Select(p => new PointF(
                        plotArea.Left + (float)((p.Data.ToOADate() - minX) / (maxX - minX) * plotArea.Width),
                        plotArea.Bottom - (float)((p.Balioa - minY) / (maxY - minY) * plotArea.Height)))
                    .ToArray();

                if (points.Length >= 2)
                {
                    graphics.DrawLines(seriePen, points);
                }

                if (!seriea.Etena)
                {
                    foreach (PointF point in points)
                    {
                        graphics.FillEllipse(markerBrush, point.X - 4.5f, point.Y - 4.5f, 9, 9);
                        graphics.DrawEllipse(Pens.White, point.X - 4.5f, point.Y - 4.5f, 9, 9);
                    }
                }
            }

            graphics.DrawString(_ardatzYIzenburua, axisFont, textBrush, new PointF(plotArea.Left, plotArea.Top - 38));
            graphics.DrawString("Data", axisFont, textBrush, new PointF(plotArea.Right - 50, plotArea.Bottom + 96));

            float legendaY = legendArea.Top + 76;
            foreach (GrafikoSeriea seriea in _unekoSerieak)
            {
                using Pen legendPen = new Pen(seriea.Kolorea, seriea.Etena ? 2f : 3f)
                {
                    DashStyle = seriea.Etena ? DashStyle.Dash : DashStyle.Solid
                };

                Rectangle itemArea = new Rectangle(legendArea.Left + 14, (int)legendaY, legendArea.Width - 28, 108);
                using SolidBrush itemBrush = new SolidBrush(Color.White);
                graphics.FillRectangle(itemBrush, itemArea);
                graphics.DrawRectangle(Pens.Gainsboro, itemArea);

                graphics.DrawLine(legendPen, itemArea.Left + 16, itemArea.Top + 32, itemArea.Left + 80, itemArea.Top + 32);
                if (!seriea.Etena)
                {
                    using SolidBrush sampleBrush = new SolidBrush(seriea.Kolorea);
                    graphics.FillEllipse(sampleBrush, itemArea.Left + 42, itemArea.Top + 24, 14, 14);
                    graphics.DrawEllipse(Pens.White, itemArea.Left + 42, itemArea.Top + 24, 14, 14);
                }

                TextRenderer.DrawText(
                    graphics,
                    seriea.Izena,
                    legendTitleFont,
                    new Rectangle(itemArea.Left + 96, itemArea.Top + 14, itemArea.Width - 112, 38),
                    textColor,
                    TextFormatFlags.Left | TextFormatFlags.WordBreak | TextFormatFlags.VerticalCenter);

                TextRenderer.DrawText(
                    graphics,
                    seriea.Etena ? "Joera lineala" : "Neurketa seriea",
                    subtitleFont,
                    new Rectangle(itemArea.Left + 96, itemArea.Top + 58, itemArea.Width - 112, 30),
                    mutedTextColor,
                    TextFormatFlags.Left | TextFormatFlags.WordBreak | TextFormatFlags.VerticalCenter);

                legendaY += 116;
            }
        }

        private void _edukiPanela_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
