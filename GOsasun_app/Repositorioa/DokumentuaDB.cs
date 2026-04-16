using System;
using System.Collections.Generic;
using GOsasun_app.Modeloa;
using MySql.Data.MySqlClient;

namespace GOsasun_app.Repositorioa
{
    public class DokumentuaDB
    {
        public List<Dokumentua> LortuDokumentuGuztiak(string? bilaketa = null, DateTime? hasieraData = null, DateTime? amaieraData = null, int? pazienteId = null)
        {
            List<Dokumentua> dokumentuak = new List<Dokumentua>();

            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    SELECT d.id, d.jarraipena_id, d.fitxategi_izena, d.bidea_zerbitzarian, d.dokumentu_izena, d.deskribapena, d.igotze_data,
                           j.erregistro_data AS jarraipen_data, j.paziente_id,
                           e.nan, e.izena, e.abizenak
                    FROM dokumentuak d
                    JOIN jarraipenak j ON d.jarraipena_id = j.id
                    JOIN erabiltzaileak e ON j.paziente_id = e.id
                    WHERE (@testua IS NULL
                        OR e.nan COLLATE utf8mb4_unicode_ci LIKE CONVERT(@testua USING utf8mb4) COLLATE utf8mb4_unicode_ci
                        OR e.izena COLLATE utf8mb4_unicode_ci LIKE CONVERT(@testua USING utf8mb4) COLLATE utf8mb4_unicode_ci
                        OR e.abizenak COLLATE utf8mb4_unicode_ci LIKE CONVERT(@testua USING utf8mb4) COLLATE utf8mb4_unicode_ci
                        OR COALESCE(d.dokumentu_izena, '') COLLATE utf8mb4_unicode_ci LIKE CONVERT(@testua USING utf8mb4) COLLATE utf8mb4_unicode_ci
                        OR d.fitxategi_izena COLLATE utf8mb4_unicode_ci LIKE CONVERT(@testua USING utf8mb4) COLLATE utf8mb4_unicode_ci
                        OR DATE_FORMAT(d.igotze_data, '%Y-%m-%d') COLLATE utf8mb4_unicode_ci LIKE CONVERT(@testua USING utf8mb4) COLLATE utf8mb4_unicode_ci
                        OR DATE_FORMAT(d.igotze_data, '%d/%m/%Y') COLLATE utf8mb4_unicode_ci LIKE CONVERT(@testua USING utf8mb4) COLLATE utf8mb4_unicode_ci)
                      AND (@hasieraData IS NULL OR d.igotze_data >= @hasieraData)
                      AND (@amaieraData IS NULL OR d.igotze_data < @amaieraData)
                      AND (@pazienteId IS NULL OR j.paziente_id = @pazienteId)
                    ORDER BY d.igotze_data DESC";

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
                            dokumentuak.Add(MapDokumentua(irakurlea));
                        }
                    }
                }
            }

            return dokumentuak;
        }

        public List<Dokumentua> LortuJarraipenarenDokumentuak(int jarraipenaId)
        {
            List<Dokumentua> dokumentuak = new List<Dokumentua>();

            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    SELECT id, jarraipena_id, fitxategi_izena, bidea_zerbitzarian, dokumentu_izena, deskribapena, igotze_data
                    FROM dokumentuak
                    WHERE jarraipena_id = @jarraipenaId
                    ORDER BY igotze_data DESC";

                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    komandoa.Parameters.AddWithValue("@jarraipenaId", jarraipenaId);

                    using (var irakurlea = komandoa.ExecuteReader())
                    {
                        while (irakurlea.Read())
                        {
                            dokumentuak.Add(MapDokumentua(irakurlea));
                        }
                    }
                }
            }

            return dokumentuak;
        }

        public List<Dokumentua> LortuPazientearenBesteDokumentuak(int pazienteId, int? baztertuJarraipenaId = null, string? bilaketa = null)
        {
            List<Dokumentua> dokumentuak = new List<Dokumentua>();

            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    SELECT d.id, d.jarraipena_id, d.fitxategi_izena, d.bidea_zerbitzarian, d.dokumentu_izena, d.deskribapena, d.igotze_data,
                           j.erregistro_data AS jarraipen_data, j.paziente_id,
                           e.nan, e.izena, e.abizenak
                    FROM dokumentuak d
                    JOIN jarraipenak j ON d.jarraipena_id = j.id
                    JOIN erabiltzaileak e ON j.paziente_id = e.id
                    WHERE j.paziente_id = @pazienteId
                      AND (@baztertuJarraipenaId IS NULL OR d.jarraipena_id <> @baztertuJarraipenaId)
                      AND (@testua IS NULL
                            OR e.nan LIKE @testua
                            OR e.izena LIKE @testua
                            OR e.abizenak LIKE @testua
                            OR d.dokumentu_izena LIKE @testua
                            OR d.fitxategi_izena LIKE @testua)
                    ORDER BY d.igotze_data DESC";

                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    string? testua = string.IsNullOrWhiteSpace(bilaketa) ? null : $"%{bilaketa.Trim()}%";
                    komandoa.Parameters.AddWithValue("@pazienteId", pazienteId);
                    komandoa.Parameters.AddWithValue("@baztertuJarraipenaId", (object?)baztertuJarraipenaId ?? DBNull.Value);
                    komandoa.Parameters.AddWithValue("@testua", (object?)testua ?? DBNull.Value);

                    using (var irakurlea = komandoa.ExecuteReader())
                    {
                        while (irakurlea.Read())
                        {
                            dokumentuak.Add(MapDokumentua(irakurlea));
                        }
                    }
                }
            }

            return dokumentuak;
        }

        public Dokumentua? LortuDokumentua(int dokumentuId)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    SELECT d.id, d.jarraipena_id, d.fitxategi_izena, d.bidea_zerbitzarian, d.dokumentu_izena, d.deskribapena, d.igotze_data,
                           j.erregistro_data AS jarraipen_data, j.paziente_id,
                           e.nan, e.izena, e.abizenak
                    FROM dokumentuak d
                    JOIN jarraipenak j ON d.jarraipena_id = j.id
                    JOIN erabiltzaileak e ON j.paziente_id = e.id
                    WHERE d.id = @id";

                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    komandoa.Parameters.AddWithValue("@id", dokumentuId);
                    using (var irakurlea = komandoa.ExecuteReader())
                    {
                        return irakurlea.Read() ? MapDokumentua(irakurlea) : null;
                    }
                }
            }
        }

        public bool GordeDokumentua(Dokumentua dokumentua)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    INSERT INTO dokumentuak (jarraipena_id, fitxategi_izena, bidea_zerbitzarian, dokumentu_izena, deskribapena, igotze_data)
                    VALUES (@jarraipenaId, @fitxategiIzena, @bidea, @dokumentuIzena, @deskribapena, @igotzeData)";

                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    komandoa.Parameters.AddWithValue("@jarraipenaId", dokumentua.JarraipenaId);
                    komandoa.Parameters.AddWithValue("@fitxategiIzena", dokumentua.FitxategiIzena);
                    komandoa.Parameters.AddWithValue("@bidea", dokumentua.BideaZerbitzarian);
                    komandoa.Parameters.AddWithValue("@dokumentuIzena", (object?)dokumentua.DokumentuIzena ?? DBNull.Value);
                    komandoa.Parameters.AddWithValue("@deskribapena", (object?)dokumentua.Deskribapena ?? DBNull.Value);
                    komandoa.Parameters.AddWithValue("@igotzeData", dokumentua.IgotzeData);

                    return komandoa.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool EguneratuDokumentua(Dokumentua dokumentua)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    UPDATE dokumentuak
                    SET dokumentu_izena = @dokumentuIzena,
                        deskribapena = @deskribapena
                    WHERE id = @id";

                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    komandoa.Parameters.AddWithValue("@id", dokumentua.Id);
                    komandoa.Parameters.AddWithValue("@dokumentuIzena", (object?)dokumentua.DokumentuIzena ?? DBNull.Value);
                    komandoa.Parameters.AddWithValue("@deskribapena", (object?)dokumentua.Deskribapena ?? DBNull.Value);

                    return komandoa.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool AldatuDokumentuarenJarraipena(int dokumentuId, int jarraipenaId)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = "UPDATE dokumentuak SET jarraipena_id = @jarraipenaId WHERE id = @id";
                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    komandoa.Parameters.AddWithValue("@jarraipenaId", jarraipenaId);
                    komandoa.Parameters.AddWithValue("@id", dokumentuId);
                    return komandoa.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool EzabatuDokumentua(int dokumentuId)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = "DELETE FROM dokumentuak WHERE id = @id";
                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    komandoa.Parameters.AddWithValue("@id", dokumentuId);
                    return komandoa.ExecuteNonQuery() > 0;
                }
            }
        }

        private static Dokumentua MapDokumentua(MySqlDataReader irakurlea)
        {
            return new Dokumentua
            {
                Id = irakurlea.GetInt32("id"),
                JarraipenaId = irakurlea.GetInt32("jarraipena_id"),
                PazienteId = irakurlea.HasColumn("paziente_id") ? irakurlea.GetInt32("paziente_id") : 0,
                FitxategiIzena = DatuBaseTestua.Zuzendu(irakurlea.GetString("fitxategi_izena")),
                BideaZerbitzarian = DatuBaseTestua.Zuzendu(irakurlea.GetString("bidea_zerbitzarian")),
                DokumentuIzena = irakurlea.IsDBNull(irakurlea.GetOrdinal("dokumentu_izena")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("dokumentu_izena")),
                Deskribapena = irakurlea.IsDBNull(irakurlea.GetOrdinal("deskribapena")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("deskribapena")),
                IgotzeData = irakurlea.GetDateTime("igotze_data"),
                JarraipenData = irakurlea.HasColumn("jarraipen_data") && !irakurlea.IsDBNull(irakurlea.GetOrdinal("jarraipen_data")) ? irakurlea.GetDateTime("jarraipen_data") : null,
                PazienteNan = irakurlea.HasColumn("nan") && !irakurlea.IsDBNull(irakurlea.GetOrdinal("nan")) ? DatuBaseTestua.Zuzendu(irakurlea.GetString("nan")) : null,
                PazienteIzena = irakurlea.HasColumn("izena") && !irakurlea.IsDBNull(irakurlea.GetOrdinal("izena")) ? DatuBaseTestua.Zuzendu(irakurlea.GetString("izena")) : null,
                PazienteAbizenak = irakurlea.HasColumn("abizenak") && !irakurlea.IsDBNull(irakurlea.GetOrdinal("abizenak")) ? DatuBaseTestua.Zuzendu(irakurlea.GetString("abizenak")) : null
            };
        }
    }

    internal static class MySqlReaderExtensions
    {
        public static bool HasColumn(this MySqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (string.Equals(reader.GetName(i), columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}