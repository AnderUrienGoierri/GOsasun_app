using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Repositorioa
{
    public class HitzorduDB
    {
        private Hitzordua MapHitzordua(MySqlDataReader reader)
        {
            return new Hitzordua
            {
                HitzorduId = reader.GetInt32("id"),
                PazienteId = reader.GetInt32("paziente_id"),
                OsasunLangileId = reader.GetInt32("osasun_langile_id"),
                Data = reader.GetDateTime("data"),
                HasieraOrdua = reader.GetTimeSpan("hasiera_ordua"),
                BukaeraOrdua = reader.IsDBNull(reader.GetOrdinal("bukaera_ordua")) ? (TimeSpan?)null : reader.GetTimeSpan("bukaera_ordua"),
                Arrazoia = reader.IsDBNull(reader.GetOrdinal("arrazoia")) ? null : DatuBaseTestua.Zuzendu(reader.GetString("arrazoia")),
                Egoera = DatuBaseTestua.Zuzendu(reader.GetString("egoera")),
                SortzeData = reader.GetDateTime("sortze_data"),
                PazienteIzena = reader.IsDBNull(reader.GetOrdinal("p_izena")) ? null : DatuBaseTestua.Zuzendu(reader.GetString("p_izena")),
                PazienteAbizenak = reader.IsDBNull(reader.GetOrdinal("p_abizena")) ? null : DatuBaseTestua.Zuzendu(reader.GetString("p_abizena")),
                OsasunLangileIzena = reader.IsDBNull(reader.GetOrdinal("m_izena")) ? null : DatuBaseTestua.Zuzendu(reader.GetString("m_izena")),
                OsasunLangileAbizenak = reader.IsDBNull(reader.GetOrdinal("m_abizena")) ? null : DatuBaseTestua.Zuzendu(reader.GetString("m_abizena"))
            };
        }

        public List<Hitzordua> LortuHitzorduGuztiak()
        {
            List<Hitzordua> hitzorduak = new List<Hitzordua>();
            using (var kon = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    SELECT h.*, 
                            ep.izena as p_izena, ep.abizenak as p_abizena,
                            em.izena as m_izena, em.abizenak as m_abizena
                    FROM hitzorduak h
                    JOIN erabiltzaileak ep ON h.paziente_id = ep.id
                    JOIN erabiltzaileak em ON h.osasun_langile_id = em.id
                    ORDER BY h.data DESC, h.hasiera_ordua DESC";
                using (var cmd = new MySqlCommand(query, kon))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) hitzorduak.Add(MapHitzordua(reader));
                }
            }
            return hitzorduak;
        }

        public List<Hitzordua> LortuPazientearenHitzorduak(int pazienteId)
        {
            List<Hitzordua> hitzorduak = new List<Hitzordua>();
            using (var kon = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    SELECT h.*, 
                            ep.izena as p_izena, ep.abizenak as p_abizena,
                            em.izena as m_izena, em.abizenak as m_abizena
                    FROM hitzorduak h
                    JOIN erabiltzaileak ep ON h.paziente_id = ep.id
                    JOIN erabiltzaileak em ON h.osasun_langile_id = em.id
                    WHERE h.paziente_id = @id
                    ORDER BY h.data DESC, h.hasiera_ordua DESC";
                using (var cmd = new MySqlCommand(query, kon))
                {
                    cmd.Parameters.AddWithValue("@id", pazienteId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) hitzorduak.Add(MapHitzordua(reader));
                    }
                }
            }
            return hitzorduak;
        }

        public List<Hitzordua> LortuOsasunLangilearenHitzorduak(int langileId)
        {
            List<Hitzordua> hitzorduak = new List<Hitzordua>();
            using (var kon = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    SELECT h.*, 
                            ep.izena as p_izena, ep.abizenak as p_abizena,
                            em.izena as m_izena, em.abizenak as m_abizena
                    FROM hitzorduak h
                    JOIN erabiltzaileak ep ON h.paziente_id = ep.id
                    JOIN erabiltzaileak em ON h.osasun_langile_id = em.id
                    WHERE h.osasun_langile_id = @id
                    ORDER BY h.data DESC, h.hasiera_ordua DESC";
                using (var cmd = new MySqlCommand(query, kon))
                {
                    cmd.Parameters.AddWithValue("@id", langileId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) hitzorduak.Add(MapHitzordua(reader));
                    }
                }
            }
            return hitzorduak;
        }

        public void GehituHitzordua(Hitzordua h)
        {
            using (var kon = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"INSERT INTO hitzorduak (paziente_id, osasun_langile_id, data, hasiera_ordua, bukaera_ordua, arrazoia, egoera) 
                                VALUES (@pid, @mid, @data, @has, @buk, @arr, @ego)";
                using (var cmd = new MySqlCommand(query, kon))
                {
                    cmd.Parameters.AddWithValue("@pid", h.PazienteId);
                    cmd.Parameters.AddWithValue("@mid", h.OsasunLangileId);
                    cmd.Parameters.AddWithValue("@data", h.Data);
                    cmd.Parameters.AddWithValue("@has", h.HasieraOrdua);
                    cmd.Parameters.AddWithValue("@buk", (object?)h.BukaeraOrdua ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@arr", (object?)h.Arrazoia ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ego", h.Egoera);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EguneratuHitzordua(Hitzordua h)
        {
            using (var kon = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"UPDATE hitzorduak SET paziente_id=@pid, osasun_langile_id=@mid, data=@data, 
                                        hasiera_ordua=@has, bukaera_ordua=@buk, arrazoia=@arr, egoera=@ego 
                                WHERE id=@id";
                using (var cmd = new MySqlCommand(query, kon))
                {
                    cmd.Parameters.AddWithValue("@pid", h.PazienteId);
                    cmd.Parameters.AddWithValue("@mid", h.OsasunLangileId);
                    cmd.Parameters.AddWithValue("@data", h.Data);
                    cmd.Parameters.AddWithValue("@has", h.HasieraOrdua);
                    cmd.Parameters.AddWithValue("@buk", (object?)h.BukaeraOrdua ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@arr", (object?)h.Arrazoia ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ego", h.Egoera);
                    cmd.Parameters.AddWithValue("@id", h.HitzorduId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EzabatuHitzordua(int hitzorduId)
        {
            using (var kon = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = "DELETE FROM hitzorduak WHERE id = @id";
                using (var cmd = new MySqlCommand(query, kon))
                {
                    cmd.Parameters.AddWithValue("@id", hitzorduId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
