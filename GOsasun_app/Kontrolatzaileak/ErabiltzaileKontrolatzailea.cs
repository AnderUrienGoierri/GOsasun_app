using MySql.Data.MySqlClient;
using GOsasun_app.Modeloak;

namespace GOsasun_app.Kontrolatzaileak
{
    /// <summary>
    /// Erabiltzaileen kudeaketarako kontrolatzailea (Kontrolatzailea).
    /// Saio-hasiera eta erabiltzaile-datuen eskurapena kudeatzen du.
    /// </summary>
    public class ErabiltzaileKontrolatzailea
    {
        /// <summary>
        /// Erabiltzailea datu-basean egiaztatzen du email eta pasahitz bidez.
        /// </summary>
        /// <param name="emaila">Erabiltzailearen helbide elektronikoa.</param>
        /// <param name="pasahitza">Erabiltzailearen pasahitza (testu arrunta oraingoz).</param>
        /// <returns>Erabiltzailea objektua arrakasta bada, null bestela.</returns>
        /// <exception cref="MySqlException">Datu-basearekin konexio errorea badago.</exception>
        public Erabiltzailea? Login(string emaila, string pasahitza)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    SELECT e.erabiltzaile_id,
                            COALESCE(m.izena, p.izena, h.izena) as izena,
                            COALESCE(m.abizenak, p.abizenak, h.abizenak) as abizena,
                            e.email,
                            r.izena as rol_izena
                    FROM Erabiltzaileak e
                    JOIN Rolak r ON e.rol_id = r.rol_id
                    LEFT JOIN Medikuak m ON e.erabiltzaile_id = m.mediku_id
                    LEFT JOIN Pazienteak p ON e.erabiltzaile_id = p.paziente_id
                    LEFT JOIN Harrerako_Langileak h ON e.erabiltzaile_id = h.langile_id
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
                            int id = irakurlea.GetInt32("erabiltzaile_id");
                            string izena = irakurlea.IsDBNull(irakurlea.GetOrdinal("izena")) ? "Erabiltzailea" : irakurlea.GetString("izena");
                            string abizena = irakurlea.IsDBNull(irakurlea.GetOrdinal("abizena")) ? "" : irakurlea.GetString("abizena");
                            string email = irakurlea.GetString("email");
                            string rolIzena = irakurlea.GetString("rol_izena");

                            if (rolIzena.Equals("Pazientea", StringComparison.OrdinalIgnoreCase))
                                return new Pazientea { Id = id, Izena = izena, Abizenak = abizena, Emaila = email, RolId = 3 };
                            else if (rolIzena.Equals("Medikua", StringComparison.OrdinalIgnoreCase))
                                return new Medikua { Id = id, Izena = izena, Abizenak = abizena, Emaila = email, RolId = 2 };
                            else
                                return new HarrerakoLangilea { Id = id, Izena = izena, Abizenak = abizena, Emaila = email, RolId = 4 };
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Mediku bati esleitutako pazienteen zerrenda lortzen du, aukeran bilaketa iragazki batekin.
        /// </summary>
        /// <param name="medikuId">Medikuaren IDa.</param>
        /// <param name="bilatzailea">Bilatzeko testua (izena edo abizena).</param>
        /// <returns>Pazientea objektuen zerrenda.</returns>
        public List<Pazientea> LortuMedikuarenPazienteak(int medikuId, string? bilatzailea = null)
        {
            List<Pazientea> pazienteak = new List<Pazientea>();

            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    SELECT e.erabiltzaile_id, e.email, e.pasahitza, e.rol_id, e.aktibo, e.sortze_data,
                            p.nan, p.izena, p.abizenak, p.jaiotze_data, p.telefonoa, p.odol_taldea,
                            p.azken_altuera, p.azken_pisua, p.egoera_klinikoa, p.irudia
                    FROM mediku_Paziente mp
                    JOIN pazienteak p ON mp.paziente_id = p.paziente_id
                    JOIN erabiltzaileak e ON p.paziente_id = e.erabiltzaile_id
                    WHERE mp.mediku_id = @medikuId";

                if (!string.IsNullOrEmpty(bilatzailea))
                {
                    query += " AND (p.izena LIKE @testua OR p.abizenak LIKE @testua)";
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
                                Id = irakurlea.GetInt32("erabiltzaile_id"),
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
    }
}
