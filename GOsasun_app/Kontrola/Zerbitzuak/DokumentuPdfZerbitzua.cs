using GOsasun_app.Modeloa;
using GOsasun_app.Repositorioa;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace GOsasun_app.Kontrola.Zerbitzuak
{
    public class DokumentuPdfZerbitzua
    {
        private const string PazienteDokumentuKarpeta = @"C:\Apache24-64\htdocs\GOsasun_web\paziente_dokumentuak";

        private readonly ErabiltzaileDB _erabiltzaileDb = new ErabiltzaileDB();
        private readonly JarraipenaDB _jarraipenaDb = new JarraipenaDB();
        private readonly ErrezetaDB _errezetaDb = new ErrezetaDB();
        private readonly HitzorduDB _hitzorduDb = new HitzorduDB();
        private readonly TxostenGrafikaZerbitzua _txostenGrafikaZerbitzua = new TxostenGrafikaZerbitzua();

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

        public string SortuPazientearenTxostena(
            int pazienteId,
            string dokumentuIzena,
            IReadOnlyCollection<TxostenGrafikaMota>? grafikaMotak = null,
            DateTime? grafikaHasieraData = null,
            DateTime? grafikaAmaieraData = null)
        {
            Pazientea pazientea = _erabiltzaileDb.LortuPazientea(pazienteId)
                ?? throw new InvalidOperationException("Pazientea ez da aurkitu.");

            List<Jarraipena> jarraipenGuztiak = _jarraipenaDb
                .LortuPazientearenJarraipenak(pazienteId)
                .OrderByDescending(x => x.ErregistroData)
                .ToList();

            Jarraipena? azkenJarraipena = jarraipenGuztiak.FirstOrDefault();
            List<Jarraipena> azkenHilabetekoJarraipenak = jarraipenGuztiak
                .Where(x => x.ErregistroData >= DateTime.Now.AddMonths(-1))
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
            List<TxostenGrafikaMota> txostenerakoGrafikak = (grafikaMotak ?? Array.Empty<TxostenGrafikaMota>())
                .Distinct()
                .ToList();
            List<Jarraipena> grafiketarakoJarraipenak = jarraipenGuztiak
                .Where(x => !grafikaHasieraData.HasValue || x.ErregistroData.Date >= grafikaHasieraData.Value.Date)
                .Where(x => !grafikaAmaieraData.HasValue || x.ErregistroData.Date <= grafikaAmaieraData.Value.Date)
                .OrderBy(x => x.ErregistroData)
                .ToList();

            string txostenFitxategiIzena = SortuTxostenFitxategiIzena(dokumentuIzena);
            string bidea = SortuHelmugaBidea($"{txostenFitxategiIzena}.pdf");
            byte[]? logoa = IrakurriIrudia(BilatuFitxategia(Path.Combine("img", "png", "logoak", "GOsasun_logoa.png")));
            byte[]? pazienteIrudia = IrakurriIrudia(BilatuPazienteIrudia(pazientea));

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
                                row.ConstantItem(110).Height(58).Image(logoa).FitArea();
                            }
                            else
                            {
                                row.ConstantItem(110).Height(58).AlignMiddle().Text("GOsasun").Bold().FontSize(22).FontColor("#1F5E78");
                            }

                            row.RelativeItem().PaddingLeft(10).Column(info =>
                            {
                                info.Item().Text("TXOSTEN MEDIKOA").Bold().FontSize(22).FontColor("#1F5E78");
                                info.Item().Text($"Sortze data: {DateTime.Now:yyyy/MM/dd HH:mm}");
                                info.Item().Text($"Dokumentua: {dokumentuIzena}").SemiBold();
                            });
                        });

                        column.Item().PaddingTop(10).BorderBottom(1).BorderColor("#D7E4EA");
                    });

                    page.Content().PaddingTop(16).Column(column =>
                    {
                        column.Spacing(14);
                        column.Item().Element(x => SortuTxostenGoiburua(x, pazientea, azkenJarraipena, dokumentuIzena, logoa, pazienteIrudia));

                        // Lehen orrialdea historia klinikoaren laburpenari soilik uzten zaio.
                        column.Item().PageBreak();

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

                            GehituInfoGelaxka(table, "Egoera klinikoa", Balioa(pazientea.EgoeraKlinikoa));
                            GehituInfoGelaxka(table, "Sexua", Balioa(pazientea.Sexua));
                            GehituInfoGelaxka(table, "Odol taldea", Balioa(pazientea.OdolTaldea));
                            GehituInfoGelaxka(table, "Altuera", pazientea.AzkenAltuera.HasValue ? $"{pazientea.AzkenAltuera.Value:N2} m" : "-");
                            GehituInfoGelaxka(table, "Pisua", pazientea.AzkenPisua.HasValue ? $"{pazientea.AzkenPisua.Value:N2} kg" : "-");
                            GehituInfoGelaxka(table, "GMI", FormatGmi(pazientea.AzkenPisua, pazientea.AzkenAltuera));
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

                        if (txostenerakoGrafikak.Count > 0)
                        {
                            for (int i = 0; i < txostenerakoGrafikak.Count; i++)
                            {
                                TxostenGrafikaMota mota = txostenerakoGrafikak[i];
                                byte[]? grafikaIrudia = _txostenGrafikaZerbitzua.SortuGrafikaIrudia(pazientea, grafiketarakoJarraipenak, mota, grafikaHasieraData, grafikaAmaieraData);

                                column.Item().PageBreak();
                                column.Item().Element(x => AtalBurua(x, "Bilakaera grafikoa"));
                                column.Item().PaddingTop(8).Text(TxostenGrafikaZerbitzua.LortuGrafikaTestua(mota))
                                    .Bold()
                                    .FontSize(13)
                                    .FontColor("#1F5E78");
                                column.Item().Text(FormatGrafikaDataTartea(grafikaHasieraData, grafikaAmaieraData, grafiketarakoJarraipenak))
                                    .FontColor("#6B7C85");

                                if (grafikaIrudia == null)
                                {
                                    column.Item().PaddingTop(12).Text("Ez dago datu nahikorik aukeratutako parametro honen grafika sortzeko.");
                                }
                                else
                                {
                                    column.Item()
                                        .PaddingTop(12)
                                        .Border(1)
                                        .BorderColor("#D7E4EA")
                                        .Padding(8)
                                        .Height(320)
                                        .Image(grafikaIrudia)
                                        .FitArea();
                                }
                            }
                        }
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

        private static void SortuTxostenGoiburua(IContainer container, Pazientea pazientea, Jarraipena? azkenJarraipena, string dokumentuIzena, byte[]? logoa, byte[]? pazienteIrudia)
        {
            container
                .Border(1)
                .BorderColor("#BFD6DE")
                .Background("#F6FBFC")
                .Padding(18)
                .Column(column =>
                {
                    column.Spacing(14);

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Column(info =>
                        {
                            info.Spacing(5);
                            info.Item().Text("GOsasun historia klinikoaren laburpena")
                                .Bold()
                                .FontSize(18)
                                .FontColor("#1F5E78");
                            info.Item().Text($"Dokumentu mota: {dokumentuIzena}").SemiBold().FontColor("#2F4858");
                            info.Item().Text($"Paziente IDa: {pazientea.Id}");
                            info.Item().Text($"Azken eguneratzea: {FormatNeurketaData(azkenJarraipena)}");
                        });

                        if (logoa != null)
                        {
                            row.ConstantItem(120).Height(62).AlignMiddle().Image(logoa).FitArea();
                        }
                    });

                    column.Item().Row(row =>
                    {
                        row.ConstantItem(140).Column(irudia =>
                        {
                            irudia.Item().Text("Pazientearen irudia")
                                .SemiBold()
                                .FontColor("#1F5E78");

                            if (pazienteIrudia != null)
                            {
                                irudia.Item()
                                    .PaddingTop(8)
                                    .Height(150)
                                    .Border(1)
                                    .BorderColor("#D7E4EA")
                                    .Padding(6)
                                    .Image(pazienteIrudia)
                                    .FitArea();
                            }
                            else
                            {
                                irudia.Item()
                                    .PaddingTop(8)
                                    .Height(150)
                                    .Border(1)
                                    .BorderColor("#D7E4EA")
                                    .AlignCenter()
                                    .AlignMiddle()
                                    .Text("Irudirik ez")
                                    .FontColor("#6B7C85");
                            }
                        });

                        row.RelativeItem().PaddingLeft(18).Column(info =>
                        {
                            info.Spacing(12);
                            info.Item().Element(x => SortuInformazioBlokea(x, "Erabiltzaile datuak", datuak =>
                            {
                                GehituDatuLerroa(datuak, "Izena-abizenak", Balioa(pazientea.IzenOsoa));
                                GehituDatuLerroa(datuak, "NAN", Balioa(pazientea.Nan));
                                GehituDatuLerroa(datuak, "Jaiotze data", FormatJaiotzeDataEtaAdina(pazientea.JaiotzeData));
                                GehituDatuLerroa(datuak, "Emaila", Balioa(pazientea.Emaila));
                                GehituDatuLerroa(datuak, "Telefonoa", Balioa(pazientea.Telefonoa));
                                GehituDatuLerroa(datuak, "Helbidea", FormatHelbidea(pazientea));
                                GehituDatuLerroa(datuak, "Hizkuntza", Balioa(pazientea.Hizkuntza));
                            }));

                            info.Item().Element(x => SortuInformazioBlokea(x, "Datu medikoak", datuak =>
                            {
                                GehituDatuLerroa(datuak, "Egoera klinikoa", Balioa(pazientea.EgoeraKlinikoa));
                                GehituDatuLerroa(datuak, "Sexua", Balioa(pazientea.Sexua));
                                GehituDatuLerroa(datuak, "Odol taldea", Balioa(pazientea.OdolTaldea));
                                GehituDatuLerroa(datuak, "Azken altuera", FormatAltuera(pazientea.AzkenAltuera));
                                GehituDatuLerroa(datuak, "Azken pisua", FormatPisua(pazientea.AzkenPisua));
                                GehituDatuLerroa(datuak, "GMI", FormatGmi(pazientea.AzkenPisua, pazientea.AzkenAltuera));
                                GehituDatuLerroa(datuak, "Azken tentsioa", FormatTentsioa(azkenJarraipena));
                                GehituDatuLerroa(datuak, "Azken pultsua", FormatPultsua(azkenJarraipena));
                            }));
                        });
                    });
                });
        }

        private static void AtalBurua(IContainer container, string testua)
        {
            container.Background("#E8F3F7")
                .PaddingVertical(6)
                .PaddingHorizontal(10)
                .Text(testua)
                .Bold()
                .FontSize(14)
                .FontColor("#1F5E78");
        }

        private static void SortuInformazioBlokea(IContainer container, string izenburua, Action<ColumnDescriptor> edukia)
        {
            container
                .Border(1)
                .BorderColor("#D7E4EA")
                .Background(Colors.White)
                .Padding(12)
                .Column(column =>
                {
                    column.Spacing(8);
                    column.Item().Text(izenburua)
                        .Bold()
                        .FontSize(13)
                        .FontColor("#1F5E78");
                    edukia(column);
                });
        }

        private static void GehituDatuLerroa(ColumnDescriptor column, string etiketa, string balioa)
        {
            column.Item().Row(row =>
            {
                row.ConstantItem(130).Text(etiketa).SemiBold().FontColor("#35515F");
                row.RelativeItem().Text(balioa);
            });
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

        private static string FormatHelbidea(Pazientea pazientea)
        {
            string[] zatiak =
            {
                Balioa(pazientea.Helbidea),
                Balioa(pazientea.Herria),
                Balioa(pazientea.PostaKodea)
            };

            string emaitza = string.Join(", ", zatiak.Where(x => x != "-"));
            return string.IsNullOrWhiteSpace(emaitza) ? "-" : emaitza;
        }

        private static string FormatJaiotzeDataEtaAdina(DateTime jaiotzeData)
        {
            if (jaiotzeData == DateTime.MinValue)
            {
                return "-";
            }

            int adina = KalkulatuAdina(jaiotzeData, DateTime.Today);
            return $"{jaiotzeData:yyyy/MM/dd} ({adina} urte)";
        }

        private static int KalkulatuAdina(DateTime jaiotzeData, DateTime erreferentzia)
        {
            int adina = erreferentzia.Year - jaiotzeData.Year;
            if (jaiotzeData.Date > erreferentzia.AddYears(-adina))
            {
                adina--;
            }

            return Math.Max(adina, 0);
        }

        private static string FormatAltuera(decimal? altuera)
        {
            return altuera.HasValue ? $"{altuera.Value.ToString("N2", CultureInfo.InvariantCulture)} m" : "-";
        }

        private static string FormatPisua(decimal? pisua)
        {
            return pisua.HasValue ? $"{pisua.Value.ToString("N2", CultureInfo.InvariantCulture)} kg" : "-";
        }

        private static string FormatGmi(decimal? pisua, decimal? altuera)
        {
            if (!pisua.HasValue || !altuera.HasValue || altuera.Value <= 0)
            {
                return "-";
            }

            decimal gmi = pisua.Value / (altuera.Value * altuera.Value);
            return gmi.ToString("N1", CultureInfo.InvariantCulture);
        }

        private static string FormatTentsioa(Jarraipena? jarraipena)
        {
            if (jarraipena?.TentsioSistolikoa == null && jarraipena?.TentsioDiastolikoa == null)
            {
                return "-";
            }

            string sistolikoa = jarraipena?.TentsioSistolikoa?.ToString() ?? "-";
            string diastolikoa = jarraipena?.TentsioDiastolikoa?.ToString() ?? "-";
            return $"{sistolikoa}/{diastolikoa} mmHg";
        }

        private static string FormatPultsua(Jarraipena? jarraipena)
        {
            return jarraipena?.PultsuaPpm.HasValue == true ? $"{jarraipena.PultsuaPpm.Value} ppm" : "-";
        }

        private static string FormatNeurketaData(Jarraipena? jarraipena)
        {
            return jarraipena == null ? "Ez dago erregistrorik" : jarraipena.ErregistroData.ToString("yyyy/MM/dd HH:mm");
        }

        private static string FormatGrafikaDataTartea(DateTime? hasieraData, DateTime? amaieraData, IReadOnlyList<Jarraipena> jarraipenak)
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

        private static string SortuIzenSegurua(string izena)
        {
            foreach (char baliogabea in Path.GetInvalidFileNameChars())
            {
                izena = izena.Replace(baliogabea, '_');
            }

            return izena;
        }

        private static string SortuTxostenFitxategiIzena(string dokumentuIzena)
        {
            string oinarria = string.IsNullOrWhiteSpace(dokumentuIzena) ? "txostena" : dokumentuIzena.Trim();
            oinarria = Path.GetFileNameWithoutExtension(oinarria);
            oinarria = SortuIzenSegurua(oinarria);

            if (oinarria.StartsWith("Osasun_txostena_", StringComparison.OrdinalIgnoreCase))
            {
                return oinarria;
            }

            return $"Osasun_txostena_{oinarria}";
        }

        private static byte[]? IrakurriIrudia(string? path)
        {
            return string.IsNullOrWhiteSpace(path) || !File.Exists(path) ? null : File.ReadAllBytes(path);
        }

        private static string? BilatuPazienteIrudia(Pazientea pazientea)
        {
            IEnumerable<string> aukerak = LortuPazienteIrudiAukerak(pazientea);
            foreach (string aukera in aukerak)
            {
                string? aurkitua = BilatuFitxategia(aukera);
                if (!string.IsNullOrWhiteSpace(aurkitua))
                {
                    return aurkitua;
                }
            }

            return BilatuFitxategia(Path.Combine("img", "png", "irudi_lehenetsia.png"));
        }

        private static IEnumerable<string> LortuPazienteIrudiAukerak(Pazientea pazientea)
        {
            HashSet<string> aukerak = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (!string.IsNullOrWhiteSpace(pazientea.Irudia))
            {
                string normalizatua = pazientea.Irudia.Trim().Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);
                normalizatua = normalizatua.TrimStart(Path.DirectorySeparatorChar);

                if (!string.IsNullOrWhiteSpace(normalizatua))
                {
                    aukerak.Add(normalizatua);
                    aukerak.Add(Path.GetFileName(normalizatua));
                }
            }

            aukerak.Add(Path.Combine("img", "png", "pazienteak", $"pazientea_{pazientea.Id}.png"));
            return aukerak;
        }

        private static string? BilatuFitxategia(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
            {
                return null;
            }

            if (Path.IsPathRooted(relativePath))
            {
                return File.Exists(relativePath) ? relativePath : null;
            }

            string normalizatua = relativePath.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar).TrimStart(Path.DirectorySeparatorChar);
            foreach (string erroa in LortuIrudiErroak())
            {
                foreach (string aukera in LortuBideAukerak(erroa, normalizatua))
                {
                    if (File.Exists(aukera))
                    {
                        return aukera;
                    }
                }
            }

            return null;
        }

        private static IEnumerable<string> LortuIrudiErroak()
        {
            HashSet<string> erroak = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            string zerbitzarikoIrudiErroa = Path.Combine("C:", "Apache24-64", "htdocs", "GOsasun_web", "img", "png");
            if (Directory.Exists(zerbitzarikoIrudiErroa))
            {
                erroak.Add(zerbitzarikoIrudiErroa);
            }

            string? aplikazioIrudiErroa = LortuAplikazioIrudiErroa();
            if (!string.IsNullOrWhiteSpace(aplikazioIrudiErroa) && Directory.Exists(aplikazioIrudiErroa))
            {
                erroak.Add(aplikazioIrudiErroa);
            }

            return erroak;
        }

        private static string? LortuAplikazioIrudiErroa()
        {
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
                if (string.IsNullOrWhiteSpace(hasiera) || !Directory.Exists(hasiera))
                {
                    continue;
                }

                DirectoryInfo? karpeta = new DirectoryInfo(hasiera);
                while (karpeta != null)
                {
                    string imgErroa = Path.Combine(karpeta.FullName, "img");
                    if (string.Equals(karpeta.Name, "GOsasun_app", StringComparison.OrdinalIgnoreCase) && Directory.Exists(imgErroa))
                    {
                        return imgErroa;
                    }

                    karpeta = karpeta.Parent;
                }
            }

            return null;
        }

        private static IEnumerable<string> LortuBideAukerak(string erroa, string normalizatua)
        {
            HashSet<string> aukerak = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (string erlatiboAukera in LortuErlatiboAukerak(normalizatua))
            {
                aukerak.Add(Path.Combine(erroa, erlatiboAukera));
            }

            return aukerak;
        }

        private static IEnumerable<string> LortuErlatiboAukerak(string normalizatua)
        {
            HashSet<string> aukerak = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                normalizatua
            };

            string imgAurreizkia = $"img{Path.DirectorySeparatorChar}";
            string imgPngAurreizkia = $"img{Path.DirectorySeparatorChar}png{Path.DirectorySeparatorChar}";

            if (normalizatua.StartsWith(imgAurreizkia, StringComparison.OrdinalIgnoreCase))
            {
                aukerak.Add(normalizatua.Substring(imgAurreizkia.Length));
            }

            if (normalizatua.StartsWith(imgPngAurreizkia, StringComparison.OrdinalIgnoreCase))
            {
                aukerak.Add(normalizatua.Substring(imgPngAurreizkia.Length));
                aukerak.Add(Path.Combine("png", normalizatua.Substring(imgPngAurreizkia.Length)));
            }

            string fitxategiIzena = Path.GetFileName(normalizatua);
            if (!string.IsNullOrWhiteSpace(fitxategiIzena))
            {
                aukerak.Add(Path.Combine("png", fitxategiIzena));
                aukerak.Add(Path.Combine("png", "pazienteak", fitxategiIzena));
                aukerak.Add(Path.Combine("png", "osasun_langileak", fitxategiIzena));
                aukerak.Add(Path.Combine("png", "harrerakoak", fitxategiIzena));
                aukerak.Add(Path.Combine("pazienteak", fitxategiIzena));
                aukerak.Add(Path.Combine("osasun_langileak", fitxategiIzena));
                aukerak.Add(Path.Combine("harrerakoak", fitxategiIzena));
            }

            return aukerak;
        }
    }
}