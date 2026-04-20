using System.Collections.Generic;
using GOsasun_app.Modeloa;
using GOsasun_app.Repositorioa;

namespace GOsasun_app.Kontrola
{
    public class OsasunLangileKontrolatzailea
    {
        private readonly OsasunLangileaDB _osasunLangileaDb = new OsasunLangileaDB();

        public List<OsasunLangilea> LortuGuztiakOsasunLangileak(string? bilatzailea = null)
        {
            return _osasunLangileaDb.LortuGuztiakOsasunLangileak(bilatzailea);
        }

        public OsasunLangilea? LortuOsasunLangilea(int osasunLangileId)
        {
            return _osasunLangileaDb.LortuOsasunLangilea(osasunLangileId);
        }

        public bool SortuOsasunLangilea(OsasunLangilea m)
        {
            return _osasunLangileaDb.SortuOsasunLangilea(m);
        }

        public bool SortuOsasunLangilea(OsasunLangilea m, string? irudiBidea)
        {
            return _osasunLangileaDb.SortuOsasunLangilea(m, irudiBidea);
        }

        public bool EzabatuOsasunLangilea(int id)
        {
            return _osasunLangileaDb.EzabatuOsasunLangilea(id);
        }

        public bool EguneratuOsasunLangilea(OsasunLangilea m)
        {
            return _osasunLangileaDb.EguneratuOsasunLangilea(m);
        }
    }
}