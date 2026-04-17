using GOsasun_app.Modeloa;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace GOsasun_app.Kontrola.Zerbitzuak
{
    public enum TxostenGrafikaMota
    {
        Pisua,
        Altuera,
        Pultsua,
        Presioa
    }

    public sealed class TxostenGrafikaZerbitzua
    {
        private sealed class GrafikoSeriea
        {
            public string Izena { get; init; } = string.Empty;
            public Color Kolorea { get; init; }
            public bool Etena { get; init; }
            public List<(DateTime Data, double Balioa)> Puntuak { get; init; } = new List<(DateTime Data, double Balioa)>();
        }

        public static string LortuGrafikaTestua(TxostenGrafikaMota mota)
        {
            return mota switch
            {
                TxostenGrafikaMota.Pisua => "Pisua (kg)",
                TxostenGrafikaMota.Altuera => "Altuera (m)",
                TxostenGrafikaMota.Pultsua => "Pultsua (ppm)",
                TxostenGrafikaMota.Presioa => "Presio Sist./Diast. (mmHg)",
                _ => "Grafika"
            };
        }

        public byte[]? SortuGrafikaIrudia(
            Pazientea pazientea,
            IReadOnlyList<Jarraipena> jarraipenak,
            TxostenGrafikaMota mota,
            DateTime? hasieraData,
            DateTime? amaieraData)
        {
            List<Jarraipena> ordenatutakoJarraipenak = jarraipenak
                .OrderBy(jarraipena => jarraipena.ErregistroData)
                .ToList();

            List<GrafikoSeriea> serieak = SortuSerieak(ordenatutakoJarraipenak, mota, out string ardatzYIzenburua);
            if (serieak.Count == 0 || serieak.All(seriea => seriea.Puntuak.Count == 0))
            {
                return null;
            }

            string izenburua = $"{LortuGrafikaTestua(mota)} - {pazientea.IzenOsoa}";
            string azpititulua = SortuAzpititulua(ordenatutakoJarraipenak, hasieraData, amaieraData);
            return MarraztuGrafikoa(izenburua, azpititulua, ardatzYIzenburua, serieak);
        }

        private static List<GrafikoSeriea> SortuSerieak(IReadOnlyList<Jarraipena> jarraipenak, TxostenGrafikaMota mota, out string ardatzYIzenburua)
        {
            List<GrafikoSeriea> serieak = new List<GrafikoSeriea>();
            ardatzYIzenburua = "Balioa";

            switch (mota)
            {
                case TxostenGrafikaMota.Pisua:
                    GehituSerieBakarra(serieak, jarraipenak, "Pisua (kg)", "Pisua (kg)", Color.FromArgb(52, 152, 219), j => j.PisuaKg.HasValue ? (double?)j.PisuaKg.Value : null, out ardatzYIzenburua);
                    break;
                case TxostenGrafikaMota.Altuera:
                    GehituSerieBakarra(serieak, jarraipenak, "Altuera (m)", "Altuera (m)", Color.FromArgb(39, 174, 96), j => j.Altuera.HasValue ? (double?)j.Altuera.Value : null, out ardatzYIzenburua);
                    break;
                case TxostenGrafikaMota.Pultsua:
                    GehituSerieBakarra(serieak, jarraipenak, "Pultsua (ppm)", "Pultsua (ppm)", Color.FromArgb(231, 76, 60), j => j.PultsuaPpm.HasValue ? (double?)j.PultsuaPpm.Value : null, out ardatzYIzenburua);
                    break;
                case TxostenGrafikaMota.Presioa:
                    GehituPresioSerieak(serieak, jarraipenak, out ardatzYIzenburua);
                    break;
            }

            return serieak;
        }

        private static void GehituSerieBakarra(
            List<GrafikoSeriea> serieak,
            IReadOnlyList<Jarraipena> jarraipenak,
            string serieIzena,
            string ardatzIzena,
            Color kolorea,
            Func<Jarraipena, double?> balioHautatzailea,
            out string ardatzYIzenburua)
        {
            List<(DateTime Data, double Balioa)> puntuak = jarraipenak
                .Select(jarraipena => (jarraipena.ErregistroData, balioHautatzailea(jarraipena)))
                .Where(item => item.Item2.HasValue)
                .Select(item => (item.ErregistroData, item.Item2!.Value))
                .ToList();

            ardatzYIzenburua = ardatzIzena;
            if (puntuak.Count == 0)
            {
                return;
            }

            serieak.Add(SortuDatuSeriea(serieIzena, kolorea, puntuak));
            GehituErregresioSeriea(serieak, $"{serieIzena} joera", puntuak, kolorea);
        }

        private static void GehituPresioSerieak(List<GrafikoSeriea> serieak, IReadOnlyList<Jarraipena> jarraipenak, out string ardatzYIzenburua)
        {
            ardatzYIzenburua = "Presioa (mmHg)";

            List<(DateTime Data, double Balioa)> sistolikoa = jarraipenak
                .Where(j => j.TentsioSistolikoa.HasValue)
                .Select(j => (j.ErregistroData, (double)j.TentsioSistolikoa!.Value))
                .ToList();

            List<(DateTime Data, double Balioa)> diastolikoa = jarraipenak
                .Where(j => j.TentsioDiastolikoa.HasValue)
                .Select(j => (j.ErregistroData, (double)j.TentsioDiastolikoa!.Value))
                .ToList();

            if (sistolikoa.Count > 0)
            {
                serieak.Add(SortuDatuSeriea("Sistolikoa", Color.FromArgb(52, 152, 219), sistolikoa));
                GehituErregresioSeriea(serieak, "Sistoliko joera", sistolikoa, Color.FromArgb(52, 152, 219));
            }

            if (diastolikoa.Count > 0)
            {
                serieak.Add(SortuDatuSeriea("Diastolikoa", Color.FromArgb(231, 76, 60), diastolikoa));
                GehituErregresioSeriea(serieak, "Diastoliko joera", diastolikoa, Color.FromArgb(231, 76, 60));
            }
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

        private static void GehituErregresioSeriea(List<GrafikoSeriea> serieak, string izena, IReadOnlyList<(DateTime Data, double Balioa)> puntuak, Color kolorea)
        {
            if (puntuak.Count < 2)
            {
                return;
            }

            (double malda, double ebakidura) = KalkulatuErregresioLineala(puntuak);
            double lehenX = puntuak.First().Data.ToOADate();
            double azkenX = puntuak.Last().Data.ToOADate();

            serieak.Add(SortuDatuSeriea(
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

        private static string SortuAzpititulua(IReadOnlyList<Jarraipena> jarraipenak, DateTime? hasieraData, DateTime? amaieraData)
        {
            if (hasieraData.HasValue || amaieraData.HasValue)
            {
                string hasiera = hasieraData?.ToString("yyyy/MM/dd") ?? "-";
                string amaiera = amaieraData?.ToString("yyyy/MM/dd") ?? "-";
                return $"Datu tartea: {hasiera} - {amaiera}";
            }

            if (jarraipenak.Count == 0)
            {
                return "Datu tartea: neurketa guztiak";
            }

            return $"Datu tartea: neurketa guztiak ({jarraipenak.First().ErregistroData:yyyy/MM/dd} - {jarraipenak.Last().ErregistroData:yyyy/MM/dd})";
        }

        private static byte[] MarraztuGrafikoa(string izenburua, string azpititulua, string ardatzYIzenburua, IReadOnlyList<GrafikoSeriea> serieak)
        {
            using Bitmap bitmap = new Bitmap(1400, 760);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.Clear(Color.FromArgb(248, 251, 253));

            Rectangle area = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
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

            using Font titleFont = new Font("Segoe UI", 16F, FontStyle.Bold);
            using Font subtitleFont = new Font("Segoe UI", 10F, FontStyle.Regular);
            using Font axisFont = new Font("Segoe UI", 9.3F, FontStyle.Bold);
            using Font labelFont = new Font("Segoe UI", 8.4F, FontStyle.Regular);
            using Font legendTitleFont = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            using Brush textBrush = new SolidBrush(textColor);
            using Brush mutedTextBrush = new SolidBrush(mutedTextColor);
            using Pen axisPen = new Pen(Color.FromArgb(132, 146, 166), 1.2f);
            using Pen gridPen = new Pen(Color.FromArgb(230, 236, 242), 1f);
            using Pen dividerPen = new Pen(Color.FromArgb(226, 232, 238), 1f);

            graphics.DrawString(izenburua, titleFont, textBrush, new PointF(cardArea.Left + 22, cardArea.Top + 18));
            graphics.DrawString(azpititulua, subtitleFont, mutedTextBrush, new PointF(cardArea.Left + 22, cardArea.Top + 58));
            graphics.DrawLine(dividerPen, cardArea.Left + 18, cardArea.Top + 98, cardArea.Right - 18, cardArea.Top + 98);

            Rectangle legendArea = new Rectangle(cardArea.Right - 360, cardArea.Top + 118, 318, cardArea.Height - 158);
            Rectangle plotArea = new Rectangle(cardArea.Left + 86, cardArea.Top + 136, Math.Max(150, legendArea.Left - (cardArea.Left + 118)), Math.Max(200, cardArea.Height - 262));

            using SolidBrush plotBrush = new SolidBrush(plotFill);
            graphics.FillRectangle(plotBrush, plotArea);
            graphics.DrawRectangle(cardPen, plotArea);

            using SolidBrush legendBrush = new SolidBrush(Color.FromArgb(249, 251, 252));
            graphics.FillRectangle(legendBrush, legendArea);
            graphics.DrawRectangle(cardPen, legendArea);
            TextRenderer.DrawText(graphics, "Legenda klinikoa", legendTitleFont, new Rectangle(legendArea.Left + 18, legendArea.Top + 16, legendArea.Width - 36, 38), textColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.PreserveGraphicsClipping);
            graphics.DrawLine(dividerPen, legendArea.Left + 14, legendArea.Top + 58, legendArea.Right - 14, legendArea.Top + 58);

            List<(DateTime Data, double Balioa)> puntuGuztiak = serieak.SelectMany(seriea => seriea.Puntuak).ToList();
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

            foreach (GrafikoSeriea seriea in serieak.Where(s => s.Puntuak.Count > 0))
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

            graphics.DrawString(ardatzYIzenburua, axisFont, textBrush, new PointF(plotArea.Left, plotArea.Top - 38));
            graphics.DrawString("Data", axisFont, textBrush, new PointF(plotArea.Right - 50, plotArea.Bottom + 96));

            float legendaY = legendArea.Top + 76;
            foreach (GrafikoSeriea seriea in serieak)
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

                TextRenderer.DrawText(graphics, seriea.Izena, legendTitleFont, new Rectangle(itemArea.Left + 96, itemArea.Top + 14, itemArea.Width - 112, 38), textColor, TextFormatFlags.Left | TextFormatFlags.WordBreak | TextFormatFlags.VerticalCenter);
                TextRenderer.DrawText(graphics, seriea.Etena ? "Joera lineala" : "Neurketa seriea", subtitleFont, new Rectangle(itemArea.Left + 96, itemArea.Top + 58, itemArea.Width - 112, 30), mutedTextColor, TextFormatFlags.Left | TextFormatFlags.WordBreak | TextFormatFlags.VerticalCenter);

                legendaY += 116;
            }

            using MemoryStream memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, ImageFormat.Png);
            return memoryStream.ToArray();
        }
    }
}