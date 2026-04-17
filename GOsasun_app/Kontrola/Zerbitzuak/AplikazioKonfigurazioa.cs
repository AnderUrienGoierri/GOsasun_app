using System.Text.Json;
using System.Text.Json.Serialization;

namespace GOsasun_app.Kontrola.Zerbitzuak
{
    public sealed class AplikazioKonfigurazioa
    {
        public int Bertsioa { get; set; } = 1;

        public DatuBaseKonfigurazioa DatuBasea { get; set; } = new DatuBaseKonfigurazioa();

        public BiltegiratzeKonfigurazioa Biltegiratzea { get; set; } = new BiltegiratzeKonfigurazioa();

        public AbiokoKonfigurazioa Abioa { get; set; } = new AbiokoKonfigurazioa();
    }

    public sealed class DatuBaseKonfigurazioa
    {
        public string Zerbitzaria { get; set; } = "localhost";

        public uint Portua { get; set; } = 3306;

        public string DatuBasea { get; set; } = "GOsasun_DB";

        public string Erabiltzailea { get; set; } = "root";

        public string Pasahitza { get; set; } = "1MG32025";
    }

    public sealed class BiltegiratzeKonfigurazioa
    {
        public string WebErroa { get; set; } = Path.Combine("C:", "Apache24-64", "htdocs", "GOsasun_web");
    }

    public sealed class AbiokoKonfigurazioa
    {
        public bool LehenAbioEgiaztatua { get; set; }

        public bool SortuDatuBaseEskemaLehenAbioan { get; set; }

        public bool KargatuHasierakoDatuakLehenAbioan { get; set; }
    }

    public static class AplikazioKonfigurazioaHornitzailea
    {
        private static readonly object BlokeoObjektua = new object();
        private static readonly JsonSerializerOptions JsonAukerak = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.Never
        };

        private static AplikazioKonfigurazioa? _cache;

        public static string KonfigurazioFitxategiBidea => Path.Combine(AppContext.BaseDirectory, "appsettings.json");

        public static AplikazioKonfigurazioa LortuKonfigurazioa()
        {
            lock (BlokeoObjektua)
            {
                if (_cache != null)
                {
                    return _cache;
                }

                AplikazioKonfigurazioa konfigurazioa = SortuKonfigurazioaDiskotik();
                konfigurazioa = NormalizatuKonfigurazioa(konfigurazioa);
                _cache = konfigurazioa;
                GordeDiskoan(konfigurazioa);
                return konfigurazioa;
            }
        }

        public static void GordeKonfigurazioa(AplikazioKonfigurazioa konfigurazioa)
        {
            lock (BlokeoObjektua)
            {
                _cache = NormalizatuKonfigurazioa(konfigurazioa);
                GordeDiskoan(_cache);
            }
        }

        public static void BerrizKargatu()
        {
            lock (BlokeoObjektua)
            {
                _cache = null;
            }
        }

        private static AplikazioKonfigurazioa SortuKonfigurazioaDiskotik()
        {
            if (!File.Exists(KonfigurazioFitxategiBidea))
            {
                return SortuLehenetsia();
            }

            string edukia = File.ReadAllText(KonfigurazioFitxategiBidea);
            if (string.IsNullOrWhiteSpace(edukia))
            {
                return SortuLehenetsia();
            }

            return JsonSerializer.Deserialize<AplikazioKonfigurazioa>(edukia, JsonAukerak) ?? SortuLehenetsia();
        }

        private static void GordeDiskoan(AplikazioKonfigurazioa konfigurazioa)
        {
            string edukia = JsonSerializer.Serialize(konfigurazioa, JsonAukerak);
            File.WriteAllText(KonfigurazioFitxategiBidea, edukia);
        }

        private static AplikazioKonfigurazioa SortuLehenetsia()
        {
            return new AplikazioKonfigurazioa();
        }

