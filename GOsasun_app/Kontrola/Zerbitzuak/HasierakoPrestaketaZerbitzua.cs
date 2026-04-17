using System.Text;
using System.Text.RegularExpressions;
using GOsasun_app.Repositorioa;
using MySql.Data.MySqlClient;

namespace GOsasun_app.Kontrola.Zerbitzuak
{
    public sealed class HasierakoPrestaketaEmaitza
    {
        public bool Ondo { get; init; }

        public string Mezua { get; init; } = string.Empty;

        public bool InformazioaErakutsi { get; init; }

        public static HasierakoPrestaketaEmaitza Arrakasta(string mezua = "", bool informazioaErakutsi = false)
        {
            return new HasierakoPrestaketaEmaitza
            {
                Ondo = true,
                Mezua = mezua,
                InformazioaErakutsi = informazioaErakutsi && !string.IsNullOrWhiteSpace(mezua)
            };
        }

        public static HasierakoPrestaketaEmaitza Errorea(string mezua)
        {
            return new HasierakoPrestaketaEmaitza
            {
                Ondo = false,
                Mezua = mezua,
                InformazioaErakutsi = true
            };
        }
    }

    internal sealed class DatuBaseHasieratzeEmaitza
    {
        public bool Ondo { get; init; }

        public string Mezua { get; init; } = string.Empty;

        public bool EskemaSortuDa { get; init; }

        public bool DatuakKargatuDira { get; init; }
    }

    public static class HasierakoPrestaketaZerbitzua
    {
        public static HasierakoPrestaketaEmaitza Exekutatu()
        {
            AplikazioKonfigurazioa konfigurazioa;
            try
            {
                konfigurazioa = AplikazioKonfigurazioaHornitzailea.LortuKonfigurazioa();
            }
            catch (Exception ex)
            {
                return HasierakoPrestaketaEmaitza.Errorea($"Ezin izan da konfigurazioa irakurri: {ex.Message}");
            }

            try
            {
                AplikazioBideak.ZiurtatuBiltegiratzeKarpetak();
            }
            catch (Exception ex)
            {
                return HasierakoPrestaketaEmaitza.Errorea($"Ezin izan dira Apache/web karpetak prestatu: {ex.Message}");
            }

            List<string> oharrak = new List<string>();

            if (konfigurazioa.Abioa.SortuDatuBaseEskemaLehenAbioan || konfigurazioa.Abioa.KargatuHasierakoDatuakLehenAbioan)
            {
                DatuBaseHasieratzeEmaitza emaitza = DatuBaseHasieratzailea.ZiurtatuPrestDago(konfigurazioa);
                if (!emaitza.Ondo)
                {
                    return HasierakoPrestaketaEmaitza.Errorea(emaitza.Mezua);
                }

                if (emaitza.EskemaSortuDa)
                {
                    oharrak.Add("Datu-basearen eskema prestatu da lehen exekuzioan.");
                }

                if (emaitza.DatuakKargatuDira)
                {
                    oharrak.Add("Hasierako erregistroak kargatu dira lehen exekuzioan.");
                }
            }

            if (!DatuBaseKonexioa.ProbatuKonexioa(out string erroreMezua))
            {
                return HasierakoPrestaketaEmaitza.Errorea(
                    "Ezin izan da datu-basera konektatu.\n" +
                    $"Zerbitzaria: {konfigurazioa.DatuBasea.Zerbitzaria}:{konfigurazioa.DatuBasea.Portua}\n" +
                    $"Datu-basea: {konfigurazioa.DatuBasea.DatuBasea}\n\n" +
                    erroreMezua);
            }

            if (!DatuBaseHasieratzailea.EskemaPrestDago())
            {
                return HasierakoPrestaketaEmaitza.Errorea(
                    "Datu-basearen eskema ez dago prest. Aktibatu lehen abioan eskema sortzea edo berrikusi sql fitxategiak.");
            }

            bool aldatuDa = !konfigurazioa.Abioa.LehenAbioEgiaztatua;
            konfigurazioa.Abioa.LehenAbioEgiaztatua = true;

            if (konfigurazioa.Abioa.SortuDatuBaseEskemaLehenAbioan)
            {
                konfigurazioa.Abioa.SortuDatuBaseEskemaLehenAbioan = false;
                aldatuDa = true;
            }

            if (konfigurazioa.Abioa.KargatuHasierakoDatuakLehenAbioan)
            {
                konfigurazioa.Abioa.KargatuHasierakoDatuakLehenAbioan = false;
                aldatuDa = true;
            }

            if (aldatuDa)
            {
                AplikazioKonfigurazioaHornitzailea.GordeKonfigurazioa(konfigurazioa);
            }

            string mezua = string.Join(Environment.NewLine, oharrak);
            return HasierakoPrestaketaEmaitza.Arrakasta(mezua, oharrak.Count > 0);
        }
    }

