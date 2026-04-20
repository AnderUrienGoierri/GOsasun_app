using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Repositorioa
{
    public class OsasunLangileaDB
    {
        private static string? SortuBilatzaileLikeBalioa(string? bilatzailea)
        {
            return string.IsNullOrWhiteSpace(bilatzailea)
                ? null
                : $"%{bilatzailea.Trim()}%";
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

        public List<OsasunLangilea> LortuGuztiakOsasunLangileak(string? bilatzailea = null)
        {
            List<OsasunLangilea> langileak = new List<OsasunLangilea>();
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"SELECT e.id, e.email, e.pasahitza, e.rol_id, e.aktibo, e.sortze_data, e.hizkuntza,
                                        e.nan, e.izena, e.abizenak, e.jaiotze_data, e.telefonoa, e.helbidea, e.herria, e.posta_kodea, e.irudia,
                                        ol.elkargokide_zenbakia, ol.espezialitatea, ol.kontsulta, ol.lanaldia
                                 FROM osasun_langileak ol
                                 JOIN erabiltzaileak e ON ol.id = e.id
                                 WHERE e.aktibo = 1
                                   AND (@bilatzailea IS NULL
                                        OR e.izena LIKE @bilatzailea
                                        OR e.abizenak LIKE @bilatzailea
                                        OR COALESCE(e.nan, '') LIKE @bilatzailea
                                        OR CONCAT(e.izena, ' ', e.abizenak) LIKE @bilatzailea)
                                 ORDER BY e.abizenak, e.izena";
                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    komandoa.Parameters.AddWithValue("@bilatzailea", (object?)SortuBilatzaileLikeBalioa(bilatzailea) ?? DBNull.Value);
                    using (var irakurlea = komandoa.ExecuteReader())
                    {
                        while (irakurlea.Read())
                        {
                            langileak.Add(new OsasunLangilea
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
                                ElkargokideZenbakia = irakurlea.IsDBNull(irakurlea.GetOrdinal("elkargokide_zenbakia")) ? string.Empty : DatuBaseTestua.Zuzendu(irakurlea.GetString("elkargokide_zenbakia")),
                                Espezialitatea = irakurlea.IsDBNull(irakurlea.GetOrdinal("espezialitatea")) ? string.Empty : DatuBaseTestua.Zuzendu(irakurlea.GetString("espezialitatea")),
                                Kontsulta = irakurlea.IsDBNull(irakurlea.GetOrdinal("kontsulta")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("kontsulta")),
                                Lanaldia = irakurlea.IsDBNull(irakurlea.GetOrdinal("lanaldia")) ? "Osoa" : DatuBaseTestua.Zuzendu(irakurlea.GetString("lanaldia"))
                            });
                        }
                    }
                }
            }

            return langileak;
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
                    string q1 = @"INSERT INTO erabiltzaileak (email, pasahitza, rol_id, aktibo, hizkuntza, izena, abizenak, jaiotze_data, telefonoa, nan, helbidea, herria, posta_kodea, irudia) 
                                  VALUES (@email, @pass, 1, 1, @hizkuntza, @izena, @abizenak, @jaiotze, @telefonoa, @nan, @helbidea, @herria, @posta, @irudia); 
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
                        cmd1.Parameters.AddWithValue("@helbidea", (object?)m.Helbidea ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@herria", (object?)m.Herria ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@posta", (object?)m.PostaKodea ?? DBNull.Value);
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

        public bool EzabatuOsasunLangilea(int id)
        {
            return DesaktibatuErabiltzailea(id);
        }

        public bool EguneratuOsasunLangilea(OsasunLangilea m)
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
                                    nan = @nan,
                                    izena = @izena,
                                    abizenak = @abizenak,
                                    jaiotze_data = @jaiotze,
                                    telefonoa = @telefonoa,
                                    helbidea = @helbidea,
                                    herria = @herria,
                                    posta_kodea = @posta,
                                    irudia = @irudia
                                  WHERE id = @id";
                    using (var cmd1 = new MySqlCommand(q1, konexioa, transakzioa))
                    {
                        cmd1.Parameters.AddWithValue("@id", m.Id);
                        cmd1.Parameters.AddWithValue("@email", m.Emaila);
                        cmd1.Parameters.AddWithValue("@pasahitza", m.Pasahitza);
                        cmd1.Parameters.AddWithValue("@hizkuntza", m.Hizkuntza);
                        cmd1.Parameters.AddWithValue("@nan", m.Nan);
                        cmd1.Parameters.AddWithValue("@izena", m.Izena);
                        cmd1.Parameters.AddWithValue("@abizenak", m.Abizenak);
                        cmd1.Parameters.AddWithValue("@jaiotze", m.JaiotzeData);
                        cmd1.Parameters.AddWithValue("@telefonoa", (object?)m.Telefonoa ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@helbidea", (object?)m.Helbidea ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@herria", (object?)m.Herria ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@posta", (object?)m.PostaKodea ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@irudia", string.IsNullOrWhiteSpace(m.Irudia) ? "img/lehenetsia.png" : m.Irudia);
                        cmd1.ExecuteNonQuery();
                    }

                    string q2 = @"UPDATE osasun_langileak SET
                                    elkargokide_zenbakia = @elkargokide,
                                    espezialitatea = @espezialitatea,
                                    kontsulta = @kontsulta,
                                    lanaldia = @lanaldia
                                  WHERE id = @id";
                    using (var cmd2 = new MySqlCommand(q2, konexioa, transakzioa))
                    {
                        cmd2.Parameters.AddWithValue("@id", m.Id);
                        cmd2.Parameters.AddWithValue("@elkargokide", m.ElkargokideZenbakia);
                        cmd2.Parameters.AddWithValue("@espezialitatea", m.Espezialitatea);
                        cmd2.Parameters.AddWithValue("@kontsulta", (object?)m.Kontsulta ?? DBNull.Value);
                        cmd2.Parameters.AddWithValue("@lanaldia", m.Lanaldia);
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
    }
}