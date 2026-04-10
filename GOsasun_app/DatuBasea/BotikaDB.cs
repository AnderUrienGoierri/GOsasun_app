using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GOsasun_app.Modeloa;

namespace GOsasun_app.DatuBasea
{
    public class BotikaDB
    {
        public List<Botika> LortuBotikaGuztiak()
        {
            List<Botika> botikak = new List<Botika>();
            using (var konexioa = DatuBaseKonexioa.LortuKonexioa())
            {
                string query = "SELECT * FROM botikak ORDER BY izena ASC";
                using (var komandoa = new MySqlCommand(query, konexioa))
                using (var irakurlea = komandoa.ExecuteReader())
                {
                    while (irakurlea.Read())
                    {
                        botikak.Add(new Botika
                        {
                            BotikaId = irakurlea.GetInt32("id"),
                            Izena = irakurlea.GetString("izena"),
                            IzenKimikoa = irakurlea.IsDBNull(irakurlea.GetOrdinal("izen_kimikoa")) ? null : irakurlea.GetString("izen_kimikoa"),
                            NomenklaturaKimikoa = irakurlea.IsDBNull(irakurlea.GetOrdinal("nomenklatura_kimikoa")) ? null : irakurlea.GetString("nomenklatura_kimikoa"),
                            EraginFokoa = irakurlea.IsDBNull(irakurlea.GetOrdinal("eragin_fokoa")) ? null : irakurlea.GetString("eragin_fokoa"),
                            Aktibitatea = irakurlea.IsDBNull(irakurlea.GetOrdinal("aktibitatea")) ? null : irakurlea.GetString("aktibitatea")
                        });
                    }
                }
            }
            return botikak;
        }
    }
}
