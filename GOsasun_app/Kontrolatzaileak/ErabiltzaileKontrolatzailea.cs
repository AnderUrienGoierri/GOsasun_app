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
    }
}
