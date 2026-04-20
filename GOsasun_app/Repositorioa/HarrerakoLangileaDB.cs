using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Repositorioa
{
    public class HarrerakoLangileaDB
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

        public List<HarrerakoLangilea> LortuGuztiakHarrerakoak(string? bilatzailea = null)
        {
            List<HarrerakoLangilea> harrerakoak = new List<HarrerakoLangilea>();
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"SELECT e.id, e.email, e.pasahitza, e.rol_id, e.aktibo, e.sortze_data, e.hizkuntza,
                                        e.nan, e.izena, e.abizenak, e.jaiotze_data, e.telefonoa, e.helbidea, e.herria, e.posta_kodea, e.irudia,
                                        hl.txanda
                                 FROM harrerako_langileak hl
                                 JOIN erabiltzaileak e ON hl.id = e.id
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
                            harrerakoak.Add(new HarrerakoLangilea
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
                            });
                        }
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
                    string q1 = @"INSERT INTO erabiltzaileak (email, pasahitza, rol_id, aktibo, hizkuntza, nan, izena, abizenak, jaiotze_data, telefonoa, helbidea, herria, posta_kodea, irudia) 
                                  VALUES (@email, @pass, 3, 1, @hizkuntza, @nan, @izena, @abizenak, @jaiotze, @telefonoa, @helbidea, @herria, @posta, @irudia); 
                                  SELECT LAST_INSERT_ID();";
                    using (var cmd1 = new MySqlCommand(q1, konexioa, transakzioa))
                    {
                        cmd1.Parameters.AddWithValue("@email", h.Emaila);
                        cmd1.Parameters.AddWithValue("@pass", h.Pasahitza);
                        cmd1.Parameters.AddWithValue("@hizkuntza", h.Hizkuntza);
                        cmd1.Parameters.AddWithValue("@nan", string.IsNullOrWhiteSpace(h.Nan) ? DBNull.Value : h.Nan);
                        cmd1.Parameters.AddWithValue("@izena", h.Izena);
                        cmd1.Parameters.AddWithValue("@abizenak", h.Abizenak);
                        cmd1.Parameters.AddWithValue("@jaiotze", h.JaiotzeData);
                        cmd1.Parameters.AddWithValue("@telefonoa", (object?)h.Telefonoa ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@helbidea", (object?)h.Helbidea ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@herria", (object?)h.Herria ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@posta", (object?)h.PostaKodea ?? DBNull.Value);
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

        public bool EzabatuHarrerakoa(int id)
        {
            return DesaktibatuErabiltzailea(id);
        }

        public bool EguneratuHarrerakoa(HarrerakoLangilea h)
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
                        cmd1.Parameters.AddWithValue("@id", h.Id);
                        cmd1.Parameters.AddWithValue("@email", h.Emaila);
                        cmd1.Parameters.AddWithValue("@pasahitza", h.Pasahitza);
                        cmd1.Parameters.AddWithValue("@hizkuntza", h.Hizkuntza);
                        cmd1.Parameters.AddWithValue("@nan", string.IsNullOrWhiteSpace(h.Nan) ? DBNull.Value : h.Nan);
                        cmd1.Parameters.AddWithValue("@izena", h.Izena);
                        cmd1.Parameters.AddWithValue("@abizenak", h.Abizenak);
                        cmd1.Parameters.AddWithValue("@jaiotze", h.JaiotzeData);
                        cmd1.Parameters.AddWithValue("@telefonoa", (object?)h.Telefonoa ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@helbidea", (object?)h.Helbidea ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@herria", (object?)h.Herria ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@posta", (object?)h.PostaKodea ?? DBNull.Value);
                        cmd1.Parameters.AddWithValue("@irudia", string.IsNullOrWhiteSpace(h.Irudia) ? "img/lehenetsia.png" : h.Irudia);
                        cmd1.ExecuteNonQuery();
                    }

                    string q2 = @"UPDATE harrerako_langileak SET
                                    txanda = @txanda
                                    WHERE id = @id";
                    using (var cmd2 = new MySqlCommand(q2, konexioa, transakzioa))
                    {
                        cmd2.Parameters.AddWithValue("@id", h.Id);
                        cmd2.Parameters.AddWithValue("@txanda", h.Txanda);
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