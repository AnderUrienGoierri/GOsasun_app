using System;
using MySql.Data.MySqlClient;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Repositorioa
{
    public class ErabiltzaileDB
    {
        private const int OsasunLangileaRolId = 1;
        private const int PazienteaRolId = 2;
        private const int HarreraRolId = 3;

        public Erabiltzailea? Login(string emaila, string pasahitza)
        {
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = @"
                    SELECT e.id, e.izena, e.abizenak, e.email, e.rol_id, r.izena as rol_izena
                    FROM erabiltzaileak e
                    JOIN rolak r ON e.rol_id = r.id
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
                            int id = irakurlea.GetInt32("id");
                            string izena = DatuBaseTestua.Zuzendu(irakurlea.GetString("izena"));
                            string abizena = DatuBaseTestua.Zuzendu(irakurlea.GetString("abizenak"));
                            string email = DatuBaseTestua.Zuzendu(irakurlea.GetString("email"));
                            int rolId = irakurlea.GetInt32("rol_id");
                            string rolIzena = NormalizatuRolIzena(irakurlea.GetString("rol_izena"));

                            if (rolId == PazienteaRolId || rolIzena == "pazientea")
                            {
                                return new Pazientea { Id = id, Izena = izena, Abizenak = abizena, Emaila = email, RolId = rolId };
                            }

                            if (rolId == OsasunLangileaRolId || rolIzena == "osasunlangilea" || rolIzena == "medikua")
                            {
                                return new OsasunLangilea { Id = id, Izena = izena, Abizenak = abizena, Emaila = email, RolId = rolId };
                            }

                            return new HarrerakoLangilea { Id = id, Izena = izena, Abizenak = abizena, Emaila = email, RolId = rolId == 0 ? HarreraRolId : rolId };
                        }
                    }
                }
            }
            return null;
        }

        private static string NormalizatuRolIzena(string rolIzena)
        {
            return rolIzena.Replace(" ", string.Empty).Trim().ToLowerInvariant();
        }
    }
}
