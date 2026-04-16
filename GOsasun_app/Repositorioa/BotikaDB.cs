using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Repositorioa
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
                            Izena = DatuBaseTestua.Zuzendu(irakurlea.GetString("izena")),
                            IzenKimikoa = irakurlea.IsDBNull(irakurlea.GetOrdinal("izen_kimikoa")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("izen_kimikoa")),
                            NomenklaturaKimikoa = irakurlea.IsDBNull(irakurlea.GetOrdinal("nomenklatura_kimikoa")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("nomenklatura_kimikoa")),
                            EraginFokoa = irakurlea.IsDBNull(irakurlea.GetOrdinal("eragin_fokoa")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("eragin_fokoa")),
                            Aktibitatea = irakurlea.IsDBNull(irakurlea.GetOrdinal("aktibitatea")) ? null : DatuBaseTestua.Zuzendu(irakurlea.GetString("aktibitatea"))
                        });
                    }
                }
            }
            return botikak;
        }
    }
}
