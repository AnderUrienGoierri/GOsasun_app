using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Repositorioa
{
    public class ErabiltzaileDB
    {
        private const int OsasunLangileaRolId = 1;
        private const int PazienteaRolId = 2;
        private const int HarreraRolId = 3;

        public Erabiltzailea? Login(string emaila, string pasahitza)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    SELECT e.id, e.izena, e.abizenak, e.email, e.rol_id, r.izena as rol_izena
                    FROM erabiltzaileak e
                    JOIN rolak r ON e.rol_id = r.id
                    WHERE e.email = @emaila
                    AND e.pasahitza = @pasahitza
                    AND e.aktibo = 1";

                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    komandoa.Parameters.AddWithValue("@emaila", emaila);
                    komandoa.Parameters.AddWithValue("@pasahitza", pasahitza);

                    using (var irakurlea = komandoa.ExecuteReader())
                    {
                        if (irakurlea.Read())
                        {
                            int id = irakurlea.GetInt32("id");
                            string izena = DatuBaseTestua.Zuzendu(irakurlea.GetString("izena"));
                            string abizena = DatuBaseTestua.Zuzendu(irakurlea.GetString("abizenak"));
                            string email = DatuBaseTestua.Zuzendu(irakurlea.GetString("email"));
                            int rolId = irakurlea.GetInt32("rol_id");
                            string rolIzena = NormalizatuRolIzena(irakurlea.GetString("rol_izena"));

                            if (rolId == PazienteaRolId || rolIzena == "pazientea")
                            {
                                return new Pazientea { Id = id, Izena = izena, Abizenak = abizena, Emaila = email, RolId = rolId };
                            }

                            if (rolId == OsasunLangileaRolId || rolIzena == "osasunlangilea" || rolIzena == "medikua")
                            {
                                return new OsasunLangilea { Id = id, Izena = izena, Abizenak = abizena, Emaila = email, RolId = rolId };
                            }

                            return new HarrerakoLangilea { Id = id, Izena = izena, Abizenak = abizena, Emaila = email, RolId = rolId == 0 ? HarreraRolId : rolId };
                        }
                    }
                }
            }
            return null;
        }

        private static string NormalizatuRolIzena(string rolIzena)
        {
            return rolIzena.Replace(" ", string.Empty).Trim().ToLowerInvariant();
        }

        public List<Pazientea> LortuLangilearenPazienteak(int langileId, string? bilatzailea = null, string? egoeraFiltroa = null)
        {
            List<Pazientea> pazienteak = new List<Pazientea>();

            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    SELECT e.id, e.email, e.pasahitza, e.rol_id, e.aktibo, e.sortze_data,
                        e.nan, e.izena, e.abizenak, e.jaiotze_data, e.telefonoa, e.helbidea, e.herria, e.posta_kodea, e.irudia,
                            p.sexua, p.odol_taldea, p.azken_altuera, p.azken_pisua, p.egoera_klinikoa
                    FROM pazientek_langileak pl
                    JOIN erabiltzaileak e ON pl.paziente_id = e.id
                    LEFT JOIN pazienteak p ON e.id = p.id
                                        WHERE pl.langile_id = @langileId AND e.aktibo = 1
                                            AND (@egoeraFiltroa IS NULL OR COALESCE(p.egoera_klinikoa, 'Alta') = @egoeraFiltroa)";

                if (!string.IsNullOrEmpty(bilatzailea))
                {
                    query += " AND (e.izena LIKE @testua OR e.abizenak LIKE @testua OR e.nan LIKE @testua)";
                }

                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    komandoa.Parameters.AddWithValue("@langileId", langileId);
                    komandoa.Parameters.AddWithValue("@egoeraFiltroa", (object?)NormalizatuEgoeraFiltroa(egoeraFiltroa) ?? DBNull.Value);
                    if (!string.IsNullOrEmpty(bilatzailea))
                    {
                        komandoa.Parameters.AddWithValue("@testua", "%" + bilatzailea + "%");
                    }

                    using (var irakurlea = komandoa.ExecuteReader())
                    {
                        while (irakurlea.Read())
                        {
                            pazienteak.Add(new Pazientea
                            {
                                Id = irakurlea.GetInt32("id"),
                                Emaila = DatuBaseTestua.Zuzendu(irakurlea.GetString("email")),
                                Pasahitza = irakurlea.GetString("pasahitza"),
                                RolId = irakurlea.GetInt32("rol_id"),
                                Aktibo = irakurlea.GetBoolean("aktibo"),
                                SortzeData = irakurlea.GetDateTime("sortze_data"),
                                Nan = DatuBaseTestua.Zuzendu(irakurlea.GetString("nan")),
                                Izena = DatuBaseTestua.Zuzendu(irakurlea.GetString("izena")),
                                Abizenak = DatuBaseTestua.Zuzendu(irakurlea.GetString("abizenak")),
                                JaiotzeData = irakurlea.IsDBNull(irakurlea.GetOrdinal("jaiotze_data")) ? DateTime.MinValue : irakurlea.GetDateTime("jaiotze_data"),
                                Telefonoa = irakurlea.IsDBNull(irakurlea.GetOrdinal("telefonoa")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("telefonoa")),
                                Helbidea = irakurlea.IsDBNull(irakurlea.GetOrdinal("helbidea")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("helbidea")),
                                Herria = irakurlea.IsDBNull(irakurlea.GetOrdinal("herria")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("herria")),
                                PostaKodea = irakurlea.IsDBNull(irakurlea.GetOrdinal("posta_kodea")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("posta_kodea")),
                                OdolTaldea = irakurlea.IsDBNull(irakurlea.GetOrdinal("odol_taldea")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("odol_taldea")),
                                Sexua = irakurlea.IsDBNull(irakurlea.GetOrdinal("sexua")) ? "-" : DatuBaseTestua.Zuzendu(irakurlea.GetString("sexua")),
                                AzkenAltuera = irakurlea.IsDBNull(irakurlea.GetOrdinal("azken_altuera")) ? (decimal?)null : irakurlea.GetDecimal("azken_altuera"),
                                AzkenPisua = irakurlea.IsDBNull(irakurlea.GetOrdinal("azken_pisua")) ? (decimal?)null : irakurlea.GetDecimal("azken_pisua"),
                                EgoeraKlinikoa = irakurlea.IsDBNull(irakurlea.GetOrdinal("egoera_klinikoa")) ? "Alta" : DatuBaseTestua.Zuzendu(irakurlea.GetString("egoera_klinikoa")),
                                Irudia = irakurlea.IsDBNull(irakurlea.GetOrdinal("irudia")) ? "img/lehenetsia.png" : DatuBaseTestua.Zuzendu(irakurlea.GetString("irudia"))
                            });
                        }
                    }
                }
            }
            return pazienteak;
        }

                public List<Pazientea> LortuGuztiakPazienteak(string? bilatzailea = null, string? egoeraFiltroa = null)
        {
            List<Pazientea> pazienteak = new List<Pazientea>();
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"SELECT e.id, e.email, e.pasahitza, e.rol_id, e.aktibo, e.sortze_data,
                                                                                e.nan, e.izena, e.abizenak, e.jaiotze_data, e.telefonoa, e.helbidea, e.herria, e.posta_kodea, e.irudia,
                                        p.sexua, p.odol_taldea, p.azken_altuera, p.azken_pisua, p.egoera_klinikoa
                                 FROM erabiltzaileak e
                                 LEFT JOIN pazienteak p ON e.id = p.id
                                                                 WHERE e.rol_id = 2 AND e.aktibo = 1
                                                                     AND (@egoeraFiltroa IS NULL OR COALESCE(p.egoera_klinikoa, 'Alta') = @egoeraFiltroa)";

                if (!string.IsNullOrEmpty(bilatzailea))
                {
                    query += " AND (e.izena LIKE @testua OR e.abizenak LIKE @testua OR e.nan LIKE @testua)";
                }

                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    komandoa.Parameters.AddWithValue("@egoeraFiltroa", (object?)NormalizatuEgoeraFiltroa(egoeraFiltroa) ?? DBNull.Value);
                    if (!string.IsNullOrEmpty(bilatzailea))
                    {
                        komandoa.Parameters.AddWithValue("@testua", "%" + bilatzailea + "%");
                    }

                    using (var irakurlea = komandoa.ExecuteReader())
                    {
                        while (irakurlea.Read())
                        {
                            pazienteak.Add(new Pazientea
                            {
                                Id = irakurlea.GetInt32("id"),
                                Emaila = DatuBaseTestua.Zuzendu(irakurlea.GetString("email")),
                                Pasahitza = irakurlea.IsDBNull(irakurlea.GetOrdinal("pasahitza")) ? string.Empty : irakurlea.GetString("pasahitza"),
                                RolId = irakurlea.GetInt32("rol_id"),
                                Aktibo = irakurlea.GetBoolean("aktibo"),
                                SortzeData = irakurlea.GetDateTime("sortze_data"),
                                Izena = DatuBaseTestua.Zuzendu(irakurlea.GetString("izena")),
                                Abizenak = DatuBaseTestua.Zuzendu(irakurlea.GetString("abizenak")),
                                Nan = DatuBaseTestua.Zuzendu(irakurlea.GetString("nan")),
                                JaiotzeData = irakurlea.IsDBNull(irakurlea.GetOrdinal("jaiotze_data")) ? DateTime.MinValue : irakurlea.GetDateTime("jaiotze_data"),
                                Telefonoa = irakurlea.IsDBNull(irakurlea.GetOrdinal("telefonoa")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("telefonoa")),
                                Helbidea = irakurlea.IsDBNull(irakurlea.GetOrdinal("helbidea")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("helbidea")),
                                Herria = irakurlea.IsDBNull(irakurlea.GetOrdinal("herria")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("herria")),
                                PostaKodea = irakurlea.IsDBNull(irakurlea.GetOrdinal("posta_kodea")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("posta_kodea")),
                                Sexua = irakurlea.IsDBNull(irakurlea.GetOrdinal("sexua")) ? "-" : DatuBaseTestua.Zuzendu(irakurlea.GetString("sexua")),
                                OdolTaldea = irakurlea.IsDBNull(irakurlea.GetOrdinal("odol_taldea")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("odol_taldea")),
                                AzkenAltuera = irakurlea.IsDBNull(irakurlea.GetOrdinal("azken_altuera")) ? (decimal?)null : irakurlea.GetDecimal("azken_altuera"),
                                AzkenPisua = irakurlea.IsDBNull(irakurlea.GetOrdinal("azken_pisua")) ? (decimal?)null : irakurlea.GetDecimal("azken_pisua"),
                                EgoeraKlinikoa = irakurlea.IsDBNull(irakurlea.GetOrdinal("egoera_klinikoa")) ? "Alta" : DatuBaseTestua.Zuzendu(irakurlea.GetString("egoera_klinikoa"))
                            });
                        }
                    }
                }
            }
            return pazienteak;
        }

        private static string? NormalizatuEgoeraFiltroa(string? egoeraFiltroa)
        {
            if (string.IsNullOrWhiteSpace(egoeraFiltroa)) return null;

            return egoeraFiltroa.Trim().ToLowerInvariant() switch
            {
                "alta" or "altan" => "Alta",
                "baja" or "bajan" => "Baja",
                _ => null
            };
        }

        public List<OsasunLangilea> LortuGuztiakOsasunLangileak()
        {
            List<OsasunLangilea> langileak = new List<OsasunLangilea>();
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"SELECT e.id, e.email, e.izena, e.abizenak
                                 FROM osasun_langileak ol
                                 JOIN erabiltzaileak e ON ol.id = e.id
                                 WHERE e.aktibo = 1";
                using (var komandoa = new MySqlCommand(query, konexioa))
                using (var irakurlea = komandoa.ExecuteReader())
                {
                    while (irakurlea.Read())
                    {
                        langileak.Add(new OsasunLangilea
                        {
                            Id = irakurlea.GetInt32("id"),
                            Emaila = DatuBaseTestua.Zuzendu(irakurlea.GetString("email")),
                            Izena = DatuBaseTestua.Zuzendu(irakurlea.GetString("izena")),
                            Abizenak = DatuBaseTestua.Zuzendu(irakurlea.GetString("abizenak"))
                        });
                    }
                }
            }
            return langileak;
        }

        public Pazientea? LortuPazientea(int pazienteId)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    SELECT e.id, e.email, e.pasahitza, e.rol_id, e.aktibo, e.sortze_data,
                           e.nan, e.izena, e.abizenak, e.jaiotze_data, e.telefonoa, e.helbidea, e.herria, e.posta_kodea, e.irudia,
                           p.sexua, p.odol_taldea, p.azken_altuera, p.azken_pisua, p.egoera_klinikoa
                    FROM erabiltzaileak e
                    LEFT JOIN pazienteak p ON e.id = p.id
                    WHERE e.id = @id AND e.rol_id = 2 AND e.aktibo = 1";

                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    komandoa.Parameters.AddWithValue("@id", pazienteId);

                    using (var irakurlea = komandoa.ExecuteReader())
                    {
                        if (!irakurlea.Read()) return null;

                        return new Pazientea
                        {
                            Id = irakurlea.GetInt32("id"),
                            Emaila = DatuBaseTestua.Zuzendu(irakurlea.GetString("email")),
                            Pasahitza = irakurlea.IsDBNull(irakurlea.GetOrdinal("pasahitza")) ? string.Empty : irakurlea.GetString("pasahitza"),
                            RolId = irakurlea.GetInt32("rol_id"),
                            Aktibo = irakurlea.GetBoolean("aktibo"),
                            SortzeData = irakurlea.GetDateTime("sortze_data"),
                            Nan = DatuBaseTestua.Zuzendu(irakurlea.GetString("nan")),
                            Izena = DatuBaseTestua.Zuzendu(irakurlea.GetString("izena")),
                            Abizenak = DatuBaseTestua.Zuzendu(irakurlea.GetString("abizenak")),
                            JaiotzeData = irakurlea.IsDBNull(irakurlea.GetOrdinal("jaiotze_data")) ? DateTime.MinValue : irakurlea.GetDateTime("jaiotze_data"),
                            Telefonoa = irakurlea.IsDBNull(irakurlea.GetOrdinal("telefonoa")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("telefonoa")),
                            Helbidea = irakurlea.IsDBNull(irakurlea.GetOrdinal("helbidea")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("helbidea")),
                            Herria = irakurlea.IsDBNull(irakurlea.GetOrdinal("herria")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("herria")),
                            PostaKodea = irakurlea.IsDBNull(irakurlea.GetOrdinal("posta_kodea")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("posta_kodea")),
                            Sexua = irakurlea.IsDBNull(irakurlea.GetOrdinal("sexua")) ? "-" : DatuBaseTestua.Zuzendu(irakurlea.GetString("sexua")),
                            OdolTaldea = irakurlea.IsDBNull(irakurlea.GetOrdinal("odol_taldea")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("odol_taldea")),
                            AzkenAltuera = irakurlea.IsDBNull(irakurlea.GetOrdinal("azken_altuera")) ? (decimal?)null : irakurlea.GetDecimal("azken_altuera"),
                            AzkenPisua = irakurlea.IsDBNull(irakurlea.GetOrdinal("azken_pisua")) ? (decimal?)null : irakurlea.GetDecimal("azken_pisua"),
                            EgoeraKlinikoa = irakurlea.IsDBNull(irakurlea.GetOrdinal("egoera_klinikoa")) ? "Alta" : DatuBaseTestua.Zuzendu(irakurlea.GetString("egoera_klinikoa")),
                            Irudia = irakurlea.IsDBNull(irakurlea.GetOrdinal("irudia")) ? "img/lehenetsia.png" : DatuBaseTestua.Zuzendu(irakurlea.GetString("irudia"))
                        };
                    }
                }
            }
        }

        public OsasunLangilea? LortuOsasunLangilea(int osasunLangileId)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    SELECT e.id, e.email, e.pasahitza, e.rol_id, e.aktibo, e.sortze_data, e.hizkuntza,
                           e.nan, e.izena, e.abizenak, e.jaiotze_data, e.telefonoa, e.helbidea, e.herria, e.posta_kodea, e.irudia,
                           ol.elkargokide_zenbakia, ol.espezialitatea, ol.kontsulta, ol.lanaldia
                    FROM erabiltzaileak e
                    JOIN osasun_langileak ol ON ol.id = e.id
                    WHERE e.id = @id AND e.aktibo = 1";

                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    komandoa.Parameters.AddWithValue("@id", osasunLangileId);
                    using (var irakurlea = komandoa.ExecuteReader())
                    {
                        if (!irakurlea.Read()) return null;

                        return new OsasunLangilea
                        {
                            Id = irakurlea.GetInt32("id"),
                            Emaila = DatuBaseTestua.Zuzendu(irakurlea.GetString("email")),
                            Pasahitza = irakurlea.IsDBNull(irakurlea.GetOrdinal("pasahitza")) ? string.Empty : irakurlea.GetString("pasahitza"),
                            RolId = irakurlea.GetInt32("rol_id"),
                            Aktibo = irakurlea.GetBoolean("aktibo"),
                            SortzeData = irakurlea.GetDateTime("sortze_data"),
                            Hizkuntza = irakurlea.IsDBNull(irakurlea.GetOrdinal("hizkuntza")) ? "Euskara" : DatuBaseTestua.Zuzendu(irakurlea.GetString("hizkuntza")),
                            Nan = DatuBaseTestua.Zuzendu(irakurlea.GetString("nan")),
                            Izena = DatuBaseTestua.Zuzendu(irakurlea.GetString("izena")),
                            Abizenak = DatuBaseTestua.Zuzendu(irakurlea.GetString("abizenak")),
                            JaiotzeData = irakurlea.IsDBNull(irakurlea.GetOrdinal("jaiotze_data")) ? DateTime.MinValue : irakurlea.GetDateTime("jaiotze_data"),
                            Telefonoa = irakurlea.IsDBNull(irakurlea.GetOrdinal("telefonoa")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("telefonoa")),
                            Helbidea = irakurlea.IsDBNull(irakurlea.GetOrdinal("helbidea")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("helbidea")),
                            Herria = irakurlea.IsDBNull(irakurlea.GetOrdinal("herria")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("herria")),
                            PostaKodea = irakurlea.IsDBNull(irakurlea.GetOrdinal("posta_kodea")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("posta_kodea")),
                            Irudia = irakurlea.IsDBNull(irakurlea.GetOrdinal("irudia")) ? "img/lehenetsia.png" : DatuBaseTestua.Zuzendu(irakurlea.GetString("irudia")),
                            ElkargokideZenbakia = irakurlea.IsDBNull(irakurlea.GetOrdinal("elkargokide_zenbakia")) ? string.Empty : DatuBaseTestua.Zuzendu(irakurlea.GetString("elkargokide_zenbakia")),
                            Espezialitatea = irakurlea.IsDBNull(irakurlea.GetOrdinal("espezialitatea")) ? string.Empty : DatuBaseTestua.Zuzendu(irakurlea.GetString("espezialitatea")),
                            Kontsulta = irakurlea.IsDBNull(irakurlea.GetOrdinal("kontsulta")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("kontsulta")),
                            Lanaldia = irakurlea.IsDBNull(irakurlea.GetOrdinal("lanaldia")) ? "Osoa" : DatuBaseTestua.Zuzendu(irakurlea.GetString("lanaldia"))
                        };
                    }
                }
            }
        }

        public List<HarrerakoLangilea> LortuGuztiakHarrerakoak()
        {
            List<HarrerakoLangilea> harrerakoak = new List<HarrerakoLangilea>();
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"SELECT e.id, e.email, e.izena, e.abizenak
                                 FROM harrerako_langileak hl
                                 JOIN erabiltzaileak e ON hl.id = e.id
                                 WHERE e.aktibo = 1";
                using (var komandoa = new MySqlCommand(query, konexioa))
                using (var irakurlea = komandoa.ExecuteReader())
                {
                    while (irakurlea.Read())
                    {
                        harrerakoak.Add(new HarrerakoLangilea
                        {
                            Id = irakurlea.GetInt32("id"),
                            Emaila = DatuBaseTestua.Zuzendu(irakurlea.GetString("email")),
                            Izena = DatuBaseTestua.Zuzendu(irakurlea.GetString("izena")),
                            Abizenak = DatuBaseTestua.Zuzendu(irakurlea.GetString("abizenak"))
                        });
                    }
                }
            }
            return harrerakoak;
        }

        public HarrerakoLangilea? LortuHarrerakoa(int harrerakoaId)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    SELECT e.id, e.email, e.pasahitza, e.rol_id, e.aktibo, e.sortze_data, e.hizkuntza,
                           e.nan, e.izena, e.abizenak, e.jaiotze_data, e.telefonoa, e.helbidea, e.herria, e.posta_kodea, e.irudia,
                           hl.txanda
                    FROM erabiltzaileak e
                    JOIN harrerako_langileak hl ON hl.id = e.id
                    WHERE e.id = @id AND e.aktibo = 1";

                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    komandoa.Parameters.AddWithValue("@id", harrerakoaId);
                    using (var irakurlea = komandoa.ExecuteReader())
                    {
                        if (!irakurlea.Read()) return null;

                        return new HarrerakoLangilea
                        {
                            Id = irakurlea.GetInt32("id"),
                            Emaila = DatuBaseTestua.Zuzendu(irakurlea.GetString("email")),
                            Pasahitza = irakurlea.IsDBNull(irakurlea.GetOrdinal("pasahitza")) ? string.Empty : irakurlea.GetString("pasahitza"),
                            RolId = irakurlea.GetInt32("rol_id"),
                            Aktibo = irakurlea.GetBoolean("aktibo"),
                            SortzeData = irakurlea.GetDateTime("sortze_data"),
                            Hizkuntza = irakurlea.IsDBNull(irakurlea.GetOrdinal("hizkuntza")) ? "Euskara" : DatuBaseTestua.Zuzendu(irakurlea.GetString("hizkuntza")),
                            Nan = irakurlea.IsDBNull(irakurlea.GetOrdinal("nan")) ? string.Empty : DatuBaseTestua.Zuzendu(irakurlea.GetString("nan")),
                            Izena = DatuBaseTestua.Zuzendu(irakurlea.GetString("izena")),
                            Abizenak = DatuBaseTestua.Zuzendu(irakurlea.GetString("abizenak")),
                            JaiotzeData = irakurlea.IsDBNull(irakurlea.GetOrdinal("jaiotze_data")) ? DateTime.MinValue : irakurlea.GetDateTime("jaiotze_data"),
                            Telefonoa = irakurlea.IsDBNull(irakurlea.GetOrdinal("telefonoa")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("telefonoa")),
                            Helbidea = irakurlea.IsDBNull(irakurlea.GetOrdinal("helbidea")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("helbidea")),
                            Herria = irakurlea.IsDBNull(irakurlea.GetOrdinal("herria")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("herria")),
                            PostaKodea = irakurlea.IsDBNull(irakurlea.GetOrdinal("posta_kodea")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("posta_kodea")),
                            Irudia = irakurlea.IsDBNull(irakurlea.GetOrdinal("irudia")) ? "img/lehenetsia.png" : DatuBaseTestua.Zuzendu(irakurlea.GetString("irudia")),
                            Txanda = irakurlea.IsDBNull(irakurlea.GetOrdinal("txanda")) ? "Goizez" : DatuBaseTestua.Zuzendu(irakurlea.GetString("txanda"))
                        };
                    }
                }
            }
        }

        public List<OsasunLangilea> LortuPazientearenOsasunLangileak(int pazienteId)
        {
            List<OsasunLangilea> langileak = new List<OsasunLangilea>();

            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    SELECT e.id, e.email, e.izena, e.abizenak,
                           ol.espezialitatea, ol.kontsulta, ol.lanaldia, ol.elkargokide_zenbakia
                    FROM pazientek_langileak pl
                    JOIN erabiltzaileak e ON e.id = pl.langile_id
                    JOIN osasun_langileak ol ON ol.id = e.id
                    WHERE pl.paziente_id = @pazienteId AND e.aktibo = 1
                    ORDER BY e.abizenak, e.izena";

                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    komandoa.Parameters.AddWithValue("@pazienteId", pazienteId);
                    using (var irakurlea = komandoa.ExecuteReader())
                    {
                        while (irakurlea.Read())
                        {
                            langileak.Add(new OsasunLangilea
                            {
                                Id = irakurlea.GetInt32("id"),
                                Emaila = DatuBaseTestua.Zuzendu(irakurlea.GetString("email")),
                                Izena = DatuBaseTestua.Zuzendu(irakurlea.GetString("izena")),
                                Abizenak = DatuBaseTestua.Zuzendu(irakurlea.GetString("abizenak")),
                                Espezialitatea = irakurlea.IsDBNull(irakurlea.GetOrdinal("espezialitatea")) ? string.Empty : DatuBaseTestua.Zuzendu(irakurlea.GetString("espezialitatea")),
                                Kontsulta = irakurlea.IsDBNull(irakurlea.GetOrdinal("kontsulta")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("kontsulta")),
                                Lanaldia = irakurlea.IsDBNull(irakurlea.GetOrdinal("lanaldia")) ? "Osoa" : DatuBaseTestua.Zuzendu(irakurlea.GetString("lanaldia")),
                                ElkargokideZenbakia = irakurlea.IsDBNull(irakurlea.GetOrdinal("elkargokide_zenbakia")) ? string.Empty : DatuBaseTestua.Zuzendu(irakurlea.GetString("elkargokide_zenbakia"))
                            });
                        }
                    }
                }
            }

            return langileak;
        }

        public bool SortuPazientea(Pazientea p)
        {
            return SortuPazientea(p, Array.Empty<int>(), null);
        }

        public bool SortuPazientea(Pazientea p, IEnumerable<int>? osasunLangileIds, string? irudiBidea)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            using (var transakzioa = konexioa.BeginTransaction())
            {
                try
                {
                    string irudiBalioa = string.IsNullOrWhiteSpace(irudiBidea) ? "img/lehenetsia.png" : irudiBidea;
                    string q1 = @"INSERT INTO erabiltzaileak (email, pasahitza, rol_id, aktibo, hizkuntza, nan, izena, abizenak, jaiotze_data, telefonoa, helbidea, herria, posta_kodea, irudia) 
                                  VALUES (@email, @pass, 2, 1, @hizkuntza, @nan, @izena, @abizenak, @jaiotze, @telefonoa, @helbidea, @herria, @posta, @irudia); 
                                  SELECT LAST_INSERT_ID();";
                    using (var cmd1 = new MySqlCommand(q1, konexioa, transakzioa))
                    {
                        cmd1.Parameters.AddWithValue("@email", p.Emaila);
                        cmd1.Parameters.AddWithValue("@pass", p.Pasahitza);
                        cmd1.Parameters.AddWithValue("@hizkuntza", p.Hizkuntza);
                        cmd1.Parameters.AddWithValue("@nan", p.Nan);
                        cmd1.Parameters.AddWithValue("@izena", p.Izena);
                        cmd1.Parameters.AddWithValue("@abizenak", p.Abizenak);
                        cmd1.Parameters.AddWithValue("@jaiotze", p.JaiotzeData);
                        cmd1.Parameters.AddWithValue("@telefonoa", (object?)p.Telefonoa ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@helbidea", (object?)p.Helbidea ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@herria", (object?)p.Herria ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@posta", (object?)p.PostaKodea ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@irudia", irudiBalioa);
                        int newId = Convert.ToInt32(cmd1.ExecuteScalar());

                        string q2 = @"INSERT INTO pazienteak (id, sexua, odol_taldea, azken_altuera, azken_pisua, egoera_klinikoa) 
                                    VALUES (@id, @sexua, @odol, @altuera, @pisua, 'Alta')";
                        using (var cmd2 = new MySqlCommand(q2, konexioa, transakzioa))
                        {
                            cmd2.Parameters.AddWithValue("@id", newId);
                            cmd2.Parameters.AddWithValue("@sexua", p.Sexua);
                            cmd2.Parameters.AddWithValue("@odol", (object?)p.OdolTaldea ?? DBNull.Value);
                            cmd2.Parameters.AddWithValue("@altuera", (object?)p.AzkenAltuera ?? DBNull.Value);
                            cmd2.Parameters.AddWithValue("@pisua", (object?)p.AzkenPisua ?? DBNull.Value);
                            cmd2.ExecuteNonQuery();
                        }

                        foreach (int langileId in (osasunLangileIds ?? Array.Empty<int>()).Distinct())
                        {
                            string q3 = @"INSERT INTO pazientek_langileak (paziente_id, langile_id) VALUES (@pazienteId, @langileId)";
                            using (var cmd3 = new MySqlCommand(q3, konexioa, transakzioa))
                            {
                                cmd3.Parameters.AddWithValue("@pazienteId", newId);
                                cmd3.Parameters.AddWithValue("@langileId", langileId);
                                cmd3.ExecuteNonQuery();
                            }
                        }
                    }
                    transakzioa.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transakzioa.Rollback();
                    Console.WriteLine($"Errorea: {ex.Message}");
                    return false;
                }
            }
        }

        public bool SortuOsasunLangilea(OsasunLangilea m)
        {
            return SortuOsasunLangilea(m, null);
        }

        public bool SortuOsasunLangilea(OsasunLangilea m, string? irudiBidea)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            using (var transakzioa = konexioa.BeginTransaction())
            {
                try
                {
                    string irudiBalioa = string.IsNullOrWhiteSpace(irudiBidea) ? "img/lehenetsia.png" : irudiBidea;
                    string q1 = @"INSERT INTO erabiltzaileak (email, pasahitza, rol_id, aktibo, hizkuntza, izena, abizenak, jaiotze_data, telefonoa, nan, irudia) 
                                  VALUES (@email, @pass, 1, 1, @hizkuntza, @izena, @abizenak, @jaiotze, @telefonoa, @nan, @irudia); 
                                  SELECT LAST_INSERT_ID();";
                    using (var cmd1 = new MySqlCommand(q1, konexioa, transakzioa))
                    {
                        cmd1.Parameters.AddWithValue("@email", m.Emaila);
                        cmd1.Parameters.AddWithValue("@pass", m.Pasahitza);
                        cmd1.Parameters.AddWithValue("@hizkuntza", m.Hizkuntza);
                        cmd1.Parameters.AddWithValue("@izena", m.Izena);
                        cmd1.Parameters.AddWithValue("@abizenak", m.Abizenak);
                        cmd1.Parameters.AddWithValue("@jaiotze", m.JaiotzeData);
                        cmd1.Parameters.AddWithValue("@telefonoa", (object?)m.Telefonoa ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@nan", m.Nan);
                        cmd1.Parameters.AddWithValue("@irudia", irudiBalioa);
                        int newId = Convert.ToInt32(cmd1.ExecuteScalar());

                        string q2 = @"INSERT INTO osasun_langileak 
                                    (id, elkargokide_zenbakia, espezialitatea, kontsulta, lanaldia) 
                                    VALUES (@id, @elkargokide, @espezialitatea, @kontsulta, @lanaldia)";
                        using (var cmd2 = new MySqlCommand(q2, konexioa, transakzioa))
                        {
                            cmd2.Parameters.AddWithValue("@id", newId);
                            cmd2.Parameters.AddWithValue("@elkargokide", m.ElkargokideZenbakia);
                            cmd2.Parameters.AddWithValue("@espezialitatea", m.Espezialitatea);
                            cmd2.Parameters.AddWithValue("@kontsulta", (object?)m.Kontsulta ?? DBNull.Value);
                            cmd2.Parameters.AddWithValue("@lanaldia", m.Lanaldia);
                            cmd2.ExecuteNonQuery();
                        }
                    }
                    transakzioa.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transakzioa.Rollback();
                    Console.WriteLine($"Errorea: {ex.Message}");
                    return false;
                }
            }
        }

        public bool SortuHarrerakoa(HarrerakoLangilea h)
        {
            return SortuHarrerakoa(h, null);
        }

        public bool SortuHarrerakoa(HarrerakoLangilea h, string? irudiBidea)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            using (var transakzioa = konexioa.BeginTransaction())
            {
                try
                {
                    string irudiBalioa = string.IsNullOrWhiteSpace(irudiBidea) ? "img/lehenetsia.png" : irudiBidea;
                    string q1 = @"INSERT INTO erabiltzaileak (email, pasahitza, rol_id, aktibo, hizkuntza, izena, abizenak, jaiotze_data, telefonoa, irudia) 
                                  VALUES (@email, @pass, 3, 1, @hizkuntza, @izena, @abizenak, @jaiotze, @telefonoa, @irudia); 
                                  SELECT LAST_INSERT_ID();";
                    using (var cmd1 = new MySqlCommand(q1, konexioa, transakzioa))
                    {
                        cmd1.Parameters.AddWithValue("@email", h.Emaila);
                        cmd1.Parameters.AddWithValue("@pass", h.Pasahitza);
                        cmd1.Parameters.AddWithValue("@hizkuntza", h.Hizkuntza);
                        cmd1.Parameters.AddWithValue("@izena", h.Izena);
                        cmd1.Parameters.AddWithValue("@abizenak", h.Abizenak);
                        cmd1.Parameters.AddWithValue("@jaiotze", h.JaiotzeData);
                        cmd1.Parameters.AddWithValue("@telefonoa", (object?)h.Telefonoa ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@irudia", irudiBalioa);
                        int newId = Convert.ToInt32(cmd1.ExecuteScalar());

                        string q2 = @"INSERT INTO harrerako_langileak (id, txanda) VALUES (@id, @txanda)";
                        using (var cmd2 = new MySqlCommand(q2, konexioa, transakzioa))
                        {
                            cmd2.Parameters.AddWithValue("@id", newId);
                            cmd2.Parameters.AddWithValue("@txanda", h.Txanda);
                            cmd2.ExecuteNonQuery();
                        }
                    }
                    transakzioa.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transakzioa.Rollback();
                    Console.WriteLine($"Errorea: {ex.Message}");
                    return false;
                }
            }
        }

        public bool EzabatuPazientea(int id)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = "UPDATE erabiltzaileak SET aktibo = 0 WHERE id = @id";
                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    komandoa.Parameters.AddWithValue("@id", id);
                    return komandoa.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool EguneratuPazientea(Pazientea p)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            using (var transakzioa = konexioa.BeginTransaction())
            {
                try
                {
                    string q1 = @"UPDATE erabiltzaileak SET 
                                    email = @email, 
                                    izena = @izena, 
                                    abizenak = @abizenak, 
                                    nan = @nan, 
                                    jaiotze_data = @jaiotze, 
                                    telefonoa = @telefonoa, 
                                    helbidea = @helbidea, 
                                    herria = @herria, 
                                    posta_kodea = @posta
                                  WHERE id = @id";
                    using (var cmd1 = new MySqlCommand(q1, konexioa, transakzioa))
                    {
                        cmd1.Parameters.AddWithValue("@id", p.Id);
                        cmd1.Parameters.AddWithValue("@email", p.Emaila);
                        cmd1.Parameters.AddWithValue("@izena", p.Izena);
                        cmd1.Parameters.AddWithValue("@abizenak", p.Abizenak);
                        cmd1.Parameters.AddWithValue("@nan", p.Nan);
                        cmd1.Parameters.AddWithValue("@jaiotze", p.JaiotzeData);
                        cmd1.Parameters.AddWithValue("@telefonoa", (object?)p.Telefonoa ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@helbidea", (object?)p.Helbidea ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@herria", (object?)p.Herria ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@posta", (object?)p.PostaKodea ?? DBNull.Value);
                        cmd1.ExecuteNonQuery();
                    }

                    string q2 = @"UPDATE pazienteak SET 
                                    sexua = @sexua, 
                                    odol_taldea = @odol, 
                                    egoera_klinikoa = @egoera
                                  WHERE id = @id";
                    using (var cmd2 = new MySqlCommand(q2, konexioa, transakzioa))
                    {
                        cmd2.Parameters.AddWithValue("@id", p.Id);
                        cmd2.Parameters.AddWithValue("@sexua", p.Sexua);
                        cmd2.Parameters.AddWithValue("@odol", (object?)p.OdolTaldea ?? DBNull.Value);
                        cmd2.Parameters.AddWithValue("@egoera", p.EgoeraKlinikoa);
                        cmd2.ExecuteNonQuery();
                    }

                    transakzioa.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transakzioa.Rollback();
                    Console.WriteLine($"Errorea: {ex.Message}");
                    return false;
                }
            }
        }

        public bool AldatuPazientearenEgoera(int pazienteId, string egoeraBerria)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = "UPDATE pazienteak SET egoera_klinikoa = @egoera WHERE id = @id";
                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    komandoa.Parameters.AddWithValue("@egoera", egoeraBerria);
                    komandoa.Parameters.AddWithValue("@id", pazienteId);
                    return komandoa.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool EsleituOsasunLangileakPazienteari(int pazienteId, IEnumerable<int> osasunLangileIds)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            using (var transakzioa = konexioa.BeginTransaction())
            {
                try
                {
                    using (var ezabatu = new MySqlCommand("DELETE FROM pazientek_langileak WHERE paziente_id = @pazienteId", konexioa, transakzioa))
                    {
                        ezabatu.Parameters.AddWithValue("@pazienteId", pazienteId);
                        ezabatu.ExecuteNonQuery();
                    }

                    foreach (int langileId in osasunLangileIds.Distinct())
                    {
                        using (var txertatu = new MySqlCommand("INSERT INTO pazientek_langileak (paziente_id, langile_id) VALUES (@pazienteId, @langileId)", konexioa, transakzioa))
                        {
                            txertatu.Parameters.AddWithValue("@pazienteId", pazienteId);
                            txertatu.Parameters.AddWithValue("@langileId", langileId);
                            txertatu.ExecuteNonQuery();
                        }
                    }

                    transakzioa.Commit();
                    return true;
                }
                catch
                {
                    transakzioa.Rollback();
                    return false;
                }
            }
        }
    }
}
