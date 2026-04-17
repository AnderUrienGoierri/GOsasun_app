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
        // ---------------------------SORTU OBJETUA----------------------------------------------
        private readonly ErabiltzaileDB _db = new ErabiltzaileDB();
        private readonly LoginBlokeoZerbitzua _loginBlokeoZerbitzua = new LoginBlokeoZerbitzua();

        // ---------------------------LORTU------------------------------------------------------

        /// <summary>
        /// Erabiltzailea datu-basean egiaztatzen du email eta pasahitz bidez.
        /// </summary>
        public LoginEmaitza Login(string emaila, string pasahitza)
        {
            LoginSegurtasunEgoera egoera = _loginBlokeoZerbitzua.LortuEgoera();

            if (egoera.Blokeatuta)
            {
                return new LoginEmaitza
                {
                    Egoera = egoera
                };
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
        public List<OsasunLangilea> LortuGuztiakOsasunLangileak()
        {
            return _db.LortuGuztiakOsasunLangileak();
        }

        public List<OsasunLangilea> LortuPazientearenOsasunLangileak(int pazienteId)
        {
            return _db.LortuPazientearenOsasunLangileak(pazienteId);
        }

        public bool EsleituOsasunLangileakPazienteari(int pazienteId, IReadOnlyCollection<int> langileIds)
        {
            return _db.EsleituOsasunLangileakPazienteari(pazienteId, langileIds);
        }

        public OsasunLangilea? LortuOsasunLangilea(int langileId)
        {
            return _db.LortuOsasunLangilea(langileId);
        }

        public Pazientea? LortuPazientea(int pazienteId)
        {
            return _db.LortuPazientea(pazienteId);
        }

        /// <summary>
        /// Sistema osoko harrerako langile guztien zerrenda lortzen du.
        /// </summary>
        public List<HarrerakoLangilea> LortuGuztiakHarrerakoak()
        {
            return _db.LortuGuztiakHarrerakoak();
        }

        public HarrerakoLangilea? LortuHarrerakoa(int harrerakoId)
        {
            return _db.LortuHarrerakoa(harrerakoId);
        }

// ------------------------SORTU------------------------------------

        public bool SortuPazientea(Pazientea p)
        {
            return _db.SortuPazientea(p);
        }

        public bool SortuPazientea(Pazientea p, int langileId)
        {
            return _db.SortuPazientea(p, langileId);
        }

        public bool SortuPazientea(Pazientea p, IReadOnlyCollection<int> langileIds, string? irudiIturria = null)
        {
            return _db.SortuPazientea(p, langileIds, irudiIturria);
        }

        public bool SortuPazientea(Pazientea p, string? irudiIturria)
        {
            return _db.SortuPazientea(p, irudiIturria);
        }

        public bool SortuPazientea(Pazientea p, int langileId, string? irudiIturria)
        {
            return _db.SortuPazientea(p, langileId, irudiIturria);
        }

        public bool SortuOsasunLangilea(OsasunLangilea m)
        {
            return _db.SortuOsasunLangilea(m);
        }

        public bool SortuOsasunLangilea(OsasunLangilea m, string? irudiIturria)
        {
            return _db.SortuOsasunLangilea(m, irudiIturria);
        }

        public bool SortuHarrerakoa(HarrerakoLangilea h)
        {
            return _db.SortuHarrerakoa(h);
        }

        public bool SortuHarrerakoa(HarrerakoLangilea h, string? irudiIturria)
        {
            return _db.SortuHarrerakoa(h, irudiIturria);
        }

// ------------------------EZABATU------------------------------------

        public bool EzabatuPazientea(int id)
        {
            return _db.EzabatuPazientea(id);
        }

        public bool AldatuPazientearenEgoera(int pazienteId, string egoeraBerria)
        {
            return _db.AldatuPazientearenEgoera(pazienteId, egoeraBerria);
        }

        public bool EguneratuPazientea(Pazientea p)
        {
            return _db.EguneratuPazientea(p);
        }
    }
}
