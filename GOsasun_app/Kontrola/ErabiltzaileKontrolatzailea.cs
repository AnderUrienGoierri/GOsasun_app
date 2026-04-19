using System.Collections.Generic;
using GOsasun_app.Modeloa;
using GOsasun_app.Repositorioa;
using GOsasun_app.Kontrola.Zerbitzuak;

namespace GOsasun_app.Kontrola
{
    /// <summary>
    /// Erabiltzaileen kudeaketarako kontrolatzailea (Kontrolatzailea).
    /// Saio-hasiera eta erabiltzaile-datuen eskurapena kudeatzen du.
    /// </summary>
    public class ErabiltzaileKontrolatzailea
    {
        // ---------------------------SORTU OBJETUA------------------------------------------------------    
        private readonly ErabiltzaileDB _db = new ErabiltzaileDB();
        private readonly LoginBlokeoZerbitzua _loginBlokeoZerbitzua = new LoginBlokeoZerbitzua();

        // ---------------------------LORTU------------------------------------------------------        

        /// <summary>
        /// Erabiltzailea datu-basean egiaztatzen du email eta pasahitz bidez.
        /// </summary>
        public LoginEmaitza Login(string emaila, string pasahitza)
        {
            LoginSegurtasunEgoera unekoEgoera = _loginBlokeoZerbitzua.LortuEgoera();
            if (unekoEgoera.Blokeatuta)
            {
                return new LoginEmaitza { Egoera = unekoEgoera };
            }

            Erabiltzailea? erabiltzailea = _db.Login(emaila, pasahitza);
            if (erabiltzailea != null)
            {
                _loginBlokeoZerbitzua.Berrezarri();
                return new LoginEmaitza
                {
                    Erabiltzailea = erabiltzailea,
                    Egoera = _loginBlokeoZerbitzua.LortuEgoera()
                };
            }

            return new LoginEmaitza
            {
                Egoera = _loginBlokeoZerbitzua.ErregistratuHutsegitea()
            };
        }

        public LoginSegurtasunEgoera LortuLoginBlokeoEgoera()
        {
            return _loginBlokeoZerbitzua.LortuEgoera();
        }

        // ------------------------LORTU------------------------------------

        /// <summary>
        /// Osasun langile bati esleitutako pazienteen zerrenda lortzen du.
        /// </summary>
        public List<Pazientea> LortuLangilearenPazienteak(int langileId, string? bilatzailea = null, string? egoeraFiltroa = null)

        {
            return _db.LortuLangilearenPazienteak(langileId, bilatzailea, egoeraFiltroa);
        }

        /// <summary>
        /// Sistema osoko paziente guztien zerrenda lortzen du.
        /// </summary>
        public List<Pazientea> LortuGuztiakPazienteak(string? bilatzailea = null, string? egoeraFiltroa = null)
        {
            return _db.LortuGuztiakPazienteak(bilatzailea, egoeraFiltroa);
        }

        /// <summary>
        /// Sistema osoko osasun langile guztien zerrenda lortzen du.
        /// </summary>
        public List<OsasunLangilea> LortuGuztiakOsasunLangileak(string? bilatzailea = null)
        {
            return _db.LortuGuztiakOsasunLangileak(bilatzailea);
        }

        public Pazientea? LortuPazientea(int pazienteId)
        {
            return _db.LortuPazientea(pazienteId);
        }

        public OsasunLangilea? LortuOsasunLangilea(int osasunLangileId)
        {
            return _db.LortuOsasunLangilea(osasunLangileId);
        }

        /// <summary>
        /// Sistema osoko harrerako langile guztien zerrenda lortzen du.
        /// </summary>
        public List<HarrerakoLangilea> LortuGuztiakHarrerakoak(string? bilatzailea = null)
        {
            return _db.LortuGuztiakHarrerakoak(bilatzailea);
        }

        public HarrerakoLangilea? LortuHarrerakoa(int harrerakoaId)
        {
            return _db.LortuHarrerakoa(harrerakoaId);
        }

        public List<OsasunLangilea> LortuPazientearenOsasunLangileak(int pazienteId)
        {
            return _db.LortuPazientearenOsasunLangileak(pazienteId);
        }

// ------------------------SORTU------------------------------------

        public bool SortuPazientea(Pazientea p)
        {
            return _db.SortuPazientea(p);
        }

        public bool SortuPazientea(Pazientea p, IEnumerable<int> osasunLangileIds, string? irudiBidea)
        {
            return _db.SortuPazientea(p, osasunLangileIds, irudiBidea);
        }

        public bool SortuOsasunLangilea(OsasunLangilea m)
        {
            return _db.SortuOsasunLangilea(m);
        }

        public bool SortuOsasunLangilea(OsasunLangilea m, string? irudiBidea)
        {
            return _db.SortuOsasunLangilea(m, irudiBidea);
        }

        public bool SortuHarrerakoa(HarrerakoLangilea h)
        {
            return _db.SortuHarrerakoa(h);
        }

        public bool SortuHarrerakoa(HarrerakoLangilea h, string? irudiBidea)
        {
            return _db.SortuHarrerakoa(h, irudiBidea);
        }

        public bool EsleituOsasunLangileakPazienteari(int pazienteId, IEnumerable<int> osasunLangileIds)
        {
            return _db.EsleituOsasunLangileakPazienteari(pazienteId, osasunLangileIds);
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

        public bool AldatuPazientearenEgoera(int pazienteId, string egoeraBerria)
        {
            return _db.AldatuPazientearenEgoera(pazienteId, egoeraBerria);
        }
    }
}
