using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GOsasun_app.Modeloa;

namespace GOsasun_app.DatuBasea
{
    public class ErabiltzaileDB
    {
        public Erabiltzailea? Login(string emaila, string pasahitza)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    SELECT e.id,
                            COALESCE(m.izena, p.izena, h.izena) as izena,
                            COALESCE(m.abizenak, p.abizenak, h.abizenak) as abizena,
                            e.email,
                            r.izena as rol_izena
                    FROM erabiltzaileak e
                    JOIN rolak r ON e.rol_id = r.id
                    LEFT JOIN medikuak m ON e.id = m.id
                    LEFT JOIN pazienteak p ON e.id = p.id
                    LEFT JOIN harrerako_Langileak h ON e.id = h.id
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
                            string izena = irakurlea.IsDBNull(irakurlea.GetOrdinal("izena")) ? "Erabiltzailea" : irakurlea.GetString("izena");
                            string abizena = irakurlea.IsDBNull(irakurlea.GetOrdinal("abizena")) ? "" : irakurlea.GetString("abizena");
                            string email = irakurlea.GetString("email");
                            string rolIzena = irakurlea.GetString("rol_izena");

                            if (rolIzena.Equals("Pazientea", StringComparison.OrdinalIgnoreCase))
                                return new Pazientea { Id = id, Izena = izena, Abizenak = abizena, Emaila = email, RolId = 2 };
                            else if (rolIzena.Equals("Medikua", StringComparison.OrdinalIgnoreCase))
                                return new Medikua { Id = id, Izena = izena, Abizenak = abizena, Emaila = email, RolId = 1 };
                            else
                                return new HarrerakoLangilea { Id = id, Izena = izena, Abizenak = abizena, Emaila = email, RolId = 3 };
                        }
                    }
                }
            }
            return null;
        }

        public List<Pazientea> LortuMedikuarenPazienteak(int medikuId, string? bilatzailea = null)
        {
            List<Pazientea> pazienteak = new List<Pazientea>();

            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    SELECT e.id, e.email, e.pasahitza, e.rol_id, e.aktibo, e.sortze_data,
                            p.nan, p.izena, p.abizenak, p.jaiotze_data, p.telefonoa, p.odol_taldea,
                            p.azken_altuera, p.azken_pisua, p.egoera_klinikoa, p.irudia
                    FROM mediku_Paziente mp
                    JOIN pazienteak p ON mp.paziente_id = p.id
                    JOIN erabiltzaileak e ON p.id = e.id
                    WHERE mp.mediku_id = @medikuId";

                if (!string.IsNullOrEmpty(bilatzailea))
                {
                    query += " AND (p.izena LIKE @testua OR p.abizenak LIKE @testua OR p.nan LIKE @testua)";
                }

                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    komandoa.Parameters.AddWithValue("@medikuId", medikuId);
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
                                Emaila = irakurlea.GetString("email"),
                                Pasahitza = irakurlea.GetString("pasahitza"),
                                RolId = irakurlea.GetInt32("rol_id"),
                                Aktibo = irakurlea.GetBoolean("aktibo"),
                                SortzeData = irakurlea.GetDateTime("sortze_data"),
                                Nan = irakurlea.GetString("nan"),
                                Izena = irakurlea.GetString("izena"),
                                Abizenak = irakurlea.GetString("abizenak"),
                                JaiotzeData = irakurlea.GetDateTime("jaiotze_data"),
                                Telefonoa = irakurlea.IsDBNull(irakurlea.GetOrdinal("telefonoa")) ? null : irakurlea.GetString("telefonoa"),
                                OdolTaldea = irakurlea.IsDBNull(irakurlea.GetOrdinal("odol_taldea")) ? null : irakurlea.GetString("odol_taldea"),
                                AzkenAltuera = irakurlea.IsDBNull(irakurlea.GetOrdinal("azken_altuera")) ? (decimal?)null : irakurlea.GetDecimal("azken_altuera"),
                                AzkenPisua = irakurlea.IsDBNull(irakurlea.GetOrdinal("azken_pisua")) ? (decimal?)null : irakurlea.GetDecimal("azken_pisua"),
                                EgoeraKlinikoa = irakurlea.GetString("egoera_klinikoa"),
                                Irudia = irakurlea.GetString("irudia")
                            });
                        }
                    }
                }
            }
            return pazienteak;
        }

        public List<Pazientea> LortuGuztiakPazienteak()
        {
            List<Pazientea> pazienteak = new List<Pazientea>();
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"SELECT e.id, e.email, e.pasahitza, e.rol_id, e.aktibo, e.sortze_data,
                                        p.nan, p.izena, p.abizenak, p.jaiotze_data, p.telefonoa, p.odol_taldea,
                                        p.azken_altuera, p.azken_pisua, p.egoera_klinikoa, p.irudia
                                 FROM pazienteak p
                                 JOIN erabiltzaileak e ON p.id = e.id";
                using (var komandoa = new MySqlCommand(query, konexioa))
                using (var irakurlea = komandoa.ExecuteReader())
                {
                    while (irakurlea.Read())
                    {
                        pazienteak.Add(new Pazientea
                        {
                            Id = irakurlea.GetInt32("id"),
                            Emaila = irakurlea.GetString("email"),
                            Izena = irakurlea.GetString("izena"),
                            Abizenak = irakurlea.GetString("abizenak"),
                            Nan = irakurlea.GetString("nan")
                        });
                    }
                }
            }
            return pazienteak;
        }

        public List<Medikua> LortuGuztiakMedikuak()
        {
            List<Medikua> medikuak = new List<Medikua>();
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"SELECT e.id, e.email, m.izena, m.abizenak
                                 FROM medikuak m
                                 JOIN erabiltzaileak e ON m.id = e.id";
                using (var komandoa = new MySqlCommand(query, konexioa))
                using (var irakurlea = komandoa.ExecuteReader())
                {
                    while (irakurlea.Read())
                    {
                        medikuak.Add(new Medikua
                        {
                            Id = irakurlea.GetInt32("id"),
                            Emaila = irakurlea.GetString("email"),
                            Izena = irakurlea.GetString("izena"),
                            Abizenak = irakurlea.GetString("abizenak")
                        });
                    }
                }
            }
            return medikuak;
        }

        public List<HarrerakoLangilea> LortuGuztiakHarrerakoak()
        {
            List<HarrerakoLangilea> harrerakoak = new List<HarrerakoLangilea>();
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"SELECT e.id, e.email, h.izena, h.abizenak
                                 FROM harrerako_Langileak h
                                 JOIN erabiltzaileak e ON h.id = e.id";
                using (var komandoa = new MySqlCommand(query, konexioa))
                using (var irakurlea = komandoa.ExecuteReader())
                {
                    while (irakurlea.Read())
                    {
                        harrerakoak.Add(new HarrerakoLangilea
                        {
                            Id = irakurlea.GetInt32("id"),
                            Emaila = irakurlea.GetString("email"),
                            Izena = irakurlea.GetString("izena"),
                            Abizenak = irakurlea.GetString("abizenak")
                        });
                    }
                }
            }
            return harrerakoak;
        }

        public bool SortuPazientea(Pazientea p)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            using (var transakzioa = konexioa.BeginTransaction())
            {
                try
                {
                    string q1 = "INSERT INTO erabiltzaileak (email, pasahitza, rol_id, aktibo, hizkuntza) VALUES (@email, @pass, 2, 1, @hizkuntza); SELECT LAST_INSERT_ID();";
                    using (var cmd1 = new MySqlCommand(q1, konexioa, transakzioa))
                    {
                        cmd1.Parameters.AddWithValue("@email", p.Emaila);
                        cmd1.Parameters.AddWithValue("@pass", p.Pasahitza);
                        cmd1.Parameters.AddWithValue("@hizkuntza", p.Hizkuntza);
                        int newId = Convert.ToInt32(cmd1.ExecuteScalar());

                        string q2 = @"INSERT INTO pazienteak 
                                    (id, nan, izena, abizenak, sexua, jaiotze_data, telefonoa, helbidea, herria, posta_kodea, egoera_klinikoa, irudia) 
                                    VALUES (@id, @nan, @izena, @abizenak, @sexua, @jaiotze, @telefonoa, @helbidea, @herria, @posta, 'Alta', 'img/lehenetsia_pazientea.png')";
                        using (var cmd2 = new MySqlCommand(q2, konexioa, transakzioa))
                        {
                            cmd2.Parameters.AddWithValue("@id", newId);
                            cmd2.Parameters.AddWithValue("@nan", p.Nan);
                            cmd2.Parameters.AddWithValue("@izena", p.Izena);
                            cmd2.Parameters.AddWithValue("@abizenak", p.Abizenak);
                            cmd2.Parameters.AddWithValue("@sexua", p.Sexua);
                            cmd2.Parameters.AddWithValue("@jaiotze", p.JaiotzeData);
                            cmd2.Parameters.AddWithValue("@telefonoa", (object?)p.Telefonoa ?? DBNull.Value);
                            cmd2.Parameters.AddWithValue("@helbidea", (object?)p.Helbidea ?? DBNull.Value);
                            cmd2.Parameters.AddWithValue("@herria", (object?)p.Herria ?? DBNull.Value);
                            cmd2.Parameters.AddWithValue("@posta", (object?)p.PostaKodea ?? DBNull.Value);
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

        public bool SortuMedikua(Medikua m)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            using (var transakzioa = konexioa.BeginTransaction())
            {
                try
                {
                    string q1 = "INSERT INTO erabiltzaileak (email, pasahitza, rol_id, aktibo, hizkuntza) VALUES (@email, @pass, 1, 1, @hizkuntza); SELECT LAST_INSERT_ID();";
                    using (var cmd1 = new MySqlCommand(q1, konexioa, transakzioa))
                    {
                        cmd1.Parameters.AddWithValue("@email", m.Emaila);
                        cmd1.Parameters.AddWithValue("@pass", m.Pasahitza);
                        cmd1.Parameters.AddWithValue("@hizkuntza", m.Hizkuntza);
                        int newId = Convert.ToInt32(cmd1.ExecuteScalar());

                        string q2 = @"INSERT INTO medikuak 
                                    (id, izena, abizenak, jaiotze_data, elkargokide_zenbakia, espezialitatea, kontsulta, lanaldia, telefonoa, irudia) 
                                    VALUES (@id, @izena, @abizenak, @jaiotze, @elkargokide, @espezialitatea, @kontsulta, @lanaldia, @telefonoa, 'img/lehenetsia_medikua.png')";
                        using (var cmd2 = new MySqlCommand(q2, konexioa, transakzioa))
                        {
                            cmd2.Parameters.AddWithValue("@id", newId);
                            cmd2.Parameters.AddWithValue("@izena", m.Izena);
                            cmd2.Parameters.AddWithValue("@abizenak", m.Abizenak);
                            cmd2.Parameters.AddWithValue("@jaiotze", m.JaiotzeData);
                            cmd2.Parameters.AddWithValue("@elkargokide", m.ElkargokideZenbakia);
                            cmd2.Parameters.AddWithValue("@espezialitatea", m.Espezialitatea);
                            cmd2.Parameters.AddWithValue("@kontsulta", (object?)m.Kontsulta ?? DBNull.Value);
                            cmd2.Parameters.AddWithValue("@lanaldia", m.Lanaldia);
                            cmd2.Parameters.AddWithValue("@telefonoa", (object?)m.Telefonoa ?? DBNull.Value);
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
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            using (var transakzioa = konexioa.BeginTransaction())
            {
                try
                {
                    string q1 = "INSERT INTO erabiltzaileak (email, pasahitza, rol_id, aktibo, hizkuntza) VALUES (@email, @pass, 3, 1, @hizkuntza); SELECT LAST_INSERT_ID();";
                    using (var cmd1 = new MySqlCommand(q1, konexioa, transakzioa))
                    {
                        cmd1.Parameters.AddWithValue("@email", h.Emaila);
                        cmd1.Parameters.AddWithValue("@pass", h.Pasahitza);
                        cmd1.Parameters.AddWithValue("@hizkuntza", h.Hizkuntza);
                        int newId = Convert.ToInt32(cmd1.ExecuteScalar());

                        string q2 = @"INSERT INTO harrerako_Langileak 
                                    (id, izena, abizenak, txanda, jaiotze_data, telefonoa) 
                                    VALUES (@id, @izena, @abizenak, @txanda, @jaiotze, @telefonoa)";
                        using (var cmd2 = new MySqlCommand(q2, konexioa, transakzioa))
                        {
                            cmd2.Parameters.AddWithValue("@id", newId);
                            cmd2.Parameters.AddWithValue("@izena", h.Izena);
                            cmd2.Parameters.AddWithValue("@abizenak", h.Abizenak);
                            cmd2.Parameters.AddWithValue("@txanda", h.Txanda);
                            cmd2.Parameters.AddWithValue("@jaiotze", (object?)h.JaiotzeData ?? DBNull.Value);
                            cmd2.Parameters.AddWithValue("@telefonoa", (object?)h.Telefonoa ?? DBNull.Value);
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
    }
}