        private static AplikazioKonfigurazioa NormalizatuKonfigurazioa(AplikazioKonfigurazioa konfigurazioa)
        {
            konfigurazioa ??= SortuLehenetsia();
            konfigurazioa.DatuBasea ??= new DatuBaseKonfigurazioa();
            konfigurazioa.Biltegiratzea ??= new BiltegiratzeKonfigurazioa();
            konfigurazioa.Abioa ??= new AbiokoKonfigurazioa();

            konfigurazioa.DatuBasea.Zerbitzaria = NormalizatuTestua(konfigurazioa.DatuBasea.Zerbitzaria, "localhost");
            konfigurazioa.DatuBasea.Portua = konfigurazioa.DatuBasea.Portua == 0 ? 3306U : konfigurazioa.DatuBasea.Portua;
            konfigurazioa.DatuBasea.DatuBasea = NormalizatuTestua(konfigurazioa.DatuBasea.DatuBasea, "GOsasun_DB");
            konfigurazioa.DatuBasea.Erabiltzailea = NormalizatuTestua(konfigurazioa.DatuBasea.Erabiltzailea, "root");
            konfigurazioa.DatuBasea.Pasahitza ??= string.Empty;
            konfigurazioa.Biltegiratzea.WebErroa = NormalizatuBidea(konfigurazioa.Biltegiratzea.WebErroa, Path.Combine("C:", "Apache24-64", "htdocs", "GOsasun_web"));

            return konfigurazioa;
        }

        private static string NormalizatuTestua(string? balioa, string lehenetsia)
        {
            return string.IsNullOrWhiteSpace(balioa) ? lehenetsia : balioa.Trim();
        }

        private static string NormalizatuBidea(string? balioa, string lehenetsia)
        {
            string hautatua = string.IsNullOrWhiteSpace(balioa) ? lehenetsia : balioa.Trim().TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            return Path.GetFullPath(hautatua);
        }
    }

    public static class AplikazioBideak
    {
        public static string LortuWebErroa()
        {
            return AplikazioKonfigurazioaHornitzailea.LortuKonfigurazioa().Biltegiratzea.WebErroa;
        }

        public static string LortuDokumentuKarpeta()
        {
            return Path.Combine(LortuWebErroa(), "dokumentuak");
        }

        public static string LortuPazienteDokumentuKarpeta()
        {
            return Path.Combine(LortuWebErroa(), "paziente_dokumentuak");
        }

        public static string LortuXmlKarpeta()
        {
            return Path.Combine(LortuWebErroa(), "xml_paziente_neurketak");
        }

        public static string LortuIrudiKarpeta()
        {
            return Path.Combine(LortuWebErroa(), "img", "png");
        }

        public static string LortuAplikazioIrudiKarpeta()
        {
            return Path.Combine(AppContext.BaseDirectory, "img");
        }

        public static IEnumerable<string> LortuIrudiErroak()
        {
            HashSet<string> erroak = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            GehituErroa(erroak, LortuIrudiKarpeta());
            GehituErroa(erroak, LortuAplikazioIrudiKarpeta());

            return erroak;
        }

        public static string LortuIrudiHelmugaBidea(string irudiErlatiboa)
        {
            string normalizatua = irudiErlatiboa
                .Replace('/', Path.DirectorySeparatorChar)
                .Replace('\\', Path.DirectorySeparatorChar)
                .TrimStart(Path.DirectorySeparatorChar);

            string imgPngAurreizkia = $"img{Path.DirectorySeparatorChar}png{Path.DirectorySeparatorChar}";
            string imgAurreizkia = $"img{Path.DirectorySeparatorChar}";

            if (normalizatua.StartsWith(imgPngAurreizkia, StringComparison.OrdinalIgnoreCase))
            {
                normalizatua = normalizatua.Substring(imgPngAurreizkia.Length);
            }
            else if (normalizatua.StartsWith(imgAurreizkia, StringComparison.OrdinalIgnoreCase))
            {
                normalizatua = normalizatua.Substring(imgAurreizkia.Length);
            }

            return Path.Combine(LortuIrudiKarpeta(), normalizatua);
        }

        public static void ZiurtatuBiltegiratzeKarpetak()
        {
            Directory.CreateDirectory(LortuWebErroa());
            Directory.CreateDirectory(LortuDokumentuKarpeta());
            Directory.CreateDirectory(LortuPazienteDokumentuKarpeta());
            Directory.CreateDirectory(LortuXmlKarpeta());
            Directory.CreateDirectory(LortuIrudiKarpeta());
        }

        private static void GehituErroa(HashSet<string> erroak, string? bidea)
        {
            if (!string.IsNullOrWhiteSpace(bidea))
            {
                erroak.Add(bidea);
            }
        }
    }
}