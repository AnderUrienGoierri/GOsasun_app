using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GOsasun_app.Modeloa;

namespace GOsasun_app.DatuBasea
{
    public class NeurketaDB
    {
        public List<Neurketa> LortuPazientearenNeurketak(int pazienteId)
        {
            List<Neurketa> neurketak = new List<Neurketa>();

            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    SELECT id, paziente_id, tentsio_sistolikoa, 
                           tentsio_diastolikoa, pisua_kg, altuera, pultsua_ppm, sintomak, erregistro_data
                    FROM neurketak
                    WHERE paziente_id = @pazienteId
                    ORDER BY erregistro_data DESC";

                using (var komandoa = new MySqlCommand(query, konexioa))
                {
                    komandoa.Parameters.AddWithValue("@pazienteId", pazienteId);

                    using (var irakurlea = komandoa.ExecuteReader())
                    {
                        while (irakurlea.Read())
                        {
                            neurketak.Add(new Neurketa
                            {
                                NeurketaId = irakurlea.GetInt32("id"),
                                PazienteId = irakurlea.GetInt32("paziente_id"),
                                TentsioSistolikoa = irakurlea.IsDBNull(irakurlea.GetOrdinal("tentsio_sistolikoa")) ? (int?)null : irakurlea.GetInt32("tentsio_sistolikoa"),
                                TentsioDiastolikoa = irakurlea.IsDBNull(irakurlea.GetOrdinal("tentsio_diastolikoa")) ? (int?)null : irakurlea.GetInt32("tentsio_diastolikoa"),
                                PisuaKg = irakurlea.IsDBNull(irakurlea.GetOrdinal("pisua_kg")) ? (decimal?)null : irakurlea.GetDecimal("pisua_kg"),
                                Altuera = irakurlea.IsDBNull(irakurlea.GetOrdinal("altuera")) ? (decimal?)null : irakurlea.GetDecimal("altuera"),
                                PultsuaPpm = irakurlea.IsDBNull(irakurlea.GetOrdinal("pultsua_ppm")) ? (int?)null : irakurlea.GetInt32("pultsua_ppm"),
                                Sintomak = irakurlea.IsDBNull(irakurlea.GetOrdinal("sintomak")) ? null : irakurlea.GetString("sintomak"),
                                ErregistroData = irakurlea.GetDateTime("erregistro_data")
                            });
                        }
                    }
                }
            }
            return neurketak;
        }

        public bool GordeNeurketa(Neurketa neurketa)
        {
            try
            {
                using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
                {
                    string query = @"
                        INSERT INTO neurketak (paziente_id, tentsio_sistolikoa, 
                                             tentsio_diastolikoa, pisua_kg, altuera, pultsua_ppm, sintomak, erregistro_data)
                        VALUES (@pazienteId, @tentsioSistolikoa, 
                                @tentsioDiastolikoa, @pisuaKg, @altuera, @pultsuaPpm, @sintomak, @erregistroData)";

                    using (var komandoa = new MySqlCommand(query, konexioa))
                    {
                        komandoa.Parameters.AddWithValue("@pazienteId", neurketa.PazienteId);
                        komandoa.Parameters.AddWithValue("@tentsioSistolikoa", (object?)neurketa.TentsioSistolikoa ?? DBNull.Value);
                        komandoa.Parameters.AddWithValue("@tentsioDiastolikoa", (object?)neurketa.TentsioDiastolikoa ?? DBNull.Value);
                        komandoa.Parameters.AddWithValue("@pisuaKg", (object?)neurketa.PisuaKg ?? DBNull.Value);
                        komandoa.Parameters.AddWithValue("@altuera", (object?)neurketa.Altuera ?? DBNull.Value);
                        komandoa.Parameters.AddWithValue("@pultsuaPpm", (object?)neurketa.PultsuaPpm ?? DBNull.Value);
                        komandoa.Parameters.AddWithValue("@sintomak", (object?)neurketa.Sintomak ?? DBNull.Value);
                        komandoa.Parameters.AddWithValue("@erregistroData", neurketa.ErregistroData);

                        return komandoa.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Errorea neurketa gordetzean: " + ex.Message);
                return false;
            }
        }
    }
}
