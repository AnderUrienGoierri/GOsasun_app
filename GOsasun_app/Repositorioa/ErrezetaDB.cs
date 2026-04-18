using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Repositorioa
{
    public class ErrezetaDB
    {
         private const string OinarrizkoErrezetaSelect = @"
                  SELECT e.id as ErrezetaId, e.hitzordu_id, e.osasun_langile_id, e.paziente_id, e.igorpen_data, e.iraungitze_data, e.diagnostiko_laburra, e.aktibo,
                      ep.izena, ep.abizenak, ep.nan,
                      h.data as hitzordu_data
                  FROM errezetak e
                  JOIN erabiltzaileak ep ON e.paziente_id = ep.id
                  LEFT JOIN hitzorduak h ON e.hitzordu_id = h.id";

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

        public List<Errezeta> LortuErrezetaGuztiak()
        {
            string query = $@"
                {OinarrizkoErrezetaSelect}
                WHERE e.aktibo = 1
                ORDER BY e.igorpen_data DESC";

            return LortuErrezetak(query);
        }

        public List<Errezeta> LortuOsasunLangilearenErrezetak(int langileId)
        {
            string query = $@"
                {OinarrizkoErrezetaSelect}
                WHERE e.osasun_langile_id = @langileId AND e.aktibo = 1
                ORDER BY e.igorpen_data DESC";

            return LortuErrezetak(query, cmd => cmd.Parameters.AddWithValue("@langileId", langileId));
        }

        public List<Errezeta> LortuPazientearenErrezetak(int pazienteId)
        {
            string query = $@"
                {OinarrizkoErrezetaSelect}
                WHERE e.paziente_id = @pazienteId AND e.aktibo = 1
                ORDER BY e.igorpen_data DESC";

            return LortuErrezetak(query, cmd => cmd.Parameters.AddWithValue("@pazienteId", pazienteId));
        }

        private List<Errezeta> LortuErrezetak(string query, Action<MySqlCommand>? parametrizatu = null)
        {
            var errezetak = new List<Errezeta>();

            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                using (var cmd = new MySqlCommand(query, konexioa))
                {
                    parametrizatu?.Invoke(cmd);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            errezetak.Add(new Errezeta
                            {
                                ErrezetaId = Convert.ToInt32(reader["ErrezetaId"]),
                                HitzorduId = reader["hitzordu_id"] != DBNull.Value ? Convert.ToInt32(reader["hitzordu_id"]) : null,
                                OsasunLangileId = Convert.ToInt32(reader["osasun_langile_id"]),
                                PazienteId = Convert.ToInt32(reader["paziente_id"]),
                                IgorpenData = Convert.ToDateTime(reader["igorpen_data"]),
                                IraungitzeData = reader["iraungitze_data"] != DBNull.Value ? Convert.ToDateTime(reader["iraungitze_data"]) : null,
                                Diagnostikoa = reader["diagnostiko_laburra"] == DBNull.Value ? null : DatuBaseTestua.Zuzendu(reader["diagnostiko_laburra"].ToString()!),
                                Aktibo = Convert.ToBoolean(reader["aktibo"]),
                                PazienteIzenOsoa = $"{DatuBaseTestua.Zuzendu(reader["izena"].ToString()!)} {DatuBaseTestua.Zuzendu(reader["abizenak"].ToString()!)}",
                                PazienteNan = reader["nan"] == DBNull.Value ? null : DatuBaseTestua.Zuzendu(reader["nan"].ToString()!),
                                HitzorduData = reader["hitzordu_data"] != DBNull.Value ? Convert.ToDateTime(reader["hitzordu_data"]) : null
                            });
                        }
                    }
                }

                string queryBotikak = @"
                    SELECT eb.errezeta_id, eb.botika_id, eb.dosia, eb.maiztasuna, b.izena as BotikaIzena
                    FROM errezeta_botikak eb
                    JOIN botikak b ON eb.botika_id = b.id
                    WHERE eb.errezeta_id = @errezetaId";

                foreach (var errezeta in errezetak)
                {
                    using (var cmd2 = new MySqlCommand(queryBotikak, konexioa))
                    {
                        cmd2.Parameters.AddWithValue("@errezetaId", errezeta.ErrezetaId);
                        using (var readerBotikak = cmd2.ExecuteReader())
                        {
                            while (readerBotikak.Read())
                            {
                                errezeta.Botikak.Add(new ErrezetaBotika
                                {
                                    ErrezetaId = errezeta.ErrezetaId,
                                    BotikaId = Convert.ToInt32(readerBotikak["botika_id"]),
                                    Dosia = readerBotikak["dosia"] == DBNull.Value ? null : DatuBaseTestua.Zuzendu(readerBotikak["dosia"].ToString()!),
                                    Maiztasuna = readerBotikak["maiztasuna"] == DBNull.Value ? null : DatuBaseTestua.Zuzendu(readerBotikak["maiztasuna"].ToString()!),
                                    BotikaIzena = readerBotikak["BotikaIzena"] == DBNull.Value ? null : DatuBaseTestua.Zuzendu(readerBotikak["BotikaIzena"].ToString()!)
                                });
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
