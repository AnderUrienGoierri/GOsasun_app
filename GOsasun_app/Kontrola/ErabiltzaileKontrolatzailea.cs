using System.Collections.Generic;
using GOsasun_app.Modeloa;
using GOsasun_app.Repositorioa;

namespace GOsasun_app.Kontrola
{
    /// <summary>
    /// Erabiltzaileen kudeaketarako kontrolatzailea (Kontrolatzailea).
    /// Saio-hasiera eta erabiltzaile-datuen eskurapena kudeatzen du.
    /// </summary>
    public class ErabiltzaileKontrolatzailea
    {
        private readonly ErabiltzaileDB _db = new ErabiltzaileDB();

// ------------------------------------------------------------

        /// <summary>
        /// Erabiltzailea datu-basean egiaztatzen du email eta pasahitz bidez.
        /// </summary>
        public Erabiltzailea? Login(string emaila, string pasahitza)
        {
            return _db.Login(emaila, pasahitza);
        }

// ------------------------LORTU------------------------------------

        /// <summary>
        /// Osasun langile bati esleitutako pazienteen zerrenda lortzen du.
        /// </summary>
        public List<Pazientea> LortuLangilearenPazienteak(int langileId, string? bilatzailea = null)

        {
            return _db.LortuLangilearenPazienteak(langileId, bilatzailea);
        }

        public List<OsasunLangilea> LortuPazientearenLangileak(int paziente_id, string? bilatzailea = null)

        {
            return _db.LortuPazientearenLangileak(paziente_id, bilatzailea);
        }

        /// <summary>
        /// Sistema osoko paziente guztien zerrenda lortzen du.
        /// </summary>
        public List<Pazientea> LortuGuztiakPazienteak(string? bilatzailea = null)
        {
            return _db.LortuGuztiakPazienteak(bilatzailea);
        }

        /// <summary>
        /// Sistema osoko osasun langile guztien zerrenda lortzen du.
        /// </summary>
        public List<OsasunLangilea> LortuGuztiakOsasunLangileak()
        {
            return _db.LortuGuztiakOsasunLangileak();
        }

        /// <summary>
        /// Sistema osoko harrerako langile guztien zerrenda lortzen du.
        /// </summary>
        public List<HarrerakoLangilea> LortuGuztiakHarrerakoak()
        {
            return _db.LortuGuztiakHarrerakoak();
        }

// ------------------------SORTU------------------------------------

        public bool SortuPazientea(Pazientea p)
        {
            return _db.SortuPazientea(p);
        }

        public bool SortuOsasunLangilea(OsasunLangilea m)
        {
            return _db.SortuOsasunLangilea(m);
        }

        public bool SortuHarrerakoa(HarrerakoLangilea h)
        {
            return _db.SortuHarrerakoa(h);
        }

// ------------------------EZABATU------------------------------------

        public bool EzabatuPazientea(int id)
        {
            return _db.EzabatuPazientea(id);
        }
        public bool EzabatuOsasunLangilea(int id)
        {
            return _db.EzabatuOsasunLangilea(id);
        }
        public bool EzabatuHarrerakoa(int id)
        {
            return _db.EzabatuHarrerakoa(id);
        }

// ------------------------EGUNERATU------------------------------------

        public bool EguneratuPazientea(Pazientea p)
        {
            return _db.EguneratuPazientea(p);
        }
        public bool EguneratuOsasunLangilea(OsasunLangilea m)
        {
            return _db.EguneratuOsasunLangilea(m);
        }
        public bool EguneratuHarrerakoa(HarrerakoLangilea h)
        {
            return _db.EguneratuHarrerakoa(h);
        }
    }
}
