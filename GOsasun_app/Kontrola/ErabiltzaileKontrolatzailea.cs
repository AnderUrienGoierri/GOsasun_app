using System.Collections.Generic;
using GOsasun_app.Modeloa;
using GOsasun_app.DatuBasea;

namespace GOsasun_app.Kontrola
{
    /// <summary>
    /// Erabiltzaileen kudeaketarako kontrolatzailea (Kontrolatzailea).
    /// Saio-hasiera eta erabiltzaile-datuen eskurapena kudeatzen du.
    /// </summary>
    public class ErabiltzaileKontrolatzailea
    {
        private readonly ErabiltzaileDB _db = new ErabiltzaileDB();

        /// <summary>
        /// Erabiltzailea datu-basean egiaztatzen du email eta pasahitz bidez.
        /// </summary>
        public Erabiltzailea? Login(string emaila, string pasahitza)
        {
            return _db.Login(emaila, pasahitza);
        }

        /// <summary>
        /// Mediku bati esleitutako pazienteen zerrenda lortzen du.
        /// </summary>
        public List<Pazientea> LortuMedikuarenPazienteak(int medikuId, string? bilatzailea = null)
        {
            return _db.LortuMedikuarenPazienteak(medikuId, bilatzailea);
        }

        /// <summary>
        /// Sistema osoko paziente guztien zerrenda lortzen du.
        /// </summary>
        public List<Pazientea> LortuGuztiakPazienteak()
        {
            return _db.LortuGuztiakPazienteak();
        }

        /// <summary>
        /// Sistema osoko mediku guztien zerrenda lortzen du.
        /// </summary>
        public List<Medikua> LortuGuztiakMedikuak()
        {
            return _db.LortuGuztiakMedikuak();
        }

        /// <summary>
        /// Sistema osoko harrerako langile guztien zerrenda lortzen du.
        /// </summary>
        public List<HarrerakoLangilea> LortuGuztiakHarrerakoak()
        {
            return _db.LortuGuztiakHarrerakoak();
        }
    }
}