    internal static class DatuBaseHasieratzailea
    {
        private static readonly string[] OinarrizkoTaulak =
        {
            "rolak",
            "erabiltzaileak",
            "pazienteak",
            "jarraipenak",
            "dokumentuak",
            "hitzorduak",
            "errezetak"
        };

        public static DatuBaseHasieratzeEmaitza ZiurtatuPrestDago(AplikazioKonfigurazioa konfigurazioa)
        {
            bool eskemaSortuDa = false;
            bool datuakKargatuDira = false;

            try
            {
                ZiurtatuZerbitzariaEskuragarriDago();

                if (konfigurazioa.Abioa.SortuDatuBaseEskemaLehenAbioan && !EskemaPrestDago())
                {
                    ExekutatuEskemaSqlak();
                    eskemaSortuDa = true;
                }

                if (konfigurazioa.Abioa.KargatuHasierakoDatuakLehenAbioan && HasierakoDatuakBeharDira())
                {
                    ExekutatuHasierakoDatuak();
                    datuakKargatuDira = true;
                }

                return new DatuBaseHasieratzeEmaitza
                {
                    Ondo = true,
                    EskemaSortuDa = eskemaSortuDa,
                    DatuakKargatuDira = datuakKargatuDira
                };
            }
            catch (Exception ex)
            {
                return new DatuBaseHasieratzeEmaitza
                {
                    Ondo = false,
                    Mezua = $"Lehen exekuzioko DB prestaketan errorea gertatu da: {ex.Message}",
                    EskemaSortuDa = eskemaSortuDa,
                    DatuakKargatuDira = datuakKargatuDira
                };
            }
        }

