using System;
using System.Collections.Generic;
using GOsasun_app.Modeloa;
using MySql.Data.MySqlClient;

namespace GOsasun_app.Repositorioa
{
    public class DokumentuaDB
    {
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
                            dokumentuak.Add(new Dokumentua
                            {
                                Id = irakurlea.GetInt32("id"),
                                JarraipenaId = irakurlea.GetInt32("jarraipena_id"),
                                FitxategiIzena = irakurlea.GetString("fitxategi_izena"),
                                BideaZerbitzarian = irakurlea.GetString("bidea_zerbitzarian"),
                                DokumentuIzena = irakurlea.IsDBNull(irakurlea.GetOrdinal("dokumentu_izena")) ? null : irakurlea.GetString("dokumentu_izena"),
                                Deskribapena = irakurlea.IsDBNull(irakurlea.GetOrdinal("deskribapena")) ? null : irakurlea.GetString("deskribapena"),
                                IgotzeData = irakurlea.GetDateTime("igotze_data")
                            });
                        }
                    }
                }
            }

            return dokumentuak;
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
    }
}