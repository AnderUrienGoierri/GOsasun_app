using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Repositorioa
{
    public class JarraipenaDB
    {
        public List<Jarraipena> LortuPazientearenJarraipenak(int pazienteId)
        {
            List<Jarraipena> jarraipenak = new List<Jarraipena>();

            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    SELECT id, paziente_id, osasun_langile_id, tentsio_sistolikoa,
                            tentsio_diastolikoa, pisua_kg, altuera, pultsua_ppm, oharrak, bidea_zerbitzarian, erregistro_data
                    FROM jarraipenak
                    WHERE paziente_id = @pazienteId
                    ORDER BY erregistro_data DESC";

                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    komandoa.Parameters.AddWithValue("@pazienteId", pazienteId);

                    using (var irakurlea = komandoa.ExecuteReader())
                    {
                        while (irakurlea.Read())
                        {
                            jarraipenak.Add(new Jarraipena
                            {
                                Id = irakurlea.GetInt32("id"),
                                PazienteId = irakurlea.GetInt32("paziente_id"),
                                OsasunLangileId = irakurlea.IsDBNull(irakurlea.GetOrdinal("osasun_langile_id")) ? (int?)null : irakurlea.GetInt32("osasun_langile_id"),
                                TentsioSistolikoa = irakurlea.IsDBNull(irakurlea.GetOrdinal("tentsio_sistolikoa")) ? (int?)null : irakurlea.GetInt32("tentsio_sistolikoa"),
                                TentsioDiastolikoa = irakurlea.IsDBNull(irakurlea.GetOrdinal("tentsio_diastolikoa")) ? (int?)null : irakurlea.GetInt32("tentsio_diastolikoa"),
                                PisuaKg = irakurlea.IsDBNull(irakurlea.GetOrdinal("pisua_kg")) ? (decimal?)null : irakurlea.GetDecimal("pisua_kg"),
                                Altuera = irakurlea.IsDBNull(irakurlea.GetOrdinal("altuera")) ? (decimal?)null : irakurlea.GetDecimal("altuera"),
                                PultsuaPpm = irakurlea.IsDBNull(irakurlea.GetOrdinal("pultsua_ppm")) ? (int?)null : irakurlea.GetInt32("pultsua_ppm"),
                                Oharrak = irakurlea.IsDBNull(irakurlea.GetOrdinal("oharrak")) ? null : irakurlea.GetString("oharrak"),
                                BideaZerbitzarian = irakurlea.IsDBNull(irakurlea.GetOrdinal("bidea_zerbitzarian")) ? null : irakurlea.GetString("bidea_zerbitzarian"),
                                ErregistroData = irakurlea.GetDateTime("erregistro_data")
                            });
                        }
                    }
                }
            }
            return jarraipenak;
        }

        public bool GordeJarraipena(Jarraipena jarraipena)
        {
            try
            {
                using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
                {
                    string query = @"
                        INSERT INTO jarraipenak (paziente_id, osasun_langile_id, tentsio_sistolikoa,
                                                tentsio_diastolikoa, pisua_kg, altuera, pultsua_ppm, oharrak, bidea_zerbitzarian, erregistro_data)
                        VALUES (@pazienteId, @langileId, @tentsioSistolikoa,
                                @tentsioDiastolikoa, @pisuaKg, @altuera, @pultsuaPpm, @oharrak, @bidea, @erregistroData)";

                    using (var komandoa = new MySqlCommand(query, konexioa))
                    {
                        komandoa.Parameters.AddWithValue("@pazienteId", jarraipena.PazienteId);
                        komandoa.Parameters.AddWithValue("@langileId", (object?)jarraipena.OsasunLangileId ?? DBNull.Value);
                        komandoa.Parameters.AddWithValue("@tentsioSistolikoa", (object?)jarraipena.TentsioSistolikoa ?? DBNull.Value);
                        komandoa.Parameters.AddWithValue("@tentsioDiastolikoa", (object?)jarraipena.TentsioDiastolikoa ?? DBNull.Value);
                        komandoa.Parameters.AddWithValue("@pisuaKg", (object?)jarraipena.PisuaKg ?? DBNull.Value);
                        komandoa.Parameters.AddWithValue("@altuera", (object?)jarraipena.Altuera ?? DBNull.Value);
                        komandoa.Parameters.AddWithValue("@pultsuaPpm", (object?)jarraipena.PultsuaPpm ?? DBNull.Value);
                        komandoa.Parameters.AddWithValue("@oharrak", (object?)jarraipena.Oharrak ?? DBNull.Value);
                        komandoa.Parameters.AddWithValue("@bidea", (object?)jarraipena.BideaZerbitzarian ?? DBNull.Value);
                        komandoa.Parameters.AddWithValue("@erregistroData", jarraipena.ErregistroData);

                        return komandoa.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Errorea jarraipena gordetzean: " + ex.Message);
                return false;
            }
        }
    }
}