        public static bool EskemaPrestDago()
        {
            try
            {
                using MySqlConnection konexioa = DatuBaseKonexioa.LortuKonexioa();
                foreach (string taula in OinarrizkoTaulak)
                {
                    using MySqlCommand komandoa = new MySqlCommand(
                        "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = @schema AND table_name = @taula;",
                        konexioa);
                    komandoa.Parameters.AddWithValue("@schema", DatuBaseKonexioa.LortuDatuBaseIzena());
                    komandoa.Parameters.AddWithValue("@taula", taula);

                    object? emaitza = komandoa.ExecuteScalar();
                    if (Convert.ToInt32(emaitza) == 0)
                    {
                        return false;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool HasierakoDatuakBeharDira()
        {
            if (!EskemaPrestDago())
            {
                return false;
            }

            using MySqlConnection konexioa = DatuBaseKonexioa.LortuKonexioa();
            using MySqlCommand komandoa = new MySqlCommand("SELECT COUNT(*) FROM rolak;", konexioa);
            object? emaitza = komandoa.ExecuteScalar();
            return Convert.ToInt32(emaitza) == 0;
        }

        private static void ZiurtatuZerbitzariaEskuragarriDago()
        {
            using MySqlConnection konexioa = DatuBaseKonexioa.LortuKonexioa(false);
            using MySqlCommand komandoa = new MySqlCommand("SELECT 1;", konexioa);
            komandoa.ExecuteScalar();
        }

        private static void ExekutatuEskemaSqlak()
        {
            string sqlKarpeta = Path.Combine(AppContext.BaseDirectory, "sql");

            ExekutatuScripta(Path.Combine(sqlKarpeta, "GOsasun_DB.sql"), false);
            ExekutatuScripta(Path.Combine(sqlKarpeta, "GOsasun_DB_trigger.sql"), true);
            ExekutatuScripta(Path.Combine(sqlKarpeta, "GOsasun_DB_bistak.sql"), true, aukerakoa: true);
            ExekutatuScripta(Path.Combine(sqlKarpeta, "GOsasun_DB_indizeak.sql"), true, aukerakoa: true);
        }

        private static void ExekutatuHasierakoDatuak()
        {
            string sqlKarpeta = Path.Combine(AppContext.BaseDirectory, "sql");
            ExekutatuScripta(Path.Combine(sqlKarpeta, "GOsasun_DB_data.sql"), true);
        }

        private static void ExekutatuScripta(string fitxategia, bool datuBasearekin, bool aukerakoa = false)
        {
            if (!File.Exists(fitxategia))
            {
                if (aukerakoa)
                {
                    return;
                }

                throw new FileNotFoundException($"Ez da SQL fitxategia aurkitu: {fitxategia}");
            }

            string edukia = PrestatuScriptarenEdukia(File.ReadAllText(fitxategia), DatuBaseKonexioa.LortuDatuBaseIzena());

            using MySqlConnection konexioa = DatuBaseKonexioa.LortuKonexioa(datuBasearekin);
            foreach (string agindua in BanatuSqlAginduak(edukia))
            {
                using MySqlCommand komandoa = new MySqlCommand(agindua, konexioa);
                komandoa.ExecuteNonQuery();
            }
        }

        private static string PrestatuScriptarenEdukia(string edukia, string datuBaseIzena)
        {
            string ihesduna = datuBaseIzena.Replace("`", "``");
            string emaitza = Regex.Replace(
                edukia,
                @"CREATE\s+DATABASE\s+IF\s+NOT\s+EXISTS\s+`?[^\s;`]+`?",
                $"CREATE DATABASE IF NOT EXISTS `{ihesduna}`",
                RegexOptions.IgnoreCase);

            emaitza = Regex.Replace(
                emaitza,
                @"USE\s+`?[^\s;`]+`?",
                $"USE `{ihesduna}`",
                RegexOptions.IgnoreCase);

            return emaitza;
        }

        private static IEnumerable<string> BanatuSqlAginduak(string edukia)
        {
            using StringReader irakurgailua = new StringReader(edukia.Replace("\r\n", "\n"));
            StringBuilder metatua = new StringBuilder();
            string unekoMugatzailea = ";";
            string? lerroa;

            while ((lerroa = irakurgailua.ReadLine()) != null)
            {
                string garbitua = lerroa.Trim();

                if (string.IsNullOrWhiteSpace(garbitua) && metatua.Length == 0)
                {
                    continue;
                }

                if (garbitua.StartsWith("--", StringComparison.Ordinal))
                {
                    continue;
                }

                if (garbitua.StartsWith("DELIMITER ", StringComparison.OrdinalIgnoreCase))
                {
                    unekoMugatzailea = garbitua.Substring("DELIMITER ".Length).Trim();
                    continue;
                }

                metatua.AppendLine(lerroa);

                if (AmaitzenDaMugatzailearekin(metatua, unekoMugatzailea))
                {
                    string agindua = KenduAmaierakoMugatzailea(metatua.ToString(), unekoMugatzailea).Trim();
                    if (!string.IsNullOrWhiteSpace(agindua))
                    {
                        yield return agindua;
                    }

                    metatua.Clear();
                }
            }

            string hondarra = metatua.ToString().Trim();
            if (!string.IsNullOrWhiteSpace(hondarra))
            {
                yield return hondarra;
            }
        }

        private static bool AmaitzenDaMugatzailearekin(StringBuilder edukia, string mugatzailea)
        {
            string testua = edukia.ToString().TrimEnd();
            return testua.EndsWith(mugatzailea, StringComparison.Ordinal);
        }

        private static string KenduAmaierakoMugatzailea(string edukia, string mugatzailea)
        {
            string testua = edukia.TrimEnd();
            return testua.EndsWith(mugatzailea, StringComparison.Ordinal)
                ? testua.Substring(0, testua.Length - mugatzailea.Length)
                : testua;
        }
    }
}