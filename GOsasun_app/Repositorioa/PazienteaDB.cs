using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Repositorioa
{
    public class PazienteaDB
    {
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

        private static bool DesaktibatuErabiltzailea(int id)
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

        public bool EzabatuPazientea(int id)
        {
            return DesaktibatuErabiltzailea(id);
        }

        public bool EguneratuPazientea(Pazientea p)
        {
            return EguneratuPazientea(p, Array.Empty<int>());
        }

        public bool EguneratuPazientea(Pazientea p, IEnumerable<int> osasunLangileIds)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            using (var transakzioa = konexioa.BeginTransaction())
            {
                try
                {
                    string q1 = @"UPDATE erabiltzaileak SET 
                                    email = @email, 
                                    pasahitza = @pasahitza,
                                    hizkuntza = @hizkuntza,
                                    izena = @izena, 
                                    abizenak = @abizenak, 
                                    nan = @nan, 
                                    jaiotze_data = @jaiotze, 
                                    telefonoa = @telefonoa, 
                                    helbidea = @helbidea, 
                                    herria = @herria, 
                                    posta_kodea = @posta,
                                    irudia = @irudia
                                  WHERE id = @id";
                    using (var cmd1 = new MySqlCommand(q1, konexioa, transakzioa))
                    {
                        cmd1.Parameters.AddWithValue("@id", p.Id);
                        cmd1.Parameters.AddWithValue("@email", p.Emaila);
                        cmd1.Parameters.AddWithValue("@pasahitza", p.Pasahitza);
                        cmd1.Parameters.AddWithValue("@hizkuntza", p.Hizkuntza);
                        cmd1.Parameters.AddWithValue("@izena", p.Izena);
                        cmd1.Parameters.AddWithValue("@abizenak", p.Abizenak);
                        cmd1.Parameters.AddWithValue("@nan", p.Nan);
                        cmd1.Parameters.AddWithValue("@jaiotze", p.JaiotzeData);
                        cmd1.Parameters.AddWithValue("@telefonoa", (object?)p.Telefonoa ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@helbidea", (object?)p.Helbidea ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@herria", (object?)p.Herria ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@posta", (object?)p.PostaKodea ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@irudia", string.IsNullOrWhiteSpace(p.Irudia) ? "img/lehenetsia.png" : p.Irudia);
                        cmd1.ExecuteNonQuery();
                    }

                    string q2 = @"UPDATE pazienteak SET 
                                    sexua = @sexua, 
                                    odol_taldea = @odol, 
                                    azken_altuera = @altuera,
                                    azken_pisua = @pisua,
                                    egoera_klinikoa = @egoera
                                  WHERE id = @id";
                    using (var cmd2 = new MySqlCommand(q2, konexioa, transakzioa))
                    {
                        cmd2.Parameters.AddWithValue("@id", p.Id);
                        cmd2.Parameters.AddWithValue("@sexua", p.Sexua);
                        cmd2.Parameters.AddWithValue("@odol", (object?)p.OdolTaldea ?? DBNull.Value);
                        cmd2.Parameters.AddWithValue("@altuera", (object?)p.AzkenAltuera ?? DBNull.Value);
                        cmd2.Parameters.AddWithValue("@pisua", (object?)p.AzkenPisua ?? DBNull.Value);
                        cmd2.Parameters.AddWithValue("@egoera", p.EgoeraKlinikoa);
                        cmd2.ExecuteNonQuery();
                    }

                    using (var ezabatu = new MySqlCommand("DELETE FROM pazientek_langileak WHERE paziente_id = @pazienteId", konexioa, transakzioa))
                    {
                        ezabatu.Parameters.AddWithValue("@pazienteId", p.Id);
                        ezabatu.ExecuteNonQuery();
                    }

                    foreach (int langileId in osasunLangileIds.Distinct())
                    {
                        using (var txertatu = new MySqlCommand("INSERT INTO pazientek_langileak (paziente_id, langile_id) VALUES (@pazienteId, @langileId)", konexioa, transakzioa))
                        {
                            txertatu.Parameters.AddWithValue("@pazienteId", p.Id);
                            txertatu.Parameters.AddWithValue("@langileId", langileId);
                            txertatu.ExecuteNonQuery();
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