using GOsasun_app.Modeloa;
using GOsasun_app.Repositorioa;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace GOsasun_app.Kontrola.Zerbitzuak
{
    public class DokumentuPdfZerbitzua
    {
        private const string PazienteDokumentuKarpeta = @"C:\Apache24-64\htdocs\GOsasun_web\paziente_dokumentuak";

        private readonly ErabiltzaileDB _erabiltzaileDb = new ErabiltzaileDB();
        private readonly JarraipenaDB _jarraipenaDb = new JarraipenaDB();
        private readonly ErrezetaDB _errezetaDb = new ErrezetaDB();
        private readonly HitzorduDB _hitzorduDb = new HitzorduDB();

        static DokumentuPdfZerbitzua()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public static string SortuHelmugaBidea(string fitxategiIzena)
        {
            Directory.CreateDirectory(PazienteDokumentuKarpeta);
            string oinarria = Path.GetFileNameWithoutExtension(fitxategiIzena);
            string luzapena = Path.GetExtension(fitxategiIzena);
            string segurua = SortuIzenSegurua($"{oinarria}_{DateTime.Now:yyyyMMdd_HHmmss}{luzapena}");
            return Path.Combine(PazienteDokumentuKarpeta, segurua);
        }

        public string SortuPazientearenTxostena(int pazienteId, string dokumentuIzena)
        {
            Pazientea pazientea = _erabiltzaileDb.LortuPazientea(pazienteId)
                ?? throw new InvalidOperationException("Pazientea ez da aurkitu.");

            List<Jarraipena> azkenHilabetekoJarraipenak = _jarraipenaDb
                .LortuPazientearenJarraipenak(pazienteId)
                .Where(x => x.ErregistroData >= DateTime.Now.AddMonths(-1))
                .OrderByDescending(x => x.ErregistroData)
                .ToList();

            Errezeta? azkenErrezeta = _errezetaDb
                .LortuPazientearenErrezetak(pazienteId)
                .OrderByDescending(x => x.IgorpenData)
                .FirstOrDefault();

            List<Hitzordua> hitzorduak = _hitzorduDb.LortuPazientearenHitzorduak(pazienteId);
            Hitzordua? azkenHitzordua = hitzorduak
                .Where(x => x.Data.Date <= DateTime.Today)
                .OrderByDescending(x => x.Data)
                .ThenByDescending(x => x.HasieraOrdua)
                .FirstOrDefault();
            Hitzordua? hurrengoHitzordua = hitzorduak
                .Where(x => x.Data.Date >= DateTime.Today)
                .OrderBy(x => x.Data)
                .ThenBy(x => x.HasieraOrdua)
                .FirstOrDefault();

            string bidea = SortuHelmugaBidea($"{dokumentuIzena}.pdf");
            byte[]? logoa = IrakurriIrudia(BilatuFitxategia(Path.Combine("img", "png", "logoak", "GOsasun_logoa.png")));
            byte[]? pazienteIrudia = IrakurriIrudia(BilatuPazienteIrudia(pazientea.Irudia));

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(28);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header().Column(column =>
                    {
                        column.Item().Row(row =>
                        {
                            if (logoa != null)
                            {
                                row.ConstantItem(120).Height(70).Image(logoa).FitArea();
                            }
                            else
                            {
                                row.ConstantItem(120).Height(70).AlignMiddle().Text("GOsasun").Bold().FontSize(24).FontColor(Colors.Red.Darken2);
                            }

                            row.RelativeItem().PaddingLeft(10).Column(info =>
                            {
                                info.Item().Text("MEDIKU TXOSTENA").Bold().FontSize(24).FontColor(Colors.Red.Darken2);
                                info.Item().Text($"Sortze data: {DateTime.Now:yyyy/MM/dd HH:mm}");
                                info.Item().Text($"Dokumentua: {dokumentuIzena}").SemiBold();
                            });

                            if (pazienteIrudia != null)
                            {
                                row.ConstantItem(85).Height(85).AlignRight().Image(pazienteIrudia).FitArea();
                            }
                        });

                        column.Item().PaddingTop(12).BorderBottom(1).BorderColor(Colors.Grey.Lighten1);
                    });

                    page.Content().PaddingTop(16).Column(column =>
                    {
                        column.Spacing(14);
                        column.Item().Element(x => AtalBurua(x, "Pazientearen datuak"));
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(def =>
                            {
                                def.RelativeColumn(2);
                                def.RelativeColumn(3);
                                def.RelativeColumn(2);
                                def.RelativeColumn(3);
                            });

                            GehituInfoGelaxka(table, "Izena", pazientea.IzenOsoa);
                            GehituInfoGelaxka(table, "NAN", pazientea.Nan);
                            GehituInfoGelaxka(table, "Jaiotze data", pazientea.JaiotzeData == DateTime.MinValue ? "-" : pazientea.JaiotzeData.ToString("yyyy/MM/dd"));
                            GehituInfoGelaxka(table, "Telefonoa", Balioa(pazientea.Telefonoa));
                            GehituInfoGelaxka(table, "Emaila", Balioa(pazientea.Emaila));
                            GehituInfoGelaxka(table, "Helbidea", Balioa(pazientea.Helbidea));
                            GehituInfoGelaxka(table, "Herria", Balioa(pazientea.Herria));
                            GehituInfoGelaxka(table, "Posta kodea", Balioa(pazientea.PostaKodea));
                            GehituInfoGelaxka(table, "Sexua", Balioa(pazientea.Sexua));
                            GehituInfoGelaxka(table, "Odol taldea", Balioa(pazientea.OdolTaldea));
                            GehituInfoGelaxka(table, "Altuera", pazientea.AzkenAltuera.HasValue ? $"{pazientea.AzkenAltuera.Value:N2} m" : "-");
                            GehituInfoGelaxka(table, "Pisua", pazientea.AzkenPisua.HasValue ? $"{pazientea.AzkenPisua.Value:N2} kg" : "-");
                        });

                        column.Item().Element(x => AtalBurua(x, "Azken hilabeteko jarraipen historiala"));
                        if (azkenHilabetekoJarraipenak.Count == 0)
                        {
                            column.Item().Text("Ez dago azken hilabeteko jarraipen erregistrorik.");
                        }
                        else
                        {
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(def =>
                                {
                                    def.RelativeColumn(2);
                                    def.RelativeColumn(1);
                                    def.RelativeColumn(1);
                                    def.RelativeColumn(1);
                                    def.RelativeColumn(1);
                                    def.RelativeColumn(3);
                                });

                                table.Header(header =>
                                {
                                    GehituGoiburua(header, "Data");
                                    GehituGoiburua(header, "Sist.");
                                    GehituGoiburua(header, "Diast.");
                                    GehituGoiburua(header, "Pultsua");
                                    GehituGoiburua(header, "Pisua");
                                    GehituGoiburua(header, "Oharrak");
                                });

                                foreach (Jarraipena item in azkenHilabetekoJarraipenak)
                                {
                                    GehituEdukia(table, item.ErregistroData.ToString("yyyy/MM/dd HH:mm"));
                                    GehituEdukia(table, item.TentsioSistolikoa?.ToString() ?? "-");
                                    GehituEdukia(table, item.TentsioDiastolikoa?.ToString() ?? "-");
                                    GehituEdukia(table, item.PultsuaPpm?.ToString() ?? "-");
                                    GehituEdukia(table, item.PisuaKg?.ToString("N2") ?? "-");
                                    GehituEdukia(table, Balioa(item.Oharrak));
                                }
                            });
                        }

                        column.Item().Element(x => AtalBurua(x, "Azken errezeta"));
                        if (azkenErrezeta == null)
                        {
                            column.Item().Text("Ez dago errezeta aktiborik.");
                        }
                        else
                        {
                            column.Item().Text($"Igorpen data: {azkenErrezeta.IgorpenData:yyyy/MM/dd}");
                            column.Item().Text($"Diagnostikoa: {Balioa(azkenErrezeta.Diagnostikoa)}");
                            string botikak = azkenErrezeta.Botikak.Count == 0
                                ? "-"
                                : string.Join(", ", azkenErrezeta.Botikak.Select(x => x.BotikaIzena).Where(x => !string.IsNullOrWhiteSpace(x)));
                            column.Item().Text($"Botikak: {botikak}");
                        }

                        column.Item().Element(x => AtalBurua(x, "Hitzordu laburpena"));
                        column.Item().Text($"Azken hitzordua: {FormatHitzordua(azkenHitzordua)}");
                        column.Item().Text($"Hurrengo hitzordua: {FormatHitzordua(hurrengoHitzordua)}");
                    });

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("GOsasun - Paziente txostena");
                        x.Span(" | ");
                        x.CurrentPageNumber();
                    });
                });
            }).GeneratePdf(bidea);

            return bidea;
        }

        private static void AtalBurua(IContainer container, string testua)
        {
            container.Background(Colors.Red.Lighten4)
                .PaddingVertical(6)
                .PaddingHorizontal(10)
                .Text(testua)
                .Bold()
                .FontSize(14)
                .FontColor(Colors.Red.Darken2);
        }

        private static void GehituInfoGelaxka(TableDescriptor table, string etiketa, string balioa)
        {
            table.Cell().Padding(4).Text(etiketa).SemiBold();
            table.Cell().Padding(4).Text(balioa);
        }

        private static void GehituGoiburua(TableCellDescriptor header, string testua)
        {
            header.Cell().Element(x => x.BorderBottom(1).BorderColor(Colors.Grey.Lighten1).Padding(4))
                .Text(testua)
                .SemiBold();
        }

        private static void GehituEdukia(TableDescriptor table, string testua)
        {
            table.Cell()
                .Element(x => x.BorderBottom(1).BorderColor(Colors.Grey.Lighten3).PaddingVertical(3).PaddingHorizontal(4))
                .Text(testua);
        }

        private static string Balioa(string? balioa)
        {
            return string.IsNullOrWhiteSpace(balioa) ? "-" : balioa.Trim();
        }

        private static string FormatHitzordua(Hitzordua? hitzordua)
        {
            if (hitzordua == null) return "-";
            string orduak = $"{hitzordua.HasieraOrdua:hh\\:mm}";
            if (hitzordua.BukaeraOrdua.HasValue)
            {
                orduak += $" - {hitzordua.BukaeraOrdua.Value:hh\\:mm}";
            }

            return $"{hitzordua.Data:yyyy/MM/dd} ({orduak}) {Balioa(hitzordua.Arrazoia)}";
        }

        private static string SortuIzenSegurua(string izena)
        {
            foreach (char baliogabea in Path.GetInvalidFileNameChars())
            {
                izena = izena.Replace(baliogabea, '_');
            }

            return izena;
        }

        private static byte[]? IrakurriIrudia(string? path)
        {
            return string.IsNullOrWhiteSpace(path) || !File.Exists(path) ? null : File.ReadAllBytes(path);
        }

        private static string? BilatuPazienteIrudia(string? irudia)
        {
            if (string.IsNullOrWhiteSpace(irudia)) return BilatuFitxategia(Path.Combine("img", "png", "irudi_lehenetsia.png"));

            string normalizatua = irudia.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
            string? aurkitua = BilatuFitxategia(normalizatua);
            return aurkitua ?? BilatuFitxategia(Path.Combine("img", "png", "irudi_lehenetsia.png"));
        }

        private static string? BilatuFitxategia(string relativePath)
        {
            HashSet<string> erroak = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string?[] hasierakoak =
            {
                Application.StartupPath,
                AppContext.BaseDirectory,
                Directory.GetCurrentDirectory(),
                Environment.CurrentDirectory,
                Path.GetDirectoryName(typeof(DokumentuPdfZerbitzua).Assembly.Location)
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

            foreach (string erroa in erroak)
            {
                string[] aukerak =
                {
                    Path.Combine(erroa, relativePath),
                    Path.Combine(erroa, "GOsasun_app", relativePath)
                };

                string? aurkitua = aukerak.FirstOrDefault(File.Exists);
                if (!string.IsNullOrWhiteSpace(aurkitua)) return aurkitua;
            }

            return null;
        }
    }
}