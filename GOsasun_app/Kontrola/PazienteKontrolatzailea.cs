using System.Collections.Generic;
using GOsasun_app.Modeloa;
using GOsasun_app.Repositorioa;

namespace GOsasun_app.Kontrola
{
    public class PazienteKontrolatzailea
    {
        private readonly PazienteaDB _pazienteaDb = new PazienteaDB();

        public List<Pazientea> LortuLangilearenPazienteak(int langileId, string? bilatzailea = null, string? egoeraFiltroa = null)
        {
            return _pazienteaDb.LortuLangilearenPazienteak(langileId, bilatzailea, egoeraFiltroa);
        }

        public List<Pazientea> LortuGuztiakPazienteak(string? bilatzailea = null, string? egoeraFiltroa = null)
        {
            return _pazienteaDb.LortuGuztiakPazienteak(bilatzailea, egoeraFiltroa);
        }

        public Pazientea? LortuPazientea(int pazienteId)
        {
            return _pazienteaDb.LortuPazientea(pazienteId);
        }

        public List<OsasunLangilea> LortuPazientearenOsasunLangileak(int pazienteId)
        {
            return _pazienteaDb.LortuPazientearenOsasunLangileak(pazienteId);
        }

        public bool SortuPazientea(Pazientea p)
        {
            return _pazienteaDb.SortuPazientea(p);
        }

        public bool SortuPazientea(Pazientea p, IEnumerable<int> osasunLangileIds, string? irudiBidea)
        {
            return _pazienteaDb.SortuPazientea(p, osasunLangileIds, irudiBidea);
        }

        public bool EsleituOsasunLangileakPazienteari(int pazienteId, IEnumerable<int> osasunLangileIds)
        {
            return _pazienteaDb.EsleituOsasunLangileakPazienteari(pazienteId, osasunLangileIds);
        }

        public bool EzabatuPazientea(int id)
        {
            return _pazienteaDb.EzabatuPazientea(id);
        }

        public bool EguneratuPazientea(Pazientea p)
        {
            return _pazienteaDb.EguneratuPazientea(p);
        }

        public bool EguneratuPazientea(Pazientea p, IEnumerable<int> osasunLangileIds)
        {
            return _pazienteaDb.EguneratuPazientea(p, osasunLangileIds);
        }

        public bool AldatuPazientearenEgoera(int pazienteId, string egoeraBerria)
        {
            return _pazienteaDb.AldatuPazientearenEgoera(pazienteId, egoeraBerria);
        }
    }
}