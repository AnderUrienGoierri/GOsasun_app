using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Repositorioa
{
    public class ErrezetaDB
    {
        public bool SortuErrezeta(Errezeta errezeta)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            using (var transakzioa = konexioa.BeginTransaction())
            {
                try
                {
                    // Errezetak taulan txertatu
                    string insertErrezetaQuery = @"
                        INSERT INTO errezetak (hitzordu_id, osasun_langile_id, paziente_id, igorpen_data, iraungitze_data, xml_fitxategia_bidea, diagnostiko_laburra, aktibo)
                        VALUES (@hitzorduId, @langileId, @pazienteId, @igorpenData, @iraungitzeData, @xmlBidea, @diagnostikoa, @aktibo);
                        SELECT LAST_INSERT_ID();";

                    int errezetaId;
                    using (var cmd = new MySqlCommand(insertErrezetaQuery, konexioa, transakzioa))
                    {
                        cmd.Parameters.AddWithValue("@hitzorduId", (object?)errezeta.HitzorduId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@langileId", errezeta.OsasunLangileId);
                        cmd.Parameters.AddWithValue("@pazienteId", errezeta.PazienteId);
                        cmd.Parameters.AddWithValue("@igorpenData", errezeta.IgorpenData);
                        cmd.Parameters.AddWithValue("@iraungitzeData", (object?)errezeta.IraungitzeData ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@xmlBidea", (object?)errezeta.XmlBidea ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@diagnostikoa", (object?)errezeta.Diagnostikoa ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@aktibo", errezeta.Aktibo);
                        
                        errezetaId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Errezetari lotutako botika bakoitza errezeta_botikak taulan txertatu
                    string insertBotikaQuery = @"
                        INSERT INTO errezeta_botikak (errezeta_id, botika_id, dosia, maiztasuna)
                        VALUES (@errezetaId, @botikaId, @dosia, @maiztasuna)";

                    foreach (var eb in errezeta.Botikak)
                    {
                        using (var cmd2 = new MySqlCommand(insertBotikaQuery, konexioa, transakzioa))
                        {
                            cmd2.Parameters.AddWithValue("@errezetaId", errezetaId);
                            cmd2.Parameters.AddWithValue("@botikaId", eb.BotikaId);
                            cmd2.Parameters.AddWithValue("@dosia", (object?)eb.Dosia ?? DBNull.Value);
                            cmd2.Parameters.AddWithValue("@maiztasuna", (object?)eb.Maiztasuna ?? DBNull.Value);
                            cmd2.ExecuteNonQuery();
                        }
                    }

                    transakzioa.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transakzioa.Rollback();
                    Console.WriteLine($"Errorea Errezeta sortzean: {ex.Message}");
                    return false;
                }
            }
        }

        public List<Errezeta> LortuOsasunLangilearenErrezetak(int langileId)
        {
            var errezetak = new List<Errezeta>();
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                // Errezeten datu nagusiak + paziente onuraduna + balizko hitzordu data
                string query = @"
                    SELECT e.id as ErrezetaId, e.hitzordu_id, e.paziente_id, e.igorpen_data, e.iraungitze_data, e.diagnostiko_laburra, e.aktibo,
                           p.izena, p.abizenak, p.nan,
                           h.data as hitzordu_data
                    FROM errezetak e
                    JOIN pazienteak p ON e.paziente_id = p.id
                    LEFT JOIN hitzorduak h ON e.hitzordu_id = h.id
                    WHERE e.osasun_langile_id = @langileId AND e.aktibo = 1
                    ORDER BY e.igorpen_data DESC";

                using (var cmd = new MySqlCommand(query, konexioa))
                {
                    cmd.Parameters.AddWithValue("@langileId", langileId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var errezeta = new Errezeta
                            {
                                ErrezetaId = Convert.ToInt32(reader["ErrezetaId"]),
                                HitzorduId = reader["hitzordu_id"] != DBNull.Value ? Convert.ToInt32(reader["hitzordu_id"]) : null,
                                OsasunLangileId = langileId,
                                PazienteId = Convert.ToInt32(reader["paziente_id"]),
                                IgorpenData = Convert.ToDateTime(reader["igorpen_data"]),
                                IraungitzeData = reader["iraungitze_data"] != DBNull.Value ? Convert.ToDateTime(reader["iraungitze_data"]) : null,
                                Diagnostikoa = reader["diagnostiko_laburra"]?.ToString(),
                                Aktibo = Convert.ToBoolean(reader["aktibo"]),
                                PazienteIzenOsoa = $"{reader["izena"]} {reader["abizenak"]}",
                                PazienteNan = reader["nan"]?.ToString(),
                                HitzorduData = reader["hitzordu_data"] != DBNull.Value ? Convert.ToDateTime(reader["hitzordu_data"]) : null
                            };
                            errezetak.Add(errezeta);
                        }
                    }
                }

                // Egin dezagun beste kontsulta bat botikak betetzeko
                string queryBotikak = @"
                    SELECT eb.errezeta_id, eb.botika_id, eb.dosia, eb.maiztasuna, b.izena as BotikaIzena 
                    FROM errezeta_botikak eb 
                    JOIN botikak b ON eb.botika_id = b.id
                    WHERE eb.errezeta_id = @errezetaId";

                foreach (var e in errezetak)
                {
                    using (var cmd2 = new MySqlCommand(queryBotikak, konexioa))
                    {
                        cmd2.Parameters.AddWithValue("@errezetaId", e.ErrezetaId);
                        using (var readerBotikak = cmd2.ExecuteReader())
                        {
                            while (readerBotikak.Read())
                            {
                                var eb = new ErrezetaBotika
                                {
                                    ErrezetaId = e.ErrezetaId,
                                    BotikaId = Convert.ToInt32(readerBotikak["botika_id"]),
                                    Dosia = readerBotikak["dosia"]?.ToString(),
                                    Maiztasuna = readerBotikak["maiztasuna"]?.ToString(),
                                    BotikaIzena = readerBotikak["BotikaIzena"]?.ToString() // Oharra: ErrezetaBotika klaseak hau ez badu, ez da ezer gertatuko lotura delako. Baina ikusteko ondo dator.
                                };
                                e.Botikak.Add(eb);
                            }
                        }
                    }
                }
            }
            return errezetak;
        }

        public bool EguneratuErrezeta(Errezeta errezeta)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            using (var transakzioa = konexioa.BeginTransaction())
            {
                try
                {
                    // Errezetak eguneratu
                    string updateErrezetaQuery = @"
                        UPDATE errezetak 
                        SET iraungitze_data = @iraungitzeData, 
                            diagnostiko_laburra = @diagnostikoa
                        WHERE id = @errezetaId AND osasun_langile_id = @langileId";

                    using (var cmd = new MySqlCommand(updateErrezetaQuery, konexioa, transakzioa))
                    {
                        cmd.Parameters.AddWithValue("@iraungitzeData", (object?)errezeta.IraungitzeData ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@diagnostikoa", (object?)errezeta.Diagnostikoa ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@errezetaId", errezeta.ErrezetaId);
                        cmd.Parameters.AddWithValue("@langileId", errezeta.OsasunLangileId); // Segurtasun plusa
                        cmd.ExecuteNonQuery();
                    }

                    // Errezeta_botikak zaharrak ezabatu
                    string deleteBotikakQuery = "DELETE FROM errezeta_botikak WHERE errezeta_id = @errezetaId";
                    using (var cmdDel = new MySqlCommand(deleteBotikakQuery, konexioa, transakzioa))
                    {
                        cmdDel.Parameters.AddWithValue("@errezetaId", errezeta.ErrezetaId);
                        cmdDel.ExecuteNonQuery();
                    }

                    // Botika berriak sartu
                    string insertBotikaQuery = @"
                        INSERT INTO errezeta_botikak (errezeta_id, botika_id, dosia, maiztasuna)
                        VALUES (@errezetaId, @botikaId, @dosia, @maiztasuna)";

                    foreach (var eb in errezeta.Botikak)
                    {
                        using (var cmdIns = new MySqlCommand(insertBotikaQuery, konexioa, transakzioa))
                        {
                            cmdIns.Parameters.AddWithValue("@errezetaId", errezeta.ErrezetaId);
                            cmdIns.Parameters.AddWithValue("@botikaId", eb.BotikaId);
                            cmdIns.Parameters.AddWithValue("@dosia", (object?)eb.Dosia ?? DBNull.Value);
                            cmdIns.Parameters.AddWithValue("@maiztasuna", (object?)eb.Maiztasuna ?? DBNull.Value);
                            cmdIns.ExecuteNonQuery();
                        }
                    }

                    transakzioa.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transakzioa.Rollback();
                    Console.WriteLine($"Errorea Errezeta eguneratzean: {ex.Message}");
                    return false;
                }
            }
        }

        public bool EzabatuErrezeta(int errezetaId)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            using (var transakzioa = konexioa.BeginTransaction())
            {
                try
                {
                    string delBotikakQuery = "DELETE FROM errezeta_botikak WHERE errezeta_id = @errezetaId";
                    using (var cmd = new MySqlCommand(delBotikakQuery, konexioa, transakzioa))
                    {
                        cmd.Parameters.AddWithValue("@errezetaId", errezetaId);
                        cmd.ExecuteNonQuery();
                    }

                    string delErrezetaQuery = "DELETE FROM errezetak WHERE id = @errezetaId";
                    using (var cmd = new MySqlCommand(delErrezetaQuery, konexioa, transakzioa))
                    {
                        cmd.Parameters.AddWithValue("@errezetaId", errezetaId);
                        cmd.ExecuteNonQuery();
                    }

                    transakzioa.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transakzioa.Rollback();
                    Console.WriteLine($"Errorea Errezeta ezabatzean: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
