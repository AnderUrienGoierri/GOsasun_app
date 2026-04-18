using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Repositorioa
{
    public class JarraipenaDB
    {
        public List<Jarraipena> LortuJarraipenGuztiak(string? bilaketa = null, DateTime? hasieraData = null, DateTime? amaieraData = null, int? pazienteId = null)
        {
            List<Jarraipena> jarraipenak = new List<Jarraipena>();

            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    SELECT j.id, j.paziente_id, e.nan, e.izena, e.abizenak, j.tentsio_sistolikoa,
                           j.tentsio_diastolikoa, j.pisua_kg, j.altuera, j.pultsua_ppm,
                           j.oharrak, j.erregistro_data, COUNT(d.id) AS dokumentu_kopurua
                    FROM jarraipenak j
                    JOIN erabiltzaileak e ON e.id = j.paziente_id
                    LEFT JOIN dokumentuak d ON d.jarraipena_id = j.id
                                        WHERE (@testua IS NULL OR e.nan LIKE @testua OR e.izena LIKE @testua OR e.abizenak LIKE @testua)
                                            AND (@hasieraData IS NULL OR j.erregistro_data >= @hasieraData)
                                            AND (@amaieraData IS NULL OR j.erregistro_data < @amaieraData)
                                            AND (@pazienteId IS NULL OR j.paziente_id = @pazienteId)
                    GROUP BY j.id, j.paziente_id, e.nan, e.izena, e.abizenak, j.tentsio_sistolikoa,
                             j.tentsio_diastolikoa, j.pisua_kg, j.altuera, j.pultsua_ppm,
                             j.oharrak, j.erregistro_data
                    ORDER BY j.erregistro_data DESC";

                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    string? testua = string.IsNullOrWhiteSpace(bilaketa) ? null : $"%{bilaketa.Trim()}%";
                    komandoa.Parameters.AddWithValue("@testua", (object?)testua ?? DBNull.Value);
                                        komandoa.Parameters.AddWithValue("@hasieraData", (object?)hasieraData?.Date ?? DBNull.Value);
                                        komandoa.Parameters.AddWithValue("@amaieraData", (object?)amaieraData?.Date.AddDays(1) ?? DBNull.Value);
                    komandoa.Parameters.AddWithValue("@pazienteId", (object?)pazienteId ?? DBNull.Value);

                    using (var irakurlea = komandoa.ExecuteReader())
                    {
                        while (irakurlea.Read())
                        {
                            jarraipenak.Add(new Jarraipena
                            {
                                Id = irakurlea.GetInt32("id"),
                                PazienteId = irakurlea.GetInt32("paziente_id"),
                                PazienteNan = DatuBaseTestua.Zuzendu(irakurlea.GetString("nan")),
                                PazienteIzena = DatuBaseTestua.Zuzendu(irakurlea.GetString("izena")),
                                PazienteAbizenak = DatuBaseTestua.Zuzendu(irakurlea.GetString("abizenak")),
                                TentsioSistolikoa = irakurlea.IsDBNull(irakurlea.GetOrdinal("tentsio_sistolikoa")) ? (int?)null : irakurlea.GetInt32("tentsio_sistolikoa"),
                                TentsioDiastolikoa = irakurlea.IsDBNull(irakurlea.GetOrdinal("tentsio_diastolikoa")) ? (int?)null : irakurlea.GetInt32("tentsio_diastolikoa"),
                                PisuaKg = irakurlea.IsDBNull(irakurlea.GetOrdinal("pisua_kg")) ? (decimal?)null : irakurlea.GetDecimal("pisua_kg"),
                                Altuera = irakurlea.IsDBNull(irakurlea.GetOrdinal("altuera")) ? (decimal?)null : irakurlea.GetDecimal("altuera"),
                                PultsuaPpm = irakurlea.IsDBNull(irakurlea.GetOrdinal("pultsua_ppm")) ? (int?)null : irakurlea.GetInt32("pultsua_ppm"),
                                Oharrak = irakurlea.IsDBNull(irakurlea.GetOrdinal("oharrak")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("oharrak")),
                                ErregistroData = irakurlea.GetDateTime("erregistro_data"),
                                DokumentuKopurua = irakurlea.GetInt32("dokumentu_kopurua")
                            });
                        }
                    }
                }
            }

            return jarraipenak;
        }

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
                                Oharrak = irakurlea.IsDBNull(irakurlea.GetOrdinal("oharrak")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("oharrak")),
                                BideaZerbitzarian = irakurlea.IsDBNull(irakurlea.GetOrdinal("bidea_zerbitzarian")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("bidea_zerbitzarian")),
                                ErregistroData = irakurlea.GetDateTime("erregistro_data")
                            });
                        }
                    }
                }
            }
            return jarraipenak;
        }

        public Jarraipena? LortuJarraipena(int jarraipenaId)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    SELECT id, paziente_id, osasun_langile_id, tentsio_sistolikoa,
                           tentsio_diastolikoa, pisua_kg, altuera, pultsua_ppm, oharrak, bidea_zerbitzarian, erregistro_data
                    FROM jarraipenak
                    WHERE id = @jarraipenaId";

                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    komandoa.Parameters.AddWithValue("@jarraipenaId", jarraipenaId);

                    using (var irakurlea = komandoa.ExecuteReader())
                    {
                        if (irakurlea.Read())
                        {
                            return new Jarraipena
                            {
                                Id = irakurlea.GetInt32("id"),
                                PazienteId = irakurlea.GetInt32("paziente_id"),
                                OsasunLangileId = irakurlea.IsDBNull(irakurlea.GetOrdinal("osasun_langile_id")) ? (int?)null : irakurlea.GetInt32("osasun_langile_id"),
                                TentsioSistolikoa = irakurlea.IsDBNull(irakurlea.GetOrdinal("tentsio_sistolikoa")) ? (int?)null : irakurlea.GetInt32("tentsio_sistolikoa"),
                                TentsioDiastolikoa = irakurlea.IsDBNull(irakurlea.GetOrdinal("tentsio_diastolikoa")) ? (int?)null : irakurlea.GetInt32("tentsio_diastolikoa"),
                                PisuaKg = irakurlea.IsDBNull(irakurlea.GetOrdinal("pisua_kg")) ? (decimal?)null : irakurlea.GetDecimal("pisua_kg"),
                                Altuera = irakurlea.IsDBNull(irakurlea.GetOrdinal("altuera")) ? (decimal?)null : irakurlea.GetDecimal("altuera"),
                                PultsuaPpm = irakurlea.IsDBNull(irakurlea.GetOrdinal("pultsua_ppm")) ? (int?)null : irakurlea.GetInt32("pultsua_ppm"),
                                Oharrak = irakurlea.IsDBNull(irakurlea.GetOrdinal("oharrak")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("oharrak")),
                                BideaZerbitzarian = irakurlea.IsDBNull(irakurlea.GetOrdinal("bidea_zerbitzarian")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("bidea_zerbitzarian")),
                                ErregistroData = irakurlea.GetDateTime("erregistro_data")
                            };
                        }
                    }
                }
            }

            return null;
        }

        public int? GordeJarraipenaEtaLortuId(Jarraipena jarraipena)
        {
            try
            {
                using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
                {
                    string query = @"
                        INSERT INTO jarraipenak (paziente_id, osasun_langile_id, tentsio_sistolikoa,
                                                tentsio_diastolikoa, pisua_kg, altuera, pultsua_ppm, oharrak, bidea_zerbitzarian, erregistro_data)
                        VALUES (@pazienteId, @langileId, @tentsioSistolikoa,
                                @tentsioDiastolikoa, @pisuaKg, @altuera, @pultsuaPpm, @oharrak, @bidea, @erregistroData);
                        SELECT LAST_INSERT_ID();";

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

                        object? emaitza = komandoa.ExecuteScalar();
                        return emaitza == null ? null : Convert.ToInt32(emaitza);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Errorea jarraipena gordetzean: " + ex.Message);
                return null;
            }
        }

        public bool GordeJarraipena(Jarraipena jarraipena)
        {
            return GordeJarraipenaEtaLortuId(jarraipena).HasValue;
        }

        public bool EguneratuJarraipena(Jarraipena jarraipena)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    UPDATE jarraipenak
                    SET tentsio_sistolikoa = @tentsioSistolikoa,
                        tentsio_diastolikoa = @tentsioDiastolikoa,
                        pisua_kg = @pisuaKg,
                        altuera = @altuera,
                        pultsua_ppm = @pultsuaPpm,
                        oharrak = @oharrak,
                        bidea_zerbitzarian = @bidea
                    WHERE id = @id";

                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    komandoa.Parameters.AddWithValue("@id", jarraipena.Id);
                    komandoa.Parameters.AddWithValue("@tentsioSistolikoa", (object?)jarraipena.TentsioSistolikoa ?? DBNull.Value);
                    komandoa.Parameters.AddWithValue("@tentsioDiastolikoa", (object?)jarraipena.TentsioDiastolikoa ?? DBNull.Value);
                    komandoa.Parameters.AddWithValue("@pisuaKg", (object?)jarraipena.PisuaKg ?? DBNull.Value);
                    komandoa.Parameters.AddWithValue("@altuera", (object?)jarraipena.Altuera ?? DBNull.Value);
                    komandoa.Parameters.AddWithValue("@pultsuaPpm", (object?)jarraipena.PultsuaPpm ?? DBNull.Value);
                    komandoa.Parameters.AddWithValue("@oharrak", (object?)jarraipena.Oharrak ?? DBNull.Value);
                    komandoa.Parameters.AddWithValue("@bidea", (object?)jarraipena.BideaZerbitzarian ?? DBNull.Value);
                    return komandoa.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool EzabatuJarraipena(int jarraipenaId)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = "DELETE FROM jarraipenak WHERE id = @jarraipenaId";

                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    komandoa.Parameters.AddWithValue("@jarraipenaId", jarraipenaId);
                    return komandoa.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
